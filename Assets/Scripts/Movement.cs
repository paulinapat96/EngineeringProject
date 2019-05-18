using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{

    class Move
    {
        public float startTime;
        public float duration;
        public Vector3 beginPosition;
        public Vector3 endPosition;
        public int direction;
        public float velocity;
        public float deltaPos;
        public bool qualifiesToFly;

        public Move(float startTime, Vector3 beginPosition, int direction)
        {
            this.startTime = startTime;
            this.beginPosition = beginPosition;
            this.direction = direction;
            this.endPosition = Vector3.zero;
            velocity = 0;
            deltaPos = 0;
            qualifiesToFly = false;
        }

        public string Printf()
        {
            return System.Math.Round(startTime, 2) + "s \t\t" + beginPosition.y + " -> " + endPosition.y + " = " + deltaPos + "\t\tdir = " +
                   direction + "\t\tv = " + velocity;
        }
    }

    [SerializeField]
    private float minDistanceToFly;
    [SerializeField]
    private float minVelocityToFly;
    [SerializeField]
    private bool isLeftWing;

    public static event Action<float, bool> OnWingForce;

    private float _timer;
    private Vector3 _lastMovePosition;
    private Vector3 _previousPosition;
    private int _lastMoveDirection;
    private List<Move> listOfMoves = new List<Move>();
    private Vector3[] logOfPreviousPositions = new Vector3[3] { Vector3.zero, Vector3.zero, Vector3.zero };

    private void Start()
    {
        _timer = 0;
        listOfMoves.Add(new Move(_timer, transform.localPosition, 0));
        logOfPreviousPositions = new Vector3[3] { transform.localPosition, transform.localPosition, transform.localPosition };
        _lastMovePosition = GetLastMoveStartPostion();
        _lastMoveDirection = GetLastMoveDirection();

    }

    private void Update()
    {
        _timer += Time.deltaTime;
        LogPosition();

        if (logOfPreviousPositions[2].y > transform.localPosition.y)
        {
            if (_lastMoveDirection != -1)
            {
                ChangeMovement(-1);
                Debug.Log("Down");
            }
        }
        else if (logOfPreviousPositions[2].y < transform.localPosition.y)
        {
            if (_lastMoveDirection != 1)
            {
                ChangeMovement(1);
                Debug.Log("Up");
            }
        }

        DrawDebug();
    }

    private Vector3 GetLastMoveStartPostion()
    {
        return listOfMoves[listOfMoves.Count - 1].beginPosition;
    }

    private int GetLastMoveDirection()
    {
        return listOfMoves[listOfMoves.Count - 1].direction;
    }

    private void ChangeMovement(int direction)
    {
        Move moveToEnd = listOfMoves[listOfMoves.Count - 1];
        float deltaT = _timer - moveToEnd.startTime;
        float deltaS = Vector3.Distance(moveToEnd.beginPosition, logOfPreviousPositions[2]);
        float velocity = deltaS / deltaT;
        moveToEnd.velocity = velocity;
        moveToEnd.duration = deltaT;
        moveToEnd.deltaPos = deltaS;
        moveToEnd.endPosition = logOfPreviousPositions[2];
        listOfMoves[listOfMoves.Count - 1] = moveToEnd;

        listOfMoves.Add(new Move(_timer, transform.localPosition, direction));
        _lastMoveDirection = direction;
        _lastMovePosition = transform.localPosition;

        QualifyToFly();

    }

    private void QualifyToFly()
    {
        Move moveToQualify = listOfMoves[listOfMoves.Count - 2];
        if (moveToQualify.deltaPos > minDistanceToFly && moveToQualify.velocity > minVelocityToFly)
        {
            moveToQualify.qualifiesToFly = true;
            if (OnWingForce != null) OnWingForce(moveToQualify.velocity, isLeftWing);
        }
    }

    private void LogPosition()
    {
        logOfPreviousPositions[2] = logOfPreviousPositions[1];
        logOfPreviousPositions[1] = logOfPreviousPositions[0];
        logOfPreviousPositions[0] = transform.localPosition;

    }

    public Text text;
    private void DrawDebug()
    {

        int k;
        text.text = "List Of Moves [10] \nDir \t\t\tVel \n";
        for (int i = 11; i > 0; i--)
        {
            k = listOfMoves.Count - i;
            if (k < 0) continue;
            text.text += listOfMoves[k].Printf() + "\n";
        }
    }
}