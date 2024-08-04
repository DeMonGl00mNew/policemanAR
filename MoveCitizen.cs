using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCitizen : MonoBehaviour
{
    // Скорость движения и вращения гражданина
    public float speed = 1;
    public float RotateSpeed = 1;

    // Массивы для статусов и визуальных эффектов
    public GameObject[] Status;
    public GameObject[] View;

    // Список индексов точек для перемещения
    private List<int> PointIndexes = new List<int>();
    private Transform moveTarget; // Целевая точка для перемещения
    private Vector3 relativePos; // Относительная позиция к целевой точке
    Collider m_Collider; // Коллайдер объекта
    RaycastHit m_Hit; // Результат Raycast
    float m_MaxDistance; // Максимальная дистанция для Raycast
    bool m_HitDetect; // Флаг, указывающий на обнаружение препятствий
    public bool Stealing = false; // Флаг, указывающий на кражу
    bool isNotTalk = true; // Флаг, указывающий, говорит ли гражданин
    public bool isArrested = false; // Флаг, указывающий, арестован ли гражданин

    void Start()
    {
        // Запускаем корутину для определения цели
        StartCoroutine(OpredelenieCeli());
        m_Collider = GetComponent<Collider>(); // Получаем коллайдер
        m_MaxDistance = 1.0f; // Устанавливаем максимальную дистанцию для Raycast
    }

    void Update()
    {
        // Если гражданин не арестован, перемещаем его к цели
        if (!isArrested)
            walkToTarget();
    }

    private void OnDestroy()
    {
        // Удаляем гражданина из соответствующего списка при уничтожении
        if (Stealing)
            TalkAndThieftManager.Instance.thiefs.Remove(this);
        else
            TalkAndThieftManager.Instance.speakers.Remove(this);
    }

    // Корутину для разговора гражданина
    public IEnumerator Talking(float timer = 5f, bool isThief = false)
    {
        // Проверяем, может ли гражданин говорить
        if (isNotTalk && !isArrested)
        {
            isNotTalk = false; // Устанавливаем флаг, что гражданин говорит

            // Устанавливаем статус в зависимости от того, ворует ли гражданин
            if (isThief)
            {
                Stealing = true;
                Status[1].SetActive(true); // Активируем статус вора
                TalkAndThieftManager.Instance.thiefs.Add(this); // Добавляем в список воров
            }
            else
            {
                Stealing = false;
                Status[0].SetActive(true); // Активируем статус говорящего
                TalkAndThieftManager.Instance.speakers.Add(this); // Добавляем в список говорящих
            }

            yield return new WaitForSeconds(timer); // Ждем указанное время

            // Удаляем гражданина из соответствующего списка
            if (Stealing)
                TalkAndThieftManager.Instance.thiefs.Remove(this);
            else
                TalkAndThieftManager.Instance.speakers.Remove(this);

            isNotTalk = true; // Сбрасываем флаг

            // Деактивируем статусы
            Status[0].SetActive(false);
            Status[1].SetActive(false);

            // Если гражданин не арестован, сбрасываем статус кражи
            if (!isArrested)
                Stealing = false;
        }
    }

    // Корутину для определения цели перемещения
    IEnumerator OpredelenieCeli()
    {
        while (true)
        {
            if (!isArrested)
            {
                PointIndexes.Clear(); // Очищаем список индексов
                // Проверяем все точки спавна
                for (int i = 0; i < Spawner.Instance.SpawnPoints.Length; i++)
                {
                    if (Spawner.Instance.SpawnPoints[i].isEmpty)
                        PointIndexes.Add(i); // Добавляем индекс пустой точки
                }

                // Если есть доступные точки, выбираем одну случайным образом
                if (PointIndexes.Count > 0)
                    moveTarget = Spawner.Instance.SpawnPoints[Random.Range(0, PointIndexes.Count)].transform;
            }

            yield return new WaitForSeconds(4); // Ждем 4 секунды перед следующей проверкой
        }
    }

    // Метод для перемещения гражданина к целевой точке
    void walkToTarget()
    {
        relativePos = moveTarget.position - transform.position; // Вычисляем относительную позицию
        relativePos = new Vector3(relativePos.x, 0, relativePos.z); // Игнорируем высоту
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up); // Вычисляем вращение
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * RotateSpeed); // Плавно поворачиваем к цели

        // Если нет препятствий, перемещаем гражданина
        if (!m_HitDetect)
            transform.position = Vector3.MoveTowards(transform.position, moveTarget.position, speed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // Проверяем наличие препятствий с помощью BoxCast
        if (!isArrested)
            m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, transform.localScale, transform.forward, out m_Hit, transform.rotation, m_MaxDistance);
    }
}