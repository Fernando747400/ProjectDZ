using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames
{
    public class Module : MonoBehaviour
    {
        [SerializeField] private ModuleData moduleData;
        public ModuleData MyData => moduleData;
        void Start()
        {

        }

        
    }
}