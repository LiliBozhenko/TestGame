using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAll : MonoBehaviour
{
    private BoxCollider2D _boundare_Collider;  //ссылка на бокс колайдер 2д 
    private Vector2 _viewport_Size;   //переменная вектор2 для угла камеры
    private void Awake()
    {
        _boundare_Collider = GetComponent<BoxCollider2D>();  //настройка ссылки на бк2l
    }

    private void Start()
    {
        ResizeColider();  //метод для расчета бк2д
    }
        void ResizeColider()  //расчет бк2д под любой экран, автоматически
    {
        _viewport_Size = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) * 2; //умножение верхнего правого угла камеры на 2

        _viewport_Size.x *= 1.5f; //умножение ширины и высоты на 1.5
        _viewport_Size.y *= 1.5f;
        _boundare_Collider.size = _viewport_Size;  //изменение размеров бк2д с помощью расчетов
    }
    public void OnTriggerExit2D(Collider2D coll) //логика для объектов покидающие бк2д (уничтожение)
    {
        switch (coll.tag)
        {
            case "Planet":
                Destroy(coll.gameObject);
                break;
            case "Bullet":
                Destroy(coll.gameObject);
                break;
        }
    }                    


}
