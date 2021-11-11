using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShootingSetting
{
    [Range(0, 100)] //переменная для настрйоки шанса стрельбы врага в волне
    public int shot_Change; //интервал внутри которого происходит выстрел
    public float shot_Time_Min, shot_Time_Max;
}

public class Wave : MonoBehaviour 
{
    public ShootingSetting shooting_Setting; //cсылка на класс ShootingSetting
    [Space] //отступ в инспекторе
    public GameObject obj_Enemy; //переменная для хранения врага в волне 
    public int count_in_Wave;//кол-во врагов в волне
    public float speed_Enemy;//скорость движения врагов в волне
    public float time_Spawn; //задержка между генерацией врагов
    public Transform[] path_Points; //массив для хранения точек движения волны
    public bool is_return;//логическая переменная, враг достигнув последней точки либо уничтожен, либо начнет путь с первой точки пути 

    //Test Wave
    //если истинна - генерация врага каждые 5 сек
    [Header("Test wave!")]
    public bool is_Test_Wave;

    private FollowThePath follow_Component; 
    private Enemy enemy_Component_Script; //переменная для передачи данных врагу

    private void Start()
    {
        //запуск программы генерации волны
        StartCoroutine(CreateEnemyWave());
    }

    IEnumerator CreateEnemyWave()
    {
        //цикл который зависит от кол-ва врагов в волне
        for (int i = 0; i < count_in_Wave; i++)
        {
            //создание и помещения врага в new_enemy
            GameObject new_enemy = Instantiate(obj_Enemy, obj_Enemy.transform.position, Quaternion.identity);
            follow_Component = new_enemy.GetComponent<FollowThePath>(); //ссылка на компонент FollowThePath 
            follow_Component.path_Points = path_Points;//передача через ссылку точки пути перемещения врага 
            follow_Component.speed_Enemy = speed_Enemy; //передача через ссылку скорость перемещения врага 
            follow_Component.is_return = is_return;//передача через ссылку значение логической переменной. истинна - движения бесконечно, ложь - уничтожение в конце пути

            enemy_Component_Script = new_enemy.GetComponent<Enemy>(); //ссылка на комнент Enemy
            enemy_Component_Script.shot_Chance = shooting_Setting.shot_Change; //передача через ссылку шанса выстрела
            enemy_Component_Script.shot_Time_Min = shooting_Setting.shot_Time_Min;   //передача интервала внутри которого будет происходить выстрел
            enemy_Component_Script.shot_Time_Max = shooting_Setting.shot_Time_Max;
            new_enemy.SetActive(true);
            //задержка перед созданием нового врага 
            yield return new WaitForSeconds(time_Spawn);
        }
        //если логическая переменная истинна - 5 сек и запускаем волну 
        if (is_Test_Wave)
        {
            //если логическая переменная истинна - 5 сек и запускаем волну
            yield return new WaitForSeconds(5f);
            StartCoroutine(CreateEnemyWave());
        }

        //если ложь - враг уничтожен в конце пути 
        if (!is_return)
            Destroy(gameObject);
    }
    private void OnDrawGizmos() //соединения линиями точек пути, по которым движется волна для настройки 
    {
        NewPositionByPath(path_Points);
    }
    void NewPositionByPath(Transform[] path) //сглаживание линий движения врага
    {
        Vector3[] path_Positions = new Vector3[path.Length];
        for (int i = 0; i < path.Length; i++)
        {
            path_Positions[i] = path[i].position;
        }
        path_Positions = Smoothing(path_Positions);
        path_Positions = Smoothing(path_Positions);
        path_Positions = Smoothing(path_Positions); 
        for (int i = 0; i < path_Positions.Length - 1; i++)
        {
            Gizmos.DrawLine(path_Positions[i], path_Positions[i + 1]);
        }
    }
    Vector3[] Smoothing(Vector3[] path_Positions)
    {
        Vector3[] new_Path_Positions = new Vector3[(path_Positions.Length - 2) * 2 + 2];
        new_Path_Positions[0] = path_Positions[0];
        new_Path_Positions[new_Path_Positions.Length - 1] = path_Positions[path_Positions.Length - 1];

        int j = 1;
        for (int i = 0; i < path_Positions.Length - 2; i++)
        {
            new_Path_Positions[j] = path_Positions[i] + (path_Positions[i + 1] - path_Positions[i]) * 0.75f;
            new_Path_Positions[j + 1] = path_Positions[i + 1] + (path_Positions[i + 2] - path_Positions[i + 1]) * 0.25f;
            j += 2;
        }
        return new_Path_Positions;
    }

}
