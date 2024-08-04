using UnityEngine;


public class Jail : MonoBehaviour
{
    public Transform PlayerTransform; // Ссылка на трансформ игрока
    public bool isInstall = false; // Флаг установки тюрьмы
    public static Jail Instance; // Статический экземпляр тюрьмы
    private void Awake()
    {
        if (!Instance)
            Instance = this; // Устанавливаем текущий экземпляр как единственный
        else if (Instance != this)
            Destroy(gameObject); // Уничтожаем объект, если уже существует другой экземпляр
    }

    public void PloshadkaSetup()
    {
        if (PlayerFoundLost.Instance.IsFound) // Если игрок найден
        {
            Player.Instance.transform.localPosition = new Vector3(0, 0, 0); // Устанавливаем позицию игрока в начальную точку
            transform.position = PlayerTransform.position - new Vector3(0, 0, 1f); // Устанавливаем позицию тюрьмы с учетом смещения
            transform.rotation = PlayerTransform.rotation; // Устанавливаем поворот тюрьмы как у игрока
            isInstall = true; // Устанавливаем флаг установки тюрьмы в true
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out MoveCitizen moveCitizen)) // Если другой объект имеет компонент MoveCitizen
        {
            if (moveCitizen.isArrested) // Если гражданин арестован
            {
                Player.Instance.isBusy = false; // Устанавливаем флаг занятости игрока в false

                if (moveCitizen.Stealing) // Если гражданин ворует
                    GameManager.Instance.Schet(+1); // Увеличиваем счетчик воровства
                else
                    GameManager.Instance.Schet(-1); // Уменьшаем счетчик воровства
                Spawner.Citizens.Remove(moveCitizen); // Удаляем гражданина из списка
                Destroy(moveCitizen.gameObject); // Уничтожаем объект гражданина

                if (Spawner.Citizens.Count == 0) // Если больше нет граждан
                {
                    GameManager.Instance.GameOver(); // Вызываем метод завершения игры
                }
            }
        }
    }



}
