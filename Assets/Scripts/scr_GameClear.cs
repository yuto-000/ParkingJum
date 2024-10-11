using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_GameClear : MonoBehaviour
{
    bool Clear;
    [SerializeField]
    int Next;
    int time;

    [SerializeField]Canvas canvas;
    private void Start()
    {
        canvas.enabled = false;
        Clear = false;
    }
    private void Update()
    {
        time--;
        Debug.Log(canvas.enabled);
        if (Clear && time < 440)
        {

            canvas.enabled = true;

        }
        if (Clear&&time<0) SceneManager.LoadScene(Next);
    }
    private void OnTriggerExit(Collider other)
    {
       
        if (other.gameObject.tag == "Car")
        {

            Clear = true;
            time = 500;
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
