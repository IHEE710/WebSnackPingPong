using TMPro;
using UnityEngine;

public class Block : MonoBehaviour // �� Ŭ������
{
    [SerializeField] private SpriteRenderer renderer; // �������� �������̴�
    [SerializeField] private Collider2D col; // �ε����� �� �����ϴ� collider�̴�
    [SerializeField] private GameObject textingObj; // �� ���� ������Ʈ�̴�
    [SerializeField] private int count = 0; // �� ī��Ʈ�̴�
    [SerializeField] private TMP_Text textCount; // ���� ������ ī��Ʈ ���ڴ�

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        textingObj = transform.GetChild(0).gameObject;
        textCount = textingObj.GetComponent<TMP_Text>();
        textCount.text = count.ToString();
    }

    public void UnenableInteract() // ���� �Ⱥ��̰� �ϴ� �Լ���
    {
        renderer.enabled = false;
        col.enabled = false;
        textingObj.SetActive(false);
    }

    public void EnableInteract(int count) // ���� ���̰��ϴ� �Լ���
    {
        renderer.enabled = true;
        col.enabled = true;
        textCount.text = count.ToString();
        textingObj.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision) // �ε����� �� ī��Ʈ�� ���߰� ī��Ʈ ���� �����Ѵ� 0���ϰ� �Ǹ� �Ⱥ��̰� �Ѵ�
    {
        count--;
        textCount.text = count.ToString();
        if(count <= 0)
        {
            UnenableInteract();
        }
    }
}
