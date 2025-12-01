using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    float arrowSpeed = 10.0f;
    Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        //화살 발사 방향
        if (transform.localScale.x < 0)
        {
            rigid.AddForce(-transform.right * arrowSpeed, ForceMode2D.Impulse);
        }
        else
        {
            rigid.AddForce(transform.right * arrowSpeed, ForceMode2D.Impulse);
        }
    }

    void Update()
    {
        Destroy(gameObject, 3f);
    }
}
