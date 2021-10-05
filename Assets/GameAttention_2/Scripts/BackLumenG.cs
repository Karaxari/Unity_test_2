using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackLumenG : MonoBehaviour
{
    [SerializeField] private SceneControllerG controllerScene;
    [SerializeField] private CubeControlG controller;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private string targetMessage;
    public Color highlightColor = Color.cyan;
    public Material[] arrMaterial;

    public string nameMaterial = null;

    private Material matNow = null;
    private Material matOld = null;
    private float scaleSize = 1.2f;

    private GameObject objNow = null;
    private GameObject objOld = null;

   
    public void OnMouseDown()
    {
        matNow = GetComponent<MeshRenderer>().material;
        //transform.localScale = new Vector3(120f, 120f, 120f);
        //Debug.Log(gameObject.name);
        //Debug.Log("--------------------------");
        //string name = gameObject.name;
        ////checkResult(name);
        ////Debug.Log("Size: " + oldArrIndex.Length);
        //
        string name = matNow.name;
        name = name.Replace(" (Instance)", "");

        int num = controller.addStack(name);
        gameObject.SetActive(false);
        if (num == 0)
            controllerScene.checkResult();

        //if (matOld != null && matNow.name == matOld.name)
        //{
        //    Debug.Log("Kurvaaaaaaaaa!");
        //}

        //matOld = matNow;
    }

}
