using UnityEngine;

public class Player : MonoBehaviour
{
    // Ссылка на объект, который будет арестован
    MoveCitizen moveCitizenArrest = null;
    // Флаг, указывающий, занят ли игрок (например, арестом)
    public bool isBusy = false;

    // Статическая ссылка на экземпляр игрока (Singleton)
    public static Player Instance;

    private void Awake()
    {
        // Проверка, существует ли уже экземпляр игрока
        if (!Instance)
            Instance = this; // Если нет, устанавливаем текущий объект как экземпляр
        else if (Instance != this)
            Destroy(gameObject); // Если экземпляр уже существует, уничтожаем новый объект
    }

    private void Update()
    {
        // Если тюрьма установлена, фиксируем позицию игрока по оси Y на уровне тюрьмы
        if (Jail.Instance.isInstall)
        {
            transform.position = new Vector3(transform.position.x, Jail.Instance.transform.position.y, transform.position.z);
        }
    }

    // Метод для ареста гражданина
    public void Arrest()
    {
        // Проверяем, есть ли гражданин для ареста и не занят ли игрок
        if (moveCitizenArrest != null && !isBusy)
        {
            isBusy = true; // Устанавливаем флаг занятости
            moveCitizenArrest.isArrested = true; // Устанавливаем статус ареста у гражданина
            moveCitizenArrest.transform.parent = transform; // Устанавливаем игрока родителем для арестованного
            moveCitizenArrest.transform.localPosition = new Vector3(0.116f, 0, 0.328f); // Устанавливаем локальную позицию арестованного

            // Если гражданин был в процессе кражи, меняем его визуализацию
            if (moveCitizenArrest.Stealing)
            {
                moveCitizenArrest.View[0].SetActive(false); // Отключаем вид кражи
                moveCitizenArrest.View[1].SetActive(true); // Включаем вид ареста
            }
        }
    }

    // Метод для освобождения гражданина
    public void Otpustit()
    {
        // Проверяем, есть ли арестованный гражданин
        if (moveCitizenArrest != null)
        {
            isBusy = false; // Сбрасываем флаг занятости
            moveCitizenArrest.isArrested = false; // Сбрасываем статус ареста у гражданина
            moveCitizenArrest.Stealing = false; // Сбрасываем статус кражи
            moveCitizenArrest.transform.parent = null; // Убираем родителя у арестованного
            moveCitizenArrest.View[0].SetActive(true); // Включаем вид кражи
            moveCitizenArrest.View[1].SetActive(false); // Отключаем вид ареста
        }
    }

    // Метод, вызываемый при входе в триггер
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, есть ли компонент MoveCitizen у объекта, с которым столкнулись
        if (other.TryGetComponent(out MoveCitizen moveCitizen))
        {
            // Если игрок занят, выходим из метода
            if (isBusy)
                return;

            // Устанавливаем ссылку на гражданина, который попал в триггер
            moveCitizenArrest = moveCitizen;
        }
    }

    // Метод, вызываемый при выходе из триггера
    private void OnTriggerExit(Collider other)
    {
        // Проверяем, есть ли компонент MoveCitizen у объекта, с которым столкнулись
        if (other.TryGetComponent(out MoveCitizen moveCitizen))
        {
            // Если игрок занят, выходим из метода
            if (isBusy)
                return;

            // Сбрасываем ссылку на гражданина, который вышел из триггера
            moveCitizenArrest = null;
        }
    }
}