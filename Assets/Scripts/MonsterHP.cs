using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MonsterHP : MonoBehaviour
{
    public int monsterHP;
    public int maxHP;

    [Header("몬스터 HP UI")]
    public GameObject hpUI;
    public Slider hpSlider;

    [Header("HP UI 위치 오프셋(몬스터 기준)")]
    public Vector3 uiOffset = new Vector3(0, 0.25f, 0);   // 머리 위로 띄울 거리

    void Start()
    {
        monsterHP = GameManager.Instance.monsterHP;
        maxHP = monsterHP;

        // HP 슬라이더 초기 설정
        if (hpSlider != null)
        {
            hpSlider.minValue = 0;
            hpSlider.maxValue = maxHP;
            hpSlider.value = monsterHP;
        }
        // 처음 hpUI 숨기기
        if (hpUI != null)
        {
            hpUI.SetActive(false);

            hpUI.transform.localPosition = uiOffset;
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
    }

    // 데미지 받았을 때 호출
    public void TakeDamage(int Damage)
    {
        // hpUI 활성화
        if (hpUI != null && !hpUI.activeSelf)
        {
            hpUI.SetActive(true);
        }

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
