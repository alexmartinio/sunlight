using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public bool on = false;
    public bool countdownTrigger;
    public GameObject[] screenNumbers;
    public ReactorTimer timer;
    public GameObject panel;
    public GameObject closeAnim;


    // Update is called once per frame
    void OnEnable()
    {
        on = true;
        countdownTrigger = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (countdownTrigger)
        {
            countdownTrigger = false;
            panel.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(ComputerCountdown(1f));

        }
    }

    private IEnumerator ComputerCountdown(float waitTime)
    {
        int i = 0;
        while(i <= 5)
        {
                if (!(timer.inDialogue))
                {
                    if (!(timer.winning))
                    {
                        screenNumbers[i].SetActive(true);
                        yield return new WaitForSeconds(waitTime);
                        screenNumbers[i].SetActive(false);
                    }
                    i++;
                }
                if (i == 5)
                {
                    panel.SetActive(false);
                    closeAnim.SetActive(true);

                }
                else if (timer.inDialogue)
                {
                    yield break;
                }
        }
    }
}
