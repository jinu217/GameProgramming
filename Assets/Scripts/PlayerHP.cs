using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int playerHP;

    void Start()
    {
        playerHP = GameManager.Instance.playerHP;
        Debug.Log("플레이어 시작 HP: " + playerHP);
    }

    void Update()
    {
        playerHP = GameManager.Instance.playerHP;
    }

}
