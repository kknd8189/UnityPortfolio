using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpButtenEvent : MonoBehaviour
{
    public Player Player;
    public Button ExpButton;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        ExpButton.onClick.AddListener(Player.BuyExp);
    }

}
