using UnityEngine;

public class MoveBack : MonoBehaviour
{
    public float speed;
    private float leftBound = -15f;

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z < leftBound)
        {
            Destroy(gameObject);
        }
    }
}