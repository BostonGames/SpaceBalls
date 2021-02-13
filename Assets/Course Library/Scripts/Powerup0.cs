using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup0 : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    [SerializeField] GameObject aura;
    [SerializeField] ParticleSystem spawnParticles;
    [SerializeField] ParticleSystem xplodeParticles;
    [SerializeField] Quaternion noRotation;
    [SerializeField] Vector3 offset = new Vector3(0.0f, 0.0f, 0.0f);


    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        //spawn particle effect
        noRotation.eulerAngles = new Vector3(-90.0f, 0.0f, 0.0f);
        Instantiate(spawnParticles, gameObject.transform.position, noRotation);
    }

    // Update is called once per frame
    void Update()
    {


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Explode();
        }

    }

    private void Explode()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        aura.SetActive(false);

        Instantiate(xplodeParticles, gameObject.transform.position - offset, noRotation);
        Invoke("DestroyPowerup", 1.0f);
    }

    private void DestroyPowerup()
    {
        Destroy(gameObject);
    }
}
