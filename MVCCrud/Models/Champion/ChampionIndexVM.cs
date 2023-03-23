using Database.Entity;

namespace MVCCrud.Models.Champion
{
    public class ChampionIndexVM
    {
        public int Champion_ID { get; set; }
        public string Name { get; set; }
        public bool JelChest { get; set; }
        
        public string Role { get; set; }
    }
}
