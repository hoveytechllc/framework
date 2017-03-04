using System.IO;

namespace HoveyTech.Data.EfCore.Tests.TestContext
{
    public class TestDbLocationProvider
    {
        public static string TestPath;
        private static readonly object Lock = new object();

        public static string GetTestFilePath()
        {
            lock (Lock)
            {
                if (TestPath == null)
                {
                    TestPath = Path.Combine(Path.GetTempPath(), "HoveyTechFrameworkTests");

                    if (Directory.Exists(TestPath))
                    {
                        foreach (var filePath in Directory.GetFiles(TestPath, "*.*", SearchOption.AllDirectories))
                            File.Delete(filePath);

                        foreach (var dir in Directory.GetDirectories(TestPath, "*.*", SearchOption.AllDirectories))
                            Directory.Delete(dir);
                    }
                    else
                    {
                        Directory.CreateDirectory(TestPath);
                    }
                }

                return Path.Combine(TestPath, $"{Path.GetTempFileName()}.db");
            }
        }
    }
}
