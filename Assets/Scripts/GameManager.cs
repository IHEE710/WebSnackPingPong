using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour // ���� ���������� �Ѱ��ϴ� �Ŵ��� Ŭ������
{
    [SerializeField] private Player playerScript; // �÷��̾� ��ũ��Ʈ
    [SerializeField] private int score; // �� ����
    [SerializeField] private int stage; // ���� ��������

    void Awake()
    {
        playerScript = FindAnyObjectByType<Player>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // �����̽��ٸ� ������ ������ ���۵ȴ�
        {
            Debug.Log("GameStart");
            playerScript.SpawnPlayer();
            StartSetting();
        }
        else if (Input.GetKeyDown(KeyCode.Escape)) // ESC�� ������ ���ӿ������·� ����� �ٽ� �����̽��ٸ� ������ ���۵ȴ�
        {
            Debug.Log("GameOver");
            playerScript.DestoryPlayer();
        }
    }

    public int Stage
    {
        get { return stage; } 
    }    

    public void plusStage() // �������� ���
    {
        stage++;
    }

    public void StartSetting() // ���������� ���� �ʱ�ȭ
    {
        score = 0;
        stage = 1;
    }

}
