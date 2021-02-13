using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedUpPlayback : MonoBehaviour
{
    private ParticleSystem thisParticleSystem;
    [SerializeField] float playbackSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        

        thisParticleSystem = gameObject.GetComponent<ParticleSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        var main = thisParticleSystem.main;
        main.simulationSpeed = playbackSpeed;
    }
}
