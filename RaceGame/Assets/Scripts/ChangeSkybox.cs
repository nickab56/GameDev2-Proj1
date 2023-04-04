using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{
    public Material NomralSkybox;
    public Material DestroyedSkybox;
    public GameObject Particles;
    public Light Sun;
    public AudioSource EpicMusic;
    public AudioSource FutureMusic;

    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = DestroyedSkybox;
        Particles.SetActive(true);
        Sun.enabled = false;
        EpicMusic.Play();
        FutureMusic.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
