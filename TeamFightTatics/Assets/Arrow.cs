using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public float Speed = 15f;
    private int _power;

    private GameObject Target;
    public Rigidbody ArrowRigidbody;
    private void Awake()
    {
        ArrowRigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        ArrowRigidbody.velocity = transform.forward * Speed;
        Quaternion ballTargetRotation = Quaternion.LookRotation(Target.transform.position + new Vector3(0, 0.8f) - transform.position);
        ArrowRigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, ballTargetRotation, 10));
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
