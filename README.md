# Using OrmLite.Sqlite with a Desktop App

There are quite a few resources online on how to use OrmLite with a web-based application.  But there are fewer, if any resources on how to set up a desktop application using ServiceStack.OrmLite.Sqlite.

I've created a component that takes care of most of this process.  You only need to create a set of child classes with a few overrides to get going with a desktop application.  Everything else is pretty much taken care of.

This readme will take you step by step on creating, configuring and writing the necessary classes to get you going.  It will NOT cover any UI aspect.  That will be left for the future.

# CE.Domain

The component CE.Domain is this component.  It contains the basic infrastructure needed to setup a desktop application using OrmLite.Sqlite.

You can get the component one of two ways.  You can either install the NuGet package by searching for CE.Domain or clone the source code from https://github.com/MrMontana1889/CE.Domain (the current GitHub project you are looking at).  For the purpose of this readme, the NuGet package will be used.

Also note that this NuGet package also uses CE.Support and CE.Resources.  Make sure you are using the latest available version for both of these components.

## CE.Domain.Inventory

I recently wrote a program for my dad to keep track of "stuff" at home.  It's a basic inventory program.  It's nothing complicated but my CE.Domain component was perfect for this use case.  This readme will step you through on how to setup a component that uses CE.Domain and allows you to keep track of inventory.  This readme will not get into the UI aspects but perhaps in the future I will write a component that allows you to do that.

## Project Creation

Setup a new .NET Framework class library project named CE.Domain.Inventory (though you may use whatever you wish).

