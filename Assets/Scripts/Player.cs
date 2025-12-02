using UnityEngine;

public class Player : MonoBehaviour
{
    // 변수 선언
    public float jumpForce = 100.0f;
    public float walkForce = 2.0f;
    bool isflat = false; 

    // 컴포넌트 선언
    Rigidbody2D rigid;
    ArrowGenerator arrow;
    Animator animator;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        arrow = GetComponent<ArrowGenerator>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
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
        animator.SetTrigger("is_jumping");
        animator.SetBool("isflat", isflat);
        rigid.linearVelocity = Vector2.zero; 
        rigid.AddForce(new Vector2(0, jumpForce));
    }

    void Attack()
    {
        arrow.ArrowGenerate();
    }


    // 무한 점프 방지, 바닥과 닿아야 점프 가능
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Flap")
        {
            isflat = true;
            animator.SetBool("isflat", isflat);
        }
    }
}
