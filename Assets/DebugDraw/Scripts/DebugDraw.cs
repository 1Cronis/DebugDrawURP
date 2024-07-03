using CustomDebugDraw;
using System.Buffers;
using UnityEngine;

public class DebugDraw
{
    public static void DrawSphere(Vector3 position, float size, Color color, Color colorW = default)
    {
        //Draw(DrawUtils.MeshSphere, DrawUtils.GetMatrix(position, size), DrawUtils.GetMaterial(color));
        CommandBufferRendering.renderingEvent.Invoke(DrawUtils.MeshSphere, DrawUtils.GetMatrix(position, size), MaterialPullComponent.GetMaterial(color));
        if (colorW != default)
            DrawWireSphere(position, size, colorW);
    }

    public static void DrawWireSphere(Vector3 position, float size, Color color)
    {
        Matrix4x4[] matrix = ArrayPool<Matrix4x4>.Shared.Rent(3);
        matrix[0].SetTRS(position, Quaternion.Euler(Vector3.zero), new Vector3(size, size, size));
        matrix[1].SetTRS(position, Quaternion.Euler(new Vector3(90, 0, 0)), new Vector3(size, size, size));
        matrix[2].SetTRS(position, Quaternion.Euler(new Vector3(0, 0, 90)), new Vector3(size, size, size));

        var material = MaterialPullComponent.GetMaterial(color);
        for (int i = 0; i < matrix.Length; i++)
        {
            //Draw(DrawUtils.MeshWireCircle, matrix[i], DrawUtils.GetMaterial(color));
            CommandBufferRendering.renderingEvent.Invoke(DrawUtils.MeshWireCircle, matrix[i], material);
        }
        ArrayPool<Matrix4x4>.Shared.Return(matrix);
    }

    public static void DrawCube(Vector3 position, Vector3 size, Color color, Color colorW = default)
    {
        //Draw(DrawUtils.MeshCube, DrawUtils.GetMatrix(position, size), DrawUtils.GetMaterial(color));
        CommandBufferRendering.renderingEvent.Invoke(DrawUtils.MeshCube, DrawUtils.GetMatrix(position, size), MaterialPullComponent.GetMaterial(color));
        if (colorW != default)
            DrawWireCube(position, size, colorW);
    }

    public static void DrawWireCube(Vector3 position, Vector3 size, Color color)
    {
        //Draw(DrawUtils.MeshWireCube, DrawUtils.GetMatrix(position, size), DrawUtils.GetMaterial(color));
        CommandBufferRendering.renderingEvent.Invoke(DrawUtils.MeshWireCube, DrawUtils.GetMatrix(position, size), MaterialPullComponent.GetMaterial(color));
    }

    public static void DrawCircle(Vector3 position, Vector3 normal, float size, Color color, Color colorW = default)
    {
        //Draw(DrawUtils.MeshCircle, DrawUtils.GetMatrix(position, size, normal), DrawUtils.GetMaterial(color));
        CommandBufferRendering.renderingEvent.Invoke(DrawUtils.MeshCircle, DrawUtils.GetMatrix(position, size, normal), MaterialPullComponent.GetMaterial(color));
        if (colorW != default)
            DrawWireCircle(position, normal, size, colorW);
    }

    public static void DrawWireCircle(Vector3 position, Vector3 normal, float size, Color color)
    {
        CommandBufferRendering.renderingEvent.Invoke(DrawUtils.MeshWireCircle, DrawUtils.GetMatrix(position, size, normal), MaterialPullComponent.GetMaterial(color));
    }

    public static void Draw(Mesh meshWireCircle, Matrix4x4 matrix4x4, Material material)
    {
        //CommandBufferRendering.renderingEvent.Invoke( meshWireCircle, matrix4x4, material);
        
    }
}


