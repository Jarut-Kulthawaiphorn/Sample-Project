using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using Utilities2D.Polygon2DTriangulation;

[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class RectEffector : MonoBehaviour
{
    Mesh effectMesh;
    Material drawfxMat;
    [SerializeField] Material burnMat;
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;

    Vector3[] vertices;
    Vector2[] uv;
    int[] triangles;

    float z = 0.03f;

    float mapWidth;
    float mapHeight;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();

        //vertices = new Vector3[0];
        //uv = new Vector2[0];
        //triangles = new int[0];

        effectMesh = new Mesh();

        mapWidth = GManager.instance.width;
        mapHeight = GManager.instance.height;

        vertices = new Vector3[0];
        uv = new Vector2[0];
        triangles = new int[0];
    }

    public void AddRect(List<Rect> rects, bool isEnd)
    {
        if (rects != null)
        {
            System.Array.Resize(ref vertices, vertices.Length + rects.Count * 4);
            System.Array.Resize(ref uv, uv.Length + rects.Count * 4);
            System.Array.Resize(ref triangles, triangles.Length + rects.Count * 6);

            Debug.Log(vertices.Length);
        }

        for (int i = 0; i < rects.Count; i++)
        {
            vertices[i * 4 + 0].Set(rects[i].xMax, rects[i].yMax, z);
            vertices[i * 4 + 1].Set(rects[i].xMax, rects[i].yMin, z);
            vertices[i * 4 + 2].Set(rects[i].xMin, rects[i].yMax, z);
            vertices[i * 4 + 3].Set(rects[i].xMin, rects[i].yMin, z);

            uv[i * 4 + 0].Set((rects[i].xMax + mapWidth / 2) / mapWidth, (rects[i].yMax + mapHeight / 2) / mapHeight);
            uv[i * 4 + 1].Set((rects[i].xMax + mapWidth / 2) / mapWidth, (rects[i].yMin + mapHeight / 2) / mapHeight);
            uv[i * 4 + 2].Set((rects[i].xMin + mapWidth / 2) / mapWidth, (rects[i].yMax + mapHeight / 2) / mapHeight);
            uv[i * 4 + 3].Set((rects[i].xMin + mapWidth / 2) / mapWidth, (rects[i].yMin + mapHeight / 2) / mapHeight);

            triangles[i * 6 + 0] = (4 * i + 0);
            triangles[i * 6 + 1] = (4 * i + 1);
            triangles[i * 6 + 2] = (4 * i + 2);
            triangles[i * 6 + 3] = (4 * i + 2);
            triangles[i * 6 + 4] = (4 * i + 1);
            triangles[i * 6 + 5] = (4 * i + 3);
        }

        effectMesh.vertices = vertices;
        effectMesh.uv = uv;
        effectMesh.triangles = triangles;
        effectMesh.RecalculateNormals();
        meshFilter.mesh = effectMesh;

        vertices = new Vector3[vertices.Length];
        uv = new Vector2[uv.Length];
        triangles = new int[triangles.Length];

        if (!isEnd)
        {
            DrawnFX();
        }
        else
        {
            BurnFX();
        }
    }

    public void DrawnFX()
    {
        meshRenderer.material.DOFade(1, 0);
        meshRenderer.material.DOFade(0, 2);
    }


    public void BurnFX()
    {
        meshRenderer.material = burnMat;
        float value = -0.1f;
        DOTween.To(() => value, x => value = x, 1, 2f).OnUpdate(() => meshRenderer.material.SetFloat("_FadeAmount", value));
    }
}