using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAround : MonoBehaviour
{
    public GameObject sun; //this will be orbitted around
    public float speed = 10f; //speed of orbitting object
    public Vector3 orbitRotation = new Vector3(0f, 0f, 0f);

    private void Start()
    {

    }

    void FixedUpdate()
    {
        OrbitsAround(); //this one will make an object orbit around the 'sun' object
    }

    private void OrbitsAround()
    {
        transform.RotateAround(sun.transform.position, orbitRotation, speed * Time.deltaTime); //make thing orbit
        

    }
}
