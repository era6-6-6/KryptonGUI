using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Collections.Game.Items;
using Krypton_Core.PacketReaderNew;
using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read
{
    public class BoosterInfo : Paket
    {
        public const short ID = 101;
        public List<Booster> boosters = new();
        public List<BoosterValue> BoostersValue = new();


        public override void Read(EndianBinaryReader param1)
        {
            string Type = "";
            double Value = 0;
            int TimeInSeconds = 0;
            var lootId = 0;
            var loc =param1.ReadByte();
            
            for (int i = 0; i < loc; i++)
            {

                param1.ReadInt16();
                BoosterValue val = new BoosterValue();
                Type = param1.ReadString();
                Value = param1.ReadSingle();
                val.Type = Type;
                val.Value = Value;
                BoostersValue.Add(val);


            }
            var loc2 = param1.ReadByte();
            for (int i = 0; i < loc2; i++)
            {
                param1.ReadInt16();
                param1.ReadInt16();
                Booster booster = new Booster();
                var boosterType = param1.ReadString();
                TimeInSeconds = param1.ReadInt32();
                TimeInSeconds = (int)((uint)TimeInSeconds << 10 | (uint)TimeInSeconds >> 22);
                booster.BoosterType = boosterType;
                booster.TimeInSeconds = TimeInSeconds;
                booster.StartCounter();
                boosters.Add(booster);
            }
        }

        public override void Write()
        {

        }

       
    }
}
