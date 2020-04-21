using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerIntro : MonoBehaviour
{

    public Dialogue dialogue;
    bool firstPress = false;
    public bool permanent = false;
    public bool enableOnStart;
    public GameObject commsLight;

    public GameObject nextEvent;
    public GameObject nextEvent2;
    public GameObject disableEvent;
    public GameObject gameSession;
    public GameObject dialogManager;
    DialogueManager dialogueManager;
    public bool isPressed = false;
    public Countdown countdown;

    private bool insideTrigger = false;
    private GameObject dead;


    public void Start()
    {
        dialogueManager = dialogManager.GetComponent<DialogueManager>();
        dead = GameObject.FindGameObjectWithTag("Player").transform.Find("DeadSound").gameObject;
    }

    public void OnEnable()
    {
        isPressed = false;
        insideTrigger = false;
    }

    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }
    public void NextSentence()
    {
        dialogueManager.DisplayNextSentence();
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (!(dead.activeSelf)) {
        insideTrigger = true;
        if (enableOnStart)
        {
            TriggerDialogue();
            firstPress = true;
            if (commsLight) { commsLight.GetComponent<SpriteRenderer>().enabled = true; }
            enableOnStart = false;
        }
        if (firstPress == false && isPressed)
        {
            TriggerDialogue();
            firstPress = true;
            if (commsLight)
            {
                commsLight.GetComponent<SpriteRenderer>().enabled = true;
            }
            isPressed = false;
        }
        else if (firstPress == true && isPressed)
        {
            NextSentence();
            isPressed = false;
        }

        if (dialogueManager.ended == true)
        {
            if (countdown && countdown.on)
            {
                countdown.countdownTrigger = true;
                print("flipped");
            }
            dialogueManager.ended = false;
            isPressed = false;
            if (commsLight) { commsLight.GetComponent<SpriteRenderer>().enabled = false; }
            if (nextEvent2)
            {
                nextEvent2.SetActive(true);
            }
            if (nextEvent)
            {
                // Special condition for 4 active if password is correct
                if (nextEvent.name == "4")
                {
                    if (dialogueManager.paswordEntered == true)
                    {
                        nextEvent.SetActive(true);
                    }
                }
                else
                {
                    nextEvent.SetActive(true);
                }
            }
            if (disableEvent)
            {
                // Special condition for 4 deactivate if password is correct
                if (nextEvent.name == "4")
                {
                    if (dialogueManager.paswordEntered == true)
                    {
                        gameObject.transform.parent.gameObject.SetActive(false);
                    }
                }
                else
                {
                    disableEvent.SetActive(false);
                }
            }
            if (permanent)
            {
                firstPress = false;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Interact") && insideTrigger == true)
        {
            isPressed = true;
        }
       
     
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        insideTrigger = false;
    }
}
