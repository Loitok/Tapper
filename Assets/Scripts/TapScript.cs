using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TapScript : MonoBehaviour
{
    private GameObject gameMaster;
    private GameObject gameProcess;
    public GameObject gameOverPanel;
    public GameObject gameOverBlack;
    public GameObject gameOver;
    public GameObject adManager;
    public Text floatingPoints;
    private int scoreCount = 1;
    private bool isSmallLevel;
    private bool isVibration;
    private CountDownController countDown;
    private Shake shake;

    void Start()
    {
        if (PlayerPrefs.GetInt("Vibration") == 0)
            isVibration = false;
        else
            isVibration = true;
        floatingPoints.gameObject.SetActive(false);
        if (PlayerPrefsSafe.GetInt("Level") == 30)
        {
            isSmallLevel = true;
            countDown = GameObject.FindGameObjectWithTag("CountDown").GetComponent<CountDownController>();
        }
        else if (PlayerPrefsSafe.GetInt("Level") == 50)
        {
            isSmallLevel = false;
            countDown = GameObject.FindGameObjectWithTag("CountDown").GetComponent<CountDownController>();
        }

            gameMaster = GameObject.FindGameObjectWithTag("GameMaster");
        if (PlayerPrefsSafe.GetInt("Level") != 100)
            gameProcess = GameObject.FindGameObjectWithTag("GameProcess");
        else
            gameProcess = GameObject.Find("Time");
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
    }

    void Update()
    {
        if (PlayerPrefsSafe.GetInt("Level") != 100)
        {
            if (isSmallLevel)
            {
                if (countDown.countdownTime <= 10)
                {
                    scoreCount = 10;
                    floatingPoints.text = "+10";
                }
                else if (countDown.countdownTime > 10 && countDown.countdownTime <= 20)
                {
                    scoreCount = 5;
                    floatingPoints.text = "+5";
                }
                else
                {
                    scoreCount = 1;
                    floatingPoints.text = "+1";
                }
            }
            else if (!isSmallLevel)
            {
                if (countDown.countdownTime <= 10)
                {
                    scoreCount = 10;
                   floatingPoints.text = "+10";
                }
                else if (countDown.countdownTime > 10 && countDown.countdownTime <= 20)
                {
                    scoreCount = 7;
                    floatingPoints.text = "+7";
                }
                else if (countDown.countdownTime > 20 && countDown.countdownTime <= 30)
                {
                    scoreCount = 5;
                    floatingPoints.text = "+5";
                }
                else if (countDown.countdownTime > 30 && countDown.countdownTime <= 40)
                {
                    scoreCount = 3;
                   floatingPoints.text = "+3";
                }
                else
                {
                    scoreCount = 1;
                    floatingPoints.text = "+1";
                }
            }
        }
    }
    public void Click()
    {  
        if (gameObject.GetComponent<Image>().color == Color.white)
        {
            StartCoroutine(IsWhiteTapping());
        }
        else
        {
            gameMaster.GetComponent<GameScript>().ChangePanels();
            if (PlayerPrefsSafe.GetInt("Level") != 100)
                gameProcess.GetComponent<SlidingNumbers>().AddToNumber(scoreCount);
            else
                gameProcess.GetComponent<GameScriptRunning>().AddToNumber(1);
            StartCoroutine(IsBlackTapping());
        }

        if (PlayerPrefsSafe.GetInt("Level") != 100 && countDown.countdownTime == 0)
            gameOverBlack.SetActive(true);
    }

    IEnumerator IsWhiteTapping()
    {
        Handheld.Vibrate();
        shake.CamShake();
        gameOver.SetActive(true);
        gameOverBlack.SetActive(true);
        PlayerPrefsSafe.SetInt("LoadingAD", PlayerPrefsSafe.GetInt("LoadingAD") + 1);
        if (PlayerPrefsSafe.GetInt("LoadingAD") == 4)
        {
            adManager.GetComponent<ADManager>().Display_Interstitial();
            PlayerPrefsSafe.SetInt("LoadingAD", 0);
        }
        gameObject.GetComponent<Image>().color = Color.black;
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponent<Image>().color = Color.white;
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponent<Image>().color = Color.black;
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponent<Image>().color = Color.white;
        yield return new WaitForSeconds(.1f);
        gameObject.GetComponent<Image>().color = Color.black;
        yield return new WaitForSeconds(1);
        gameOverBlack.SetActive(false);
        gameOver.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    IEnumerator IsBlackTapping()
    {
        floatingPoints.gameObject.SetActive(true);
        if(isVibration)
            SoundManager.snd.PlaySounds();
        gameObject.GetComponent<Image>().color = Color.green;
        yield return new WaitForSeconds(.05f);
        gameObject.GetComponent<Image>().color = Color.white;
        yield return new WaitForSeconds(.25f);
        floatingPoints.gameObject.SetActive(false);
    }
}
