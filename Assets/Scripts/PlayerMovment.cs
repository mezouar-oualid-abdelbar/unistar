using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    public float speed = 12f;
    public float gravity = -30f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        bool sphereCheck = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        bool rayCheck = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance + 0.1f, groundMask);

        // Increase the distance slightly so it "reaches" for the floor
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.6f, groundMask);

        Debug.DrawRay(groundCheck.position, Vector3.down * groundDistance, isGrounded ? Color.green : Color.red);


        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // --- JUMP SECTION WITH DEBUG ---
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                Debug.Log("JUMP SUCCESS!");
            }
            else
            {
                // This will now tell you the exact distance to the floor
                RaycastHit hit;
                if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, 10f))
                {
                    Debug.Log("Jump failed. Floor is " + hit.distance + " units away. Your groundDistance is " + groundDistance);
                }
                else
                {
                    Debug.Log("Jump failed. No floor detected at all beneath GroundCheck!");
                }
            }
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (lastPosition != gameObject.transform.position && isGrounded == true)
        {
            isMoving = true;

        }
        else
        {
            isMoving = false;
        }

        lastPosition = gameObject.transform.position;

    }
}