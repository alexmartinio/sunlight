using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State1 : MonoBehaviour
{
    public ReactorTimer reactor;
    public Coolant coolant;
    public float idleTime = 20f;
    public GameObject radUI;
    public GameObject state2;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        reactor.enable = false;
        radUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!(reactor.inDialogue))
        {
            timer += Time.deltaTime;
        }
        if (coolant.coolantLevel <= 0 || coolant.coolantLevel >= 100 || timer >= idleTime)
        {
            reactor.enable = true;
            radUI.SetActive(true);
            state2.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
