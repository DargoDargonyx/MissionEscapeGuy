using UnityEngine;

public class Essence : MonoBehaviour
{
    void OEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GlobalVariables.essence += 1;
            Destroy(gameObject);
        }
    }
}
