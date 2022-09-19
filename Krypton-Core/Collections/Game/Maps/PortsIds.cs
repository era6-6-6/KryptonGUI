using Krypton_Core.Collections.Game.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Collections.Game.Maps
{
    public class PortsIds
    {
        /// <summary>
        /// The function find in PortsIds and the PortId, return the next mapId
        /// </summary>
        public static int GetNextMapID(int portID)
        {
            try
            {
                
                int? nextMapID = PortIDS.Find(x => x.Item2 == portID).Item1;
                return (int)nextMapID;
            }
            catch
            {
                Console.WriteLine("unknown port id  " + portID);
                
                return 0;
            }
        }

        /// <summary>
        /// MapID, PortID
        /// </summary>
        public static List<Tuple<int, int>> PortIDS = new List<Tuple<int, int>>()
        {
            //1-1
            new(2 , 150000165),
            new(71 , 150000394),
            new(71 , 150000372),
            
            
            //1-1
            new(1 , 150000166),
            new(3 , 150000167),
            new(4 , 150000169),
            
            
            //1-3
            new(2 , 150000168),
            new(7 , 150000171),
            new(4 , 150000191),
            new(200 , 150000447),
           
            //1-4
            new(2 , 150000170),
            new(3 , 150000192),
            new(13, 150000195),
            new(12, 150000175),
            
            //1-5
            new(16, 150000314),
            new(29, 150000343),
            new(19, 150000317),
            new(18, 150000315),
            
            //1-6
            new(17, 150000316),
            new(20, 150000319),
            
            //1-7
            new(17, 150000318),
            new(20, 150000321),
            new(0 , 150000499),

            //1-8
            new(18, 150000320),
            new(19, 150000322),
            new(0, 150000207), //BL Map ID missing
            

            //2-1
            new(6, 150000180),
            
            //2-2
            new(5, 150000179),
            new(8, 150000181),
            new(7, 150000174),

            //2-3
            new(6, 150000173),
            new(8, 150000189),
            new(3, 150000172),
            new(200, 150000448),

            //2-4
            new(6, 150000182),
            new(7, 150000190),
            new(14, 150000197),
            new(11, 150000183),

            //2-5
            new(22, 150000325),
            new(23, 150000327),
            new(29, 150000345),
            new(16, 150000324),
            
            //2-6
            new(21, 150000326),
            new(24, 150000329),
            
            //2-7
            new(21, 150000328),
            new(24, 150000331),
            
            //2-8
            new(22, 150000330),
            new(0, 150000211),//BL Map ID missing
            new(23, 150000332),

            //3-1
            new(10, 150000188),
            
            //3-2
            new(9, 150000187),
            new(12, 150000178),
            new(11, 150000186),
            
            //3-3
            new(10, 150000185),
            new(12, 150000194),
            new(8, 150000184),
            new(200, 150000449), //Low Map ID missing
            //3-4
            new(10, 150000177),
            new(4, 150000176),
            new(15, 150000199),
            new(11, 150000193),
            
            //3-5
            new(16, 150000334),
            new(26, 150000335),
            new(27, 150000337),
            new(29, 150000347),
            
            //3-6
            new(25, 150000336),
            new(28, 150000339),
            
            //3-7
            new(25, 150000338),
            new(28, 150000341),
            //3-8
            new(27, 150000342),
            new(26, 150000340),
            new(0, 150000215), //BL Map ID missing
            
            //4-1
            new(4, 150000196),
            new(14, 150000201),
            new(15, 150000206),
            new(16 , 150000304),

            //4-2
            new(13, 150000202),
            new(8, 150000198),
            new(16, 150000203),
            
            //4-3
            new(14, 150000204),
            new(13, 150000205),
            new(12, 150000200),
            new(16, 150000308),

            //4-4
            new(17, 150000313),
            new(13, 150000305),
            new(21, 150000323),
            new(14, 150000307),
            new(25, 150000333),
            new(15, 150000309),
            //4-5
            new(17, 150000344),
            new(91, 150000451),
            new(21, 150000346),
            new(91, 150000453),
            new(25, 150000348),
            new(91, 150000455),
            //5-1
            new(29, 150000452),
            new(29, 150000454),
            new(29, 150000456),
            new(92, 150000461),
            new(92, 150000459),
            new(92, 150000457),
            //5-2 
            new(91, 150000458),
            new(91, 150000460),
            new(91, 150000462),
            new(93, 150000467),
            new(93, 150000465),
            new(93, 150000463),
            new(0, 150000469), //5-4 Map ID missing
            new(0, 150000471), //5-4 Map ID missing
            new(0, 150000473), //5-4 Map ID missing
            //5-3
            new(92, 150000468),
            new(92, 150000464),
            new(92, 150000466),
            new(16, 150000475),
            new(16, 150000477),
            new(16, 150000479),
            
            //1-BL ALL ,BL M,AP IDS ARE MISSING!!
            new(0, 150000209),
            new(0, 150000210),
            new(20, 150000208),
            
            //2-BL
            new(0, 150000213),
            new(0, 150000214),
            new(24, 150000212),

            new(28, 150000216),
            new(0, 150000218),
            new(0, 150000217)
        };






    }
}
