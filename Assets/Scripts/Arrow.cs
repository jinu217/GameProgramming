using UnityEngine;

public class Arrow : MonoBehaviour
{
    float arrowSpeed = 10.0f;
    Rigidbody2D rigid;
    void Start()
    {
        //È­»ì ¹ß»ç Èû
        rigid = GetComponent<Rigidbody2D>();
        rigid.AddForce(transform.right * arrowSpeed, ForceMode2D.Impulse);
    }
}
