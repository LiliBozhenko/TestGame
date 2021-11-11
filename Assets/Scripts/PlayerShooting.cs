using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Guns //отвечает за стрельбу
{
    public GameObject obj_Central_Gun, obj_Right_Gun, obj_Left_Gun;
    public ParticleSystem ps_Central_Gun, ps_Right_Gun, ps_Left_Gun;
}

public class PlayerShooting : MonoBehaviour
{
    public static PlayerShooting instance; //ссылка на игрока
    public Guns guns;  //ссылка на класс
    [HideInInspector]  //переменная для создания режима стрельбы
    public int max_Power_Level_Guns = 5;
    public GameObject obj_Bullet;   //переменная для хранения пуль
    public float time_Bullet_Spawn = 0.3f;   //задержка между выстрелами игрока
    [HideInInspector]
    public float timer_Shot; //переменная для создания таймера выстрела
    [Range(1, 5)]
    public int cur_Power_Level_Guns = 1; //переменная для хранения текущих режимов стрельбы 

    private void Awake()
    {
        if (instance == null)   //настрйока ссылки на игрока
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() //ссылка на систему частиц(комнепоненты орудий игрока)
    {
        guns.ps_Central_Gun = guns.obj_Central_Gun.GetComponent<ParticleSystem>();
        guns.ps_Left_Gun = guns.obj_Left_Gun.GetComponent<ParticleSystem>();
        guns.ps_Right_Gun = guns.obj_Right_Gun.GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        //используя таймер запуск метода MakeAshot
        if (Time.time > timer_Shot )
        {
            timer_Shot = Time.time + time_Bullet_Spawn;
            MakeAShot();
        }
    }
    private void CreateBullet(GameObject bullet, Vector3 position_Bullet, Vector3 rotation_Bullet)   //метод создания пуль
    {
        Instantiate(bullet, position_Bullet, Quaternion.Euler(rotation_Bullet)); 
    }
    
    private void MakeAShot() //написание метода MakeAshot, создает режим стрельбы, в зависимости от выбора 
    {
        switch (cur_Power_Level_Guns)
        {
            case 1:  //первый режим стрелбы
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                guns.ps_Central_Gun.Play(); //активация вспышки

                break;
            case 2:  //второй режим стрелбы
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, Vector3.zero);
                guns.ps_Right_Gun.Play();
                guns.ps_Left_Gun.Play();
                break;
            case 3:  //третий режим стрелбы
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, -5));
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 5));
                guns.ps_Right_Gun.Play();
                guns.ps_Left_Gun.Play();
                guns.ps_Central_Gun.Play();
                break;
            case 4:  //четвертый режим стрелбы
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, 0));
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, 5));
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 0));
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, -5));
                guns.ps_Right_Gun.Play();
                guns.ps_Left_Gun.Play();
                guns.ps_Central_Gun.Play();
                break;
            case 5:  //пятый режим стрелбы, режим веера
                CreateBullet(obj_Bullet, guns.obj_Central_Gun.transform.position, Vector3.zero);
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, -5));
                CreateBullet(obj_Bullet, guns.obj_Right_Gun.transform.position, new Vector3(0, 0, -15));
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 5));
                CreateBullet(obj_Bullet, guns.obj_Left_Gun.transform.position, new Vector3(0, 0, 15));
                guns.ps_Right_Gun.Play();
                guns.ps_Left_Gun.Play();
                guns.ps_Central_Gun.Play();
                break; 
        }
    }

}
