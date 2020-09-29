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

        public bool Running { get; set; } = true;

        public static GameManagerScript Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void Pause()
        {
            Running = false;
        }

        public void Unpause()
        {
            Running = true;
        }

        private void Start()
        {
            Map = Instantiate(_mapPrefab).GetComponent<MapScript>();
            
            Character1 = Instantiate(_characterPrefab, new Vector3(0, 0, Map.Height - 1), Quaternion.identity, Map.transform).GetComponent<CharacterScript>();
            Character1.SetController(new PlayerCharacterController());
            Character1.SetMaterial(_character1Material);
            
            Character2 = Instantiate(_characterPrefab, new Vector3(Map.Width - 1, 0, 0), Quaternion.identity, Map.transform).GetComponent<CharacterScript>();
            Character2.SetController(new RandomCharacterController());
            Character2.SetMaterial(_character2Material);
        }
    }
}