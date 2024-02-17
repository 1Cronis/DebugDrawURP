using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;


//[ExecuteInEditMode]
[DefaultExecutionOrder(-1)]
public class CustomGraphicsRenderer : MonoBehaviour
{
    //public Material material;
    //public Mesh mesh;

    private CommandBuffer UpdateCommandBuffer;
    private CommandBuffer FixedUpdateCommandBuffer;

    private Camera sceneViewCamera;

    void OnEnable()
    {
        UpdateCommandBuffer = new();
        FixedUpdateCommandBuffer = new();
        sceneViewCamera = SceneView.lastActiveSceneView?.camera;
        SceneView.duringSceneGui += OnSceneGUI;
        Editor.finishedDefaultHeaderGUI += EditorTest;
    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
        Editor.finishedDefaultHeaderGUI -= EditorTest;
    }

    void EditorTest(Editor editor)
    {
        Debug.Log("EditorTest");
    }

    void OnSceneGUI(SceneView sceneView)
    {
        //Camera sceneCamera = SceneView.lastActiveSceneView?.camera;
        //if (sceneCamera == null)
        //{
        //    Debug.LogError("Scene camera not found!");
        //    return;
        //}

        //var m = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        //Graphics.DrawMesh(mesh, m, material, 0, sceneCamera, 0, null, ShadowCastingMode.Off, false, null, false);
        //Debug.Log("OnSceneGUI");
        if (sceneViewCamera == null)
            return; // ≈сли камера не найдена, выходим из метода

        if (sceneViewCamera == sceneView.camera)
        {
            Graphics.ExecuteCommandBuffer(UpdateCommandBuffer);
            Graphics.ExecuteCommandBuffer(FixedUpdateCommandBuffer);
        }
    }

    private void FixedUpdate() => FixedUpdateCommandBuffer.Clear();

    private void Update() => UpdateCommandBuffer.Clear();

    public void Draw(Vector3 pos, Mesh mesh, Material material)
    {
        var m = Matrix4x4.TRS(pos, Quaternion.identity, Vector3.one);
        if (Time.deltaTime == Time.fixedDeltaTime)
            FixedUpdateCommandBuffer.DrawMesh(mesh, m, material);
        else
            UpdateCommandBuffer.DrawMesh(mesh, m, material);


    }
}


