// Modificado Raymundo Mosqueda 05/10/23

using UnityEngine;

namespace com.LazyGames 
{
    public interface IGeneralTarget
    {
        void ReceiveAggression(Vector3 direction, float velocity, float dmg = 0);
    }
    
}
