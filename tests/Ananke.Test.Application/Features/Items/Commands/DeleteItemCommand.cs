using Ananke.Application.Features.Items.Commands;
using Ananke.Domain.Entity;
using Ananke.Infrastructure.Repository;
using FluentAssertions;
using Moq.AutoMock;
using Moq;

namespace Ananke.Test.Application.Features.Items.Commands
{
    public class DeleteItemCommand : ItemBaseTest
    {
        [Fact]
        public void RemoveItem_ValidResult_Test()
        {
            // Arrange
            int id = 1;
            List<Item> items = [
                item1,item2,item5,item4, itemExtension
            ];

            Mock<IItemRepository> itemRepoMock = new();
            itemRepoMock.Setup(repo => repo.RemoveByIdAsync(It.IsAny<int>())).Callback<int>(id => items.Remove(items.Find(i => i.Id == id)));

            AutoMocker autoMocker = new();
            autoMocker.Use(itemRepoMock.Object);

            var handler = new DeleteItemByIdCommandHandler(itemRepoMock.Object);
            var command = new DeleteItemByIdCommand(id);

            items.Find(item => item.Id == id).Should().NotBeNull();

            // Act
            handler.Handle(command, CancellationToken.None);

            // Assert
            items.Should().HaveCount(4);
            items.Should().NotContainEquivalentOf(item3);
            items.Find(item => item.Id == id).Should().BeNull();
        }

        [Fact]
        public void RemoveItem_DoesntExist_Test()
        {
            Folder folder = new() { Name = "C:\\" };

            // Arrange
            List<Item> items = [
                new() { Id = 1, },
            ];

            Mock<IItemRepository> itemRepoMock = new();
            itemRepoMock.Setup(repo => repo.RemoveByIdAsync(It.IsAny<int>())).Callback<int>(id => items.Remove(items.Find(i => i.Id == id)));

            AutoMocker autoMocker = new();
            autoMocker.Use(itemRepoMock.Object);

            var handler = new DeleteItemByIdCommandHandler(itemRepoMock.Object);
            var command = new DeleteItemByIdCommand(3);

            // Act
            handler.Handle(command, CancellationToken.None);

            // Assert
            items.Should().HaveCount(5);
            items.Should().NotContainEquivalentOf(new Item { Folder = folder, Name = "c", Extension = null });
            items.Find(item => item.Id == 3).Should().BeNull();
        }
    }
}