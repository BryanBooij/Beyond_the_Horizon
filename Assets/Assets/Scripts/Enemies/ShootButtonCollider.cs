using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class ShootButtonCollider : MonoBehaviour
    {
        public static Collider2D Instance { get; private set; }

        void Awake()
        {
            Instance = GetComponent<Collider2D>();
        }
    }
}