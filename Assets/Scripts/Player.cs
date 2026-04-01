using UnityEngine;
using UnityEngine.InputSystem;



public class Player : MonoBehaviour
{
    private Vector3 movement = Vector3.zero;
    private new Rigidbody rigidbody;
    private Pause pause;
    private bool isSprinting = false;

    [SerializeField]
    private LayerMask ground;
    private bool isGrounded = true;

    [SerializeReference]
    private new Camera camera;

    [SerializeField]
    private int speed = 1;

    private float xAngle = 0;

    private play inputActions;

    private PlayerInput playerinput;

    private bool inScreen = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        inputActions = new play();
    }

    private void Start()
    {
        playerinput = GetComponent<PlayerInput>();
        pause = Pause.Instance;
    }

    private void FixedUpdate()
    {
        if (pause.isPaused | inScreen)
        {
            isSprinting = false;
            return;
        }
        setCorrectDrag();

        Vector3 force = transform.TransformDirection(movement)
         * speed * 10f * (isSprinting ? 3f : 1f);
        rigidbody.AddForce(force, ForceMode.Acceleration);

        Vector3 velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);

        if (velocity.magnitude > speed * (isSprinting ? 3f : 1f))
        {
            Vector3 limitedVelocity = velocity.normalized * speed;

            rigidbody.velocity = new Vector3(limitedVelocity.x, rigidbody.velocity.y, limitedVelocity.z);
        }
    }

    private void setCorrectDrag()
    {
        isGrounded = Physics.Raycast(transform.position,
         Vector3.down,
         transform.localScale.y + 0.1f,
         ground
         );

        if (isGrounded)
        {
            rigidbody.drag = 7;
        }
        else
        {
            rigidbody.drag = 0;
        }
    }

    public void OnMove(InputValue input)
    {
        Vector2 move = input.Get<Vector2>();
        if (!inScreen)
            movement = new Vector3(move.x, 0, move.y);
    }

    public void OnLook(InputValue input)
    {
        if (camera == null || pause.isPaused || inScreen) return;
        Vector2 look = input.Get<Vector2>() / 10;
        Vector3 CapsuleMovment = new Vector3(0, look.x, 0);

        xAngle += look.y;

        xAngle = Mathf.Clamp(xAngle, -90f, 90f);

        transform.eulerAngles += CapsuleMovment;
        camera.transform.rotation = Quaternion.Euler(-xAngle, camera.transform.eulerAngles.y, camera.transform.eulerAngles.z);
        Vector3 backup = camera.transform.eulerAngles;
        camera.transform.eulerAngles = new Vector3(backup.x, backup.y, 0f);
        backup = transform.eulerAngles;
        transform.eulerAngles = new Vector3(backup.x, backup.y, 0f);
    }

    public void OnUse(InputValue input)
    {
        if (inScreen) return;
        bool used = input.Get<float>() > 0.5f;
        if (!used)
            return;

        Ray ray = camera.ScreenPointToRay(new Vector2(UnityEngine.Screen.width / 2, UnityEngine.Screen.height / 2));

        if (!Physics.Raycast(ray, out var rayhit, 3.0f))
        {
            return;
        }

        string name = rayhit.collider.gameObject.name;

        if (name == "screen")
        {
            inScreen = true;
        }

        IUsable hit = rayhit.collider.gameObject.GetComponent<IUsable>();

        hit?.Use();

    }

    public void OnPause(InputValue input)
    {
        if (!inScreen)
        {
            float pause = input.Get<float>();
            if (pause > 0.9f)
                this.pause.Set(!this.pause.isPaused);
        }
        else
        {
            OnExitComputer();
        }
    }

    public void OnSprint(InputValue input)
    {
        isSprinting = !isSprinting;
    }

    public void OnExitComputer()
    {
        Screen.Instantace.exit();
        inScreen = false;
    }

    void OnPoint(InputValue input)
    {
        MouseHandler.Position = input.Get<Vector2>();
    }

}
