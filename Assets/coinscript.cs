using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class coinscript : MonoBehaviour
{
    float rot = 60;
    bool played = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right,  rot*Time.deltaTime); 
        if(GetComponent<AudioSource>().isPlaying)
        {
            played = true;
            GetComponent<MeshRenderer>().enabled = false;
        } else if (played)
        {
            Destroy(gameObject);
        }
    }
}
