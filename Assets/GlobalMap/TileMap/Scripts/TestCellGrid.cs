using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCellGrid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        Debug.Log("enter mouse");
    }

    private void OnMouseUp()
    {
        GetLocalPos();
    }


    public void GetLocalPos()
    {
        Debug.Log(transform.position);
    }

}
