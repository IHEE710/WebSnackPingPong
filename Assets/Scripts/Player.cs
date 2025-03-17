using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private bool turningAllow = false; // ȸ���� ��뿩��
    [SerializeField] private bool clickAllow = false; // Ŭ�� ��� ����
    [SerializeField] private int ballCount = 5; // ������ �� ����
    [SerializeField] private float gap = 0.05f; // �� �߻� Ÿ�� ����
    [SerializeField] private SpriteRenderer playerRender; // �÷��̾� ���̰��ϴ� ����
    [SerializeField] private Vector2 playerPos = new Vector2(0, 1); // �÷��̾ �⺻���� �ٶ󺸰� �ִ� ���� ����
    [SerializeField] private GameObject directer; // ȭ��ǥ ������Ʈ
    [SerializeField] private Transform playerTrans; // �÷��̾� Ʈ������
    [SerializeField] private Vector2 pointerPos; // Ŭ�������� ���� ����
    [SerializeField] private Vector2 directPos; // ȭ��ǥ ����
    [SerializeField] private ShooterQueue polling; // ���� ������ Ǯ (ť)

    public void Awake()
    {
        polling = GetComponent<ShooterQueue>();
        directer = transform.GetChild(0).gameObject;
        playerTrans = GetComponent<Transform>();
        playerRender = GetComponent<SpriteRenderer>();
        directer.SetActive(false);
        //playerRender.enabled = false;
    }

    public void SpawnPlayer() // ���ӽ��� �� �÷��̾ ���̰��ϱ�
    {
        playerRender.enabled = true;
        //directer.SetActive(true);
        clickAllow = true;
        polling.InitQueue(ballCount);
    }

    public void DestoryPlayer() // ���ӿ����� �÷��̾ ������ �ʰ� �ϱ�
    {
        polling.RemoveQueue();
        clickAllow = false;
        directer.SetActive(false);
        playerRender.enabled = false;
    }

    public void OnLoadShoot(InputAction.CallbackContext context) // Ŭ�� �� ���� �߻��Ѵ� ���� �ٽ� �ǵ��ƿ��� ������ Ŭ���� ������ �ʴ´�
    {
        if (!clickAllow) return;
        if (context.performed) // ���������� �� ȸ���� ����Ѵ�
        {
            directer.SetActive(true);
            turningAllow = true;
        }
        else if (context.canceled && turningAllow) // ������ ��ҵǰ� ȸ���� ����Ǿ������� ���� �߻��Ѵ�
        {
            StartCoroutine(Shooting());
            directer.SetActive(false);
            clickAllow = false;
            turningAllow = false;
        }
    }

    public void OnRotate(InputAction.CallbackContext context) // �÷��̾ ȸ���Ѵ�
    {
        if (!turningAllow) return;
        pointerPos = context.ReadValue<Vector2>();
        directPos = -(pointerPos - (Vector2)playerTrans.position).normalized;
        if (Vector2.Dot(playerPos, directPos) < 0)
        {
            return;
        }
        playerTrans.up = directPos;
    }

    public IEnumerator Shooting() // ���� �� ������ ������ ���� �߻��Ѵ�
    {
        for(int i = 0; i < polling.Capacity; i++)
        {
            GameObject obj_v = polling.PopQueue();
            obj_v.transform.position = playerTrans.position;
            Ball ball_v = obj_v.GetComponent<Ball>();
            ball_v.Move(directPos);
            yield return new WaitForSeconds(gap);
        }
    }

    public void Return(GameObject obj_v) // ���� �ٽ� ��ȯ�ϸ� ���� ���� ��ȯ������ Ŭ���� ����������
    {
        obj_v.transform.position = playerTrans.position;
        polling.PollingQueue(obj_v);
        if(polling.Capacity == polling.Size) clickAllow = true;
    }
}
