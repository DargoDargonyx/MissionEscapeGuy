using UnityEngine;

public class Scrap : MonoBehaviour
{
    void OEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GlobalVariables.scrap += 1;
            Destroy(gameObject);
        }
    }
}
