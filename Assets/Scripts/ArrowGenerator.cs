using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    // 화살 생성
    public GameObject arrowPrefab;
    public GameObject Player;

     void Start()
    {
        // 위치 플레이어 위치
        Player = GameObject.Find("Player");
    }
    public void ArrowGenerate()
    {
        Vector3 posit_temp = new Vector3(0, -0.2f, 0); 
        GameObject instant = Instantiate(arrowPrefab, Player.transform.position + posit_temp, Player.transform.rotation);

        // 왼쪽일 때 생성 시 Scale x축을 음수로 변경
        if(Player.transform.localScale.x < 0)
        {
            Vector3 scale = instant.transform.localScale;
            scale.x = scale.x * -1;
            instant.transform.localScale = scale;
            Debug.Log(instant.transform.localScale.x);
        }
    }
}
