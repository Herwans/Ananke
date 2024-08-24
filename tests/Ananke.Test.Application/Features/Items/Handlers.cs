using Ananke.Application.DTO;
using Ananke.Application.Services;
using Ananke.Domain.Entity;
using Ananke.Infrastructure.Repository;
using FluentAssertions;
using Moq.AutoMock;
using Moq;
using Ananke.Application.Features.Items.Commands;
using Ananke.Application.Features.Items.Queries;

namespace Ananke.Test.Application.Features.Items
{
    public class Handlers
    {
        [Fact]
        public void AddItemCommand_ValidResult_Test()
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
            var handler = new AddItemCommandHandler(itemRepoMock.Object);
            var command = new AddItemCommand(@"C:\g");

            // Act
            handler.Handle(command, CancellationToken.None);

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
            var handler = new AddItemCommandHandler(itemRepoMock.Object);
            var command = new AddItemCommand(@"C:\e");

            // Act
            handler.Handle(command, CancellationToken.None);

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

            var handler = new AddDirectoryCommandHandler(itemRepoMock.Object, directoryMock.Object);
            var command = new AddDirectoryCommand(@"C:\");

            // Act
            handler.Handle(command, CancellationToken.None);

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

            var handler = new AddDirectoryCommandHandler(itemRepoMock.Object, directoryMock.Object);
            var command = new AddDirectoryCommand(@"C:\", true);

            // Act
            handler.Handle(command, CancellationToken.None);

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

            var handler = new AddDirectoryCommandHandler(itemRepoMock.Object, directoryMock.Object);
            var command = new AddDirectoryCommand(@"C:\");

            // Act
            handler.Handle(command, CancellationToken.None);

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

            var handler = new GetItemsQueryHandler(itemRepoMock.Object);
            var command = new GetItemsQuery();

            // Act
            var results = handler.Handle(command, CancellationToken.None).Result;

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

            var handler = new DeleteItemByIdCommandHandler(itemRepoMock.Object);
            var command = new DeleteItemByIdCommand(3);

            // Act
            handler.Handle(command, CancellationToken.None);

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

            var handler = new DeleteItemByIdCommandHandler(itemRepoMock.Object);
            var command = new DeleteItemByIdCommand(3);

            // Act
            handler.Handle(command, CancellationToken.None);

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

            var handler = new GetItemByIdQueryHandler(itemRepoMock.Object);
            var command = new GetItemByIdQuery(3);

            // Act
            ItemDTO? item = handler.Handle(command, CancellationToken.None).Result;

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

            var handler = new GetItemByIdQueryHandler(itemRepoMock.Object);
            var command = new GetItemByIdQuery(3);

            // Act
            ItemDTO? item = handler.Handle(command, CancellationToken.None).Result;

            // Assert
            item.Should().BeNull();
        }
    }
}