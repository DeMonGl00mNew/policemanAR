using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������� ��� ���������� ���������������� ����� ���������� � ������
public class TalkAndThieftManager : MonoBehaviour
{
    // ������ �����
    public List<MoveCitizen> thiefs = new List<MoveCitizen>();
    // ������ ��������� �������
    public List<MoveCitizen> speakers = new List<MoveCitizen>();
    // ������������ ���������� �����
    public int thiefsMax = 1;

    // ����������� ������ �� ��������� ���������
    public static TalkAndThieftManager Instance;

    // ����� Awake ���������� ��� ������������� �������
    private void Awake()
    {
        // ���������, ���������� �� ��� ���������
        if (!Instance)
            Instance = this; // ���� ���, ������� ����� ���������
        else if (Instance != this)
            Destroy(gameObject); // ���������� ���������
    }

    // ����� Start ���������� ����� ������ ������
    private void Start()
    {
        // ��������� �������� ��� ���������� ����������������
        StartCoroutine(ThiefOrSpeak());
    }

    // �������� ��� ���������� ���������� ����� � ��������� �������
    IEnumerator ThiefOrSpeak()
    {
        // ����������� ����
        while (true)
        {
            // ���������, ���� �� �������� ��� ��������������
            if (Spawner.Citizens.Count > 0)
            {
                // ���� ���������� ����� ������ �������������
                if (thiefs.Count < thiefsMax)
                {
                    // ��������� �������� ��� ���������� ����������, ����� �� ����� �������� ��� ���
                    StartCoroutine(Spawner.Citizens[Random.Range(0, Spawner.Citizens.Count)].Talking(5, true));
                }
                // ���� ���������� ��������� ������� ������ ���������� �������
                else if (speakers.Count < Spawner.Citizens.Count - thiefsMax)
                {
                    // ��������� �������� ��� ���������� ����������, ����� �� ����� �������� ��� ������� ���������
                    StartCoroutine(Spawner.Citizens[Random.Range(0, Spawner.Citizens.Count)].Talking(5, false));
                }
                // ���� 3 ������� ����� ��������� ���������
                yield return new WaitForSeconds(3);
            }
            else
                yield return null; // ���� ������� ���, ���� ���������� �����
        }
    }
}