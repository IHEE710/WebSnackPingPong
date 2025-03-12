using System.Collections.Generic;
using UnityEngine;

public class BlockGroup : MonoBehaviour // �� �׷� Ŭ������
{
    [SerializeField] private List<GameObject> blocks = new List<GameObject>(); // ���� �����ϴ� 1���� ����Ʈ��
    [SerializeField] private Transform groupPosition; // ���׷�(��ü)�� ��ġ�̴�
    [SerializeField] private bool currentStage; // ���׷��� 2���� ��� ���� �����̱⿡ ���� �� �׷��� ���� ���������� ��Ÿ�����ϴ� ������������ �����Ѵ�
    [SerializeField] private BlockCounter couter; //���� ī��Ʈ�� ������ ��ũ��Ʈ�̴�
    [SerializeField] private int arrySize = 84; // ���� �� ������
    [SerializeField] private int firstSize = 21; //���� �� ������

    void Awake()
    {
        couter = FindAnyObjectByType<BlockCounter>();
        groupPosition = GetComponent<Transform>();
        for (int i = 0; i < arrySize; i++)
        {
            GameObject obj_v = transform.GetChild(i).gameObject;
            blocks.Add(obj_v);
        }
    }

    public void SettingBlockCount(int start) // ���� ���������Ѵٸ� ���� ī��Ʈ���� ���ϰ� ���̰��Ѵ� (���� ������ ������� �ʴ´�)
    {
        couter.SelectProb();
        for (int i = start; i < arrySize; i++)
        {
            Block obj_v = blocks[i].GetComponent<Block>();
            obj_v.EnableInteract(couter.ChooseProbAndCount());
        }
    }

    public void ResetBlockGruop() // ���� ������ �ʱ�ȭ ��ų �Լ���
    {
        
    }
}
