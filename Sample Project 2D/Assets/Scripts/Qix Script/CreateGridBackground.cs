using UnityEngine;

[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class CreateGridBackground : MonoBehaviour
{
    public float width;
    public float heigh;

    [HideInInspector] public float x;
    [HideInInspector] public float y;

    public static CreateGridBackground instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }
        instance = this;
        x = width / 2;
        y = heigh / 2;
    }

    void Start()
    {
        //GetComponent<MeshFilter>().mesh = CreatePlaneMesh();
    }

    Mesh CreatePlaneMesh()
    {
        Mesh mesh = new Mesh();
        float z = 0.05f;

        //plane shape
        Vector3[] vertices = new Vector3[]
        {
            new Vector3( x,  y, z),
            new Vector3( x, -y, z),
            new Vector3(-x,  y, z),
            new Vector3(-x, -y, z),
        };

        Vector2[] uv = new Vector2[]
        {
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(0, 0),
        };

        int[] triangles = new int[]
        {
            0, 1, 2,
            2, 1, 3,
        };

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        return mesh;
    }
}