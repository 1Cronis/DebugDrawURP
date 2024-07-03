using UnityEngine;



namespace CustomRender
{
    //[ExecuteInEditMode]
    public class CustomRender : MonoBehaviour
    {
        [SerializeField] CommandBufferTest CommandBuffer;
        //[SerializeField] CustomGraphicsRenderer Graphics;
        [SerializeField] Material material;
        [SerializeField] Mesh mesh;
        [SerializeField] Mesh mesh2;
        [SerializeField] float size = 0.25f;
        [SerializeField] int Fps;

        public bool customGraphicsON = false;
        public bool CommandBufferON = true;
        public bool AddList = false;
        //public int ListIndex = 0;
        //public List<Material> test = new();

        private void Update()
        {
            Application.targetFrameRate = Fps;
            //Debug.Log($"{test.Count}");
            //if (test[ListIndex] == null)
            //{
            //    Debug.Log($"{test[ListIndex]}");
            //}
            //if (AddList)
            //{
            //    AddList = false;
            //    test.Add(new Material(Shader.Find("Custom/CustomRender")));
            //    Debug.Log($"{test[ListIndex]}");
            //}

            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawSphere(transform.position + Vector3.right * 5f, 1f, Color.blue, Color.yellow);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green, Color.red);
            //DebugDraw.DrawCube(transform.position + Vector3.right * 10f, Vector3.one, Color.green,Color.red);
            if (AddList)
            {
                DebugDraw.DrawSphere(transform.position + Vector3.right * 5f, 1f, Color.blue,Color.yellow);
                DebugDraw.DrawCube(transform.position + Vector3.right * 20f, Vector3.one, Color.green, Color.cyan);
                DebugDraw.DrawCube(transform.position + Vector3.right * 20f, Vector3.one, Color.green, Color.cyan);
                DebugDraw.DrawCube(transform.position + Vector3.right * 20f, Vector3.one, Color.green, Color.cyan);
                DebugDraw.DrawCube(transform.position + Vector3.right * 20f, Vector3.one, Color.green, Color.cyan);
            }



        }

        private void FixedUpdate()
        {

            DebugDraw.DrawSphere(transform.position + Vector3.left * 5f, 1f, Color.red);
            DebugDraw.DrawCube(transform.position + Vector3.left * 10f, Vector3.one, Color.cyan);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawSphere(transform.position + Vector3.left * 5f, 1f, Color.red);
            DebugDraw.DrawCube(transform.position + Vector3.left * 10f, Vector3.one, Color.cyan);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f, Vector3.one, Color.red);
            DebugDraw.DrawWireCube(transform.position + Vector3.left * 15f,Vector3.one,Color.red);
        }

    }




}
 