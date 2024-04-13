using UnityEngine;

namespace AdvTriggers
{
    public class SphereTrigger : Trigger
    {
        [SerializeField]
        private float _radius = 2;
        [Space]
        [SerializeField]
        private bool _gizmos;
        [SerializeField, HideInInspector]
        private float _sqrRadius;
        [Space]
        [SerializeField, ColorUsage(false)]
        private Color _gizmosColor = Color.green;

        private void OnValidate() 
        {
            _radius = Mathf.Max(_radius, 0f);
            _sqrRadius = _radius * _radius;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawWireSphere(transform.position, _radius);
            Color color = _gizmosColor;
            color.a = 0.3f;
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, _radius);
        }

        public override bool Contains(Vector3 point)
        {
            float sqrDistance = (transform.position - point).sqrMagnitude;
            return sqrDistance <= _sqrRadius;
        }
    }
}