using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [SerializeField] AudioSource _audioclip;
    [SerializeField] GameLogic _gameLogic;
    public float TimeLeft = 900;
    public bool TimerOn = false;
    public int _playersAlive;

    public TextMeshProUGUI TimerTxt;

    void Start()
    {

        TimerOn = true;
        _playersAlive = _gameLogic.GetRemainingPlayerCount();
        _gameLogic.PlayerCaughtEvent += ChangePlayerCount;

    }

    // Update is called once per frame
    void Update()
    {
        //GameWon();
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                TimerOn = false;
                TimeLeft = 0;
                _gameLogic.TimeRunOut();
            }
        }

    }

    private void ChangePlayerCount(object sender, EventArgs e) {
        _playersAlive = _gameLogic.GetRemainingPlayerCount();
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        TimerTxt.text = TimerTxt.text + "\n Players alive: "+ _playersAlive;

    }

}