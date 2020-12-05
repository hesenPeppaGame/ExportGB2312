/*************************************************************************************
* 创建时间：2020年11月28日16:01:45
* 作    者：HeSen JR
* 说   明：输出GB2312标准的所有字符
 *************************************************************************************/
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExportGB2312Tool
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteGB2312File("D://gb2312.txt");
        }

        /// <summary>
        /// 输出GB2312所有字符
        /// 字符未输出，只输出了汉字
        /// </summary>
        /// <param name="path"></param>
        public static void WriteGB2312File2(string path)
        {
            StringBuilder sb = new StringBuilder();
            Encoding gb = Encoding.GetEncoding("gb2312");
            int count = 1;
            //输出所有ASCII
            for (int i = 1; i <= 255; i++)
            {
                char c = (char)i;//通过强制类型转换来将ASCII码对应的字符数出
                sb.Append(c.ToString());
            }

            byte[] by = new byte[2];
            for (int i = 0xB0; i < 0xF7; i++)
            {
                for (int l = 0xA1; l < 0xFE; l++)
                {
                    by[0] = (byte)i;
                    by[1] = (byte)l;
                    sb.Append(gb.GetString(by));
                    if (count % 40 == 0)
                    {
                        sb.AppendLine();
                    }

                    count++;
                }

            }

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(sb.ToString());
            }
        }


        /// <summary>
        /// 输出GB2312所有字符
        /// </summary>
        /// <param name="path"></param>
        private static void WriteGB2312File(string path)
        {
            StringBuilder sb = new StringBuilder();

            //输出所有ASCII
            for (int i = 1; i <= 255; i++)
            {
                char c = (char)i;//通过强制类型转换来将ASCII码对应的字符数出
                sb.Append(c);
            }

            //输出所有GB2312
            byte[] by = new byte[2];
            int count = 0;
            string source;
            Encoding gb = Encoding.GetEncoding("gb2312");
            for (int i = 109; i < 8894; i++)
            {
                by[0] = (byte)(i / 100 + 160);
                by[1] = (byte)(i % 100 + 160);
                source = gb.GetString(by);
                sb.Append(source);
                count++;
                if (count % 40 == 0)
                {
                    sb.AppendLine();
                }

                if (i.ToString().EndsWith("94"))
                {
                    i += 6;
                }
            }


            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(sb.ToString());
            }
        }
    }
}
