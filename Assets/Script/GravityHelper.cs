using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHelper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //PlayerController player = other.GetComponent<PlayerController>();
            //player.SetState();
            Debug.Log("Menyentuh batas coy");
            other.gameObject.GetComponent<PlayerController>().enabled = false;
        }
    }
}
