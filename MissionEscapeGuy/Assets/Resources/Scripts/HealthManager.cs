using Unity.Netcode;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private Image image;
    private SpriteResolver spriteResolver;
    private NetworkObject networkObject;
    private TheGuy player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = player == null ? GetComponentInParent<TheGuy>() : player;

        image = image == null ? GetComponent<Image>() : image;
        spriteResolver = spriteResolver == null ? GetComponent<SpriteResolver>() : spriteResolver;
    }

    // Update is called once per frame
    void Update()
    {
        checkHealth();
    }

    void checkHealth()
    {
        image.sprite = spriteResolver.spriteLibrary.GetSprite("GUI", "HealthBar_" + player.getHealth());
    }
}
