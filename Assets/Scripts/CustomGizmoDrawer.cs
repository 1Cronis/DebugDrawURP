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

        // Проверяем, если камера из окна сцены уже была получена
        if (sceneViewCamera == null)
        {
            sceneViewCamera = SceneView.lastActiveSceneView?.camera;
            if (sceneViewCamera == null)
                return; // Если камера не найдена, выходим из метода
        }

        CommandBuffer cb = new CommandBuffer();
        // Не используем флаги AsyncCompute и AsyncGraphics
        cb.SetExecutionFlags(CommandBufferExecutionFlags.None);

        // Рассчитываем матрицу преобразования объекта
        Matrix4x4 matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

        // Устанавливаем матрицу преобразования для отрисовки объекта
        cb.SetGlobalMatrix("_LocalToWorldMatrix", matrix);

        // Устанавливаем цвет гизмоса
        cb.SetGlobalColor("_Color", gizmoColor);

        // Рисуем меш с использованием материала и матрицы преобразования
        cb.DrawMesh(mesh, matrix, material);

        context.ExecuteCommandBuffer(cb);
        CommandBufferPool.Release(cb);
    }

}
