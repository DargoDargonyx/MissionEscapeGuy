using Unity.Netcode;
using UnityEngine;


public class HelloWorldPlayer : NetworkBehaviour
{
    public void Move()
    {
        SubmitPositionRequestServerRpc();
    }

    [Rpc(SendTo.Server)]
    void SubmitPositionRequestServerRpc(RpcParams rpcParams = default)
    {  
        var randomPosition = GetRandomPositionOnPlane();
        transform.position = randomPosition;
    }

    static Vector3 GetRandomPositionOnPlane()
    {
        return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
    }
}
