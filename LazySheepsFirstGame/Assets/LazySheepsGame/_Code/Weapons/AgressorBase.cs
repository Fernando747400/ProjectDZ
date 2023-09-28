using UnityEngine;
using com.LazyGames;

namespace com.LazyGames
{
    public class AgressorBase : MonoBehaviour
    {
        [Header("Agressor Base")] [SerializeField]
        private AgressorData agressorData;

        protected bool TryGetGeneralTarget(GameObject target)
        {
            try
            {
                bool result = target.GetComponent<IGeneralTarget>().Type == TargetsType.Enemy;
                return result;
            }
            catch
            {
                return false;
            }
        }
    }
}