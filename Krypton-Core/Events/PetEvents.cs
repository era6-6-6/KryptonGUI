using Krypton_Core.Commands.Read;


namespace Krypton_Core.Events
{
    
    public class PetEvents
    {
        Api? Api { get; set; }
        public PetEvents(Api api)
        {
            Api = api;
        }
        public void RegisterEvents()
        {
            if (Api?._user.packetManager != null)
                Api._user.packetManager.onPetInitUp += (s, e) => { UpdatePet(e); };
        }

        private void UpdatePet(PetInitUp e)
        {
            var pet = Api?._user.players.Pet;
            pet.Fuel = e.Fuel;
            pet.Hp = e.Hp;
            pet.Experience = e.PetExp;
            pet.MaxHp = e.MaxHp;
            pet.Shd = e.Shd;
            pet.MaxShd = e.MaxShd;
            pet.Username = pet.Username;
            pet.Id = e.PetID;
            pet.Level = e.Level;
            pet.MaxFuel = e.MaxFuel;
           
                

        }
    }
}
