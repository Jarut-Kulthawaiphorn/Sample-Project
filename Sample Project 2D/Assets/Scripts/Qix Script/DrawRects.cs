using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class DrawRects : MonoBehaviour
{
    static List<Rect> rects;
    static List<Rect> newRectList;
    static bool newRects = true;

    Vector3[] vertices;
    Vector2[] uv;
    int[] triangles;

    Mesh mesh;
    float score = 0.0f;

    public static Rect hitRect;

    List<Rect> sourceRects;
    float z = 0.03f;

    int verticesOffset;
    int uvOffset;
    int triangleOffset;

    float mapWidth;
    float mapHeight;

    void Start()
    {
        rects = new List<Rect>();
        init();
    }

    void init()
    {
        mesh = new Mesh();

        vertices = new Vector3[rects.Count * 4];
        uv = new Vector2[rects.Count * 4];
        triangles = new int[rects.Count * 6];

        verticesOffset = 0;
        uvOffset = 0;
        triangleOffset = 0;

        sourceRects = rects;

        score = 0.0f;

        mapWidth = CreateGridBackground.instance.width;
        mapHeight = CreateGridBackground.instance.heigh;
    }

    void Update()
    {
        if (newRects)
        {
            NewWay();
        }
    }


    private void NewWay()
    {
        
        if (newRectList != null)
        {

            verticesOffset = vertices.Length;
            uvOffset = uv.Length;
            triangleOffset = triangles.Length;

            System.Array.Resize(ref vertices, vertices.Length + newRectList.Count * 4);
            System.Array.Resize(ref uv, uv.Length + newRectList.Count * 4);
            System.Array.Resize(ref triangles, triangles.Length + newRectList.Count * 6);

            this.sourceRects = newRectList;
        }

        for (int i = 0; i < this.sourceRects.Count; i++)
        {
            vertices[verticesOffset + i * 4 + 0] = new Vector3(sourceRects[i].xMax, sourceRects[i].yMax, z);
            vertices[verticesOffset + i * 4 + 1] = new Vector3(sourceRects[i].xMax, sourceRects[i].yMin, z);
            vertices[verticesOffset + i * 4 + 2] = new Vector3(sourceRects[i].xMin, sourceRects[i].yMax, z);
            vertices[verticesOffset + i * 4 + 3] = new Vector3(sourceRects[i].xMin, sourceRects[i].yMin, z);

            uv[uvOffset + i * 4 + 0] = new Vector2((sourceRects[i].xMax + mapWidth / 2) / mapWidth, (sourceRects[i].yMax + mapHeight / 2) / mapHeight);
            uv[uvOffset + i * 4 + 1] = new Vector2((sourceRects[i].xMax + mapWidth / 2) / mapWidth, (sourceRects[i].yMin + mapHeight / 2) / mapHeight);
            uv[uvOffset + i * 4 + 2] = new Vector2((sourceRects[i].xMin + mapWidth / 2) / mapWidth, (sourceRects[i].yMax + mapHeight / 2) / mapHeight);
            uv[uvOffset + i * 4 + 3] = new Vector2((sourceRects[i].xMin + mapWidth / 2) / mapWidth, (sourceRects[i].yMin + mapHeight / 2) / mapHeight);

            triangles[triangleOffset + i * 6 + 0] = (verticesOffset + 4 * i + 0);
            triangles[triangleOffset + i * 6 + 1] = (verticesOffset + 4 * i + 1);
            triangles[triangleOffset + i * 6 + 2] = (verticesOffset + 4 * i + 2);
            triangles[triangleOffset + i * 6 + 3] = (verticesOffset + 4 * i + 2);
            triangles[triangleOffset + i * 6 + 4] = (verticesOffset + 4 * i + 1);
            triangles[triangleOffset + i * 6 + 5] = (verticesOffset + 4 * i + 3);

            score += (sourceRects[i].width * sourceRects[i].height);
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        ScoreManager.Score = score;

        GetComponent<MeshFilter>().mesh = mesh;

        newRects = false;
    }

    public static void AddRects(List<Rect> rects)
    {
        DrawRects.rects.AddRange(rects);
        DrawRects.newRectList = rects;
        newRects = true;
    }


    public static bool InRects(Vector3 point)
    {
        bool rc = false;

        foreach (Rect rect in rects)
        {
            if (rect.Contains(point))
            {
                hitRect = rect;
                rc = true;
                break;
            }
        }

        return rc;
    }
}
