namespace Corelibs.Basic.Repository
{
    public class JsonEntity
    {
        public JsonEntity(string id, string content)
        {
            ID = id;
            Content = content;
        }

        public string ID { get; }
        public string Content { get; }
    }
}
