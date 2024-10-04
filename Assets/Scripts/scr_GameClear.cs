using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_GameClear : MonoBehaviour
{
    bool Clear;
    private void Start()
    {
        Clear = false;
    }
    private void Update()
    {
        Debug.Log(Clear);
        if (Clear) SceneManager.LoadScene(0);
    }
    private void OnTriggerExit(Collider other)
    {
       
        if (other.gameObject.tag == "Car")
        {
            Clear = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
          
            Clear = false;
        }
    }
}
