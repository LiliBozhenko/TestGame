using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //ссылка на игрока для его взаимодействия с окружением и магазином 
    public static Player instance = null;
    //переменная для хранения очков жизни игрока
    public int player_Health = 1;

    public GameObject obj_Shield;  //ссылка на объект щит игрока
    public int shield_Health = 1;  //перменная для хранения очков жизни щита

    private Slider _slider_hp_Player; //ссылка на ползунок жизни игрока
    private Slider _slider_hp_Shield; //ссылка на ползунок жизней щита
    private void Awake()
    {
        //настройка ссылки на самого себя 
        if (instance == null) 
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _slider_hp_Player = GameObject.FindGameObjectWithTag("sl_HP").GetComponent<Slider>();   //нахождение объекта с тегом игрока и выбор его слайдер компонента
        _slider_hp_Shield = GameObject.FindGameObjectWithTag("SL_Shield").GetComponent<Slider>();  //нахождение объекта с тегом щита и выбор его слайдер компонента                                                                                     //нахождение объекта с тегом игрока и выбор его слайдер компонента                                                                                        //нахождение объекта с тегом щита и выбор его слайдер компонента
    }


    private void Start()
    {
        //установка ползунка равному очкам жизни игрока, деленное на 10
        _slider_hp_Player.value = (float)player_Health / 15;

        //делаем щит видимым, если у него есть очки жизни
        if (shield_Health !=0)
        {
            obj_Shield.SetActive(true);
            _slider_hp_Shield.value = (float)shield_Health / 6;
        }
        //скрытие щита, если нет очков жизни
        else
        {
            obj_Shield.SetActive(false);
        }
    }
    
     public void GetDamageShield(int damage)    //метод получения урона щита
    {
        //уменьшаем очки жизни щита на кол-во полученного урона
        shield_Health -= damage;

        _slider_hp_Shield.value = (float)shield_Health / 10;   //после обновления очков щита - обновление значения ползунка

        if (shield_Health <=0)   //если у щита нет очков жизни..
        {
            obj_Shield.SetActive(false); //скрываем его 
        }
        
    }

    //Метод получения урона игроком 
    public void GetDamage(int damage)
    {
        //уменьшение кол-во жизней игрока на очки полученного урона
        player_Health -= damage;

        _slider_hp_Player.value = (float)player_Health / 10; //после обновления очков игрока - обновление значения ползунка


        //если у игрока нет жизней - вызов метода разрушения игрока 
        if (player_Health <=0)
        {
            Destruction();
        }
    }
    //метод разрешения игрока
    void Destruction()
    {
        //если метод вызывается - игрок уничтожается 
        Destroy(gameObject);
    }

}
