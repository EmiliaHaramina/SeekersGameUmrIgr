using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] PlayerGameLogic _pGL;
    float timer = 0;
    bool _startGame = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;


        // Check if 15 seconds have passed
        if (timer >= 15f && !_startGame)
        {
            _startGame = true;
            //mozda dodat spawnat playere na svoja mjesta
        }
    }
    /*
    private void OnCollisionEnter(Collision col)
    {
       if (this.transform.parent.tag == "seeker")
            { 
            if(col.transform.parent.tag == "hider" || col.transform.parent.tag == "Player"){
                _pGL.GotCaught(col.transform.parent.gameObject);
            }
            if (col.transform.parent.tag == "head") {
                if (col.transform.parent.transform.parent.tag == "hider" || col.transform.parent.transform.parent.tag == "Player") {
                    _pGL.GotCaught(col.transform.parent.transform.parent.gameObject);
                }
            }
        }
    }
    */

    private void OnTriggerEnter(Collider col)
    {

        if (this.transform.parent.tag == "seeker" && _startGame)
        {
            if (col.transform.parent.tag == "hider" || col.transform.parent.tag == "Player")
            {
                _pGL.GotCaught(col.transform.parent.gameObject);
                _startGame = false;
                timer = 10;
            }
            if (col.transform.parent.tag == "head")
            {
                if (col.transform.parent.transform.parent.tag == "hider" || col.transform.parent.transform.parent.tag == "Player")
                {
                    _pGL.GotCaught(col.transform.parent.transform.parent.gameObject);
                    _startGame = false;
                    timer = 10;
                }
            }
        }
    }


}
