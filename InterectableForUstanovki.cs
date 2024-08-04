using UnityEngine; // Подключаем пространство имен UnityEngine для работы с игровыми объектами и компонентами
using UnityEngine.UI; // Подключаем пространство имен для работы с UI элементами, такими как кнопки

public class InterectableForUstanovki : MonoBehaviour // Определяем класс, который наследует от MonoBehaviour
{
    // Объявляем делегат для обработки взаимодействий
    public delegate void Interect();

    // Статическое событие, к которому могут подписываться другие классы
    public static Interect InterectForUstanovki;

    // Ссылка на кнопку, которая будет взаимодействовать с игроком
    public Button PlatformImageButton;

    private void Start()
    {
        // Делаем кнопку неактивной при старте игры
        PlatformImageButton.interactable = false;
    }

    private void OnEnable()
    {
        // Подписываемся на событие InterectForUstanovki, чтобы вызывать метод inter при его срабатывании
        InterectForUstanovki += inter;
    }

    private void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта, чтобы избежать утечек памяти
        InterectForUstanovki -= inter;
    }

    private void inter()
    {
        // Проверяем, найден ли игрок
        if (PlayerFoundLost.Instance.IsFound)
        {
            // Если игрок найден, делаем кнопку активной
            PlatformImageButton.interactable = true;
        }
        else
        {
            // Если игрок не найден, делаем кнопку неактивной
            PlatformImageButton.interactable = false;
        }
    }
}