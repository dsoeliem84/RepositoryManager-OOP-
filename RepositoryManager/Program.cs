using Petrolink.RepositoryManager;


// I provide 4 data (2 in XML format and the other 2 in JSON format)
string xmlData1 = "<?xml version=\"1.0\"?><data><name>John Doe</name><email>john.doe@Petrolink.com</email><phone>+62 812-345-678</phone><description>Fix Bug #1</description></data>";
string xmlData2 = "<?xml version=\"1.0\"?><data><name>Steve Jobs</name><email>steve.jobs@Petrolink.com</email><phone>+62 856-789-100</phone><description>Fix Bug #3</description></data>";

string jsonData1 = "{\"data\":{\"name\":\"Elon Musk\",\"email\":\"elon.musk@Petrolink.com\",\"phone\":\"+62 830-123-654\",\"description\":\"Fix Bug #2\"}}";
string jsonData2 = "{\"data:\":{\"name\":\"Jeff Bezos\",\"email\":\"jeff.bezos@Petrolink.com\",\"phone\":\"+62 812-888-000\",\"description\":\"Fix Bug #4\"}}";


// Create an object RepositoryManager and call Initialize()
RepositoryManager RM = new RepositoryManager();
await RM.Initialize();


//Register 4 repository items to the RepositoryManager object.
await RM.Register("item#1", xmlData1, ItemType.XML);
await RM.Register("item#2", jsonData1, ItemType.JSON);
await RM.Register("item#3", xmlData2, ItemType.XML);
await RM.Register("item#4", jsonData2, ItemType.JSON);


string item2 = await RM.Retrieve("item#2");

//I added an extra Method named Count() to help us get the total Repository Items in the Repository Manager
//At the beginning Repository Manager has 4 items as we registered at above lines
Console.WriteLine("Repository Manager has total: " + await RM.Count() + " data");

//Then I call Deregister() method to remove "Item#2" from the Repository Manager
Console.WriteLine("Item Repository no #2 contains= " + await RM.Retrieve("item#2"));
await RM.Deregister("item#2");

Console.WriteLine();
Console.WriteLine("Repository item 'item#2' has been removed");

Console.WriteLine();
//Total repository items now becomes 3. and item 2 has no item value
Console.WriteLine("Repository Manager has total: " + await RM.Count() + " data");
Console.WriteLine("Item Repository no #2 contains= " + await RM.Retrieve("item#2"));

Console.WriteLine();
Console.WriteLine("Item Repository no #3 contains " + await RM.GetType("item#3") + " data");
Console.WriteLine("Item Repository no #4 contains " + await RM.GetType("item#4") + " data");

Console.WriteLine();
Console.WriteLine("Item Repository no #3 contains= " + await RM.Retrieve("item#3"));
Console.WriteLine();
Console.WriteLine("Item Repository no #4 contains= " + await RM.Retrieve("item#4"));

// For future enhancement if we wanted to modify RepositoryManager to either store the data in text file or in database.
// Using TextFile: We can serialize and deserialize property List<RepositoryItem> _listItems so we can read and update the text file.
// Using DB: We can use RepositoryItem as Class model and use EntityFramework to replace List<> Add() method with DbContext.DbSet<RepositoryItem>.Add() 
