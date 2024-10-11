using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class scr_Coin : MonoBehaviour
{
    public GameObject Target;
    public GameObject Set;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Target.transform.position);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground") {
            Instantiate(Set, new Vector3(Target.transform.position.x, Target.transform.position.y+2, Target.transform.position.z), Quaternion.Euler(90, 0, 0));
        }
    }
}
