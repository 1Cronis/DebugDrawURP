using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

namespace CustomDebugDraw
{
    [DefaultExecutionOrder(-10)]
    public class CommandBufferRendering : MonoBehaviour
    {
        private Camera sceneViewCamera;
        private CommandBufferPass commandBufferPass;
        public static System.Action<Mesh, Matrix4x4, Material> renderingEvent;

        private static CommandBufferRendering instance;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoad()
        {
            GameObject gizmoManagerObject = new GameObject("[Command Buffer Rendering]", typeof(CommandBufferRendering));
            instance = gizmoManagerObject.GetComponent<CommandBufferRendering>();
            DontDestroyOnLoad(gizmoManagerObject);
        }

        private void Start()
        {
            commandBufferPass = new CommandBufferPass();
            commandBufferPass.UpdateCommandBuffer = new CommandBuffer();
            commandBufferPass.FixedUpdateCommandBuffer = new CommandBuffer();

            sceneViewCamera = SceneView.lastActiveSceneView?.camera;

            RenderPipelineManager.beginCameraRendering += Draw;
            renderingEvent += AddDrawCommandBuffer;
        }

        private void OnDisable()
        {
            RenderPipelineManager.beginCameraRendering -= Draw;
            renderingEvent -= AddDrawCommandBuffer;
        }

        private void Draw(ScriptableRenderContext context, Camera camera)
        {
            if (sceneViewCamera == null)
                return;

            if (sceneViewCamera == camera)
            {
                var renderer = sceneViewCamera.GetComponent<UniversalAdditionalCameraData>()?.scriptableRenderer;
                if (renderer != null)
                {
                    renderer.EnqueuePass(commandBufferPass);
                }
            }
        }

        private void AddDrawCommandBuffer(Mesh mesh, Matrix4x4 matrix4x4, Material material)
        {
            if (Time.deltaTime == Time.fixedDeltaTime)
                commandBufferPass.FixedUpdateCommandBuffer?.DrawMesh(mesh, matrix4x4, material);
            else
                commandBufferPass.UpdateCommandBuffer?.DrawMesh(mesh, matrix4x4, material);

        }

        private void FixedUpdate() => ClearBufferFixedUpdate();

        private void Update() => ClearBufferUpdate();   

        private void ClearBufferFixedUpdate()
        {
            commandBufferPass?.FixedUpdateCommandBuffer?.Clear();
            MaterialPullComponent.UpdateListMaterialFixedUpdate();
        }

        private void ClearBufferUpdate()
        {
            commandBufferPass?.UpdateCommandBuffer?.Clear();
            MaterialPullComponent.UpdateListMaterialUpdate();
        }
    }

    
}