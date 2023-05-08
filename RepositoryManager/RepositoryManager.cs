using System.Text.Json;
using System.Xml;

namespace Petrolink.RepositoryManager
{
    public class RepositoryManager : IRepositoryManager
    {
        private List<RepositoryItem> _listItems;

        public Task Deregister(string itemName)
        {
            var item = _listItems.SingleOrDefault(x => x.ItemName == itemName);
            if (item != null)
                _listItems.Remove(item);
            return Task.CompletedTask;
        }

        public Task<ItemType> GetType(string itemName)
        {
            var data = this._listItems.FirstOrDefault(x => x.ItemName == itemName);
            if (data == null)
                throw new Exception("Item not found");
            else
                return Task.FromResult(data.ItemType);
        }

        public Task Initialize()
        {
            _listItems = new List<RepositoryItem>();
            return Task.CompletedTask;
        }

        public async Task Register(string itemName, string itemContent, ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.JSON:
                    if (!await IsJsonFormat(itemContent))
                        throw new Exception("Invalid JSON format for parameter itemContent");
                    break;

                case ItemType.XML:
                    if (!await IsXMLFormat(itemContent))
                        throw new Exception("Invalid XML format for parameter itemContent");
                    break;
            }

            _listItems.Add(new RepositoryItem
            {
                ItemType = itemType,
                ItemContent = itemContent,
                ItemName = itemName
            });
        }

        public Task<string> Retrieve(string itemName)
        {
            // Because there is no indication whether itemname is Unique or not
            // And also because on the instruction the implementation retrieve is return string not a collection
            // Then I decided to return the first item on the list that match the itemName

            var data = this._listItems.FirstOrDefault(x => x.ItemName == itemName);

            return (data == null) ? Task.FromResult(string.Empty) : Task.FromResult(data.ItemContent);
        }

        protected Task<bool> IsJsonFormat(string content)
        {
            try
            {
                JsonDocument.Parse(content);

                return Task.FromResult(true);
            }
            catch (JsonException)
            {
                return Task.FromResult(false);
            }
        }

        protected Task<bool> IsXMLFormat(string content)
        {
            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(content);
                return Task.FromResult(true);
            }
            catch (XmlException)
            {
                return Task.FromResult(false); ;
            }
        }

        public Task<int> Count()
        {
            return Task.FromResult(_listItems.Count());
        }
    }
}
