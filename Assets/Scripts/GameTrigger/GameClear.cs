using UnityEngine;

public class GameClear : MonoBehaviour
{
    [Header("게임 클리어 패널")]
    public GameObject clearPanel;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;

        if (collision.CompareTag("Player"))
        {
            triggered = true;

            // GameManager에 클리어 상태 기록
            GameManager.Instance.isGameClear = true;

            // 게임 멈춤
            Time.timeScale = 0f;

            // 클리어 패널 활성화
            if (clearPanel != null)
                clearPanel.SetActive(true);

            Debug.Log("게임 클리어!");
        }
    }
}