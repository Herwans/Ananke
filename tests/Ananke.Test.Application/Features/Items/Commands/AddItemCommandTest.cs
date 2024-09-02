using Ananke.Application.Features.Items.Commands;
using Ananke.Domain.Entity;
using Ananke.Infrastructure.Repository;
using FluentAssertions;
using Moq.AutoMock;
using Moq;

namespace Ananke.Test.Application.Features.Items.Commands
{
    public class AddItemCommandTest : ItemBaseTest
    {
        [Fact]
        public void AddItem_ExtensionLess_ValidResult_Test()
        {
            // Arrange
            List<Item> items = [];

            Mock<IItemRepository> itemRepoMock = new();
            itemRepoMock.Setup(repo => repo.Add(It.IsAny<Item>())).Callback<Item>(items.Add);

            AutoMocker autoMocker = new();
            autoMocker.Use(itemRepoMock.Object);
            var handler = new AddItemCommandHandler(itemRepoMock.Object);
            var command = new AddItemCommand(item4.Path);

            // Act
            handler.Handle(command, CancellationToken.None);

            // Assert
            items.Should().HaveCount(1);
            items.Should().ContainEquivalentOf(item4);
        }

        [Fact]
        public void AddItem_NonEmpty_ValidResult_Test()
        {
            // Arrange
            List<Item> items = [item1, item2];

            Mock<IItemRepository> itemRepoMock = new();
            itemRepoMock.Setup(repo => repo.Add(It.IsAny<Item>())).Callback<Item>(items.Add);

            AutoMocker autoMocker = new();
            autoMocker.Use(itemRepoMock.Object);
            var handler = new AddItemCommandHandler(itemRepoMock.Object);
            var command = new AddItemCommand(item4.Path);

            // Act
            handler.Handle(command, CancellationToken.None);

            // Assert
            items.Should().HaveCount(3);
            items.Should().ContainEquivalentOf(item4);
        }

        [Fact]
        public void AddItem_WithExtension_ValidResult_Test()
        {
            // Arrange
            List<Item> items = [];

            Mock<IItemRepository> itemRepoMock = new();
            Mock<IExtensionRepository> extensionRepoMock = new();
            itemRepoMock.Setup(repo => repo.Add(It.IsAny<Item>())).Callback<Item>(item =>
            {
                item.Extension = extensionRepoMock.Object.GetByName("txt").Id;
                items.Add(item);
            });
            extensionRepoMock.Setup(repo => repo.GetByName(It.IsAny<string>())).Returns(extension);

            AutoMocker autoMocker = new();
            autoMocker.Use(itemRepoMock.Object);
            var handler = new AddItemCommandHandler(itemRepoMock.Object);
            var command = new AddItemCommand(itemExtension.Path);

            // Act
            handler.Handle(command, CancellationToken.None);

            // Assert
            items.Should().HaveCount(1);
            items.Should().ContainEquivalentOf(itemExtension);
        }

        [Fact]
        public void AddItem_AlreadyExists_Test()
        {
            // Arrange
            List<Item> items = [
                item1
            ];
            Mock<IItemRepository> itemRepoMock = new();
            itemRepoMock.Setup(repo => repo.GetAll()).Returns(items);
            itemRepoMock.Setup(repo => repo.Add(It.IsAny<Item>())).Callback<Item>(item => items.Add(item));

            AutoMocker autoMocker = new();
            autoMocker.Use(itemRepoMock.Object);
            var handler = new AddItemCommandHandler(itemRepoMock.Object);
            var command = new AddItemCommand(item1.Path);

            // Act
            handler.Handle(command, CancellationToken.None);

            // Assert
            items.Should().HaveCount(1);
            items.Should().ContainEquivalentOf(item1);
            items.Count(item => item.Path == item1.Path).Should().Be(1);
        }
    }
}