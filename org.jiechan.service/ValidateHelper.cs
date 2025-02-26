﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace org.jiechan.service {

	public class ValidateHelper
	{
		private static readonly Regex RegNumber = new Regex("^[0-9]+$");
		private static readonly Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
		private static readonly Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
		private static readonly Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$");
		private static readonly Regex RegEmail = new Regex("^[\\w-]+(\\.[\\w-]+)*@[\\w-]+(\\.[\\w-]+)+$");
		private static readonly Regex RegCHZN = new Regex("[一-龥]");
		public static int GetStringLength(string stringValue)
		{
			return Encoding.Default.GetBytes(stringValue).Length;
		}
		public static bool IsValidUserName(string userName)
		{
			int stringLength = ValidateHelper.GetStringLength(userName);
			return stringLength >= 4 && stringLength <= 20 && Regex.IsMatch(userName, "^([\\u4e00-\\u9fa5A-Za-z_0-9]{0,})$");
		}
		public static bool IsValidPassword(string password)
		{
			return Regex.IsMatch(password, "^[A-Za-z_0-9]{6,16}$");
		}
		public static bool IsValidInt(string val)
		{
			return Regex.IsMatch(val, "^[1-9]\\d*\\.?[0]*$");
		}
		public static bool IsNumber(string inputData)
		{
			Match match = ValidateHelper.RegNumber.Match(inputData);
			return match.Success;
		}
		public static bool IsNumberSign(string inputData)
		{
			Match match = ValidateHelper.RegNumberSign.Match(inputData);
			return match.Success;
		}
		public static bool IsDecimal(string inputData)
		{
			Match match = ValidateHelper.RegDecimal.Match(inputData);
			return match.Success;
		}
		public static bool IsDecimalSign(string inputData)
		{
			Match match = ValidateHelper.RegDecimalSign.Match(inputData);
			return match.Success;
		}
		public static bool IsHasCHZN(string inputData)
		{
			Match match = ValidateHelper.RegCHZN.Match(inputData);
			return match.Success;
		}
		public static int GetCHZNLength(string inputData)
		{
			ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
			byte[] bytes = aSCIIEncoding.GetBytes(inputData);
			int num = 0;
			for (int i = 0; i <= bytes.Length - 1; i++)
			{
				if (bytes[i] == 63)
				{
					num++;
				}
				num++;
			}
			return num;
		}
		public static bool IsIdCard(string idCard)
		{
			if (string.IsNullOrEmpty(idCard))
			{
				return false;
			}
			if (idCard.Length == 15)
			{
				return Regex.IsMatch(idCard, "^[1-9]\\d{7}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])\\d{3}$");
			}
			return idCard.Length == 18 && Regex.IsMatch(idCard, "^[1-9]\\d{5}[1-9]\\d{3}((0\\d)|(1[0-2]))(([0|1|2]\\d)|3[0-1])((\\d{4})|\\d{3}[A-Z])$", RegexOptions.IgnoreCase);
		}
		public static bool IsEmail(string inputData)
		{
			Match match = ValidateHelper.RegEmail.Match(inputData);
			return match.Success;
		}
		public static bool IsValidZip(string zip)
		{
			Regex regex = new Regex("^\\d{6}$", RegexOptions.None);
			Match match = regex.Match(zip);
			return match.Success;
		}
		public static bool IsValidPhone(string phone)
		{
			Regex regex = new Regex("^(\\(\\d{3,4}\\)|\\d{3,4}-)?\\d{7,8}$", RegexOptions.None);
			Match match = regex.Match(phone);
			return match.Success;
		}
		public static bool IsValidMobile(string mobile)
		{
			Regex regex = new Regex("^(13|15)\\d{9}$", RegexOptions.None);
			Match match = regex.Match(mobile);
			return match.Success;
		}
		public static bool IsValidPhoneAndMobile(string number)
		{
			Regex regex = new Regex("^(\\(\\d{3,4}\\)|\\d{3,4}-)?\\d{7,8}$|^(13|15)\\d{9}$", RegexOptions.None);
			Match match = regex.Match(number);
			return match.Success;
		}
		public static bool IsValidURL(string url)
		{
			return Regex.IsMatch(url, "^(http|https|ftp)\\://[a-zA-Z0-9\\-\\.]+\\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\\-\\._\\?\\,\\'/\\\\\\+&%\\$#\\=~])*[^\\.\\,\\)\\(\\s]$");
		}
		public static bool IsValidIP(string ip)
		{
			return Regex.IsMatch(ip, "^((2[0-4]\\d|25[0-5]|[01]?\\d\\d?)\\.){3}(2[0-4]\\d|25[0-5]|[01]?\\d\\d?)$");
		}
		public static bool IsValidDomain(string host)
		{
			Regex regex = new Regex("^\\d+$");
			return host.IndexOf(".") != -1 && !regex.IsMatch(host.Replace(".", string.Empty));
		}
		public static bool IsBase64String(string str)
		{
			return Regex.IsMatch(str, "[A-Za-z0-9\\+\\/\\=]");
		}
		public static bool IsGuid(string guid)
		{
			return !string.IsNullOrEmpty(guid) && Regex.IsMatch(guid, "[A-F0-9]{8}(-[A-F0-9]{4}){3}-[A-F0-9]{12}|[A-F0-9]{32}", RegexOptions.IgnoreCase);
		}
		public static bool IsDate(string strValue)
		{
			return Regex.IsMatch(strValue, "^((\\d{2}(([02468][048])|([13579][26]))[\\-\\/\\s]?((((0?[13578])|(1[02]))[\\-\\/\\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\\-\\/\\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\\-\\/\\s]?((0?[1-9])|([1-2][0-9])))))|(\\d{2}(([02468][1235679])|([13579][01345789]))[\\-\\/\\s]?((((0?[13578])|(1[02]))[\\-\\/\\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\\-\\/\\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\\-\\/\\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))");
		}
		public static bool IsDateHourMinute(string strValue)
		{
			return Regex.IsMatch(strValue, "^(19[0-9]{2}|[2-9][0-9]{3})-((0(1|3|5|7|8)|10|12)-(0[1-9]|1[0-9]|2[0-9]|3[0-1])|(0(4|6|9)|11)-(0[1-9]|1[0-9]|2[0-9]|30)|(02)-(0[1-9]|1[0-9]|2[0-9]))\\x20(0[0-9]|1[0-9]|2[0-3])(:[0-5][0-9]){1}$");
		}
		public static string CheckMathLength(string inputData, int maxLength)
		{
			if (!string.IsNullOrEmpty(inputData))
			{
				inputData = inputData.Trim();
				if (inputData.Length > maxLength)
				{
					inputData = inputData.Substring(0, maxLength);
				}
			}
			return inputData;
		}
		public static string Encode(string str)
		{
			str = str.Replace("&", "&amp;");
			str = str.Replace("'", "''");
			str = str.Replace("\"", "&quot;");
			str = str.Replace(" ", "&nbsp;");
			str = str.Replace("<", "&lt;");
			str = str.Replace(">", "&gt;");
			str = str.Replace("\n", "<br>");
			return str;
		}
		public static string Decode(string str)
		{
			str = str.Replace("<br>", "\n");
			str = str.Replace("&gt;", ">");
			str = str.Replace("&lt;", "<");
			str = str.Replace("&nbsp;", " ");
			str = str.Replace("&quot;", "\"");
			return str;
		}
	}
}
