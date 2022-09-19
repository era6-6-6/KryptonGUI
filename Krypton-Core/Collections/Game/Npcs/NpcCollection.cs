using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton_Core.Settings;

namespace Krypton_Core.Collections.Game.Npcs
{
    public class NpcCollection
    {
        public static List<Tuple<int, string, int >> Npcs = new List<Tuple<int, string, int>>()
        {
            new(1  ,  "-=[ Streuner ]=-",             1 ),
            //add more npcs here from comment under
            new(1 , "-=[ Recruit Streuner ]=-"        ,2 ),
            new(1 , "-=[ Aider Streuner ]=-"          ,3 ),
            //1-2
            new(2   , "-=[ Streuner ]=-"                ,1),
            new(2   , "-=[ Recruit Streuner ]=-"        ,2),
            new(2   , "-=[ Lordakia ]=-"                ,4),
            new(2   , "..::{ Boss Streuner }::.."       ,5),
            new(2   , "..::{ Boss Lordakia }::.."       ,6),
            new(2   , "-=[ Aider Streuner ]=-"          ,3),
            //1-3
            new(3 , "-=[ Lordakia ]=-"                ,4 ),
            new(3 , "-=[ Devolarium ]=-"              ,7 ),
            new( 3 , "-=[ Mordon ]=-"                  ,8),
            new( 3 , "-=[ Saimon ]=-"                  ,9),
            new( 3 , "..::{ Boss Mordon }::.."         ,10),
            new( 3 , "..::{ Boss Saimon }::.."         ,11),
            new( 3 , "..::{ Boss Devolarium }::.."     ,12),
            //1-4
            new(4 , "-=[ Lordakia ]=-"                ,4),
            new(4 , "-=[ Mordon ]=-"                  ,8),
            new(4 , "-=[ Sibelon ]=-"                 ,30),
            new(4 , "-=[ Saimon ]=-"                  ,9),
            new(4 , "..::{ Boss Saimon }::.."         ,11),
            new(4 , "..::{ Boss Sibelon }::.."        ,13),
                //1-5                ,int
            new(17 ,  "-=[ Lordakia ]=-"               ,4),
            new(17 ,  "-=[ Sibelonit ]=-"              ,14),
            new(17 ,  "-=[ Lordakium ]=-"              ,15),
            new(17 ,  "..::{ Boss Sibelonit }::.."     ,16),
            new(17 ,  "..::{ Boss Lordakium }::..."    ,17),
            new(18 , "-=[ Kristallin ]=-" , 18),
            new(18 , "-=[ Kristallon ]=-" , 19),
            new(18 , "-=[ Cubikon ]=-" , 20),
            new(18 , "..::{ Boss Kristallin }::.." , 21),
            new(18 , "-=[ Plagued Kristallin ]=-" , 22),
            new(18 , "-=[ Protegit ]=-" , 23),
            //1-7                ,int
            new(19 , "-=[ Kristallin ]=-" , 18),
            new(19 , "-=[ Kristallon ]=-" , 19),
            new(19 , "..::{ Boss Kristallin }::.." , 21),
            new(19 , "..::{ Boss Kristallon }::.." , 24),
            //1-8                ,int
            new(20 , "-=[ StreuneR ]=-" , 25),
            new(20 , "..::{ Boss StreuneR }::.." , 26),
            new(20 , "\\\\ Purpose XXI //" , 27),
            //EIC                ,int
            //2-1                ,int
            new(5 , "-=[ Streuner ]=-" , 1),
            new(5 , "-=[ Recruit Streuner ]=-" , 2),
            new(5 , "-=[ Aider Streuner ]=-" , 3),
            //2-2                ,int                   
            new(6 , "-=[ Streuner ]=-" , 1),
            new(6 , "-=[ Recruit Streuner ]=-" , 2),
            new(6 , "-=[ Lordakia ]=-" , 4),
            new(6 , "..::{ Boss Streuner }::.." , 5),
            new(6 , "..::{ Boss Lordakia }::.." , 6),
            new(6 , "-=[ Aider Streuner ]=-" , 3),
            //2-3                ,int                   
            new(7 , "-=[ Lordakia ]=-" , 4),
            new(7 , "-=[ Devolarium ]=-" , 7),
            new(7 , "-=[ Mordon ]=-" , 8),
            new(7 , "-=[ Saimon ]=-" , 9),
            new(7 , "..::{ Boss Mordon }::.." , 10),
            new(7 , "..::{ Boss Saimon }::.." , 11),
            new(7 , "..::{ Boss Devolarium }::.." , 12),
            //2-4                ,int
            new(8 , "-=[ Lordakia ]=-" , 4),
            new(8 , "-=[ Mordon ]=-" , 8),
            new(8 , "-=[ Sibelon ]=-" , 30),
            new(8 , "-=[ Saimon ]=-" , 9),
            new(8 , "..::{ Boss Saimon }::.." , 11),
            new(8 , "..::{ Boss Sibelon }::.." , 13),
            //2-5                ,int
            new(21 , "-=[ Lordakia ]=-" , 4),
            new(21 , "-=[ Sibelonit ]=-" , 14),
            new(21 , "-=[ Lordakium ]=-" , 15),
            new(21 , "..::{ Boss Sibelonit }::.." , 16),
            new(21 , "..::{ Boss Lordakium }::..." , 17),
            new(21 , "* Lordakium Spore *" , 31),
            //2-6                ,int
            new(22 , "-=[ Kristallin ]=-" , 18),
            new(22 , "-=[ Kristallon ]=-" , 19),
            new(22 , "-=[ Cubikon ]=-" , 20),
            new(22 , "..::{ Boss Kristallin }::.." , 21),
            new(22 , "-=[ Plagued Kristallin ]=-" , 22),
            new(22 , "-=[ Protegit ]=-" , 23),
            //2-7                ,int
            new(23 , "-=[ Kristallin ]=-" , 18),
            new(23 , "-=[ Kristallon ]=-" , 19),
            new(23 , "..::{ Boss Kristallin }::.." , 21),
            new(23 , "..::{ Boss Kristallon }::.." , 24),
            //2-8                ,int
            new(24 , "-=[ StreuneR ]=-" , 25),
            new(24 , "..::{ Boss StreuneR }::.." , 26),
            new(24 , "\\\\ Purpose XXI //" , 27),
            //VRU                ,int
            //3-1                ,int
            new(9 , "-=[ Streuner ]=-" , 1),
            new(9 , "-=[ Recruit Streuner ]=-" , 2),
            new(9 , "-=[ Aider Streuner ]=-" , 3),
            //3-2                ,int                   
            new(10 , "-=[ Streuner ]=-" , 1),
            new(10 , "-=[ Recruit Streuner ]=-" , 2),
            new(10 , "-=[ Lordakia ]=-" , 4),
            new(10 , "..::{ Boss Streuner }::.." , 5),
            new(10 , "..::{ Boss Lordakia }::.." , 6),
            new(10 , "-=[ Aider Streuner ]=-" , 3),
            new(11 , "-=[ Lordakia ]=-" , 4),
            new(11 , "-=[ Devolarium ]=-" , 7),
            new(11 , "-=[ Mordon ]=-" , 8),
            new(11 , "-=[ Saimon ]=-" , 9),
            new(11 , "..::{ Boss Mordon }::.." , 10),
            new(11 , "..::{ Boss Saimon }::.." , 11),
            new(11 , "..::{ Boss Devolarium }::.." , 12),
            new(12 , "-=[ Lordakia ]=-" , 4),
            new(12 , "-=[ Mordon ]=-" , 8),
            new(12 , "-=[ Sibelon ]=-" , 30),
            new(12 , "-=[ Saimon ]=-" , 9),
            new(12 , "..::{ Boss Saimon }::.." , 11),
            new(12 , "..::{ Boss Sibelon }::.." , 13),
            //3-5                ,int
            new(25 , "-=[ Lordakia ]=-" , 4),
            new(25 , "-=[ Sibelonit ]=-" , 14),
            new(25 , "-=[ Lordakium ]=-" , 15),
            new(25 , "..::{ Boss Sibelonit }::.." , 16),
            new(25 , "..::{ Boss Lordakium }::..." , 17),
            //3-6                ,int
            new(26 , "-=[ Kristallin ]=-" , 18),
            new(26 , "-=[ Kristallon ]=-" , 19),
            new(26 , "-=[ Cubikon ]=-" , 20),
            new(26 , "..::{ Boss Kristallin }::.." , 21),
            new(26 , "-=[ Plagued Kristallin ]=-" , 22),
            new(26 , "-=[ Protegit ]=-" , 23),
            //3-7                ,int
            new(27 , "-=[ Kristallin ]=-" , 18),
            new(27 , "-=[ Kristallon ]=-" , 19),
            new(27 , "..::{ Boss Kristallin }::.." , 21),
            new(27 , "..::{ Boss Kristallon }::.." , 24),
            //3-8                ,int
            new(28 , "-=[ StreuneR ]=-" , 25),
            new(28 , "..::{ Boss StreuneR }::.." , 26),
            new(28 , "\\\\ Purpose XXI //" , 27),

            // event

            new(100 ,"-=[ Terror Mime5is ]=-", 32),
            new(100 ,"-=[ Raging Mimes1s ]=-", 33),
            new(100 ,"-=[ Kamikaze Mime5is ]=-", 34),
            new(100 ,"-=[ Cloning Mim3sis ]=- ", 35),
            new(100 ,"-=[ Hexor M1mesis ]=-", 36),
            new(100 ,"-=[ Reflector Mimesi5 ]=-", 37),
            new(100 ,"-=[ Cloned Mim3sis ]=-", 38),
            new(100 ,"-=[ 1100101 ]=-", 39),
            new(100 ,"<=< Ice Meteoroid >=>", 40),
            new(100 ,"<=< Icy >=>", 41),
            new(100 ,"<=< Super Ice Meteoroid >=>", 42),
            new(100 ,"-=[ Sunburst Lordakium ]=-", 43),
            new(100 ,"--=[ Hitac ]=", 44),
            new(100 ,"-= [Boss Curcubitor] =- ", 45),
            new(100 ,"-= [Curcubitor] =-", 46),
            new(100 ,"-=[ Sunburst Lordakium ]=-", 47),
            new(100 ,"-={ demaNeR Freighter }=-", 48),
            new(100 ,"-={ demaNeR Escort }=-", 49),

            //Low                ,int
            new(200,"-=[ Vagrant ]=-", 50),
            new(200,"-=[ Marauder ]=-", 51),
            new(200,"-=[ Outcast ]=-", 52),
            new(200,"-=[ Corsair ]=-", 53),
            new(200,"-=[ Hooligan ]=-", 54),
            new(200,"-=[ Ravager ]=-", 55),
            new(200,"-=[ Convict ]=-", 56),
            new(200,"-=[ Century Falcon ]=-", 57),

            new(91,"-=[ Interceptor ]=-", 58),
            new(91,"-=[ Annihilator ]=-", 59),
            new(91,"-=[ Barracuda ]=-", 60),
            new(91,"-=[ Saboteur ]=-", 61),
            //5-2                ,int
            new(92,"-=[ Interceptor ]=-", 62),
            new(92,"-=[ Annihilator ]=-", 63),
            new(92,"-=[ Barracuda ]=-", 64),
            new(92,"-=[ Saboteur ]=-", 65),

            //5-3                ,int
            new(93,"-=[ Interceptor ]=-", 66),
            new(93,"-=[ Annihilator ]=-", 67),
            new(93,"-=[ Barracuda ]=-", 68),
            new(93,"-=[ Saboteur ]=-", 69),
            new(93,"-=[ Battleray ]=-", 70),

            new(100,"-=[ Purpose XXI ]=-", 71),
            new(100,"-=[ Attend IX ]=-", 72),
            new(100,"-=[ Impulse II ]=-", 73),
            new(100,"-=[ Invoke XVI ]=-", 74),
            new(100,"-=[ Mindfire Behemoth ]=-", 75),






            
        };
        //add this to tuple over 





