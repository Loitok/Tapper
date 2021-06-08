using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScriptRunning : MonoBehaviour
{
    private int startCountDownTime = 3;
    public Text startCountDownDisplay;
    public GameObject startBlackPanel;
    public GameObject gameOverPanel;
    public Animator camAnim;
    private int countdownTime;
    public Text countdownDisplay;
    public Text numberText;
    public Text highscoreText;
    public float animationTime = 1.5f;
    private float desiredNumber;
    private float initialNumber;
    private float currentNumber;
    private GameObject highscore;
    private int score = 0;
    private int scoreCount;
    private int highScore;
    public GameObject gameOverBlack;
    public GameObject gameOver;
    private Shake shake;
    public GameObject adManager;
    private void Start()
    {
        countdownTime = 10;
        startCountDownDisplay.text = "10 seconds for every 40 tips";
        countdownDisplay.text = "10";
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
        highscore = GameObject.Find("Highscores");
        highScore = PlayerPrefsSafe.GetInt("HighScore100");
        highscoreText.text = highScore.ToString("0");
        numberText.text = score.ToString("0");

        StartCoroutine(CountdownToStart());
    }

    
    IEnumerator CountdownToStart()
    {
        yield return new WaitForSeconds(1);

        while (startCountDownTime > 0)
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
            else
                countdownDisplay.color = new Color(243,255,0,255);
        }

        countdownDisplay.text = "0";
        if (!gameOverPanel)
        {
            shake.CamShake();
            gameOverBlack.SetActive(true);
            gameOver.SetActive(true);
            PlayerPrefsSafe.SetInt("LoadingAD", PlayerPrefsSafe.GetInt("LoadingAD") + 1);
            if (PlayerPrefsSafe.GetInt("LoadingAD") == 4)
            {
                adManager.GetComponent<ADManager>().Display_Interstitial();
                PlayerPrefsSafe.SetInt("LoadingAD", 0);
            }
            yield return new WaitForSeconds(1);
            gameOver.SetActive(false);
            gameOverBlack.SetActive(false);
            gameOverPanel.SetActive(true);
        }
    }


    public void SetNumber(float value)
    {
        initialNumber = currentNumber;
        desiredNumber = value;
    }

    public void AddToNumber(int value)
    {
        initialNumber = currentNumber;
        desiredNumber += value;
        score += value;
        scoreCount += value;
    }
    public void Update()
    {

        if (currentNumber != desiredNumber)
        {
            if (initialNumber < desiredNumber)
            {
                currentNumber += (animationTime * Time.deltaTime) * (desiredNumber - initialNumber);
                if (currentNumber >= desiredNumber)
                    currentNumber = desiredNumber;
            }
            else
            {
                currentNumber -= (animationTime * Time.deltaTime) * (initialNumber - desiredNumber);
                if (currentNumber <= desiredNumber)
                    currentNumber = desiredNumber;
            }

            numberText.text = currentNumber.ToString("0");
        }

        ScoreCounter();
    }

    void ScoreCounter()
    {
        if (scoreCount == 40)
        {
            countdownTime += 10;
            scoreCount -= 40;
        }

        PlayerPrefsSafe.SetInt("Score100", score);
        if (highScore < score)
        {
            PlayerPrefsSafe.SetInt("HighScore100", score);
            highScore = score;
            highscore.GetComponent<Highscores>().AddNewHighscore(PlayerPrefs.GetString("PlayerName"), highScore);
        }
    }
       
}
