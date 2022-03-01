using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public TMP_InputField idText;

    public Button LoginButton;
    public Button LogOutButton;
    public Button SceneChangeButton;

    public TextMeshProUGUI progressText;

    public PlayerSelect PlayerSelect;
    private void Start()
    {
        LoginButton.onClick.AddListener(LoginEvent);
        SceneChangeButton.onClick.AddListener(LoadEvent);
        LogOutButton.onClick.AddListener(LogOutEvent);
    }

    private void LoginEvent()
    {
        StartCoroutine(LoginSection());
    }

    private void LoadEvent()
    {
        StartCoroutine(LoadScene());
    }
    private void LogOutEvent()
    {
        Application.Quit();
    }
    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("MainScene");
        asyncOperation.allowSceneActivation = false;

        Debug.Log($"로딩중~~ : {asyncOperation.progress}");

        while (!asyncOperation.isDone)
        {
            progressText.text = $"Loading {asyncOperation.progress * 100} %";
            if (asyncOperation.progress >= 0.8f)
            {
                progressText.text = "시작하려면 화면을 클릭하세요";

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    PlayerSelect.CharacterSelect();
                    asyncOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

    IEnumerator LoginSection()
    {
        yield return null;

        idText.gameObject.SetActive(false);
        LoginButton.gameObject.SetActive(false);
        LogOutButton.gameObject.SetActive(false);

        progressText.text = $"{idText.text}님 환영합니다";

        yield return new WaitForSeconds(2f);

        PlayerSelect.Init();
        progressText.text = $"캐릭터를 선택해주세요";

        yield return null;

    }
}
