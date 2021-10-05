using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenereteButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SceneController controller;
    [SerializeField] private Sprite StartImage;
    [SerializeField] private Sprite[] images;
    public Transform panel;

    public float sizeButton = 2f;
    public float gapButton = 120f;
    void Start()
    {
        Vector3 canvasPos = panel.transform.position;
        GameObject newButton = new GameObject("Start Button", typeof(Image), typeof(Button), typeof(LayoutElement));
        newButton.GetComponent<Button>().onClick.AddListener(() => ButtonClickedStart(newButton.GetComponent<Button>()));
        newButton.GetComponent<Image>().sprite = StartImage;
        newButton.transform.position = new Vector3(canvasPos.x, canvasPos.y - 460f, canvasPos.z + 100.0f);
        newButton.transform.parent = panel.transform;
        newButton.transform.localScale = new Vector3(0.8f * sizeButton, 0.5f * sizeButton, 0.5f * sizeButton);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generageButton()
    {
        int num = controller.GetActiveCube();
        if (num <= 0) return; // выход в случаи отсутствия кубов
        float pos;
        if ((num % 2) == 0)
        {
            pos = (num / 2 * gapButton);
        }
        else
        {
            pos = (num / 2 * gapButton) + gapButton / 2;
        }
        for (int i = 0; i <= num; i++)
        {
            float cof = i * gapButton - pos;
            Vector3 canvasPos = panel.transform.position;
            GameObject newButton = new GameObject("Button_" + i, typeof(Image), typeof(Button), typeof(LayoutElement));
            newButton.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(newButton.name));
            newButton.GetComponent<Image>().sprite = images[i];
            newButton.transform.position = new Vector3(canvasPos.x + cof, canvasPos.y - 460f, canvasPos.z + 100.0f);
            newButton.transform.parent = panel.transform;
            newButton.transform.localScale = new Vector3(0.5f * sizeButton, 0.5f * sizeButton, 0.5f * sizeButton);
        }

    }
    void ButtonClicked(string name)
    {
        //Debug.Log(name);
        if ("Button_0" == name) controller.checkResult(0);
        else if ("Button_1" == name) controller.checkResult(1);
        else if ("Button_2" == name) controller.checkResult(2);
        else if ("Button_3" == name) controller.checkResult(3);
        else if ("Button_4" == name) controller.checkResult(4);

        controller.RefreshCube();
    }

    void ButtonClickedStart(Button but)
    {
        but.gameObject.SetActive(false); // Удаление кнопки старт
        //Destroy(but.gameObject);
        controller.RefreshCube();
        generageButton();
    }
}
