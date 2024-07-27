using UnityEngine;

// Класс PlayerFoundLost отвечает за отслеживание состояния игрока (найден/потерян)
public class PlayerFoundLost : MonoBehaviour
{
    // Переменная для хранения состояния найденного игрока
    int i = 0;

    // Статическое свойство для реализации паттерна Singleton
    static public PlayerFoundLost Instance { get; private set; }

    // Флаг, указывающий, найден ли игрок
    [HideInInspector] public bool IsFound = false;

    // Метод Awake вызывается при инициализации объекта
    void Awake()
    {
        // Проверяем, если экземпляр еще не создан
        if (Instance == null)
        {
            // Устанавливаем текущий экземпляр как единственный
            Instance = this;
        }
        // Если экземпляр уже существует и это не он
        else if (Instance != this)
        {
            // Уничтожаем этот объект, чтобы избежать дублирования
            Destroy(gameObject);
        }
    }

    // Метод для обработки события, когда игрок найден
    public void Found()
    {
        // Устанавливаем флаг IsFound в true
        IsFound = true;

        // Вызываем событие, связанное с взаимодействием для установки
        InterectableForUstanovki.InterectForUstanovki?.Invoke();
    }

    // Метод для обработки события, когда игрок потерян
    public void Lost()
    {
        // Устанавливаем флаг IsFound в false
        IsFound = false;

        // Вызываем событие, связанное с взаимодействием для установки
        InterectableForUstanovki.InterectForUstanovki?.Invoke();
    }
}