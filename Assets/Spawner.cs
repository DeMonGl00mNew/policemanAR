using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� Spawner �������� �� �������� NPC (�������) � ����
public class Spawner : MonoBehaviour
{
    // ������ ����� ������, ��� ����� ���������� NPC
    public SpawnPoint[] SpawnPoints;

    // ����������� ������ ��� �������� ���� ��������� �������
    static public List<MoveCitizen> Citizens = new List<MoveCitizen>();

    // ������ ����������, ������� ����� �����������
    public GameObject CitizenPrefab;

    // ���������� �������, ������� ����� �������
    public int count = 2;

    // ����������� ������ �� ��������� Spawner ��� ���������� �������� Singleton
    public static Spawner Instance;

    // ����� Awake ���������� ��� ������������� �������
    private void Awake()
    {
        // ���������, ���������� �� ��� ��������� Spawner
        if (!Instance)
            Instance = this; // ���� ���, ������������� ������� ������ ��� ���������
        else if (Instance != this)
            Destroy(gameObject); // ���� ��������� ��� ����������, ���������� ������� ������
    }

    // ����� GameOn ���������� ��� ������ ������ �������
    public void GameOn()
    {
        // ������� ��������� ���������� �������
        for (int i = 0; i < count; i++)
        {
            // ��������� ���������� ���������� � ������ Citizens
            Citizens.Add(spawnNPC(CitizenPrefab, i).GetComponent<MoveCitizen>());
        }
    }

    // ����� ��� ������ NPC
    private GameObject spawnNPC(GameObject NPC, int number)
    {
        // ������� ��������� NPC � ��������� ������� � � ��������� �����������
        GameObject currentNPC = Instantiate(NPC, RandomPlace().position, Quaternion.Euler(0, Random.Range(0, 180), 0), transform);
        // ������������� ��� NPC
        currentNPC.name = "citizen " + number;
        return currentNPC; // ���������� ��������� ������
    }

    // ����� ��� ��������� ��������� ����� ������
    private Transform RandomPlace()
    {
        // �������� �� ���� ������ ������
        foreach (var point in SpawnPoints)
        {
            // ���� ����� ��������, ���������� �
            if (point.isEmpty)
                return point.transform;
        }
        // ���� ��� ����� ������, ���������� ��������� ������������� �������
        return transform;
    }
}