using DG.Tweening;
using System.Collections;
using UnityEngine;

//AKA GameManager
public class GManager : MonoBehaviour
{
    public static GManager instance;
    public ScoreManager scoreManager;

    [HideInInspector] public float width;
    [HideInInspector] public float height;

    public GameObject map;

    private void Awake()
    {
        if (instance != null)
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