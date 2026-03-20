using System.Collections.Generic;
using Environmet;
using Unity.VisualScripting;
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
        private float currentRadius;

        [SerializeField] private float maxCharge;
        [SerializeField] private float chargeSpeed;
        private float charge;
        private Vector3 lastDirection;
        
        private MeshRenderer rend;
        private Color baseColor;
        
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        protected override void Awake()
        {
            base.Awake();
            //massTypes.Add(0.5f);
            massTypes.Add(3f);
            //.Add(10f);
            radius = transform.localScale.x / 2;
            rend = GetComponent<MeshRenderer>();
            baseColor = rend.material.color;
        }

        // Update is called once per frame
        void Update()
        {
            float hInput = Input.GetAxisRaw("Horizontal");
            float vInput = Input.GetAxisRaw("Vertical");
            Vector3 inputDir = new Vector3(hInput, 0, vInput).normalized;
            bool isPressingWasd = inputDir != Vector3.zero;
            if (Mathf.Approximately(main.Rb.mass, 3f))
            {
                MovingBall(hInput, vInput);
            }

            if (Mathf.Approximately(main.Rb.mass, 10f))
            {
                ImpulseBall(isPressingWasd, inputDir);
            }
            
            ChangeMass();
        }

        private void ImpulseBall(bool isPressingWasd, Vector3 inputDir)
        {
            if (isPressingWasd)
            {
                charge += chargeSpeed * Time.deltaTime;
                charge = Mathf.Clamp(charge, 0f, maxCharge);
                
                float t = charge / maxCharge;
                rend.material.color = Color.Lerp(baseColor, Color.red, t);
            }

            if (!isPressingWasd && charge > 0f)
            {
                main.Rb.AddForce(lastDirection * charge, ForceMode.Impulse);
                charge = 0f;

                rend.material.color = baseColor;
            }

            if (isPressingWasd)
            {
                lastDirection = inputDir;
            }
        }

        private void MovingBall(float hInput, float vInput)
        {
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
                currentRadius = radius;
                MassAdder adder = other.GetComponent<MassAdder>();
                adder.AddingMass(massTypes);
                
                massTypes.Sort();
                main.Rb.mass = currentRadius;
            }
        }
    }

}
