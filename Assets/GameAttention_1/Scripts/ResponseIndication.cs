using UnityEngine;

public class ResponseIndication : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform panel;
    public Material matGood;
    public Material matLoos;
    public Material matNone;
    public float sizeSphere = 75f;

    private GameObject sphere;

    void Start()
    {
        Vector3 panelTrans = panel.transform.position;

        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.parent = panel.transform;
        sphere.transform.position = new Vector3(panelTrans.x + 300f, panelTrans.y -240f, panelTrans.z);
        sphere.transform.localScale = new Vector3(sizeSphere, sizeSphere, sizeSphere);
        sphere.GetComponent<MeshRenderer>().material = matNone;
    }

    public void GoodIndication()
    {
        sphere.GetComponent<MeshRenderer>().material = matGood;
    }

    public void LossIndication()
    {
        sphere.GetComponent<MeshRenderer>().material = matLoos;
    }

    public void NoneIndication()
    {
        sphere.GetComponent<MeshRenderer>().material = matNone;
    }
}
