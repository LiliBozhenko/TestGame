using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //переменная через которую регулируется урон пули
    public int damage;
    //логическая переменная, через которую определяется чья это пуля (игрока или врага)
    public bool is_Enemy_Bullet;

    //метод уничтожения пути. вызов метода - разрушение пули
    private void Destruction()
    {
       Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D coll)  //логика столкновения пули с врагом\игроком
    {
        //если пуля принадлежит врагу и сталкивается с игркоом - ему наносится урон
             if(is_Enemy_Bullet && coll.tag == "Player")
        {
            Player.instance.GetDamage(damage);
            //после столкнове6ния вызов метода разрушения пули 
            Destruction();
        }
             //если пуля игрока столкнулась с врагом - вызывается копонент Enemy и  метод поражения врага 
             else if(!is_Enemy_Bullet && coll.tag == "Enemy")
        {
            coll.GetComponent<Enemy>().GetDamage(damage);
            //метод разрушения пули
            Destruction();
        }
            

        else if (is_Enemy_Bullet && coll.tag == "Shield") //если пуля сталкивается со щитом игрока..  
        {
            Player.instance.GetDamageShield(damage);//вызов метода  повреждение щита игрок
            Destruction(); //разрушение пули после столкновения
        }
    } 
}
