using UnityEngine;

// ����� PlayerFoundLost �������� �� ������������ ��������� ������ (������/�������)
public class PlayerFoundLost : MonoBehaviour
{
    // ���������� ��� �������� ��������� ���������� ������
    int i = 0;

    // ����������� �������� ��� ���������� �������� Singleton
    static public PlayerFoundLost Instance { get; private set; }

    // ����, �����������, ������ �� �����
    [HideInInspector] public bool IsFound = false;

    // ����� Awake ���������� ��� ������������� �������
    void Awake()
    {
        // ���������, ���� ��������� ��� �� ������
        if (Instance == null)
        {
            // ������������� ������� ��������� ��� ������������
            Instance = this;
        }
        // ���� ��������� ��� ���������� � ��� �� ��
        else if (Instance != this)
        {
            // ���������� ���� ������, ����� �������� ������������
            Destroy(gameObject);
        }
    }

    // ����� ��� ��������� �������, ����� ����� ������
    public void Found()
    {
        // ������������� ���� IsFound � true
        IsFound = true;

        // �������� �������, ��������� � ��������������� ��� ���������
        InterectableForUstanovki.InterectForUstanovki?.Invoke();
    }

    // ����� ��� ��������� �������, ����� ����� �������
    public void Lost()
    {
        // ������������� ���� IsFound � false
        IsFound = false;

        // �������� �������, ��������� � ��������������� ��� ���������
        InterectableForUstanovki.InterectForUstanovki?.Invoke();
    }
}