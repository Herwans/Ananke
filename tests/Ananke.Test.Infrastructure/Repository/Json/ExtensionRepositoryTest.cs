using Ananke.Domain.Entity;
using Ananke.Infrastructure.Repository.Json;
using FluentAssertions;

namespace Ananke.Test.Infrastructure.Repository.Json
{
    public class ExtensionRepositoryTest : JsonRepositoryBaseTest
    {
        [Fact]
        public void Add_Empty_Test()
        {
            string file = @"H:\" + System.Reflection.MethodBase.GetCurrentMethod().Name + ".json";
            DeleteFile(file).Should().BeTrue();

            ExtensionRepository extensionRepository = new ExtensionRepository(file);
            extensionRepository.Add("txt");
            File.Exists(file).Should().BeTrue();

            File.ReadAllText(file).Should().Be("[{\"Name\":\"txt\",\"Id\":1}]");

            DeleteFile(file).Should().BeTrue();
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

        [Fact]
        public void GetByName_Test()
        {
            string file = @"H:\" + System.Reflection.MethodBase.GetCurrentMethod().Name + ".json";
            File.Delete(file);
            File.Exists(file).Should().BeFalse();
            File.WriteAllText(file, "[{\"Name\":\"mp4\",\"Id\":1}]");

            ExtensionRepository extensionRepository = new ExtensionRepository(file);

            Extension? result = extensionRepository.GetByName("mp4");

            result.Should().BeEquivalentTo(new { Id = 1, Name = "mp4" });
            File.Delete(file);
        }

        [Fact]
        public void GetByName_Empty_Test()
        {
            string file = @"H:\" + System.Reflection.MethodBase.GetCurrentMethod().Name + ".json";
            DeleteFile(file).Should().BeTrue();

            File.WriteAllText(file, "[]");
            ExtensionRepository extensionRepository = new ExtensionRepository(file);

            Extension? result = extensionRepository.GetByName("mp4");

            result.Should().BeNull();

            DeleteFile(file).Should().BeTrue();
        }
    }
}