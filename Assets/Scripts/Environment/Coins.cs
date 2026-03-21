using System;
using Player;
using UnityEngine;

namespace Environment
{
    public class Coins : MonoBehaviour
    {
        [SerializeField] private int pointNum;
        [SerializeField] private float velocidadRotacion;

        private AudioSource audioComp;

        private void Start()
        {
            audioComp = GetComponent<AudioSource>();
        }

        void Update()
        {
            transform.Rotate((Vector3.forward * (velocidadRotacion * Time.deltaTime)));
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerMain player = other.GetComponent<PlayerMain>();
                player.Points += pointNum;
                audioComp.Play();
                foreach (var col in GetComponentsInChildren<Collider>())
                    col.enabled = false;
                foreach (var mesh in GetComponentsInChildren<MeshRenderer>())
                    mesh.enabled = false;
                Destroy(gameObject, audioComp.clip.length);
            }
        }
    }
}

