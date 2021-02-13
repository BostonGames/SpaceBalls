using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    [SerializeField] GameObject powerup0Indicator;
    [SerializeField] GameObject powerup1Indicator;
    [SerializeField] float speed =  50.0f;
    [SerializeField] float horizontalSpeed = 50.0f;
    [SerializeField] float forceAmount = 20.0f;
    [SerializeField] float powerUpForce = 5.0f;
    public bool hasPowerup0;
    public bool hasPowerup1;

    [SerializeField] AudioClip powerup0PickupClip;
    [SerializeField] AudioClip powerup1PickupClip;
    [SerializeField] AudioClip impactClip;

    private AudioSource playerAudio;



    //[SerializeField] Vector3 powerup0Offset = new Vector3(0.0f, -1.5f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();
        focalPoint = GameObject.Find("Focal Point");
        hasPowerup0 = false;
        hasPowerup1 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            TouchMoveH();
            TouchMoveV();
        }
        else
        {
            KeyMove();
        }
    }

    void TouchMoveH()
    {       
        
        //this works for the left mouse button as well as touch on mobile devices
        if (Input.GetMouseButton(0))
        {
            //this takes the screen position of the pointer/touch position on the viewport and converts it into game space
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //HORIZONTAL MOVEMENT
            if (touchPos.x < 0)
            {
                //move left
                playerRb.velocity = -focalPoint.transform.right * horizontalSpeed * Time.deltaTime;
            }
            else if (touchPos.x > 0)
            {
                //move right
                playerRb.velocity = focalPoint.transform.right * horizontalSpeed * Time.deltaTime;
            }


        }
        else
        {
           // playerRb.velocity = Vector2.zero;
        }
    }

    void TouchMoveV()
    {


        //this works for the left mouse button as well as touch on mobile devices
        if (Input.GetMouseButton(0))
        {
            //this takes the screen position of the pointer/touch position on the viewport and converts it into game space
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);           

            //VERTICAL MOVEMENT
            if (touchPos.y < 9.5f)
            {
                //move backward
                playerRb.velocity = -focalPoint.transform.forward * horizontalSpeed * Time.deltaTime;
            }
            else if (touchPos.y > 9.5f)
            {
                //move forward
                playerRb.velocity = focalPoint.transform.forward * horizontalSpeed * Time.deltaTime;
            }
        }
        else
        {
            //playerRb.velocity = Vector2.zero;
        }
    }

    void KeyMove()
    {
        //VERTICAL MOVEMENT
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput * Time.deltaTime, ForceMode.Acceleration);




        //HORIZONTAL MOVEMENT
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //move left
            playerRb.velocity = -focalPoint.transform.right * horizontalSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            playerRb.velocity = focalPoint.transform.right * horizontalSpeed * Time.deltaTime;
        }
        else
        {
            //playerRb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        //info for specific enemies here
        if (collision.gameObject.name.Contains("Enemy"))
        {
            Debug.Log("Player has collided with: " + collision.gameObject.name + " with powerup set to " + hasPowerup0);
            playerAudio.PlayOneShot(impactClip, 1.0f);
        }

        //applies to all enemies
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();

            enemyRB.AddForce(-transform.forward * forceAmount, ForceMode.Impulse);
            gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * (forceAmount * 1.5f), ForceMode.Impulse);

        }    
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup0)
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();

            enemyRB.AddForce(-transform.forward * forceAmount * powerUpForce, ForceMode.Impulse);
            gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * (forceAmount / 1.1f), ForceMode.Impulse);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Powerup"))
        {            

            if (other.gameObject.name.Contains("Star"))
            {
                hasPowerup0 = true;
                powerup0Indicator.gameObject.SetActive(true);
                playerAudio.PlayOneShot(powerup0PickupClip, 1.0f);
                Debug.Log("Player has colleted " + other.gameObject.name + ", powerup set to " + hasPowerup0);
                StartCoroutine(PowerupCountdownRoutine(powerup0Indicator,7.0f));
            }

            if (other.gameObject.name.Contains("Moon"))
            {
                hasPowerup1 = true;
                powerup1Indicator.gameObject.SetActive(true);
                playerAudio.PlayOneShot(powerup1PickupClip, 1.0f);
                Debug.Log("Player has colleted " + other.gameObject.name + ", powerup set to " + hasPowerup1);
                StartCoroutine(PowerupCountdownRoutine(powerup1Indicator, 15.0f));
            }
        }
    }

    IEnumerator PowerupCountdownRoutine(GameObject powerup, float secs)
    {
        yield return new WaitForSeconds(secs);
        hasPowerup0 = false;
        powerup.gameObject.SetActive(false);


    }
}
