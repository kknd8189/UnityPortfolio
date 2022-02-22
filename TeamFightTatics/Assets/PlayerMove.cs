using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { Run, Idle, }

public class PlayerMove : MonoBehaviour
{
    private Vector3 destination;
    [SerializeField]
    private PlayerState _playerState;
    public PlayerState PlayerState
    {
        get { return _playerState; }
        set
        { 
            _playerState = value;
        }
    }

    private Animator anim;
    private float _speed = 70f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        _playerState = PlayerState.Idle;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 300.0f, 1 << 6))
            {
                destination = hit.point;
               _playerState = PlayerState.Run;
            }
        }

        switch (_playerState)
        {
            case PlayerState.Run:
                runUpdate();
                break;
            case PlayerState.Idle:
                idleUpdate();
                break;
        }
    }
    private void runUpdate()
    {
        if (Vector3.Distance(destination, transform.position) <= 0.24f)
        {
            _playerState = PlayerState.Idle;
            return;
        }

        Vector3 dir = destination - transform.position;
        transform.position += dir.normalized * Time.deltaTime * _speed;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir.normalized), 20 * Time.deltaTime);

        anim.SetFloat("speed", _speed);

    }

    private void idleUpdate()
    {
        anim.SetFloat("speed", 0);
    }
}
