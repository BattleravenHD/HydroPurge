using System;
using System.IO;
using UnityEngine;

namespace Utils
{
    public class ScreenshotCapture : MonoBehaviour
    {
        public void CaptureScreenshot()
        {
            // Generate a unique file name based on the current timestamp
            string fileName = $"Screenshot_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.png";

            // Get the path to the user's desktop
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // Combine the desktop path and the file name to get the full save path
            string savePath = Path.Combine(desktopPath, fileName);

            // Capture the screenshot
            ScreenCapture.CaptureScreenshot(savePath);

            Debug.Log($"Screenshot captured and saved to: {savePath}");
        }
    }
}