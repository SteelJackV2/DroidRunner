using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class mobileController : MonoBehaviour {

    public float speed;
    public Transform camTransform = Camera.main.transform;
    float rotation;
    public Vector3 movement;
    public GameObject head;
    public float rotationSpeed;
    public FloatingJoystick move;
    public FloatingJoystick look;
    private Rigidbody rb;
    public Text scoreText;
    public Text timeText;
    public GameObject coins;
    public bool game;
    public AudioSource speaker;
    public AudioClip sf;
    private int scoreCounter;
    public int timeCounter;
    public int startime = 1000;
    public Transform player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreCounter = 0;
        scoreText.text = " Score: " + scoreCounter;
        timeText.text = " Time: " + timeCounter;
        startime = 1000;
        game = true;
        speaker = GetComponent<AudioSource>();

        camTransform = Camera.main.transform;
        player = GetComponent<Transform>();
    }

    private void Update()
    {
        head.transform.position = transform.position;
        startime--;


        if (startime <= 0)
        {
            game = true;
            timeCounter--;
        }


        if (timeCounter <= 0)
        {
            timeText.text = "Time is over";
            game = false;
        }
        else
        {
            game = true;
            timeText.text = " Time: " + timeCounter;
        }
    }
     
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        movement = new Vector3(move.Horizontal + moveHorizontal, 0.0f, move.Vertical +moveVertical);
        movement = camTransform.TransformDirection(movement);
        float r = camTransform.rotation.y;
        head.transform.Rotate(Vector3.forward, ((look.Horizontal) * rotationSpeed));


        if (game)
        {
            rb.AddForce(movement*4f);
        }

        if (!game)
        {
            coins.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            scoreCounter++;
            scoreText.text = " Score: " + scoreCounter;
            speaker.Play();
        }

    }
}
