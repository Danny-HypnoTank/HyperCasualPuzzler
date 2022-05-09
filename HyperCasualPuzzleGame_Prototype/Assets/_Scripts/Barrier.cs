using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField]
    private int barrierHealth = 1;
    [SerializeField]
    private GameManager gameState;
    [SerializeField]
    private Animator barrierAnimator;
    [SerializeField]
    private CameraShake cameraShake;
    [SerializeField]
    private AudioSource breakingSound;
    [SerializeField]
    private AudioSource openDoorSound;
    [SerializeField]
    private GameObject hitEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameState.Smashed ==  true)
        {
            //play sound, shake camera and animation here
            Instantiate(hitEffect, this.transform.position, Quaternion.identity);
            breakingSound.Play();
            cameraShake.StartShake(0.5f, 2f);
            this.gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "Player" && gameState.Smashed == false)
        {
            if(gameState.CurrentCollectables >= barrierHealth)
            {
                //play sound, shake camera and animation here
                Instantiate(hitEffect, this.transform.position, Quaternion.identity);
                openDoorSound.Play();
                barrierAnimator.SetBool("Open", true);
            }
        }
    }

}
