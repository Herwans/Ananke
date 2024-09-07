using Ananke.Application.Features.Items.Commands;
using Ananke.Application.Services;
using Ananke.Domain.Entity;
using Ananke.Infrastructure.Repository;
using FluentAssertions;
using Moq;

namespace Ananke.Test.Application.Features.Items.Commands
{
    public class AddDirectoryCommandTest : ItemBaseTest
    {
        [Fact]
        public void AddDirectory_ValidResult_Test()
        {
            // Arrange
            List<Item> items = [];

            var directoryMock = new Mock<IFileSystemService>();
            directoryMock.Setup(dir => dir.GetFiles(It.IsAny<string>())).Returns([
                item1.Path,
                item2.Path,
                ]);

            var itemRepoMock = new Mock<IItemRepository>();
            itemRepoMock.Setup(repo => repo.AddAll(It.IsAny<IEnumerable<Item>>())).Callback<IEnumerable<Item>>(param => items.AddRange(param));

            var handler = new AddDirectoryCommandHandler(itemRepoMock.Object, directoryMock.Object);
            var command = new AddDirectoryCommand(@"C:\");

            // Act
            handler.Handle(command, CancellationToken.None);

            // Assert
            items.Should().HaveCount(2);
            items.Should().ContainEquivalentOf(item1);
            items.Should().ContainEquivalentOf(item2);
        }

        [Fact]
        public void AddDirectory_RecursiveValidResult_Test()
        {
            // Arrange
            List<Item> items = [];

            var directoryMock = new Mock<IFileSystemService>();
            directoryMock.SetupSequence(dir => dir.GetFiles(It.IsAny<string>())).Returns([
                item1.Path,
                item2.Path,
                ]).Returns([itemExtension.Path]);
            directoryMock.SetupSequence(dir => dir.GetDirectories(It.IsAny<string>())).Returns([@"C:\folder"]).Returns([]);

            Mock<IItemRepository> itemRepoMock = new();
            Mock<IExtensionRepository> extensionRepoMock = new();
            extensionRepoMock.Setup(repo => repo.GetByName(It.IsAny<string>())).Returns(extension);

            itemRepoMock.Setup(repo => repo.AddAll(It.IsAny<IEnumerable<Item>>())).Callback<IEnumerable<Item>>(pars =>
            {
                foreach (Item param in pars)
                {
                    if (param.Path.EndsWith(".txt"))
                    {
                        param.Extension = extensionRepoMock.Object.GetByName("txt").Id;
                    }
                }
                items.AddRange(pars);
            });

            var handler = new AddDirectoryCommandHandler(itemRepoMock.Object, directoryMock.Object);
            var command = new AddDirectoryCommand(@"C:\", true);

            // Act
            handler.Handle(command, CancellationToken.None);

            // Assert
            items.Should().HaveCount(3);
            items.Should().ContainEquivalentOf(item1);
            items.Should().ContainEquivalentOf(item2);
            items.Should().ContainEquivalentOf(itemExtension);
        }

        [Fact]
        public void AddDirectory_AlreadyExists_Test()
        {
            // Arrange
            List<Item> items = [new() { Path = @"C:\a", Directory = @"C:\", Name = "a", Extension = null }];

            var directoryMock = new Mock<IFileSystemService>();
            directoryMock.Setup(dir => dir.GetFiles(It.IsAny<string>())).Returns([
                @"C:\a",
                @"C:\b"
                ]);

            var itemRepoMock = new Mock<IItemRepository>();
            itemRepoMock.Setup(repo => repo.AddAll(It.IsAny<IEnumerable<Item>>())).Callback<IEnumerable<Item>>(param => items.AddRange(param));
            itemRepoMock.Setup(repo => repo.GetAll()).Returns(items);

            var handler = new AddDirectoryCommandHandler(itemRepoMock.Object, directoryMock.Object);
            var command = new AddDirectoryCommand(@"C:\");

            // Act
            handler.Handle(command, CancellationToken.None);

            // Assert
            items.Should().HaveCount(2);
            items.Should().ContainEquivalentOf(new Item { Path = @"C:\a", Directory = @"C:\", Name = "a", Extension = null });
            items.Should().ContainEquivalentOf(new Item { Path = @"C:\b", Directory = @"C:\", Name = "b", Extension = null });
        }
    }
}