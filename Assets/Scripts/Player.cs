using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Animator anim;
    public float speed = 1.0f;
    public float maxSpeed = 1.0f;
    public float minSpeed = 3.0f;
    public bool isWalking;
    Vector3 moveVector;
    public bool stopPlayer = false;
    public GameObject gameStates;
    public GameObject deadSound;
    public GameObject tiredSound;
    public float publicAnimSpeed;
    public Countdown countdown;

    SpriteRenderer sprite;
    Rigidbody2D myRigidBody;
    ReactorTimer reactorTimer;
    public string[] inventory = new string[1];
    bool press = false;
    bool inTrigger = false;
    private GameObject dead;
    Collider2D itemName;

    private void Start()
    {
        dead = GameObject.FindGameObjectWithTag("Player").transform.Find("DeadSound").gameObject;
        speed = maxSpeed;
        stopPlayer = false;
        sprite = GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        reactorTimer = gameStates.GetComponent<ReactorTimer>();
        inventory[0] = null;
        tiredSound.SetActive(false);

    }
    void FixedUpdate()
    {
        float input_x = Input.GetAxisRaw("Horizontal");

        if (!(stopPlayer))
        {
            if (input_x == -1)
            {
                //Walking left
                sprite.flipX = true;
                moveVector = new Vector3(input_x, 0, 0).normalized;
                transform.position += moveVector * Time.deltaTime * speed;
                isWalking = true;
                anim.SetBool("isTired", false);

            }
            else if (input_x == 1)
            {
                //Walking right
                sprite.flipX = false;
                moveVector = new Vector3(input_x, 0, 0).normalized;
                transform.position += moveVector * Time.deltaTime * speed;
                isWalking = true;
                anim.SetBool("isTired", false);
            }
            else
            {
                //Standing still
                isWalking = false;
                if (reactorTimer.radLevel >= 40)
                {
                    anim.SetBool("isTired", true);
                }
                else
                {
                    anim.SetBool("isTired", false);
                }
            }
        }
        if (stopPlayer)
        {
            isWalking = false;
        }
        SpeedControl();
    }

    private void SpeedControl()
    {
        if (reactorTimer.radLevel >= 100f)
        {
            deadSound.SetActive(true);
            stopPlayer = true;
            isWalking = false;
            speed = 0;

            anim.SetBool("isSlow", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isTired", false);
            anim.SetBool("isDead", true);
            sprite.flipX = false;
            tiredSound.SetActive(false);

            reactorTimer.timeSpeed = 200f;
        }
        else if(reactorTimer.radLevel >= 40f)
        {
            anim.SetFloat("speed", ((speed / maxSpeed) * 0.8f));
            publicAnimSpeed = ((speed / maxSpeed) * 0.8f);
            anim.SetBool("isSlow", isWalking);
            anim.SetBool("isWalking", false);
            tiredSound.SetActive(true);
        }
        else
        {
            anim.SetFloat("speed", ((speed / maxSpeed)*0.6f));
            publicAnimSpeed = ((speed / maxSpeed) * 0.6f);
            anim.SetBool("isWalking", isWalking);
            tiredSound.SetActive(false);
        }
        speed = Mathf.Clamp((reactorTimer.maxRadLevel - reactorTimer.radLevel) /100f * maxSpeed, minSpeed, maxSpeed);
    }

    // Add inventory items
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "canPick" && countdown.on && !(dead.activeSelf))
        {
            itemName = collision;
        }
    }

    private void PickUpName()
    {
        if (inventory[0] == null)
        {
            itemName.transform.parent.GetComponent<AudioSource>().Play();
            inventory[0] = itemName.name;
            Destroy(itemName.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "canPick" && countdown.on && !(dead.activeSelf))
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "canPick" && countdown.on && !(dead.activeSelf))
        {
            inTrigger = false;
        }
    }

    // Get the item in the inventory
    public string GetInventory()
    {
        return inventory[0];
    }

    // Sets the item to null
    public void RemoveItemFromList()
    {
        inventory[0] = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) && inTrigger)
        {
            PickUpName();
        }
    }
}
