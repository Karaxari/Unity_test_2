using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform panel;
    public GameObject[] allCars = null;
    public PathCreation.PathCreator path_1;
    public PathCreation.PathCreator path_2;
    public PathCreation.EndOfPathInstruction endOfPathInstruction;
    public float speed = 50;
    public float distance = 15f;
    public int numStep = 10;
    public int countCar = 10;
    public int countClone = 3;

    int [] allIndexCars = null;

    List<GameObject> allObject = new List<GameObject>();
    List<GameObject> rightObject = new List<GameObject>();
    List<GameObject> leftObject = new List<GameObject>();
    List<GameObject> queueÑars = new List<GameObject>();
    List<GameObject> allObjectCars = new List<GameObject>();
    //[RequireComponent(typeof("PathFollower"))]
    // Start is called before the first frame update
    void Start()
    {
        allIndexCars = new int[allCars.Length];
        for (int i = 0; i<allCars.Length - 1; i++)
        {
            allIndexCars[i] = i;
        }
        allIndexCars = ShuffleArray(allIndexCars);

        for (int i = 0; i < countCar; i++)
        {
            int index = allIndexCars[i];
            for (int j = 0; j < countClone; j++)
            {
                GameObject car = Instantiate(allCars[index], allCars[index].transform.position, Quaternion.identity);
                car.name = allCars[index].name + "_" + j.ToString();
                car.transform.parent = panel.transform;
                car.transform.position = new Vector3(panel.position.x, panel.position.y, panel.position.z - 40);
                car.transform.Rotate(car.transform.rotation.x, car.transform.rotation.y + 90, car.transform.rotation.z, Space.World);
                car.AddComponent<PathCreation.Examples.PathFollower>();
                car.GetComponent<PathCreation.Examples.PathFollower>().pathCreator = null;
                car.GetComponent<PathCreation.Examples.PathFollower>().endOfPathInstruction = endOfPathInstruction;
                car.GetComponent<PathCreation.Examples.PathFollower>().speed = speed;
                car.GetComponent<PathCreation.Examples.PathFollower>().distance = distance;
                car.GetComponent<PathCreation.Examples.PathFollower>().numStep = numStep;
                allObjectCars.Add(car);
            }
            GameObject car_ = Instantiate(allCars[index], allCars[index].transform.position, Quaternion.identity);
            car_.transform.localScale = new Vector3(25f, 25f, 25f);
            allObject.Add(car_);
        }

        foreach (GameObject obj in allObject)
        {
            int r = Random.Range(0, 2);
            Debug.Log("Right 0, Left 1: " + r.ToString());
            switch (r) {
                case 0: rightObject.Add(obj); break;
                case 1: leftObject.Add(obj); break;
            }

        }

        spavnObject();

        //GameObject cube = Instantiate(allCars[0], allCars[0].transform.position, Quaternion.identity);
        //cube.transform.position = new Vector3(0f, 0f, 0f);
        //cube.transform.Rotate(cube.transform.rotation.x, cube.transform.rotation.y + 90, cube.transform.rotation.z, Space.World);
        //cube.AddComponent<PathCreation.Examples.PathFollower>();
        //cube.GetComponent<PathCreation.Examples.PathFollower>().pathCreator = path_1;
        //cube.GetComponent<PathCreation.Examples.PathFollower>().endOfPathInstruction = endOfPathInstruction;
        //cube.GetComponent<PathCreation.Examples.PathFollower>().speed = speed;


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveRight();
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveLeft();
        }
    }

    void spavnObject()
    {
        int n = 0;
        foreach (GameObject obj in rightObject)
        {
            obj.transform.position = new Vector3(panel.position.x + 9, panel.position.y + n, panel.position.z + 30);
            obj.transform.Rotate(obj.transform.rotation.x, obj.transform.rotation.y + 45, obj.transform.rotation.z, Space.World);
            n++;
        }

        n = 0;
        foreach (GameObject obj in leftObject)
        {
            obj.transform.position = new Vector3(panel.position.x - 9, panel.position.y + n, panel.position.z + 30);
            obj.transform.Rotate(obj.transform.rotation.x, obj.transform.rotation.y + 135, obj.transform.rotation.z, Space.World);
            n++;
        }

        for (int i = 0; i < countClone; i++)
        {
            allObjectCars[i].transform.position = new Vector3(panel.position.x, panel.position.y, panel.position.z + 40 + (i * distance));
            queueÑars.Add(allObjectCars[i]);
        }
    }

    void addCar()
    {
        //int rand = Random.Range(0, countCar - 1);
        //GameObject car = Instantiate(allCars[rand], allCars[rand].transform.position, Quaternion.identity);
        //car.transform.parent = panel.transform;
        //car.transform.position = new Vector3(panel.position.x, panel.position.y, panel.position.z + 40 + ((countCar - 1) * distance));
        //car.transform.Rotate(car.transform.rotation.x, car.transform.rotation.y + 90, car.transform.rotation.z, Space.World);
        //car.AddComponent<PathCreation.Examples.PathFollower>();
        //car.GetComponent<PathCreation.Examples.PathFollower>().pathCreator = null;
        //car.GetComponent<PathCreation.Examples.PathFollower>().endOfPathInstruction = endOfPathInstruction;
        //car.GetComponent<PathCreation.Examples.PathFollower>().speed = speed;
        //car.GetComponent<PathCreation.Examples.PathFollower>().distance = distance;
        //car.GetComponent<PathCreation.Examples.PathFollower>().numStep = numStep;
        //allObject.Add(car);
        

        Debug.Log("Size count allObjectCars: " + allObjectCars.Count);
        int rand = Random.Range(0, allObjectCars.Count);
        while (queueÑars.Contains(allObjectCars[rand]))
        {
            Debug.Log(allObjectCars[rand].name + " Óæå åñòü â î÷åðåäè!");
            rand = Random.Range(0, allObjectCars.Count);
        }

        
        queueÑars.RemoveAt(0);

        //GameObject car = allObjectCars[rand];
        
        allObjectCars[rand].transform.position = new Vector3(panel.position.x, panel.position.y, panel.position.z + 40 + ((countClone - 1) * distance));
        allObjectCars[rand].transform.rotation = Quaternion.Euler(0.0f, 90f, 0.0f);
       
        allObjectCars[rand].GetComponent<PathCreation.Examples.PathFollower>().pathCreator = null;
        allObjectCars[rand].GetComponent<PathCreation.Examples.PathFollower>().endOfPathInstruction = endOfPathInstruction;

        queueÑars.Add(allObjectCars[rand]);
        Debug.Log(allObjectCars[rand].name);
        Debug.Log("Pocition: " + allObjectCars[rand].transform.position.x.ToString() + " " + allObjectCars[rand].transform.position.z.ToString());
        Debug.Log(queueÑars[0].name);
    }

    void moveRight()
    {
        Debug.Log("Queue size: " + queueÑars.Count.ToString());
        //Debug.Log(queueÑars[0].name);
        if (queueÑars.Count > 0)
        {
            queueÑars[0].GetComponent<PathCreation.Examples.PathFollower>().pathCreator = path_1;
            for (int i = 1; i<queueÑars.Count; i++)
            {
                queueÑars[i].GetComponent<PathCreation.Examples.PathFollower>().moveCar();
            }
            addCar();        }
    }

    void moveLeft()
    {
        Debug.Log("Queue size: " + queueÑars.Count.ToString());
        //Debug.Log(queueÑars[0].name);
        if (queueÑars.Count > 0)
        {
            queueÑars[0].GetComponent<PathCreation.Examples.PathFollower>().pathCreator = path_2;
            for (int i = 1; i < queueÑars.Count; i++)
            {
                queueÑars[i].GetComponent<PathCreation.Examples.PathFollower>().moveCar();
            }
            addCar();
        }
    }
    

    public void checkResult(int num)
    {
        switch (num)
        {
            case 0: moveRight(); break;
            case 1: moveLeft(); break;
        }
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }
}
