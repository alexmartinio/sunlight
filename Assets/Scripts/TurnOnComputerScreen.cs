using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnComputerScreen : MonoBehaviour
{

        private bool firstPress;

    private GameObject dead;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        firstPress = true;
        dead = GameObject.FindGameObjectWithTag("Player").transform.Find("DeadSound").gameObject;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!(dead.activeSelf))
        {
            if (firstPress == true && Input.GetButtonDown("Interact"))
            {
                GetComponent<SpriteRenderer>().enabled = true;
                firstPress = false;
            }
            else if (firstPress == false && Input.GetButtonDown("Interact"))
            {
                GetComponent<SpriteRenderer>().enabled = false;
                firstPress = true;
            }
        }
    }
}
