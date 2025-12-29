using JustSupportSystem.DTO;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace JustSupportSystem.JSystem
{

    public static class JExtensions
    {
        public readonly static string USER_COOKIE = "_adsense_re";
        public static double GetTwoDecimal(this double value)
        {
            return Math.Round(value, 2);
        }

        public static string GetCurrecyFormat(this double value)
        {
            return string.Format("{0:C}", value);
        }
        public static string FormatDatetime(this DateTime dateTime, string format = "yyyy-MM-dd hh:mm tt")
        {
            return dateTime.ToString(format);
        }
        public static string GetUniqueKey()
        {
            return Guid.NewGuid().ToString();
        }
        public static bool IsProduction()
        {
            return !Debugger.IsAttached;
        }
        public static string JSAToB(this string base64Encoded)
        {
            if (string.IsNullOrEmpty(base64Encoded)) return "";
            byte[] data = System.Convert.FromBase64String(base64Encoded);
            string base64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(data);
            return base64Decoded;
        }

        public static List<CodeName> GetCountries()
        {
            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID));
            return regions.GroupBy(p => new { p.TwoLetterISORegionName, p.EnglishName }).Select(p => new CodeName
            {
                Code = p.Key.TwoLetterISORegionName,
                Name = p.Key.EnglishName
            }).ToList();
        }

        public static bool IsValidCountry(this string data)
        {
            return GetCountries().Count(p => p.Code == data) > 0;
        }
        private static string GetSecurityKey()
        {
            return "X246C8DF239SD5931039B522E395D4W2";
        }
        public static string Encrypt(this string toEncrypt)
        {
            try
            {
                byte[] iv = new byte[16];
                byte[] array;
                //NEW CODE - AES
                var key = Encoding.UTF8.GetBytes(GetSecurityKey());
                using (var aesAlg = Aes.Create())
                {
                    aesAlg.Key = key;
                    aesAlg.IV = iv;
                    using (var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV))
                    {
                        using (var msEncrypt = new MemoryStream())
                        {
                            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                            using (var swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(toEncrypt);
                            }

                            array = msEncrypt.ToArray();

                        }
                    }
                    return Convert.ToBase64String(array);
                }
            }
            catch (Exception e)
            {
                return "Cannot Encrypt data";
            }

        }

        public static string RemoveHtmlTags(string content)
        {
            char[] array = new char[content.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < content.Length; i++)
            {
                char let = content[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        public static string LimitLength(this string data, int limit)
        {
            if (data == null)
            {
                return data;
            }
            if (data.Length > limit)
            {
                return data.Substring(0, limit);
            }
            return data;
        }
        public static string ToUrl(this string value)
        {
            var url = Regex.Replace(value.ToLower(), @"[^A-Za-z0-9_\.~]+", "-");
            url = url.TrimStart('-').TrimEnd('-');
            return url.LimitLength(199);
        }
        public static string ToOnlyApha(this string value)
        {
            return Regex.Replace(value, @"([\W+]|[\d+])", "");
        }

        public static string TrimBegin(this string data, string replaceString)
        {
            if (data.Length <= replaceString.Length)
            {
                return data;
            }
            return data.Substring(replaceString.Length);
        }

        public static string Decrypt(this string cipherString)
        {
            try
            {
                byte[] iv = new byte[16];
                var buffer = Convert.FromBase64String(cipherString.Replace(" ", "+"));
                using (var aesAlg = Aes.Create())
                {
                    aesAlg.Key = Encoding.UTF8.GetBytes(GetSecurityKey());
                    aesAlg.IV = iv;
                    using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                    {

                        string result;
                        using (var msDecrypt = new MemoryStream(buffer))
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (var srDecrypt = new StreamReader(csDecrypt))
                                {
                                    result = srDecrypt.ReadToEnd();
                                }
                            }
                        }

                        return result;
                    }
                }
            }
            catch (Exception e)
            {
                return "Cannot decrypt";
            }

        }
        public static bool IsValidateText(this string value, ValidationsType type)
        {
            Regex r;
            switch (type)
            {
                case ValidationsType.ALLOW_HTML:
                    return true;
                case ValidationsType.ALPHA_NUMERIC_SPACE:
                    r = new Regex("^[a-zA-Z0-9\\s]+$");
                    return r.IsMatch(value);
                case ValidationsType.ALPHA_SPACE:
                    r = new Regex("^[a-zA-Z\\s]+$");
                    return r.IsMatch(value);
                case ValidationsType.ALPHA_NUMERIC:
                    r = new Regex("^[a-zA-Z0-9]+$");
                    return r.IsMatch(value);
                case ValidationsType.ALPHA:
                    r = new Regex("^[a-zA-Z]+$");
                    return r.IsMatch(value);
                case ValidationsType.NUMERIC:
                    r = new Regex("^[0-9]+$");
                    return r.IsMatch(value);
                case ValidationsType.DECIMAL:
                    r = new Regex("^[0-9]+$");
                    return r.IsMatch(value.Replace(".", ""));
                case ValidationsType.DATE_TIME:
                    r = new Regex("([12]\\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\\d|3[01]) (0[0-9]|1[0-9]|2[1-4]):(0[0-9]|[1-5][0-9]):(0[0-9]|[1-5][0-9]))");
                    return r.IsMatch(value);
                case ValidationsType.STRONG_PASSWORD:
                    string password = value;
                    HashSet<char> specialCharacters = new HashSet<char>() { '%', '$', '#', '!', '@', '^', '&', '*', '~', '+', '-', '>', '<',
                        '?'
                    };
                    if (password.Any(char.IsLower) && password.Any(char.IsUpper) && password.Any(char.IsDigit) && password.Any(specialCharacters.Contains) && password.Length >= 8)
                    {
                        return true;
                    }
                    return false;
            }
            return false;
        }
    }
    public enum ValidationsType
    {
        FULLTEXT = 1,
        ALPHA_NUMERIC_SPACE = 2,
        ALPHA_NUMERIC = 3,
        ALPHA_SPACE = 4,
        ALPHA = 5,
        NUMERIC = 6,
        STRONG_PASSWORD = 7,
        ALLOW_HTML = 8,
        DECIMAL = 9,
        CUSTOM_REGEX = 10,
        DATE_TIME = 11,
    }
}
