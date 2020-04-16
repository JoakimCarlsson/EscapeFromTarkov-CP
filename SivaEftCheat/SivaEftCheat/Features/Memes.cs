using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = System.Random;

namespace SivaEftCheat.Features
{
    class Memes
    {
        //Copy pasted

        //private static Vector3 _newPose = Vector3.zero;
        //private static float _nextRandom;
        //private static readonly Random Rnd = new Random();
        //private static float tempR = 0f;
        //private static float radius = 1f;
        //private static float theta = 0f;
        //private static Vector3 RealPlayerPosition;
        //private static bool revertedCiriMode;
        //private static float nextTime = 0f;

        //private static Vector3 CalculateCiriNextPosition(Vector3 CenterPosition, float num = 1f)
        //{
        //    _nextRandom = Rnd.Next(0, (int)(1000 * num)) / 1000f;
        //    tempR = radius * (float)Math.Sqrt(_nextRandom);
        //    theta = _nextRandom * 2 * 3.14f;
        //    //Base.Transform.forward
        //    _newPose.x = CenterPosition.x + tempR * (float)Math.Cos(theta);
        //    _newPose.z = CenterPosition.z + tempR * (float)Math.Sin(theta);
        //    _newPose.y = CenterPosition.y;
        //    return _newPose;
        //}
        //public static void StartCiriMode(bool enable)
        //{
        //    if (!revertedCiriMode)
        //        RealPlayerPosition = Main.LocalPlayer.Transform.position;
        //    if (enable)
        //    {
        //        if (nextTime < Time.time)
        //        {
        //            Main.LocalPlayer.Transform.position = CalculateCiriNextPosition(RealPlayerPosition, 2f);
        //            nextTime = Time.time + 0.1f;
        //        }
        //        if (!revertedCiriMode)
        //            revertedCiriMode = true;
        //    }
        //    else
        //    {
        //        if (revertedCiriMode)
        //        {
        //            revertedCiriMode = false;
        //            Main.LocalPlayer.Transform.position = RealPlayerPosition;
        //        }
        //    }
        //}
    }
}
