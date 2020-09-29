using System;
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

        public CharacterScript Character1 { get; private set; }
        public CharacterScript Character2 { get; private set; }

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
            
            Character1 = Instantiate(_characterPrefab, new Vector3(0, 0, Map.Height - 1), Quaternion.identity, Map.transform).GetComponent<CharacterScript>();
            Character1.SetController(new PlayerCharacterController());
            Character1.SetMaterial(_character1Material);
            
            Character2 = Instantiate(_characterPrefab, new Vector3(Map.Width - 1, 0, 0), Quaternion.identity, Map.transform).GetComponent<CharacterScript>();
            Character2.SetController(new RandomCharacterController());
            Character2.SetMaterial(_character2Material);
        }

        private void LateUpdate()
        {
            CheckPlayers();
        }

        public void CheckPlayers()
        {
            if (!Character1.gameObject.activeSelf && !Character2.gameObject.activeSelf)
            {
                Running = false;
                _victoryMenu.OpenMenu(0);
                return;
            }

            if (!Character1.gameObject.activeSelf)
            {
                Running = false;
                _victoryMenu.OpenMenu(2);
                return;
            }

            if (!Character2.gameObject.activeSelf)
            {
                Running = false;
                _victoryMenu.OpenMenu(1);
            }
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}