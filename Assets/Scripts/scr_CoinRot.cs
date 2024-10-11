using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_CoinRot : MonoBehaviour
{
    float MoveY = 10;
    int time = 0;
    // Start is called before the first frame update
    void Start()
    {
        time = 180;
    }

    // Update is called once per frame
    void Update()
    {
        time--;
        if (time<0)
        {
            Destroy(this.gameObject);
        }
        this.gameObject.transform.Rotate(new Vector3(0, 0, 200) * Time.deltaTime);
    }
}
