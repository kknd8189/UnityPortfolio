using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private bool isMove;
    private Vector3 destination;
    public float Speed = 10f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100f, Color.red, 1.0f);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 300.0f))
            {
                setDestination(hit.point);
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

            Vector3 dir = destination - transform.position;
            transform.position += dir.normalized * Time.deltaTime * Speed;
        }
    }

    private void setDestination(Vector3 dest)
    {
        destination = dest;
        isMove = true;
    }
}
