using System;
using Bomberman.Bomb;
using Bomberman.GameManager;
using Bomberman.Terrain;
using TMPro.EditorUtilities;
using UnityEngine;

namespace Bomberman.Character
{
	public class CharacterScript : MonoBehaviour
	{
		private ICharacterController _controller;

		[SerializeField]
		private GameObject _bombPrefab;
		private BombScript _bomb;

		[SerializeField]
		[Range(0f, 5f)]
		private float _bombFuze;
		
		[SerializeField]
		[Range(1, 5)]
		private int _bombRadius;

		private Vector2Int _position;
		public Vector2Int Position
		{
			get => _position;
			set
			{
				Vector2Int posDiff = value - _position;
				if (posDiff.sqrMagnitude > 1) throw new InvalidOperationException("Cannot move in diagonal");

				if (GameManagerScript.Instance.Map.GetTerrainTypeAtPos(value.x, value.y) == TerrainType.Floor)
				{
					_position = value;
					transform.position = new Vector3(value.x, 0, value.y);
				}
			}
		}

		private void Start()
		{
			_bomb = Instantiate(_bombPrefab, GameManagerScript.Instance.transform).GetComponent<BombScript>();
			_bomb.gameObject.SetActive(false);
		}

		public void SetController(ICharacterController controller)
		{
			_controller = controller;
		}

		private void Update()
		{
			if (_controller == null) return;

			RequestedActions actions = _controller.Update(_bomb.IsReady);

			Position += actions.Move;
			if (actions.Move.sqrMagnitude != 0)
				transform.rotation = Quaternion.LookRotation(new Vector3(actions.Move.x, 0, actions.Move.y));

			if (_bomb.IsReady && actions.DropBomb)
			{
				_bomb.Drop(_bombFuze, _bombRadius, Position);
			}
		}
	}
}
