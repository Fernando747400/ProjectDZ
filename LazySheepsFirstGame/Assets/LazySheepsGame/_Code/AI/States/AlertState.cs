using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.LazyGames.DZ
{
    public class AlertState : EnemyState
    {
        public override void EnterState()
        {
            //perform alerted animation
        }

        public override void UpdateState()
        {
            //look towards noise source
            
        }

        public override void ExitState()
        {
            // enter flee state
            // enter aggro state
            // return to wander state
        }
    }
    
}
