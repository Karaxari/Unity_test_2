using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControllerG : MonoBehaviour
{
    public Transform panel;
    public Canvas canvas;
    public int sizePanel = 4; // размер игрвой сетки
    public int numCube = 4; // количество активных кубов
    public float sizeCude = 100f; // размер куба
    public float gapCube = 100f; //удаленность кубов
    public GameObject cubePrefab;
    public Stack<string> stackName = null;

    [SerializeField] private CubeControlG controller;
    [SerializeField] private Material[] materials;
    [SerializeField] private Sprite[] images;
    [SerializeField] GameObject buutonPrefab;

    //private const int sizeArr = sizePanel;
    GameObject[] allObjects = null;
    int[] arrIndex = null;
    public int[] oldArrIndex = null;
    int[] numbers = null;
    int colGood = 0; // счетчик правильнх ответов за ссесию
    int sumGoob = 0; // сумма правильных ответов
    int colBad = 0; // счетчик неправильнх ответов за ссесию
    string nameObj = null;

    // Start is called before the first frame update
    void Start()
    {
        allObjects = new GameObject[sizePanel * sizePanel];
        numbers = new int[sizePanel * sizePanel];
        arrIndex = new int[numCube];
        oldArrIndex = new int[numCube];
        float pos;
        if ((sizePanel % 2) == 0)
        {
            pos = (sizePanel / 2 * gapCube) - gapCube / 2;
        }
        else
        {
            pos = (sizePanel / 2 * gapCube);
        }    
        Vector3 panelTrans = panel.transform.position;
        for (int i = 0; i < sizePanel; i++)
        {
            for (int j = 0; j < sizePanel; j++)
            {
                //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                GameObject cube = Instantiate(cubePrefab, cubePrefab.transform.position, Quaternion.identity);
                //cubePrefab
                //GameObject cube = cubePrefab.Instantiate<GameObject>();
                cube.name = "Cude_" + ((i * sizePanel) + j);
                cube.transform.parent = panel.transform;
                cube.transform.position = new Vector3(panelTrans.x + (i * gapCube) + (gapCube - sizeCude) - pos, panelTrans.y + (j * gapCube) - pos, panelTrans.z);
                cube.transform.localScale = new Vector3(sizeCude, sizeCude, sizeCude);
                //int rand = Random.Range(0, 6);
                //cube.GetComponent<MeshRenderer>().material = materials[rand];

                allObjects[(i * sizePanel) + j] = cube;
                numbers[(i * sizePanel) + j] = (i * sizePanel) + j;

            }
        }

        controller.createSphere(panel.transform);

        activeCube();

    }

    // Update is called once per frame
    void Update()
    {
        if (controller.colAnswer <= 0)
        {
            activeCube();
        }
    }
    IEnumerator waiter()
    {
        Debug.Log("Answer: " + controller.colAnswer);
        yield return new WaitForSeconds(1);
    }

    void activeCube()
    {
        int rand = Random.Range(0, materials.Length - 1);
        foreach (GameObject obj in allObjects)
        {
            obj.GetComponent<MeshRenderer>().material = materials[rand];
            obj.SetActive(true);
        }

        controller.refreshSphere();
        controller.addName(materials[rand].name, numCube);

        int r = -1;
        numbers = ShuffleArray(numbers);
        for (int i=0; i<numCube; i++)
        {
            do
            {
               r = Random.Range(0, materials.Length - 1);
            } while (r == rand);
            Debug.Log(r);
            allObjects[numbers[i]].GetComponent<MeshRenderer>().material = materials[r];
            allObjects[numbers[i]].SetActive(true);
            oldArrIndex[i] = numbers[i];
        }
        
        //Debug.Log("Num Cube: " + numCube);
        //Debug.Log("---------------------------------");
        outArrIndex();
    }

    //void deactiveCube()
    //{
    //    //oldArrIndex = arrIndex;
    //    for(int k =0; k<numCube; k++)
    //    {
    //        oldArrIndex[k] = arrIndex[k];
    //    }
    //    for (int i = 0; i < numCube; i++)
    //    {
    //        GameObject ob = (GameObject)allObjects[arrIndex[i]];
    //        ob.GetComponent<MeshRenderer>().material = materials[0];
    //    }
    //}
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

    //public void RefreshCube()
    //{
    //    deactiveCube();
    //    activeCube();
    //}

    public int GetActiveCube()
    {
        return numCube;
    }

    //public void checkResult(string name)
    //{
    //    int col = colGood;
    //    //Debug.Log("$$$$$$$$$$$$$$$$$$$$$$$$$$$$");
    //    //Debug.Log("Num Cube: " + numCube);
    //    //Debug.Log("Size: " + oldArrIndex.Length);
  

    //    for (int i = 0; i < numCube; i++)
    //    {
    //        Debug.Log(name);
    //        Debug.Log(allObjects[oldArrIndex[i]].name);
    //        if (name == allObjects[oldArrIndex[i]].name)
    //        {
    //            colGood++;
    //        }
    //    }
    //    if (col != colGood)
    //    {
    //        Debug.Log("Good!");
    //        //Анимация правильного ответа
    //        if (colGood == numCube)
    //        {
    //            sumGoob += colGood;
    //            colGood = 0;
    //            activeCube();
    //            //новый раунд в игре
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("Loos!");
    //        colBad++;
    //        //Анимация неправильного ответа
    //    }

    //}
    public void checkResult()
    {
        int col = numCube;
        for(int i=0; i< arrIndex.Length; i++)
        {
            if (allObjects[arrIndex[i]].activeInHierarchy == false)
            {
                col--;
            }
        }
        if (col == 0)
        {
            activeCube();
        }
    }

    public void outArrIndex()
    {
        foreach (int i in oldArrIndex)
        {
            Debug.Log(i);
        }
    }

}



