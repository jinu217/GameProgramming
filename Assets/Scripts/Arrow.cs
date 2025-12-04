using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    float arrowSpeed = 10.0f;
    Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); 

    }

    void Update()
    {
        ArrowMove();

        //3초후 자동 삭제
        Destroy(gameObject, 3f);
    }

    //화살 발사 방향
    void ArrowMove()
    {
        if (transform.localScale.x < 0)
        {
            rigid.linearVelocity = new Vector2(-arrowSpeed, 0);   
        }
        else
        {
            rigid.linearVelocity = new Vector2(arrowSpeed, 0);
        }
    }


    //화살이 몬스터와 접촉했을 때 데미지 전달
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 접촉한 Layer가 Player인지 확인
        if (collision.gameObject.tag == "Monster")
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();
            // 피격 방향 monster 위치 - monster 위치 뺀 값이 0보다 크면 1,  0보다 작으면 -1  
            int dirc = (collision.transform.position.x - transform.position.x > 0 ? 1 : -1);
            
            // player의 공격력과 화살 방향 전달
            monster.MonsterOnHit(GameManager.Instance.playerDamage, dirc);
            Destroy(gameObject);
        }
    }
}
