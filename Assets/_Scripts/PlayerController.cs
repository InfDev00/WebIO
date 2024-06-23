

using System;
using _Scripts.UI;
using Photon.Pun;
using UnityEngine;

namespace _Scripts
{
    public class PlayerController : MonoBehaviourPun, IPunObservable
    {
        [Header("move")]
        public float moveSpeed = 5f;
        private Vector2 _moveVector;
        public JoyStick joyStick;

        [Header("exp")] 
        public int level = 0;
        private int _exp;
        private int _maxExp;
        public int objExp = 1;

        public Action<int, int> OnObjUpdate = null;
        public Action OnDamaged = null;
        
        private Rigidbody _rigidBody;
        private Vector3 _remotePosition;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            LevelUp();
            OnObjUpdate?.Invoke(_exp, _maxExp);
        }

        private void FixedUpdate()
        {
            if (photonView.IsMine) _moveVector = joyStick.InputVector;
            Move();
        }
        private void Move()
        {    
            if (!photonView.IsMine)
            {
                transform.position = Vector3.Lerp(transform.position, _remotePosition, 10 * Time.deltaTime);
            }
            else
            {
                var force = new Vector3(_moveVector.x, 0, _moveVector.y) * moveSpeed;
                _rigidBody.AddForce(force);
            }

        }

        private void LevelUp()
        {
            level += 1;
            
            var scale = 1 + (level - 1) * 0.5f;
            transform.localScale = new Vector3(scale, scale, scale);
            
            _maxExp = (int)Mathf.Pow(2, level) + 5;
            _exp = 0;
        }

        public void Damaged()
        {
            OnDamaged?.Invoke();
            if(photonView.IsMine)PhotonNetwork.Destroy(gameObject);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obj"))
            {
                Destroy(other.gameObject);
                _exp += objExp;
                
                if(_exp >= _maxExp) LevelUp();
                
                OnObjUpdate?.Invoke(_exp, _maxExp);
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if(stream.IsWriting)
            {
                stream.SendNext(transform.position);
                stream.SendNext(transform.localScale.x);
            }
            else
            {
                _remotePosition = (Vector3)stream.ReceiveNext();
                var x = (float)stream.ReceiveNext();
                transform.localScale = new Vector3(x, x, x);
            }
        }
    }
}
