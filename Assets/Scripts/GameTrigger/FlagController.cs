using UnityEngine;

public class FlagController : MonoBehaviour
{
    [Header("플레이어 Transform")]
    public Transform player;

    [Header("깃발 반응")]
    public float activateDistance = 5f;

    [Header("깃발 Animator")]
    public Animator animator;

    private bool hasActivated = false;

    void Update()
    {
        if (hasActivated) return;       
        if (player == null || animator == null) return;
        // 거리 계산
        float dist = Vector2.Distance(player.position, transform.position);

        if (dist <= activateDistance)
        {
            hasActivated = true;
            animator.SetTrigger("Activate");  // 애니메이션 실행 (Idle → Activate → LoopWave)
        }
    }
}
