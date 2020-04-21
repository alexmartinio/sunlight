using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coolant : MonoBehaviour
{
    public float coolantLevel = 0.5f;
    public float valveChange = 0.1f; //how much player moves coolant
    public float coolantChangeMin = 0.001f; //how much coolant moves on its own
    public float coolantChangeMax = 0.005f;
    public bool up;
    public GameObject coolantBar;
    public GameObject coolantBarLid;
    public GameObject water;
    public GameObject reactor;
    public AudioClip audio1;
    public AudioClip audio2;
    private GameObject dead;

    // Start is called before the first frame update
    void Start()
    {
        dead = GameObject.FindGameObjectWithTag("Player").transform.Find("DeadSound").gameObject;
        coolantLevel = 50f;
        bool up = false;
        coolantBar.GetComponent<Image>().fillAmount = coolantLevel / 100f;
        coolantBarLid.GetComponent<Image>().fillAmount = coolantLevel / 100f;
        water.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        coolantBar.GetComponent<Image>().fillAmount = coolantLevel / 100f;
        coolantBarLid.GetComponent<Image>().fillAmount = coolantLevel / 100f;
        if (coolantLevel >=85f || coolantLevel <= 15f)
        {
            water.SetActive(true);
        }
        else
        {
            water.SetActive(false);
        }
        if (coolantLevel >= 100f)
        {
            reactor.GetComponent<ReactorTimer>().timeSpeed = 100f;
            coolantLevel = 100f;
        } else if (coolantLevel <= 0)
        {
            reactor.GetComponent<ReactorTimer>().timeSpeed = 100f;
            coolantLevel = 0f;
        }
        else if (coolantLevel >=0 || coolantLevel <= 100)
        {
            reactor.GetComponent<ReactorTimer>().timeSpeed = 10f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!(dead.activeSelf))
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
            if (Input.GetButtonDown("RotateUp"))
            {
                coolantLevel += valveChange;
                up = (Random.value > 0.5f);
                PlayAudio(audio2);

            }
            else if (Input.GetButtonDown("RotateDown"))
            {
                coolantLevel -= valveChange;
                PlayAudio(audio1);
                up = (Random.value > 0.5f);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    public void IncrementCoolant()
    {
        if (up)
        {
            coolantLevel += Random.Range(coolantChangeMin, coolantChangeMax);
        }
        else if (!(up))
        {
            coolantLevel += -(Random.Range(coolantChangeMin, coolantChangeMax));
        }

        if (coolantLevel > 100)
        {
            coolantLevel = 100f;
        } else if (coolantLevel < 0)
        {
            coolantLevel = 0f;
        }
    }
    private void PlayAudio(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().volume = Random.Range(GetComponent<AudioSource>().volume - 0.05f, GetComponent<AudioSource>().volume + 0.05f);
        GetComponent<AudioSource>().pitch = Random.Range(0.7f, 0.8f);
        GetComponent<AudioSource>().Play();
    }
}
