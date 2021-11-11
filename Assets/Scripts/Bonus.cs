using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) //что будет с объектом с которым столкнулся игрок
    {
        if (collision.tag == "Player") //если объект столкнулся с игроком..
        {
           if (PlayerShooting.instance.cur_Power_Level_Guns < PlayerShooting.instance.max_Power_Level_Guns) //проверка текущего уровня стрельбы
            {
                PlayerShooting.instance.cur_Power_Level_Guns++; //проверка уровня стрельбы игрока, если он не максимальный - смена режима стрельбы
            }
            Destroy(gameObject); //уничтожение бонуса
        }
        
    }
}
