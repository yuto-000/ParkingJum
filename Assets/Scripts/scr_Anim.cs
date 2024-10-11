using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scr_Anim : MonoBehaviour
{
    [SerializeField]
    [Tooltip("モーション")]
    public GameObject Goals;

    [SerializeField]
    [Tooltip("モーション")]
    public GameObject Car;

    private int DestNum = 0;
    private NavMeshAgent Agent;




    private List<Transform> Target = new List<Transform>();

    float[] Pos;
    bool Hit = false;
    // Start is called before the first frame update
    void Start()
    {
        Pos = new float[Goals.transform.childCount];
        for (int i = 0; i < Goals.transform.childCount; i++)
        {
            Pos[i] = 0;
            Target.Add(Goals.transform.GetChild(i).gameObject.transform);
            Goals.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void nextGoal()
    {
        DestNum += 1;

        if (DestNum == Goals.transform.childCount)
        {
            Destroy(Car);
        }
        else
        {
          

            Agent.destination = Target[DestNum].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Hit)
        {
            // Debug.Log(Agent.remainingDistance);
            if (Agent.remainingDistance < 2.0f)
            {
                nextGoal();
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Agent = GetComponent<NavMeshAgent>();
            for (int i = 0; i < Goals.transform.childCount; i++)
            {

                Pos[i] = Vector3.Distance(Car.transform.position, Target[i].transform.position);

            }

            var min = Mathf.Min(Pos);
            for (int i = 0; i < Goals.transform.childCount; i++)
            {
                if (min == Pos[i])
                {
                    DestNum = i + 1;
                }

            }
            Agent.destination = Target[DestNum].position;
            Hit = true;

        }
    }
}
