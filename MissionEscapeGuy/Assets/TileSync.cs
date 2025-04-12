using Unity.Netcode;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSync : NetworkBehaviour
{
    public NetworkVariable<TileBase[][]> tilegrid;
}
