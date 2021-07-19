using UnityEngine;
using UnityEngine.Events;

namespace CanerSungur.CameraSystem
{
    public static class CameraFollow
    {
        /// <summary>
        /// Follows target smoothly.
        /// Put this function into FixedUpdate
        /// </summary>
        /// <param name="camera">Cache and put camera transform in here</param>
        /// <param name="target"></param>
        /// <param name="offset">Distance between target and camera.</param>
        /// <param name="smoothTime"></param>
        public static void FollowSmoothly(Transform camera, Transform target, Vector3 offset, float smoothTime)
        {
            Vector3 targetPosition = target.position + offset;
            camera.position = Vector3.Lerp(camera.position, targetPosition, smoothTime * Time.fixedDeltaTime);
        }

        /// <summary>
        /// Folllows target smoothly on Z axis only.
        /// Put this function into FixedUpdate
        /// </summary>
        /// <param name="camera">Cache and put camera transform in here</param>
        /// <param name="target"></param>
        /// <param name="offset">Distance between target and camera.</param>
        /// <param name="smoothTime"></param>
        public static void FollowSmoothlyOnZAxis(Transform camera, Transform target, Vector3 offset, float smoothTime)
        {
            Vector3 targetPosition = new Vector3(camera.position.x, camera.position.y, target.position.z + offset.z);
            camera.position = Vector3.Lerp(camera.position, targetPosition, smoothTime * Time.fixedDeltaTime);
        }

        /// <summary>
        /// Folllows target smoothly on Y axis only.
        /// Put this function into FixedUpdate
        /// </summary>
        /// <param name="camera">Cache and put camera transform in here</param>
        /// <param name="target"></param>
        /// <param name="offset">Distance between target and camera.</param>
        /// <param name="smoothTime"></param>
        public static void FollowSmoothlyOnYAxis(Transform camera, Transform target, Vector3 offset, float smoothTime)
        {
            Vector3 targetPosition = new Vector3(camera.position.x, target.position.y + offset.y, camera.position.z);
            camera.position = Vector3.Lerp(camera.position, targetPosition, smoothTime * Time.fixedDeltaTime);
        }
    }

    public static class CameraZoom
    {
        /// <summary>
        /// Zooms in until Camera reaches the given FOV
        /// </summary>
        /// <param name="cam"></param>
        /// <param name="fov">Field of View</param>
        /// <param name="zoomInSpeed"></param>
        public static void ZoomIn(Camera cam, float fov, float zoomInSpeed)
        {
            if (cam.fieldOfView >= fov)
                cam.fieldOfView -= Time.deltaTime * zoomInSpeed;
        }

        /// <summary>
        /// Zooms out until Camera reaches the given FOV
        /// </summary>
        /// <param name="cam"></param>
        /// <param name="fov">Field of View</param>
        /// <param name="zoomOutSpeed"></param>
        public static void ZoomOut(Camera cam, float fov, float zoomOutSpeed)
        {
            if (cam.fieldOfView <= fov)
                cam.fieldOfView += Time.deltaTime * zoomOutSpeed;
        }
    }

    public static class CameraTransition
    {
        /// <summary>
        /// Makes transition without rotation.
        /// When transition ends, it invokes the given event.
        /// You have to set up a bool for canStartTransition & set true from somewhere
        /// Put this function in Update
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="targetPosition"></param>
        /// <param name="speed"></param>
        /// <param name="canStartTransition"></param>
        /// <param name="transitionFinished">Event that will be triggered when transition ends.</param>
        public static void MakeCameraTransitionWithoutRotation(Transform camera, Vector3 targetPosition, float speed, bool canStartTransition, UnityEvent transitionFinished)
        {
            if (canStartTransition)
                camera.position = Vector3.Lerp(camera.position, targetPosition, speed * Time.deltaTime);

            if ((camera.position - targetPosition).sqrMagnitude <= 0.2f)
            {
                canStartTransition = false;
                transitionFinished.Invoke();
            }
        }
        /// <summary>
        /// Makes transition with rotation.
        /// When transition ends, it invokes the given event.
        /// You have to set up a bool for canStartTransition & set true from somewhere
        /// Put this function in Update
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="targetPosition"></param>
        /// <param name="cameraRotation"></param>
        /// <param name="targetRotation"></param>
        /// <param name="speed"></param>
        /// <param name="canStartTransition"></param>
        /// <param name="transitionFinished">Event that will be triggered when transition ends.</param>
        public static void MakeCameraTransitionWithRotation(Transform camera, Vector3 targetPosition, Quaternion cameraRotation, Quaternion targetRotation, float speed, bool canStartTransition, UnityEvent transitionFinished)
        {
            if (canStartTransition)
            {
                camera.position = Vector3.Lerp(camera.position, targetPosition, speed * Time.deltaTime);
                camera.rotation = Quaternion.Lerp(camera.rotation, targetRotation, speed * Time.deltaTime);
            }

            if ((camera.position - targetPosition).sqrMagnitude <= 0.2f)
            {
                canStartTransition = false;
                transitionFinished.Invoke();
            }
        }
    }
}