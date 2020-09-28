using UnityEngine;

namespace Bomberman.Character
{
	public interface ICharacterController
	{
		RequestedActions Update(bool bombReady);
	}
}