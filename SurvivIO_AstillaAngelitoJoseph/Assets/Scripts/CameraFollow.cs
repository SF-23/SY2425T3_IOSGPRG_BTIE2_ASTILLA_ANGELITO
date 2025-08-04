using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform player; // Assign your player's Transform here in the Inspector
    [SerializeField] private Vector3 offset;   // Offset from the player (e.g., 0, 5, -10 for a top-down back view)
    [SerializeField] private float smoothSpeed = 0.125f; // How smoothly the camera moves

    // Update is called once per frame
    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (player != null)
        {
            Vector3 desiredPosition = player.position + offset;
            // Smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
