using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Chicken : MonoBehaviour {

    public Sprite[] chickenSprites;
    SpriteRenderer _spriteRenderer;
    NavMeshAgent _navMeshAgent;

    public Transform target;


    public Vector3 lastPosition;

    public float animationTimeInterval = 0.2f;


    private float _currentEatTime = 0.0f;
    private float _maxEatTime = 1.5f;


    private int _currentIdx = 0;
    private int _maxIdx = 0;
    private float _currentTime = 0.0f; 

	// Use this for initialization
	void Start ()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _maxIdx = chickenSprites.Length;
	}

    public float vec;
    // Update is called once per frame
    void Update ()
    {
        if(_navMeshAgent.enabled)
            if (target != null)
            {
                _navMeshAgent.SetDestination(target.position);
                lastPosition = target.position;
                vec = Vector3.Distance(transform.position, target.position);
                if (vec < 3f)
                {
                    _currentEatTime += Time.deltaTime;

                    if (_currentEatTime > _maxEatTime)
                    {
                        _currentEatTime = 0.0f;
                        GameManager.instance.OnEatSeed(target.GetComponent<Seed>());
                    }
                }
                else
                    _currentEatTime = 0.0f;


            }
            else
            {
                _navMeshAgent.SetDestination(lastPosition);
                target = GameManager.instance.GetNearestTarget(transform);
            }



        _currentTime += Time.deltaTime;


        _spriteRenderer.flipX = _navMeshAgent.velocity.x > 0.0f;


        if(_currentTime >= animationTimeInterval)
        {
            _currentTime = 0.0f;
            _spriteRenderer.sprite = chickenSprites[++_currentIdx % _maxIdx];
        }

	}


}
