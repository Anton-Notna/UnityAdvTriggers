using UnityEngine;

namespace AdvTriggers
{

    public class SphereTrigger : Trigger
    {
        [SerializeField]
        private float _radius;
        [SerializeField]
        private bool _gizmos;
        [SerializeField, HideInInspector]
        private float _sqrRadius;

        private void OnValidate() 
        {
            _radius = Mathf.Max(_radius, 0f);
            _sqrRadius = _radius * _radius;
        }

        private void OnDrawGizmos()
        {
            if (_gizmos == false)
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _radius);
            Color color = Color.green;
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