namespace e10.Shared.Data.Abstraction
{
    public abstract class Post : Entity
    {
        public string Title { get; set; }
        public string Body { get; set; }

        public Topic Topic { get; set; }
        public int TopicId { get; set; }
    }
}