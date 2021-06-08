using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CountDownController : MonoBehaviour
{
    private int startCountDownTime = 3;
    public Text startCountDownDisplay;
    public GameObject startBlackPanel;
    public GameObject blackPanel;
    public GameObject gameOverPanel;
    public Animator camAnim;
    public int countdownTime;
    public Text countdownDisplay;
    public GameObject gameOver;
    private Shake shake;
    public GameObject adManager;
    private void Start()
    {
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
        
        if (PlayerPrefsSafe.GetInt("Level") == 30)
        {
            countdownTime = 30;
            startCountDownDisplay.text = "Only 30 seconds";
            countdownDisplay.text = "30";
        }

        else if (PlayerPrefsSafe.GetInt("Level") == 50)
        {
            countdownTime = 50;
            startCountDownDisplay.text = "Only 50 seconds";
            countdownDisplay.text = "50";
        }
        StartCoroutine(CountdownToStart());
    }
    IEnumerator CountdownToStart()
    {
        yield return new WaitForSeconds(1);

        while(startCountDownTime > 0)
        {
            camAnim.SetTrigger("starting");
            startCountDownDisplay.text = startCountDownTime.ToString();   
            yield return new WaitForSeconds(1);       
            startCountDownTime--;
        }
        camAnim.SetTrigger("starting");
        startCountDownDisplay.color = Color.green;
        startCountDownDisplay.text = "Go!";
        startBlackPanel.SetActive(false);
        StartCoroutine(Game());
        yield return new WaitForSeconds(1);
        startCountDownDisplay.gameObject.SetActive(false); 
    }

    IEnumerator Game()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
            if (countdownTime <= 5)
                countdownDisplay.color = Color.red;
        }
        countdownDisplay.text = "0";
        if (!gameOverPanel)
        {
            PlayerPrefsSafe.SetInt("LoadingAD", PlayerPrefsSafe.GetInt("LoadingAD") + 1);
            if (PlayerPrefsSafe.GetInt("LoadingAD") == 4)
            {
                adManager.GetComponent<ADManager>().Display_Interstitial();
                PlayerPrefsSafe.SetInt("LoadingAD", 0);
            }
            shake.CamShake();
            blackPanel.SetActive(true);
            gameOver.SetActive(true);
            yield return new WaitForSeconds(1);
            gameOver.SetActive(false);
            blackPanel.SetActive(false);
            gameOverPanel.SetActive(true);
        }
    }

 
}
