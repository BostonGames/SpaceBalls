using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 3.0f;
    private Rigidbody enemyRb;
    private GameObject player;
    [SerializeField] ParticleSystem spawnParticles;
    [SerializeField] ParticleSystem xplodeParticles;
    [SerializeField] Quaternion noRotation;

    [SerializeField] AudioClip deathClip;
    private AudioSource enemyAudio;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyAudio = GetComponent<AudioSource>();
        player = GameObject.Find("Player");

        //spawn particle effect
        noRotation.eulerAngles = new Vector3(-90.0f, 0.0f, 0.0f);
        Instantiate(spawnParticles, gameObject.transform.position, noRotation);
    }

    // Update is called once per frame
    void Update()
    {
        //enemy follows Player
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        //helps identify which way to AddForce onCollisionEnter
        transform.forward = enemyRb.velocity;

        if(gameObject.transform.position.y < -2)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Moon")
        {
            //TODO: add points
            Explode();
            
            Debug.Log("Enemy has hit object tagged as Moon");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SafeZone"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;

        enemyAudio.PlayOneShot(deathClip, 1.0f);
        Instantiate(xplodeParticles, gameObject.transform.position, noRotation);
        Invoke("DestroyEnemy", 1.0f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
