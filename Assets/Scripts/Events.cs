using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{

    public GameObject[] events;

    ReactorTimer timer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad == 0)
        {

            CallEvent(1);
        }
            if (Time.timeSinceLevelLoad == 30)
        {
            CallEvent(2);
        }

    }

    public void CallEvent(int eventNumber)
    {
        if (eventNumber == 0) {
            return;
        }
        events[eventNumber-1].GetComponent<BoxCollider2D>().enabled = true;
    }
}
