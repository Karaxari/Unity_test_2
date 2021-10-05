using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public enum MovementType
    {
        Moveing,
        Lerping
    }

    public MovementType Type = MovementType.Moveing; // вид движения
    public MovementPath MyPath; // используемы путь
    public float speed = 1; // скорость движения
    public float maxDistance = .1f; // на какое расстояние подъехать объекту к точке, чтобы понять что это точка 

    private IEnumerator<Transform> pointInPath; // проверка точек

    void Start()
    {
        if(MyPath == null) // проверка существования пути
        {
            Debug.Log("Применить путь");
            return;
        }

        pointInPath = MyPath.GetNextPathPoint(); // обращение к коротину GetNextPoint
        pointInPath.MoveNext(); // получение следующей точки в пути

        if(pointInPath.Current == null) // есть ли точка к которой передвигаться
        {
            Debug.Log("Нужны точки");
            return;
        }

        transform.position = pointInPath.Current.position; // объект встает на стартовую точку
    }

    void Update()
    {
        if(pointInPath == null || pointInPath.Current == null) // проверка отсутвия пути
        {
            return;
        }

        if(Type == MovementType.Moveing)
        {
            transform.forward = Vector3.RotateTowards(transform.forward, pointInPath.Current.position - transform.position, speed * Time.deltaTime, 0.0f);
            transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, speed * Time.deltaTime);



            //transform.position = Vector3.MoveTowards(transform.position, pointInPath.Current.position, speed * Time.deltaTime); // движение объекта к следующей точке
            
            //Vector3 direction = pointInPath.Current.position - transform.position;
            //Quaternion rotation = Quaternion.LookRotation(direction);
            //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
            
            //float angle = signedAngleBet();

            ////angle = angle - transform.rotation.z;
            //angle = transform.rotation.y + angle;
            //Debug.Log(angle);
            //transform.Rotate(0.0f, angle, 0.0f, Space.World);
        }
        else if(Type == MovementType.Lerping)
        {
            transform.position = Vector3.Lerp(transform.position, pointInPath.Current.position, speed * Time.deltaTime); // движение объекта к следующей точке
            float angle = signedAngleBet();

            //angle = angle - transform.rotation.z;
            angle = transform.rotation.y + angle;
            Debug.Log(angle);
            transform.Rotate(0.0f, angle, 0.0f, Space.World);
        }

        var distanceSqure = (transform.position - pointInPath.Current.position).sqrMagnitude; // проверим, достаточно ли мы близко к точке, чтоб начать двигаться к следующей
        if(distanceSqure < maxDistance * maxDistance) // достаточно ли мы близко, по теореме Пифагора
        {
            pointInPath.MoveNext(); // двигаемя к следющей точке
        }
    }

    float signedAngleBetween()
    {
        // angle in [0,180]
        float angle = Vector3.Angle(transform.position, pointInPath.Current.position);
        float sign = Mathf.Sign(Vector3.Dot(Vector3.forward, Vector3.Cross(transform.position, pointInPath.Current.position)));

        // angle in [-179,180]
        float signed_angle = angle * sign;

        // angle in [0,360] (not used but included here for completeness)
        float angle360 =  (signed_angle + 180) % 360;

        return angle360;
    }

    float signedAngleBet()
    {
        Vector3 targetDir = pointInPath.Current.position - transform.position;
        Vector3 forward = transform.forward;

        float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);
        angle = angle * (-1);

        float angle360 = (angle + 180) % 360;

        return angle360;
    }
}
