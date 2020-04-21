using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class openCoolant : MonoBehaviour
{
    public GameObject lid;
    public GameObject UI;
    public GameObject bar;
    private bool firstPress;
    public AudioClip[] audioClips;
    private GameObject dead;

    private void Start()
    {
        dead = GameObject.FindGameObjectWithTag("Player").transform.Find("DeadSound").gameObject;
        lid.GetComponent<SpriteRenderer>().enabled = false;
        UI.GetComponent<SpriteRenderer>().enabled = false;
        firstPress = true;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!(dead.activeSelf))
        {
            if (firstPress == true && Input.GetButtonDown("Interact"))
            {
                lid.GetComponent<SpriteRenderer>().enabled = true;
                firstPress = false;
                StartCoroutine(InstantiateAfterDelay(0.3f));
                GetComponent<AudioSource>().clip = audioClips[0];
                GetComponent<AudioSource>().Play();
            }
            else if (firstPress == false && Input.GetButtonDown("Interact"))
            {
                lid.GetComponent<SpriteRenderer>().enabled = false;
                UI.GetComponent<SpriteRenderer>().enabled = false;
                bar.GetComponent<Image>().enabled = false;
                firstPress = true;
                GetComponent<AudioSource>().clip = audioClips[1];
                GetComponent<AudioSource>().Play();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        lid.GetComponent<SpriteRenderer>().enabled = false;
        UI.GetComponent<SpriteRenderer>().enabled = false;
        bar.GetComponent<Image>().enabled = false;
        firstPress = true;
        if (GetComponent<AudioSource>().clip == audioClips[0])
        {
            GetComponent<AudioSource>().clip = audioClips[1];
            GetComponent<AudioSource>().Play();
        }
    }
    IEnumerator InstantiateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (lid.GetComponent<SpriteRenderer>().enabled == true){
            UI.GetComponent<SpriteRenderer>().enabled = true;
            bar.GetComponent<Image>().enabled = true;
        }
    }
}