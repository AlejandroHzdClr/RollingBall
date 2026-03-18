using System;
using System.Collections.Generic;
using System.Numerics;
using Environmet;
using Player;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Player
{
    public class PlayerController : PlayerSystem
    {

        private Vector3 movementDirection;
        private List<float> massTypes = new List<float>();
        private int massPosition;
        private float radius;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        protected override void Awake()
        {
            base.Awake();
            massTypes.Add(3f);
            radius = transform.localScale.x / 2;
        }

        // Update is called once per frame
        void Update()
        {
            float hInput = Input.GetAxisRaw("Horizontal");
            float vInput = Input.GetAxisRaw("Vertical");
            if (main.Is2D)
            {
                movementDirection = new Vector3(0,0, hInput).normalized;
            }
            else
            {
                movementDirection = new Vector3(hInput,0, vInput).normalized;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Physics.Raycast(transform.position, Vector3.down,radius + 0.1f))
                {
                    //Debug.Log("Puede saltar, tiene debajo algo");
                    main.Rb.AddForce(Vector3.up * main.JumpForce, ForceMode.Impulse);
                }
            }

            ChangeMass();
        }

        private void ChangeMass()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (massPosition > 0)
                {
                    massPosition--;
                }
            
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (massPosition < massTypes.Count - 1)
                {
                    massPosition++;
                }
            }

            main.Rb.mass = massTypes[massPosition];
        }

        private void FixedUpdate()
        {
            main.Rb.AddForce(movementDirection * main.MovementForce,ForceMode.Force);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("WindTower"))
            {
                main.Rb.AddForce(Vector3.up * main.WindPower,ForceMode.Force);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("DestroyWall"))
            {
                if (main.Rb.mass >= 5)
                {
                    Destroy(other.gameObject);
                }
            }

            if (other.CompareTag("PowerUp"))
            {
                MassAdder adder = other.GetComponent<MassAdder>();

                if (massTypes.Contains(0.5f))
                {
                    adder.AddingMass(massTypes);
                }
                else
                {
                    adder.AddingMass(massTypes);
                }
                
                massTypes.Sort();
            }
        }
    }

}
