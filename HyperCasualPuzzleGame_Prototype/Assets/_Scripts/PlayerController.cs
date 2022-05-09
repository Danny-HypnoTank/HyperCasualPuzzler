using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed, rotationSpeed, endSpeed;
    [SerializeField]
    private float playerSize = 0;

    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private GameManager gameState;
    [SerializeField]
    private CameraShake cameraShake;

    [SerializeField]
    private Animator playerAnimator;
    [SerializeField]
    private AudioSource fallAudio;
    [SerializeField]
    private GameObject hitEffect;

    private void Update()
    {
        Movement();
        Smash();
        Rotation();

        //on smash make play animation of player growing, when at max size play sound effect (bang)
    }

    private void Smash()
    {
        if (gameState.Smashed == true)
        {
            playerAnimator.SetBool("Smash", true);
        }
    }

    private void Movement()
    {
        if (gameState.Smashed == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                rb.velocity = new Vector2(0, 1 * speed * Time.deltaTime);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                rb.velocity = new Vector2(0, -1 * speed * Time.deltaTime);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                rb.velocity = new Vector2(-1 * speed * Time.deltaTime, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                rb.velocity = new Vector2(1 * speed * Time.deltaTime, 0);
            }
            else
            {

            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                rb.velocity = new Vector2(0, 1 * endSpeed * Time.deltaTime);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                rb.velocity = new Vector2(0, -1 * endSpeed * Time.deltaTime);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                rb.velocity = new Vector2(-1 * endSpeed * Time.deltaTime, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                rb.velocity = new Vector2(1 * endSpeed * Time.deltaTime, 0);
            }
            else
            {

            }
        }
    }

    private void Rotation()
    {
        
    }

    private void PlayerGrow()
    {
        if (playerSize < 0.4f)
        {
            transform.localScale += new Vector3(playerSize, playerSize, playerSize);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collectable")
        {
            gameState.CurrentCollectables++;
            playerSize += 0.1f;
            PlayerGrow();
            //Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Bullet")
        {
            gameState.GameOver = true;
            Instantiate(hitEffect, this.transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Hole")
        {
            gameState.GameOver = true;
            Instantiate(hitEffect, this.transform.position, Quaternion.identity);
            cameraShake.StartShake(0.5f, 1f);
            fallAudio.Play();
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" && gameState.LevelComplete == false)
        {
            gameState.GameOver = true;
            cameraShake.StartShake(0.5f, 1f);
            Instantiate(hitEffect, this.transform.position, Quaternion.identity);
            this.gameObject.SetActive(false);
        }
    }
}
