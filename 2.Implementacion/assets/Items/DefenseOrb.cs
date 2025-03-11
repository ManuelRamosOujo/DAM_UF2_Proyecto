using UnityEngine;

public class DefenseOrb : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Mesh mesh;
    private int segments = 50;
    private float radius = 0.5f;
    private float speed = 0.5f;
    private float amplitude = 0.2f;

    void Start(){
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Sprites/Default"));
        meshRenderer.material.color = new Color(0,0.8353f,1);

        mesh = new Mesh();
        meshFilter.mesh = mesh;
        CreateCircleMesh();
        meshRenderer.sortingLayerName = "Objects";
        meshRenderer.sortingOrder = 19565;
    }

    void Update(){
        float yOffset = Mathf.Sin(Time.time * speed) * amplitude;
        transform.localPosition = new Vector3(0,0.45f + yOffset, -1);

        float t = Mathf.PingPong(Time.time * 0.3f, 1);
        meshRenderer.material.color = Color.Lerp(new Color(0,0.8353f,1), Color.white, t);
    }

    void CreateCircleMesh(){
        Vector3[] vertices = new Vector3[segments + 1];
        int[] triangles = new int[segments * 3];

        vertices[0] = Vector3.zero;
        float angle = 0f;

        for (int i = 1; i <= segments; i++)
        {
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            vertices[i] = new Vector3(x, y, 0);
            angle += 2 * Mathf.PI / segments;
        }

        for (int i = 0; i < segments; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = (i + 2 > segments) ? 1 : i + 2;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
