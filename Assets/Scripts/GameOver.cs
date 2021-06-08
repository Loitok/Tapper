using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text scoreText;
    public GameObject gameOverPanel;
    public GameObject buttonLeft;
    public GameObject buttonRight;
    public GameObject[] list;
    private float change;
    private int i = 0;
    void Start()
    {
        
        buttonLeft.SetActive(false);
        if (PlayerPrefsSafe.GetInt("Level") == 30)
            change = 30;

        else if (PlayerPrefsSafe.GetInt("Level") == 50)
            change = 50;

        else if (PlayerPrefsSafe.GetInt("Level") == 100)
            change = 100;
        gameOverPanel.SetActive(false);
    }
    void Update()
    {
        if (change == 30)
            scoreText.text = "you scored : " + PlayerPrefsSafe.GetInt("Score");
        else if (change == 50)
            scoreText.text = "you scored : " + PlayerPrefsSafe.GetInt("Score50");
        else if (change == 100)
            scoreText.text = "you scored : " + PlayerPrefsSafe.GetInt("Score100");
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnHome()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnLeftTap()
    {
        if(i == 1)
        {
            list[0].SetActive(true);
            list[1].SetActive(false);
            i = 0;
            buttonLeft.SetActive(false);
        }

        else if(i == 2)
        {
            list[1].SetActive(true);
            buttonRight.SetActive(true);
            list[2].SetActive(false);
            i = 1;
            
        }

    }

    public void OnRightTap()
    {
        if(i == 0)
        {
            list[1].SetActive(true);
            buttonLeft.SetActive(true);
            list[0].SetActive(false);
            i = 1;
            
        }
        else if(i == 1)
        {
            list[2].SetActive(true);
            list[1].SetActive(false);
            i = 2;
            buttonRight.SetActive(false);
        }
    }
}
