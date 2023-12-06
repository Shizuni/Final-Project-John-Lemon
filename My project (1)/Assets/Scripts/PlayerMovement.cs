using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;
    public float walkSpeed = 2f;
    public float sprintSpeed = 4f; 

    private Animator m_Animator;
    private Rigidbody m_Rigidbody;
    private AudioSource m_AudioSource;
    private Vector3 m_Movement;
    private Quaternion m_Rotation = Quaternion.identity;
    private bool isSprinting = false; 

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        
        isSprinting = Input.GetKey(KeyCode.LeftShift);

        
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

        
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * speed * Time.fixedDeltaTime);
    }

    private void OnAnimatorMove()
    {
        
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
