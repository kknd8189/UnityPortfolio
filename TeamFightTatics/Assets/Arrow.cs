using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public float Speed = 10f;
    private int _power;

    private GameObject Target;
    public new Rigidbody rigidbody;

    private void Update()
    {
        transform.position += transform.up * Speed * Time.deltaTime;
        Vector3 dir = (Target.transform.position - transform.position + Target.transform.up * 3).normalized;
        transform.up = Vector3.Lerp(transform.up, dir, 0.25f);
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
