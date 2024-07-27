using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� SpawnPoint �������� �� ���������� ���������� ����� ������
public class SpawnPoint : MonoBehaviour
{
    // ����������, �����������, ����� �� ����� ������
    public bool isEmpty = true;

    // ����� ���������� ��� ����� ������� ������� � �������
    private void OnTriggerEnter(Collider other)
    {
        // ���������, ����� �� ������ ��������� MoveCitizen
        if (other.TryGetComponent(out MoveCitizen moveCitizen))
            // ���� ��, �� ����� ������ �� �����
            isEmpty = false;
    }

    // ����� ���������� ��� ������ ������� ������� �� ��������
    private void OnTriggerExit(Collider other)
    {
        // ���������, ����� �� ������ ��������� MoveCitizen
        if (other.TryGetComponent(out MoveCitizen moveCitizen))
            // ���� ��, �� ����� ����� �����
            isEmpty = true;
    }
}