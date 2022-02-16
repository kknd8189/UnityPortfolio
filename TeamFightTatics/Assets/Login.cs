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
    public Button loginButton;
    public TextMeshProUGUI progressText;
    private void Start()
    {
        loginButton.onClick.AddListener(LoadButton);
    }
    void LoadButton()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("MainScene");
        asyncOperation.allowSceneActivation = false;

        Debug.Log($"Pro : {asyncOperation.progress}");
        Debug.Log($"LOGIN SUCCESSED {idText.text} pwd : {pwdText.text}");

        while (!asyncOperation.isDone)
        {
            progressText.text = $"Loading {asyncOperation.progress * 100} %";
            if (asyncOperation.progress >= 0.8f)
            {
                progressText.text = "다음으로 넘어가자";
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
