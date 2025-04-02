using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GM gameManager;

    [Header("Movement")]
    public float moveSpeed; //이동 속도
    public float jumpForce; //점프 강도
    public bool isGround = false; //땅에 닿았는지 체크

    private Rigidbody2D rb;
    private Animator anim;

    [Header("Item")]
    public bool hasItem = false; //아이템 소지 체크
    public bool isNearDoor = false; // [수정됨] 도어 근처에 있는지 체크

    [Header("Flashlight")]
    public float maxFlashlightTime; //손전등 시간
    public float flashlightTimer; //타이머
    [SerializeField] private float flashlightDrainRate; // 기본 감소 속도
    public bool isUsingFlashlight; //손전등 사용 체크
    public float flashlightRechargeRate; // 초당 충전 속도

    public GameObject flashlightLight; // 손전등 불빛 오브젝트

    [Header("Jump Settings")]
    public float gravityScale = 2f; //중력 크기
    public float fallMultiplier = 2.5f; 
    public float lowJumpMultiplier = 2f;

    private float moveInput = 0f; // 현재 이동 입력 저장
    private bool jumpPressed = false; // 점프 입력 상태 저장

    [Header("UI")]
    public Slider flashlightBar; // 손전등 배터리 UI 슬라이더
    public GameObject flashlightUI; // 손전등 UI 오브젝트 (Canvas 내 UI)

    //캐싱
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        flashlightTimer = maxFlashlightTime;
        rb.gravityScale = gravityScale;
    }

    void Start()
    {
        // UI 초기화
        if (flashlightBar != null)
        {
            flashlightBar.maxValue = maxFlashlightTime;
            flashlightBar.value = maxFlashlightTime;
            ;
        }

        if (flashlightUI != null)
        {
            flashlightUI.SetActive(false); // 손전등 UI 초기에는 숨김
        }

        // 손전등 불빛 초기 비활성화
        if (flashlightLight != null)
        {
            flashlightLight.SetActive(false); // 게임 시작 시 불빛 비활성화
        }
    }

    void Update()
    {
        HandleInput();
        Move();
        Jump();
        ModifyGravity();
        ItemUsage();
        Flashlight();

        // 손전등 키를 떼었을 때만 충전 기능을 실행하도록 변경
        if (!Input.GetKey(KeyCode.C))
        {
            RechargeFlashlight(); // 손전등 키 떼었을 때만 충전
        }

        SetFlashlightUI(); // 게이지 UI 업데이트


        if (isUsingFlashlight)  // 손전등이 켜져 있을 때 지속적으로 배터리 감소
        {
            flashlightTimer -= flashlightDrainRate * Time.deltaTime;
            flashlightTimer = Mathf.Max(flashlightTimer, 0f); // 0 이하로 내려가지 않도록 보정
        }

    }
    

    void HandleInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal"); // 좌우 이동 입력
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true; // 점프 키가 눌렸음을 저장
        }
    }

    void Move()
    {
        if (moveInput != 0) // 이동 중일 때
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
            anim.SetBool("isRunning", true);

            // 손전등 사용 중일 때는 항상 왼쪽을 바라보게 함
            if (isUsingFlashlight)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else // 손전등을 사용하지 않을 때만 방향 변경 가능
            {
                transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
            }
        }
        else // 이동 키를 떼면 즉시 정지
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            anim.SetBool("isRunning", false);
        }
    }

    void Jump()
    {
        if (jumpPressed && isGround) // 점프 키가 눌린 상태일 때만 실행
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // 기존 x 속도 유지
            isGround = false; // 점프 상태로 변경
            anim.SetBool("isJumping", true);
        }
        jumpPressed = false; // 점프 키 초기화
    }

    void ModifyGravity()
    {
        if (rb.linearVelocity.y < 0)
        {
            rb.gravityScale = gravityScale * fallMultiplier;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.gravityScale = gravityScale * lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }

    void ItemUsage()
    {
        // 아이템이 있고 도어 앞에서 Z 키를 누르면 게임 클리어 처리
        if (Input.GetKeyDown(KeyCode.Z) && hasItem && isNearDoor)
        {
            Debug.Log("Game Clear!");
            hasItem = false;
            gameManager.SetGameClear();
        }

    }

    void Flashlight()
    {
        // 버튼을 누르고 있고 배터리가 남아있을 때 켜기
        if (flashlightTimer > 0  && Input.GetKey(KeyCode.C) && !isUsingFlashlight)
        {
            isUsingFlashlight = true;
            Debug.Log("Flashlight turned ON");

            anim.SetBool("isUsingFlashlight", true);
            anim.SetBool("isRunning", false);   // 손전등을 켰다면 달리기 애니메이션 중지
            transform.localScale = new Vector3(-1, 1, 1); // 손전등 사용 시 무조건 왼쪽을 바라보게 설정
           
         
            // 손전등 불빛 활성화
            if (flashlightLight != null)
            {
                flashlightLight.SetActive(true); // 손전등 켰을 때 불빛 활성화
            }

        }

        // 버튼을 뗐거나 배터리가 0일 때 꺼짐
        else if (Input.GetKeyUp(KeyCode.C) || flashlightTimer <= 0) 
        {
            Debug.Log("Flashlight turned OFF");

            // 손전등 더이상 사용되지 않도록 설정 
            isUsingFlashlight = false;
            anim.SetBool("isUsingFlashlight", false);

            // 손전등 불빛 비활성화
            if (flashlightLight != null)
            {
                flashlightLight.SetActive(false); // 손전등 껐을 때 불빛 비활성화
            }


        }
    }
     
    // 손전등 충전 함수
    void RechargeFlashlight()
    {
        if (!isUsingFlashlight && flashlightTimer < maxFlashlightTime)
        {
            flashlightTimer += flashlightRechargeRate * Time.deltaTime;
            flashlightTimer = Mathf.Min(flashlightTimer, maxFlashlightTime); 
            // 최대값 초과 방지
        }
    }

    // UI 업데이트 함수 
    void SetFlashlightUI()
    {
        if (flashlightBar != null)
        {
            flashlightBar.value = flashlightTimer;
        }
        // 배터리가 부족하면 슬라이더 색깔 변경
        if (flashlightTimer <= 0)
        {
            flashlightBar.fillRect.GetComponentInChildren<Image>().color = Color.red; 
            // 빨간색으로 변경
        }
        else
        {
            flashlightBar.fillRect.GetComponentInChildren<Image>().color = Color.green; 
            // 정상적으로 초록색
        }

        // UI 표시 조건: 충전이 완료되었을 때만 숨기기
        if (flashlightUI != null)
        {
            flashlightUI.SetActive(flashlightTimer < maxFlashlightTime);
        }
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            hasItem = true;
            Destroy(collision.gameObject);
        }

        // 도어와 충돌 시 도어 근처 상태를 true로 설정
        if (collision.CompareTag("Door"))
        {
            isNearDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
      {
        // [수정됨] 도어에서 벗어나면 상태를 false로 설정
        if (collision.CompareTag("Door"))
        {
            isNearDoor = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            anim.SetBool("isJumping", false); // 착지하면 점프 애니메이션 해제
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameManager.SetGameOver();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}