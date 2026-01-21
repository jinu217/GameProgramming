using UnityEngine;

public class Player : MonoBehaviour
{
    // 변수 선언
    public float jumpForce = 100.0f;
    public float walkForce = 2.0f;    
    public float attackSpeed = 0.5f;
    float time;
    bool isflat = false;

    // 컴포넌트 선언
    Rigidbody2D rigid;
    PlayerHP playerHP;
    SpriteRenderer spriteRenderer;
    ArrowGenerator arrow;
    Animator animator;

    enum EnumStates
    {
        Idle = 0,
        Walk = 1
    }


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

        Move();
        AttackSpeed();
        if (Input.GetButtonDown("Jump") && isflat)
        {
            Jump();
        }
    }

    //이동 함수들
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rigid.linearVelocity = new Vector2(x * walkForce, rigid.linearVelocity.y);

        if (x != 0)
        {
            animator.SetInteger("State", (int)EnumStates.Walk);
            spriteRenderer.flipX = (x < 0);
        }
        else
        {
            animator.SetInteger("State", 0);
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
        if (collision.gameObject.tag == "Flat" & collision.contacts[0].normal.y > 0.9f)
        {
            isflat = true;
            animator.SetBool("isflat", isflat);
        }
        if(collision.gameObject.tag == "Box")
        {
            isflat = true;
            animator.SetBool("isflat", isflat);
        }
    }

    void AttackSpeed()
    {
        // 공격 속도 재장전
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Q) && time <= 0)
        {
            Attack();
            time = attackSpeed;
        }
    }


    public void Die()
    {
        Destroy(gameObject);
    }
}
