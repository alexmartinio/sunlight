using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highlightOnTriggerPanel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>().inventory[0] != null){
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
}
    }
private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
