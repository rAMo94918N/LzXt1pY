// 代码生成时间: 2025-09-22 23:46:19
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessing
{
    /// <summary>
    /// A class to resize multiple images to a specified size.
    /// </summary>
    public class BatchImageResizer
    {
        private readonly string sourceDirectory;
        private readonly string targetDirectory;
        private readonly Size newSize;

        /// <summary>
        /// Initializes a new instance of the BatchImageResizer class.
        /// </summary>
        /// <param name="sourceDirectory">The directory containing the original images.</param>
        /// <param name="targetDirectory">The directory where resized images will be saved.</param>
        /// <param name="newSize">The new size for the images.</param>
        public BatchImageResizer(string sourceDirectory, string targetDirectory, Size newSize)
        {
            this.sourceDirectory = sourceDirectory;
            this.targetDirectory = targetDirectory;
            this.newSize = newSize;
        }

        /// <summary>
        /// Resizes all images in the source directory to the specified size.
        /// </summary>
        public void ResizeImages()
        {
            // Check if the source directory exists
            if (!Directory.Exists(sourceDirectory))
            {
                throw new DirectoryNotFoundException($"The source directory {sourceDirectory} was not found.");
            }

            // Check if the target directory exists, if not create it
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            // Get all image files in the source directory
            var imageFiles = Directory.GetFiles(sourceDirectory, "*", SearchOption.AllDirectories)
                .Where(f => Image.FromFile(f).RawFormat.Equals(ImageFormat.Png) || Image.FromFile(f).RawFormat.Equals(ImageFormat.Jpeg))
                .ToList();

            foreach (var file in imageFiles)
            {
                try
                {
                    // Create a new image from the file
                    using (Image image = Image.FromFile(file))
                    {
                        // Create a new bitmap to hold the resized image
                        using (var newImage = new Bitmap(newSize.Width, newSize.Height))
                        {
                            using (var graphics = Graphics.FromImage(newImage))
                            {
                                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                                // Draw the original image on the new bitmap to resize it
                                graphics.DrawImage(image, 0, 0, newSize.Width, newSize.Height);

                                // Save the resized image to the target directory
                                var targetPath = Path.Combine(targetDirectory, Path.GetFileName(file));
                                newImage.Save(targetPath, image.RawFormat);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception and continue with the next image
                    Console.WriteLine($"Error resizing image {file}: {ex.Message}");
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Example usage:
                var sourceDir = @"C:\OriginalImages";
                var targetDir = @"C:\ResizedImages";
                var newSize = new Size(800, 600); // Resize to 800x600

                var resizer = new BatchImageResizer(sourceDir, targetDir, newSize);
                resizer.ResizeImages();

                Console.WriteLine("All images have been resized.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
