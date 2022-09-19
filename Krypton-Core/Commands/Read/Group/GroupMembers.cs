using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Collections.Game.Players;
using Krypton_Core.Packets.Bytes;

namespace Krypton_Core.Commands.Read.Group
{
    public class GroupMembers
    {
        public const short ID = 31883;
        public List<Player> UsersInGroup = new();

        public GroupMembers(EndianBinaryReader param1)
        {
            var id = param1.ReadInt32();
            id = (int)((uint)id >> 12 | (uint)id << 20);
            Console.WriteLine("id: " + id);
            var loc = param1.ReadByte();
            Console.WriteLine("loc: " + loc);
            for (int i = 0; i < loc; i++)
            {
                var user = new Player();

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"----player {i + 1} -------");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();


                param1.ReadInt16();
                var uk = param1.ReadBoolean();
                Console.WriteLine("uk: " + uk);

                param1.ReadInt16();
                param1.ReadInt16();

                param1.ReadInt16();

                //class 1012
                param1.ReadInt16();
                param1.ReadInt16();

                var uk2 = param1.ReadInt32(); //max hp
                uk2 = (int)((uint)uk2 << 1 | (uint)uk2 >> 31); //max hp
                Console.WriteLine("uk2: " + uk2);
                var uk3 = param1.ReadInt32(); //maxnano
                uk3 = (int)((uint)uk3 >> 8 | (uint)uk3 << 24);
                Console.WriteLine("uk3: " + uk3);
                var uk4 = param1.ReadInt32(); //max shd
                uk4 = (int)((uint)uk4 << 14 | (uint)uk4 >> 18);
                Console.WriteLine("uk4: " + uk4);
                var hp = param1.ReadInt32(); //hp
                hp = (int)((uint)hp >> 2 | (uint)hp << 30);
                Console.WriteLine("hp: " + hp);
                var shield = param1.ReadInt32(); //shd
                shield = (int)((uint)shield << 16 | (uint)shield >> 16);
                Console.WriteLine("shd: " + shield);
                var uk6 = param1.ReadInt32(); //nano
                uk6 = (int)((uint)uk6 >> 6 | (uint)uk6 << 26);
                Console.WriteLine("uk6: " + uk6);




                //class 1001
                param1.ReadInt16();
                param1.ReadInt16();
                var ukk = param1.ReadBoolean();
                Console.WriteLine("ukk: " + ukk);
                var uk7 = param1.ReadInt32();
                uk7 = (int)((uint)uk7 >> 8 | (uint)uk7 << 24);
                Console.WriteLine("uk7: " + uk7);

                //
                var cloacked = param1.ReadBoolean();
                var active = param1.ReadBoolean();
                Console.WriteLine("cloacked: " + cloacked);
                Console.WriteLine("active: " + active);


                var username = param1.ReadString();
                Console.WriteLine("username: " + username);


                //class 1006
                var clan = param1.ReadInt16();
                Console.WriteLine("clan: " + clan);

                param1.ReadInt16();
                var clantag = param1.ReadString();
                Console.WriteLine("clanTag: " + clantag);
                var uk8 = param1.ReadInt32();
                uk8 = (int)((uint)uk8 >> 9 | (uint)uk8 << 23); //clan id
                Console.WriteLine("uk8: " + uk8);



                //
                var level = param1.ReadInt32(); //22
                level = (int)((uint)level << 11 | (uint)level >> 21);
                Console.WriteLine("level: " + level);



                //class 1002
                var location = param1.ReadInt16();

                param1.ReadInt16();
                var x = param1.ReadInt32();
                x = (int)((uint)x << 13 | (uint)x >> 19);
                Console.WriteLine("location: " + location);
                var mapId = param1.ReadInt32(); // map
                mapId = (int)((uint)mapId << 5 | (uint)mapId >> 27);
                Console.WriteLine("x : " + x);
                Console.WriteLine("MapId: " + mapId);
                var y = param1.ReadInt32();
                y = (int)((uint)y << 11 | (uint)y >> 21);
                Console.WriteLine("y: " + y);





                //
                var uk9 = param1.ReadBoolean();
                var uk10 = param1.ReadBoolean();
                Console.WriteLine("uk9 " + uk9);
                Console.WriteLine("uk10: " + uk10);


                //make it player later 
                //class 1004
                var target = param1.ReadInt16();
                Console.WriteLine("target: " + target);

                var targetPlayer = new Player();

                param1.ReadInt16();
                var name = param1.ReadString();
                Console.WriteLine("TargetName: " + name);
                //class 446
                param1.ReadInt16();
                param1.ReadInt16();
                param1.ReadInt16();
                //1012
                param1.ReadInt16();
                param1.ReadInt16();

                var uk11 = param1.ReadInt32();
                uk11 = (int)((uint)uk11 << 1 | (uint)uk11 >> 31);
                Console.WriteLine("uk11: " + uk11);
                var uk12 = param1.ReadInt32();
                uk12 = (int)((uint)uk12 >> 8 | (uint)uk12 << 24);
                Console.WriteLine("uk12: " + uk12);
                var uk13 = param1.ReadInt32();
                uk13 = (int)((uint)uk13 << 14 | (uint)uk13 >> 18);
                Console.WriteLine("uk13: " + uk13);
                var Npchp = param1.ReadInt32();
                Npchp = (int)((uint)Npchp >> 2 | (uint)Npchp << 30);
                Console.WriteLine("npcHp: " + Npchp);
                var Npcshield = param1.ReadInt32();
                Npcshield = (int)((uint)Npcshield << 16 | (uint)Npcshield >> 16);
                Console.WriteLine("npcShield: " + Npcshield);
                var uk14 = param1.ReadInt32();
                uk14 = (int)((uint)uk14 >> 6 | (uint)uk14 << 26); // targetId
                Console.WriteLine("uk14: " + uk14);
                targetPlayer.Username = name;
                targetPlayer.MaxHP = uk11;
                targetPlayer.MaxNano = uk12;
                targetPlayer.MaxShd = uk13;
                targetPlayer.Hp = Npchp;
                targetPlayer.Shd = Npcshield;
                targetPlayer.ID = uk14;













                var UserId = param1.ReadInt32();
                UserId = (int)((uint)UserId << 10 | (uint)UserId >> 22);
                Console.WriteLine("userId: " + UserId);

                var factionid = param1.ReadInt16();
                Console.WriteLine("factionId: " + factionid);

                param1.ReadInt16();
                factionid = param1.ReadInt16();

                Console.WriteLine("factionId: " + factionid);
                user.MaxHP = uk2;
                user.Hp = hp;
                user.MaxNano = uk3;
                user.NanoHp = uk6;
                user.MaxShd = uk4;
                user.Shd = shield;
                user.Cloacked = cloacked;
                user.Username = username;
                user.ClanTag = clantag;
                user.Level = level;
                user.X = x;
                user.MapId = mapId;
                user.Y = y;
                if (targetPlayer.MaxHP != 0)
                {
                    user.Target = targetPlayer;
                }

                user.ID = UserId;
                user.FactionID = factionid;

                UsersInGroup.Add(user);

            }

            var uk20 = param1.ReadInt32();
            uk20 = uk20 >> 15 | uk20 << 17;
            //class 1321
            var something = param1.ReadInt16();
            if (something != 0)
            {
                param1.ReadInt16();
            }

            var uk21 = param1.ReadInt32();
            uk21 = uk21 << 14 | uk21 >> 18;
            Console.WriteLine("uk20 " + uk20);
            Console.WriteLine("uk21 " + uk21);
        }
    }
}
