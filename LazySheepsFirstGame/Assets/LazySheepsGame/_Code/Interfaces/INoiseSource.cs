//Creado Raymundo Mosqueda 21/10/23

using UnityEditor;
using UnityEngine;

namespace com.LazyGames.DZ
{
    public interface INoiseSource 
    {
        public void MakeNoise(NoiseParameters noiseParameters, float velocity, Vector3 position);
    }
}
