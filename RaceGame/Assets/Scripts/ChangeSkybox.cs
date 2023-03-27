using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{
    public Material NomralSkybox;
    public Material DestroyedSkybox;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = DestroyedSkybox;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
