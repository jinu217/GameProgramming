using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterHP : MonoBehaviour
{
    public int monsterHP;
    public int maxHP;

    [Header("몬스터 HP UI")]
    public Slider hpSlider;
    public TextMeshProUGUI hpText;
    void Start()
    {
        monsterHP = GameManager.Instance.monsterHP;
        maxHP = monsterHP;

        // HP 슬라이더 초기 세팅
        if (hpSlider != null)
        {
            hpSlider.minValue = 0;
            hpSlider.maxValue = maxHP;
            hpSlider.value = monsterHP;
        }

        UpdateUI();
    }

    void Update()
    {
        monsterHP = GameManager.Instance.monsterHP;

        UpdateUI();
    }

    void UpdateUI()
    {
        if (hpSlider != null)
        {
            hpSlider.value = monsterHP;
        }

        if (hpText != null)
        {
            hpText.text = monsterHP + "/" + maxHP;
        }
    }


    // 데미지 받았을 때 로직
    public void TakeDamage(int Damage)
    {
        if (GameManager.Instance.monsterHP < 0)
        {
            GameManager.Instance.monsterHP = 0;
        }
        if (GameManager.Instance.monsterHP == 0)
        {
            return;
        }
        if (GameManager.Instance.monsterHP > 0)
        {
            GameManager.Instance.monsterHP -= Damage;
        }
    }
}
