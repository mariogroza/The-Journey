using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class BackGroundChange : MonoBehaviour
{
    public GameObject Mov;
    public GameObject Padure;  

    public void BackGroundChanger()
    {
        Mov.SetActive(false);
        Padure.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            BackGroundChanger();
        }
    }
}
