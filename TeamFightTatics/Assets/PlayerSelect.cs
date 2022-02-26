using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelect : MonoBehaviour
{
    public List<GameObject> PlayerCharacters = new List<GameObject>();
    private GameObject playercharacter;

    public int PlayerNumber = 0;

    public Button NextButton;
    public Button PreviousButton;
    public Button StartButton;

    private void Awake()
    {
        playercharacter = PlayerCharacters[PlayerNumber];
    }
    public void Init()
    {
        playercharacter.SetActive(true);
        NextButton.gameObject.SetActive(true);
        PreviousButton.gameObject.SetActive(true);
        StartButton.gameObject.SetActive(true);
    }
    public void NextPrefab()
    {
        playercharacter.SetActive(false);

        PlayerNumber++;

        if (PlayerNumber > 3)
        {
            PlayerNumber = 0;
        }

        playercharacter = PlayerCharacters[PlayerNumber];
        playercharacter.SetActive(true);
    }
    public void PreviousPefab()
    {
        playercharacter.SetActive(false);

        PlayerNumber--;

        if (PlayerNumber < 0)
        {
            PlayerNumber = 3;
        }

        playercharacter = PlayerCharacters[PlayerNumber];
        playercharacter.SetActive(true);
    }

    public void CharacterSelect()
    {
        playercharacter.AddComponent<PlayerMove>();
        playercharacter.AddComponent<Player>().PlayerNumber = PlayerNumber;
        playercharacter.AddComponent<Synergy>();
        
        DontDestroyOnLoad(playercharacter);
    }
}
