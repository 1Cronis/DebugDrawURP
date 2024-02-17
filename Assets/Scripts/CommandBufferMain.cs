using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace CustomRenderv2
{
    [ExecuteInEditMode]
    public sealed class CommandBufferMain : MonoBehaviour
    {
        [SerializeField] Material material;
        [SerializeField] Mesh mesh;
        [SerializeField] float size = 0.25f;

        private Camera sceneViewCamera;

        private void OnEnable()
        {
            RenderPipelineManager.beginCameraRendering += Draw;
        }

        private void OnDisable()
        {
            RenderPipelineManager.beginCameraRendering -= Draw;
        }

        void Draw(ScriptableRenderContext context, Camera camera)
        {
            // Проверяем, если камера из окна сцены уже была получена
            if (sceneViewCamera == null)
            {
                sceneViewCamera = SceneView.lastActiveSceneView?.camera;
                if (sceneViewCamera == null)
                    return; // Если камера не найдена, выходим из метода
            }

            // Проверяем, если камера - камера Scene View
            if (sceneViewCamera == camera)
            {
                if (material == null || mesh == null)
                    return;

                material.SetFloat("_Size", size);
                material.SetMatrix("_LocalToWorldMatrix", transform.localToWorldMatrix);

                var renderer = sceneViewCamera.GetComponent<UniversalAdditionalCameraData>()?.scriptableRenderer;
                if (renderer != null)
                {
                    var pass = new Pass
                    {
                        renderPassEvent = RenderPassEvent.BeforeRenderingTransparents,
                        Material = material,
                        Mesh = mesh,
                        Matrix = transform.localToWorldMatrix
                    };

                    renderer.EnqueuePass(pass);
                }
            }
        }
    }

    class Pass : ScriptableRenderPass
    {
        public Matrix4x4 Matrix { get; set; }
        public Material Material { get; set; }
        public Mesh Mesh { get; set; }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cb = CommandBufferPool.Get("DrawMesh");

            // Можете установить дополнительные свойства или матрицу здесь, если необходимо.
            cb.DrawMesh(Mesh, Matrix, Material);

            context.ExecuteCommandBuffer(cb);
            CommandBufferPool.Release(cb);
        }
    }
}
