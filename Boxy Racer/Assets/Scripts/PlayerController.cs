using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Car Settings")]
    public float maxSpeed = 20.0f;
    public float speedIncreasePerWave = 2f;
    public float xRange;
    public int score;
    public int hp = 3;
    public Image[] hearts;

    [Header("Audio and Effects")]
    public ParticleSystem dieParticle;
    public ParticleSystem takeParticle;
    public AudioClip die;
    public AudioClip take;
    public AudioClip win;

    public static bool isGameOver = false;

    private int currentHp;
    private float horizontalInput = 0;
    private AudioSource playerAudio;
    private GameManager gameManager;
    private bool isInvincible = false;
    private MeshRenderer playerRenderer;

    private void Awake()
    {
        currentHp = hp;
        playerAudio = GetComponent<AudioSource>();
        playerRenderer = GetComponent<MeshRenderer>(); // หรือ GetComponentInChildren<MeshRenderer>(); ถ้าอยู่ในลูก
    }

    void Start()
    {
        ResetPlayer();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        // รับค่าการเคลื่อนที่จาก Input System
        InputAction moveAction = InputSystem.actions.FindAction("Move");
        Vector2 input = moveAction.ReadValue<Vector2>();
        horizontalInput = input.x;

        // ขยับรถไปซ้ายขวา (แกน X)
        transform.Translate(Vector3.right * horizontalInput * maxSpeed * Time.deltaTime);

        // จำกัดขอบเขตการเคลื่อนที่
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
        if (isInvincible) return;

        currentHp--;

        if (hp >= 0 && currentHp < hearts.Length)
        {
            hearts[currentHp].enabled = false;
        }

        if (currentHp <= 0 && !isGameOver)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityCooldown());
        }
    }

    IEnumerator InvincibilityCooldown()
    {
        isInvincible = true;

        float blinkDuration = 0.2f;
        float elapsed = 0f;

        while (elapsed < 2f)
        {
            if (playerRenderer != null)
            {
                playerRenderer.enabled = false;
                yield return new WaitForSeconds(blinkDuration / 2);
                playerRenderer.enabled = true;
                yield return new WaitForSeconds(blinkDuration / 2);
            }

            elapsed += blinkDuration;
        }

        isInvincible = false;
    }

    void Die()
    {
        isGameOver = true;
        gameManager.gameOverPanel.SetActive(true);
        playerAudio.PlayOneShot(win);
        Time.timeScale = 0f;
    }
    public void ResetPlayer()
    {
        // รีเซ็ตสถานะพื้นฐาน
        currentHp = hp;
        isGameOver = false;
        isInvincible = false;
        maxSpeed = 20.0f;

        // เปิด Heart UI ทั้งหมด
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = true;
        }

        // รีเซ็ตตำแหน่ง
        transform.position = new Vector3(0, transform.position.y, transform.position.z);

        // รีเซ็ตเวลา
        Time.timeScale = 1f;

        Debug.Log("Player reset แล้ว");
    }

}
