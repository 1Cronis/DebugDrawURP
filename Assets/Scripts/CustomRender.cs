using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;


namespace CustomRender
{
    //[ExecuteInEditMode]
    public class CustomRender : MonoBehaviour
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

            DebugDraw.DrawSphere(transform.position + Vector3.right * 5f, 1f, Color.blue,Color.yellow);
            DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green,Color.red);
           
            
        }

        private void FixedUpdate()
        {

            DebugDraw.DrawSphere(transform.position + Vector3.left * 5f,1f,Color.red);
            DebugDraw.DrawCube(transform.position + Vector3.left * 10f,Vector3.one,Color.cyan);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f,Vector3.one,Color.red);

        }

    }




}
 