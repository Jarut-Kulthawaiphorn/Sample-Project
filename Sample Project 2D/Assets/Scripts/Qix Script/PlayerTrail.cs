using UnityEngine;

[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class PlayerTrail : MonoBehaviour
{
    [SerializeField] float width = 0.01f;
    public PlayerController player;

    void Update()
    {
        var lines = player.GetDrawingLinesInclLive().ToArray();
        GetComponent<MeshFilter>().mesh = DynamicLines.GetMesh(lines, width);
    }
}