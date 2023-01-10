using UnityEngine;

[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class PlayerTrail : MonoBehaviour
{

    public PlayerController player;

    void Update()
    {
        var lines = player.GetDrawingLinesInclLive().ToArray();
        GetComponent<MeshFilter>().mesh = DynamicLines.GetMesh(lines, 0.01f);
    }
}