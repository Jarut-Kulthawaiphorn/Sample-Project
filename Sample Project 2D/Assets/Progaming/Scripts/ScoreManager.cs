using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public static bool isEnd = false;

    public Text guitext;
    int lastScorePer = 0;
    public int target = 10;
    //public bool dead;
    float mapArea;
    public int scorePer;

    [SerializeField] GameObject player1;
    [SerializeField] Text popUpText;
    [SerializeField] Animator popUpTextAnim;
    [SerializeField] GameObject winSign;
    [SerializeField] AudioSource BgAudio;
    [SerializeField] SoundController soundController;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        //dead = false;
        mapArea = GManager.instance.width * GManager.instance.height;
    }

    public void AddScore(float score)
    {
        scorePer = (int)(100.0f * score / mapArea);
        int additionPer = scorePer - lastScorePer;
        popUpTextAnim.gameObject.SetActive(true);

        popUpText.text = "+" + additionPer.ToString();
        lastScorePer = scorePer;

        popUpTextAnim.SetTrigger("PopUp");
        guitext.text = scorePer.ToString("d2");

        if (scorePer >= target)
        {
            isEnd = true;
            StartCoroutine(Complete());
        }

    }

    public IEnumerator Complete()
    {
        BgAudio.DOFade(0, 2);
        player1.SetActive(false);
        if (!winSign.activeSelf)
            soundController.PlaySound(soundController.CompleteSound);
        winSign.SetActive(true);
        if (ItemMan.instance.currentItem != null)
        {
            ItemMan.instance.currentItem.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(3.1f);
    }
}
