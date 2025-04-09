using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEPAVerwaltungWPF.Helper;

public static class DatabaseInitializer
{
    public static void EnsureDatabaseCopied()
    {
        string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "KEPAVerwaltung");
        string targetDbPath = Path.Combine(appDataPath, "KEPAVerwaltung.mdf");
        string sourceDbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "KEPAVerwaltung.mdf"); // Originalpfad im MSIX-Paket

        if (!Directory.Exists(appDataPath))
        {
            Directory.CreateDirectory(appDataPath);
        }

        if (!File.Exists(targetDbPath))
        {
            File.Copy(sourceDbPath, targetDbPath, true);
        }
    }
}