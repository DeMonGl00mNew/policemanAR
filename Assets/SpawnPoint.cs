using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс SpawnPoint отвечает за управление состоянием точки спавна
public class SpawnPoint : MonoBehaviour
{
    // Переменная, указывающая, пуста ли точка спавна
    public bool isEmpty = true;

    // Метод вызывается при входе другого объекта в триггер
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, имеет ли объект компонент MoveCitizen
        if (other.TryGetComponent(out MoveCitizen moveCitizen))
            // Если да, то точка больше не пуста
            isEmpty = false;
    }

    // Метод вызывается при выходе другого объекта из триггера
    private void OnTriggerExit(Collider other)
    {
        // Проверяем, имеет ли объект компонент MoveCitizen
        if (other.TryGetComponent(out MoveCitizen moveCitizen))
            // Если да, то точка снова пуста
            isEmpty = true;
    }
}