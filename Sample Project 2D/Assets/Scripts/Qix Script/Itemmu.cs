using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AKA Item but Japanses accent
public class Itemmu : MonoBehaviour
{
    public Sprite[] SpList;
    SpriteRenderer spRenderer;
    int index;
    PlayerController player;
    GameManager gameManager;
    public AudioClip GetItemSound;
    ItemMan manager;

    internal void GetItem()
    {
        GetComponent<AudioSource>().PlayOneShot(GetItemSound);
        GetComponent<SpriteRenderer>().enabled = false;
        this.gameObject.SetActive(false);
        GetComponent<Collider2D>().enabled = false;
        player.GetComponent<Player>().SpeedUp();
        Invoke("Reset", 15);
    }
    public void Reset()
    {
        player.GetComponent<Player>().Reset();
        CompleteItem();
    }

    void CompleteItem()
    {
        Destroy(gameObject);
        manager.CompleteItem();
    }
    public void OnPlayerDie()
    {
        Reset();
    }
}
