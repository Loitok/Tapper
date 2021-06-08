using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlidingNumbers : MonoBehaviour
{
    public Text numberText;
    public Text highscoreText;
    public float animationTime = 1.5f;

    private float desiredNumber;
    private float initialNumber;
    private float currentNumber;

    private GameObject highscore;

    private int score = 0;
    private int highScore;

    void Start()
    {
        highscore = GameObject.Find("Highscores");

        if (PlayerPrefsSafe.GetInt("Level") == 30)
            highScore = PlayerPrefsSafe.GetInt("HighScore");
        else if (PlayerPrefsSafe.GetInt("Level") == 50)
            highScore = PlayerPrefsSafe.GetInt("HighScore50");

        highscoreText.text = highScore.ToString("0");
        numberText.text = score.ToString("0");
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
        if (PlayerPrefsSafe.GetInt("Level") == 30)
        {
            PlayerPrefsSafe.SetInt("Score", score);
            if (highScore < score)
            {
                PlayerPrefsSafe.SetInt("HighScore", score);
                highScore = score;
                highscore.GetComponent<Highscores>().AddNewHighscore(PlayerPrefs.GetString("PlayerName"), highScore);
            }
        }

        else if (PlayerPrefsSafe.GetInt("Level") == 50)
        {
            PlayerPrefsSafe.SetInt("Score50", score);
            if (highScore < score)
            {
                PlayerPrefsSafe.SetInt("HighScore50", score);
                highScore = score;
                highscore.GetComponent<Highscores>().AddNewHighscore(PlayerPrefs.GetString("PlayerName"), highScore);
            }
            
        }
    }
}
