using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class EnemyBall : MonoBehaviour
    {
        public float moveSpeed = 5f;
        
        private Rigidbody _rigidBody;
        private Vector2 _moveVector;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _moveVector = Random.insideUnitCircle.normalized;

            var vec = new Vector3(_moveVector.x, 0, _moveVector.y) * 100f * moveSpeed;
            _rigidBody.AddForce(vec);
        }

        private void OnCollisionEnter(Collision other)
        {
            if(other.gameObject.TryGetComponent<PlayerController>(out var player)) player.Damaged();
            else if(other.gameObject.CompareTag("Wall"))
            {
                var normal = other.contacts[0].normal + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                
                _moveVector = _rigidBody.velocity.normalized;
                _moveVector = Vector2.Reflect(_moveVector, new Vector2(normal.x, normal.z)).normalized;
                
                _rigidBody.velocity = Vector3.zero;

                var vec = new Vector3(_moveVector.x, 0, _moveVector.y) * 100f * moveSpeed;
                _rigidBody.AddForce(vec);
            }
        }
    }
}