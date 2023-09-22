using UnityEngine;

namespace AdvTriggers
{
    public class TriggerTest : MonoBehaviour
    {
        [SerializeField]
        private Trigger _trigger;
        [SerializeField]
        private bool _gizmos;

        private void OnDrawGizmos()
        {
            if (_gizmos == false)
                return;

            if (_trigger == null)
                return;

            bool intersects = _trigger.Contains(transform.position);
            Gizmos.color = intersects ? Color.green : Color.red;
            Gizmos.DrawSphere(transform.position, 0.1f);
            Gizmos.DrawWireSphere(transform.position, 1f);
        }
    }
}