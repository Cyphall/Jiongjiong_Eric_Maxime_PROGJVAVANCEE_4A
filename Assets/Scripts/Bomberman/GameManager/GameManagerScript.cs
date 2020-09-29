using System;
using System.Collections.Generic;
using Bomberman.Character;
using Bomberman.Terrain;
using UnityEngine;

namespace Bomberman.GameManager
{
    public class GameManagerScript : MonoBehaviour
    {
        [SerializeField]
        private GameObject _mapPrefab;
        [SerializeField]
        private GameObject _victoryMenuPrefab;
        [SerializeField]
        private GameObject _characterPrefab;
        [SerializeField]
        private Material _character1Material;
        [SerializeField]
        private Material _character2Material;
        

        public MapScript Map { get; private set; }
        
        public List<CharacterScript> Characters { get; } = new List<CharacterScript>();

        public bool Running { get; set; } = true;

        public static GameManagerScript Instance { get; private set; }
        
        private VictoryMenuScript _victoryMenu;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Map = Instantiate(_mapPrefab).GetComponent<MapScript>();

            _victoryMenu = Instantiate(_victoryMenuPrefab).GetComponent<VictoryMenuScript>();

            {
                CharacterScript character = Instantiate(_characterPrefab, new Vector3(0, 0, Map.Height - 1), Quaternion.identity, Map.transform).GetComponent<CharacterScript>();
                character.Initialize(new PlayerCharacterController(), _character1Material, "Player 1");
                Characters.Add(character);
            }
            {
                CharacterScript character = Instantiate(_characterPrefab, new Vector3(Map.Width - 1, 0, 0), Quaternion.identity, Map.transform).GetComponent<CharacterScript>();
                character.Initialize(new RandomCharacterController(), _character2Material, "Player 2");
                Characters.Add(character);
            }
        }

        private void LateUpdate()
        {
            CheckPlayers();
        }

        public void CheckPlayers()
        {
            int aliveCount = 0;
            CharacterScript lastAlive = null;
            for (int i = 0; i < Characters.Count; i++)
            {
                if (Characters[i].gameObject.activeSelf)
                {
                    aliveCount++;
                    lastAlive = Characters[i];
                }
            }

            if (aliveCount == 1)
            {
                Running = false;
                _victoryMenu.OpenMenu(lastAlive.name);
            }
            else if (aliveCount == 0)
            {
                Running = false;
                _victoryMenu.OpenMenu(null);
            }
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}