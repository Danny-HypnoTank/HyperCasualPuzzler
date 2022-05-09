using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField]
    private GameManager gameState;
    [SerializeField]
    private Animator[] doorAnimators;
    [SerializeField]
    private CameraShake cameraShake;
    [SerializeField]
    private AudioSource doorOpenSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameState.Smashed == true)
        {
            //play sound, shake camera and animation here
            doorAnimators[0].SetBool("Open", true);
            doorAnimators[1].SetBool("Open", true);
            doorOpenSound.Play();
            //cameraShake.StartShake(0.5f, 1f);
            gameState.Exit = true;
        }
    }
}
