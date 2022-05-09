using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    private int breakingPoint;
    [SerializeField]
    private GameManager gameState;
    //reference camera shake here
    [SerializeField]
    private bool moveable = false;
    [SerializeField]
    private GameObject[] waypoints;
    private int currentWaypoint = 0;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float WPradius = 1;
    [SerializeField]
    private CameraShake cameraShake;
    [SerializeField]
    private AudioSource breakingSound;
    [SerializeField]
    private GameObject hitEffect;

    private void Update()
    {
        if (moveable == true)
        {
            if (Vector3.Distance(waypoints[currentWaypoint].transform.position, transform.position) < WPradius)
            {
                currentWaypoint++;
                if (currentWaypoint >= waypoints.Length)
                {
                    currentWaypoint = 0;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, Time.deltaTime * speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && gameState.Smashed == true)
        {
            //play sound, shake camera and animation here
            Instantiate(hitEffect, this.transform.position, Quaternion.identity);
            breakingSound.Play();
            cameraShake.StartShake(0.5f, 1f);
            this.gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Player" && gameState.Smashed == false)
        {
            if (gameState.CurrentCollectables > breakingPoint)
            {
                //play sound, shake camera and animation here
                Instantiate(hitEffect, this.transform.position, Quaternion.identity);
                breakingSound.Play();
                cameraShake.StartShake(0.5f, 2f);
                this.gameObject.SetActive(false);
            }
        }
    }
}
