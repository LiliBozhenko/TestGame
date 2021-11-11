
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class EnemyWaves //класс для взаимодейсвия с волной
{
    public float timeToStart; //время через которое будет создаваться волна
    public GameObject wave; //переменная хранения создаваемой волны
    public bool is_Last_Wave; //логическая переменная, которая отвечает за конец игры. истинна - конец
}

public class LevelController : MonoBehaviour
{
    public static LevelController instance; //ссылка на самого себя
    public GameObject[] playerShip; //массив в котором хранятся все корабли игрока
    public EnemyWaves[] enemyWaves; //массив для вражеских волн
    private bool is_Final = false;  //переменна вызывающая конец игры

    public GameObject panel; //переменная отображения паузы
    private bool _isPause; //логическая переменная состояния паузы
    public GameObject[] btnPause; //массив для хранения кнопок
    public Text text_Score;  //переменная для работы с текстом

    private void Awake() //настройка ссылки на игрок
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start() //создание волн
    {
        Time.timeScale = 1; //что бы не было при выходе и запуске игры замороженно время
        for (int i = 0; i < DataBase.instance.playerShipsInfo.Length; i++) //вызов метода загрузки корабля
        {
            if (DataBase.instance.playerShipsInfo[i][0] == 1) //проверка в каком подмассиве находится 1 в первом элемен
            {
                LoadPlayer(i);
            }
        }

        for (int i = 0; i < enemyWaves.Length; i++) //в данном цикле запускается программа. первое значение - время появление волны. воторое - какой тип волны волны будет создан
        {
            StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart, enemyWaves[i].wave, enemyWaves[i].is_Last_Wave));
        }
    }
    private void Update()  //проверка добедил игрок или проиграл, если не была вызвана пауза
    {

        if (is_Final == true && GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !_isPause)  //если нет объектов с тегом enemy - выиграл 
        {
            Debug.Log("Win");
            GamePause();
            btnPause[1].SetActive(false);
        }
        if (Player.instance == null && !_isPause) //если игрок умер - проиграл
        {
            Debug.Log("Lose");
            GamePause();
        }
    }
    public void ScoreInGame(int score)  //запись для метода записи набранных промежуточных очков, которые могут быть добавлены в основные
    {
        DataBase.instance.Score_Game += score;
        text_Score.text = "Очки " + DataBase.instance.Score_Game.ToString();  //отображение очков в панели игрока
    }
    public void LoadPlayer(int ship) //метод загружающий корабль игрока с DataBase
    {
        Instantiate(playerShip[ship]);
        Player.instance.player_Health = DataBase.instance.playerShipsInfo[ship][2];
        PlayerMoving.instance.speed_Player = DataBase.instance.playerShipsInfo[ship][3];
        Player.instance.shield_Health = DataBase.instance.playerShipsInfo[ship][4];

    }
    public void GamePause() //вызов или скрытие игровой паузы
    {
        if (!_isPause)  //изменение состояние игровой паузы на противоположное
        {
            _isPause = true;
            Time.timeScale = 0; //заморозка времени в паузе
            panel.SetActive(true); //отображение панели
            if (Player.instance != null) //проверка жив ли игрок вовремя вызова паузы (будут отображаться только 2 кнопки)
            {
                btnPause[0].SetActive(false);
                btnPause[1].SetActive(true);
            }
            else //появление игровой паузы если игрок погиб
            {
                btnPause[0].SetActive(true);
                btnPause[1].SetActive(false);
            }
        }
        else  //панель не отображается
        {
            _isPause = false;
            Time.timeScale = 1; //переключение времени на нормальное 
            panel.SetActive(false); //cкрыть панель паузы
        }
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      //Права на данный курс принадлежат Дорофеевой Карине Олеговне, данный курс создавался для Udemy сайта
    public void BtnRestartGame() //код для кнопки Рестарт
    {
        DataBase.instance.Score_Game = 0;  //если нажимается рестарт - обнуляется кол-во промежуточных очков
        Time.timeScale = 1; //при нажатии на кнопку время будет возвращаться в нормальное состояние, которое самостоятельно не сбрасывается
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //загрузка сцены по имени
    }
    public void BthExitGame() //метод для кнопки выхода
    {
        DataBase.instance.SaveGame(); //сохраняются промежуточные очки в основные
        DataBase.instance.GameLoadScene("Menu"); //переход в главное меню игры

    }
    IEnumerator CreateEnemyWave(float delay, GameObject Wave, bool Final)
    {
        if (delay != 0) //если время создание волны не равно нулю, то делает задержку на заданное время 
            yield return new WaitForSeconds(delay);
        if (Player.instance != null) //проверка жив ли игрок, прежде чем создавать волну
            Instantiate(Wave);
        if (Final == true) //если текущая волна будет финальной - передача значения в переменную, отвечающую концу игры
            is_Final = true;
    }
}