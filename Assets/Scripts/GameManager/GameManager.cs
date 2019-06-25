using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityHelpers;
using UnityEngine.SceneManagement;



public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private Camera _playerCamera;
    [SerializeField] private float timer;
    [SerializeField] Image timerSlider;
    private float gameTime = 50;

    SeedManager _seedManager;
    ChickenManager _chickenManager;
    public Text description;

    public Image []healthIcons;

    public AudioClip endGame;

    public Vector3 PlayerPosition
    {
        get { return new Vector3(_playerCamera.transform.position.x, 0.5f, _playerCamera.transform.position.z); }
    }


    private void Start()
    {
        _seedManager = GetComponent<SeedManager>();
        _chickenManager = GetComponent<ChickenManager>();
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        timerSlider.fillAmount = timer/ gameTime;
   

        if (timer >= gameTime)
        {
            _seedManager.EndGame();
            GetComponent<AudioSource>().PlayOneShot(endGame);
            description.fontSize = 70;
            description.text = "You ate a lot of seeds! \n<color='red'>Get ready for the flight!</color>";
            StartCoroutine(ChangeScene());
        }

    }

    public Transform GetNearestTarget(Transform chicken)
    {
        return _seedManager.GetNearestTarget(chicken);
    }


    public void OnPickedSeed(Seed seed)
    {
        _seedManager.OnPickedSeed(seed);

        Debug.Log(SeedManager.currentSeed);

        if (SeedManager.currentSeed == 3 || SeedManager.currentSeed == 6 || SeedManager.currentSeed == 12)
            _chickenManager.ActivateChicken();

    }

    public void OnEatSeed(Seed seed)
    {
        _seedManager.OnEatSeed(seed);
    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(6.0f);
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("PigeonRunGame");
    }

}
