using EFT;
using UnityEngine;

namespace SivaEftCheat.Utils
{
    class RayCast
    {
        private static readonly LayerMask LayerMask = 1 << 12 | 1 << 16 | 1 << 18; //Might renove 18
        private static RaycastHit _raycastHit;

        public static bool IsVisible(Player player)
        {
            return Physics.Linecast(
                Camera.main.transform.position,
                player.PlayerBones.Head.position,
                out _raycastHit,
                LayerMask) && _raycastHit.collider && _raycastHit.collider.gameObject.transform.root.gameObject == player.gameObject.transform.root.gameObject;
        }
        public static bool IsBodyPartVisible(Player player, int bodypart)
        {
            Vector3 BodyPartToAim = GameUtils.FinalVector(player.PlayerBody.SkeletonRootJoint, bodypart);
            return Physics.Linecast(
                Camera.main.transform.position,
                BodyPartToAim,
                out _raycastHit,
                LayerMask) && _raycastHit.collider && _raycastHit.collider.gameObject.transform.root.gameObject == player.gameObject.transform.root.gameObject;
        }
        public static Vector3 BarrelRayCast(Player player)
        {
            try
            {
                if (player.Fireport == null)
                    return Vector3.zero;

                Physics.Linecast(
                    player.Fireport.position,
                    player.Fireport.position - player.Fireport.up * 1000f,
                    out _raycastHit,
                    LayerMask);

                return _raycastHit.point;
            }
            catch
            {
                return Vector3.zero;
            }
        }
    }
}
