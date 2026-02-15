using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Xml.Serialization.Configuration;

namespace System.Xml.Serialization
{
	// Token: 0x020001E8 RID: 488
	internal class XmlCustomFormatter
	{
		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001931 RID: 6449 RVA: 0x000963C4 File Offset: 0x000945C4
		private static DateTimeSerializationSection.DateTimeSerializationMode Mode
		{
			get
			{
				if (XmlCustomFormatter.mode == DateTimeSerializationSection.DateTimeSerializationMode.Default)
				{
					DateTimeSerializationSection dateTimeSerializationSection = PrivilegedConfigurationManager.GetSection(ConfigurationStrings.DateTimeSerializationSectionPath) as DateTimeSerializationSection;
					if (dateTimeSerializationSection != null)
					{
						XmlCustomFormatter.mode = dateTimeSerializationSection.Mode;
					}
					else
					{
						XmlCustomFormatter.mode = DateTimeSerializationSection.DateTimeSerializationMode.Roundtrip;
					}
				}
				return XmlCustomFormatter.mode;
			}
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00002127 File Offset: 0x00000327
		private XmlCustomFormatter()
		{
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00096404 File Offset: 0x00094604
		internal static string FromDefaultValue(object value, string formatter)
		{
			if (value == null)
			{
				return null;
			}
			Type type = value.GetType();
			if (type == typeof(DateTime))
			{
				if (formatter == "DateTime")
				{
					return XmlCustomFormatter.FromDateTime((DateTime)value);
				}
				if (formatter == "Date")
				{
					return XmlCustomFormatter.FromDate((DateTime)value);
				}
				if (formatter == "Time")
				{
					return XmlCustomFormatter.FromTime((DateTime)value);
				}
			}
			else if (type == typeof(string))
			{
				if (formatter == "XmlName")
				{
					return XmlCustomFormatter.FromXmlName((string)value);
				}
				if (formatter == "XmlNCName")
				{
					return XmlCustomFormatter.FromXmlNCName((string)value);
				}
				if (formatter == "XmlNmToken")
				{
					return XmlCustomFormatter.FromXmlNmToken((string)value);
				}
				if (formatter == "XmlNmTokens")
				{
					return XmlCustomFormatter.FromXmlNmTokens((string)value);
				}
			}
			throw new Exception(Res.GetString("The default value type, {0}, is unsupported.", new object[] { type.FullName }));
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00096511 File Offset: 0x00094711
		internal static string FromDate(DateTime value)
		{
			return XmlConvert.ToString(value, "yyyy-MM-dd");
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x00096520 File Offset: 0x00094720
		internal static string FromTime(DateTime value)
		{
			if (!LocalAppContextSwitches.IgnoreKindInUtcTimeSerialization && value.Kind == DateTimeKind.Utc)
			{
				return XmlConvert.ToString(DateTime.MinValue + value.TimeOfDay, "HH:mm:ss.fffffffZ");
			}
			return XmlConvert.ToString(DateTime.MinValue + value.TimeOfDay, "HH:mm:ss.fffffffzzzzzz");
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00096575 File Offset: 0x00094775
		internal static string FromDateTime(DateTime value)
		{
			if (XmlCustomFormatter.Mode == DateTimeSerializationSection.DateTimeSerializationMode.Local)
			{
				return XmlConvert.ToString(value, "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz");
			}
			return XmlConvert.ToString(value, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00096592 File Offset: 0x00094792
		internal static string FromChar(char value)
		{
			return XmlConvert.ToString((ushort)value);
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x0009659A File Offset: 0x0009479A
		internal static string FromXmlName(string name)
		{
			return XmlConvert.EncodeName(name);
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x000965A2 File Offset: 0x000947A2
		internal static string FromXmlNCName(string ncName)
		{
			return XmlConvert.EncodeLocalName(ncName);
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x000965AA File Offset: 0x000947AA
		internal static string FromXmlNmToken(string nmToken)
		{
			return XmlConvert.EncodeNmToken(nmToken);
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x000965B4 File Offset: 0x000947B4
		internal static string FromXmlNmTokens(string nmTokens)
		{
			if (nmTokens == null)
			{
				return null;
			}
			if (nmTokens.IndexOf(' ') < 0)
			{
				return XmlCustomFormatter.FromXmlNmToken(nmTokens);
			}
			string[] array = nmTokens.Split(new char[] { ' ' });
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(XmlCustomFormatter.FromXmlNmToken(array[i]));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00096620 File Offset: 0x00094820
		internal static void WriteArrayBase64(XmlWriter writer, byte[] inData, int start, int count)
		{
			if (inData == null || count == 0)
			{
				return;
			}
			writer.WriteBase64(inData, start, count);
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00096632 File Offset: 0x00094832
		internal static string FromByteArrayHex(byte[] value)
		{
			if (value == null)
			{
				return null;
			}
			if (value.Length == 0)
			{
				return "";
			}
			return XmlConvert.ToBinHexString(value);
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x0009664C File Offset: 0x0009484C
		internal static string FromEnum(long val, string[] vals, long[] ids, string typeName)
		{
			long num = val;
			StringBuilder stringBuilder = new StringBuilder();
			int num2 = -1;
			for (int i = 0; i < ids.Length; i++)
			{
				if (ids[i] == 0L)
				{
					num2 = i;
				}
				else
				{
					if (val == 0L)
					{
						break;
					}
					if ((ids[i] & num) == ids[i])
					{
						if (stringBuilder.Length != 0)
						{
							stringBuilder.Append(" ");
						}
						stringBuilder.Append(vals[i]);
						val &= ~ids[i];
					}
				}
			}
			if (val != 0L)
			{
				throw new InvalidOperationException(Res.GetString("Instance validation error: '{0}' is not a valid value for {1}.", new object[]
				{
					num,
					(typeName == null) ? "enum" : typeName
				}));
			}
			if (stringBuilder.Length == 0 && num2 >= 0)
			{
				stringBuilder.Append(vals[num2]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x000966FC File Offset: 0x000948FC
		internal static object ToDefaultValue(string value, string formatter)
		{
			if (formatter == "DateTime")
			{
				return XmlCustomFormatter.ToDateTime(value);
			}
			if (formatter == "Date")
			{
				return XmlCustomFormatter.ToDate(value);
			}
			if (formatter == "Time")
			{
				return XmlCustomFormatter.ToTime(value);
			}
			if (formatter == "XmlName")
			{
				return XmlCustomFormatter.ToXmlName(value);
			}
			if (formatter == "XmlNCName")
			{
				return XmlCustomFormatter.ToXmlNCName(value);
			}
			if (formatter == "XmlNmToken")
			{
				return XmlCustomFormatter.ToXmlNmToken(value);
			}
			if (formatter == "XmlNmTokens")
			{
				return XmlCustomFormatter.ToXmlNmTokens(value);
			}
			throw new Exception(Res.GetString("The formatter {0} cannot be used for default values.", new object[] { formatter }));
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x000967BD File Offset: 0x000949BD
		internal static DateTime ToDateTime(string value)
		{
			if (XmlCustomFormatter.Mode == DateTimeSerializationSection.DateTimeSerializationMode.Local)
			{
				return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allDateTimeFormats);
			}
			return XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.RoundtripKind);
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x000967DA File Offset: 0x000949DA
		internal static DateTime ToDateTime(string value, string[] formats)
		{
			return XmlConvert.ToDateTime(value, formats);
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x000967E3 File Offset: 0x000949E3
		internal static DateTime ToDate(string value)
		{
			return XmlCustomFormatter.ToDateTime(value, XmlCustomFormatter.allDateFormats);
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x000967F0 File Offset: 0x000949F0
		internal static DateTime ToTime(string value)
		{
			if (!LocalAppContextSwitches.IgnoreKindInUtcTimeSerialization)
			{
				return DateTime.ParseExact(value, XmlCustomFormatter.allTimeFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.NoCurrentDateDefault | DateTimeStyles.RoundtripKind);
			}
			return DateTime.ParseExact(value, XmlCustomFormatter.allTimeFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.NoCurrentDateDefault);
		}

		// Token: 0x06001944 RID: 6468 RVA: 0x00096821 File Offset: 0x00094A21
		internal static char ToChar(string value)
		{
			return (char)XmlConvert.ToUInt16(value);
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00096829 File Offset: 0x00094A29
		internal static string ToXmlName(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x00096829 File Offset: 0x00094A29
		internal static string ToXmlNCName(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00096829 File Offset: 0x00094A29
		internal static string ToXmlNmToken(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x06001948 RID: 6472 RVA: 0x00096829 File Offset: 0x00094A29
		internal static string ToXmlNmTokens(string value)
		{
			return XmlConvert.DecodeName(XmlCustomFormatter.CollapseWhitespace(value));
		}

		// Token: 0x06001949 RID: 6473 RVA: 0x00096836 File Offset: 0x00094A36
		internal static byte[] ToByteArrayBase64(string value)
		{
			if (value == null)
			{
				return null;
			}
			value = value.Trim();
			if (value.Length == 0)
			{
				return new byte[0];
			}
			return Convert.FromBase64String(value);
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x0009685A File Offset: 0x00094A5A
		internal static byte[] ToByteArrayHex(string value)
		{
			if (value == null)
			{
				return null;
			}
			value = value.Trim();
			return XmlConvert.FromBinHexString(value);
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00096870 File Offset: 0x00094A70
		internal static long ToEnum(string val, Hashtable vals, string typeName, bool validate)
		{
			long num = 0L;
			string[] array = val.Split(null);
			for (int i = 0; i < array.Length; i++)
			{
				object obj = vals[array[i]];
				if (obj != null)
				{
					num |= (long)obj;
				}
				else if (validate && array[i].Length > 0)
				{
					throw new InvalidOperationException(Res.GetString("Instance validation error: '{0}' is not a valid value for {1}.", new object[]
					{
						array[i],
						typeName
					}));
				}
			}
			return num;
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x000968DD File Offset: 0x00094ADD
		private static string CollapseWhitespace(string value)
		{
			if (value == null)
			{
				return null;
			}
			return value.Trim();
		}

		// Token: 0x04000AA9 RID: 2729
		private static DateTimeSerializationSection.DateTimeSerializationMode mode;

		// Token: 0x04000AAA RID: 2730
		private static string[] allDateTimeFormats = new string[]
		{
			"yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz", "yyyy", "---dd", "---ddZ", "---ddzzzzzz", "--MM-dd", "--MM-ddZ", "--MM-ddzzzzzz", "--MM--", "--MM--Z",
			"--MM--zzzzzz", "yyyy-MM", "yyyy-MMZ", "yyyy-MMzzzzzz", "yyyyzzzzzz", "yyyy-MM-dd", "yyyy-MM-ddZ", "yyyy-MM-ddzzzzzz", "HH:mm:ss", "HH:mm:ss.f",
			"HH:mm:ss.ff", "HH:mm:ss.fff", "HH:mm:ss.ffff", "HH:mm:ss.fffff", "HH:mm:ss.ffffff", "HH:mm:ss.fffffff", "HH:mm:ssZ", "HH:mm:ss.fZ", "HH:mm:ss.ffZ", "HH:mm:ss.fffZ",
			"HH:mm:ss.ffffZ", "HH:mm:ss.fffffZ", "HH:mm:ss.ffffffZ", "HH:mm:ss.fffffffZ", "HH:mm:sszzzzzz", "HH:mm:ss.fzzzzzz", "HH:mm:ss.ffzzzzzz", "HH:mm:ss.fffzzzzzz", "HH:mm:ss.ffffzzzzzz", "HH:mm:ss.fffffzzzzzz",
			"HH:mm:ss.ffffffzzzzzz", "HH:mm:ss.fffffffzzzzzz", "yyyy-MM-ddTHH:mm:ss", "yyyy-MM-ddTHH:mm:ss.f", "yyyy-MM-ddTHH:mm:ss.ff", "yyyy-MM-ddTHH:mm:ss.fff", "yyyy-MM-ddTHH:mm:ss.ffff", "yyyy-MM-ddTHH:mm:ss.fffff", "yyyy-MM-ddTHH:mm:ss.ffffff", "yyyy-MM-ddTHH:mm:ss.fffffff",
			"yyyy-MM-ddTHH:mm:ssZ", "yyyy-MM-ddTHH:mm:ss.fZ", "yyyy-MM-ddTHH:mm:ss.ffZ", "yyyy-MM-ddTHH:mm:ss.fffZ", "yyyy-MM-ddTHH:mm:ss.ffffZ", "yyyy-MM-ddTHH:mm:ss.fffffZ", "yyyy-MM-ddTHH:mm:ss.ffffffZ", "yyyy-MM-ddTHH:mm:ss.fffffffZ", "yyyy-MM-ddTHH:mm:sszzzzzz", "yyyy-MM-ddTHH:mm:ss.fzzzzzz",
			"yyyy-MM-ddTHH:mm:ss.ffzzzzzz", "yyyy-MM-ddTHH:mm:ss.fffzzzzzz", "yyyy-MM-ddTHH:mm:ss.ffffzzzzzz", "yyyy-MM-ddTHH:mm:ss.fffffzzzzzz", "yyyy-MM-ddTHH:mm:ss.ffffffzzzzzz"
		};

		// Token: 0x04000AAB RID: 2731
		private static string[] allDateFormats = new string[]
		{
			"yyyy-MM-ddzzzzzz", "yyyy-MM-dd", "yyyy-MM-ddZ", "yyyy", "---dd", "---ddZ", "---ddzzzzzz", "--MM-dd", "--MM-ddZ", "--MM-ddzzzzzz",
			"--MM--", "--MM--Z", "--MM--zzzzzz", "yyyy-MM", "yyyy-MMZ", "yyyy-MMzzzzzz", "yyyyzzzzzz"
		};

		// Token: 0x04000AAC RID: 2732
		private static string[] allTimeFormats = new string[]
		{
			"HH:mm:ss.fffffffzzzzzz", "HH:mm:ss", "HH:mm:ss.f", "HH:mm:ss.ff", "HH:mm:ss.fff", "HH:mm:ss.ffff", "HH:mm:ss.fffff", "HH:mm:ss.ffffff", "HH:mm:ss.fffffff", "HH:mm:ssZ",
			"HH:mm:ss.fZ", "HH:mm:ss.ffZ", "HH:mm:ss.fffZ", "HH:mm:ss.ffffZ", "HH:mm:ss.fffffZ", "HH:mm:ss.ffffffZ", "HH:mm:ss.fffffffZ", "HH:mm:sszzzzzz", "HH:mm:ss.fzzzzzz", "HH:mm:ss.ffzzzzzz",
			"HH:mm:ss.fffzzzzzz", "HH:mm:ss.ffffzzzzzz", "HH:mm:ss.fffffzzzzzz", "HH:mm:ss.ffffffzzzzzz"
		};
	}
}