        public List<Tuple<int, NpcSettings>> NpcsSettings = new()
        {
            new Tuple<int, NpcSettings>(1 , new NpcSettings()),
            new Tuple<int, NpcSettings>(2 , new NpcSettings()),
            new Tuple<int, NpcSettings>(3 , new NpcSettings()),
            new Tuple<int, NpcSettings>(4 , new NpcSettings()),
            new Tuple<int, NpcSettings>(5 , new NpcSettings()),
            new Tuple<int, NpcSettings>(6 , new NpcSettings()),
            new Tuple<int, NpcSettings>(7 , new NpcSettings()),
            new Tuple<int, NpcSettings>(8 , new NpcSettings()),
            new Tuple<int, NpcSettings>(9 , new NpcSettings()),
            new Tuple<int, NpcSettings>(10 , new NpcSettings()),
            new Tuple<int, NpcSettings>(11 , new NpcSettings()),
            new Tuple<int, NpcSettings>(12 , new NpcSettings()),
            new Tuple<int, NpcSettings>(13 , new NpcSettings()),
            new Tuple<int, NpcSettings>(14 , new NpcSettings()),
            new Tuple<int, NpcSettings>(15 , new NpcSettings()),
            new Tuple<int, NpcSettings>(16 , new NpcSettings()),
            new Tuple<int, NpcSettings>(17 , new NpcSettings()),
            new Tuple<int, NpcSettings>(18 , new NpcSettings()),
            new Tuple<int, NpcSettings>(19 , new NpcSettings()),
            new Tuple<int, NpcSettings>(20 , new NpcSettings()),
            new Tuple<int, NpcSettings>(21 , new NpcSettings()),
            new Tuple<int, NpcSettings>(22 , new NpcSettings()),
            new Tuple<int, NpcSettings>(23 , new NpcSettings()),
            new Tuple<int, NpcSettings>(24 , new NpcSettings()),
            new Tuple<int, NpcSettings>(25 , new NpcSettings()),
            new Tuple<int, NpcSettings>(26 , new NpcSettings()),
            new Tuple<int, NpcSettings>(27 , new NpcSettings()),
            new Tuple<int, NpcSettings>(28 , new NpcSettings()),
            new Tuple<int, NpcSettings>(29 , new NpcSettings()),
            new Tuple<int, NpcSettings>(30 , new NpcSettings()),
            new Tuple<int, NpcSettings>(31 , new NpcSettings()),
            new Tuple<int, NpcSettings>(32 , new NpcSettings()),
            new Tuple<int, NpcSettings>(33 , new NpcSettings()),
            new Tuple<int, NpcSettings>(34 , new NpcSettings()),
            new Tuple<int, NpcSettings>(35 , new NpcSettings()),
            new Tuple<int, NpcSettings>(36 , new NpcSettings()),
            new Tuple<int, NpcSettings>(37 , new NpcSettings()),
            new Tuple<int, NpcSettings>(38 , new NpcSettings()),
            new Tuple<int, NpcSettings>(39 , new NpcSettings()),
            new Tuple<int, NpcSettings>(40 , new NpcSettings()),
            new Tuple<int, NpcSettings>(41 , new NpcSettings()),
            new Tuple<int, NpcSettings>(42 , new NpcSettings()),
            new Tuple<int, NpcSettings>(43 , new NpcSettings()),
            new Tuple<int, NpcSettings>(44 , new NpcSettings()),
            new Tuple<int, NpcSettings>(45 , new NpcSettings()),
            new Tuple<int, NpcSettings>(46 , new NpcSettings()),
            new Tuple<int, NpcSettings>(47 , new NpcSettings()),
            new Tuple<int, NpcSettings>(48 , new NpcSettings()),
            new Tuple<int, NpcSettings>(49 , new NpcSettings()),
            new Tuple<int, NpcSettings>(50 , new NpcSettings()),
            new Tuple<int, NpcSettings>(51 , new NpcSettings()),
            new Tuple<int, NpcSettings>(52 , new NpcSettings()),
            new Tuple<int, NpcSettings>(53 , new NpcSettings()),
            new Tuple<int, NpcSettings>(54 , new NpcSettings()),
            new Tuple<int, NpcSettings>(55 , new NpcSettings()),
            new Tuple<int, NpcSettings>(56 , new NpcSettings()),
            new Tuple<int, NpcSettings>(57 , new NpcSettings()),
            new Tuple<int, NpcSettings>(58 , new NpcSettings()),
            new Tuple<int, NpcSettings>(59 , new NpcSettings()),
            new Tuple<int, NpcSettings>(60 , new NpcSettings()),
            new Tuple<int, NpcSettings>(61 , new NpcSettings()),
            new Tuple<int, NpcSettings>(62 , new NpcSettings()),
            new Tuple<int, NpcSettings>(63 , new NpcSettings()),
            new Tuple<int, NpcSettings>(64 , new NpcSettings()),
            new Tuple<int, NpcSettings>(65 , new NpcSettings()),
            new Tuple<int, NpcSettings>(66 , new NpcSettings()),
            new Tuple<int, NpcSettings>(67 , new NpcSettings()),
            new Tuple<int, NpcSettings>(68 , new NpcSettings()),
            new Tuple<int, NpcSettings>(69 , new NpcSettings()),
            new Tuple<int, NpcSettings>(70 , new NpcSettings()),
            new Tuple<int, NpcSettings>(71 , new NpcSettings()),
            new Tuple<int, NpcSettings>(72 , new NpcSettings()),
            new Tuple<int, NpcSettings>(73 , new NpcSettings()),
            new Tuple<int, NpcSettings>(74 , new NpcSettings()),
            new Tuple<int, NpcSettings>(75 , new NpcSettings()),
        };













    }
}
