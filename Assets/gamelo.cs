using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gamelo : MonoBehaviour
{
    // Start is called before the first frame update
     
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = GameLogic._whoWon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
