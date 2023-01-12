using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AKA ItemManager
public class ItemMan : MonoBehaviour
{
    public static ItemMan instance;
    public Itemmu ItemPrefab;
    int index = 0;
    public Itemmu currentItem;
    float counter = 0;

    private void Awake()
    {
        if(instance != null) 
        {
            Destroy(instance);
        }
        instance = this;
    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        RandomSpace();
    }

    void Update()
    {
        // if(currentItem!=null)
        counter += Time.deltaTime;

        if (counter > 20 && currentItem != null)
        {
            counter = 0;
            Destroy(currentItem.gameObject);
            currentItem = null;

        }
        if (counter > 45 && currentItem == null)
        {
            counter = 0;
            if (currentItem == null)
            {
                RandomSpace();
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {

            RandomSpace();
        }
    }

    private void RandomSpace()
    {
        float mapHalfWidth = GManager.instance.width * 0.5f;
        float mapHalfHeight = GManager.instance.height * 0.5f;

        float x = Random.Range(-mapHalfWidth, mapHalfWidth);
        float y = Random.Range(-mapHalfHeight, mapHalfHeight);

        var tmp = Instantiate(ItemPrefab, new Vector3(x, y, 0),Quaternion.identity);

        currentItem = tmp;
    }

    public void CompleteItem()
    {
        counter = 0;
        currentItem = null;
    }

    public void PlayerDie()
    {
        if (currentItem != null)
        {
            currentItem.OnPlayerDie();
        }
    }
}
