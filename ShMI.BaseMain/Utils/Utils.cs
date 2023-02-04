using System;
using System.IO;
using System.Security.Cryptography;

namespace ShMI.BaseMain
{
    public static class Utils
    {
        public static string ShMI_Decrypt(string TextCode)
        {
            try
            {
                DESCryptoServiceProvider key = new DESCryptoServiceProvider
                {
                    Key = new byte[] { 21, 13, 137, 31, 27, 62, 73, 121 },
                    IV = new byte[] { 121, 113, 10, 131, 127, 162, 173, 19 }
                };

                int count = TextCode.Length / 3;
                byte[] buffer = new byte[count];
                for (int t = 0; t < count; t++)
                {
                    buffer[t] = byte.Parse(TextCode.Substring(0, 3));
                    TextCode = TextCode.Substring(3);
                }
                StreamReader sr = new StreamReader(new CryptoStream(new MemoryStream(buffer), key.CreateDecryptor(), CryptoStreamMode.Read));
                string PlainText = sr.ReadLine();
                sr.Close();
                return PlainText;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string ShMI_Encrypt(string PlainText)
        {
            try
            {
                DESCryptoServiceProvider key = new DESCryptoServiceProvider
                {
                    Key = new byte[] { 21, 13, 137, 31, 27, 62, 73, 121 },
                    IV = new byte[] { 121, 113, 10, 131, 127, 162, 173, 19 }
                };
                MemoryStream _memoryStream = new MemoryStream();
                StreamWriter _streamWriter = new StreamWriter(new CryptoStream(_memoryStream, key.CreateEncryptor(), CryptoStreamMode.Write));
                _streamWriter.WriteLine(PlainText);
                _streamWriter.Close();
                byte[] _buffer = _memoryStream.ToArray();
                _memoryStream.Close();
                string stringAll = "";
                foreach (byte _b in _buffer)
                {
                    string textWord = _b.ToString().PadLeft(3, '0');
                    stringAll += textWord;
                }
                return stringAll;
            }
            catch
            {
                return "";
            }
        }


        public static void SetLogServiceError(this string ProName, ModuleBaseMain module_Core, Exception er, string addMsg = "")
        {
            string msg = string.Format("{0:dd.MM.yyyy HH:mm:ss ffff} Процедура - {1} {2}", DateTime.Now, ProName, addMsg);
            msg += string.Format("\r\nMessage:\t{0}\r\nGetType:\t\t{1}\r\nStackTrace:\t{2}\r\nInnerException:\t{3}", er.Message, er.GetType(), er.StackTrace, er.InnerException);
            //_ = module_Core.AddMSG_POST(msg);
            //ModuleBaseMain.LogServiceStatic(msg);
        }

    }
}
