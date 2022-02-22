using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    
    public float Speed = 100f;
    public float TurnSpeed = 0.25f;

    private int _power;

    private GameObject Target;
    public new Rigidbody rigidbody;

    private void FixedUpdate()
    {
        transform.position += transform.up;
        Vector3 dir = (Target.transform.position - transform.position).normalized;
        transform.up = Vector3.Lerp(transform.up, dir, TurnSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Entity>() != null)
        {
            collision.collider.GetComponent<Entity>().Damaged(_power);
            PoolManager.Instance.PushArrowQueue(gameObject);
        }
    }

    public void SetArrowState(GameObject target, int power)
    {
        _power = power;
        Target = target;
    }
}
