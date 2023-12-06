using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public float walkSpeed = 0.0001f;
    public float sprintSpeed = 0.3f; // New variable for sprint speed

    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private AudioSource m_AudioSource;
    private Vector3 m_Movement;
    private Quaternion m_Rotation = Quaternion.identity;
    private bool isSprinting = false; // New variable to track sprinting state

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Check if the sprint key is held down
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        // You can add additional checks here for other keys if needed
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        // Adjust speed based on whether sprint key is held down
        float speed = isSprinting ? sprintSpeed : walkSpeed;

        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        // Apply movement with adjusted speed
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * speed * Time.fixedDeltaTime);
    }

    private void OnAnimatorMove()
    {
        // Move rotation as before
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
