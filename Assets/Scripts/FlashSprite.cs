using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSprite : MonoBehaviour
{
    private SpriteRenderer sprite;
    private GameObject dead;
    // Start is called before the first frame update
    void Start()
    {
        dead = GameObject.FindGameObjectWithTag("Player").transform.Find("DeadSound").gameObject;
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        while (true) {
            // Wait 3 seconds and then load game over
            yield return new WaitForSeconds(0.75f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.75f);
            sprite.enabled = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!(dead.activeSelf))
        {
            if (Input.GetButton("Interact"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
