using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public RectTransform loadingPanel; // ��ü �г� (������ ���)
    public Slider progressbar;
    public Text loadtext;

    private void Start()
    {
        if (loadingPanel == null)
        {
            loadingPanel = GetComponent<RectTransform>();
        }
        StartCoroutine(ShowLoading());
    }


    IEnumerator ShowLoading()
    {
        // ó���� ������ �Ʒ��� ������� (��������)
        Vector2 startPos = new Vector2(0, Screen.height);
        Vector2 endPos = Vector2.zero;
        loadingPanel.anchoredPosition = startPos;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f; // �ӵ� ����
            loadingPanel.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        yield return StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync("UserInfoScene");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float targetProgress = Mathf.Clamp01(operation.progress / 0.9f);
            progressbar.value = Mathf.MoveTowards(progressbar.value, targetProgress, Time.deltaTime);
            loadtext.text = Mathf.RoundToInt(progressbar.value * 100f) + "%";

            // �ε� �Ϸ� ��
            if (progressbar.value >= 1f && operation.progress >= 0.9f)
            {
                yield return StartCoroutine(HideLoading(operation));
            }

            yield return null;
        }
    }

    IEnumerator HideLoading(AsyncOperation operation)
    {
        // �Ʒ����� ���� ����ø��� (�������)
        Vector2 startPos = Vector2.zero;
        Vector2 endPos = new Vector2(0, Screen.height);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            loadingPanel.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        // �� ��ȯ
        operation.allowSceneActivation = true;
    }
}
