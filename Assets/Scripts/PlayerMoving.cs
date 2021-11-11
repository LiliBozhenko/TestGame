using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//класс не дающий игроку выходить за пределы экрана, виден в инспекторе
[System.Serializable]
public class Borders
{
    //границы сторон
    public float minX_Offset = 1.1f; //правая граница
    public float maxX_Offset = 1.1f; //нижняя
    public float minY_Offset = 1.1f; //верхняя
    public float maxY_Offset = 1.1f; //ограничения для расчетов, скрыты от инспектора
                                     //публичные поля, скрытые от инспектора, ограничивающие перемещения игрока
    [HideInInspector]
    public float minX, maxX, minY, maxY;
}
public class PlayerMoving : MonoBehaviour
{
    public static PlayerMoving instance;  //Ссылка на игрока, для последующего изменения скорости из других скриптов
    public Borders borders; //ссылка на класс
    public int speed_Player = 5;  // переменная для хранения скорости игрока
    private Camera _camera; //приватная ссылка на камеру для взаимойдействия с экраном
    private Vector2 _mouse_Position; //переменная хранения 2д координат

    private void Awake()
    {
        //ссылка на игрока при отсутсвии объектов
        if (instance == null)
        {
            instance = this;
        }
        else

        {
            Destroy(gameObject);
        }

        _camera = Camera.main;
    }
    private void Start()
    {
        ResizeBorders(); //вызов метода расчета границ
    }
    private void Update()
    {
        if (Input.GetMouseButton(0))   //условие проверки нажатия левой кнопки мыши по экрану
        {
            _mouse_Position = _camera.ScreenToWorldPoint(Input.mousePosition); //запись места нажатия по экрану
            transform.position = Vector2.MoveTowards(transform.position, _mouse_Position, speed_Player * Time.deltaTime);  //перемещение игрока по экрану
        }

        //закрытие созданных границ экрана для игрока
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, borders.minX, borders.maxX),
                                         Mathf.Clamp(transform.position.y, borders.minY, borders.maxY));
    }

    //создание границ используя значения камеры
    //используя значения отступов и камеры (обращаясь к классу)
    private void ResizeBorders()
    {
        borders.minX = _camera.ViewportToWorldPoint(Vector2.zero).x + borders.minX_Offset;
        //левая граница
        borders.minY = _camera.ViewportToWorldPoint(Vector2.zero).y + borders.minY_Offset;
        //нижняя
        borders.maxX = _camera.ViewportToWorldPoint(Vector2.right).x - borders.maxX_Offset;
        //правая
        borders.maxY = _camera.ViewportToWorldPoint(Vector2.up).y - borders.maxY_Offset;
        //вверхняя
    }
}