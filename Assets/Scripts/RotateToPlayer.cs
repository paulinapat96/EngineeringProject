using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPlayer : MonoBehaviour {


    private void Update()
    {
        transform.LookAt(GameManager.instance.PlayerPosition);
    }




}
