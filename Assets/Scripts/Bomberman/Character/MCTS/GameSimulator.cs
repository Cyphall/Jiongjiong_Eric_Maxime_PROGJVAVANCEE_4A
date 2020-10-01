using System;
using System.Collections.Generic;
using System.Linq;
using Bomberman.GameManager;
using Bomberman.Terrain;
using UnityEngine;
using Random = System.Random;

namespace Bomberman.Character.MCTS
{
	public static class GameSimulator
	{
		private static Random _random = new Random((int)~DateTime.Now.Ticks);
		private static GameState _state;

		public const float TIME_PER_TURN = 0.51f;

		public static void Simulate(GameState state, bool simulateRandomSelf)
		{
			_state = state;

			if (simulateRandomSelf)
				SimulateRandomSelf();

			CheckBonuses();
			SimulateBombs();
		}
		
		private static void SimulateRandomSelf()
		{
			Vector2Int[] possibleDirections =
			{
				new Vector2Int(1, 0),
				new Vector2Int(-1, 0),
				new Vector2Int(0, 1),
				new Vector2Int(0, -1)
			};

			List<Vector2Int> validDirections = new List<Vector2Int>();

			foreach (Vector2Int direction in possibleDirections)
			{
				Vector2Int pos = _state.Self.Position + direction;
				if (_state.IsPosWalkable(pos))
				{
					validDirections.Add(direction);
				}
			}

			int randomValue = _random.Next(validDirections.Count + (_state.Self.Bomb == null ? 1 : 0));

			if (randomValue < validDirections.Count)
			{
				_state.Self.Position += validDirections[_random.Next(validDirections.Count)];
			}
			else
			{
				_state.Self.Bomb = new BombState(_state.Self.BombFuze, _state.Self.BombRadius, _state.Self.Position);
			}
		}

		private static void CheckBonuses()
		{
			for (int i = 0; i < _state.Characters.Count; i++)
			{
				CharacterState character = _state.Characters[i];
				foreach (Vector2Int pos in _state.Bonuses.Keys.ToArray())
				{
					if (pos == character.Position)
					{
						switch (_state.Bonuses[pos])
						{
							case BonusType.Fuze:
								character.BombFuze *= 0.9f;
								break;
							case BonusType.Radius:
								character.BombRadius += 1;
								break;
						}

						_state.Bonuses.Remove(pos);
					}
				}
			}
		}

		private static void SimulateBombs()
		{
			List<CharacterState> killedCharacters = new List<CharacterState>();
			
			for (int i = 0; i < _state.Characters.Count; i++)
			{
				CharacterState character = _state.Characters[i];
				
				if (character.Bomb == null) continue;

				SimulateBomb(character, killedCharacters);
			}

			for (int i = 0; i < killedCharacters.Count; i++)
			{
				if (killedCharacters[i] == _state.Self)
				{
					_state.Self = null;
				}

				_state.Characters.Remove(killedCharacters[i]);
			}
		}

		private static void SimulateBomb(CharacterState character, List<CharacterState> killedCharacters)
		{
			BombState bomb = character.Bomb;

			bomb.RemainingFuze -= TIME_PER_TURN;

			if (bomb.RemainingFuze <= 0)
			{
				int x = bomb.Position.x;
				int y = bomb.Position.y;

				// Center
				ExplodeTile(x, y, killedCharacters);

				// Right
				for (int i = 1; i < bomb.Radius + 1; i++)
				{
					if (_state.GetTerrainTypeAtPos(x + i, y) == TerrainType.Wall) break;

					ExplodeTile(x + i, y, killedCharacters);
				}

				// Left
				for (int i = 1; i < bomb.Radius + 1; i++)
				{
					if (_state.GetTerrainTypeAtPos(x - i, y) == TerrainType.Wall) break;

					ExplodeTile(x - i, y, killedCharacters);
				}

				// Top
				for (int i = 1; i < bomb.Radius + 1; i++)
				{
					if (_state.GetTerrainTypeAtPos(x, y + i) == TerrainType.Wall) break;

					ExplodeTile(x, y + i, killedCharacters);
				}

				// Bottom
				for (int i = 1; i < bomb.Radius + 1; i++)
				{
					if (_state.GetTerrainTypeAtPos(x, y - i) == TerrainType.Wall) break;

					ExplodeTile(x, y - i, killedCharacters);
				}

				character.Bomb = null;
			}
		}

		private static void ExplodeTile(int x, int y, List<CharacterState> killedCharacters)
		{
			Vector2Int pos = new Vector2Int(x, y);

			for (int i = 0; i < _state.Characters.Count; i++)
			{
				if (_state.Characters[i].Position == pos)
				{
					killedCharacters.Add(_state.Characters[i]);
				}
			}
			
			if (_state.GetTerrainTypeAtPos(x, y) == TerrainType.BreakableWall)
			{
				_state.Map[x, y] = TerrainType.Floor;

				if (_random.NextDouble() < GameManagerScript.Instance.Map.ItemDropProbability)
				{
					BonusType item = (BonusType)_random.Next(2);
					_state.Bonuses.Add(new Vector2Int(x, y), item);
				}
			}
		}
	}
}