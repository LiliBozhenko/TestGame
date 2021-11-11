using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetsAndBonus : MonoBehaviour
{
    public GameObject obj_Bonus; //переменная для хранения прибафа бонуса
    public float time_Bonus_Spawn; //задержка между генерацией бонусов
    public GameObject[] obj_Planets; //массив генерации планет
    public float time_Planet_Spawn; //задержка между генерацией планет
    public float speed_Planets;    //переменная сохранения скорости планет
    List<GameObject> planetsList = new List<GameObject>(); //список запрещающий дублирование планет

    private void Start()
    {
        StartCoroutine(BonusCreation()); //запуск программы генерации бонусов
        StartCoroutine(PlanetsCreation()); //генерация планет
    }
    IEnumerator BonusCreation() //создание бонусов 
    {
        while (true)
        {
            yield return new WaitForSeconds(time_Bonus_Spawn); //задержка выполнения кода
            Instantiate(obj_Bonus, new Vector2(Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX),
                PlayerMoving.instance.borders.maxY * 1.5f), Quaternion.identity);//создание бонусов, с ограничением движения игрока и видимостей камеры
        }
    }
    IEnumerator PlanetsCreation()
    {
       for ( int i = 0; i < obj_Planets.Length; i++) //добавление планет в список, с помощью цикла
        {
            planetsList.Add(obj_Planets[i]);
        }

        yield return new WaitForSeconds(7);//запуск кода после 7 секунд после начала игры
       while (true)  //создание планет в бесконечном цикле
        {
            int randomIndex = Random.Range(0, planetsList.Count); //выбор случайной планеты из списка
             GameObject newPlanet = Instantiate(planetsList[randomIndex],
             new Vector2(Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX),
             PlayerMoving.instance.borders.maxY * 2f),     //плавное появление планеты, с учетом размера экрана
             Quaternion.Euler(0, 0, Random.Range(-25, 25))); //случайный наклон планеты от -25 до 25
            planetsList.RemoveAt(randomIndex); //после появления, удаление планеты из списка. во избежание дубликата 
             if (planetsList.Count == 0) //условия повторного заполнения списков 
            {
                for (int i = 0; i < obj_Planets.Length; i++)
                {
                    planetsList.Add(obj_Planets[i]);
                }
            }
            newPlanet.GetComponent<ObjMoving>().speed = speed_Planets; //задает скорость планетам
            yield return new WaitForSeconds(time_Planet_Spawn); //генерация времени повторения кода
        }
    }
                                                            
}
