namespace Assets.DataManager.Scripts.Models
{
    public class CardType
    {
        public int Id { get; set; }
        public string SpellName { get; set; }
        public string PictureUrl { get; set; }
        public SpellType SpellType { get; set; }
    }

    public enum SpellType
    {
        Fireball = 1,
        Explosion = 2,
        Heal = 3,
        //TODO: dopisac odpowiednie nazwy czarów do enuma
    }
}
