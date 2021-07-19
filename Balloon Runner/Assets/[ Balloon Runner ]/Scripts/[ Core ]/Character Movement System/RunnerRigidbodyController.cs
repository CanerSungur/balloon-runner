using UnityEngine;

public class RunnerRigidbodyController : MonoBehaviour
{
    [Header("Controller Setup")]
    public LayerMask GroundLayer;
    private Character player;
    public Character Player { get { return player == null ? player = GetComponent<Character>() : player; } }
    
    private Rigidbody rb;
    public Rigidbody Rigidbody { get { return rb == null ? rb = GetComponent<Rigidbody>() : rb; } }

    [Header("Speed Setup")]
    public float ForwardSpeed;
    public float SwerveSpeed;

    [Header("Rotation Setup")]
    private Quaternion maxRightRotation = Quaternion.Euler(5f, 5f, -10f);
    private Quaternion maxLeftRotation = Quaternion.Euler(5f, -5f, 10f);
    private Quaternion defaultRotation = Quaternion.identity;
    private float rotationSpeed;
    private bool goLeft = false;
    private bool goRight = false;

    private void FixedUpdate()
    {
        if (!GameManager.GameIsStarted && Input.GetMouseButtonDown(0))
            EventManager.OnGameStart.Invoke();

        if (!Player.IsControlable)
            return;

        if (!GameManager.PlatformIsOver)
        {
            if (Input.GetMouseButton(0) && !GameManager.PlatformIsOver)
            {
                Vector3 mouse = Input.mousePosition;
                Ray castPoint = Camera.main.ScreenPointToRay(mouse);
                RaycastHit hit;
                if (Physics.Raycast(castPoint, out hit, Mathf.Infinity, GroundLayer))
                {
                    Vector3 newPosition = new Vector3(hit.point.x, transform.position.y, transform.position.z);
                    transform.position = Vector3.Lerp(transform.position, newPosition, SwerveSpeed);

                    CheckTurnDirection(hit.point.x, transform.position.x);
                }
            }
            else
            {
                goLeft = false;
                goRight = false;
            }

            //TurnPlayer();
        }

        Rigidbody.velocity = Vector3.up * ForwardSpeed;
        Debug.Log("Moving forward");
    }

    private void CheckTurnDirection(float targetPoint, float currentPoint)
    {
        if (targetPoint > currentPoint)
        {
            // Go Right
            goLeft = false;
            goRight = true;
        }
        else if (targetPoint < currentPoint)
        {
            // Go Left
            goLeft = true;
            goRight = false;
        }
    }

    private void TurnPlayer()
    {
        rotationSpeed = ForwardSpeed / 50f;

        if (goLeft && !goRight)
            transform.rotation = Quaternion.Lerp(transform.rotation, maxLeftRotation, rotationSpeed);
        else if (!goLeft && goRight)
            transform.rotation = Quaternion.Lerp(transform.rotation, maxRightRotation, rotationSpeed);
        else
            transform.rotation = Quaternion.Lerp(transform.rotation, defaultRotation, 0.1f);
    }

    private void Stop()
    {
        Rigidbody.velocity = Vector3.zero;
    }
    private void ContinueRunning()
    {
        Rigidbody.velocity = Vector3.forward * ForwardSpeed;
    }

    private void ResetTransform()
    {
        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        transform.rotation = Quaternion.identity;
    }

    public bool IsGoingLeft() => goLeft;
    public bool IsGoingRight() => goRight;
}