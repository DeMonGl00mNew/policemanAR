using UnityEngine;


public class Jail : MonoBehaviour
{
    public Transform PlayerTransform; // ������ �� ��������� ������
    public bool isInstall = false; // ���� ��������� ������
    public static Jail Instance; // ����������� ��������� ������
    private void Awake()
    {
        if (!Instance)
            Instance = this; // ������������� ������� ��������� ��� ������������
        else if (Instance != this)
            Destroy(gameObject); // ���������� ������, ���� ��� ���������� ������ ���������
    }

    public void PloshadkaSetup()
    {
        if (PlayerFoundLost.Instance.IsFound) // ���� ����� ������
        {
            Player.Instance.transform.localPosition = new Vector3(0, 0, 0); // ������������� ������� ������ � ��������� �����
            transform.position = PlayerTransform.position - new Vector3(0, 0, 1f); // ������������� ������� ������ � ������ ��������
            transform.rotation = PlayerTransform.rotation; // ������������� ������� ������ ��� � ������
            isInstall = true; // ������������� ���� ��������� ������ � true
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MoveCitizen moveCitizen)) // ���� ������ ������ ����� ��������� MoveCitizen
        {
            if (moveCitizen.isArrested) // ���� ��������� ���������
            {
                Player.Instance.isBusy = false; // ������������� ���� ��������� ������ � false

                if (moveCitizen.Stealing) // ���� ��������� ������
                    GameManager.Instance.Schet(+1); // ����������� ������� ���������
                else
                    GameManager.Instance.Schet(-1); // ��������� ������� ���������
                Spawner.Citizens.Remove(moveCitizen); // ������� ���������� �� ������
                Destroy(moveCitizen.gameObject); // ���������� ������ ����������

                if (Spawner.Citizens.Count == 0) // ���� ������ ��� �������
                {
                    GameManager.Instance.GameOver(); // �������� ����� ���������� ����
                }
            }
        }
    }



}
