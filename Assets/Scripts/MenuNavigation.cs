using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    // The start arrow
    public GameObject startArrow;
    // view options arrow
    public GameObject viewControlsArrow;
    // options menu
    public GameObject optionsView;
    // quite arrow
    public GameObject quitArrow;
    // Check if arrow is on the options menu
    public bool onStart = true;
    // bool to track movement of the arrows
    bool canMove = true;
    int position = 0;

    void Start()
    {
        // On start make sure only start arrow is enabled
        startArrow.GetComponent<SpriteRenderer>().enabled = true;
        viewControlsArrow.GetComponent<SpriteRenderer>().enabled = false;
        optionsView.GetComponent<SpriteRenderer>().enabled = false;
        quitArrow.GetComponent<SpriteRenderer>().enabled = false;
    }

    void arrowMovement()
    {
        if (position < 0)
        {
            position = 0;
        }
        else if (position > 2)
        {
            position = 2;
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            position += 1;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            position -= 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
       arrowMovement();
        if(canMove == true && position == 1)
        {
            startArrow.GetComponent<SpriteRenderer>().enabled = false;
            viewControlsArrow.GetComponent<SpriteRenderer>().enabled = true;
            quitArrow.GetComponent<SpriteRenderer>().enabled = false;
            onStart = false;
        }
        else if (canMove == true && position == 0)
        {
            startArrow.GetComponent<SpriteRenderer>().enabled = true;
            viewControlsArrow.GetComponent<SpriteRenderer>().enabled = false;
            quitArrow.GetComponent<SpriteRenderer>().enabled = false;
            onStart = true;
        }
        else if (canMove == true && position == 2)
        {
            startArrow.GetComponent<SpriteRenderer>().enabled = false;
            viewControlsArrow.GetComponent<SpriteRenderer>().enabled = false;
            quitArrow.GetComponent<SpriteRenderer>().enabled = true;
        }

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space)) && onStart == false && canMove == true && position == 1)
        {
            canMove = false;
            optionsView.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space)) && onStart == false && canMove == false && position == 1)
        {
            optionsView.GetComponent<SpriteRenderer>().enabled = false;
            canMove = true;            
        }
        else if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space)) && onStart == false && canMove == true && position == 2)
        {
            Application.Quit();
        }
    }
}
