using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace wsSysMobileREST.Areas.Api.Models
{
    public class Util
    {

        public static string fechaYYYYMMDDtoDDMMYYYY(string fecha)
        {
            return fecha.Substring(6, 2) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(0, 4);
        }

        public static string compressString(string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            MemoryStream ms = new MemoryStream();
            using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }

            ms.Position = 0;
            MemoryStream outStream = new MemoryStream();

            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            byte[] gzBuffer = new byte[compressed.Length + 4];
            System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return Convert.ToBase64String(gzBuffer);
        }

        public static string fmtLeerCodigo(string codigo, int largo) 
        {
            string codigoAlineado = codigo;

            Regex reg = new Regex("[0-9]");
            if (reg.IsMatch(codigo))
            {
                codigoAlineado = codigoAlineado.PadLeft(largo);
            }
            else 
            {
                codigoAlineado = codigoAlineado.PadRight(largo);
            }


            return codigoAlineado;
        }

    }
}