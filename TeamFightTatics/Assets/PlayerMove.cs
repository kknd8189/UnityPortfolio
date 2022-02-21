using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private bool isMove;
    private Vector3 destination;
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 300.0f, 1<<6))
            {
                isMove = true;
                destination = hit.point;
                destination.y = 5;
            }
        }
        move();
    }
    private void move()
    {
        if(isMove)
        {
            if(Vector3.Distance(destination, transform.position) <= 0.1f)
            {
                isMove = false;
                return;
            }
            Vector3 dir = (destination - transform.position).normalized;
            transform.position += dir * Time.deltaTime * gameObject.GetComponent<Player>().Speed;
            transform.forward = dir;
        }
    }
}
