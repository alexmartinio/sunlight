using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeInBlack : MonoBehaviour
{
    Color colorStart;
    private Color colorEnd;
    public float duration;
    private float t = 0f;

    // Start is called before the first frame update
    void Start()
    {
        colorStart = new Color(0f, 0f, 0f, 1.0f);
        //print(colorStart);
        colorEnd = new Color(0f, 0f, 0f, 0.0f);
        StartCoroutine(Fade());
    }
    private IEnumerator Fade()
    {
        while (t < duration)
        {
            gameObject.GetComponent<Image>().color = Color.Lerp(colorStart, colorEnd, t / duration);
            yield return new WaitForSeconds(0.01f);
            t += 0.01f;
        }
    }
}
