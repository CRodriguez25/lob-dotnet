namespace LobNet.Models
{
    public class LobImageFile
    {
        public string File { get; set; }
        public bool IsLocalPath { get; set; }

        /// <summary>
        /// Constructor that takes in file, and boolean to indicate whether or not the previous parameter
        /// represents a file saved locally somewhere on your machine. 
        /// </summary>
        /// <param name="file">The file to upload. This can be a URL, HTML string, or a path to a local file.</param>
        /// <param name="isLocalPath">Whether or not the file variable represents a path to a local file</param>
        public LobImageFile(string file, bool isLocalPath)
        {
            File = file;
            IsLocalPath = isLocalPath;
        }
    }
}