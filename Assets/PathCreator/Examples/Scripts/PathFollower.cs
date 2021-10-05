using System.Collections;
using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;
        public float distance = 25f;
        public int numStep = 10;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                transform.Rotate(transform.rotation.x, transform.rotation.y + 270, transform.rotation.z, Space.World);
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

        public void moveCar()
        {
            StartCoroutine(movingCar());
        }

        IEnumerator movingCar()
        {
            float cof = distance / numStep;
            float sum = cof;
            for (int i = 0; i < numStep; i++)
            {
                //Debug.Log("Коротина: " + i);
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - cof);
                yield return null;
            }
            //yield return null;
            // Покупаем яхты / пароходы
            // Сопрограмма завершается
        }
    }
}