using UnityEngine;

public class ArrowGenerator : MonoBehaviour
{
    // 화살 생성
    public GameObject arrowPrefab;
    public void ArrowGenerate()
    {
        GameObject instant = Instantiate(arrowPrefab);
    }
}
