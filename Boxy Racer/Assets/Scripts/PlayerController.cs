using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    
    [Header("Car Settings")]
    public float maxSpeed = 20.0f;
    public int score;
    public ParticleSystem dieParticle;
    public ParticleSystem takeParticle;
    public AudioClip die;
    public AudioClip take;
    public AudioClip win;
    public float xRange;
    public static bool isGameOver = false;
    private AudioSource playerAudio;
    private float horizontalInput = 0;
    public float speedIncreasePerWave = 2f;
    public int hp = 3;
    private int currentHp;
    public Image[] hearts;



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
        
        //รับค่าการเคลื่อนที่จาก Input System
        InputAction moveAction = InputSystem.actions.FindAction("Move");
        Vector2 input = moveAction.ReadValue<Vector2>();
        horizontalInput = input.x;

        //ขยับรถไปซ้ายขวา (แกน X)
        transform.Translate(Vector3.right * horizontalInput * maxSpeed * Time.deltaTime);
            
        //ห้ามรถเกิน
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
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
        if (other.gameObject.CompareTag("Bad") && !isGameOver)
        {
            
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(die);
            dieParticle.Play();
            TakeDamage();
        }
        
        
    }
    
    public void IncreaseSpeedByWave(int wave)
    {
        maxSpeed += speedIncreasePerWave;
        Debug.Log($"Player speed increased! New speed: {maxSpeed}");
    }



    void TakeDamage()
    {
        hp --;

        if (hp >= 0 && currentHp < hearts.Length)
        {
            hearts[currentHp].enabled = false;
        }

        if (currentHp <= 0 && !isGameOver)
        {
            
            Die();
        }
    }

    void Die()
    {
        isGameOver = true;
        gameManager.gameOverPanel.SetActive(true);
        playerAudio.PlayOneShot(win);
        Time.timeScale = 0f;
    }
    
}

