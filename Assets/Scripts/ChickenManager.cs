using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChickenManager : MonoBehaviour {


    public GameObject []chickens;



    public void ActivateChicken()
    {
        for(int i=0;i<chickens.Length;i++)
        {
            if (!chickens[i].GetComponent<NavMeshAgent>().enabled)
            {
                chickens[i].GetComponent<NavMeshAgent>().enabled = true;
                break;
            }

        }
    }



}
