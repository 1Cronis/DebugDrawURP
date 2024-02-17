using System.Collections.Generic;
using UnityEngine;

internal static class DrawUtils
{
    static DrawUtils()
    {
        MeshWireCircle = GetMeshWireCircle();
        MeshWireSemiCircle = GetMeshWireSemiCircle();
        MeshCircle = GetMeshCircle();
        MeshWireCube = GetMeshWireCube();
        MeshCube = GetMeshCube();
        MeshSphere = GetMeshSphere();

        materialsBuffer = GetMaterialBuffer();
    }

    public static Mesh MeshWireCircle { get; private set; }
    public static Mesh MeshWireSemiCircle { get; private set; }
    public static Mesh MeshCircle { get; private set; }
    public static Mesh MeshWireCube { get; private set; }
    public static Mesh MeshCube { get; private set; }
    public static Mesh MeshSphere { get; private set; }

    private static List<Material> materialsBuffer = new List<Material>();

    private static Mesh GetMeshWireCircle(int segments = 100)
    {
        float radius = 1f;
        //int segments = 100;
        var mesh = new Mesh();

        int verticesCount = segments;
        Vector3[] vertices = new Vector3[verticesCount];
        int[] indices = new int[verticesCount * 2];

        for (int i = 0; i < verticesCount; i++)
        {
            float angle = 2 * Mathf.PI * i / segments;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            vertices[i] = new Vector3(x, 0, z);

            // Добавляем индексы для проволочной структуры
            indices[i * 2] = i;
            indices[i * 2 + 1] = (i + 1) % verticesCount;
        }

        mesh.vertices = vertices;
        mesh.SetIndices(indices, MeshTopology.Lines, 0);
        return mesh;
    }

