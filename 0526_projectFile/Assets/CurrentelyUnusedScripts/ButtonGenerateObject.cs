using UnityEngine;
using System.Collections;

public class ButtonGenerateObject : MonoBehaviour {

    public GameObject obj;

    public void OnClick()
    {
        Instantiate(obj);
    }
}
