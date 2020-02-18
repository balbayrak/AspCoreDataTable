using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace AspCoreDataTable.Core.Storage
{
    public static class CompressHelper
    {
        public static string CompressString(this string s)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(s);

            using (var outputStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
                    gZipStream.Write(inputBytes, 0, inputBytes.Length);

                var outputBytes = outputStream.ToArray();

                s = Convert.ToBase64String(outputBytes);
            }
            return s;
        }
        public static string UnCompressString(this string s)
        {
            byte[] inputBytes = Convert.FromBase64String(s);

            using (var inputStream = new MemoryStream(inputBytes))
            using (var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress))
            using (var streamReader = new StreamReader(gZipStream))
            {
                s = streamReader.ReadToEnd();
            }
            return s;
        }

    }
}
