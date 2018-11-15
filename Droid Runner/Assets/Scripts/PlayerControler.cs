using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour {

    public float speed;
    public Transform camTransform = Camera.main.transform;
    float rotation;
    public Vector3 movement;
    public GameObject head;
    public float rotationSpeed;
    private Rigidbody rb;

    public Text  scoreText;
    public Text timeText;
    public GameObject coins;
    public bool game;
    public AudioSource speaker;
    public AudioClip sf;
    float moveHorizontal;
    float moveVertical;
    float sprint;
    float mouse;

    public Button restart;


    private int scoreCounter;
    public int timeCounter;
    public int startime = 1000;
    public Transform player;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        scoreCounter = 0;
        scoreText.text = " Score: " +scoreCounter;
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
       

        if (startime <= 0){
            game = true;
            timeCounter--;
            Cursor.lockState = CursorLockMode.Locked;
             moveHorizontal = Input.GetAxis("Horizontal");
             moveVertical = Input.GetAxis("Vertical");
             sprint = Input.GetAxis("Sprint");
             mouse = Input.GetAxis("Mouse X");
       
        }


        if (timeCounter<= 0)
        {
            timeText.text = "Time is over";
            game = false;
        }
        else
        {
            game = true;
            timeText.text = " Time: " + timeCounter;
        }

        /*if (restart)
        {
            scoreCounter = 0;
            scoreText.text = " Score: " + scoreCounter;
            timeText.text = " Time: " + timeCounter;
            startime = 1000;
            game = true;
            player.position = new Vector3(0f,5f,0f);
        }*/

    }
    

    private void FixedUpdate()
    {

        float instSpeed = speed;

        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = camTransform.TransformDirection(movement);
        float r = camTransform.rotation.y;
        head.transform.Rotate(Vector3.forward, ((mouse) * rotationSpeed));
        if (sprint > 0)
        {
            instSpeed += 15;
        }
        else
        {
            instSpeed = speed;
        }
        if (game)
        {
            rb.AddForce(movement * instSpeed);
        }

        if (!game)
        {
            coins.SetActive(false);
        }

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
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
