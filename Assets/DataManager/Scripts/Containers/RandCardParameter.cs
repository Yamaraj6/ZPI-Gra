using UnityEngine;

namespace ZPIGame.Assets.DataManager.Scripts.Containers
{
    public class RandCardParameter
    {
        public int MaxPower { get; set; }
        public int MaxManaCost { get; set; }
        public int MaxCastingTime { get; set; }
        public AnimationCurve PositiveCurve { get; set; }
        public AnimationCurve NegativeCurve { get; set; }
    }
}