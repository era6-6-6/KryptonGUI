

namespace Krypton_Core.Collections.Game.Maps
{
    public class POIData
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public bool Accessible { get; set; }
        public bool Active { get; set; }
        public List<int> points = new List<int>();
    }
}
