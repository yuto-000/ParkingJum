using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class scr_Wheel : MonoBehaviour
{
    public GameObject Wheel;

    private List<Transform> L_Wheel = new List<Transform>();

    scr_Car s_Car;
    public string Direction;
    public bool Move;
    public void Start()
    {
        Move = false;
        
        s_Car = this.gameObject.GetComponent<scr_Car>();
        for (int i = 0; i < Wheel.transform.childCount; i++) { 
        L_Wheel.Add(Wheel.transform.GetChild(i).gameObject.transform);
        }
    }

    public void Update()
    {
        if (s_Car.Hit&&!Move)
        {
            Move = true;
            Direction = s_Car.Direction;


        }
        if (Move)
        {
            for (int i = 0; i < Wheel.transform.childCount; i++)
            {
                if (this.gameObject.transform.rotation.y >= 0)
                {
                    switch (Direction)
                    {

                        case "up":
                            L_Wheel[i].transform.Rotate(new Vector3(200, 0, 0) * Time.deltaTime);
                            break;

                        case "down":
                            L_Wheel[i].transform.Rotate(new Vector3(-200, 0, 0) * Time.deltaTime);
                            break;

                        case "right":
                            L_Wheel[i].transform.Rotate(new Vector3(200, 0, 0) * Time.deltaTime);
                            break;

                        case "left":
                            L_Wheel[i].transform.Rotate(new Vector3(-200, 0, 0) * Time.deltaTime);
                            break;
                    }
                }
                else if (this.gameObject.transform.rotation.y < 0)
                {
                    switch (Direction)
                    {

                        case "up":
                            L_Wheel[i].transform.Rotate(new Vector3(200, 0, 0) * Time.deltaTime);
                            break;

                        case "down":
                            L_Wheel[i].transform.Rotate(new Vector3(-200, 0, 0) * Time.deltaTime);
                            break;

                        case "right":
                            L_Wheel[i].transform.Rotate(new Vector3(-200, 0, 0) * Time.deltaTime);
                            break;

                        case "left":
                            L_Wheel[i].transform.Rotate(new Vector3(200, 0, 0) * Time.deltaTime);
                            break;
                    }
                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground") {
            Direction = "up";
        }
    }
}

