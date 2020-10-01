﻿using UnityEngine;

namespace Bomberman.Character
{
	public class PlayerCharacterController : ICharacterController
	{
		public RequestedAction Update(CharacterScript character)
		{
			RequestedAction actions = new RequestedAction();
			
			if (Input.GetKeyDown(KeyCode.Z))
			{
				actions.Move = new Vector2Int(0, 1);
			}
			else if (Input.GetKeyDown(KeyCode.S))
			{
				actions.Move = new Vector2Int(0, -1);
			}
			else if (Input.GetKeyDown(KeyCode.Q))
			{
				actions.Move = new Vector2Int(-1, 0);
			}
			else if (Input.GetKeyDown(KeyCode.D))
			{
				actions.Move = new Vector2Int(1, 0);
			}
			actions.DropBomb = Input.GetKeyDown(KeyCode.Space);
			

			return actions;
		}
	}
}