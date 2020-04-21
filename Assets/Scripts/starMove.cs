using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starMove : MonoBehaviour
{
    Vector3 startPos;
    public float speed;
    public float target = -721;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveOverSpeed(gameObject, new Vector3(target, 0.0f, 0f), speed));
        startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {
        // speed should be 1 unit per second
        while (objectToMove.transform.position != end)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        if (objectToMove.transform.position == end)
        {
            objectToMove.transform.position = startPos;
            StartCoroutine(MoveOverSpeed(gameObject, new Vector3(target, 0.0f, 0f), speed));
        }
    }
}
