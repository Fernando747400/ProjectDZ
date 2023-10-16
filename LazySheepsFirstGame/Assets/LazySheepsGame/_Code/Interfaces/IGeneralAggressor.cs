// Creado Raymundo Mosqueda 06/10/23

using UnityEngine;

namespace com.LazyGames.DZ
{
    public interface IGeneralAggressor
    {
        bool TryGetGeneralTarget(RaycastHit hit);
        void SendAggression(RaycastHit hit);
    }
}
