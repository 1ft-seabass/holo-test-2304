using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

public class TouchCubeAction : MonoBehaviour
{
    int SceneID = 1;

    GameObject SceneObject1;
    GameObject SceneObject2;

    // Start is called before the first frame update
    void Start()
    {
        // Get SceneObjects
        SceneObject1 = GameObject.Find("SceneObject1");
        SceneObject2 = GameObject.Find("SceneObject2");
        // First Scene
        SceneObject1.SetActive(true);
        SceneObject2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SceneChange()
    {
        Debug.Log("SceneChange");

        SceneID++;

        if (SceneID == 1)
        {
            SceneObject1.SetActive(true);
            SceneObject2.SetActive(false);
        } else if (SceneID == 2)
        {
            SceneObject1.SetActive(false);
            SceneObject2.SetActive(true);

            // Loop SceneID
            SceneID = 0;
        }
        
    }
}
