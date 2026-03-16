using UnityEngine;

namespace Player
{
    public class PlayerSystem : MonoBehaviour
    {
        
        protected PlayerMain main;
        
        protected virtual void Awake()
        {
            //Todos los PlayerSystems... cuando nazcan (Awake) buscaran a su controlador (PlayerMain)
            main = transform.root.GetComponent<PlayerMain>();
            
        }
    }
}