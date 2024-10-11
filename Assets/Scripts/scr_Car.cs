using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;


public class scr_Car : MonoBehaviour
{

    public GameObject Car;
    BoxCollider BoxCollider;

    private Vector3 Pos;

    private Vector3 TouchStartPos;
    private Vector3 TouchNowPos;
    public string Direction;


    [SerializeField] private float MoveSpeed = 0.05f;
    public bool Hit;
    private bool Lock;

    int time;

    scr_Wheel s_Wheel;
    private void Start()
    {
        s_Wheel = this.gameObject.GetComponent<scr_Wheel>();
        Hit = false;
        Lock = false;
        BoxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        time--;
        if(time==0)
            Lock = false;

        if (!Hit)
        {

            RaycastHit hits;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hits))
            {
                if (hits.collider.gameObject == Car)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        TouchStartPos = Input.mousePosition;
                        Lock = true;
                        Car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    
                        Car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0) && Lock)
            {
                Hit = true;
            }

        }
        if (Hit)
        {

            Move();
        }

        if (!Hit)
        {
            TouchNowPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            GetDirection();
        }
        if (!Lock)
        {
            Car.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }




    }
    private void GetDirection()
    {
        float directionX = TouchNowPos.x - TouchStartPos.x;
        float directionY = TouchNowPos.y - TouchStartPos.y;
        if (Car.transform.rotation.y == 0 || Car.transform.rotation.y == 1)
        {

            if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
            {
                if (30 < directionY)
                {
                    Direction = "up";
                }
                else if (-30 > directionY)
                {
                    Direction = "down";
                }
            }
            else
            {
                Direction = "touch";
            } 
        }
        else if (Car.transform.rotation.y > 0 || Car.transform.rotation.y < 0)
        {
            if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
            {
                if (30 < directionX)
                {
                    Direction = "right";
                }
                else if (-30 > directionX)
                {
                    Direction = "left";
                }
            }
            else
            {
                Direction = "touch";
            }
        
        }

    }

    private void Move()
    {
        Pos = Car.transform.position;
        switch (Direction)
        {
            case "up":
                Pos.z += MoveSpeed;
                break;

            case "down":
                Pos.z -= MoveSpeed;
                break;

            case "right":
                Pos.x += MoveSpeed;
                break;

            case "left":
                Pos.x -= MoveSpeed;
                break;
            case "touch":
                Lock = false;
                Hit = false;
                break;



        }

        Car.transform.position = Pos;
    }



    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Car" && Hit)
        {
            s_Wheel.Move = false;
            if (collision.transform.rotation.y != Car.transform.rotation.y)
            {
                switch (Direction)
                {
                    case "up":
                        SetCarAddForce(collision.transform.right * 100);

                        break;
                    case "left":
                        SetCarAddForce(collision.transform.right * 100);

                        break;
                    case "right":
                        SetCarAddForce(collision.transform.right * -100);

                        break;
                    case "down":
                        SetCarAddForce(collision.transform.right * -100);

                        break;
                }
            }
            else if (collision.transform.rotation.y == Car.transform.rotation.y)
            {
                switch (Direction)
                {
                    case "up":
                        SetCarAddForce(collision.transform.forward * 100);

                        break;
                    case "left":
                        SetCarAddForce(collision.transform.forward * 100);

                        break;
                    case "right":
                        SetCarAddForce(collision.transform.forward * -100);

                        break;
                    case "down":
                        SetCarAddForce(collision.transform.forward * -100);

                        break;
                }
            }

            Debug.Log("Hit");
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Hit = false;
            time = 40;
        }
        if (collision.gameObject.tag == "Wall")
        {
            s_Wheel.Move = false;
            switch (Direction)
            {
                case "up":
                    SetCarAddForce(collision.transform.right * 100);
                  
                    break;
                case "left":
                    SetCarAddForce(collision.transform.right * 100);
                   
                    break;
                case "right":
                    SetCarAddForce(collision.transform.right * -100);
                  
                    break;
                case "down":
                    SetCarAddForce(collision.transform.right * -100);
                  
                    break;

            }
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Hit = false;
            time = 40;
        }
    }
    public void SetCarAddForce(Vector3 force)
    {
        Rigidbody rig = this.GetComponent<Rigidbody>();

        rig.AddForce(force);

    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            BoxCollider.enabled=false;
            Destroy(this);
            Direction = "touch";
        }
    }
}
