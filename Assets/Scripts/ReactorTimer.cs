using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReactorTimer : MonoBehaviour
{
    public bool enable = false;

    public int timer = 0;
    float timerFloat = 0f;
    bool triggeredLevelFinished = false;

    // RADIATION

    public float radLevel; // amout of radiation player has suffered, ie. "health bar"
    public float maxRadLevel = 100f;
    public float radPercent; // current level of radiation incoming
    float distance;
    public float radSpeed = 1f; // how fast radiation increases
    public float radSpeedMultiplier; // Use this to fine tune how quickly radiation moves
    public float maxDistance = 8f;
    float finalMaxDistance;
    public float originMaxDistance = 3f;
    public float heal = -0.05f; // how fast radiation heals

    public GameObject player;
    public bool inDialogue;
    public GameObject coolant;
    public GameObject radBar;
    public Animator anim;

    public GameObject reactorBulge;
    public GameObject reactorGlow;

    public GameObject sound;
    public GameObject panel;
    public float maxSound;
    public float minSound;
    public AudioSource successSound;
    public AudioSource failSound;

    [Tooltip("Level time in seconds")]
    [SerializeField] float levelTime = 10f;
    [Tooltip("Light Object")]
    [SerializeField] GameObject light;
    [SerializeField] float minRange = 50.0f;
    [SerializeField] float maxRange = 50.0f;
    [SerializeField] float minIntensity = 3.0f;
    [SerializeField] float maxIntensity = 3.0f;
    public float timeSpeed = 1.0f;

    public bool winning = false;

    private GameObject dead;
    // Cached componenet references
    HardLight2D myLight;
    Collider2D itemToDrop;

    bool isPressed = false;
    bool inTrigger = false;

    Color red = new Color(206f/ 255f, 100f / 255f, 75f / 255f, 1f);
    Color yellow = new Color(255f/255f, 219f / 255f, 134f / 255f, 1f);
    Color redBlue = new Color(105f / 255f, 124f / 255f, 154f / 255f, 1f);
    Color yellowBlue = new Color(150f / 255f, 152f / 255f, 148f / 255f, 1f);


    private void Start()
    {
        dead = GameObject.FindGameObjectWithTag("Player").transform.Find("DeadSound").gameObject;
        timer = 0;
        timerFloat = 0;
        myLight = light.GetComponent<HardLight2D>();
        radLevel = 0;
        InvokeRepeating("Cool", 1.0f, 1.0f);
        inDialogue = true;
        finalMaxDistance = maxDistance;
        sound.GetComponent<AudioSource>().volume = 0;
        sound.GetComponent<AudioSource>().pitch = 1;
        myLight.Range = minRange;
        myLight.Intensity = minIntensity;
        winning = false;

    }

    public void IncrementTime()
    {
        timerFloat += Time.deltaTime * timeSpeed;
        timer = Mathf.RoundToInt(timerFloat);
        radLevel += radSpeed * Time.deltaTime * timeSpeed * radPercent * radSpeedMultiplier;
        if (radLevel >= maxRadLevel)
        {
            radLevel = maxRadLevel;
        } if (radLevel <= 0)
        {
            radLevel = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enable)
        {
            if((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) && inTrigger)
            {
                DropItem();
            }
            // Radiation calculation
            distance = Mathf.Abs(gameObject.transform.position.x - player.transform.position.x);
            maxDistance = (finalMaxDistance - originMaxDistance) / levelTime * timer + originMaxDistance;
            radSpeed = Mathf.Clamp(timer / levelTime * 2, 0, 1f);
            radPercent = Mathf.Clamp((1 - distance / maxDistance), 0, 1);

            if (radPercent == 0)
            {
                radPercent = heal;
                radBar.GetComponent<Image>().color = Color.Lerp(redBlue, yellowBlue, radLevel / maxRadLevel);
            }
            else
            {
                radBar.GetComponent<Image>().color = Color.Lerp(red, yellow, radLevel / maxRadLevel);
            }
            if (!(inDialogue))
            {
                IncrementTime();
            }

            if (timer >= (levelTime / 3.5f)) // QUARTER OF THE WAY IN
            {
                reactorBulge.GetComponent<SpriteRenderer>().enabled = true;
                reactorBulge.GetComponent<Animator>().SetFloat("speed", timer / levelTime);
            }

            if (triggeredLevelFinished) { return; }

            myLight.Range = (maxRange - minRange) / levelTime * timer + minRange;
            myLight.Intensity = (maxIntensity - minIntensity) / levelTime * timer + minIntensity;
            sound.GetComponent<AudioSource>().volume = (maxSound - minSound) / levelTime * timer + minSound;
            sound.GetComponent<AudioSource>().pitch = (3 - 1) / levelTime * timer + 1;
            bool timerFinish = (timer >= levelTime);

            if (timerFinish) // TIME ENDED
            {
                myLight.Range = maxRange;
                myLight.Intensity = maxIntensity;
                gameOverScreen();
            }

            // Radiation bar
            radBar.GetComponent<Image>().fillAmount = radLevel / maxRadLevel;
        }

    }
    void Cool()
    {
        if (!(inDialogue))
        {
            coolant.GetComponent<Coolant>().IncrementCoolant();
        }
    }

    public void gameOverScreen(int time = 3)
    {
        anim.SetTrigger("fadeOut");
        StartCoroutine(WaitForTime(time, false));
    }

    public void gameWinScreen(int time = 3)
    {
        winning = true;
        anim.SetTrigger("blackFadeOut");
        StartCoroutine(WaitForTime(time, true));
    }

    private IEnumerator WaitForTime(int time, bool win)
    {
        // Wait 3 seconds and then load game over
        yield return new WaitForSeconds(time);
        if (win == true)
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            SceneManager.LoadScene("EndScene");
        }        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && panel.activeSelf == true && !(dead.activeSelf))
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && panel.activeSelf == true && !(dead.activeSelf))
        {
            inTrigger = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && panel.activeSelf == true && !(dead.activeSelf))
        {
            itemToDrop = collision;
        }
    }

    private void DropItem()
    {
        if (itemToDrop.GetComponent<Player>().GetInventory() != null)
        {
            if (itemToDrop.GetComponent<Player>().GetInventory() == "SpareCorrect")
            {
                gameWinScreen();
                successSound.Play();
            }
            else if (itemToDrop.GetComponent<Player>().GetInventory() != null)
            {
                itemToDrop.GetComponent<Player>().inventory[0] = null;
                failSound.Play();

            }
        }
    }
}
;