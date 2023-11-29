// Creado Raymundo Mosqueda 07/09/23
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace com.LazyGames.DZ
{
    public class AggroState : EnemyState
    {
        private bool _attacking;
        public override void EnterState()
        {
            Controller.agent.speed = Controller.parameters.aggroSpeed;
            Controller.doHear = false;
            
        }

        public override void UpdateState()
        {
            Controller.agent.SetDestination(Controller.player.transform.position);
            if(CheckDistance())
                Attack();
        }
        
        public override void ExitState()
        {
            Controller.hP = Controller.parameters.maxHp;
            Controller.agent.speed = Controller.parameters.baseSpeed;
            Controller.doHear = true;
        }
        
        public override void SetAnimation()
        {
            
            if(_attacking)return;
            var newAnimState = "";
            
            switch ( Controller.agent.velocity.magnitude)
            {
                case var n when n <= 0.1f:
                    newAnimState = Controller.animDataSo.idleAnim;
                    break;
                case var n when n > 0.1f && n <= 2.1f:
                    newAnimState = Controller.animDataSo.walkAnim;
                    break;
                case var n when n > 2.1f:
                    newAnimState = Controller.animDataSo.runAnim;
                    break;
                default:
                    newAnimState = Controller.animDataSo.idleAnim;
                    break;
            }
            
            if (newAnimState == Controller.currentAnimState) return;
            Controller.animController.SetAnim(newAnimState);
            Controller.currentAnimState = newAnimState; // Update the current state
        }

        private void Attack()
        {
            var newAnimState = "";

            _attacking = true;
            Controller.SendAggression();
            Controller.doneAttacking = false;
            newAnimState = Controller.animDataSo.attackAnim;
            if (newAnimState == Controller.currentAnimState) return;
            Controller.animController.SetAnim(newAnimState);
            Controller.currentAnimState = newAnimState; // Update the current state
            StartCoroutine(corWaitForAttack());
            Controller.doneAttacking = true;
        }
        
        private IEnumerator corWaitForAttack()
        {
            yield return new WaitForSeconds(1.5f);
            _attacking = false;
        }
        private bool CheckDistance()
        {
            var dist = Vector3.Distance(transform.position, Controller.player.transform.position);
            if(dist > Controller.agent.stoppingDistance) return false;
            return true;
        }

        private void OnGeometryChanged()
        {
            if(Controller.agent.pathStatus == NavMeshPathStatus.PathComplete)return;
            Controller.target = GetClosestWall().transform.position;
        }
        
        private GameObject GetClosestWall()
        {
            System.Collections.Generic.List<GameObject> walls = Controller.sceneWallsSo.Walls;

            if (walls.Count == 0)
            {
                return null;
            }

            GameObject closestObject = null;
            float closestDistance = Mathf.Infinity;

            foreach (GameObject obj in walls)
            {
                if (obj != null)
                {
                    float distance = Vector3.Distance(obj.transform.position, Controller.player.transform.position);

                    if (distance < closestDistance)
                    {
                        closestObject = obj;
                        closestDistance = distance;
                    }
                }
            }
            return closestObject;
        }
    }
}
