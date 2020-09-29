﻿using System;
using Bomberman.Bomb;
using Bomberman.GameManager;
using Bomberman.Terrain;
using UnityEngine;

namespace Bomberman.Character
{
	public class CharacterScript : MonoBehaviour
	{
		private ICharacterController _controller;

		[SerializeField]
		private MeshRenderer _meshRenderer;

		[SerializeField]
		private GameObject _bombPrefab;
		private BombScript _bomb;

		[SerializeField]
		[Range(0f, 5f)]
		private float _bombFuze;
		public float BombFuze
		{
			get => _bombFuze;
			set => _bombFuze = value;
		}
		
		[SerializeField]
		[Range(1, 5)]
		private int _bombRadius;
		public int BombRadius
		{
			get => _bombRadius;
			set => _bombRadius = value;
		}
		
		private Vector2Int _position;
		public Vector2Int Position
		{
			get => _position;
			set
			{
				Vector2Int posDiff = value - _position;
				if (posDiff.sqrMagnitude > 1) throw new InvalidOperationException("Cannot move in diagonal");
				if (posDiff.sqrMagnitude == 0) return;

				if (GameManagerScript.Instance.Map.GetTerrainTypeAtPos(value.x, value.y) == TerrainType.Floor)
				{
					_position = value;
					transform.position = new Vector3(value.x, 0, value.y);
					GameManagerScript.Instance.Map.TryApplyItemAtPos(value.x, value.y, this);
				}
			}
		}

		public void SetMaterial(Material material)
		{
			Material[] materials = _meshRenderer.sharedMaterials;
			materials[8] = material;
			_meshRenderer.sharedMaterials = materials;
		}

		private void Start()
		{
			_position = new Vector2Int((int)transform.position.x, (int)transform.position.z);
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
