using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance;
    
    [HideInInspector] public float width;
    [HideInInspector] public float height;

    public GameObject map;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }
        instance = this;
        width = map.GetComponent<SpriteRenderer>().bounds.size.x;
        height = map.GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Start()
    {
        
    }
}