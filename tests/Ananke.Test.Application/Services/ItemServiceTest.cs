using Ananke.Application.DTO;
using Ananke.Application.Services;
using Ananke.Domain.Entity;
using Ananke.Infrastructure.Repository;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace Ananke.Test.Application.Services
{
    public class ItemServiceTest
    {
        [Fact]
        public void AddItem_ValidResult_Test()
        {
            // Arrange
            List<Item> items = [
                new() { Path = @"C:\a" },
                new() { Path = @"C:\b" },
                new() { Path = @"C:\c" },
                new() { Path = @"C:\d" },
                new() { Path = @"C:\e" },
                new() { Path = @"C:\f" }
            ];

            Mock<IItemRepository> itemRepoMock = new();
            itemRepoMock.Setup(repo => repo.Add(It.IsAny<Item>())).Callback<Item>(item => items.Add(item));

            AutoMocker autoMocker = new();
            autoMocker.Use(itemRepoMock.Object);
            ItemService itemService = autoMocker.CreateInstance<ItemService>();

            // Act
            itemService.AddItem(@"C:\g");

            // Assert
            items.Should().HaveCount(7);
            items.Should().ContainEquivalentOf(new Item { Path = @"C:\g" });
        }

        [Fact]
        public void AddItem_AlreadyExists_Test()
        {
            // Arrange
            List<Item> items = [
                new() { Path = @"C:\a" },
                new() { Path = @"C:\b" },
                new() { Path = @"C:\c" },
                new() { Path = @"C:\d" },
                new() { Path = @"C:\e" },
                new() { Path = @"C:\f" }
            ];
            Mock<IItemRepository> itemRepoMock = new();
            itemRepoMock.Setup(repo => repo.GetItems()).Returns(items);
            itemRepoMock.Setup(repo => repo.Add(It.IsAny<Item>())).Callback<Item>(item => items.Add(item));

            AutoMocker autoMocker = new();
            autoMocker.Use(itemRepoMock.Object);
            ItemService itemService = autoMocker.CreateInstance<ItemService>();

            // Act
            itemService.AddItem(@"C:\e");

            // Assert
            items.Should().HaveCount(6);
            items.Should().ContainEquivalentOf(new Item { Path = @"C:\e" });
            items.Count(item => item.Path == @"C:\e").Should().Be(1);
        }

        [Fact]
        public void AddDirectory_ValidResult_Test()
        {
            // Arrange
            List<Item> items = [];

            var directoryMock = new Mock<IFileSystemService>();
            directoryMock.Setup(dir => dir.GetFiles(It.IsAny<string>())).Returns([
                @"C:\a",
                @"C:\b"
                ]);

            var itemRepoMock = new Mock<IItemRepository>();
            itemRepoMock.Setup(repo => repo.AddAll(It.IsAny<IEnumerable<Item>>())).Callback<IEnumerable<Item>>(param => items.AddRange(param));
            ItemService itemService = new ItemService(itemRepoMock.Object, directoryMock.Object);

            // Act
            itemService.AddDirectory(@"C:\");

            // Assert
            items.Should().HaveCount(2);
            items.Should().ContainEquivalentOf(new Item { Path = @"C:\a" });
            items.Should().ContainEquivalentOf(new Item { Path = @"C:\b" });
        }

        [Fact]
        public void AddDirectory_RecursiveValidResult_Test()
        {
            // Arrange
            List<Item> items = [];

            var directoryMock = new Mock<IFileSystemService>();
            directoryMock.SetupSequence(dir => dir.GetFiles(It.IsAny<string>())).Returns([
                @"C:\a",
                @"C:\b"
                ]).Returns([@"C:\folder\c"]);
            directoryMock.SetupSequence(dir => dir.GetDirectories(It.IsAny<string>())).Returns([@"C:\folder"]).Returns([]);
            var itemRepoMock = new Mock<IItemRepository>();
            itemRepoMock.Setup(repo => repo.AddAll(It.IsAny<IEnumerable<Item>>())).Callback<IEnumerable<Item>>(param => items.AddRange(param));
            ItemService itemService = new ItemService(itemRepoMock.Object, directoryMock.Object);

            // Act
            itemService.AddDirectory(@"C:\", true);

            // Assert
            items.Should().HaveCount(3);
            items.Should().ContainEquivalentOf(new Item { Path = @"C:\a" });
            items.Should().ContainEquivalentOf(new Item { Path = @"C:\b" });
            items.Should().ContainEquivalentOf(new Item { Path = @"C:\folder\c" });
        }

        [Fact]
        public void AddDirectory_AlreadyExists_Test()
        {
            // Arrange
            List<Item> items = [new() { Path = @"C:\a" }];

            var directoryMock = new Mock<IFileSystemService>();
            directoryMock.Setup(dir => dir.GetFiles(It.IsAny<string>())).Returns([
                @"C:\a",
                @"C:\b"
                ]);

            var itemRepoMock = new Mock<IItemRepository>();
            itemRepoMock.Setup(repo => repo.AddAll(It.IsAny<IEnumerable<Item>>())).Callback<IEnumerable<Item>>(param => items.AddRange(param));
            itemRepoMock.Setup(repo => repo.GetItems()).Returns(items);
            ItemService itemService = new(itemRepoMock.Object, directoryMock.Object);

            // Act
            itemService.AddDirectory(@"C:\");

            // Assert
            items.Should().HaveCount(2);
            items.Should().ContainEquivalentOf(new Item { Path = @"C:\a" });
            items.Should().ContainEquivalentOf(new Item { Path = @"C:\b" });
        }

        [Fact]
        public void GetItems_ValidResult_Test()
        {
            // Arrange
            List<Item> items = [new() { Path = @"C:\a" }];

            Mock<IItemRepository> itemRepoMock = new();
            itemRepoMock.Setup(repo => repo.GetItems()).Returns(items);

            AutoMocker autoMocker = new();
            autoMocker.Use(itemRepoMock.Object);
            ItemService itemService = autoMocker.CreateInstance<ItemService>();

            // Act
            var results = itemService.GetItems();

            // Assert
            results.Should().HaveCount(1);
            results.Should().ContainEquivalentOf(new ItemDTO { Path = @"C:\a" });
        }

        [Fact]
        public void RemoveItem_ValidResult_Test()
        {
            // Arrange
            List<Item> items = [
                new() { Id = 1, Path = @"C:\a" },
                new() { Id = 2, Path = @"C:\b" },
                new() { Id = 3, Path = @"C:\c" },
                new() { Id = 4, Path = @"C:\d" },
                new() { Id = 5, Path = @"C:\e" },
                new() { Id = 6, Path = @"C:\f" }
            ];

            Mock<IItemRepository> itemRepoMock = new();
            itemRepoMock.Setup(repo => repo.RemoveById(It.IsAny<int>())).Callback<int>(id => items.Remove(items.Find(i => i.Id == id)));

            AutoMocker autoMocker = new();
            autoMocker.Use(itemRepoMock.Object);
            ItemService itemService = autoMocker.CreateInstance<ItemService>();

            // Act
            itemService.RemoveItem(3);

            // Assert
            items.Should().HaveCount(5);
            items.Should().NotContainEquivalentOf(new Item { Path = @"C:\c" });
            items.Find(item => item.Id == 3).Should().BeNull();
        }

        [Fact]
        public void RemoveItem_DoesntExist_Test()
        {
            // Arrange
            List<Item> items = [
                new() { Id = 1, Path = @"C:\a" },
                new() { Id = 2, Path = @"C:\b" },
                new() { Id = 4, Path = @"C:\d" },
                new() { Id = 5, Path = @"C:\e" },
                new() { Id = 6, Path = @"C:\f" }
            ];

            Mock<IItemRepository> itemRepoMock = new();
            itemRepoMock.Setup(repo => repo.RemoveById(It.IsAny<int>())).Callback<int>(id => items.Remove(items.Find(i => i.Id == id)));

            AutoMocker autoMocker = new();
            autoMocker.Use(itemRepoMock.Object);
            ItemService itemService = autoMocker.CreateInstance<ItemService>();

            // Act
            itemService.RemoveItem(3);

            // Assert
            items.Should().HaveCount(5);
            items.Should().NotContainEquivalentOf(new Item { Path = @"C:\c" });
            items.Find(item => item.Id == 3).Should().BeNull();
        }

        [Fact]
        public void GetItem_ValidResult_Test()
        {
            // Arrange
            Mock<IItemRepository> itemRepoMock = new();
            itemRepoMock.Setup(repo => repo.GetItemById(It.IsAny<int>())).Returns(new Item() { Id = 3, Path = @"C:\c" });

            AutoMocker autoMocker = new();
            autoMocker.Use(itemRepoMock.Object);
            ItemService itemService = autoMocker.CreateInstance<ItemService>();

            // Act
            ItemDTO item = itemService.GetItem(3);

            // Assert
            item.Should().NotBeNull();
            item.Path.Should().Be(@"C:\c");
        }

        [Fact]
        public void GetItem_DoesntExist_Test()
        {
            // Arrange
            Mock<IItemRepository> itemRepoMock = new();
            itemRepoMock.Setup(repo => repo.GetItemById(It.IsAny<int>())).Returns<Item?>(null);

            AutoMocker autoMocker = new();
            autoMocker.Use(itemRepoMock.Object);
            ItemService itemService = autoMocker.CreateInstance<ItemService>();

            // Act
            ItemDTO item = itemService.GetItem(3);

            // Assert
            item.Should().BeNull();
        }
    }
}