using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radiation : MonoBehaviour
{
    public float radPercent;
    float distance;
    public float maxDistance = 8f;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Abs(gameObject.transform.position.x - player.transform.position.x);
        radPercent = Mathf.Clamp((1- distance / maxDistance), 0, 1);
    
    }
}
