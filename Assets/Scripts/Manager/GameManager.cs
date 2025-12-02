using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("플레이어 HP")]
    public int playerHP = 100;

    [Header("플레이어 공격수치")]
    public int playerDamage = 10;
    public int playerAttackSpeed = 10;

    [Header("몬스터 HP")]
    public int monsterHP = 100;

    [Header("몬스터 공격수치")]
    public int monsterDamage = 5;
    public int monsterAttackSpeed = 5;

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
