using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [SerializeField] Material barrierMaterial;

    void Start()
    {
        Time.timeScale = 1;
        if (SeedManager.currentSeed < 25) SeedManager.currentSeed = 25;
        Movement.OnWingForce += DoFly;
    }

    private void DoFly(float arg1, bool arg2)
    {
        SeedManager.currentSeed -= 0.5f;
    }

    void Update()
    {
        if (SeedManager.currentSeed > 100) SeedManager.currentSeed = 100;
        if (SeedManager.currentSeed <= 0)
        {
            Time.timeScale = 0;
        }
        barrierMaterial.mainTextureOffset = new Vector2(barrierMaterial.mainTextureOffset.x, barrierMaterial.mainTextureOffset.y + 0.001f);
    }
}
