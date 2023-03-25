using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TestManagerTileMap : MonoBehaviour
{
    private Tilemap _tileMap;
    [SerializeField] private GameObject testObject;
    private void Start()
    {
        _tileMap= GetComponent<Tilemap>();
        
    }

    public GameObject Test(Vector3Int vector3)
    {
        var testObject = _tileMap!.GetInstantiatedObject(vector3);
       return testObject;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            var position = Camera.main.ScreenToWorldPoint(mousePos);
            var pos = new Vector3Int((int)position.x, (int)position.y);
           // Debug.Log($"mousepos {mousePos} ScreenToWorldPoint(mousePos) {position} pos {pos}");
            testObject = Test(pos);
           // Debug.Log(testObject!.transform.position);
        }
    }
}
