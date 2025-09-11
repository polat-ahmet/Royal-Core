using RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Modules.Health;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.MVC.View;
using UnityEngine;

namespace RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Agent.Weapon.Scripts.Bullet
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public sealed class BulletView : MonoBehaviour, IView
    {
        private float _damage;
        private Rigidbody2D _rb;
        private float _ttl;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _ttl -= Time.deltaTime;
            if (_ttl <= 0f) Return();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<IHittable>(out var h))
            {
                h.Hit(_damage);
                Return();
            }
        }

        public void Fire(BulletSpawnInfo info)
        {
            transform.position = info.Position;
            var vel = info.Direction * info.Speed;
            _rb.linearVelocity = vel;
            _damage = info.Damage;
            _ttl = info.LifeTime;
        }

        private void Return()
        {
            //TODO return to pool
            Destroy(gameObject);
        }
    }
}