using UnityEngine;

namespace com.LazyGames.DZ
{
    [CreateAssetMenu(menuName = "LazySheeps/AI/AnimDataSO")]
    public class AnimDataSO : ScriptableObject
    {
        public string idleAnim;
        public string walkAnim;
        public string runAnim;
        public string forwardHitAnim;
        public string rightHitAnim;
        public string leftHitAnim;
        public string deathAnim;
        public string attackAnim;
    }
}
