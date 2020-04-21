using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLoader : MonoBehaviour
{
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("EndScene");
    }

}
