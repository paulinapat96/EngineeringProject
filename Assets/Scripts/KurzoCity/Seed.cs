using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour {

    public Transform spawnTransform;

    public void Start()
    {
        Debug.Log("My position: " + transform.position);
    }

    public void OnPlayerLook()
    {

        GameManager.instance.OnPickedSeed(this);

    }

}
