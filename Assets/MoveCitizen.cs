using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCitizen : MonoBehaviour
{
    // �������� �������� � �������� ����������
    public float speed = 1;
    public float RotateSpeed = 1;

    // ������� ��� �������� � ���������� ��������
    public GameObject[] Status;
    public GameObject[] View;

    // ������ �������� ����� ��� �����������
    private List<int> PointIndexes = new List<int>();
    private Transform moveTarget; // ������� ����� ��� �����������
    private Vector3 relativePos; // ������������� ������� � ������� �����
    Collider m_Collider; // ��������� �������
    RaycastHit m_Hit; // ��������� Raycast
    float m_MaxDistance; // ������������ ��������� ��� Raycast
    bool m_HitDetect; // ����, ����������� �� ����������� �����������
    public bool Stealing = false; // ����, ����������� �� �����
    bool isNotTalk = true; // ����, �����������, ������� �� ���������
    public bool isArrested = false; // ����, �����������, ��������� �� ���������

    void Start()
    {
        // ��������� �������� ��� ����������� ����
        StartCoroutine(OpredelenieCeli());
        m_Collider = GetComponent<Collider>(); // �������� ���������
        m_MaxDistance = 1.0f; // ������������� ������������ ��������� ��� Raycast
    }

    void Update()
    {
        // ���� ��������� �� ���������, ���������� ��� � ����
        if (!isArrested)
            walkToTarget();
    }

    private void OnDestroy()
    {
        // ������� ���������� �� ���������������� ������ ��� �����������
        if (Stealing)
            TalkAndThieftManager.Instance.thiefs.Remove(this);
        else
            TalkAndThieftManager.Instance.speakers.Remove(this);
    }

    // �������� ��� ��������� ����������
    public IEnumerator Talking(float timer = 5f, bool isThief = false)
    {
        // ���������, ����� �� ��������� ��������
        if (isNotTalk && !isArrested)
        {
            isNotTalk = false; // ������������� ����, ��� ��������� �������

            // ������������� ������ � ����������� �� ����, ������ �� ���������
            if (isThief)
            {
                Stealing = true;
                Status[1].SetActive(true); // ���������� ������ ����
                TalkAndThieftManager.Instance.thiefs.Add(this); // ��������� � ������ �����
            }
            else
            {
                Stealing = false;
                Status[0].SetActive(true); // ���������� ������ ����������
                TalkAndThieftManager.Instance.speakers.Add(this); // ��������� � ������ ���������
            }

            yield return new WaitForSeconds(timer); // ���� ��������� �����

            // ������� ���������� �� ���������������� ������
            if (Stealing)
                TalkAndThieftManager.Instance.thiefs.Remove(this);
            else
                TalkAndThieftManager.Instance.speakers.Remove(this);

            isNotTalk = true; // ���������� ����

            // ������������ �������
            Status[0].SetActive(false);
            Status[1].SetActive(false);

            // ���� ��������� �� ���������, ���������� ������ �����
            if (!isArrested)
                Stealing = false;
        }
    }

    // �������� ��� ����������� ���� �����������
    IEnumerator OpredelenieCeli()
    {
        while (true)
        {
            if (!isArrested)
            {
                PointIndexes.Clear(); // ������� ������ ��������
                // ��������� ��� ����� ������
                for (int i = 0; i < Spawner.Instance.SpawnPoints.Length; i++)
                {
                    if (Spawner.Instance.SpawnPoints[i].isEmpty)
                        PointIndexes.Add(i); // ��������� ������ ������ �����
                }

                // ���� ���� ��������� �����, �������� ���� ��������� �������
                if (PointIndexes.Count > 0)
                    moveTarget = Spawner.Instance.SpawnPoints[Random.Range(0, PointIndexes.Count)].transform;
            }

            yield return new WaitForSeconds(4); // ���� 4 ������� ����� ��������� ���������
        }
    }

    // ����� ��� ����������� ���������� � ������� �����
    void walkToTarget()
    {
        relativePos = moveTarget.position - transform.position; // ��������� ������������� �������
        relativePos = new Vector3(relativePos.x, 0, relativePos.z); // ���������� ������
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up); // ��������� ��������
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * RotateSpeed); // ������ ������������ � ����

        // ���� ��� �����������, ���������� ����������
        if (!m_HitDetect)
            transform.position = Vector3.MoveTowards(transform.position, moveTarget.position, speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // ��������� ������� ����������� � ������� BoxCast
        if (!isArrested)
            m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, transform.localScale, transform.forward, out m_Hit, transform.rotation, m_MaxDistance);
    }
}