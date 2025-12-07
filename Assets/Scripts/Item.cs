using UnityEngine;

public class Item : MonoBehaviour
{
    public int value;

    public void Use()
    {
        GameManager.Instance.playerHP += value;
        Destroy(gameObject);
    }
}
