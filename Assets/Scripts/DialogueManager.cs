using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text dialogueText;
    public Player player;

    public bool ended = false;
    public GameObject speechBubble;
    public GameObject engineerProfile;
    public GameObject captainProfile;
    public GameObject reactor;
    public AudioClip[] audioClips;

    private bool engSpeaking;
    private bool capSpeaking;
    private AudioSource audioSource;
    public bool paswordEntered = false;
    public GameObject menuManager;

    private Queue<string> sentences;
    List<int> numbers = new List<int>();
    const string PASSWORD = "80085";

    public GameObject compSoundCorrect;
    public GameObject compSoundError;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        ended = false;
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        ended = false;
        reactor.GetComponent<ReactorTimer>().inDialogue = true;
        player.stopPlayer = true;
        speechBubble.GetComponent<SpriteRenderer>().enabled = true;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {

            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            StopAllCoroutines();
            EndDialog();
            return;
        }
        string sentence = sentences.Dequeue();

        // Profile switching
        if (engineerProfile && captainProfile)
        {
            if (sentence[0]=='E')
            {
                engineerProfile.GetComponent<SpriteRenderer>().enabled = true;
                captainProfile.GetComponent<SpriteRenderer>().enabled = false;
                engSpeaking = true;
                capSpeaking = false;
            }
            else if (sentence[0] == 'C')
            {
                captainProfile.GetComponent<SpriteRenderer>().enabled = true;
                engineerProfile.GetComponent<SpriteRenderer>().enabled = false;
                engSpeaking = false;
                capSpeaking = true;
            }
            else
            {
                print("Error: No speaker specified");
            }
            sentence = sentence.Substring(1);
        }
        
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {

        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            while (menuManager.GetComponent<QuitMenu>().isGamePaused() == true)
            {
                yield return null;
            }
            dialogueText.text += letter;
            if (dialogueText.text.Contains("|"))
            {
                //Add new line
                dialogueText.text += "\n";
                StopAllCoroutines();
                StartCoroutine(EnterText());
            }

            PlayAudio();
            yield return null;
        }
    }

    IEnumerator EnterText()
    {
        compSoundError.SetActive(false);
        while (numbers.Count != PASSWORD.Length)
        {
            // Long list of keypresses being recorded
            if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
            {
                numbers.Add(0);
                dialogueText.text += "0";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                numbers.Add(1);
                dialogueText.text += "1";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                numbers.Add(2);
                dialogueText.text += "2";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                numbers.Add(3);
                dialogueText.text += "3";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            {
                numbers.Add(4);
                dialogueText.text += "4";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
            {
                numbers.Add(5);
                dialogueText.text += "5";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
            {
                numbers.Add(6);
                dialogueText.text += "6";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
            {
                numbers.Add(7);
                dialogueText.text += "7";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
            {
                numbers.Add(8);
                dialogueText.text += "8";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
            {
                numbers.Add(9);
                dialogueText.text += "9";
            }
            yield return null;
        }

        // If password is not correct try loop again
        if (string.Join("", numbers.ToArray()) != PASSWORD)
        {
            numbers.Clear();
            StopAllCoroutines();
            StartCoroutine(TypeSentence("Try Again | "));
            compSoundError.SetActive(true);
        }
        // If correct set the passwordEntered to true
        // This will trigger in the DialogueTrigger the next scene
        else
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence("Correct"));
            numbers.Clear();
            paswordEntered = true;
            compSoundCorrect.SetActive(true);
        }
    }

    void EndDialog()
    {
        speechBubble.GetComponent<SpriteRenderer>().enabled = false;
        speechBubble.GetComponentInChildren<SpriteRenderer>().enabled = false;
        dialogueText.text = "";
        player.stopPlayer = false;
        ended = true;
        reactor.GetComponent<ReactorTimer>().inDialogue = false;
        if (engineerProfile && captainProfile)
        {
            engineerProfile.GetComponent<SpriteRenderer>().enabled = false;
            captainProfile.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Chooses a random audio clip to play from the array
    private void PlayAudio(float volume=0.75f)
    {
        if (audioSource.isPlaying == false) {
            if (engSpeaking)
            {
                audioSource.pitch = 0.75f;
            } else if (capSpeaking)
            {
                audioSource.pitch = 0.9f;
            }
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.Play();
        }

    }
}
