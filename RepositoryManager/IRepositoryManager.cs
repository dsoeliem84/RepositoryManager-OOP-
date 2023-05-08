namespace Petrolink.RepositoryManager
{
    public enum ItemType
    {
        JSON = 1,
        XML = 2
    }

    public interface IRepositoryManager
    {
        // Store an item to the repository
        // Parameter itemType is used to differentiate JSON or XML
        // 1 = itemContent is a JSON string
        // 2 = itemContent is an XML string.
        public Task Register(string itemName, string itemContent, ItemType itemType);

        // Retrieve an item from the repository
        public Task<string> Retrieve(string itemName);

        // Retrieve the type of the item (JSON or XML)
        public Task<ItemType> GetType(string itemName);

        // Remove an item from the repository
        public Task Deregister(string itemName);

        // Initialize the repository for use, if needed.
        // You could leave it empty if you have your own way to make the
        // RepositoryManager ready for use
        // (e.g using the constructor)
        public Task Initialize();
    }
}
