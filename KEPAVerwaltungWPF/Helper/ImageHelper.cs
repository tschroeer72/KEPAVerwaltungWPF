using System.Windows.Media.Imaging;
using System.Windows;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Media;

namespace KEPAVerwaltungWPF.Helper;

public static class ImageHelper
{
    public static ImageSource GetApplicationIcon()
    {
        // var mainWindow = Application.Current.MainWindow;
        // if (mainWindow != null && mainWindow.Icon != null)
        // {
        //     return mainWindow.Icon;
        // }
        // return null;
        
        string exePath = Assembly.GetExecutingAssembly().Location;

        if (File.Exists(exePath))
        {
            using (Icon icon = Icon.ExtractAssociatedIcon(exePath))
            {
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                    icon.Handle,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
        }

        return null;
    }
}