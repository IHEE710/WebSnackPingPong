using System.Collections.Generic;
using UnityEngine;

public class ShooterQueue : MonoBehaviour // ���� �����ϴ� Ǯ�̴� (������Ʈ Ǯ�� Ȱ��)
{
    [SerializeField] private Queue<GameObject> shooter; // ���� �����ϴ� ť�̴�
    [SerializeField] private GameObject ball; // �� ������Ʈ�̴�
    [SerializeField] private int capacity = 0; // ť�� ������ִ� ���� ���� �����̴�
    [SerializeField] private int size; // ť�� ũ���̴�

    public int Capacity
    {
        get { return capacity; }
        set { capacity = value; }
    }

    public int Size
    {
        get { return size; }
        set { size = value; }
    }

    public void InitQueue(int size_v) // ť�� �ʱ�ȭ�ϰ� ���� ������ ť�� �ִ´� 
    {
        size = size_v;
        shooter = new Queue<GameObject>(size);

        if (ball == null)
        {
            Debug.LogError("ShooterQueue: ball �������� ��� �ֽ��ϴ�. �������� �ν����Ϳ� �����ϼ���.");
            return;
        }

        GameObject[] instantQueue = new GameObject[size];
        for (int count = 0; count < size; count++)
        {
            instantQueue[count] = Instantiate(ball, this.transform.position, this.transform.rotation);
        }

        foreach (GameObject obj_v in instantQueue)
        {
            PollingQueue(obj_v);
        }

        Debug.Log($"ShooterQueue: InitQueue �Ϸ� - size: {size}, capacity: {capacity}");
    }

    public void RemoveQueue() // �÷��̾ ���ӿ������� �� ť�� �ƿ� �����Ѵ�
    {
        if (shooter != null)
        {
            while (shooter.Count > 0)
            {
                GameObject obj_v = shooter.Dequeue();
                Destroy(obj_v);
            }
        }

        capacity = 0;
        size = 0;
        shooter = null;

        Debug.Log("ShooterQueue: RemoveQueue �Ϸ�");
    }

    public void PollingQueue(GameObject obj_v) // ���� ť�� �ִ´�
    {
        obj_v.SetActive(false);
        shooter.Enqueue(obj_v);
        capacity++;
    }

    public GameObject PopQueue() // ť���� ���� ������
    {
        if (shooter == null || shooter.Count == 0)
        {
            Debug.LogError("ShooterQueue: PopQueue ���� - Queue�� ��� �ֽ��ϴ�.");
            return null;
        }

        GameObject obj_v = shooter.Dequeue();
        obj_v.SetActive(true);
        capacity--;
        return obj_v;
    }
}
