using Unity.Netcode;
using UnityEngine;

public class ButtonMapping : MonoBehaviour
{
    public void PurpleButton() => ClientManager.selCol = 1;
    public void BlueButton() => ClientManager.selCol = 2;
    public void RedButton() => ClientManager.selCol = 3;
    public void GreenButton() => ClientManager.selCol = 4;
    public void OrangeButton() => ClientManager.selCol = 5;
    public void ReadyUp() => NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject().GetComponent<TheGuy>().isReady.Value = true;
}