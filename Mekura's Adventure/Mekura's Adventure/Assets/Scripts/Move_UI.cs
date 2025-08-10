using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_UI : MonoBehaviour
{
    // Start is called before the first frame update
    //private void OnMouseDown()
    //{
    //    //Vector3 mousePosition = Input.mousePosition;
    //    //Camera mainCamera = Camera.main;
    //    //if (mainCamera != null)
    //    //{
    //    //    Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
    //    //    transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
    //    //    Debug.Log("ASD");
    //    //}

    //}
    //void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Debug.Log("asdfasdf");
    //    }
    //}
    void OnMouseDrag()
    {
        Vector3 mousePosition =  Input.mousePosition;
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
            Debug.Log("ASD");
        }
    }
}
