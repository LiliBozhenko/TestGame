using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowThePath : MonoBehaviour
{
   [HideInInspector] public Transform[] path_Points; //Массив сохраняющий траекторию движения врага
   [HideInInspector] public float speed_Enemy;  //переменная для сохранения скорости движения врага
   [HideInInspector] public bool is_return;             //после достижения врагом последней точки траектории, он будет либо уничтожен, либо перемещен на первую точку
                                                        //11111111
   [HideInInspector] public Vector3[] _new_Position;      //вектор для хранения точек пути
    //111111111
    private int cur_Pos;  //переменная для хранения порядкового номера точки пути 
    private void Start()
    {
        _new_Position = NewPositionByPath(path_Points);
        transform.position = _new_Position[0]; //отправка врага в начальную точку пути
    }
  
    private void Update()
    {
        //перемещение врага в точку пути с заданной скоростью
        transform.position = Vector3.MoveTowards(transform.position, _new_Position[cur_Pos], speed_Enemy * Time.deltaTime);
        //если враг добрался до точки пути, то к его текущей точки пути добавляется единица 
        if (Vector3.Distance(transform.position,_new_Position[cur_Pos]) <0.2f)
        {
            cur_Pos++;
            //если враг добрался до конечной точки пути и ls_return = true, то он возвращается на первоначальную точку пока не будет уничтожен
            if (is_return && Vector3.Distance(transform.position, _new_Position[_new_Position.Length - 1]) < 0.3f)
                cur_Pos = 0;
        }
        //если враг добрался до конечной точки пути и ls_return = false, то он будет уничтожен
            if ( Vector3.Distance(transform.position, _new_Position[_new_Position.Length - 1]) < 0.2f && !is_return)
        {
            Destroy(gameObject);
        }
    }

    Vector3[] NewPositionByPath(Transform[] pathPos)
    {
        Vector3[] pathPositions = new Vector3[pathPos.Length];
        for (int i = 0; i < path_Points.Length; i++) //помещения сохранения точек пути в массив сохранения векторов
        {
            pathPositions[i] = pathPos[i].position;
        }
        pathPositions = Smoothing(pathPositions);
        pathPositions = Smoothing(pathPositions);
        pathPositions = Smoothing(pathPositions);
        return pathPositions;
    }
    Vector3[] Smoothing(Vector3[] path_Positions) //перемещения врага по сглаженным линиям
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
