using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private float _speed;
    private int _power;
    private Vector3 _destination;
    private void OnEnable()
    {
        _speed = 200f;
    }
    private void Update()
    {
        Vector3 dir = (_destination - transform.position).normalized;
        transform.position += dir * Time.deltaTime * _speed;
        transform.forward = dir;
    }
    public void setDest(Vector3 dest)
    {
        _destination = dest;
    }
    public void setPower(int value)
    {
        _power = value;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<Entity>() != null)
        {
            collision.collider.GetComponent<Entity>().Damaged(_power);
            PoolManager.Instance.PushArrowQueue(gameObject);
        }
    }
}
