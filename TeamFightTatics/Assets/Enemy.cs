using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : Entity
{
    public TextMeshProUGUI HPtm;

    private void Start()
    {
        maxHP = 100;
        currentHP = maxHP;
    }

    private void Update()
    {
        HPtm.text = currentHP.ToString();
    }
}
