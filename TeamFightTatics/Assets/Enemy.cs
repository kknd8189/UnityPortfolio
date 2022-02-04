using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : Entity
{
    public GameObject HP;
    private TextMeshProUGUI HPtm;

    private void Start()
    {
        maxHP = 100;
        currentHP = maxHP;
        HPtm = HP.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        HPtm.text = currentHP.ToString();
    }
}
