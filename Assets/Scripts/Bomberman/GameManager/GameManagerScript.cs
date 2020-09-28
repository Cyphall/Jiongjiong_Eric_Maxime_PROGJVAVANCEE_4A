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
        private GameObject _characterPrefab;

        public MapScript Map { get; private set; }

        public CharacterScript Character1 { get; private set; }
        public CharacterScript Character2 { get; private set; }

        public static GameManagerScript Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Map = Instantiate(_mapPrefab).GetComponent<MapScript>();
            
            Character1 = Instantiate(_characterPrefab, Map.transform).GetComponent<CharacterScript>();
            Character1.SetController(new PlayerCharacterController());
        }
    }
}