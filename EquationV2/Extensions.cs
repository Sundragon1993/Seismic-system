using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationV2
{
    public static class Extensions
    {

        public static int ReadInt(this FileStream file)

        {

            var arr = new byte[4];

            file.Read(arr, 0, 4);

            return BitConverter.ToInt32(arr, 0);

        }

        public static Int16 ReadInt16(this FileStream file)

        {

            return (Int16)(file.ReadByte() + (file.ReadByte() << 8));

        }

        public static string ReadStr(this FileStream file, int nlen)

        {

            var arr = new byte[nlen];

            file.Read(arr, 0, nlen);

            var str = Encoding.ASCII.GetString(arr);

            return str;

        }

        public static void Write(this FileStream file, byte[] arr)

        {

            file.Write(arr, 0, arr.Length);

        }

        public static void Write(this FileStream file, string str)

        {

            file.Write(Encoding.ASCII.GetBytes(str));

        }

        public static void Write(this FileStream file, int num)

        {

            file.Write(BitConverter.GetBytes(num));

        }

        public static void Write(this FileStream file, Int16 num)

        {

            file.Write(BitConverter.GetBytes(num));

        }

    }

}

