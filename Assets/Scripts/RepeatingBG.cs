using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBG : MonoBehaviour
{
    //переменная для хранения спрайта(картинка, фон) в пикселях.
    public float vertical_Size;
    //!!высота изображения должна быть выше камеры
    private Vector2 _offSet_Up; //поднятие спрайта

    private void Update()
    {
        
       if (transform.position.y < - vertical_Size)  //условие находится ли спрайт выше своей высоты 
        {
            RepeatBackground(); //вызов метода
        }
    }

    void RepeatBackground()         //метод перемещения спрайтов друг за другом (бессконечный фон)
    {
        //расчет смещения для переменной (в зависимости от кол-ва спрайтов)
        _offSet_Up = new Vector2(0, vertical_Size * 2f);
        //создание новой позиции 
        transform.position = (Vector2)transform.position + _offSet_Up;
    }
}
