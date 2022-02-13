using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyManager : MonoBehaviour
{
    public static SynergyManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
