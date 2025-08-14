using UnityEngine;
using SCP008X.Components;
using Exiled.API.Features;

namespace SCP008X
{
    public static class Extensions
    {
        public static bool Infect(this Player target)
        {
            if (target.GameObject.TryGetComponent(out SCP008 scp008))
                return false;

            target.GameObject.AddComponent<SCP008>();
            return true;
        }

        public static bool Cure(this Player target)
        {
            if (!target.GameObject.TryGetComponent(out SCP008 scp008))
                return false;

            Object.Destroy(scp008);
            return true;
        }
    }
}
