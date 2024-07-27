using UnityEngine;

public class Player : MonoBehaviour
{
    // ������ �� ������, ������� ����� ���������
    MoveCitizen moveCitizenArrest = null;
    // ����, �����������, ����� �� ����� (��������, �������)
    public bool isBusy = false;

    // ����������� ������ �� ��������� ������ (Singleton)
    public static Player Instance;

    private void Awake()
    {
        // ��������, ���������� �� ��� ��������� ������
        if (!Instance)
            Instance = this; // ���� ���, ������������� ������� ������ ��� ���������
        else if (Instance != this)
            Destroy(gameObject); // ���� ��������� ��� ����������, ���������� ����� ������
    }

    private void Update()
    {
        // ���� ������ �����������, ��������� ������� ������ �� ��� Y �� ������ ������
        if (Jail.Instance.isInstall)
        {
            transform.position = new Vector3(transform.position.x, Jail.Instance.transform.position.y, transform.position.z);
        }
    }

    // ����� ��� ������ ����������
    public void Arrest()
    {
        // ���������, ���� �� ��������� ��� ������ � �� ����� �� �����
        if (moveCitizenArrest != null && !isBusy)
        {
            isBusy = true; // ������������� ���� ���������
            moveCitizenArrest.isArrested = true; // ������������� ������ ������ � ����������
            moveCitizenArrest.transform.parent = transform; // ������������� ������ ��������� ��� �������������
            moveCitizenArrest.transform.localPosition = new Vector3(0.116f, 0, 0.328f); // ������������� ��������� ������� �������������

            // ���� ��������� ��� � �������� �����, ������ ��� ������������
            if (moveCitizenArrest.Stealing)
            {
                moveCitizenArrest.View[0].SetActive(false); // ��������� ��� �����
                moveCitizenArrest.View[1].SetActive(true); // �������� ��� ������
            }
        }
    }

    // ����� ��� ������������ ����������
    public void Otpustit()
    {
        // ���������, ���� �� ������������ ���������
        if (moveCitizenArrest != null)
        {
            isBusy = false; // ���������� ���� ���������
            moveCitizenArrest.isArrested = false; // ���������� ������ ������ � ����������
            moveCitizenArrest.Stealing = false; // ���������� ������ �����
            moveCitizenArrest.transform.parent = null; // ������� �������� � �������������
            moveCitizenArrest.View[0].SetActive(true); // �������� ��� �����
            moveCitizenArrest.View[1].SetActive(false); // ��������� ��� ������
        }
    }

    // �����, ���������� ��� ����� � �������
    private void OnTriggerEnter(Collider other)
    {
        // ���������, ���� �� ��������� MoveCitizen � �������, � ������� �����������
        if (other.TryGetComponent(out MoveCitizen moveCitizen))
        {
            // ���� ����� �����, ������� �� ������
            if (isBusy)
                return;

            // ������������� ������ �� ����������, ������� ����� � �������
            moveCitizenArrest = moveCitizen;
        }
    }

    // �����, ���������� ��� ������ �� ��������
    private void OnTriggerExit(Collider other)
    {
        // ���������, ���� �� ��������� MoveCitizen � �������, � ������� �����������
        if (other.TryGetComponent(out MoveCitizen moveCitizen))
        {
            // ���� ����� �����, ������� �� ������
            if (isBusy)
                return;

            // ���������� ������ �� ����������, ������� ����� �� ��������
            moveCitizenArrest = null;
        }
    }
}