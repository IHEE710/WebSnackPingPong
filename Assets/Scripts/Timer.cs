using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour // �ð����� Ŭ�����̴�
{
    [SerializeField] private float elapsedTime; // ����ð��� ������ �ð��̴�
    [SerializeField] private float LimitteTime; // ���ѽð��̴�

    private void Awake()
    {
        elapsedTime = 0f;
        LimitteTime = 300f;
    }
}
