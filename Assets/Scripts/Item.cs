using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Item : MonoBehaviour
{
    public int value;
    public void Use()
    {
            GameManager.Instance.playerHP += value;
            Destroy(gameObject);
    }
}
