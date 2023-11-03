// Creado Raymundo Mosqueda 07/09/23
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace com.LazyGames.DZ
{
    public class AggroState : EnemyState
    {
        public override void EnterState()
        {
            Controller.agent.speed = Controller.parameters.aggroSpeed;
            Controller.doHear = false;

        }

        public override void UpdateState()
        {
            Controller.agent.SetDestination(Controller.player.transform.position);
            CheckDistance();
        }
        
        public override void ExitState()
        {
            Controller.hP = Controller.parameters.maxHp;
            Controller.agent.speed = Controller.parameters.baseSpeed;
            Controller.doHear = true;
        }
        
        private void CheckDistance()
        {
            var dist = Vector3.Distance(transform.position, Controller.player.transform.position);
            if(dist > Controller.agent.stoppingDistance) return;
            Attack();
        }

        private void Attack()
        {
            // play attack animation
            // deal damage to player
        }
        
        private void OnGeometryChanged()
        {
            if(Controller.agent.pathStatus == NavMeshPathStatus.PathComplete)return;
            Controller.target = GetClosestWall().transform.position;
        }
        
        private GameObject GetClosestWall()
        {
            List<GameObject> walls = Controller.sceneWallsSo.Walls;

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
