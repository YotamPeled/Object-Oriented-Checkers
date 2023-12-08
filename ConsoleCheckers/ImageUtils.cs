using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleCheckers
{
    public static class ImageUtils
    {
        public static Bitmap ResizeImage(Image i_Image, int i_Length)
        {
            Image originalImage = i_Image;

            // Define the desired width and height for the resized image
            int desiredWidth = i_Length;
            int desiredHeight = i_Length;

            // Create a new bitmap with the desired width and height
            Bitmap resizedImage = new Bitmap(desiredWidth, desiredHeight);

            // Use Graphics to draw the resized image
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                // Set the interpolation mode for better quality
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                // Draw the original image onto the resized bitmap
                g.DrawImage(originalImage, 0, 0, desiredWidth, desiredHeight);
            }

            // Assuming you have a button named 'myButton' in your form
            return resizedImage;
        }

        public static void DrawCircleOnButton(Button button)
        {
            // Create a bitmap the same size as the button
            Bitmap bitmap = new Bitmap(button.Width, button.Height);

            // Use Graphics object from the bitmap
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Calculate circle dimensions and position
                int diameter = Math.Min(button.Width, button.Height) - 15;
                int x = (button.Width - diameter) / 2;
                int y = (button.Height - diameter) / 2;

                // Draw the circle
                using (Pen pen = new Pen(Color.Wheat, 2))
                {
                    g.DrawEllipse(pen, x, y, diameter, diameter);
                }
            }

            // Set the bitmap as the button's background image
            button.BackgroundImage = bitmap;
        }

        public static void ClearCircleFromButton(Button button)
        {
            // Clear the button's background image
            button.BackgroundImage = null;
        }
    }
}
