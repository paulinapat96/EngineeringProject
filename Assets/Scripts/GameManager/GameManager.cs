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
        timerSlider.fillAmount = timer/60;
   

        if (timer >= 60.0)
        {
            _seedManager.EndGame();
            GetComponent<AudioSource>().PlayOneShot(endGame);
            description.color = Color.blue;
            description.fontSize = 70;
            description.text = "You collected " + _seedManager.seedPicked + " seeds. \nGet ready for the flight!";
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

        Debug.Log(_seedManager.seedPicked);

        if (_seedManager.seedPicked == 2 || _seedManager.seedPicked == 5 || _seedManager.seedPicked == 10)
            _chickenManager.ActivateChicken();

    }

    public void OnEatSeed(Seed seed)
    {
        _seedManager.OnEatSeed(seed);
    }

    public IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(10.0f);
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("PigeonRunGame");
    }

}
