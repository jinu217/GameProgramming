using UnityEngine;

public class Item : MonoBehaviour
{
    public float value;

    public void Use()
    {

        Destroy(gameObject);
    }
}
