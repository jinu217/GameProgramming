using System.Collections;
using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.InputSystem;

public class Monster : MonoBehaviour
{
    public int nextmove;
    public float speed;
    public int monsterDamage;

    MonsterHP monsterHP;
    Animator animator;
    Rigidbody2D rigid;
    PlayerHP playerHP;
    SpriteRenderer spriteRenderer;    
    
    public GameObject [] item;

  void Start()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        monsterHP = GetComponent<MonsterHP>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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
            animator.SetInteger("State", 1);
            rigid.linearVelocity = new Vector2(nextmove * speed, rigid.linearVelocity.y);

            // 몬스터 약간 앞쪽 Flat 감지 null 값일 때 방향 바꿈
            Vector2 frontVec = new Vector2(rigid.position.x + (nextmove * 0.2f), rigid.position.y);            
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Flat"));
            RaycastHit2D BoxrayHit_right = Physics2D.Raycast(frontVec, Vector3.right, 0.5f, LayerMask.GetMask("Box"));
            RaycastHit2D BoxrayHit_left = Physics2D.Raycast(frontVec, Vector3.left, 0.5f, LayerMask.GetMask("Box"));

            

            if (rayHit.collider == null)
            {
                Changemove();
            }
            if (BoxrayHit_right.collider != null && BoxrayHit_right.collider.CompareTag("Box"))
            {
                Changemove();
            }
            if (BoxrayHit_left.collider != null && BoxrayHit_left.collider.CompareTag("Box"))
            {
                Changemove();
            }
            // 애니메이션 설정 및 좌우 반전
            if (nextmove > 0)
                spriteRenderer.flipX = true; // 오른쪽
            else
                spriteRenderer.flipX = false;  // 왼쪽
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

    // 방향을 바꾸고, 다시 움직임 로직 실행
    void Changemove()
    {
        nextmove = nextmove * -1;
        //Invoke 카운트 초기화
        CancelInvoke();
        Invoke("Think", 5);
    }

    public void MonsterOnHit(int damage, int dir)
    {
        //튕기는 방향
        rigid.AddForce(new Vector2(dir, 0.1f) * 1.5f, ForceMode2D.Impulse);
        // 데미지 실제 적용
        monsterHP.TakeDamage(damage);
        int currentState = animator.GetInteger("State");
        StartCoroutine(OnHitAnim());
    }

    IEnumerator OnHitAnim()
    {
        CancelInvoke();
        animator.SetTrigger("ishit");
        yield return new WaitForSeconds(1f);
        Think();
    }



    void OnCollisionStay2D(Collision2D collision)
    {
        // 접촉한 Layer가 Player인지 확인
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            // 피격 방향 Player 위치 - monster 위치 뺀 값이 0보다 크면 1,  0보다 작으면 -1  
            int dirc = (collision.transform.position.x - transform.position.x > 0 ? 1 : -1);
            player.PlayerOnHit(monsterDamage, dirc);
        }
    }

    
    // 죽음 애니메이션 & 아이템 드랍
    public void Die()
    {        
        animator.SetBool("isdead", true);

        //아이템 드랍
        int i = Random.Range(0, item.Length);
        GameObject selectedItem = item[i];
        Instantiate(selectedItem, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
