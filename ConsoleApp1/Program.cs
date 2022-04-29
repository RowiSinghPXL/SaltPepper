using System;
using System.Security.Cryptography;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string pwd = "Pxl123$";
            string salt = SaltPepper.GenerateSalt();
            string pepper = SaltPepper.GeneratePepper();

            Console.WriteLine("Voorbeeld:");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine("Paswoord: " + pwd);
            Console.WriteLine("Generated salt: " + salt);
            Console.WriteLine("Hashed pwd + salt: " + Hash.ComputeMd5Hash(pwd + salt));
            Console.WriteLine("Generated pepper: " + pepper);
            Console.WriteLine("Hashed pwd + pepper + salt: " + Hash.ComputeMd5Hash(pwd + pepper + salt));
        }
    }
    
    public static class SaltPepper
    {
        public static string GenerateSalt()
        {
            RNGCryptoServiceProvider rncCsp = new RNGCryptoServiceProvider();
            byte[] salt = new byte[32];
            rncCsp.GetBytes(salt);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < salt.Length; i++)
            {
                sb.Append(salt[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static string GeneratePepper()
        {
            RNGCryptoServiceProvider rncCsp = new RNGCryptoServiceProvider();
            byte[] pepper = new byte[14];
            rncCsp.GetBytes(pepper);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < pepper.Length; i++)
            {
                sb.Append(pepper[i].ToString("X2"));
            }
            return sb.ToString();

            
        }
    }

    public static class Hash
    {
        public static string ComputeMd5Hash(string message)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] input = Encoding.ASCII.GetBytes(message);
                byte[] hash = md5.ComputeHash(input);

                //string hashstring = Convert.ToBase64String(hash);

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
