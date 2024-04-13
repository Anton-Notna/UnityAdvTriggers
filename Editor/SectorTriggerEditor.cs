using UnityEditor;
using UnityEngine;

namespace AdvTriggers
{
    [CustomEditor(typeof(SectorTrigger)), CanEditMultipleObjects]
    public class SectorTriggerEditor : Editor
    {
        private SectorTrigger _sector;
        private bool? _formatting;

        private static Vector3 ToWorld(MonoBehaviour monoBehaviour, Vector3 local)
            => monoBehaviour.transform.TransformDirection(local) + monoBehaviour.transform.position;

        private static Vector3 ToLocal(MonoBehaviour monoBehaviour, Vector3 world)
        {
            Vector3 local = world - monoBehaviour.transform.position;
            return monoBehaviour.transform.InverseTransformDirection(local);
        }

        private void OnSceneGUI()
        {
            RefreshFormatting();

            _sector = (SectorTrigger)target;
            Undo.RecordObject(target, "SectorTrigger modification");

            _sector.Ceiling = Format(DrawVectorLength(_sector.Ceiling, Vector3.up, _sector.GizmosColor, 0.22f), _sector.Ceiling, _sector.Snap);
            _sector.Floor = Format(DrawVectorLength(_sector.Floor, -Vector3.up, _sector.GizmosColor, 0.2f), _sector.Floor, _sector.Snap);

            _sector.Radius = Format(DrawVectorLength(_sector.Radius, Vector3.forward, _sector.GizmosColor, 0.15f), _sector.Radius, _sector.Snap);
            _sector.InnerRadius = Format(DrawVectorLength(_sector.InnerRadius, Vector3.forward, _sector.GizmosColor, 0.12f), _sector.InnerRadius, _sector.Snap);

            DrawArks(Vector3.up * _sector.Ceiling, _sector.Radius, _sector.Angle, _sector.GizmosColor);
            DrawArks(-Vector3.up * _sector.Floor, _sector.Radius, _sector.Angle, _sector.GizmosColor);
            DrawArks(Vector3.up * _sector.Ceiling, _sector.InnerRadius, _sector.Angle, _sector.GizmosColor);
            DrawArks(-Vector3.up * _sector.Floor, _sector.InnerRadius, _sector.Angle, _sector.GizmosColor);

            DrawRectangle(_sector.Angle, _sector.GizmosColor);
            DrawRectangle(-_sector.Angle, _sector.GizmosColor);

            Vector3 angle = DrawVector(Quaternion.AngleAxis(_sector.Angle, Vector3.up) * Vector3.forward * _sector.Radius, _sector.GizmosColor);
            float degrees = Vector3.SignedAngle(Vector3.ProjectOnPlane(angle, Vector3.up).normalized, Vector3.forward, -Vector3.up);
            if (degrees < 0)
            {
                if (degrees > -90f)
                    degrees = 0;
                else
                    degrees = 180f;
            }

            _sector.Angle = Format(degrees, _sector.Angle, _sector.AngleSnap);
        }

        private void RefreshFormatting()
        {
            if (Event.current.keyCode == KeyCode.LeftControl)
            {
                bool formatting = Event.current.type == EventType.KeyDown;
                if (formatting != _formatting)
                    _formatting = formatting;
            }
        }

        private void DrawRectangle(float yRotation, Color color, float alpha = 0.05f)
        {
            Vector3[] verts = new Vector3[]
            {
                ToWorld(_sector, Quaternion.AngleAxis(yRotation, Vector3.up) * (Vector3.up * _sector.Ceiling + Vector3.forward * _sector.InnerRadius)),
                ToWorld(_sector, Quaternion.AngleAxis(yRotation, Vector3.up) * (Vector3.up * _sector.Ceiling + Vector3.forward * _sector.Radius)),
                ToWorld(_sector, Quaternion.AngleAxis(yRotation, Vector3.up) * (-Vector3.up * _sector.Floor + Vector3.forward * _sector.Radius)),
                ToWorld(_sector, Quaternion.AngleAxis(yRotation, Vector3.up) * (-Vector3.up * _sector.Floor + Vector3.forward * _sector.InnerRadius)),
            };

            Color colorA = color;
            colorA.a = alpha;
            Handles.DrawSolidRectangleWithOutline(verts, colorA, color);
        }

        private void DrawArks(Vector3 localOffset, float radius, float angle, Color color)
        {
            DrawArc(localOffset, radius, angle, color);
            DrawArc(localOffset, radius, -angle, color);
        }

        private void DrawArc(Vector3 localOffset, float radius, float angle, Color color)
        {
            Handles.color = color;
            Handles.DrawWireArc(
                ToWorld(_sector, localOffset),
                _sector.transform.TransformDirection(Vector3.up),
                _sector.transform.TransformDirection(Vector3.forward),
                angle,
                radius);
        }

        private float DrawVectorLength(float length, Vector3 localDirection, Color color, float size = 0.15f)
        {
            Handles.color = color;
            Handles.DrawDottedLine(_sector.transform.position, ToWorld(_sector, localDirection * length), 10f);
            var fmh_87_17_638282469999501057 = Quaternion.identity; Vector3 worldCeiling = Handles.FreeMoveHandle(
                ToWorld(_sector, localDirection * length),
                size,
                Vector3.zero, Handles.CircleHandleCap);

            Vector3 local = ToLocal(_sector, worldCeiling);
            float result = Vector3.Dot(localDirection, Vector3.Project(local, localDirection));
            return result;
        }

        private Vector3 DrawVector(Vector3 local, Color color)
        {
            Handles.color = color;
            Handles.DrawLine(_sector.transform.position, ToWorld(_sector, local));
            var fmh_102_17_638282469999527728 = Quaternion.identity; Vector3 worldOrientation = Handles.FreeMoveHandle(
                ToWorld(_sector, local),
                0.1f,
                Vector3.zero, Handles.CircleHandleCap);

            return ToLocal(_sector, worldOrientation);
        }

        private float Format(float value, float previousValue, float step)
        {
            if (value == previousValue)
                return value;

            if (_formatting.HasValue == false)
                return value;

            if (_formatting.Value == false)
                return value;

            float delta = value % step;
            float formatted = value - delta;
            bool bigDelta = delta / step >= 0.5f;
            if (bigDelta)
                formatted += step;

            return formatted;
        }
    }
}