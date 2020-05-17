using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Player player;
    public Vector3 lookOffset = new Vector3(0, 1f, 0);
    public float maxCameraDistance = 3f;
    public float followTime = 0.5f;

    private Vector3 currentVelocity;

    void Update()
    {
        Vector3 offsetDirection = -player.GetLookDirection();
        Vector3 targetOffset = 
            lookOffset.z * player.GetForwardDirection() + 
            lookOffset.x * player.GetStrafeDirection() + 
            lookOffset.y * Vector3.up;
        Vector3 lookTarget = player.transform.position + targetOffset;

        float cameraDistance = maxCameraDistance;
        RaycastHit hit;
        Ray ray = new Ray(lookTarget, offsetDirection);
        if (Physics.Raycast(ray, out hit, maxCameraDistance))
        {
            cameraDistance = hit.distance;
        }

        Vector3 cameraPosition = lookTarget + (offsetDirection * cameraDistance);
        transform.position = Vector3.SmoothDamp(transform.position, cameraPosition, ref currentVelocity, followTime);
        transform.LookAt(lookTarget);
    }
}
