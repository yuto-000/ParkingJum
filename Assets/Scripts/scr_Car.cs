using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class scr_Car : MonoBehaviour
{
    public GameObject game;
    BoxCollider boxCollider;

    private Vector3 pos;

    private Vector3 touchStartPos;
    private Vector3 touchNowPos;
    private string direction;
    private bool isTouch;

   
    private bool isMove;
    [SerializeField] private float moveSpeed;
    private bool hit;
    private bool kotei;
    private void Start()
    {
        isTouch = false;
        isMove = false;
        hit = false;
        kotei = false;
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (!hit)
        {

            RaycastHit hits;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hits))
            {

                if (hits.collider.gameObject == game)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        touchStartPos = Input.mousePosition;
                        kotei = true;
                        game.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    
                        game.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0) && kotei)
            {
                hit = true;
            }

        }
        if (hit)
        {

            Move();
        }

        if (!hit)
        {
            touchNowPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
            GetDirection();
        }
        if (!kotei)
        {
            game.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        }

    }
    private void GetDirection()
    {
        float directionX = touchNowPos.x - touchStartPos.x;
        float directionY = touchNowPos.y - touchStartPos.y;
        if (game.transform.rotation.y > 0)
        {
            if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
            {
                if (30 < directionX)
                {
                    direction = "right";
                }
                else if (-30 > directionX)
                {
                    direction = "left";
                }
            }
            else
            {
                direction = "touch";
            }
        }
        else if (game.transform.rotation.y == 0)
        {

            if (Mathf.Abs(directionX) < Mathf.Abs(directionY))
            {
                if (30 < directionY)
                {
                    direction = "up";
                }
                else if (-30 > directionY)
                {
                    direction = "down";
                }
            }
            else
            {
                direction = "touch";
            }
        }

      

        if (direction == null || direction == "touch") { return; }

        isTouch = false;
        isMove = true;
    }

    private void Move()
    {
        pos = game.transform.position;
        switch (direction)
        {
            case "up":
                pos.z += moveSpeed;
                break;

            case "down":
                pos.z -= moveSpeed;
                break;

            case "right":
                pos.x += moveSpeed;
                break;

            case "left":
                pos.x -= moveSpeed;
                break;
            case "touch":
                kotei = false;
                hit = false;
                break;



        }

        game.transform.position = pos;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Car")
        {

            collision.rigidbody.velocity = Vector3.zero;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            hit = false;
            kotei = false;
        }
        if (collision.gameObject.tag == "Wall")
        {

            GetComponent<Rigidbody>().velocity = Vector3.zero;
            hit = false;
            kotei = false;
        }
    }

}
