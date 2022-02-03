using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 screenSpace;
    private Vector3 offset;
    private Vector3 FinalPosition;

    private void OnMouseDown()
    {
        screenSpace = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
    }
    private void OnMouseDrag()
    {
        var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
        var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;

        transform.position = curPosition;
        FinalPosition = curPosition;
    }

    private void OnMouseUp()
    {
        transform.position = FinalPosition;
    }
}
