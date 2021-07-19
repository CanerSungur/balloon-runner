using UnityEngine;

public class RunnerCharacterController : MonoBehaviour
{
    private CharacterController controller;
    public CharacterController Controller { get { return controller == null ? controller = GetComponent<CharacterController>() : controller; } }

    private Collider coll;
    public Collider Collider { get { return coll == null ? coll = GetComponent<Collider>() : coll; } }

    private Character player;
    public Character Player { get { return player == null ? player = GetComponent<Character>() : player; } }

    [Header("Controller Setup")]
    private Vector3 playerVelocity;
    private bool playerIsGrounded;
    private float gravityValue = -9.81f;
    [SerializeField] private LayerMask groundLayerMask;
    private Vector2 swerveLimit = new Vector2(-3.5f, 3.5f);

    [Header("Speed Setup")]
    public float ForwardSpeed;
    public float SwerveSpeed;

    [Header("Joystick Setup")]
    //[SerializeField] private Joystick joystick;

    public static bool IsMoving;

    private void Start()
    {
        IsMoving = false;

        #region Character Controller Setup

        //Controller.stepOffset = 0.3f;
        //Controller.skinWidth = 0.08f;
        //Controller.center = new Vector3(0f, 17.08f, 5.7f);
        //Controller.radius = 17.51f;
        //Controller.height = 14.1f;

        #endregion
    }

    private void Update()
    {
        if (!GameManager.GameIsStarted)
            return;

        playerIsGrounded = IsGrounded();

        if (playerIsGrounded && playerVelocity.y < 0f)
            playerVelocity.y = 0f;

        Vector3 move = new Vector3(/*joystick.Horizontal*/ 1 * SwerveSpeed, 0, ForwardSpeed);
        Controller.Move(move * Time.deltaTime);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            IsMoving = true;
        }
        else
            IsMoving = false;

        playerVelocity.y += gravityValue * Time.deltaTime;
        Controller.Move(playerVelocity * Time.deltaTime);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, swerveLimit.x, swerveLimit.y), transform.position.y, transform.position.z);
    }

    private void CenterPlayer()
    {
        // Center Player
        transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private bool IsGrounded()
    {
        // We look down to see if there's a collider of a specific below us.
        return Physics.Raycast(Collider.bounds.center, Vector3.down, Collider.bounds.extents.y + 0.05f, groundLayerMask);
    }
}
