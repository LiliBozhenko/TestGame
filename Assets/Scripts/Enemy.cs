using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int enemy_Health; //переменная для хранения жизней врага
    public int score_Value;  //переменная для настройки кол-ва очков за разрушенике врага
    [Space]
    
    public GameObject obj_Bullet;//переменная для хранения пули врага
    public float shot_Time_Min, shot_Time_Max; //интервал времени, в течении  которого производится выстрел 
    public int shot_Chance;//шанс выстрела, что бы стреляли не все враги на поле одновременно

    [Header("Boss")]
    public bool is_Boss; //если истинна - босс, если ложь - обычный враг
    public GameObject obj_Bullet_Boss;   //переменная для хранения пулей босса
    public float time_Bullet_Boss_Spawn;  //переменная для задержки выстрелов
    private float _timer_Shot_Boss;   //переменная для создания таймера
    public int shot_Chance_Boss;  //шанс выстрела


        //заддержка между выстрелами босса
    private void Start()
    {
        if (!is_Boss) //если враг не является боссом, то делаем только 1 выстрел и всё
        {
            Invoke("OpenFire", Random.Range(shot_Time_Min, shot_Time_Max)); //вызов OpenFire который будет вызываться в случайный промежуток времени в течении  интервала
        }
    }

    private void Update()
    {
        if (is_Boss) //если враг является боссом, то исп. 2 метода(вида) стрельбы:
        {
            //openFire
            //openFireBoss
            if (Time.time > _timer_Shot_Boss)
            {
                _timer_Shot_Boss = Time.time + time_Bullet_Boss_Spawn;
                OpenFire();
                OpenFireBoss();
            }
        }

    }
    private void OpenFireBoss() //метод OpenFireBoss();
    {
      if (Random.value < (float)shot_Chance_Boss / 100)  //условие на шанс выстрела 
        {
           for (int zZz = -40; zZz < 40; zZz +=10)  //создания выстрела используя цикл, меняя их траекторию движения по оси Z 
            {
                Instantiate(obj_Bullet_Boss, transform.position, Quaternion.Euler(0, 0, zZz));
            }
        }
    }

    private void OpenFire()  //метод OpenFire 
    {
      if(Random.value < (float)shot_Chance / 100) //проверка шанса выстрела
        {
            Instantiate(obj_Bullet, transform.position, Quaternion.identity);  //если мы делаем выстрел - создается пуля с позиции врага
        }
    }
    public void GetDamage(int damage) //метод отвечающий за получение урона врагом
    {
        enemy_Health -= damage; //уменьшение очков жизни на кол-во полученного урона
        if(enemy_Health <= 0) //если жизни закончились - вызов метода уничтожения врага
        {
            Destruction();
        }
    }  
    private void Destruction() //метод разрушения врага
    {
        LevelController.instance.ScoreInGame(score_Value);  //если враг разрушается - он передает данные в метод ScoreInGame и засчитывается как очки
        Destroy(gameObject); //уничтожени е объекта при обращении к методу
    }
    private void OnTriggerEnter2D(Collider2D coll) //действие при столкновении врага с игроком 
    {
        if (coll.tag == "Player")
        {
            GetDamage(1);
            Player.instance.GetDamage(1); //МЕТОД ПОВРЕЖДЕНИЯ ИГРОКА ПРИ СТОЛКНОВЕНИИ С ВРАГОМ
        }
    }
}
