using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private Player player;
    private bool cooldown;
    private AudioSource audio;
    public AudioClip[] audioClips;
    private float volume;
    private float pitch;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();
        audio = gameObject.GetComponent<AudioSource>();
        cooldown = false;
        volume = audio.volume;
        pitch = audio.pitch;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isWalking)
        {
            speed = 1 / (4 * player.publicAnimSpeed);
            StartCoroutine(PlaySound(speed));
        }
    }

    private IEnumerator PlaySound(float pause)
    {
        if (cooldown == false)
        {
            cooldown = true;
            audio.clip = audioClips[Random.Range(0, audioClips.Length)];
            audio.volume = Random.Range((volume - 0.05f), (volume + 0.05f));
            audio.pitch = Random.Range((pitch - 0.05f), (pitch + 0.05f));
            audio.Play();
            yield return new WaitForSeconds(pause);
            cooldown = false;
        }
    }
}
