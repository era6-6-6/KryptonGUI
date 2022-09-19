
using Command = Krypton_Core.Managers.Command;

namespace Krypton_Core.Commands.Write
{
    public class SelectEnemy : Command
    {
        public const short ID = 165;

        private int EnemyID { get; set; }
        private int EnemyX { get; set; }
        private int EnemyY { get; set; }
        private int PlayerX { get; set; }
        public int PlayerY { get; set; }

        public SelectEnemy(int id , int enemyX , int enemyY , int playerX , int playerY)
        {
            EnemyID = id;
            EnemyX = enemyX;
            EnemyY = enemyY;
            PlayerX = playerX;
            PlayerY = playerY;
            Write();
        }
        public void Write()
        {
            param1.writeByte(0x00);
            param1.writeShort(22);
            param1.writeShort(ID);
            param1.writeInt((int)((uint)EnemyID << 5 | (uint)EnemyID >> 27));
            param1.writeInt((int)((uint)EnemyX >> 2 | (uint)EnemyX << 30));
            param1.writeInt((int)((uint)EnemyY >> 10 | (uint)EnemyY << 22));
            param1.writeInt((int)((uint)PlayerX >> 1 | (uint)PlayerX << 31));
            param1.writeInt((int)((uint)PlayerY >> 1 | (uint)PlayerY << 31));
        }


    }
}
