using Ananke.Domain.Entity;

namespace Ananke.Test.Application.Features.Items
{
    public class ItemBaseTest
    {
        protected static readonly Extension extension = new() { Id = 1, Name = "txt" };
        protected static readonly Extension[] extensions = [extension];
        protected static readonly Item item1 = new() { Path = @"C:\a", Directory = @"C:\", Name = "a", Extension = null };
        protected static readonly Item item2 = new() { Path = @"C:\b", Directory = @"C:\", Name = "b", Extension = null };
        protected static readonly Item item3 = new() { Path = @"C:\c", Directory = @"C:\", Name = "c", Extension = null };
        protected static readonly Item item4 = new() { Path = @"C:\d", Directory = @"C:\", Name = "d", Extension = null };
        protected static readonly Item item5 = new() { Id = 1, Path = @"C:\e", Directory = @"C:\", Name = "e", Extension = null };
        protected static readonly Item itemExtension = new() { Path = @"C:\this\folder\file.txt", Directory = @"C:\this\folder", Name = "file", Extension = 1 };
        protected static readonly Item itemExtensionIdless = new() { Path = @"C:\this\folder\file.txt", Directory = @"C:\this\folder", Name = "file", Extension = null };
    }
}