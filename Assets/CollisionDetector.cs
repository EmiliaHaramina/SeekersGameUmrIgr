using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField] PlayerGameLogic _pGL;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (this.transform.parent.tag == "seeker")
        {
            if (col.transform.parent.tag == "hider" || col.transform.parent.tag == "Player")
            {
                _pGL.GotCaught(col.transform.parent.gameObject);
            }
            if (col.transform.parent.tag == "head")
            {
                if (col.transform.parent.transform.parent.tag == "hider" || col.transform.parent.transform.parent.tag == "Player")
                {
                    _pGL.GotCaught(col.transform.parent.transform.parent.gameObject);
                }
            }
        }
    }


}
