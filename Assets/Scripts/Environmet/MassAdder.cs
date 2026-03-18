using System.Collections.Generic;
using UnityEngine;

namespace Environmet
{
    public class MassAdder : MonoBehaviour
    {
        [SerializeField] private float mass;
        
        public void AddingMass(List<float> massList)
        {
            massList.Add(mass);
            Destroy(this.gameObject);
        }
    }
}