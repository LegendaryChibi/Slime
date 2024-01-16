using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slime : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private LayerMask _groundMask;
    [SerializeField]
    private Transform _groundCheck;
    private float _groundCheckRadius = 0.3f;

/*    [SerializeField]
    private Camera _camera;*/

    [SerializeField]
    private Rigidbody _rigidbody;
    private Vector3 _direction;

    private float _speed = 8;
    private float _jumpForce = 5f;

    private bool isGrounded;
    private bool jumped = false;
    private bool GroundedWhileRunning;

    [SerializeField]
    private GameObject body;

    public GameObject SlimeMesh;

    public float frequency;
    private float lastSpawned;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        _direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (_direction.magnitude > 0.1f && !isGrounded)
        {
            _direction /= 2;
        }

        isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundMask);
        animator.SetBool("Landed", isGrounded);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!jumped && isGrounded)
            {
                StartCoroutine(Jump());
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && isGrounded)
        {
            animator.SetTrigger("Attack");
        }

        if (Input.GetKey(KeyCode.LeftShift) && isGrounded && _direction.magnitude > 0.1f)
        {
            body.SetActive(false);
            if (Time.time > lastSpawned + frequency)
            {
                Sprint();
                lastSpawned = Time.time;
            }
        }
        else
        {
            body.SetActive(true);
            _speed = 8;
        }
    }

    void Sprint()
    {
        _speed = 12;
        GameObject slimeTrail = ObjectPoolManager.Instance.GetPooledObject(ObjectPoolManager.PoolTypes.SlimePool);
        slimeTrail.SetActive(true);
        slimeTrail.transform.position = transform.position;
        slimeTrail.transform.rotation = Quaternion.identity;
    }

    private IEnumerator Jump()
    {
        jumped = true;
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        yield return new WaitForSeconds(2f);
        jumped = false;
    }

    void FixedUpdate()
    {
        bool isRunning = _direction.magnitude > 0.1f;
        GroundedWhileRunning = isRunning && isGrounded;

        animator.SetBool("Move", GroundedWhileRunning);

        if (isRunning)
        {
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            moveDirection.Normalize();

            _rigidbody.MovePosition(_rigidbody.position + moveDirection * (_speed * Time.fixedDeltaTime));
        }
    }
}
