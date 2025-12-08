using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("플레이어 HP")]
    public int playerHP = 100;

    [Header("플레이어 공격수치")]
    public int playerDamage = 10;
    public int playerAttackSpeed = 10;



    [Header("게임 상태")]
    public bool isGameClear = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
