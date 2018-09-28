using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace CMD.Shared.Helpers
{
    public static class Helper
    {
        public static string EncryptPassword(string Valor)

        {

            byte[] bytMensagem = System.Text.Encoding.UTF8.GetBytes(Valor);

            // Cria o Hash MD5 hash

            System.Security.Cryptography.MD5CryptoServiceProvider oMD5Provider = new System.Security.Cryptography.MD5CryptoServiceProvider();

            // Gera o Hash Code

            byte[] bytHashCode = oMD5Provider.ComputeHash(bytMensagem);

            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < bytHashCode.Length; i++)
            {
                sBuilder.Append(bytHashCode[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();

        }

        public static string GetEnumDescription(Enum value)
        {
            // Get the Description attribute value for the enum value
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        public static List<SelectListItem> EnumToSelect<T>()
        {
            var enumType = typeof(T);

            return Enum.GetValues(enumType).Cast<Enum>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = Convert.ToInt32(v).ToString()
            }).ToList();
        }
    }
}