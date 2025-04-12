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
        
    }
}
