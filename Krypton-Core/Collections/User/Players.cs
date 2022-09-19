using Krypton_Core.Collections.Game.Npcs;
using Krypton_Core.Collections.Game.Players;
using Newtonsoft.Json;

namespace Krypton_Core.Collections.User
{
    public class Players 
    {
        [JsonIgnore]
        public List<Player> AllPlayers = new();
        

        public List<Player> Npcs = new();
        public List<Player> PlayersInGroup = new();

        public List<Player> AllyPlayers = new();
                    
        public List<Player> EnemyPlayers = new();
        [JsonIgnore]
        public List<Player> NpcsToShoot = new();
        public Pet? Pet { get; set; }
        
        public void AddPet(Pet pet)
        {
            Pet = pet;
        }

        public void DestroyGroup()
        {
            lock (PlayersInGroup)
            {
                PlayersInGroup.Clear();
                
            }
        }

        public void AddGroupMembers( List<Player> members)
        {
            lock (PlayersInGroup)
            {
                PlayersInGroup = members;
            }
        }

        public void RemovePlayerFromGroup(int id)
        {
            lock (PlayersInGroup)
            {
                PlayersInGroup.Remove(PlayersInGroup.Find(x => x.ID == id));
            }
        }

        public void Clean()
        {
            //lock and clean
            lock(Npcs)
            {
                Npcs.Clear();
            }
            lock (AllyPlayers)
            {
                AllyPlayers.Clear();
            }
            lock (EnemyPlayers)
            {
                EnemyPlayers.Clear();
            }
            lock(AllPlayers)
            {
                AllPlayers.Clear();
            }
            lock(NpcsToShoot)
            {
                NpcsToShoot.Clear();
            }


        }
        public void Add(Player player , int FactionId , bool shoot)
        {
            if (AllPlayers.Contains(player))
            {
                return;
            }
            lock (AllPlayers)
            {
                AllPlayers.Add(player);
            }
            if(shoot == true)
            {
                lock (NpcsToShoot)
                {
                    NpcsToShoot.Add(player);
                }
            }
         
            if (player.isNpcs)
            {
                lock (Npcs)
                {
                    Npcs.Add(player);
                }
            }
            else
            {
                if (player.FactionID != FactionId)
                {
                    lock (EnemyPlayers)
                    {
                        EnemyPlayers.Add(player);
                    }
                }
                else
                {
                    lock (AllyPlayers)
                    {
                        AllyPlayers.Add(player);
                    }
                }
            }
        }
        public void Remove(int id)
        {
            Player? player = AllPlayers.Find(x => x.ID == id);
            if(player != null)
            {
                lock(AllPlayers)
                {
                    AllPlayers.RemoveAll(x => x.ID == id);
                }
                if(AllyPlayers.Contains(player))
                {
                    lock(AllyPlayers)
                    {
                        AllyPlayers.RemoveAll(player => player.ID == id);
                    }
                }
                else if(Npcs.Contains(player))
                {
                    lock(Npcs)
                    {
                        Npcs.RemoveAll(x => x.ID == id);
                    }
                }
                else if(EnemyPlayers.Contains(player))
                {
                    lock(EnemyPlayers)
                    {
                        EnemyPlayers.RemoveAll(player => player.ID == id);
                    }
                }
                if (NpcsToShoot.Contains(player))
                {
                    lock(NpcsToShoot)
                    {
                        NpcsToShoot.RemoveAll(player => player.ID == id);

                    }
                }
            }
            
           
        }
        

    }
}
