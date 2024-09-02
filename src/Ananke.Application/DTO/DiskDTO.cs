namespace Ananke.Application.DTO
{
    public class DiskDTO
    {
        public string Label { get; set; }
        public long TotalSpace { get; set; }
        public long AvailableSpace { get; set; }
        public string Name { get; internal set; }
    }
}