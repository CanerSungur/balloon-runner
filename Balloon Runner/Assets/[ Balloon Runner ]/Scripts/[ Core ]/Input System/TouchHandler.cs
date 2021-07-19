using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using CanerSungur.InputSystem;

public class TouchHandler : Singleton<TouchHandler>
{
    //public enum HorizontalDirection { None, Left, Right}
    //public HorizontalDirection _HorizontalDirection = HorizontalDirection.None;
    //public enum VerticalDirection { None, Forward, Back }
    //public VerticalDirection _VerticalDirection = VerticalDirection.None;


    public static UnityEvent OnTapDetected = new UnityEvent();
    public static UnityEvent OnTouchStart = new UnityEvent();
    public static UnityEvent OnTouchEnd = new UnityEvent();
    public bool IsTouching = false;

    public static Vector2 TouchPosition;

    private void OnEnable()
    {
        OnTouchStart.AddListener(() => IsTouching = true);
        OnTouchEnd.AddListener(() => IsTouching = false);
    }

    private void OnDisable()
    {
        OnTouchStart.RemoveListener(() => IsTouching = true);
        OnTouchEnd.RemoveListener(() => IsTouching = false);
    }

    private void Update()
    {
        if (EventSystem.current == null) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            // You can start level here. Like tap to start.

            //LevelManager.Instance.StartLevel();
        }

        TapInput();
        GetTouchInput();
    }

    #region Tap Input

    private float tapInputDownTime;
    private float tapDuration;
    private void TapInput()
    {
        //Editor Input
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            tapInputDownTime = Time.time;

        if (Input.GetMouseButtonUp(0))
        {
            tapDuration = Mathf.Abs(tapInputDownTime - Time.time);

            if (tapDuration < 0.2f)
            {
                OnTapDetected.Invoke();
                //Debug.Log("Tap " + tapDuration);
            }
            tapDuration = 0;

        }
#else //Android and IOS Input


        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    tapInputDownTime = Time.time;
                    break;
                case TouchPhase.Moved:
                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    tapDuration = Mathf.Abs(tapInputDownTime - Time.time);

                    if (tapDuration < 0.2f)
                    {
                        OnTapInput.Invoke();
                        Debug.Log("Tap " + tapDuration);
                    }
                    tapDuration = 0;
                    GameManager.Instance.StartGame();
                    LevelManager.Instance.StartLevel();
                    break;
                case TouchPhase.Canceled:
                    tapDuration = Mathf.Abs(tapInputDownTime - Time.time);

                    if (tapDuration < 0.2f)
                    {
                        OnTapInput.Invoke();
                        Debug.Log("Tap " + tapDuration);
                    }
                    tapDuration = 0;
                    break;
                default:
                    break;
            }
        }
#endif
    }
    #endregion

    #region Touch Input

    private void GetTouchInput()
    {
        Vector2 firstTouchPos;
        Vector2 mousePos;
        if (Input.GetMouseButtonDown(0) && !IsTouching)
        {
            OnTouchStart.Invoke();
            Debug.Log("Touch Started!");
        }

        if (Input.GetMouseButtonUp(0) && IsTouching)
        {
            OnTouchEnd.Invoke();
            Debug.Log("Touch Ended!");
        }

        if (Input.GetMouseButton(0))
        {
            TouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TouchActions.GetTouchPosition(TouchPosition);
            //if (mousePos.x < 0)
            //    _HorizontalDirection = HorizontalDirection.Left;
            //else if (mousePos.x > 0)
            //    _HorizontalDirection = HorizontalDirection.Right;
        }
        else
            TouchPosition = Vector2.zero;

        Debug.Log(TouchPosition);
    }

    #endregion
}
