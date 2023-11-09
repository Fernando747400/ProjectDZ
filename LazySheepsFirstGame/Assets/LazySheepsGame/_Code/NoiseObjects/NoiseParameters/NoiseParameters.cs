// Creado Raymundo Mosqueda 21/10/23
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames.DZ
{
    [CreateAssetMenu(menuName = "LazySheeps/AI/NoiseParameters")]

    public class NoiseParameters : ScriptableObject
    {
        public LayerMask layerMask;
        public float baseRadius;
        public float loudness;
        public bool dangerous;
    }
}
