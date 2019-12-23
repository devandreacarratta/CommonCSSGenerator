using System;
using System.Security.Cryptography;
using System.Text;

namespace CommonCSSGenerator
{
    public class CSSElement
    {

        public CSSElement(string name, string value,string originalValue)
        {
            this.StyleName = name;
            this.StyleValue = value;
            this.StyleOriginalValue = originalValue;

            this.Key = GenerateItemKey(name, value);
        }

        private string GenerateItemKey(string name, string value)
        {
            using (SHA256 hashAlgorithm = SHA256.Create())
            {
                string input = $"{name}|{value}";
                byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }


        public string Key { get; private set; }

        public string StyleName { get; private set; }

        public string StyleValue { get; private set; }
        public string StyleOriginalValue { get; private set; }
    }
}