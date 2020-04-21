using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenuNavigation : MonoBehaviour
{
    // The start arrow
    public GameObject yesArrow;
    // view options arrow
    public GameObject noArrow;
    // Check if arrow is on the options menu
    public bool onStart = true;
    // bool to track movement of the arrows
    bool canMove = true;
    public GameObject menuManager;

    void Start()
    {
        // On start make sure only start arrow is enabled
        yesArrow.GetComponent<SpriteRenderer>().enabled = true;
        noArrow.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) && canMove == true)
        {
            yesArrow.GetComponent<SpriteRenderer>().enabled = false;
            noArrow.GetComponent<SpriteRenderer>().enabled = true;
            onStart = false;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && canMove == true)
        {
            yesArrow.GetComponent<SpriteRenderer>().enabled = true;
            noArrow.GetComponent<SpriteRenderer>().enabled = false;
            onStart = true;
        }
        else if ((Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && onStart == false))
        {
            menuManager.GetComponent<QuitMenu>().Resume();
        }
        else if ((Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && onStart == true))
        {
            menuManager.GetComponent<QuitMenu>().QuitGame();
        }
    }
}
