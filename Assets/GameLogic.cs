using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public event EventHandler PlayerCaughtEvent;
    // Start is called before the first frame update
    private int _hidersNum;
    private int _hidersCaught;
    private bool _hidersWon;
    void Start()
    {
        //Nekako dobit broj hidera u sobi
        //_hidersNum = taj broj
        _hidersCaught = 0;
        _hidersWon = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetRemainingPlayerCount() { 
        return _hidersNum - _hidersCaught;
    }


    public void PlayerCaught() {
        _hidersCaught++;
        PlayerCaughtEvent?.Invoke(this, null);

        if (_hidersCaught == _hidersNum) {
            GameOver(_hidersWon);
        }

    }

    public void TimeRunOut() {
        _hidersWon = true;
        GameOver(_hidersWon);
    }

    public void GameOver(bool hidersWon) {
        if (hidersWon)
        {//hiders won

        }
        else 
        {// hider lost 
        
        }
    }

}
