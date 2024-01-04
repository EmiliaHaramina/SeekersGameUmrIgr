using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameLogic : MonoBehaviour
{
    // Start is called before the first frame update
    private bool _isHider;
    [SerializeField] GameLogic gameLogic;
    void Start()
    {
        if (this.tag == "hider")
        {
            _isHider = true;
        }
        else
        {
            _isHider = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isHider)
        {
            if (collision.gameObject.tag == "seeker")
            {
                GotCaught();
            }
        }

    }

    private void GotCaught()
    {
        gameLogic.PlayerCaught();
    }
}