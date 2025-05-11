using UnityEngine;

public class MoveBack : MonoBehaviour
{
    public float speed = 10f;

    private float leftBound = -15;

    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.isGameOver)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }

        if (transform.position.z < leftBound && !CompareTag("Road"))
        {
            Destroy(gameObject);
        }
    }
}