using System.Collections.Generic;
using Environment;
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
        
        private AudioSource audioComp;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        protected override void Awake()
        {
            base.Awake();
            //massTypes.Add(0.5f);
            massTypes.Add(3f);
            //massTypes.Add(10f);
            radius = transform.localScale.x / 2;
            rend = GetComponent<MeshRenderer>();
            baseColor = rend.material.color;
            audioComp = GetComponent<AudioSource>();
            main.CanMove = true;
        }

        // Update is called once per frame
        void Update()
        {
            float hInput = Input.GetAxisRaw("Horizontal");
            float vInput = Input.GetAxisRaw("Vertical");
            Vector3 inputDir = new Vector3(hInput, 0, vInput).normalized;
            bool isPressingWasd = inputDir != Vector3.zero;
            if (main.CanMove)
            {
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
                audioComp.Play();

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
                main.Rb.AddForce(other.transform.up * main.WindPower,ForceMode.Force);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("DestroyWall"))
            {
                if (main.Rb.mass >= 5)
                {
                    WallCanDestroy wall = other.gameObject.GetComponent<WallCanDestroy>();
                    AudioSource soundWall = wall.GetComponent<AudioSource>();
                    
                    //Todo esto rompe la pared, pero a la vez sonando el sonido
                    soundWall.Play();
                    foreach (var col in other.GetComponentsInChildren<Collider>())
                        col.enabled = false;
                    foreach (var mesh in other.GetComponentsInChildren<MeshRenderer>())
                        mesh.enabled = false;
                    Destroy(other.gameObject, soundWall.clip.length);
                }
            }

            if (other.CompareTag("PowerUp"))
            {
                MassAdder adder = other.GetComponent<MassAdder>();
                adder.AddingMass(massTypes);

                foreach (var col in other.GetComponentsInChildren<Collider>())
                    col.enabled = false;

                foreach (var mesh in other.GetComponentsInChildren<MeshRenderer>())
                    mesh.enabled = false;

                float currentMass = main.Rb.mass;
                massTypes.Sort();
                
                massPosition = massTypes.IndexOf(currentMass);
            }
        }
    }

}
