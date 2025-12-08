using UnityEngine;

public class PlayerRayCast : MonoBehaviour
{

    // circle 반지름 크기
    public float radius = 1.0f;
    public float distance = 1.0f;
    public LayerMask itemLayer;


    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.E))
        {
            ScanItem();
        }
    }

    void ScanItem()
    {
        // Player 방향 바꾸면 Ray 방향도 바꿈
        Vector2 dir = transform.right;
        if(GetComponent<SpriteRenderer>().flipX == true)
        {
            dir = Vector2.left;
        }

        // 원 방향으로 Ray itemLayer만 인식
        RaycastHit2D hit = Physics2D.CircleCast(transform.position,  radius,  dir, distance, itemLayer);

        // 아이템 인식
        if (hit.collider != null)
        {
            Item item = hit.collider.GetComponent<Item>();
            if(item != null)
            {
                item.Use();                
            }
        }
    }
}
