 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_Rotation : MonoBehaviour
{
    public
        float RotX = 0.0f;
    public
        float RotY = 0.0f;
    public
        float RotZ = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Rotate(new Vector3(RotX, RotY, RotZ) * Time.deltaTime) ;
    }
}
