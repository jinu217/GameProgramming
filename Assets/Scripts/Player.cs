using UnityEngine;

public class Player : MonoBehaviour
{
    // 변수 선언
    public float jumpForce = 100.0f;
    public float walkForce = 2.0f;
    bool isflat = false; 

    // 컴포넌트 선언
    Rigidbody2D rigid;
    PlayerHP playerHP;
    SpriteRenderer spriteRenderer;
    ArrowGenerator arrow;
    Animator animator;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        arrow = GetComponent<ArrowGenerator>();
        animator = GetComponent<Animator>();
        playerHP = GetComponent <PlayerHP > ();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // gameclear 시 움직임 정지
        if (GameManager.Instance != null && GameManager.Instance.isGameClear)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = Vector2.zero;

            return;
        }

        Move();        
        if (Input.GetButtonDown("Jump") && isflat)
        {
            Jump();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
        //Debug.Log(isflat);
    }

    //이동 함수들
    void Move()
    {
        float key = 0;
        float x = Input.GetAxisRaw("Horizontal");
        // Debug.Log("x값 " + x);

        //Player local Scale의 연동
        key = x * 3;
        if(x != 0)
        {
            rigid.linearVelocity = new Vector2(x * walkForce, rigid.linearVelocity.y);
            // 걷기 애니메이션
            animator.SetInteger("State", 1);           
        }
        else
        {
            // 멈출 때 idel 애니메이션
            animator.SetInteger("State", 0);
        }       

        // 스프레이트 좌우 반전
        if(key != 0)
        {
            transform.localScale = new Vector3(key, 3, 3);
        }
    }
    void Jump()
    {
        isflat = false;
        animator.SetBool("isflat", isflat);
        animator.SetTrigger("is_jumping");
        
        rigid.linearVelocity = Vector2.zero; 
        rigid.AddForce(new Vector2(0, jumpForce));
    }

    void Attack()
    {
        arrow.ArrowGenerate();
    }

    public void PlayerOnHit(int damage, int dir)
    {
        // 데미지 실제 적용
        playerHP.TakeDamage(damage);

        // Player 레이어를 바꿔 충돌 방지 색 변경
        gameObject.layer = 8;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        // dir 방향에 따라 튕김 
        
        rigid.AddForce(new Vector2(dir, 1) * 2, ForceMode2D.Impulse);
        Invoke("OffDamaged", 3);
    }


    // 무적 해제 함수
    void OffDamaged()
    {
        gameObject.layer = 7;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    // 무한 점프 방지, 바닥과 닿아야 점프 가능
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Flat")
        {
            isflat = true;
            animator.SetBool("isflat", isflat);
        }
    }
}
