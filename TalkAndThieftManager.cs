using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Менеджер для управления взаимодействиями между гражданами и ворами
public class TalkAndThieftManager : MonoBehaviour
{
    // Список воров
    public List<MoveCitizen> thiefs = new List<MoveCitizen>();
    // Список говорящих граждан
    public List<MoveCitizen> speakers = new List<MoveCitizen>();
    // Максимальное количество воров
    public int thiefsMax = 1;

    // Статическая ссылка на экземпляр менеджера
    public static TalkAndThieftManager Instance;

    // Метод Awake вызывается при инициализации объекта
    private void Awake()
    {
        // Проверяем, существует ли уже экземпляр
        if (!Instance)
            Instance = this; // Если нет, создаем новый экземпляр
        else if (Instance != this)
            Destroy(gameObject); // Уничтожаем дубликаты
    }

    // Метод Start вызывается перед первым кадром
    private void Start()
    {
        // Запускаем корутину для управления взаимодействиями
        StartCoroutine(ThiefOrSpeak());
    }

    // Корутину для управления действиями воров и говорящих граждан
    IEnumerator ThiefOrSpeak()
    {
        // Бесконечный цикл
        while (true)
        {
            // Проверяем, есть ли граждане для взаимодействия
            if (Spawner.Citizens.Count > 0)
            {
                // Если количество воров меньше максимального
                if (thiefs.Count < thiefsMax)
                {
                    // Запускаем корутину для случайного гражданина, чтобы он начал говорить как вор
                    StartCoroutine(Spawner.Citizens[Random.Range(0, Spawner.Citizens.Count)].Talking(5, true));
                }
                // Если количество говорящих граждан меньше оставшихся граждан
                else if (speakers.Count < Spawner.Citizens.Count - thiefsMax)
                {
                    // Запускаем корутину для случайного гражданина, чтобы он начал говорить как обычный гражданин
                    StartCoroutine(Spawner.Citizens[Random.Range(0, Spawner.Citizens.Count)].Talking(5, false));
                }
                // Ждем 3 секунды перед следующей итерацией
                yield return new WaitForSeconds(3);
            }
            else
                yield return null; // Если граждан нет, ждем следующего кадра
        }
    }
}