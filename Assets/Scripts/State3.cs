using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State3 : MonoBehaviour
{
    public GameObject display;
    public GameObject breakAnim;

    void OnEnable()
    {
        if (display.activeSelf)
        {
            breakAnim.SetActive(true);
            StartCoroutine(Disappear());
        }
    }
    private IEnumerator Disappear()
    {
        while (true)
        {
            // Wait 3 seconds and then load game over
            yield return new WaitForSeconds(0.3f);
            breakAnim.SetActive(false);
            display.SetActive(false);
        }
    }


}
