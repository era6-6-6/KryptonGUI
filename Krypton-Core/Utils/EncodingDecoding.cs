﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core.Utils
{
    public class EncodingDecoding
    {

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        
        public static String Encrypt(string message , string password)
        {
            try
            {
            
            

// Create sha256 hash
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));

// Create secret IV
            byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

            string encrypted = EncryptString(message, key, iv);
            string decrypted = DecryptString(message, key, iv);
            return encrypted;
            }
            catch (Exception ex)
            
            {
                Console.WriteLine(ex.Message);
                return "";
            }

            
        }
         public static String Decrypt(string message , string password)
        {
            
            
            try
            {
// Create sha256 hash
            SHA256 mySHA256 = SHA256Managed.Create();
            byte[] key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(password));

// Create secret IV
            byte[] iv = new byte[16] { 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0, 0x0 };

            string encrypted = EncryptString(message, key, iv);
            string decrypted = DecryptString(message, key, iv);
            return decrypted;
            }
            catch (Exception ex)
            
            {
                Console.WriteLine(ex.Message);
                return "";
            }

            
        }
        
        
        
        
        
        static string EncryptString(string plainText, byte[] key, byte[] iv)
        {
    // Instantiate a new Aes object to perform string symmetric encryption
    Aes encryptor = Aes.Create();
    encryptor.Padding = PaddingMode.PKCS7;
    encryptor.Mode = CipherMode.CBC;

    // Set key and IV
    byte[] aesKey = new byte[32];
    Array.Copy(key, 0, aesKey, 0, 32);
    encryptor.Key = aesKey;
    encryptor.IV = iv;

    // Instantiate a new MemoryStream object to contain the encrypted bytes
    MemoryStream memoryStream = new MemoryStream();

    // Instantiate a new encryptor from our Aes object
    ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();

    // Instantiate a new CryptoStream object to process the data and write it to the 
    // memory stream
    CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);

    // Convert the plainText string into a byte array
    byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);

    // Encrypt the input plaintext string
    cryptoStream.Write(plainBytes, 0, plainBytes.Length);

    // Complete the encryption process
    cryptoStream.FlushFinalBlock();

    // Convert the encrypted data from a MemoryStream to a byte array
    byte[] cipherBytes = memoryStream.ToArray();

    // Close both the MemoryStream and the CryptoStream
    memoryStream.Close();
    cryptoStream.Close();

    // Convert the encrypted byte array to a base64 encoded string
    string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);

    // Return the encrypted data as a string
    return cipherText;
}

    static string DecryptString(string cipherText, byte[] key, byte[] iv)
    {
        // Instantiate a new Aes object to perform string symmetric encryption
        Aes encryptor = Aes.Create();
        encryptor.Padding = PaddingMode.PKCS7;
        encryptor.Mode = CipherMode.CBC;

    // Set key and IV
        byte[] aesKey = new byte[32];
        Array.Copy(key, 0, aesKey, 0, 32);
        encryptor.Key = aesKey;
        encryptor.IV = iv;

    // Instantiate a new MemoryStream object to contain the encrypted bytes
        MemoryStream memoryStream = new MemoryStream();

    // Instantiate a new encryptor from our Aes object
        ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();

    // Instantiate a new CryptoStream object to process the data and write it to the 
    // memory stream
        CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);

    // Will contain decrypted plaintext
        string plainText = String.Empty;

        try
        {
            // Convert the ciphertext string into a byte array
             byte[] cipherBytes = Convert.FromBase64String(cipherText);

        // Decrypt the input ciphertext string
            cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);

        // Complete the decryption process
            cryptoStream.FlushFinalBlock();

        // Convert the decrypted data from a MemoryStream to a byte array
            byte[] plainBytes = memoryStream.ToArray();

        // Convert the decrypted byte array to string
            plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
     }
     finally
     {
        // Close both the MemoryStream and the CryptoStream
            memoryStream.Close();
            cryptoStream.Close();
     }

    // Return the decrypted data as a string
    return plainText;
}

  
    }
}
