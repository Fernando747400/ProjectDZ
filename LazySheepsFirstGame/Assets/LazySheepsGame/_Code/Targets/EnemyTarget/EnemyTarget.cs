using System.Collections;
using com.LazyGames.DZ;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Serialization;

namespace com.LazyGames.DZ
{
    public class EnemyTarget : MonoBehaviour
    {
        #region Serialized Fields

        [Header(" Controllers")]
        [SerializeField] private AdvanceAnimatorController animatorController;
        [SerializeField] private EnemyController enemyController;
        [SerializeField] private AnimDataSO _animDataSo;
       
        [Header("Body Parts")]
        [HideInInspector] private EnemyBodyPart hittedBodyPart;
        [SerializeField] private Vector2 leftSide;
        [SerializeField] private Vector2 rightSide;
        [SerializeField] private Vector2 Head;

        [Header("Animations")]
        [SerializeField] private string animHead;
        [SerializeField] private string animLeftSide;
        [SerializeField] private string animRightSide;
        
        [Header("Particles")]
        // [SerializeField] private ParticleSystem bloodEffect;
       
        #endregion


        #region private variables
        
        private Vector3 currentPosition;
        
        #endregion


        #region Unity Methods
        
        private void Start()
        {
            enemyController.OnAnimEvent += HandleHitPoint;
            animHead = _animDataSo.forwardHitAnim;
            animLeftSide = _animDataSo.leftHitAnim;
            animRightSide = _animDataSo.rightHitAnim;
        }

        #endregion

        #region private methods

        

        private void HandleHitPoint(Vector3 direction)
        {
            currentPosition = transform.localPosition;
            Vector3 hitPointPosition = direction - currentPosition;
            float angle = Vector3.SignedAngle(hitPointPosition, transform.forward, Vector3.up);
            
            Debug.DrawRay(transform.localPosition, hitPointPosition, Color.red, 5f);
            
            hittedBodyPart = GetBodyPart(angle);
            animatorController.SetAnim(GetAnimName());
            SetBleedingEffect(direction);
            
            Debug.Log(angle.ToString().SetColor("#16B1F5") + "    =  "+ hittedBodyPart.ToString().SetColor("#16B1F5"));
            
        }

        private EnemyBodyPart GetBodyPart(float angle)
        {
            if (angle > rightSide.x && angle < rightSide.y)
            {
                return EnemyBodyPart.RightSide;
            }
            if (angle < leftSide.x && angle > leftSide.y)
            {
                return EnemyBodyPart.LeftSide;
            }
            
            return EnemyBodyPart.Head;
            
        }
       
        private string GetAnimName()
        {
            switch (hittedBodyPart)
            {
                case EnemyBodyPart.Head:
                    return animHead;
                case EnemyBodyPart.LeftSide:
                    return animLeftSide;
                case EnemyBodyPart.RightSide:
                    return animRightSide;
                default:
                    return "None";
            }
        }
        private void SetBleedingEffect(Vector3 position)
        {
            GameObject bloodParticle = PoolManager.Instance.SpawnPool(PoolKeys.BLOOD_PARTICLE_POOLKEY);
            bloodParticle.transform.position = position;
            StartCoroutine(DespawnParticle(bloodParticle));
        }
        private IEnumerator DespawnParticle(GameObject particle)
        {
            yield return new WaitForSeconds(1f);
            LeanPool.Despawn(particle);
        }
        
        #endregion


        
    }

    public enum EnemyBodyPart
    {
        Head,
        LeftSide,
        RightSide,
    }
}