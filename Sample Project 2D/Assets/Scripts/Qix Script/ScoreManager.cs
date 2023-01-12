using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public Text guitext;
    int lastScorePer = 0;
    public int target;
    //public bool dead;
    float mapArea;

    [SerializeField] GameObject player1;
    [SerializeField] Text popUpText;
    [SerializeField] Animator popUpTextAnim;
    [SerializeField] GameObject winSign;
    [SerializeField] AudioSource BgAudio;
    [SerializeField] SoundController soundController;
    [SerializeField] Material burnMat;

    // Use this for initialization
    void Start()
    {
        //dead = false;
        target = 80;
        mapArea = GManager.instance.width * GManager.instance.height;
    }

    public void AddScore(float score)
    {
        int scorePer = (int)(100.0f * score / mapArea);
        int additionPer = scorePer - lastScorePer;
        popUpTextAnim.gameObject.SetActive(true);
        
        popUpText.text = "+" + additionPer.ToString();
        lastScorePer = scorePer;

        popUpTextAnim.SetTrigger("PopUp");
        guitext.text = scorePer.ToString("d2");

        if (scorePer >= target)
        {
            StartCoroutine(Complete());
        }

    }

    public IEnumerator Complete()
    {
        //IsEndGame = true;
        BgAudio.DOFade(0, 2);
        player1.SetActive(false);
        if (!winSign.activeSelf)
            soundController.PlaySound(soundController.CompleteSound);
        winSign.SetActive(true);

        var Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var g in Enemy)
        {
            g.SetActive(false);
        }
        var Bullet = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (var g in Bullet)
        {
            g.SetActive(false);
        }
        var items = GameObject.FindGameObjectsWithTag("Item");
        foreach (var g in items)
        {
            g.SetActive(false);
        }
        Invoke("FadeMap", 3);
        yield return new WaitForSeconds(.1f);
        /*
        yield return new WaitForSeconds(3);
        MapParent.gameObject.SetActive(false);
        foreach (var g in ForeGround)
        {
            // g.GetComponent<SpriteRenderer>().DOFade(0, 1);
            // g.SetActive(false);
            Debug.Log(g.name);
            g.GetComponent<SpriteRenderer>().material = BurnMat;
            g.GetComponent<SpriteRenderer>().material.SetFloat("_FadeAmount", 0.5f);
        }
     
        Invoke("Return", 5);
        //Invoke("CompleteStage", 5);*/
    }

    //public void FadeMap()
    //{
    //    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
    //    if (renderer.gameObject.layer != LayerMask.NameToLayer("Effect"))
    //    {
    //        renderer.material = burnMat;
    //    }
    //    else
    //    {
    //        renderer.DOFade(0, 2);
    //    }

    //    float value = -0.1f;
    //    DOTween.To(() => value, x => value = x, 1, 2f).OnUpdate(() => UpdateValue(value, renderer));
    //}

    private void UpdateValue(float value, SpriteRenderer renderer)
    {
        renderer.material.SetFloat("_FadeAmount", value);
    }
}
