using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InputWindow : MonoBehaviour
{
    public GameObject ok_button;
    public GameObject cancel_button;
    public GameObject error;
    public TMP_InputField inputField;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
            Hide();
        else
            Show();
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    void Update()
    {
        if (!PlayerPrefs.HasKey("PlayerName"))
            cancel_button.SetActive(false);
        else
        {
            cancel_button.SetActive(true);
        }
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OKButton()
    {
        if (inputField.text.Length <= 2)
            error.SetActive(true);
        else
        {
            PlayerPrefs.SetString("PlayerName", inputField.text);
            Hide();
        }
    }

    public void CancelButton()
    {
        Hide();
    }
}
