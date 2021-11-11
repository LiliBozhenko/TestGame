using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMoving : MonoBehaviour
{
    //переменная хранения скорости перемещения объекта 
    public float speed;
    private void Update()
    {
        //перемещение объекта по вертикальной плоскости 
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
