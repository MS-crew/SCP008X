using UnityEngine;
using SCP008X.Components;
using Exiled.API.Features;

namespace SCP008X
{
    public class Methods
    {
        public static bool Infect(Player target)
        {
            if (target.GameObject.TryGetComponent(out SCP008 scp008))
                return false;

            target.GameObject.AddComponent<SCP008>();
            return true;
        }

        public static bool Cure(Player target)
        {
            if (!target.GameObject.TryGetComponent(out SCP008 scp008))
                return false;

            Object.Destroy(scp008);
            return true;
        }
    }
}
