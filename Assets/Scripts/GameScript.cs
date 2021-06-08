using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public GameObject[] panels;
    private int first, second, third, nextPanel;
    private System.Random rand;
    void Start()
    {
        rand = new System.Random((int)DateTime.Now.Ticks);
        first = rand.Next(0, panels.Length);
        second = rand.Next(0, panels.Length);
        third = rand.Next(0, panels.Length);

        if (first == second | second == third | first == third)
        {
            Start();
        }
        else
        {
            StartCoroutine(StartWaiting());          
        }
    }

    public void ChangePanels()
    {
        nextPanel = rand.Next(0, panels.Length);
            if (panels[nextPanel].GetComponent<Image>().color == Color.black)
                ChangePanels();
            else
                panels[nextPanel].GetComponent<Image>().color = Color.black;
    }

    IEnumerator StartWaiting()
    {
        yield return new WaitForSeconds(4);
        panels[first].GetComponent<Image>().color = Color.black;
        panels[second].GetComponent<Image>().color = Color.black;
        panels[third].GetComponent<Image>().color = Color.black;
    }
}
