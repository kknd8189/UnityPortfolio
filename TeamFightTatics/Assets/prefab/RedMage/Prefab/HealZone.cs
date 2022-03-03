using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealZone : MonoBehaviour
{
    private float _count;
    private void Start()
    {
        _count = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tag))
        {
            collision.gameObject.GetComponent<Persona>().CurrentHp += 20;
        }
    }

    private void Update()
    {
        _count += Time.deltaTime;

        if(_count > 2f)
        {
            Destroy(gameObject);
        }
        
    }
}
