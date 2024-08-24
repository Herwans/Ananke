namespace Ananke.Api.Requests {
    public class AddDirectoryRequest {
        public string Path { get; set; }
        public bool Recursive { get; set; } = false;
    }
}