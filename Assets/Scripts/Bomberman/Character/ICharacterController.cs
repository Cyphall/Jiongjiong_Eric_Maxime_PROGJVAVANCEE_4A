using UnityEngine;

namespace Bomberman.Character
{
	public interface ICharacterController
	{
		RequestedAction Update(CharacterScript character);
	}
}