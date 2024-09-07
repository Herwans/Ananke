using Ananke.Application.DTO;
using Ananke.Application.Features.Items.Queries;
using Ananke.Domain.Entity;
using Ananke.Infrastructure.Repository;
using Moq.AutoMock;
using Moq;
using FluentAssertions;

namespace Ananke.Test.Application.Features.Items.Queries
{
    public class GetItemsQueryTest : ItemBaseTest
    {
        [Fact]
        public async Task GetItems_ValidResult_Test()
        {
            // Arrange
            List<Item> items = [item1];

            Mock<IItemRepository> itemRepoMock = new();
            itemRepoMock.Setup(repo => repo.GetAll()).Returns(items);

            AutoMocker autoMocker = new();
            autoMocker.Use(itemRepoMock.Object);

            var handler = new GetItemsQueryHandler(itemRepoMock.Object);
            var command = new GetItemsQuery();

            // Act
            var results = await handler.Handle(command, CancellationToken.None);

            // Assert
            results.Should().HaveCount(1);
            results.Should().ContainEquivalentOf(new ItemDTO { Path = item1.Path });
        }
    }
}