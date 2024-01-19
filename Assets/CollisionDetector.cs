using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        Debug.Log(col);
        Debug.Log(this.transform.parent.transform.parent.gameObject.tag);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        Debug.Log(this.transform.parent.transform.parent.gameObject.tag);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other);
        Debug.Log(this.transform.parent.transform.parent.gameObject.tag);
    }

    private void OnCollisionStay(Collision col)
    {
        Debug.Log(col);
        Debug.Log(this.transform.parent.transform.parent.gameObject.tag);
    }

}
