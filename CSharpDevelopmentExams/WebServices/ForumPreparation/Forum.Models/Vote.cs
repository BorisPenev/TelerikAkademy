namespace Forum.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Post Post { get; set; }
    }
}