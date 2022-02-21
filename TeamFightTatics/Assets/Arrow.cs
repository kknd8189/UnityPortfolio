using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    
    private float _speed;
    [SerializeField]
    private int _power;
    [SerializeField]
    private GameObject Target;
    private void OnEnable()
    {
        _speed = 50f;
    }
    private void Update()
    {
        Vector3 dir = (Target.transform.position - transform.position).normalized;
        transform.position += dir * Time.deltaTime * _speed;
        transform.forward = dir;
    }
    public void SetDest(GameObject target)
    {
        Target = target;
    }
    public void SetPower(int value)
    {
        _power = value;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Entity>() != null)
        {
            collision.collider.GetComponent<Entity>().Damaged(_power);
            PoolManager.Instance.PushArrowQueue(gameObject);
        }
    }
}
