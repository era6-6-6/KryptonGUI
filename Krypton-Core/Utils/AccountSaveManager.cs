using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Krypton_Core.Utils
{
    public class AccountSaveManager
    {
        public static Dictionary<string, string> Users;
        private static readonly string PATH = $@"{Environment.CurrentDirectory}/LoginData.txt";
        static string key = "TzWF63polcCM14spz9yFmkoCgPD5vFPC";
        public static void Save(string username, string password)
        {
            
            string data;
            if (!File.Exists(PATH))
            {
                var file = File.Create(PATH);
                file.Close();
            }
            using (StreamReader read = File.OpenText(PATH))
            {
                data = read.ReadToEnd();
            }
            if (data.Contains(username)) return;
            using (StreamWriter wr = File.AppendText(PATH))
            {
                wr.WriteLine($"{username},{EncryptString(key, password)};");
            }
        }
        public static void Load()
        {
            Console.WriteLine($"Key = '{key}'");
            Users = new Dictionary<string, string>();
            string nameAndPass;
            if (!File.Exists(PATH))
            {
                Console.WriteLine("Can't get the file");
                return;
            }
            Users.Clear();
            using (StreamReader read = File.OpenText(PATH))
            {
                while ((nameAndPass = read.ReadLine()) != null)
                {
                    var match = Regex.Match(nameAndPass, "(.*),(.*);");
                    if (match.Success)
                    {
                        Users.Add(match.Groups[1].Value, DecryptString(key, match.Groups[2].Value));
                    }

                }
            }
        }
        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }
        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
        public static string ComputeMd5Hash(string message)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] input = Encoding.ASCII.GetBytes(message);
                byte[] hash = md5.ComputeHash(input);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

    }
}
