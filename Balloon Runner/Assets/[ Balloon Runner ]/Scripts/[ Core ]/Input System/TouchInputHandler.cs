using UnityEngine;
using System;
using UnityEngine.Events;

namespace CanerSungur.InputSystem
{
    public static class SwipeActions
    {
        public static UnityEvent OnSwipeUp = new UnityEvent();
        public static UnityEvent OnSwipeDown = new UnityEvent();
        public static UnityEvent OnSwipeRight = new UnityEvent();
        public static UnityEvent OnSwipeLeft = new UnityEvent();

        public static void Move(Swipe swipe, Vector2 direction)
        {
            //if (!Character.IsControlable)
            //    return;

            switch (swipe)
            {
                case Swipe.Up:
                    OnSwipeUp.Invoke();
                    break;
                case Swipe.Down:
                    OnSwipeDown.Invoke();
                    break;
                case Swipe.Left:
                    OnSwipeLeft.Invoke();
                    break;
                case Swipe.Right:
                    OnSwipeRight.Invoke();
                    break;
                default:
                    Debug.LogError("Something went wrong with the Swipe Input!");
                    break;
            }
        }
    }

    public static class TouchActions
    {
        public static Vector2 GetTouchPosition(Vector2 pos)
        {
            return pos;
        }
        public static void MoveWithAddForce(Rigidbody rb, Vector2 direction, float speed, ForceMode forceMode)
        {
            rb.AddForce(direction * speed, forceMode);
        }
    }
}