    private static Mesh GetMeshWireSemiCircle(int segments = 100)
    {
        var mesh = new Mesh();
        float radius = 1f;

        int verticesCount = segments + 1;
        Vector3[] vertices = new Vector3[verticesCount];
        int[] indices = new int[segments * 2];

        for (int i = 0; i < verticesCount; i++)
        {
            float angle = Mathf.PI * i / segments;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            vertices[i] = new Vector3(x, 0, z);

            if (i < segments)
            {
                // Добавляем индексы для проволочной структуры
                indices[i * 2] = i;
                indices[i * 2 + 1] = i + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.SetIndices(indices, MeshTopology.Lines, 0);
        return mesh;
    }

    private static Mesh GetMeshCircle(int segments = 100)
    {
        var mesh = new Mesh();
        float radius = 1f;

        int verticesCount = segments * 2;
        Vector3[] vertices = new Vector3[verticesCount];
        int[] triangles = new int[segments * 6];

        for (int i = 0; i < segments; i++)
        {
            float angle = 2 * Mathf.PI * i / segments;
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;

            vertices[i] = new Vector3(x, 0, z);

            vertices[i + segments] = new Vector3(x, 0, z);

            if (i < segments - 1)
            {
                triangles[i * 6] = i;
                triangles[i * 6 + 1] = i + 1;
                triangles[i * 6 + 2] = verticesCount - 1;

                triangles[i * 6 + 3] = i + segments + 1;
                triangles[i * 6 + 4] = i + segments;
                triangles[i * 6 + 5] = verticesCount - 2;
            }
            else
            {
                triangles[i * 6] = i;
                triangles[i * 6 + 1] = 0;
                triangles[i * 6 + 2] = verticesCount - 1;

                triangles[i * 6 + 3] = segments;
                triangles[i * 6 + 4] = i + segments;
                triangles[i * 6 + 5] = verticesCount - 2;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }

    private static Mesh GetMeshWireCube()
    {
        var mesh = new Mesh();
        float size = 1f;

        // Указываем вершины куба
        Vector3[] vertices = new Vector3[8];
        vertices[0] = new Vector3(-size / 2, -size / 2, -size / 2);
        vertices[1] = new Vector3(size / 2, -size / 2, -size / 2);
        vertices[2] = new Vector3(size / 2, -size / 2, size / 2);
        vertices[3] = new Vector3(-size / 2, -size / 2, size / 2);
        vertices[4] = new Vector3(-size / 2, size / 2, -size / 2);
        vertices[5] = new Vector3(size / 2, size / 2, -size / 2);
        vertices[6] = new Vector3(size / 2, size / 2, size / 2);
        vertices[7] = new Vector3(-size / 2, size / 2, size / 2);

        // Указываем индексы для проволочной структуры
        int[] indices = new int[]
        {
            0, 1, 1, 2, 2, 3, 3, 0, // Нижняя грань
            4, 5, 5, 6, 6, 7, 7, 4, // Верхняя грань
            0, 4, 1, 5, 2, 6, 3, 7  // Боковые рёбра
        };

        mesh.vertices = vertices;
        mesh.SetIndices(indices, MeshTopology.Lines, 0);
        return mesh;
    }

    private static Mesh GetMeshCube()
    {
        var mesh = new Mesh();
        float size = 1f;

        Vector3[] vertices = new Vector3[8];
        vertices[0] = new Vector3(-size / 2, -size / 2, -size / 2);
        vertices[1] = new Vector3(size / 2, -size / 2, -size / 2);
        vertices[2] = new Vector3(size / 2, -size / 2, size / 2);
        vertices[3] = new Vector3(-size / 2, -size / 2, size / 2);
        vertices[4] = new Vector3(-size / 2, size / 2, -size / 2);
        vertices[5] = new Vector3(size / 2, size / 2, -size / 2);
        vertices[6] = new Vector3(size / 2, size / 2, size / 2);
        vertices[7] = new Vector3(-size / 2, size / 2, size / 2);

        int[] triangles = new int[]
        {
        0, 1, 2, 0, 2, 3, // Нижняя грань
        4, 6, 5, 4, 7, 6, // Верхняя грань
        0, 4, 1, 1, 4, 5, // Боковые грани
        1, 5, 2, 2, 5, 6,
        2, 6, 3, 3, 6, 7,
        3, 7, 0, 0, 7, 4
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }

    private static Mesh GetMeshSphere()
    {
        var mesh = new Mesh();
        int latitudeSegments = 20;
        int longitudeSegments = 20;
        float radius = 1f;

        int numVertices = (latitudeSegments + 1) * (longitudeSegments + 1);
        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[latitudeSegments * longitudeSegments * 6];

        int index = 0;

        for (int lat = 0; lat <= latitudeSegments; lat++)
        {
            float normalizedLat = lat / (float)latitudeSegments;
            float theta = normalizedLat * Mathf.PI;

            for (int lon = 0; lon <= longitudeSegments; lon++)
            {
                float normalizedLon = lon / (float)longitudeSegments;
                float phi = normalizedLon * 2 * Mathf.PI;

                float x = Mathf.Sin(theta) * Mathf.Cos(phi) * radius;
                float y = Mathf.Cos(theta) * radius;
                float z = Mathf.Sin(theta) * Mathf.Sin(phi) * radius;

                vertices[index] = new Vector3(x, y, z);
                index++;
            }
        }

        index = 0;

        for (int lat = 0; lat < latitudeSegments; lat++)
        {
            for (int lon = 0; lon < longitudeSegments; lon++)
            {
                int current = lat * (longitudeSegments + 1) + lon;
                int next = current + 1;
                int nextRow = (lat + 1) * (longitudeSegments + 1) + lon;
                int nextRowNext = nextRow + 1;

                triangles[index++] = current;
                triangles[index++] = next;
                triangles[index++] = nextRow;

                triangles[index++] = next;
                triangles[index++] = nextRowNext;
                triangles[index++] = nextRow;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        return mesh;
    }

    

    public static void TestBufferMaterial()
    {
        for (int i = 0; i < materialsBuffer.Count; i++)
        {
            Debug.Log($"name {materialsBuffer[i].name}  Color{materialsBuffer[i].color}");
        }
    }

    private static List<Material> GetMaterialBuffer()
    {
        List<Material> mat = new ();

        for (int i = 0; i < 10; i++)
        {
            //var material = new Material(Shader.Find("Custom/UnlitOccluded"));
            var material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            material.SetColor("_Color", Color.red);
            material.SetColor("_OccludedColor", Color.red);

            mat.Add(material);
        }

        return mat;
    }



    public static Material GetMaterial(Color color)
    {
        //засирает память
        // как вариант сделать PoolMaterial  => list с готовыми материалами но пустым цветом 
        // и при вызове GetMaterial мы берем материал и задаем цвет 

        //var material = new Material(Shader.Find("Custom/UnlitOccluded"));
        var material = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        material.SetColor("_BaseColor", color);
        //material.SetColor("_Cutoff", color);
        return material;
    }

    public static Matrix4x4 GetMatrix(Vector3 position, float size, Vector3 normal = default)
    {
        var mSize = Matrix4x4.Scale(new Vector3(size, size, size));

        return GetSize(position,normal) * mSize;
    }

    public static Matrix4x4 GetMatrix(Vector3 position, Vector3 size)
    {
        var mSize = Matrix4x4.Scale(size);

        return GetSize(position) * mSize;
    }

    private static Matrix4x4 GetSize(Vector3 position, Vector3 normal = default)
    {
        var mPosition = Matrix4x4.Translate(position);
        var mRotation = Matrix4x4.Rotate(Quaternion.FromToRotation(Vector3.up,normal));

        return mPosition * mRotation;
    }

}
