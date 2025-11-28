using UnityEngine;

public class Player : MonoBehaviour
{
    // 변수 선언
    public float jumpForce = 300.0f;
    public float walkForce = 3.0f;
    bool isflap = false;

    // 컴포넌트 선언
    Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Move();        
        if (Input.GetButtonDown("Jump") && !isflap)
        {
            Jump();
        }
    }

    //이동 함수들
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rigid.linearVelocity = new Vector2(x * walkForce, rigid.linearVelocity.y);
    }

    void Jump()
    {
        isflap = true;
        rigid.linearVelocity = Vector2.zero; 
        rigid.AddForce(new Vector2(0, jumpForce));
    }

    // 무한 점프 방지, 바닥과 닿아야 점프 가능
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Flap")
        {
            isflap = false;
        }
    }
}
