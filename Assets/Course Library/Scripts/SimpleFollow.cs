using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{
    [SerializeField] Transform followedObject;

    Vector3 offset;
    [SerializeField] float smoothRate;

    // Start is called before the first frame update
    void Start()
    {
        //offset = followedObject.position - transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector3 currentPos = transform.position;
        //Vector3 newPos = followedObject.position - offset;
        Vector3 newPos = followedObject.position;

        // change the transform.position so that 
        //every time the frame is called, the currentPos will slowly move toward newPos 
        //       at the rate of smoothRate


        //transform.position = Vector3.Lerp(currentPos, newPos, smoothRate * Time.deltaTime);
        transform.position = newPos;
    }
}
