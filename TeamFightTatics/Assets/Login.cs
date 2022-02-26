using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public TMP_InputField idText;
    public TMP_InputField pwdText;

    public Button LoginButton;
    public Button SceneChangeButton;

    public TextMeshProUGUI progressText;

    public PlayerSelect PlayerSelect;

    private void Start()
    {
        LoginButton.onClick.AddListener(LoginEvent);
        SceneChangeButton.onClick.AddListener(LoadEvent);
    }

    private void LoginEvent()
    {
        StartCoroutine(LoginSection());
    }

    private void LoadEvent()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("MainScene");
        asyncOperation.allowSceneActivation = false;

        Debug.Log($"�ε���~~ : {asyncOperation.progress}");

        while (!asyncOperation.isDone)
        {
            progressText.text = $"Loading {asyncOperation.progress * 100} %";
            if (asyncOperation.progress >= 0.8f)
            {
                progressText.text = "�����Ϸ��� ȭ���� Ŭ���ϼ���";

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

        Debug.Log($"LOGIN SUCCESSED {idText.text} pwd : {pwdText.text}");

        idText.gameObject.SetActive(false);
        pwdText.gameObject.SetActive(false);
        LoginButton.gameObject.SetActive(false);

        progressText.text = $"{idText.text}�� ȯ���մϴ�";

        yield return new WaitForSeconds(2f);

        PlayerSelect.Init();
        progressText.text = $"ĳ���͸� �������ּ���";

        yield return null;

    }
}
