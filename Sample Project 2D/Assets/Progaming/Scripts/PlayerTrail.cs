using UnityEngine;

[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class PlayerTrail : MonoBehaviour
{
    public PlayerController player;
    float width;

    private void Start()
    {
        width = DrawLines.instance.width;
    }

    void Update()
    {
        var lines = player.GetDrawingLinesInclLive().ToArray();
        GetComponent<MeshFilter>().mesh = DynamicLines.GetMesh(lines, width);
    }
}