using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CustomGizmoDrawer : MonoBehaviour
{
    [SerializeField] Mesh mesh;
    [SerializeField] Material material;
    [SerializeField] Color gizmoColor = Color.white;

    private Camera sceneViewCamera;

    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += DrawGizmos;
    }

    private void OnDisable()
    {
        RenderPipelineManager.beginCameraRendering -= DrawGizmos;
    }

    void DrawGizmos(ScriptableRenderContext context, Camera camera)
    {
        if (mesh == null || material == null)
            return;

        // ���������, ���� ������ �� ���� ����� ��� ���� ��������
        if (sceneViewCamera == null)
        {
            sceneViewCamera = SceneView.lastActiveSceneView?.camera;
            if (sceneViewCamera == null)
                return; // ���� ������ �� �������, ������� �� ������
        }

        CommandBuffer cb = new CommandBuffer();
        // �� ���������� ����� AsyncCompute � AsyncGraphics
        cb.SetExecutionFlags(CommandBufferExecutionFlags.None);

        // ������������ ������� �������������� �������
        Matrix4x4 matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

        // ������������� ������� �������������� ��� ��������� �������
        cb.SetGlobalMatrix("_LocalToWorldMatrix", matrix);

        // ������������� ���� �������
        cb.SetGlobalColor("_Color", gizmoColor);

        // ������ ��� � �������������� ��������� � ������� ��������������
        cb.DrawMesh(mesh, matrix, material);

        context.ExecuteCommandBuffer(cb);
        CommandBufferPool.Release(cb);
    }

}
