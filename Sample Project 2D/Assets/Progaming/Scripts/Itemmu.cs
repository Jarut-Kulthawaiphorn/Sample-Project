using UnityEngine;

//AKA Item but Japanses accent
public class Itemmu : MonoBehaviour
{
    //PlayerController player;
    public AudioClip GetItemSound;

    internal void GetItem()
    {
        GetComponent<AudioSource>().PlayOneShot(GetItemSound);
        this.gameObject.SetActive(false);
        PlayerController.instance.SpeedUp();
        Invoke("Reset", 15);
    }
    public void Reset()
    {
        PlayerController.instance.ResetSpeed();
        ItemMan.instance.CompleteItem();
        Destroy(gameObject);
    }
}
