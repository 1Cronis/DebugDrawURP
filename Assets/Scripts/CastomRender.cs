using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;


namespace CustomRender
{
    public class CastomRender : MonoBehaviour
    {
        [SerializeField] CommandBufferTest CommandBuffer;
        [SerializeField] CustomGraphicsRenderer Graphics;
        [SerializeField] Material material;
        [SerializeField] Mesh mesh;
        [SerializeField] Mesh mesh2;
        [SerializeField] float size = 0.25f;
        [SerializeField] int Fps;

        public bool customGraphicsON = false;
        public bool CommandBufferON = true;


        private void Update()
        {
            Application.targetFrameRate = Fps;


            //Debug.Log("Update");
            if (customGraphicsON)
            {
                Graphics.Draw(transform.position + Vector3.right * 5f, mesh, material);
                Graphics.Draw(transform.position + Vector3.right * 10f, mesh2, material);
                Graphics.Draw(transform.position + Vector3.right * 15f, mesh, material);
                Graphics.Draw(transform.position + Vector3.right * 20f, mesh2, material);
                Graphics.Draw(transform.position + Vector3.right * 25f, mesh, material);
                Graphics.Draw(transform.position + Vector3.right * 30f, mesh2, material);
            }
            else if (CommandBufferON)
            {
                CommandBuffer.Draw(transform.position + Vector3.right * 10f, mesh2, material);
                CommandBuffer.Draw(transform.position + Vector3.right * 15f, mesh, material);
                CommandBuffer.Draw(transform.position + Vector3.right * 20f, mesh2, material);
                CommandBuffer.Draw(transform.position + Vector3.right * 25f, mesh, material);
                CommandBuffer.Draw(transform.position + Vector3.right * 30f, mesh2, material);
            }


        }

        private void FixedUpdate()
        {

            //Debug.Log("FixedUpdate");
            if (customGraphicsON)
            {
                Graphics.Draw(transform.position + Vector3.left * 5f, mesh, material);
                Graphics.Draw(transform.position + Vector3.left * 10f, mesh2, material);
                Graphics.Draw(transform.position + Vector3.left * 15f, mesh, material);
                Graphics.Draw(transform.position + Vector3.left * 20f, mesh2, material);
                Graphics.Draw(transform.position + Vector3.left * 25f, mesh, material);
                Graphics.Draw(transform.position + Vector3.left * 30f, mesh2, material);
            }
            else if (CommandBufferON)
            {
                CommandBuffer.Draw(transform.position + Vector3.left * 5f, mesh, material);
                CommandBuffer.Draw(transform.position + Vector3.left * 10f, mesh2, material);
                CommandBuffer.Draw(transform.position + Vector3.left * 15f, mesh, material);
                CommandBuffer.Draw(transform.position + Vector3.left * 20f, mesh2, material);
                CommandBuffer.Draw(transform.position + Vector3.left * 25f, mesh, material);
                CommandBuffer.Draw(transform.position + Vector3.left * 30f, mesh2, material);
            }
        }

    }




}
 