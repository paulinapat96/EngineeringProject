using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

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
			return System.Math.Round(startTime, 2) + "s \t\t" + beginPosition.y + " -> " + endPosition.y + " = "+ deltaPos + "\t\tdir = " +
			       direction + "\t\tv = " + velocity;
		}
	}

	[SerializeField] private float minDistanceToFly;
	[SerializeField] private float minVelocityToFly;
	
	private float _timer;
	private Vector3 _lastMovePosition;
	private Vector3 _previousPosition;
	private int _lastMoveDirection;
	private List<Move> listOfMoves = new List<Move>();
	private Vector3[] logOfPreviousPositions = new Vector3[3]{Vector3.zero,Vector3.zero,Vector3.zero};

	private void Start()
	{
		_timer = 0;
		listOfMoves.Add(new Move(_timer, transform.position, 0));
		_lastMovePosition = GetLastMoveStartPostion();
		_lastMoveDirection = GetLastMoveDirection();

	}

	private void Update()
	{
		_timer += Time.deltaTime;
		LogPosition();

		if (logOfPreviousPositions[2].y > transform.position.y)
		{
			if (_lastMoveDirection != -1)
			{
				ChangeMovement(-1);
				Debug.Log("Down");
			}
		}
		else if (logOfPreviousPositions[2].y < transform.position.y)
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
		return listOfMoves[listOfMoves.Count -1].beginPosition;
	}
	
	private int GetLastMoveDirection()
	{
		return listOfMoves[listOfMoves.Count -1].direction;
	}
	
	private void ChangeMovement(int direction)
	{

		float deltaT = _timer - listOfMoves[listOfMoves.Count - 1].startTime;
		float deltaS =  Vector3.Distance(listOfMoves[listOfMoves.Count -1].beginPosition, logOfPreviousPositions[2]);
		float velocity = deltaS / deltaT;
		listOfMoves[listOfMoves.Count -1].velocity = velocity;
		listOfMoves[listOfMoves.Count -1].duration = deltaT;
		listOfMoves[listOfMoves.Count -1].deltaPos = deltaS;
		listOfMoves[listOfMoves.Count - 1].endPosition = logOfPreviousPositions[2];
		
		listOfMoves.Add(new Move(_timer, transform.position, direction));
		_lastMoveDirection = direction;
		_lastMovePosition = transform.position;

	}

	private void LogPosition()
	{
		logOfPreviousPositions[2] = logOfPreviousPositions[1];
		logOfPreviousPositions[1] = logOfPreviousPositions[0];
		logOfPreviousPositions[0] = transform.position;

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
