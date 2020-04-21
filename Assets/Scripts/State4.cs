using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State4 : MonoBehaviour
{
    public ReactorTimer reactor;
    public GameObject nextState;
    public float idleTime = 20f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!(reactor.inDialogue))
        {
            timer += Time.deltaTime;
        }
        print(timer);
        if (timer >= idleTime)
        {
            nextState.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
