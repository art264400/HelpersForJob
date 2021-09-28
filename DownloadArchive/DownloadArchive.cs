using System.IO;
using System.IO.Compression;
using System.Text;

namespace das
{
    public static class DownloadArchive
    {
        public static byte[] ToArchiveAnyAmountFiles(string[] content, string[] fileName)
        {
            using (var compressedFileStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                {

                    for (int i = 0; i < content.Length; i++)
                    {
                        var entry = zipArchive.CreateEntry(fileName[i]);
                        using (var originalFileStream = new MemoryStream(content[i].ToBytes(Encoding.UTF8)))
                        using (var zipEntryStream = entry.Open())
                        {
                            originalFileStream.CopyTo(zipEntryStream);
                        }
                    }
                }
                return compressedFileStream.ToArray();
            }
        }
    }
}