using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Класс GameManager отвечает за управление состоянием игры
public class GameManager : MonoBehaviour
{
    // Переменная для хранения здоровья (или очков)
    public int health = 0;

    // Ссылки на текстовые элементы для отображения счета и финального результата
    public TMP_Text Score;
    public TMP_Text Final;

    // Статическая переменная для реализации паттерна Singleton
    public static GameManager Instance;

    private void Awake()
    {
        // Проверяем, существует ли уже экземпляр GameManager
        if (!Instance)
            Instance = this; // Если нет, устанавливаем текущий экземпляр
        else if (Instance != this)
            Destroy(gameObject); // Если существует, уничтожаем текущий объект
    }

    private void Start()
    {
        // Инициализируем здоровье и обновляем текст счета
        health = 0;
        Score.text = health.ToString();
    }

    // Метод для выхода из игры
    public void Exit()
    {
        Application.Quit(); // Закрывает приложение
    }

    // Метод для перезапуска игры
    public void Restart()
    {
        SceneManager.LoadScene("Game"); // Загружает сцену с именем "Game"
    }

    // Метод для увеличения счета на заданное количество
    public void Schet(int count)
    {
        health += count; // Увеличиваем здоровье на count
        Score.text = health.ToString(); // Обновляем текст счета
    }

    // Метод, вызываемый при окончании игры
    public void GameOver()
    {
        // Проверяем, есть ли очки
        if (health > 0)
        {
            Final.gameObject.SetActive(true); // Активируем объект с финальным результатом
            Final.text = $"Ты поймал {health} воров из 6."; // Отображаем количество пойманных воров
        }
        else
        {
            Final.gameObject.SetActive(true); // Активируем объект с финальным результатом
            Final.text = $"Надо ловить только преступников!"; // Сообщение, если игрок не поймал никого
        }
    }
}