using System;
using NUnit.Framework;
using UnityEngine;

namespace Player
{
    public class PlayerMain : MonoBehaviour
    {
        [field: SerializeField] public float GroundPoundForce { get; private set; }
        [field: SerializeField] public float MovementForce { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
        [field: SerializeField] public float WindPower { get; private set; }
        [field: SerializeField] public GameObject FollowCamera { get; private set; }
        [field: SerializeField] public GameObject Camera2D { get; private set; }

        public float OriginalJumpForce { get; private set; }
        public int Points { get; set; }
        public float TimePlayed { get; private set; }
        
        
        public Rigidbody Rb { get; private set; }
        public bool Is2D { get; set; }
        public bool CanMove { get; set; }
        
        private void Awake()
        {
            Rb = GetComponent<Rigidbody>();
            OriginalJumpForce = JumpForce;
        }

        public void ChangeJump(float newJumpForce)
        {
            JumpForce = newJumpForce;
        }

        private void Update()
        {
            if (CanMove)
            {
                TimePlayed += Time.deltaTime;
            }
        }
    }
}