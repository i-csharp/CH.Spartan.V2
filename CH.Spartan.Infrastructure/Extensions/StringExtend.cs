using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace CH.Spartan.Infrastructure
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExtend
    {
        #region 空值判断


        /// <summary>
        /// 判断是否存在中字符
        /// </summary>
        /// <param name="str">需要检验的字符串</param>
        /// <returns>存在字符：true代表是，false代表否</returns>
        public static bool HasValue(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        #endregion 空值判断

        #region 正则验证

        /// <summary>
        /// 判断是否存在中文和全角字符
        /// </summary>
        /// <param name="str">需要检验的字符串</param>
        /// <returns>存在中文和全角字符：true代表是，false代表否</returns>
        public static bool IsChinese(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            Regex rx = new Regex(@"[^\x00-\xff]");
            return rx.IsMatch(str);
        }

        /// <summary>
        /// 检验字符串是否为日期时间
        /// </summary>
        /// <param name="str">需要检验的字符串</param>
        /// <returns>是否为日期时间：true代表是，false代表否</returns>
        public static bool IsDateTime(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            Regex rx = new Regex(@"^[ ]*[012 ]?[0123456789]?[0123456789]{2}[ ]*[-]{1}[ ]*[01]?[0123456789]{1}[ ]*[-]{1}[ ]*[0123]?[0123456789]{1}[ ]*[012]?[0123456789]{1}[ ]*[:]{1}[ ]*[012345]?[0123456789]{1}[ ]*[:]{1}[ ]*[012345]?[0123456789]{1}[ ]*$");
            return rx.IsMatch(str);
        }

        /// <summary>
        /// 检验字符串是否为双字节字符(包括汉字)
        /// </summary>
        /// <param name="str">需要检验的字符串</param>
        /// <returns>是否为双字节字符：true代表是，false代表否</returns>
        public static bool IsDoubleByteChar(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            Regex rx = new Regex(@"[^x00-xff]");
            return rx.IsMatch(str);
        }

        /// <summary>
        /// 检验字符串是否为电子邮件
        /// </summary>
        /// <param name="str">需要检验的字符串</param>
        /// <returns>是否为电子邮件：true代表是，false代表否</returns>
        public static bool IsEmail(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            Regex rx = new Regex(@"w+([-+.]w+)*@w+([-.]w+)*.w+([-.]w+)*");
            return rx.IsMatch(str);
        }

        /// <summary>
        /// 校验是否为正的浮点数
        /// </summary>
        /// <param name="price">需要检验的字符串</param>
        /// <returns>是否为正浮点，是返回true，否则返回false</returns>
        public static bool IsFloat(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            Regex rx = new Regex(@"^[0-9]*(.)?[0-9]+$", RegexOptions.IgnoreCase);
            return rx.IsMatch(str.Trim());
        }

        /// <summary>
        /// 检验字符串是否为身份证号
        /// </summary>
        /// <param name="str">需要检验的字符串</param>
        /// <returns>是否为身份证号：true代表是，false代表否</returns>
        public static bool IsIDCard(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            Regex rx = new Regex(@"^[0123456789]{15,18}$");
            return rx.IsMatch(str);
        }

        /// <summary>
        /// 检验是否是整数
        /// </summary>
        /// <param name="str">需要检验的字符串</param>
        /// <returns>是否为整数：true是整数，false非整数</returns>
        public static bool IsInt(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            Regex rx = new Regex(@"^[0123456789]+$");
            return rx.IsMatch(str);
        }

        /// <summary>
        /// 检验字符串是否为IP地址
        /// </summary>
        /// <param name="str">需要检验的字符串</param>
        /// <returns>是否为IP地址：true代表是，false代表否</returns>
        public static bool IsIPAddress(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            Regex rx = new Regex(@"d+.d+.d+.d+");
            return rx.IsMatch(str);
        }

        /// <summary>
        /// 检验是否为数字
        /// </summary>
        /// <param name="str">需要检验的字符串</param>
        /// <returns>是否为数字：true代表是，false代表否</returns>
        public static bool IsNumber(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            Regex rx = new Regex(@"^[+-]?[0123456789]*[.]?[0123456789]*$");
            return rx.IsMatch(str);
        }

        /// <summary>
        /// 校验字符串是否只包含字母与数字
        /// </summary>
        /// <param name="toVerified">需要校验的字符串</param>
        /// <returns>true表示符合要求，false表示不符合要求</returns>
        public static bool IsOnlyLetterAndDigit(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            Regex rx = new Regex(@"^[a-zA-Z0-9-]*$");
            return rx.IsMatch(str.Trim(), 0);
        }

        /// <summary>
        /// 检验字符串是否为中国地区的电话号码
        /// </summary>
        /// <param name="str">需要检验的字符串</param>
        /// <returns>是否为中国地区的电话号码：true代表是，false代表否</returns>
        public static bool IsPhoneNumber(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            Regex rx = new Regex(@"((d{3,4})|d{3,4}-)?d{7,8}(-d{3})*");
            return rx.IsMatch(str);
        }

        /// <summary>
        /// 检验字符串是否为URL地址
        /// </summary>
        /// <param name="str">需要检验的字符串</param>
        /// <returns>是否为URL地址：true代表是，false代表否</returns>
        public static bool IsURLAddress(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            Regex rx = new Regex(@"[a-zA-z]+://[^s]*");
            return rx.IsMatch(str);
        }

        /// <summary>
        /// 检验字符串是否为邮政编码
        /// </summary>
        /// <param name="str">需要检验的字符串</param>
        /// <returns>是否为邮政编码：true代表是，false代表否</returns>
        public static bool IsZip(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            Regex rx = new Regex(@"^[0123456789]{6}$");
            return rx.IsMatch(str);
        }

        #endregion 正则验证

        #region 过滤HTML

        /// <summary>
        /// 过滤html
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FilerHTML(this string str)
        {
            if (str != "")
            {
                return Regex.Replace(str, "<[^>]*>", "");
            }
            else
            {
                return "";
            }
        }

        #endregion 过滤HTML

        #region 编码处理

        /// <summary>
        /// HtmlDecode
        /// </summary>
        public static string HtmlDecode(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return HttpUtility.HtmlDecode(str);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// HtmlEncode
        /// </summary>
        public static string HtmlEncode(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return HttpUtility.HtmlEncode(str);
            }
            else
            {
                return "";
            }
        }

        #endregion 编码处理

        #region 正则替换

        /// <summary>
        /// 正则替换
        /// </summary>
        /// <param name="oldStr"></param>
        /// <param name="pattern"></param>
        /// <param name="newStr"></param>
        /// <returns></returns>
        public static string RegexReplace(this string oldStr, string pattern, string newStr)
        {
            Regex r = new Regex(pattern, RegexOptions.IgnoreCase);
            return r.Replace(oldStr, newStr);
        }

        #endregion 正则替换

        #region 截取字符

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="suffix">截取以后的后缀</param>
        /// <returns></returns>
        public static string Substring(this string str, int length, string suffix)
        {
            if (string.IsNullOrEmpty(str))
                return "";

            if (str.Length <= length)
            {
                return str;
            }
            if (string.IsNullOrEmpty(suffix) || suffix.Length > length)
            {
                str = str.Substring(0, length);
            }
            else
            {
                str = str.Substring(0, length - suffix.Length);
            }
            return str;
        }

        #endregion 截取字符

        #region 获取后缀

        /// <summary>
        /// 获取文件后缀
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetExtension(this string filePath)
        {
            return Path.GetExtension(filePath);
        }

        #endregion 获取后缀

        #region 格式化

        /// <summary>
        /// 格式化
        /// </summary>
        /// <returns></returns>
        public static string GetFormat(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        #endregion 获取后缀

        #region 获取文件名

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileName(this string filePath)
        {
            return Path.GetFileName(filePath);
        }

        /// <summary>
        /// 获取文件名并包含后缀
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetFileNameWithoutExtension(this string filePath)
        {
            return Path.GetFileNameWithoutExtension(filePath);
        }

        #endregion 获取文件名

        #region MD5加密

        /// <summary>
        /// MD5 16位加密
        /// </summary>
        /// <param name="PassWordString">密码字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string GetMD5By16(this string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string ret = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(str)), 4, 8);
            ret = ret.Replace("-", "");
            return ret;
        }

        /// <summary>
        /// MD5 32位加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMD5By32(this string str)
        {
            byte[] b = Encoding.UTF8.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');

            return ret;
        }

        #endregion MD5加密

        #region DES加密与解密

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="str">解密内容</param>
        /// <param name="key">解密密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string GetDESDecrypt(this string str, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = str.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(str.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        /// <summary>
        /// DES加密数据
        /// </summary>
        /// <param name="str">加密内容</param>
        /// <param name="key">加密密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string GetDESEncrypt(this string str, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(str);
            des.Key = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(key, "md5").Substring(0, 8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        #endregion DES加密与解密

        #region 类型转换
        public static int ToInt(this string obj)
        {
            if (!obj.HasValue())
            {
                throw new ArgumentNullException("obj=null");
            }

            int result = 0;
            if (int.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            throw new ArgumentNullException("obj转型失败");
        }

        public static int ToInt(this string obj, int defaultValue)
        {
            if (!obj.HasValue())
            {
                return defaultValue;
            }

            int result = 0;
            if (int.TryParse(obj.ToString(), out result))
            {
                return result;
            }

            return defaultValue;
        }


        public static bool ToBool(this string obj)
        {
            if (!obj.HasValue())
            {
                throw new ArgumentNullException("obj=null");
            }

            bool result = false;

            if (bool.TryParse(obj.ToString().ToLower(), out result))
            {
                return result;
            }
            throw new ArgumentNullException("obj转型失败");
        }

        public static bool ToBool(this string obj, bool defaultValue)
        {
            if (!obj.HasValue())
            {
                throw new ArgumentNullException("obj=null");
            }

            bool result = false;

            if (bool.TryParse(obj.ToString().ToLower(), out result))
            {
                return result;
            }
            return defaultValue;
        }

        public static DateTime ToDataTime(this string obj)
        {
            if (!obj.HasValue())
            {
                throw new ArgumentNullException("obj=null");
            }

            DateTime result = DateTime.Now;

            if (DateTime.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            throw new ArgumentNullException("obj转型失败");
        }

        public static DateTime ToDataTime(this string obj, DateTime defaultValue)
        {
            if (!obj.HasValue())
            {
                throw new ArgumentNullException("obj=null");
            }

            DateTime result = DateTime.Now;

            if (DateTime.TryParse(obj.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }
        #endregion
    }
}