![New Project Create a new project named CE.Domain.Inventory](https://culinorg.files.wordpress.com/2018/07/new-project.png)

Rename the default Class1.cs class file to Enumerations.cs.  Inside this file, add the following code:

```csharp
    public struct InventoryTableNames
    {
        public const string Box = "Box";
        public const string Location = "Location";
        public const string Room = "Room";
        public const string InventoryItem = "InventoryItem";
    }

    public struct InventoryFieldNames
    {
        public const string Label = "Label";
        public const string Location = "Location";
        public const string Room = "Room";
        public const string Box = "Box";
        public const string Description = "Description";
        public const string Keyword1 = "Keyword1";
        public const string Keyword2 = "Keyword2";
        public const string Keyword3 = "Keyword3";
        public const string Keyword4 = "Keyword4";
        public const string Keyword5 = "Keyword5";
        public const string LocationId = "LocationId";
        public const string RoomId = "RoomId";
        public const string BoxId = "BoxId";

        public const string FilterDescription = "FilterDescription";
        public const string FilterKeyword1 = "FilterKeyword1";
        public const string FilterKeyword2 = "FilterKeyword2";
        public const string FilterKeyword3 = "FilterKeyword3";
        public const string FilterKeyword4 = "FilterKeyword4";
        public const string FilterKeyword5 = "FilterKeyword5";
        public const string FilterRoom = "FilterRoom";
        public const string FilterLocation = "FilterLocation";
        public const string FilterBox = "FilterBox";
    }

    public struct InventoryFieldLabelKeys
    {
        public const string Label = "LabelLabel";
        public const string Location = "LocationLabel";
        public const string Room = "RoomLabel";
        public const string Box = "BoxLabel";
        public const string Description = "DescriptionLabel";
        public const string Keyword1 = "Keyword1Label";
        public const string Keyword2 = "Keyword2Label";
        public const string Keyword3 = "Keyword3Label";
        public const string Keyword4 = "Keyword4Label";
        public const string Keyword5 = "Keyword5Label";
        public const string LocationId = "LocationIdLabel";
        public const string RoomId = "RoomIdLabel";
        public const string BoxId = "BoxIdLabel";

        public const string FilterDescription = "FilterDescriptionLabel";
        public const string FilterKeyword1 = "FilterKeyword1Label";
        public const string FilterKeyword2 = "FilterKeyword2Label";
        public const string FilterKeyword3 = "FilterKeyword3Label";
        public const string FilterKeyword4 = "FilterKeyword4Label";
        public const string FilterKeyword5 = "FilterKeyword5Label";
        public const string FilterRoom = "FilterRoomLabel";
        public const string FilterLocation = "FilterLocationLabel";
        public const string FilterBox = "FilterBoxLabel";
    }
```

The code above just declares some constants that will be used elsewhere for code readability.

## CE.Domain.DataObjects.Inventory

Add a new project to you solution named CE.Domain.DataObjects.Inventory.  This project will contain the down and dirty details of creating the data source and the tables inside of it.

Once the project is created, add a project reference to CE.Domain.Inventory.  You can also delete the default Class1.cs file that is added automatically.

At this point, add the NuGet package CE.Domain to the new project so it references the appropriate assemblies.  You should also add ServiceStack.OrmLite.Sqlite as well.  Make sure you select "ServiceStack.OrmLite.Sqlite" that comes up in the search results (version 5.1.0).

In the CE.Domain.DataObjects.Inventory project, create a new folder and name it Sqlite.  Inside this folder, create a new class and name it InventorySqliteDataConnection.cs.

This class is relatively small and very simple.  Configure the class as such:
```csharp
using CE.Domain.DataObjects.Sqlite;
using CE.Domain.DataObjects.Tables;
using CE.Domain.Inventory.DataObjects.Tables;
using System.Collections.Generic;

namespace CE.Domain.DataObjects.Inventory.Sqlite
{
    public class InventorySqliteDataConnection : GenericSqliteDataConnection
    {
        #region Constructor
        public InventorySqliteDataConnection()
        {

        }
        #endregion

        #region Protected Methods
        protected override void LoadSchemaImpl()
        {
            IList<ICreatableDomainTable> tables = new List<ICreatableDomainTable>();

            tables.Add(new InfoTable());
            tables.Add(new Box());
            tables.Add(new Room());
            tables.Add(new Location());
            tables.Add(new InventoryItem());

            foreach (ICreatableDomainTable table in tables)
                table.Create(DbConnection);
        }
        #endregion
    }
}
```

The code will not compile right away.  As you can see from the code above, there are some objects being referenced that don't yet exist.  To create these classes, follow these steps:

Add  a new folder in the project and name it Tables.

Inside the Tables folder, add the following classes.
1.Box
2.Room
3.Location
4.InventoryTableBase
5.InventoryItem

Note that the InfoTable class is already defined in the CE.Domain.DataObjects.dll reference.

Here is the content of each class file in the order listed above.
```csharp
//Box.cs
using CE.Support.Support;
using ServiceStack.DataAnnotations;

namespace CE.Domain.Inventory.DataObjects.Tables
{
    [Alias(InventoryTableNames.Box)]
    public class Box : InventoryTableBase, IEditLabeled
    {
        #region Constructor
        public Box() : base(InventoryTableNames.Box) { }
        #endregion

        #region Public Methods
        public override string ToString() => Label;
        #endregion

        #region Public Properties
        [Ignore]
        public override int TableTypeID => TableTypeId.Box;

        [AutoIncrement]
        [Required]
        [PrimaryKey]
        public override int Id { get; set; }

        [Required]
        [CustomField("TEXT")]
        public string Label { get; set; }
        #endregion
    }
}

//Room.cs
using CE.Support.Support;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System.Data;

namespace CE.Domain.Inventory.DataObjects.Tables
{
    public class Room : InventoryTableBase, IEditLabeled
    {
        #region Constructor
        public Room() : base(InventoryTableNames.Room) { }
        #endregion

        #region Public Methods
        public override string ToString() => Label;
        #endregion

        #region Public Properties
        [Ignore]
        public override int TableTypeID => TableTypeId.Room;

        [AutoIncrement]
        [Required]
        [PrimaryKey]
        public override int Id { get; set; }

        [CustomField("TEXT")]
        [Required]
        public string Label { get; set; }
        #endregion

        #region Protected Methods
        protected override void PostCreateTable(IDbConnection dbConnection)
        {
            dbConnection.Insert(new Room { Label = "Office" });
            dbConnection.Insert(new Room { Label = "Guest" });
            dbConnection.Insert(new Room { Label = "Master" });
            dbConnection.Insert(new Room { Label = "Kitchen" });
            dbConnection.Insert(new Room { Label = "Garage" });
            dbConnection.Insert(new Room { Label = "Storeroom" });
        }
        #endregion
    }
}

//Location.cs
using CE.Support.Support;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System.Data;

namespace CE.Domain.Inventory.DataObjects.Tables
{
    public class Location : InventoryTableBase, IEditLabeled
    {
        #region Constructor
        public Location() : base(InventoryTableNames.Location) { }
        #endregion

        #region Public Methods
        public override string ToString() => Label;
        #endregion

        #region Public Properties
        [Ignore]
        public override int TableTypeID => TableTypeId.Location;

        [AutoIncrement]
        [Required]
        [PrimaryKey]
        public override int Id { get; set; }

        [CustomField("TEXT")]
        [Required]
        public string Label { get; set; }
        #endregion

        #region Protected Methods
        protected override void PostCreateTable(IDbConnection dbConnection)
        {
            dbConnection.Insert(new Location { Label = "Floor" });
            dbConnection.Insert(new Location { Label = "Closet" });
            dbConnection.Insert(new Location { Label = "Cabinet" });
        }
        #endregion
    }
}

//InventoryTableBase.cs
using CE.Domain.DataObjects;
using System.Data;

namespace CE.Domain.Inventory.DataObjects.Tables
{
    public abstract class InventoryTableBase : DomainTableBase
    {
        #region Constructor
        public InventoryTableBase(string name) : base(name) { }
        #endregion

        #region Public Methods
        public override int Create_Index(IDbConnection dbConnection) => 0;
        #endregion

        #region Public Properties
        protected override string IndexName => string.Empty;
        protected override string IndexFieldName => string.Empty;
        #endregion
    }
}

//InventoryItem.cs
using ServiceStack.DataAnnotations;

namespace CE.Domain.Inventory.DataObjects.Tables
{
    [Alias(InventoryTableNames.InventoryItem)]
    public class InventoryItem : InventoryTableBase
    {
        #region Constructor
        public InventoryItem() : base(InventoryTableNames.InventoryItem) { }
        #endregion

        #region Public Properties
        [Ignore]
        public override int TableTypeID => TableTypeId.InventoryItem;

        [AutoIncrement]
        [Required]
        [PrimaryKey]
        public override int Id { get; set; }

        [CustomField("TEXT")]
        [Default("")]
        public string Description { get; set; }

        [CustomField("TEXT")]
        [Default("")]
        public string Keyword1 { get; set; }
        [CustomField("TEXT")]
        [Default("")]
        public string Keyword2 { get; set; }
        [CustomField("TEXT")]
        [Default("")]
        public string Keyword3 { get; set; }

        [CustomField("TEXT")]
        [Default("")]
        public string Keyword4 { get; set; }

        [CustomField("TEXT")]
        [Default("")]
        public string Keyword5 { get; set; }

        [References(typeof(Location))]
        public int LocationId { get; set; }

        [References(typeof(Room))]
        public int RoomId { get; set; }
        [References(typeof(Box))]
        public int BoxId { get; set; }

        [Ignore]
        public Location Location { get; set; }

        [Ignore]
        public Room Room { get; set; }

        [Ignore]
        public Box Box { get; set; }
        #endregion

        #region Protected Methods
        protected override void InitializeFields()
        {
            AddField(InventoryFieldNames.Description, typeof(string), InventoryFieldLabelKeys.Description, string.Empty, this);
            AddField(InventoryFieldNames.Keyword1, typeof(string), InventoryFieldLabelKeys.Keyword1, string.Empty, this);
            AddField(InventoryFieldNames.Keyword2, typeof(string), InventoryFieldLabelKeys.Keyword2, string.Empty, this);
            AddField(InventoryFieldNames.Keyword3, typeof(string), InventoryFieldLabelKeys.Keyword3, string.Empty, this);
            AddField(InventoryFieldNames.Keyword4, typeof(string), InventoryFieldLabelKeys.Keyword4, string.Empty, this);
            AddField(InventoryFieldNames.Keyword5, typeof(string), InventoryFieldLabelKeys.Keyword5, string.Empty, this);
            AddField(InventoryFieldNames.RoomId, typeof(int), InventoryFieldLabelKeys.RoomId, 0, this);
            AddField(InventoryFieldNames.LocationId, typeof(int), InventoryFieldLabelKeys.LocationId, 0, this);
            AddField(InventoryFieldNames.BoxId, typeof(int), InventoryFieldLabelKeys.BoxId, 0, this);
        }
        #endregion
    }
}
```

There is a lot going on here in just a few classes.  Let me summarize what is going on.
1.InventoryTableBase.cs
This file is the base class for the Box, Location, Room and InventoryItem classes.  It is mostly for convenience as it overrides a few methods so it doesn't have to be done in every subclass.
2.Box.cs
This is a very simple class.  The Box class becomes the Box table in the database when used in the LoadSchemaImpl method in the InventorySqliteDataConnection class.  It contains two columns - Id and Label.
3.Room.cs
This is also a very simple class.  The Room class becomes the Room table in the database when used in the LoadSchemaImpl method.  It also creates some default rooms in the override of PostCreateTable.
4.Location.cs
Another simple class.  The Location class becomes the Location table in the database.  It also creates some default locations by overriding the PostCreateTable method.
5.InventoryItem.cs
The InventoryItem class becomes the InventoryItem table in the database.  The InventoryItem references an ID that represents the room, and ID for the location and ID for the Box the item is located in.  For example, you could add a new inventory item described as "old modem" and it is located in the garage (room), on the floor (location) in box "1A" (box).  The room, location and box are IDs in this table that correspond to the ID in the Room, Location and Box tables.I will get into the details of the InitializeFields() method later.

Once you have these classes created and filled-in the CE.Domain.DataObjects.Inventory project will now compile.  If it does not compile, double-check your references and make sure you have the latest version for CE.Domain, CE.Support and CE.Resources.

## CE.Domain.ModelingObjects.Inventory

Think of the ModelingObjects project as where your business objects are located and the DataObjects project contains the classes that are specific to the data source itself.

There are several classes that need to be added to the CE.Domain.ModelingObjects.Inventory project.  For the purpose of this readme, I will get into only a few of them.  However, the source code for this solution will be available so you can download the source and review it in more detail.

Once again, add a new project and name it CE.Domain.ModelingObjects.Inventory.

Once added, rename the Class1.cs file into Interfaces.cs.

You will also need to add a project reference to CE.Domain.Inventory and CE.Domain.DataObjects.Inventory.  You will also need to add references via NuGet to CE.Domain and the latest version of CE.Support and CE.Resources.  Also add a reference to ServiceStack.OrmLite.Sqlite as well.  Once everything is referenced go ahead and build the project to make sure everything works.

In the Interfaces.cs file, add the following code:
```csharp
namespace CE.Domain.Inventory.ModelingObjects
{
    public interface IInventoryDataRepository : IDataRepository
    {
        InventoryItemManager InventoryItemManager { get; }
        BoxManager BoxManager { get; }
        RoomManager RoomManager { get; }
        LocationManager LocationManager { get; }
    }
}
```

Once you add this code the project will not build until those classes are defined.  See the attached source code for details of how to implement these managers.

Create a new folder and name it Repositories.  In the folder, add a new class named InventoryItemRepository.

The InventoryItemRepository is the true heart of the component.  This is where the CRUD implementations for the InventoryItem are located.  The InventoryItem has its own repository implementation since it needs to do some special loading in some cases.  The other objects, Box, Room and Location all use a generic repository.

Use the following code for the InventoryItemRepository class:
```csharp
using CE.Domain.DataObjects.Sqlite;
using CE.Domain.Inventory.DataObjects.Tables;
using CE.Domain.ModelingObjects.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CE.Domain.Inventory.ModelingObjects.Repositories
{
    public class InventoryItemRepository : GenericTableRepository<InventoryItem>
    {
        #region Constructor
        public InventoryItemRepository(ISqliteDataConnection dataConnection, IDataRepository dataRepository)
            : base(InventoryTableNames.InventoryItem, dataConnection)
        {
            DataRepository = dataRepository;
        }
        #endregion

        #region Public Methods
        public override List<InventoryItem> LoadItems(Expression<Func<InventoryItem, bool>> predicate) => LoadItems(predicate, null);

        public List<InventoryItem> LoadItems(Expression<Func<InventoryItem, bool>> predicate, InventoryFilter filter)
        {
            var items = base.LoadItems(predicate);
            List<InventoryItem> retVal = new List<InventoryItem>();
            foreach (var item in items)
            {
                PostLoad(item);
                if (filter != null)
                {
                    if (!filter.CheckAll(item))
                        continue;
                }
                retVal.Add(item);
            }
            return retVal;
        }
        public override List<InventoryItem> LoadItems() => LoadItems(null);

        public List<InventoryItem> LoadItems(InventoryFilter filter)
        {
            var items = base.LoadItems();
            List<InventoryItem> retVal = new List<InventoryItem>();
            foreach (var item in items)
            {
                PostLoad(item);
                if (filter != null)
                {
                    if (!filter.CheckAll(item))
                        continue;
                }
                retVal.Add(item);
            }
            return retVal;
        }
        public override InventoryItem Load(int id)
        {
            var retVal = base.Load(id);
            PostLoad(retVal);
            return retVal;
        }
        #endregion

        #region Protected Methods
        protected override void PostLoad(InventoryItem item)
        {
            int roomId = item.RoomId;
            int locationId = item.LocationId;
            int boxId = item.BoxId;

            ITableRepository<Room> roomRepository = DataRepository.GetTableRepositoryFor<Room>(InventoryTableNames.Room);
            ITableRepository<Location> locationRepository = DataRepository.GetTableRepositoryFor<Location>(InventoryTableNames.Location);
            ITableRepository<Box> boxRepository = DataRepository.GetTableRepositoryFor<Box>(InventoryTableNames.Box);

            item.Room = new Room();
            if (roomRepository.Exists(r => r.Id == roomId))
                item.Room = roomRepository[roomId];

            item.Location = new Location();
            if (locationRepository.Exists(l => l.Id == locationId))
                item.Location = locationRepository[locationId];

            item.Box = new Box();
            if (boxRepository.Exists(b => b.Id == boxId))
                item.Box = boxRepository[boxId];
        }
        #endregion

        #region Private Properties
        private IDataRepository DataRepository { get; set; }
        #endregion
    }
}
```

There are two main reasons for having a special repository for InventoryItem.  The first is for post-load.  When the InventoryItem is initially loaded only the ID for the room, location and box are set.  In the post load method, an instance of Room, Location and Box for those IDs are created and assigned to the InventoryItem object.  This makes it easier to access that data.

The second reason is the filter.  There is special code in place to filter the inventory items given specific conditions.  Dig into the source for more information on how that is implemented.

Inside the Repositories folder, create another class and name it InventoryDataRepositoryFactory.

This class is how the repository is instantiated.  Since there is a custom one for InventoryItem, we need to override a method and provide the instance of that class for the correct table name.

Here is the code to use for this class:
```csharp
using CE.Domain.DataObjects.Sqlite;
using CE.Domain.ModelingObjects.Repositories;

namespace CE.Domain.Inventory.ModelingObjects.Repositories
{
    public class InventoryDataRepositoryFactory : RepositoryFactory
    {
        #region Constructor
        public InventoryDataRepositoryFactory(IDataRepository dataRepository)
            : base(dataRepository)
        {

        }
        #endregion

        #region Public Methods
        public override ITableRepository<TTableType> CreateRepository<TTableType>(string name, ISqliteDataConnection dataConnection)
        {
            switch (name)
            {
                case InventoryTableNames.InventoryItem:
                    return (ITableRepository<TTableType>)(new InventoryItemRepository(dataConnection, DataRepository));
                default:
                    return base.CreateRepository<TTableType>(name, dataConnection);
            }
        }
        #endregion
    }
}
```

Note that in the override for CreateRepository, a switch is used checking the name of the table provided.  If the table name is InventoryItem, then it returns an instance of the InventoryItemRepository.  Otherwise, it calls the base implementation which just returns the generic version of the repository.

There are two more classes to create.  There are several others that are needed but they will not be covered in this readme.  Take a look at the source code for details.

Create a new class in the room of the CE.Domain.ModelingObjects.Inventory project and name it InventoryDataRepository.  Here is the code for that class:
```csharp
using CE.Domain.DataObjects.Sqlite;
using CE.Domain.Inventory.DataObjects.Tables;
using CE.Domain.Inventory.ModelingObjects.Repositories;
using CE.Domain.ModelingObjects;

namespace CE.Domain.Inventory.ModelingObjects
{
    public class InventoryDataRepository : DataRepository, IInventoryDataRepository
    {
        #region Constructor
        public InventoryDataRepository(ISqliteDataConnection dataConnection)
            : base(dataConnection)
        {

        }
        #endregion

        #region Public Properties
        public InventoryItemManager InventoryItemManager => _inventoryItemManager
            ?? (_inventoryItemManager = new InventoryItemManager(GetTableRepositoryFor<InventoryItem>(InventoryTableNames.InventoryItem)));

        public RoomManager RoomManager => _roomManager
            ?? (_roomManager = new RoomManager(GetTableRepositoryFor<Room>(InventoryTableNames.Room)));

        public LocationManager LocationManager => _locationManager
            ?? (_locationManager = new LocationManager(GetTableRepositoryFor<Location>(InventoryTableNames.Location)));

        public BoxManager BoxManager => _boxManager
            ?? (_boxManager = new BoxManager(GetTableRepositoryFor<Box>(InventoryTableNames.Box)));
        #endregion

        #region Protected Methods
        protected override IRepositoryFactory NewRepositoryFactory() => new InventoryDataRepositoryFactory(this);
        #endregion

        #region Private Fields
        private InventoryItemManager _inventoryItemManager;
        private BoxManager _boxManager;
        private RoomManager _roomManager;
        private LocationManager _locationManager;
        #endregion
    }
}
```

Take special note in the NewRepositoryFactory override where an instance of our repository factory is returned instead of a default factory.  This allows for the correct repository to be used when requesting one for the InventoryItem table.

The final class to create for the project is InventoryDataSource.  So go ahead and create a new class and name it InventoryDataSource.  Here is the source code to use:
```csharp
using CE.Domain.DataObjects.Inventory.Sqlite;
using CE.Domain.DataObjects.Sqlite;
using CE.Domain.ModelingObjects;

namespace CE.Domain.Inventory.ModelingObjects
{
    public class InventoryDataSource : DataSource
    {
        #region Constructor
        public InventoryDataSource()
        {

        }
        #endregion

        #region Protected Methods
        protected override IDataRepository NewDataRepository()
        {
            return new InventoryDataRepository(DataConnection);
        }
        protected override ISqliteDataConnection NewDataConnection()
        {
            return new InventorySqliteDataConnection();
        }
        #endregion
    }
}
```

The class overrides NewDataRepository and NewDataConnection.  For the first method, the InventoryDataRespository is created and returned.  This is important as the class will provide the correct RepositoryFactory.  The other method creates an instance of our data connection.  This is equally important as it will initialize the data source with the appropriate table so everything works correctly.

At this point you should be able to compile the three projects created and the code that was implemented.  You now have a component you can use to create an inventory of your stuff.

OK, well, you have the code that can do it.  But how exactly can it be used?  That's a great question and there's a relatively simple answer.

To best demonstrate how this new component can be used we'll use some Unit Tests.  "Unit Test" in this context is a generic term.  The purpose is to demonstrate how the new component can be used to create an inventory.

## CE.Domain.Inventory.Test

Lets create another project and name it CE.Domain.Inventory.Test.  This will be our test project.

Once created, add references to the 3 projects in the solution (CE.Domain.Inventory, CE.Domain.Inventory.DataObjects and CE.Domain.ModelingObjects.Inventory).  Also add via NuGet references to CE.Domain, CE.Support and CE.Resources along with ServiceStack.OrmLite.Sqlite, NUnit3 and FluentAssertions.

Rename the default Class1.cs class into InventoryDomainTestFixtureBase.cs.  Use the following code for the file:
using NUnit.Framework;

```csharp
using NUnit.Framework;

namespace CE.Domain.Inventory.Test
{
    public abstract class InventoryDomainTestFixtureBase
    {
        #region Constructor
        public InventoryDomainTestFixtureBase()
        {

        }
        #endregion

        #region Initialize/Cleanup
        [SetUp]
        public void Initialize()
        {
            InitializeLicense();
            InitializeImpl();
        }
        [TearDown]
        public void Cleanup()
        {
            CleanupImpl();
        }
        #endregion

        #region Protected Methods
        protected virtual void InitializeImpl() { }
        protected virtual void CleanupImpl() { }
        protected void InitializeLicense() { }
        #endregion

        #region Protected Properties
        protected IRepositoryDataSource DataSource { get; set; }
        protected string Filename { get; set; }
        #endregion
    }
}
```

Though "technically" not required, I like have a base test fixture class like this where some common stuff can be done for all unit tests.  In this case there is a way to initialize a license.  If you use more than 10 tables with OrmLite.Sqlite, you need a license (10 or below it is free to use).  For this test component we only have 4 tables so it is not a problem and therefore left as a no-op.

Create a new folder and name it ModelingObjects.  In the new folder create a new class and call it CudTestFixture.

In this test fixture we are going to add a new unit test that tests the "C" in CRUD which stands for "Create" (the rest are Retrieve, Update and Delete).

Here is the code for the unit test.  I will then go thru it so you understand what is being done.
```csharp
[Test]
public void Test_C_In_CRUD()
{
    using (DataSource dataSource = new InventoryDataSource())
    {
        Filename = Path.GetTempFileName();
        File.Delete(Filename);
        File.Exists(Filename).Should().BeFalse();

        dataSource.New(Filename);
        dataSource.IsOpen().Should().BeTrue();

        ITableRepository<Box> boxRepository = dataSource.DataRepository.GetTableRepositoryFor<Box>(InventoryTableNames.Box);
        boxRepository.Should().NotBeNull();

        Box box = new Box { Label = "A1" };
        boxRepository.Save(box);
        box.Id.Should().NotBe(0);

        ITableRepository<Room> roomRepository = dataSource.DataRepository.GetTableRepositoryFor<Room>(InventoryTableNames.Room);
        roomRepository.Should().NotBeNull();

        Room kitchen = roomRepository.Find(r => r.Label == "Kitchen");
        kitchen.Should().NotBeNull("Kitchen not found");

        ITableRepository<Location> locationRepository = dataSource.DataRepository.GetTableRepositoryFor<Location>(InventoryTableNames.Location);
        locationRepository.Should().NotBeNull();

        Location floor = locationRepository.Find(l => l.Label == "Floor");

        InventoryItem item = new InventoryItem
        {
            Description = "Test1",
            Keyword1 = "1",
            Keyword2 = "2",
            Keyword3 = "3",
            Keyword4 = "4",
            Keyword5 = "5",
            Location = floor,
            Room = kitchen,
        };

        ITableRepository<InventoryItem> inventoryRepository = dataSource.DataRepository.GetTableRepositoryFor<InventoryItem>(InventoryTableNames.InventoryItem);
        inventoryRepository.Should().NotBeNull();

        inventoryRepository.Save(item);
        item.Id.Should().NotBe(0);

        InventoryItem newItem = inventoryRepository[item.Id];
        newItem.Should().NotBeNull();

        newItem.Should().NotBeSameAs(item);
    }
}
```

Lets go thru the unit test piece by piece so it is better understood.
```csharp
using (DataSource dataSource = new InventoryDataSource())
{
	Filename = Path.GetTempFileName();
	File.Delete(Filename);
	File.Exists(Filename).Should().BeFalse();

	dataSource.New(Filename);
	dataSource.IsOpen().Should().BeTrue();
```

This part is pretty simplistic.

The using keyword is used here as DataSource implements IDisposable. it's also an easy way to make sure the data source is closed when the unit test ends.  If the unit test fails, Dispose() is still called and the Dispose() method will make sure the data source is properly closed.

The filename is initialized from GetTempFileName().  Now remember, when this method is called the file itself is also created.  But the New(string) method for the datasource expects the file NOT to exist.  This is why File.Delete is called.  Using FluentAssertions, Should() is used to make sure File.Exists returns false.

dataSource.New(Filename) creates a new file in the specified location.  For this component (and the use of CE.Domain) the data source is a Sqlite database.

The following line makes sure that the database is open.  If IsOpen() returns false then something went wrong.
```csharp
ITableRepository<Box> boxRepository = dataSource.DataRepository.GetTableRepositoryFor<Box>(InventoryTableNames.Box);
boxRepository.Should().NotBeNull();

Box box = new Box { Label = "A1" };
boxRepository.Save(box);
box.Id.Should().NotBe(0);
```

This part of the unit test is pretty obvious.  It retrieves a repository and since it is generic it needs to specify the type the repository will be associated with.  The name of the table is also created as a parameter to the method.

The code is followed by the creation of a Box object, the assignment of its label and then calling Save on the repository.  The Save call will automatically add or update the table depending on whether it finds the id.  If the id is 0, then it knows to add.  If the id is non-0 and it finds the id in the table it will update the table with the provided label.

The next line ensures that the Id was assigned and it is not 0.
```csharp
ITableRepository<Room> roomRepository = dataSource.DataRepository.GetTableRepositoryFor<Room>(InventoryTableNames.Room);
roomRepository.Should().NotBeNull();

Room kitchen = roomRepository.Find(r => r.Label == "Kitchen");
kitchen.Should().NotBeNull("Kitchen not found");
```
For this portion, the repository for the room is retrieved.  The test then finds the room labeled "Kitchen".  Using FluentAssertions it makes sure that the kitchen room object is not null.  Kitchen is one of the default rooms added.
```csharp
ITableRepository<Location> locationRepository = dataSource.DataRepository.GetTableRepositoryFor<Location>(InventoryTableNames.Location);
locationRepository.Should().NotBeNull();

Location floor = locationRepository.Find(l => l.Label == "Floor");
```
At this point the repository for the location is retrieved.  Nothing additional is done.  The default location of "Floor" is requested.
```csharp
InventoryItem item = new InventoryItem
{
	Description = "Test1",
	Keyword1 = "1",
	Keyword2 = "2",
	Keyword3 = "3",
	Keyword4 = "4",
	Keyword5 = "5",
	Location = floor,
	Room = kitchen,
};

ITableRepository<InventoryItem> inventoryRepository = dataSource.DataRepository.GetTableRepositoryFor<InventoryItem>(InventoryTableNames.InventoryItem);
inventoryRepository.Should().NotBeNull();

inventoryRepository.Save(item);
item.Id.Should().NotBe(0);
```
We are heading into the final stretch of the unit test.  At this point we have retrieved the repositories for room, location and box.  At this point we will also get the repository for InventoryItem.  Note that the default interface is being used but we know that internally our custom implementation of the repository is being returned.

A new inventory item is being instantiated and the location and room are being set to their respective objects previously created.  We know these are valid objects with an Id and Label.  However, we could provide an instance with just the label.  When Save is called, references are automatically created if they don't yet exist.  In this case that is not necessary as we are referencing and existing room and location.

The inventory item is saved by calling Save on the repository and passing in the object.  Using FluentASsertions, the object is verified as not having an id of 0.

Next is really a test of "R" in "CRUD" meaning "Retrieve".  The indexer on the repository takes in an id.  Internally it uses the id with the Find method to return the object. It will ALWAYS return a new instance of the object.  If the id is not found, it returns null.

In this case, the expected value is not null.  Using FluentAssertions, NotBeSameAs is used to make sure that the original item and the new item are not the same instance.

# Conclusion

At this point, you now have a component that uses the CE.Domain NuGet package that then uses ServiceStack.OrmLite.Sqlite to provide a data source using the Sqlite database.  The component can be used with a desktop application.  Each InventoryDataSource instance represents a single database file.  Depending on how you implement the UI, you could have a single data source behind the scenes or implement your application to manage multiple projects, each representing a different file.  That is totally up to you.

Or you could create a console application that takes parameters to execute specific commands.  For example, you could have an "ADDBOX" command to add a new box.  The next parameter would be the label.  You could have an "UPDATEBOX" and the next two commands would be the old label and label.  If the old label is found, it is updated to the new label.  This is just an example of what you could do.  Of course you will probably need a command to "open" a data source in order to persist the data.  But I leave it to you to figure out how you want to implement the UI.

# Source Code 

You can download the full source code for this sample [here](https://culinorg.files.wordpress.com/2018/07/ce-domain-inventory2.zip).
