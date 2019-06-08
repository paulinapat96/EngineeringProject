using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SeedManager : MonoBehaviour
{


    public GameObject seedPrefab;

    private List<Seed> _seedList;

    public Transform[] regions;
    AudioSource _audioSource;
    public AudioClip picked;
    public AudioClip failed;

    private float _spawnInterval = 2.0f;
    private float _currentInterval = 0.0f;
    public int seedPicked = 0;

    private float _maximumInterval;


    bool ended = false;

    struct TransformStatus
    {
        public Transform spawn;
        public bool isBusy;
    }


    List<TransformStatus> transformStatus;


    public void OnPickedSeed(Seed seed)
    {
        seedPicked++;
        SetSpawnClear(seed);
        _audioSource.PlayOneShot(picked);
        _seedList.Remove(seed);
        Destroy(seed.gameObject);
        UpdateSlider();
    }

    [SerializeField] Slider seedsSlider;
    private void UpdateSlider()
    {
        seedsSlider.value = seedPicked;
    }

    private void SetValues()
    {
        seedPicked = 0;
    }

    private void Start()
    {
        SetValues();
        transformStatus = new List<TransformStatus>();

        foreach (Transform currentRegion in regions)
        {
            TransformStatus currentTransformStatus = new TransformStatus();
            currentTransformStatus.spawn = currentRegion;
            currentTransformStatus.isBusy = false;
            transformStatus.Add(currentTransformStatus);
        }

        _audioSource = GetComponent<AudioSource>();
        _seedList = new List<Seed>();
    }



    private void SetSpawnClear(Seed seed)
    {

        for (int i = 0; i < transformStatus.Count; i++)
        {

            if (transformStatus[i].spawn == seed.spawnTransform)
            {
                Transform currentTransform = transformStatus[i].spawn;
                TransformStatus currentTransformStatus1 = new TransformStatus();
                currentTransformStatus1.spawn = currentTransform;
                currentTransformStatus1.isBusy = false;
                transformStatus.Remove(transformStatus[i]);
                transformStatus.Add(currentTransformStatus1);
                break;
            }
        }

    }

    private void SpawnSeed()
    {

        List<Transform> avalibleTransforms = new List<Transform>();

        foreach (TransformStatus currentTransformStatus in transformStatus)
        {

            if (!currentTransformStatus.isBusy)
                avalibleTransforms.Add(currentTransformStatus.spawn);
        }

        if (avalibleTransforms.Count == 0)
            return;

        int transformStatusIdx = Random.Range(0, avalibleTransforms.Count);

        for (int i = 0; i < transformStatus.Count; i++)
        {
            if (transformStatus[i].spawn == avalibleTransforms[transformStatusIdx])
            {
                Transform currentTransform = transformStatus[i].spawn;
                TransformStatus currentTransformStatus1 = new TransformStatus();
                currentTransformStatus1.spawn = currentTransform;
                currentTransformStatus1.isBusy = true;
                transformStatus.Remove(transformStatus[i]);
                transformStatus.Add(currentTransformStatus1);
                break;
            }
        }

        Seed currentSeed = Instantiate(seedPrefab, avalibleTransforms[transformStatusIdx].position, Quaternion.identity, null).GetComponent<Seed>();
        currentSeed.spawnTransform = avalibleTransforms[transformStatusIdx];

        _seedList.Add(currentSeed);
    }

    internal void OnEatSeed(Seed seed)
    {
        SetSpawnClear(seed);
        _audioSource.PlayOneShot(failed);
        _seedList.Remove(seed);
        Destroy(seed.gameObject);
    }

    public Transform GetNearestTarget(Transform chicken)
    {
        Vector3 currPos = chicken.position;

        if (_seedList.Count == 0)
            return null;


        Transform nearest = _seedList[0].transform;
        float distance = Mathf.Infinity;

        return _seedList[Random.Range(0, _seedList.Count)].transform;


        foreach (Seed currentSeed in _seedList)
        {

            float currDist = Vector3.Distance(chicken.transform.position, currentSeed.transform.position);

            if (currDist < distance && currDist > 2.0f)
            {
                distance = currDist;
                nearest = currentSeed.transform;
            }
        }

        return nearest;
    }


    public void EndGame()
    {
        ended = true;
    }


    private void Update()
    {
        if (ended)
            return;


        _currentInterval += Time.deltaTime;


        if (_currentInterval >= Mathf.Clamp(_spawnInterval - (seedPicked * 0.2f), 2.2f, _spawnInterval))
        {
            _currentInterval = 0.0f;

            for (int i = 0; i < (int)Mathf.Clamp(seedPicked / 9, 1, 5); i++)
                SpawnSeed();
        }
    }

}



