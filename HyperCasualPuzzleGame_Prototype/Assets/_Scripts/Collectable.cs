using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private GameObject collectionParticle;
    [SerializeField]
    private AudioSource collectableAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collectableAudio.Play();
            Instantiate(collectionParticle, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
