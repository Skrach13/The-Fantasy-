using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/Tile", order = 2)]
public class TileGlobalMap : Tile 
{
    [SerializeField] ITilemap tilemap;
    [SerializeField] TileAnimationData _tilemap;
    
}
