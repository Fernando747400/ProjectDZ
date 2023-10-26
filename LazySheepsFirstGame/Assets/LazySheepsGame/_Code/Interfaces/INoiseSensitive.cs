//Creado Raymundo Mosqueda 21/10/23

using UnityEngine;

namespace com.LazyGames.DZ
{
    public interface INoiseSensitive

    {
        public void HearNoise(float intensity, Vector3 position, bool dangerous);
    }
}
