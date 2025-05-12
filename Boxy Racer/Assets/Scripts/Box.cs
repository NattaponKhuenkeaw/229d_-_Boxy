using UnityEngine;

public class Box : MonoBehaviour
{
    private float moveSpeed;
    private float leftBound = -15f;

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
        Debug.Log("Box speed set to: " + moveSpeed);
    }

    void Update()
    {
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        if (transform.position.z < leftBound)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           
            Destroy(gameObject);
        }
    }
}