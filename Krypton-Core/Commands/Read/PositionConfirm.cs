using System;
using Krypton_Core.Collections.Game.Players;
using Krypton_Core.Logic;
using Krypton_Core.Packets.Bytes;
using Unglide;

namespace Krypton_Core.Commands.Read
{
    public class PositionConfirm : LogicInterface
    {
        public const short ID = 15065;

        public int ActualX { get; set; }
        public int ActualY { get; set; }

        public PositionConfirm(EndianBinaryReader param1, Api api, Tweener tween) : base(api, tween)
        {
            var uk9 = param1.ReadBoolean();
            var uk5 = param1.ReadInt32();
            uk5 = (int)((uint)uk5 << 1 | (uint)uk5 >> 31);
            var uk7 = param1.ReadBoolean();
            var uk8 = param1.ReadBoolean();
            var uk3 = param1.ReadString();
            var uk6 = param1.ReadInt32();
            uk6 = uk6 << 11 | uk6 >> 21;
            var uk2 = param1.ReadBoolean();

            var uk10 = param1.ReadBoolean();
            var uk4 = param1.ReadInt32();
            ActualY = (int)((uint)uk4 << 12 | (uint)uk4 >> 20);


            int uk1 = param1.ReadInt32(); //x
            ActualX = uk1 >> 14 | uk1 << 18;
            
            if(CalculateDistance(ActualX , ActualY) > 600)
            {
                Api.RemoveFromTween();
                FlyToCorndinates(Api._user.Position.TargetX , Api._user.Position.TargetY);
                return;
            }

        }

        protected override Player ClosestNpcReturn()
        {
            throw new NotImplementedException();
        }
    }
}