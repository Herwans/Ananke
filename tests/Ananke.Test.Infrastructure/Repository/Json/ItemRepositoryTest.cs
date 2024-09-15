using Ananke.Infrastructure.Repository.Json;
using FluentAssertions;

namespace Ananke.Test.Infrastructure.Repository.Json
{
    public class ItemRepositoryTest : JsonRepositoryBaseTest
    {
        [Fact]
        public void AddItem_Empty_Test()
        {
            string itemFile = @"H:\" + "Item" + System.Reflection.MethodBase.GetCurrentMethod().Name + ".json";
            string extensionFile = @"H:\" + "Extension" + System.Reflection.MethodBase.GetCurrentMethod().Name + ".json";
            DeleteFile(itemFile).Should().BeTrue();
            DeleteFile(extensionFile).Should().BeTrue();

            ExtensionRepository extensionRepository = new(extensionFile);
            ItemRepository itemRepository = new ItemRepository(itemFile, extensionRepository);
            itemRepository.Add(new() { Name = "test", Directory = "Z:\\TEST", Path = "Z:\\TEST\\test.txt" });
            File.Exists(itemFile).Should().BeTrue();

            File.ReadAllText(itemFile).Should().Be("[{\"Path\":\"Z:\\\\TEST\\\\test.txt\",\"Directory\":\"Z:\\\\TEST\",\"Name\":\"test\",\"Extension\":1,\"Id\":1}]");
            File.ReadAllText(extensionFile).Should().Be("[{\"Name\":\"txt\",\"Id\":1}]");

            DeleteFile(itemFile).Should().BeTrue();
            DeleteFile(extensionFile).Should().BeTrue();
        }

        [Fact]
        public void Add_CreateFolder_Test()
        {
            string directory = @"H:\" + System.Reflection.MethodBase.GetCurrentMethod().Name;
            string file = Path.Combine(directory, System.Reflection.MethodBase.GetCurrentMethod().Name + ".json");

            DeleteDirectory(directory).Should().BeTrue();

            ExtensionRepository extensionRepository = new ExtensionRepository(file);
            extensionRepository.Add("txt");
            File.Exists(file).Should().BeTrue();

            File.ReadAllText(file).Should().Be("[{\"Name\":\"txt\",\"Id\":1}]");
            DeleteDirectory(directory).Should().BeTrue();
        }

        [Fact]
        public void Add_AddToExistant_Test()
        {
            string file = @"H:\" + System.Reflection.MethodBase.GetCurrentMethod().Name + ".json";
            File.Delete(file);
            File.Exists(file).Should().BeFalse();
            File.WriteAllText(file, "[{\"Name\":\"mp4\",\"Id\":1}]");

            ExtensionRepository extensionRepository = new ExtensionRepository(file);

            extensionRepository.Add("txt");
            File.Exists(file).Should().BeTrue();

            File.ReadAllText(file).Should().Be("[{\"Name\":\"mp4\",\"Id\":1},{\"Name\":\"txt\",\"Id\":2}]");
            File.Delete(file);
        }

        [Fact]
        public void Add_AlreadyExist_Test()
        {
            string file = @"H:\" + System.Reflection.MethodBase.GetCurrentMethod().Name + ".json";
            DeleteFile(file).Should().BeTrue();
            File.WriteAllText(file, "[{\"Name\":\"mp4\",\"Id\":1}]");

            ExtensionRepository extensionRepository = new ExtensionRepository(file);

            extensionRepository.Add("mp4");
            File.Exists(file).Should().BeTrue();

            File.ReadAllText(file).Should().Be("[{\"Name\":\"mp4\",\"Id\":1}]");
            DeleteFile(file).Should().BeTrue();
        }
    }
}