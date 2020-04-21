using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNext : MonoBehaviour
{
    public int eventNumber;
    public GameObject gameSession;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameSession.GetComponent<Events>().CallEvent(eventNumber);
        print("triggered event " + eventNumber);
    }
}
