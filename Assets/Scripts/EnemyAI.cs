using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator anim;
    public float speed;  // 적의 이동 속도
    public float stopTimeAfterFlashlight; // 손전등 비춘 후 정지 유지 시간
    private float stopTimer = 0f;
    private bool isLightStopped = false;
    private bool isFlashing = false;
    public float flashlightDuration = 0f;
    private bool isGrounded = false;
    private Vector3 moveDirection = Vector3.right; // 오른쪽으로 이동

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (isGrounded && !isLightStopped && !isFlashing)
        {
            anim.SetBool("isMoving", true);
            Move();
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    private void Update()
    {
        if (isFlashing)
        {
            flashlightDuration += Time.deltaTime;
            anim.SetBool("isMoving", false);
        }

        if (!isFlashing && flashlightDuration >= 2.5f && !isLightStopped)
        {
            isLightStopped = true;
            stopTimer = 0f;
        }

        if (isLightStopped)
        {
            stopTimer += Time.deltaTime;
            if (stopTimer >= stopTimeAfterFlashlight)
            {
                isLightStopped = false;
                flashlightDuration = 0f;
            }
        }
    }

    private void Move()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Flashlight"))
        {
            isFlashing = true;
            anim.SetBool("isMoving", false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Flashlight"))
        {
            isFlashing = false;
            if (!isLightStopped)
            {
                anim.SetBool("isMoving", true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}