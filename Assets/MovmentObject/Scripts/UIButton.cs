using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] private CarController controller;
    public Transform panel;
    public Sprite right;
    public Sprite left;
    public float sizeButton = 1;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 canvasPos = panel.transform.position;
        GameObject newButton1 = new GameObject("Right", typeof(Image), typeof(Button), typeof(LayoutElement));
        newButton1.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(newButton1.name));
        newButton1.GetComponent<Image>().sprite = right;
        newButton1.transform.position = new Vector3(canvasPos.x + 700f, canvasPos.y - 440f, canvasPos.z);
        newButton1.transform.parent = panel.transform;
        
        newButton1.transform.localScale = new Vector3(1.0f * sizeButton, 0.5f * sizeButton, 0.5f * sizeButton);


        GameObject newButton2 = new GameObject("Left", typeof(Image), typeof(Button), typeof(LayoutElement));
        newButton2.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(newButton2.name));
        newButton2.GetComponent<Image>().sprite = left;
        newButton2.transform.position = new Vector3(canvasPos.x - 700f, canvasPos.y - 440f, canvasPos.z);
        newButton2.transform.parent = panel.transform;
       
        newButton2.transform.localScale = new Vector3(1.0f * sizeButton, 0.5f * sizeButton, 0.5f * sizeButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ButtonClicked(string name)
    {
        //Debug.Log(name);
        if ("Right" == name) controller.checkResult(0);
        else if ("Left" == name) controller.checkResult(1);
    }
}
