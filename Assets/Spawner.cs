using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс Spawner отвечает за создание NPC (граждан) в игре
public class Spawner : MonoBehaviour
{
    // Массив точек спавна, где могут появляться NPC
    public SpawnPoint[] SpawnPoints;

    // Статический список для хранения всех созданных граждан
    static public List<MoveCitizen> Citizens = new List<MoveCitizen>();

    // Префаб гражданина, который будет создаваться
    public GameObject CitizenPrefab;

    // Количество граждан, которое будет создано
    public int count = 2;

    // Статическая ссылка на экземпляр Spawner для реализации паттерна Singleton
    public static Spawner Instance;

    // Метод Awake вызывается при инициализации объекта
    private void Awake()
    {
        // Проверяем, существует ли уже экземпляр Spawner
        if (!Instance)
            Instance = this; // Если нет, устанавливаем текущий объект как экземпляр
        else if (Instance != this)
            Destroy(gameObject); // Если экземпляр уже существует, уничтожаем текущий объект
    }

    // Метод GameOn вызывается для начала спавна граждан
    public void GameOn()
    {
        // Создаем указанное количество граждан
        for (int i = 0; i < count; i++)
        {
            // Добавляем созданного гражданина в список Citizens
            Citizens.Add(spawnNPC(CitizenPrefab, i).GetComponent<MoveCitizen>());
        }
    }

    // Метод для спавна NPC
    private GameObject spawnNPC(GameObject NPC, int number)
    {
        // Создаем экземпляр NPC в случайной позиции и с случайной ориентацией
        GameObject currentNPC = Instantiate(NPC, RandomPlace().position, Quaternion.Euler(0, Random.Range(0, 180), 0), transform);
        // Устанавливаем имя NPC
        currentNPC.name = "citizen " + number;
        return currentNPC; // Возвращаем созданный объект
    }

    // Метод для получения случайной точки спавна
    private Transform RandomPlace()
    {
        // Проходим по всем точкам спавна
        foreach (var point in SpawnPoints)
        {
            // Если точка свободна, возвращаем её
            if (point.isEmpty)
                return point.transform;
        }
        // Если все точки заняты, возвращаем трансформ родительского объекта
        return transform;
    }
}