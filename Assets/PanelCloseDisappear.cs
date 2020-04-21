using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelCloseDisappear : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(Close());
    }

    private IEnumerator Close()
    {
        yield return new WaitForSeconds(0.8f);
        gameObject.SetActive(false);
    }
}
