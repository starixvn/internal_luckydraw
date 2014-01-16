using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Simplicode.Imaging;
using Simplicode.Imaging.Filters;

namespace YearEndParty.Core
{
    public static class Utilities
    {
        public static void RemoveAll(IList<int> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                list.Remove(list[i]);
            }
        }
        public static Int32 AsInt32(object value, Int32 defaultValue, IFormatProvider provider = null)
        {
            if (value == null || value == System.DBNull.Value)
                return defaultValue;

            if (provider == null)
                provider = CultureInfo.CurrentCulture;

            Int32 retval;

            try
            {
                retval = Convert.ToInt32(value, provider);
            }
            catch (InvalidCastException)
            {
                retval = defaultValue;
            }
            catch (FormatException)
            {
                retval = defaultValue;
            }
            return retval;
        }

        public static Image BytesToImage(byte[] bytes)
        {
            return Image.FromStream(new MemoryStream(bytes));
        }

        public static Image ScaleImage(Image image, int width, int height)
        {
            var processor = new ImageProcessor();
            var resizer = new ResizeFilter(width, height, ResizeMethod.AspectRatioFill,
                AnchorLocation.Middle);
            processor.AddFilter(resizer);
            return processor.ProcessImage(image);
        }

        public static Image ScaleImage(byte[] bytes, int width, int height)
        {
            var processor = new ImageProcessor();
            var resizer = new ResizeFilter(width, height, ResizeMethod.AspectRatioFill,
                AnchorLocation.Middle);
            processor.AddFilter(resizer);
            return processor.ProcessImage(BytesToImage(bytes));
        }

        public static byte[] ImageToBytes(Image image, bool dispose = false)
        {
            byte[] bmpBytes = null;
            using (var ms = new MemoryStream())
            {
                ImageFormat format;
                if (image.RawFormat.Equals(ImageFormat.Png))
                {
                    format = ImageFormat.Png;
                }
                else if (image.RawFormat.Equals(ImageFormat.Gif))
                {
                    format = ImageFormat.Gif;
                }
                else
                {
                    format = ImageFormat.Jpeg;
                }

                image.Save(ms, format);

                // read to end
                bmpBytes = ms.GetBuffer();
                if (dispose) image.Dispose();
                ms.Close();
            }

            return bmpBytes;
        }

        /// <summary>
        /// Encrypt string with simple key.
        /// </summary>
        /// <param name="plaintext"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string EncryptTripleDes(string plaintext, string key)
        {
            var des = new TripleDESCryptoServiceProvider();
            var hashMd5 = new MD5CryptoServiceProvider();

            des.Key = hashMd5.ComputeHash(Encoding.ASCII.GetBytes(key));
            des.Mode = CipherMode.ECB;

            var desEncrypt = des.CreateEncryptor();
            var buffer = Encoding.ASCII.GetBytes(plaintext);
            var tripleDes = Convert.ToBase64String(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            return tripleDes;
        }

        /// <summary>
        /// Decrypt simple string using a key.
        /// </summary>
        /// <param name="base64Text"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string DecryptTripleDes(string base64Text, string key)
        {
            try
            {
                var des = new TripleDESCryptoServiceProvider();
                var hashMd5 = new MD5CryptoServiceProvider();

                des.Key = hashMd5.ComputeHash(Encoding.ASCII.GetBytes(key));
                des.Mode = CipherMode.ECB;

                var desDecrypt = des.CreateDecryptor();

                var buffer = Convert.FromBase64String(base64Text);
                var decTripleDes = Encoding.ASCII.GetString(desDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
                return decTripleDes;
            }
            catch (Exception ex)
            {
                //write log
                return null;
            }

        }
    }
}
