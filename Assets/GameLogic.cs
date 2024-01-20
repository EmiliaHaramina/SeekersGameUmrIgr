using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public event EventHandler PlayerCaughtEvent;

    [SerializeField] TimerScript _timer;

    // Start is called before the first frame update
    private int _hidersNum;
    private int _hidersCaught;
    private bool _hidersWon;

    static public string _whoWon = "";
    
    void Start()
    { 


        _hidersCaught = 0;
        _hidersWon = false;

        Invoke("GetPlayerCountStart", 5f);
        Invoke("MoveSeeker", 3f);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetRemainingPlayerCount() { 
        return _hidersNum - _hidersCaught;
    }

    public void MoveSeeker() {
        GameObject.FindGameObjectWithTag("seeker").transform.position = GameObject.FindGameObjectWithTag("SeekerSpawnPoint").transform.position;
    }

    public void PlayerCaught() {
        _hidersCaught++;
        PlayerCaughtEvent?.Invoke(this, null);
        //_timer.ChangePlayerCountMethod();

        if (_hidersCaught == _hidersNum) {
            GameOver(_hidersWon);
        }
        //pozvat RPC

    }

    public void GetPlayerCountStart() {
        GameObject[] players1 = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] players2 = GameObject.FindGameObjectsWithTag("seeker");
        GameObject[] players3 = GameObject.FindGameObjectsWithTag("hider");
        int playersSize = players1.Length + players2.Length + players3.Length;
        GameObject[] players = new GameObject[playersSize];
        int i = 0;
        foreach (GameObject player in players1)
        {
            players[i] = player;
            i++;
        }
        foreach (GameObject player in players2)
        {
            players[i] = player;
            i++;
        }
        foreach (GameObject player in players3)
        {
            players[i] = player;
            i++;
        }
        _hidersNum = players.Length - 1;

        PlayerCaughtEvent?.Invoke(this, null);
        //_timer.ChangePlayerCountMethod();
    }

    public void TimeRunOut() {
        _hidersWon = true;
        GameOver(_hidersWon);
    }

    public void GameOver(bool hidersWon) {
        if (hidersWon)
        {//hiders won
            _whoWon = "HIDERS WON!";
            PhotonNetwork.LoadLevel("GameOverScene");
        }
        else 
        {// hider lost
            _whoWon = "SEEKER WON!";
            PhotonNetwork.LoadLevel("GameOverScene");
        }
    }

}
