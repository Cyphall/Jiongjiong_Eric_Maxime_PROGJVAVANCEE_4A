using System;
using System.Collections.Generic;
using Bomberman.GameManager;
using Bomberman.Terrain;
using UnityEngine;
using Random = System.Random;

namespace Bomberman.Character
{
	public class RandomCharacterController : ICharacterController
	{
		private float _delay;
		private Random _random = new Random((int)DateTime.Now.Ticks);
		
		public RequestedActions Update(CharacterScript character)
		{
			_delay += Time.deltaTime;
			
			RequestedActions actions = new RequestedActions();

			if (_delay <= 0.3f)
				return actions;
			
			_delay = 0;

			Vector2Int[] possibleDirections = {
				new Vector2Int(1, 0),
				new Vector2Int(-1, 0),
				new Vector2Int(0, 1),
				new Vector2Int(0, -1)
			};

			List<Vector2Int> validDirections = new List<Vector2Int>();
			
			foreach (Vector2Int direction in possibleDirections)
			{
				Vector2Int pos = character.Position + direction;
				if (GameManagerScript.Instance.Map.GetTerrainTypeAtPos(pos.x, pos.y) == TerrainType.Floor)
				{
					validDirections.Add(direction);
				}
			}

			actions.Move = validDirections[_random.Next(validDirections.Count)];

			actions.DropBomb = character.Bomb.IsReady;
			return actions;
		}
	}
}