using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace CustomRender
{
    //[ExecuteInEditMode]
    [DefaultExecutionOrder(-1)]
    public class CommandBufferTest : MonoBehaviour
    {
        private Camera sceneViewCamera;
        private Pass pass;

        //public static System.Action<Matrix4x4, Mesh, Material> action;

        private void Start()
        {
            pass = new Pass();
            pass.UpdateCommandBuffer = new CommandBuffer();
            pass.FixedUpdateCommandBuffer = new CommandBuffer();

            sceneViewCamera = SceneView.lastActiveSceneView?.camera;

            RenderPipelineManager.beginCameraRendering += Draw;
            //action += InvokeDraw;
        }

        private void OnDisable()
        {
            RenderPipelineManager.beginCameraRendering -= Draw;
            //action -= InvokeDraw;
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
                    renderer.EnqueuePass(pass);
                }
            }
        }

        public void InvokeDraw(Matrix4x4 pos,Mesh mesh,Material material)
        {
            //var m = Matrix4x4.TRS(pos, Quaternion.identity, Vector3.one);
            if (Time.deltaTime == Time.fixedDeltaTime)
                pass.FixedUpdateCommandBuffer?.DrawMesh(mesh, pos, material);
            else
                pass.UpdateCommandBuffer?.DrawMesh(mesh, pos, material);
        }


        private void FixedUpdate() => pass?.FixedUpdateCommandBuffer?.Clear();

        private void Update() => pass?.UpdateCommandBuffer?.Clear();
    }

    public class Pass : ScriptableRenderPass
    {
        public CommandBuffer UpdateCommandBuffer;
        public CommandBuffer FixedUpdateCommandBuffer;

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            context.ExecuteCommandBuffer(UpdateCommandBuffer);
            UpdateCommandBuffer?.Clear();
            context.ExecuteCommandBuffer(FixedUpdateCommandBuffer);
        }
    }
}
