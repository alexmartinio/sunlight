using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartLoader : MonoBehaviour
{
    public GameObject menuNavigator;

    MenuNavigation menueNavigationScript;

    private void Start()
    {
        if (menuNavigator)
        {
            menueNavigationScript = menuNavigator.GetComponent<MenuNavigation>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space)) && menueNavigationScript.onStart == true)
        {
            SceneManager.LoadScene("MainScene");
        }        
    }
}
