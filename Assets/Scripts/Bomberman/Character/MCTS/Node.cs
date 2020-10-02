using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bomberman.Character.MCTS
{
	public class Node
	{
		private static double SQRT_TWO = Math.Sqrt(2);
		
		public Dictionary<MoveType, Node> Children { get; } = new Dictionary<MoveType, Node>();
		
		public GameState State { get; }

		private int _visitCount = 1;
		private bool _expanded;
		
		private Result _result = new Result();
		public Result Result
		{
			get
			{
				Result result = new Result(0, 0);

				foreach (Node child in Children.Values)
				{
					result += child.Result;
				}

				return result + _result;
			}
		}

		public Node(GameState state)
		{
			State = state;

			for (int i = 0; i < 20; i++)
			{
				GameState simState = new GameState(State);
				while (!simState.IsGameFinished())
				{
					GameSimulator.Simulate(simState, true);
				}

				_result.Wins += simState.GetResult() ? 1 : 0;
				_result.Games++;
			}
		}

		public void Expand()
		{
			_visitCount++;
			if (_expanded)
			{
				Node bestChild = null;
				double bestUtc = 0;
				foreach (Node child in Children.Values)
				{
					if (child.State.Self == null) continue;

					double uct = child.Result.WinRate + SQRT_TWO * Math.Sqrt(Math.Log(_visitCount) / child._visitCount);
					
					if (uct > bestUtc)
					{
						bestChild = child;
						bestUtc = uct;
					}
				}
				
				bestChild?.Expand();
				return;
			}

			foreach (MoveType move in State.GetPossibleMoves())
			{
				GameState nextGameState = new GameState(State);
				nextGameState.ApplyRequestedAction(MoveTypeToRequestedAction(move));
				
				GameSimulator.Simulate(nextGameState, false);
				
				Children.Add(move, new Node(nextGameState));
			}

			_expanded = true;
		}

		public RequestedAction GetBestAction()
		{
			float bestWinRate = 0;
			RequestedAction bestAction = null;
			foreach (KeyValuePair<MoveType, Node> pair in Children)
			{
				float winRate = pair.Value.Result.WinRate;
				if (winRate >= bestWinRate)
				{
					bestWinRate = winRate;
					bestAction = MoveTypeToRequestedAction(pair.Key);
				}
			}
			
			return bestAction;
		}

		public static RequestedAction MoveTypeToRequestedAction(MoveType move)
		{
			switch (move)
			{
				case MoveType.Right:
					return new RequestedAction
					{
						Move = new Vector2Int(1, 0)
					};
				case MoveType.Left:
					return new RequestedAction
					{
						Move = new Vector2Int(-1, 0)
					};
				case MoveType.Up:
					return new RequestedAction
					{
						Move = new Vector2Int(0, 1)
					};
				case MoveType.Down:
					return new RequestedAction
					{
						Move = new Vector2Int(0, -1)
					};
				case MoveType.Drop:
					return new RequestedAction
					{
						DropBomb = true
					};
				case MoveType.Wait:
					return new RequestedAction();
			}
			
			throw new InvalidOperationException();
		}
	}
}