using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeControlG : MonoBehaviour
{
    // Start is called before the first frame update
    string nameOldMaterial; // материал не нужный
  //  string nameNowMaterial; // материал не нужный
    Stack<string> stackName = new Stack<string>();

    [SerializeField] private SceneControllerG controller;
    [SerializeField] private GameObject indicationAnswers;
    public Material correctAnswer;
    public Material wrongAnswer;
    public Material noneAnswer;

    public int colAnswer;
    GameObject sphere;

    void Start()
    {
        
       // sphere.transform.parent = panel.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void refreshSphere()
    {
        sphere.GetComponent<MeshRenderer>().material = noneAnswer;
    }

    public void createSphere(Transform panel)
    {
        Vector3 panelTrans = panel.transform.position;
        
        sphere = Instantiate(indicationAnswers, indicationAnswers.transform.position, Quaternion.identity);
        sphere.transform.parent = panel.transform;
        sphere.transform.position = new Vector3(panelTrans.x + 320, panelTrans.y - 240, panelTrans.z);
        refreshSphere();
    }

    public void addName(string name, int num)
    {
        nameOldMaterial = name;
        colAnswer = num;
        //Debug.Log(stackName.Peek());
        //Debug.Log("******************");
    }

    public int addStack(string name)
    {
        stackName.Push(name);
        Debug.Log("******************");
        Debug.Log(nameOldMaterial);
        Debug.Log(name);
        if (name == nameOldMaterial)
        {
            Debug.Log("Loos!");
            sphere.GetComponent<MeshRenderer>().material = wrongAnswer;

        }
        else
        {
            Debug.Log("Good!");
            sphere.GetComponent<MeshRenderer>().material = correctAnswer;
            colAnswer--;
        }
        Debug.Log("******************");

        return colAnswer;
        //Debug.Log(stackName.Peek());
        //Debug.Log("******************");
    }

    public void outStack()
    {
        //Debug.Log("####################");

        //Debug.Log(stackName.Peek());

       
        //Debug.Log("####################");
    }
}
