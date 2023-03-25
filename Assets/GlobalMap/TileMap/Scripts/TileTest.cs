using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Tile", menuName = "ScriptableObjects/Tile", order = 2)]
public class TileTest : Tile 
{
    [SerializeField] ITilemap tilemap;
    [SerializeField] TileAnimationData _tilemap;
    
}
