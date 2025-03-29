using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AdvancePlayerController : MonoBehaviour
{
    // [1] Define the car settings
    [Header("Car Settings")]
    public float maxSpeed = 20.0f;
    public float acceleration = 10.0f;
    public float deceleration = 15.0f;
    public float turnSpeed = 180.0f;
    public float brakingForce = 20.0f;
    public int score;
    public ParticleSystem dieParticle;
    public ParticleSystem takeParticle;
    public AudioClip die;
    public AudioClip take;
    public AudioClip win;
    private bool isPaused = false;

     
    

    private AudioSource playerAudio;
    
        

    

    // [2] Define the current state of the car
    [Header("Current State")]
    private float currentSpeed = 0.0f;
    private float horizontalInput = 0;
    private float forwardInput = 0;
    private bool isBraking = false;
    
    
    
    
    private GameManager gameManager;

    private void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && !gameManager.isGameStop)
        {
            
            gameManager.TogglePause();
            
        }
        
        // [3] Get input values
        InputAction moveAction = InputSystem.actions.FindAction("Move");
        Vector2 input = moveAction.ReadValue<Vector2>();
        horizontalInput = input.x;
        forwardInput = input.y;

        // [4] Handle braking
        isBraking = Input.GetKey(KeyCode.Space);

        // [5] Apply acceleration/deceleration
        // and Calculate currentSpeed
        if (forwardInput != 0)
        {
            // [6] Apply acceleration to currentSpeed
            currentSpeed += forwardInput * acceleration * Time.deltaTime;
        }
        else
        {
            // [7] Natural deceleration when no input
            float decelAmount = deceleration * Time.deltaTime;
            if (Mathf.Abs(currentSpeed) <= decelAmount)
                currentSpeed = 0;
            else
                currentSpeed -= Mathf.Sign(currentSpeed) * decelAmount;
        }

        // [8] Apply braking
        if (isBraking)
        {
            float brakeAmount = brakingForce * Time.deltaTime;
            if (Mathf.Abs(currentSpeed) <= brakeAmount)
                currentSpeed = 0;
            else
                currentSpeed -= Mathf.Sign(currentSpeed) * brakeAmount;
        }

        // [9] Clamp speed
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        // [10] **Apply movement**
        transform.Translate(currentSpeed * Time.deltaTime * Vector3.forward);

        // [11] Apply steering (only when moving)
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            float turnAmount = horizontalInput * turnSpeed * Time.deltaTime;
            // Reduce turning at higher speeds
            turnAmount *= Mathf.Lerp(1.0f, 0.5f, Mathf.Abs(currentSpeed) / maxSpeed);
            transform.Rotate(Vector3.up, turnAmount);
        }
    }
    

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Good"))
        {
            
            gameManager.AddScore(score);
            Destroy(other.gameObject);
            takeParticle.Play();
            playerAudio.PlayOneShot(take);
        }
        
        if (other.gameObject.CompareTag("Win"))
        {
            gameManager.isGameStop = true;
            gameManager.gameWinPanel.SetActive(true);
            playerAudio.PlayOneShot(win);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Stop();
            gameManager.TogglePause();
            
        }
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bad"))
        {
            gameManager.isGameStop = true;
            dieParticle.Play();
            gameManager.gameOverPanel.SetActive(true);
            playerAudio.PlayOneShot(die);
            gameManager.TogglePause();
            
            
        }
    }
}

