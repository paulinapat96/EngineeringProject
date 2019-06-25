using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class UIController : MonoBehaviour
{
    [SerializeField] private Slider seedSlider;
    [SerializeField] private List<GameObject> tutorialPanels;
    [SerializeField] private GameObject endPanel;


    private int currentTutorialPanel;

    void Start()
    {
        currentTutorialPanel = tutorialPanels.Count-1;
        endPanel.SetActive(false);
        
        ScrollTutorial();

    }

    void Update()
    {
        seedSlider.value = SeedManager.currentSeed;

        if (Input.anyKeyDown || SteamVR_Input.GetStateDown("Teleport", SteamVR_Input_Sources.Any).ToString() == "True" 
                             || SteamVR_Input.GetStateDown("GrabGrip", SteamVR_Input_Sources.Any).ToString() == "True"
                             || SteamVR_Input.GetStateDown("GrabPinch", SteamVR_Input_Sources.Any).ToString() == "True")
        { 
            
            ScrollTutorial(); 
        }

        if(SeedManager.currentSeed <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        endPanel.SetActive(true);
    }

    private void ScrollTutorial()
    {
            tutorialPanels[currentTutorialPanel].SetActive(false);
            currentTutorialPanel++;
            if (currentTutorialPanel >= tutorialPanels.Count) currentTutorialPanel = 0;
            tutorialPanels[currentTutorialPanel].SetActive(true);

    }
}
