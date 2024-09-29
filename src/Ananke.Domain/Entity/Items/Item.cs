using Ananke.Domain.Entity.Items;
using System.ComponentModel.DataAnnotations;
using System.IO.MemoryMappedFiles;

namespace Ananke.Domain.Entity.Items
{
    public record Item : BaseEntity
    {
        private string? path;
        public int? FolderId { get; set; }
        public Folder? Folder { get; set; }
        [Required]
        public string? Name { get; set; }
        public int? ExtensionId { get; set; }
        public Extension? Extension { get; set; }
        public string Path
        {
            get
            {
                if (Name == null) return string.Empty;
                if (path == null)
                {
                    string folderPath = "";
                    string file = Name;
                    if (Folder != null && Folder.Path != null)
                        folderPath = Folder.Path;
                    if (Extension != null)
                        file += "." + Extension.Name;
                    path = System.IO.Path.Combine(folderPath, file);
                }
                return path;
            }
        }
    }
}