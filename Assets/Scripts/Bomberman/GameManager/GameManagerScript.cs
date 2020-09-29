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
        
        [SerializeField]
        private Material _character1Material;
        [SerializeField]
        private Material _character2Material;

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
            
            Character1 = Instantiate(_characterPrefab, new Vector3(0, 0, Map.Height - 1), Quaternion.identity, Map.transform).GetComponent<CharacterScript>();
            Character1.SetController(new PlayerCharacterController());
            Character1.SetMaterial(_character1Material);
            
            Character2 = Instantiate(_characterPrefab, new Vector3(Map.Width - 1, 0, 0), Quaternion.identity, Map.transform).GetComponent<CharacterScript>();
            Character2.SetController(new Player2CharacterController());
            Character2.SetMaterial(_character2Material);
        }
    }
}