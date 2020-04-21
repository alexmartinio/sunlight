using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateWheel : MonoBehaviour
{
    public Sprite[] sprites;
    int i;
    private GameObject dead;

    bool rotateDown = false;
    bool rotateUp = false;
    bool inTrigger = false;

    void Start()
    {
        i = 0;
        dead = GameObject.FindGameObjectWithTag("Player").transform.Find("DeadSound").gameObject;
    }

    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        inTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inTrigger = false;
    }

    private void Update()
    {
        if (!(dead.activeSelf))
        {
            if (Input.GetButtonDown("RotateDown") && inTrigger)
            {
                i++;
                if (i == sprites.Length)
                {
                    i = 0;
                }
                GetComponent<SpriteRenderer>().sprite = sprites[i];
            }
            if (Input.GetButtonDown("RotateUp") && inTrigger)
            {
                i = i - 1;
                if (i <= 0)
                {
                    i = sprites.Length - 1;
                }
                GetComponent<SpriteRenderer>().sprite = sprites[i];
            }
        }
    }
}
