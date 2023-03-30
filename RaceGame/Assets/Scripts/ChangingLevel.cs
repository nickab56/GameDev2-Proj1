using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Car"))
        {
            other.transform.position = new Vector3(0, 1000, 0);

        }
    }
}
