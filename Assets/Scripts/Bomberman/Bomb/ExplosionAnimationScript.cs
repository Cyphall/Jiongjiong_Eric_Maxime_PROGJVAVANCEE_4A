using UnityEngine;

namespace Bomberman.Bomb
{
    public class ExplosionAnimationScript : MonoBehaviour
    {
        private void Start()
        {
            ParticleSystem exp = GetComponent<ParticleSystem>();
            exp.Play();
            Destroy(gameObject, exp.main.duration);
        }
    }
}
