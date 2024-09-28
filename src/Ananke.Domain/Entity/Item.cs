using System.ComponentModel.DataAnnotations;

namespace Ananke.Domain.Entity
{
    public record Item : BaseEntity
    {
        public int? FolderId { get; set; }
        public Folder Folder { get; set; }
        [Required]
        public string? Name { get; set; }
        public int? ExtensionId { get; set; }
        public Extension? Extension { get; set; }
        public string Path
        {
            get
            {
                return System.IO.Path.Combine(Folder.Path, Name + (Extension == null ? "" : "." + Extension.Name));
            }
        }
    }
}