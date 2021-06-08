using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject vibrationOn;
    public GameObject vibrationOff;
    private GameObject highscore;
    void Start()
    {
        if (PlayerPrefs.GetInt("Vibration") == 0)
        {
            vibrationOff.SetActive(true);
            vibrationOn.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Vibration") == 1)
        {
            vibrationOn.SetActive(true);
            vibrationOff.SetActive(false);
        }
            
        highscore = GameObject.Find("Highscores");
        if (PlayerPrefsSafe.HasKey("PlayerName"))
        {
            highscore.GetComponent<Highscores>().Awake();
            highscore.GetComponent<Highscores>().AddNewHighscore(PlayerPrefs.GetString("PlayerName"), PlayerPrefsSafe.GetInt("HighScore"));

            highscore.GetComponent<Highscores>().change = 2;
            highscore.GetComponent<Highscores>().Awake();
            highscore.GetComponent<Highscores>().AddNewHighscore(PlayerPrefs.GetString("PlayerName"), PlayerPrefsSafe.GetInt("HighScore50"));

            highscore.GetComponent<Highscores>().change = 3;
            highscore.GetComponent<Highscores>().Awake();
            highscore.GetComponent<Highscores>().AddNewHighscore(PlayerPrefs.GetString("PlayerName"), PlayerPrefsSafe.GetInt("HighScore100"));

            highscore.GetComponent<Highscores>().change = 0;
        }
    }
    public void On30Click()
    {
        PlayerPrefsSafe.SetInt("Level", 30);
        SceneManager.LoadScene("30Seconds");
    }

    public void On50Click()
    {
        PlayerPrefsSafe.SetInt("Level", 50);
        SceneManager.LoadScene("30Seconds");
    }

    public void OnRunningModeClick()
    {
        PlayerPrefsSafe.SetInt("Level", 100);
        SceneManager.LoadScene("RunningMode");
    }

    public void Vibration()
    {
        if(PlayerPrefs.GetInt("Vibration") == 0)
        {
            vibrationOff.SetActive(false);
            vibrationOn.SetActive(true);
            PlayerPrefs.SetInt("Vibration", 1);
        }

        else if (PlayerPrefs.GetInt("Vibration") == 1)
        {
            vibrationOn.SetActive(false);
            vibrationOff.SetActive(true);
            PlayerPrefs.SetInt("Vibration", 0);
        }
    }
}
