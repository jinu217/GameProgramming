using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHP : MonoBehaviour
{
    Player player;
    public int playerHP;
    public int maxHP;

    [Header("플레이어 HP UI")]
    public Slider hpSlider;
    public TextMeshProUGUI hpText;
    void Start()
    {
        player = GetComponent<Player>();
        playerHP = GameManager.Instance.playerHP;
        maxHP = playerHP;

        // HP 슬라이더 초기 세팅
        if (hpSlider != null)
        {
            hpSlider.minValue = 0;
            hpSlider.maxValue = maxHP;
            hpSlider.value = playerHP;
        }
        UpdateUI();
    }

    void Update()
    {
        playerHP = GameManager.Instance.playerHP;
        MaxHP();
        UpdateUI();
    }

    void UpdateUI()
    {
        if (hpSlider != null)
        {
            hpSlider.value = playerHP;
        }

        if (hpText != null)
        {
            hpText.text = playerHP + "/" + maxHP;
        }
    }


    // 데미지 받았을 때 로직
    public void TakeDamage(int Damage)
    {
        if (GameManager.Instance.playerHP < 0)
        {
            GameManager.Instance.playerHP = 0;
        }
        if (GameManager.Instance.playerHP == 0)
        {
            player.Die();
        }        
        if (GameManager.Instance.playerHP > 0)
        {
            GameManager.Instance.playerHP -= Damage;
        }
    }

    // Player의 HP가 최대를 넘을 경우, maxHP로 동기화
    void MaxHP()
    {
        if (GameManager.Instance.playerHP > maxHP)
        {
            GameManager.Instance.playerHP = maxHP;
        }
        if (GameManager.Instance.playerHP <= 0)
        {
            player.Die();
        }
    }
}
