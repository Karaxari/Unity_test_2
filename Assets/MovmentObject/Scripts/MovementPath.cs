using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    public enum PathTypes // виды пути: линейный или циклический
    {
        linear,
        loop
    }

    public PathTypes PathType; // определет тип пути
    public int movementDirection = 1; // направление движения: вперед или назад
    public int moveingTo = 0; // к какой точке двигаться
    public Transform[] PathElements; // массив из точек двиения

    public void OnDrawGizmos() // отображает линии между точками пути
    {
        if(PathElements == null || PathElements.Length < 2) // проверка есть ли хотя бы 2 элемента пути
        {
            return;
        }

        for(var i = 1; i<PathElements.Length; i++) // прогоняет все точки массива
        {
            Gizmos.DrawLine(PathElements[i - 1].position, PathElements[i].position); // рисует линии между ними
        }

        if(PathType == PathTypes.loop) // рисование циклического пути
        {
            Gizmos.DrawLine(PathElements[0].position, PathElements[PathElements.Length - 1].position);
        }
    }

    public IEnumerator<Transform> GetNextPathPoint() // получает положение следующей точки
    {
        if (PathElements == null || PathElements.Length < 1) // проверяет, есть ли точки которым нужно проверять положение
        {
            yield break; // позволет выйти из коротина если нашел несоответсвтвие
        }

        while (true)
        {
            yield return PathElements[moveingTo]; // возвращает текущее положение точки

            if(PathElements.Length == 1) // если точка всего одна, выйти
            {
                continue;
            }

            if(PathType == PathTypes.linear) // если линия не зациклена
            {
                if(moveingTo <=0) // если двигаемся по нарастающей 
                {
                    movementDirection = 1; // изменение направления движения
                }
                else if (moveingTo >= PathElements.Length - 1) // если двигаемя по убывающей
                {
                    movementDirection = -1; // изменение направления движения
                }
            }

            moveingTo = moveingTo + movementDirection; // диапазон движения от 1 до -1

            if(PathType == PathTypes.loop) // если линия циклическая
            {
                if (moveingTo >= PathElements.Length) // если мы дощли до последней точки
                {
                    moveingTo = 0; // то надо идти не в обратную сторону, а к первой точке 
                }
                
                if(moveingTo < 0) // если мы дошли до первой точки двигаясь в обратную сторону
                {
                    moveingTo = PathElements.Length - 1; // то надо двинуть к последней точке
                }
            }
        }
    }
}
