using Ananke.Application.DTO;
using Ananke.Application.Features.Items.Queries;
using Ananke.Domain.Entity;
using Ananke.Infrastructure.Repository;
using FluentAssertions;
using Moq.AutoMock;
using Moq;

namespace Ananke.Test.Application.Features.Items.Queries
{
    public class GetItem : ItemBaseTest
    {
        [Fact]
        public async Task GetItem_ValidResult_Test()
        {
            // Arrange
            Mock<IItemRepository> itemRepoMock = new();
            itemRepoMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(new Item() { Id = 3, Path = @"C:\c", Directory = @"C:\", Name = "c", Extension = null });

            AutoMocker autoMocker = new();
            autoMocker.Use(itemRepoMock.Object);

            var handler = new GetItemByIdQueryHandler(itemRepoMock.Object);
            var command = new GetItemByIdQuery(3);

            // Act
            ItemDTO? item = await handler.Handle(command, CancellationToken.None);

            // Assert
            item.Should().NotBeNull();
            item.Path.Should().Be(@"C:\c");
        }

        [Fact]
        public async Task GetItem_DoesntExist_Test()
        {
            // Arrange
            Mock<IItemRepository> itemRepoMock = new();
            itemRepoMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns<Item?>(null);

            AutoMocker autoMocker = new();
            autoMocker.Use(itemRepoMock.Object);

            var handler = new GetItemByIdQueryHandler(itemRepoMock.Object);
            var command = new GetItemByIdQuery(3);

            // Act
            ItemDTO? item = await handler.Handle(command, CancellationToken.None);

            // Assert
            item.Should().BeNull();
        }
    }
}