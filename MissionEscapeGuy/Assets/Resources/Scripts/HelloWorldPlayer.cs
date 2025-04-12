using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;


public class HelloWorldPlayer : NetworkBehaviour
{
    public void Move()
    {
        SubmitPositionRequestServerRpc();
    }

    [Rpc(SendTo.Server)]
    void SubmitPositionRequestServerRpc(RpcParams rpcParams = default)
    {
        transform.position = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject().GetComponent<Transform>().position;
    }
}
