using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartGameButton : MonoBehaviour {


    AudioSource _audioSource;

    public AudioClip pressedButton;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    public void OnPressedButton()
    {
        StartCoroutine(StartGame());
        _audioSource.PlayOneShot(pressedButton);
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("GameScene");

    }




}
