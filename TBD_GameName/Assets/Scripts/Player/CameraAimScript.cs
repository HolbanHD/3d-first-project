using UnityEngine;

/// <summary>
/// A script that is on the camera holder and assigns it to the selected location on the player.
/// </summary>

namespace Player
{

    public class MoveCamera : MonoBehaviour
    {
        Transform cameraPosition;

        void Start()
        {
            cameraPosition = GameObject.Find("playerCameraPos").transform;
        }

        void Update()
        {
            transform.position = cameraPosition.position;
        }
    }
}
