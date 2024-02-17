using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class CustomGraphicsRendererV2 : MonoBehaviour
{
    public Material material; // Материал для отображения фигуры
    public Mesh mesh;

    void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    void OnSceneGUI(SceneView sceneView)
    {
        if (material == null)
        {
            Debug.LogError("Material not assigned to CustomShapeRenderer!");
            return;
        }

        if (mesh == null)
        {
            Debug.LogError("No vertices assigned to CustomShapeRenderer!");
            return;
        }

        Camera sceneCamera = SceneView.lastActiveSceneView?.camera;
        if (sceneCamera == null)
        {
            Debug.LogError("Scene camera not found!");
            return;
        }

        DrawShape(sceneCamera,transform.position + Vector3.right * 5f);
        DrawShape(sceneCamera,transform.position + Vector3.right * 10f);
        DrawShape(sceneCamera,transform.position + Vector3.right * 15f);
        DrawShape(sceneCamera,transform.position + Vector3.right * 20f);
        DrawShape(sceneCamera,transform.position + Vector3.right * 25f);
        DrawShape(sceneCamera,transform.position + Vector3.right * 30f);
    }

    void DrawShape(Camera camera,Vector3 pos)
    {
        var m = Matrix4x4.TRS(pos, transform.rotation,Vector3.one);

        Graphics.DrawMesh(mesh,m, material, 0, camera, 0, null, ShadowCastingMode.Off, false, null, false);
    }
}
