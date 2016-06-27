using ICSharpCode.SharpZipLib.Zip;

namespace BBS.Converter
{
    public class Zipper
    {
        public Zipper()
        {
        }

        public static string UnzipReplay(string replayFile)
        {
            string filename = System.IO.Path.GetFileNameWithoutExtension(replayFile);
            string tmpDir = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "BBS", filename);
            System.IO.Directory.CreateDirectory(tmpDir);
            FastZip fastZip = new FastZip();
            fastZip.ExtractZip(replayFile, tmpDir, ".");

            string[] extractedFiles = System.IO.Directory.GetFiles(tmpDir, "*.*", System.IO.SearchOption.AllDirectories);
            return extractedFiles[0];
        }

        public static void ZipPath(string path, string zip)
        {
            FastZip fastZip = new FastZip();
            fastZip.CreateZip(zip, path, true, ".*");
        }
    }
}
