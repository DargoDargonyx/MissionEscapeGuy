using Unity.Netcode;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class ShieldManager : MonoBehaviour
{
    private Image image;
    private SpriteResolver spriteResolver;
    private NetworkObject networkObject;
    private TheGuy player;
    void Start()
    {
        player = player == null ? networkObject.GetComponent<TheGuy>() : player;

        image = image == null ? GetComponent<Image>() : image;
        spriteResolver = spriteResolver == null ? GetComponent<SpriteResolver>() : spriteResolver;
    }

    // Update is called once per frame
    void Update()
    {
        checkShield();
    }

    void checkShield()
    {
        image.sprite = spriteResolver.spriteLibrary.GetSprite("GUI", "ShieldBar_" + player.getShield());
    }
}
