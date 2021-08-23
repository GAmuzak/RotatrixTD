using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmp;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private TextMeshProUGUI temp2;

    private float time=0f;
    private double timeD = 0d;
    private void Update()
    {
        time += Time.deltaTime;
        timeD = Math.Round(time, 2);
        tmp.text = timeD.ToString();
    }

    public void EndGame()
    {
        temp2.text = $"Final time:{timeD.ToString()} seconds";
        gameOver.SetActive(true);
        Time.timeScale = 0f;
    }
}
