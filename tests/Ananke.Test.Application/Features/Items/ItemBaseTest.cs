using Ananke.Domain.Entity;

namespace Ananke.Test.Application.Features.Items
{
    public class ItemBaseTest
    {
        protected static readonly Folder folder = new() { Name = "C:\\" };
        protected static readonly Folder folder2 = new() { Name = "C:\\this\\folder" };
        protected static readonly Extension extension = new() { Id = 1, Name = "txt" };
        protected static readonly Extension[] extensions = [extension];
        protected static readonly Item item1 = new() { Folder = folder, Name = "a", Extension = null };
        protected static readonly Item item2 = new() { Folder = folder, Name = "b", Extension = null };
        protected static readonly Item item3 = new() { Folder = folder, Name = "c", Extension = null };
        protected static readonly Item item4 = new() { Folder = folder, Name = "d", Extension = null };
        protected static readonly Item item5 = new() { Id = 1, Folder = folder, Name = "e", Extension = null };

        protected static readonly Item itemExtension = new() { Folder = folder2, Name = "file", Extension = extension };
        protected static readonly Item itemExtensionIdless = new() { Folder = folder2, Name = "file", Extension = null };
    }
}