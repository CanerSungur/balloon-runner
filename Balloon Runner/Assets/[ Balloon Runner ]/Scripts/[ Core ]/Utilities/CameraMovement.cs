using UnityEngine;
using CanerSungur.CameraSystem;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera Setup")]
    private Transform cam;
    [SerializeField] private Transform target;
    private Vector3 offset;
    [SerializeField] private float smoothTime = 2f;

    private void Awake()
    {
        offset = cam.position - target.position;       
    }

    private void FixedUpdate()
    {
        CameraFollow.FollowSmoothlyOnZAxis(cam, target, offset, smoothTime);
    }
}
