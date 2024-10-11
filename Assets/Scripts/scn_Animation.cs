using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class scn_Animation : MonoBehaviour
{
    [SerializeField]
    public Transform[] goals;
    private int destNum = 0;
    private NavMeshAgent agent;

    [SerializeField]
    public GameObject car;

    float[] pos;
    bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        pos = new float[goals.Length];
        for (int i = 0; i < goals.Length; i++)
        {
            pos[i] = 0;
        }
    }

    void nextGoal()
    {
        destNum += 1;

        if (destNum == goals.Length)
        {
            Destroy(car);
        }
        else
        {

            agent.destination = goals[destNum].position;
        }
        Debug.Log(destNum);
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) {
            // Debug.Log(agent.remainingDistance);
        if (agent.remainingDistance < 2.0f)
        {
            nextGoal();
        }

    }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            agent = GetComponent<NavMeshAgent>();
            for (int i = 0; i < goals.Length; i++)
            {

                pos[i] = Vector3.Distance(car.transform.position, goals[i].transform.position);

                Debug.Log(pos[i]);
            }

            var min = Mathf.Min(pos);
            for (int i = 0; i < goals.Length; i++)
            {
                if (min == pos[i])
                {
                    destNum = i+1;
                }

            }
            agent.destination = goals[destNum].position;
            hit = true;

        }
    }
}