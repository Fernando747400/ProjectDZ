using System;
using Obvious.Soap;
using UnityEngine;

namespace com.LazyGames.Dz
{
    public class GordyEfectManagger : MonoBehaviour
    {
        [SerializeField] private BoolVariable _activeKey;
        [SerializeField] private FloatVariable _mana;
        [SerializeField] private bool _isDamage;
        [SerializeField] private int _castPoints;
        [SerializeField] private LayerMask _layerMask;

        public void CastEfect()
        {
            if (_activeKey.Value && _isDamage)
            {
                DoDamage();
                _mana.Value = 0;
            }
            else if (_activeKey.Value && !_isDamage)
            {
                Debug.Log("heal points" + _castPoints);
                _mana.Value = 0;
            }
        }
    
        public void DoDamage()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, 10, _layerMask);
            
            if(hits.Length == 0) return;
            foreach (var col in hits)
            {
                if (!col.gameObject.TryGetComponent<IGeneralTarget>(out var generalTarget)) continue;
                if(!col.CompareTag("Enemy")) continue; 
                generalTarget.ReceiveAggression(Vector3.Normalize(col.transform.position - transform.position), 1, _castPoints);
            }
        }

        private void OnDrawGizmos()
        {
            if (_activeKey.Value && _isDamage)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, 10);
            }
        }
    } 
}