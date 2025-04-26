using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour // ���� ���������� �Ѱ��ϴ� �Ŵ��� Ŭ����
{
    [Header("���� �ý���")]
    [SerializeField] private Player playerScript; // �÷��̾� ��ũ��Ʈ
    [SerializeField] private int score; // �� ����
    [SerializeField] private int stage; // ���� ��������

    public static GameManager Instance; // �̱��� ����

    [Header("ī��Ʈ�ٿ� UI")]
    [SerializeField] private TMP_Text countdownTMP; // TextMeshPro �ؽ�Ʈ
    [SerializeField] private GameObject gameElementsToEnable; // ���� ���� �� Ȱ��ȭ�� ������Ʈ ����
    [SerializeField] private TMP_Text scoreTMP; // ScoreText ����
    [SerializeField] private GameObject resultPanel; // ���â Panel
    [SerializeField] private TMP_Text gameOverTMP; // ���� ���� ������ �ؽ�Ʈ
    [SerializeField] private TMP_Text finalScoreTMP; // ���â ���� ���� Text

    private bool gameStarted = false;
    private bool gameOver = false;
    public bool TimerStarted { get; private set; } = false;

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
        Debug.Log($"���� ����: {score}");
    }

    private void UpdateScoreUI()
    {
        if (scoreTMP != null)
            scoreTMP.text = "Score: " + score.ToString();
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        playerScript = FindAnyObjectByType<Player>();
    }

    void Start()
    {
        StartCoroutine(CountdownRoutine());
    }

    IEnumerator CountdownRoutine()
    {
        // ���� ���� �� UI/������Ʈ �����
        gameElementsToEnable.SetActive(false);
        countdownTMP.gameObject.SetActive(true);

        int count = 3;
        while (count > 0)
        {
            countdownTMP.text = count.ToString();
            yield return new WaitForSeconds(1f);
            count--;
        }

        countdownTMP.text = "Start!";
        yield return new WaitForSeconds(1f);

        countdownTMP.gameObject.SetActive(false);
        gameElementsToEnable.SetActive(true);

        StartGame();
    }

    private void StartGame()
    {
        if (gameStarted) return;

        Debug.Log("GameStart");
        StartSetting();
        playerScript.SpawnPlayer(); // ī��Ʈ�ٿ� �� �ڵ� ����
        gameStarted = true;
        TimerStarted = true;
    }

    void Update()
    {
        // ESC ������ ���� ���� �׽�Ʈ��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("GameOver");
            playerScript.DestoryPlayer();
            gameStarted = false;
            StartCoroutine(CountdownRoutine()); // �ٽ� ī��Ʈ�ٿ� ����
        }
    }

    public int Stage => stage;
    public int Score => score;

    public void plusStage() // �������� ���
    {
        stage++;
    }

    public void StartSetting() // ����, �������� �ʱ�ȭ
    {
        score = 0;
        stage = 1;
        UpdateScoreUI();
    }

    // GameManager.cs �ȿ� �߰�
    public bool IsGameOver()
    {
        return gameOver;
    }

    public void GameOver()
    {
        if (gameOver) return; // �̹� ���ӿ��� ���¸� ����

        Debug.Log("Game Over by Timer");
        gameOver = true;

        playerScript.DestoryPlayer();
        gameElementsToEnable.SetActive(false);

        if (gameOverTMP != null)
        {
            gameOverTMP.gameObject.SetActive(true);
            gameOverTMP.text = "Game Over!";
        }


        resultPanel.SetActive(true);
        finalScoreTMP.text = "Score: " + score.ToString();

        Time.timeScale = 0; // ���� ����
    }

}