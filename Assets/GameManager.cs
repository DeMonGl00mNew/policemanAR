using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// ����� GameManager �������� �� ���������� ���������� ����
public class GameManager : MonoBehaviour
{
    // ���������� ��� �������� �������� (��� �����)
    public int health = 0;

    // ������ �� ��������� �������� ��� ����������� ����� � ���������� ����������
    public TMP_Text Score;
    public TMP_Text Final;

    // ����������� ���������� ��� ���������� �������� Singleton
    public static GameManager Instance;

    private void Awake()
    {
        // ���������, ���������� �� ��� ��������� GameManager
        if (!Instance)
            Instance = this; // ���� ���, ������������� ������� ���������
        else if (Instance != this)
            Destroy(gameObject); // ���� ����������, ���������� ������� ������
    }

    private void Start()
    {
        // �������������� �������� � ��������� ����� �����
        health = 0;
        Score.text = health.ToString();
    }

    // ����� ��� ������ �� ����
    public void Exit()
    {
        Application.Quit(); // ��������� ����������
    }

    // ����� ��� ����������� ����
    public void Restart()
    {
        SceneManager.LoadScene("Game"); // ��������� ����� � ������ "Game"
    }

    // ����� ��� ���������� ����� �� �������� ����������
    public void Schet(int count)
    {
        health += count; // ����������� �������� �� count
        Score.text = health.ToString(); // ��������� ����� �����
    }

    // �����, ���������� ��� ��������� ����
    public void GameOver()
    {
        // ���������, ���� �� ����
        if (health > 0)
        {
            Final.gameObject.SetActive(true); // ���������� ������ � ��������� �����������
            Final.text = $"�� ������ {health} ����� �� 6."; // ���������� ���������� ��������� �����
        }
        else
        {
            Final.gameObject.SetActive(true); // ���������� ������ � ��������� �����������
            Final.text = $"���� ������ ������ ������������!"; // ���������, ���� ����� �� ������ ������
        }
    }
}