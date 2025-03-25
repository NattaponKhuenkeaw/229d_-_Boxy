using UnityEngine;

/// <summary>
/// CameraFollowerV2 class is responsible for following the player with a certain offset.
/// And also support Camera view switching including
/// 1. Rear view (behind the player)
/// 2. Front view (in front of the player)
/// using Keyboard Key "C" for Rear View and "V" for Front View
/// </summary>
public class CameraFollowerV2 : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    void Update()
    {
        // Student code starts here ...
        // handle input and switch camera view
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
