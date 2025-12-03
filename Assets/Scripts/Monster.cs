using UnityEngine;
using UnityEngine.InputSystem;

public class Monster : MonoBehaviour
{
    public int nextmove;

    Animator animator;
    Rigidbody2D rigid;
    PlayerHP playerHP;
  void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        // 5초뒤 움직일 값 설정
        Invoke("Think", 5);
    }
    void Update()
    {
        Monstermove();
    }

    // 몬스터 이동 함수
    void Monstermove()
    {
        if (nextmove != 0)
        {
            
            rigid.linearVelocity = new Vector2(nextmove, rigid.linearVelocity.y);

            // 몬스터 약간 앞쪽 Flat 감지 null 값일 때 방향 바꿈
            Vector2 frontVec = new Vector2(rigid.position.x + nextmove, rigid.position.y);            
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Flat"));
            if (rayHit.collider == null)
            {
                nextmove = nextmove * -1;
                //Invoke 카운트 초기화
                CancelInvoke();                
                Invoke("Think", 5);
            }

            // 애니메이션 설정 및 좌우 반전
            animator.SetInteger("State", 1);
            transform.localScale = new Vector3(-nextmove, 1, 1);
        }
        else
        {
            animator.SetInteger("State", 0);
        }
    }

    // 다음 움직임 값을 설정
    void Think()
    {
        nextmove = Random.Range(-1, 2);
        Invoke("Think", 5);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // 접촉한 Layer가 Player인지 확인
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            // 피격 방향 Player 위치 - monster 위치 뺀 값이 0보다 크면 1,  0보다 작으면 -1  
            int dirc = (collision.transform.position.x - transform.position.x > 0 ? 1 : -1);
            player.OnHit(10, dirc);
        }
    }

}
