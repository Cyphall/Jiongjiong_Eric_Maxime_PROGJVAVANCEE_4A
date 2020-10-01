using UnityEngine;

namespace Bomberman.Character.MCTS
{
	public class MCTSCharacterController : ICharacterController
	{
		private float _delay;
		private const int TREE_DEPTH = 5;
		
		public RequestedAction Update(CharacterScript character)
		{
			_delay += Time.deltaTime;

			if (_delay <= GameSimulator.TIME_PER_TURN)
				return new RequestedAction();
			
			_delay = 0;

			Node root = new Node(new GameState(character));

			for (int i = 0; i < 20; i++)
			{
				root.Expand();
			}

			return root.GetBestAction();
		}
	}
}