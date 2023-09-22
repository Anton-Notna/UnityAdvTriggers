using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdvTriggers
{
    public abstract class Trigger : MonoBehaviour
    {
        public abstract bool Contains(Vector3 point);
    }
}