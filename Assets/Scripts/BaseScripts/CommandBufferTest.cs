using System.Drawing;
using System.Drawing.Drawing2D;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.XR;

namespace CustomRender
{
    //[ExecuteInEditMode]
    [DefaultExecutionOrder(-1)]
    public sealed class CommandBufferTest: MonoBehaviour
    {
        //public Material material;
        //public Mesh mesh;
        //public float size = 0.25f;

        private Camera sceneViewCamera;
        private Pass pass;

        private void Start()
        {
            pass = new Pass();
            pass.UpdateCommandBuffer = new CommandBuffer();
            pass.FixedUpdateCommandBuffer = new CommandBuffer();

            sceneViewCamera = SceneView.lastActiveSceneView?.camera;

            RenderPipelineManager.beginCameraRendering += Draw;

        }

        private void OnDisable()
        {
            RenderPipelineManager.beginCameraRendering -= Draw;
        }

        private void Draw(ScriptableRenderContext context, Camera camera)
        {
            if (sceneViewCamera == null)
                return; // ≈сли камера не найдена, выходим из метода

            if (sceneViewCamera == camera)
            {
                var renderer = sceneViewCamera.GetComponent<UniversalAdditionalCameraData>()?.scriptableRenderer;
                if (renderer != null)
                {
                    renderer.EnqueuePass(pass);
                }
            }
        }

        public void Draw(Vector3 pos,Mesh mesh,Material material)
        {
            var m = Matrix4x4.TRS(pos, Quaternion.identity, Vector3.one);
            if (Time.deltaTime == Time.fixedDeltaTime)
            {
                //pass.FixedUpdateClear = true;
                pass.FixedUpdateCommandBuffer?.DrawMesh(mesh, m, material);
                
            }
            else
            {
                pass.UpdateCommandBuffer?.DrawMesh(mesh, m, material);
                //pass.UpdateClear = true;
                
            }
        }


        private void FixedUpdate() => pass.FixedUpdateCommandBuffer?.Clear();

        private void Update() => pass.UpdateCommandBuffer?.Clear();


    }



    class Pass : ScriptableRenderPass
    {
        public CommandBuffer UpdateCommandBuffer;
        public CommandBuffer FixedUpdateCommandBuffer;

        //public bool FixedUpdateClear;
        //public bool UpdateClear;

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            context.ExecuteCommandBuffer(UpdateCommandBuffer);
            context.ExecuteCommandBuffer(FixedUpdateCommandBuffer);
            //FixedUpdateCommandBuffer?.Clear();
            //UpdateCommandBuffer?.Clear();
            //if (FixedUpdateClear)
            //{
            //    FixedUpdateCommandBuffer?.Clear();
            //    FixedUpdateClear = false;
            //}


            //if (UpdateClear)
            //{
            //    UpdateCommandBuffer?.Clear();
            //    UpdateClear = false;
            //}


        }

        //public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        //{
        //    //commandBuffer = CommandBufferPool.Get("DrawMesh");
        //    //commandBuffer.Clear();
        //    //CommandBuffer commandBuffer = new CommandBuffer();
        //    // ћожете установить дополнительные свойства или матрицу здесь, если необходимо.
        //    context.ExecuteCommandBuffer(commandBuffer);
        //    //CommandBufferPool.Release(commandBuffer);
        //}
    }
}
