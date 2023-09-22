using UnityEngine;

namespace AdvTriggers
{
    public class SectorTrigger : Trigger
    {
        [SerializeField]
        private float _ceiling;
        [SerializeField]
        private float _floor;
        [SerializeField]
        private float _radius;
        [SerializeField]
        private float _innerRadius;
        [SerializeField]
        private float _angle;
        [SerializeField, ColorUsage(false)]
        private Color _gizmosColor = Color.green;

        public float Floor
        {
            get => _floor;
            set => _floor = Mathf.Max(0, value);
        }

        public float Ceiling
        {
            get => _ceiling;
            set => _ceiling = Mathf.Max(0, value);
        }

        public float Radius
        {
            get => _radius;
            set => _radius = Mathf.Max(_innerRadius, value);
        }

        public float InnerRadius
        {
            get => _innerRadius;
            set => _innerRadius = Mathf.Min(Mathf.Max(0, value), _radius);
        }

        public float Angle
        {
            get => _angle;
            set => _angle = Mathf.Clamp(value, 0, 180f);
        }

        public Color GizmosColor => _gizmosColor;

        public override bool Contains(Vector3 point)
        {
            Vector3 localPoint = transform.InverseTransformDirection(point - transform.position);
            float localHeight = Vector3.Dot(Vector3.up, Vector3.Project(localPoint, Vector3.up));
            if (localHeight > _ceiling)
                return false;

            if (localHeight < -_floor)
                return false;

            Vector3 localFlatPoint = Vector3.ProjectOnPlane(localPoint, Vector3.up);
            float sqrDistance = localFlatPoint.sqrMagnitude;
            if (sqrDistance < _innerRadius * _innerRadius)
                return false;

            if (sqrDistance > _radius * _radius)
                return false;

            return Vector3.Angle(localFlatPoint, Vector3.forward) <= _angle;
        }

        private void OnValidate()
        {
            Angle = _angle;
            Radius = _radius;
            InnerRadius = _innerRadius;
            Ceiling = _ceiling;
            Floor = _floor;
        }
    }
}