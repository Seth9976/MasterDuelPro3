using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Configuration;
using System.Text;

namespace System.Net
{
	// Token: 0x020003A7 RID: 935
	internal sealed class HttpListenerRequestUriBuilder
	{
		// Token: 0x06001767 RID: 5991 RVA: 0x00063F58 File Offset: 0x00062158
		private HttpListenerRequestUriBuilder(string rawUri, string cookedUriScheme, string cookedUriHost, string cookedUriPath, string cookedUriQuery)
		{
			this.rawUri = rawUri;
			this.cookedUriScheme = cookedUriScheme;
			this.cookedUriHost = cookedUriHost;
			this.cookedUriPath = HttpListenerRequestUriBuilder.AddSlashToAsteriskOnlyPath(cookedUriPath);
			if (cookedUriQuery == null)
			{
				this.cookedUriQuery = string.Empty;
				return;
			}
			this.cookedUriQuery = cookedUriQuery;
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00063FA5 File Offset: 0x000621A5
		public static Uri GetRequestUri(string rawUri, string cookedUriScheme, string cookedUriHost, string cookedUriPath, string cookedUriQuery)
		{
			return new HttpListenerRequestUriBuilder(rawUri, cookedUriScheme, cookedUriHost, cookedUriPath, cookedUriQuery).Build();
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x00063FB8 File Offset: 0x000621B8
		private Uri Build()
		{
			if (HttpListenerRequestUriBuilder.useCookedRequestUrl)
			{
				this.BuildRequestUriUsingCookedPath();
				if (this.requestUri == null)
				{
					this.BuildRequestUriUsingRawPath();
				}
			}
			else
			{
				this.BuildRequestUriUsingRawPath();
				if (this.requestUri == null)
				{
					this.BuildRequestUriUsingCookedPath();
				}
			}
			return this.requestUri;
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x00064008 File Offset: 0x00062208
		private void BuildRequestUriUsingCookedPath()
		{
			if (!Uri.TryCreate(string.Concat(new string[]
			{
				this.cookedUriScheme,
				Uri.SchemeDelimiter,
				this.cookedUriHost,
				this.cookedUriPath,
				this.cookedUriQuery
			}), UriKind.Absolute, out this.requestUri))
			{
				this.LogWarning("BuildRequestUriUsingCookedPath", "Can't create Uri from string '{0}://{1}{2}{3}'.", new object[] { this.cookedUriScheme, this.cookedUriHost, this.cookedUriPath, this.cookedUriQuery });
			}
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x00064094 File Offset: 0x00062294
		private void BuildRequestUriUsingRawPath()
		{
			this.rawPath = HttpListenerRequestUriBuilder.GetPath(this.rawUri);
			bool flag;
			if (this.rawPath == string.Empty)
			{
				string text = this.rawPath;
				if (text == string.Empty)
				{
					text = "/";
				}
				flag = Uri.TryCreate(string.Concat(new string[]
				{
					this.cookedUriScheme,
					Uri.SchemeDelimiter,
					this.cookedUriHost,
					text,
					this.cookedUriQuery
				}), UriKind.Absolute, out this.requestUri);
			}
			else
			{
				HttpListenerRequestUriBuilder.ParsingResult parsingResult = this.BuildRequestUriUsingRawPath(HttpListenerRequestUriBuilder.GetEncoding(HttpListenerRequestUriBuilder.EncodingType.Primary));
				if (parsingResult == HttpListenerRequestUriBuilder.ParsingResult.EncodingError)
				{
					Encoding encoding = HttpListenerRequestUriBuilder.GetEncoding(HttpListenerRequestUriBuilder.EncodingType.Secondary);
					parsingResult = this.BuildRequestUriUsingRawPath(encoding);
				}
				flag = parsingResult == HttpListenerRequestUriBuilder.ParsingResult.Success;
			}
			if (!flag)
			{
				this.LogWarning("BuildRequestUriUsingRawPath", "Can't create Uri from string '{0}://{1}{2}{3}'.", new object[] { this.cookedUriScheme, this.cookedUriHost, this.rawPath, this.cookedUriQuery });
			}
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00064186 File Offset: 0x00062386
		private static Encoding GetEncoding(HttpListenerRequestUriBuilder.EncodingType type)
		{
			if (type == HttpListenerRequestUriBuilder.EncodingType.Secondary)
			{
				return HttpListenerRequestUriBuilder.ansiEncoding;
			}
			return HttpListenerRequestUriBuilder.utf8Encoding;
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0006419C File Offset: 0x0006239C
		private HttpListenerRequestUriBuilder.ParsingResult BuildRequestUriUsingRawPath(Encoding encoding)
		{
			this.rawOctets = new List<byte>();
			this.requestUriString = new StringBuilder();
			this.requestUriString.Append(this.cookedUriScheme);
			this.requestUriString.Append(Uri.SchemeDelimiter);
			this.requestUriString.Append(this.cookedUriHost);
			HttpListenerRequestUriBuilder.ParsingResult parsingResult = this.ParseRawPath(encoding);
			if (parsingResult == HttpListenerRequestUriBuilder.ParsingResult.Success)
			{
				this.requestUriString.Append(this.cookedUriQuery);
				if (!Uri.TryCreate(this.requestUriString.ToString(), UriKind.Absolute, out this.requestUri))
				{
					parsingResult = HttpListenerRequestUriBuilder.ParsingResult.InvalidString;
				}
			}
			if (parsingResult != HttpListenerRequestUriBuilder.ParsingResult.Success)
			{
				this.LogWarning("BuildRequestUriUsingRawPath", "Can't convert Uri path '{0}' using encoding '{1}'.", new object[] { this.rawPath, encoding.EncodingName });
			}
			return parsingResult;
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00064258 File Offset: 0x00062458
		private HttpListenerRequestUriBuilder.ParsingResult ParseRawPath(Encoding encoding)
		{
			int i = 0;
			while (i < this.rawPath.Length)
			{
				char c = this.rawPath[i];
				if (c == '%')
				{
					i++;
					c = this.rawPath[i];
					if (c == 'u' || c == 'U')
					{
						if (!this.EmptyDecodeAndAppendRawOctetsList(encoding))
						{
							return HttpListenerRequestUriBuilder.ParsingResult.EncodingError;
						}
						if (!this.AppendUnicodeCodePointValuePercentEncoded(this.rawPath.Substring(i + 1, 4)))
						{
							return HttpListenerRequestUriBuilder.ParsingResult.InvalidString;
						}
						i += 5;
					}
					else
					{
						if (!this.AddPercentEncodedOctetToRawOctetsList(encoding, this.rawPath.Substring(i, 2)))
						{
							return HttpListenerRequestUriBuilder.ParsingResult.InvalidString;
						}
						i += 2;
					}
				}
				else
				{
					if (!this.EmptyDecodeAndAppendRawOctetsList(encoding))
					{
						return HttpListenerRequestUriBuilder.ParsingResult.EncodingError;
					}
					this.requestUriString.Append(c);
					i++;
				}
			}
			if (!this.EmptyDecodeAndAppendRawOctetsList(encoding))
			{
				return HttpListenerRequestUriBuilder.ParsingResult.EncodingError;
			}
			return HttpListenerRequestUriBuilder.ParsingResult.Success;
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0006431C File Offset: 0x0006251C
		private bool AppendUnicodeCodePointValuePercentEncoded(string codePoint)
		{
			int num;
			if (!int.TryParse(codePoint, NumberStyles.HexNumber, null, out num))
			{
				this.LogWarning("AppendUnicodeCodePointValuePercentEncoded", "Can't convert percent encoded value '{0}'.", new object[] { codePoint });
				return false;
			}
			string text = null;
			try
			{
				text = char.ConvertFromUtf32(num);
				HttpListenerRequestUriBuilder.AppendOctetsPercentEncoded(this.requestUriString, HttpListenerRequestUriBuilder.utf8Encoding.GetBytes(text));
				return true;
			}
			catch (ArgumentOutOfRangeException)
			{
				this.LogWarning("AppendUnicodeCodePointValuePercentEncoded", "Can't convert percent encoded value '{0}'.", new object[] { codePoint });
			}
			catch (EncoderFallbackException ex)
			{
				this.LogWarning("AppendUnicodeCodePointValuePercentEncoded", "Can't convert string '{0}' into UTF-8 bytes: {1}", new object[] { text, ex.Message });
			}
			return false;
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x000643DC File Offset: 0x000625DC
		private bool AddPercentEncodedOctetToRawOctetsList(Encoding encoding, string escapedCharacter)
		{
			byte b;
			if (!byte.TryParse(escapedCharacter, NumberStyles.HexNumber, null, out b))
			{
				this.LogWarning("AddPercentEncodedOctetToRawOctetsList", "Can't convert percent encoded value '{0}'.", new object[] { escapedCharacter });
				return false;
			}
			this.rawOctets.Add(b);
			return true;
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00064424 File Offset: 0x00062624
		private bool EmptyDecodeAndAppendRawOctetsList(Encoding encoding)
		{
			if (this.rawOctets.Count == 0)
			{
				return true;
			}
			string text = null;
			try
			{
				text = encoding.GetString(this.rawOctets.ToArray());
				if (encoding == HttpListenerRequestUriBuilder.utf8Encoding)
				{
					HttpListenerRequestUriBuilder.AppendOctetsPercentEncoded(this.requestUriString, this.rawOctets.ToArray());
				}
				else
				{
					HttpListenerRequestUriBuilder.AppendOctetsPercentEncoded(this.requestUriString, HttpListenerRequestUriBuilder.utf8Encoding.GetBytes(text));
				}
				this.rawOctets.Clear();
				return true;
			}
			catch (DecoderFallbackException ex)
			{
				this.LogWarning("EmptyDecodeAndAppendRawOctetsList", "Can't convert bytes '{0}' into UTF-16 characters: {1}", new object[]
				{
					HttpListenerRequestUriBuilder.GetOctetsAsString(this.rawOctets),
					ex.Message
				});
			}
			catch (EncoderFallbackException ex2)
			{
				this.LogWarning("EmptyDecodeAndAppendRawOctetsList", "Can't convert string '{0}' into UTF-8 bytes: {1}", new object[] { text, ex2.Message });
			}
			return false;
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00064510 File Offset: 0x00062710
		private static void AppendOctetsPercentEncoded(StringBuilder target, IEnumerable<byte> octets)
		{
			foreach (byte b in octets)
			{
				target.Append('%');
				target.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x00064574 File Offset: 0x00062774
		private static string GetOctetsAsString(IEnumerable<byte> octets)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (byte b in octets)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x000645F0 File Offset: 0x000627F0
		private static string GetPath(string uriString)
		{
			int num = 0;
			if (uriString[0] != '/')
			{
				int num2 = 0;
				if (uriString.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
				{
					num2 = 7;
				}
				else if (uriString.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
				{
					num2 = 8;
				}
				if (num2 > 0)
				{
					num = uriString.IndexOf('/', num2);
					if (num == -1)
					{
						num = uriString.Length;
					}
				}
				else
				{
					uriString = "/" + uriString;
				}
			}
			int num3 = uriString.IndexOf('?');
			if (num3 == -1)
			{
				num3 = uriString.Length;
			}
			return HttpListenerRequestUriBuilder.AddSlashToAsteriskOnlyPath(uriString.Substring(num, num3 - num));
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00064679 File Offset: 0x00062879
		private static string AddSlashToAsteriskOnlyPath(string path)
		{
			if (path.Length == 1 && path[0] == '*')
			{
				return "/*";
			}
			return path;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00064696 File Offset: 0x00062896
		private void LogWarning(string methodName, string message, params object[] args)
		{
			bool on = Logging.On;
		}

		// Token: 0x04000E8B RID: 3723
		private static readonly bool useCookedRequestUrl = SettingsSectionInternal.Section.HttpListenerUnescapeRequestUrl;

		// Token: 0x04000E8C RID: 3724
		private static readonly Encoding utf8Encoding = new UTF8Encoding(false, true);

		// Token: 0x04000E8D RID: 3725
		private static readonly Encoding ansiEncoding = Encoding.GetEncoding(0, new EncoderExceptionFallback(), new DecoderExceptionFallback());

		// Token: 0x04000E8E RID: 3726
		private readonly string rawUri;

		// Token: 0x04000E8F RID: 3727
		private readonly string cookedUriScheme;

		// Token: 0x04000E90 RID: 3728
		private readonly string cookedUriHost;

		// Token: 0x04000E91 RID: 3729
		private readonly string cookedUriPath;

		// Token: 0x04000E92 RID: 3730
		private readonly string cookedUriQuery;

		// Token: 0x04000E93 RID: 3731
		private StringBuilder requestUriString;

		// Token: 0x04000E94 RID: 3732
		private List<byte> rawOctets;

		// Token: 0x04000E95 RID: 3733
		private string rawPath;

		// Token: 0x04000E96 RID: 3734
		private Uri requestUri;

		// Token: 0x020003A8 RID: 936
		private enum ParsingResult
		{
			// Token: 0x04000E98 RID: 3736
			Success,
			// Token: 0x04000E99 RID: 3737
			InvalidString,
			// Token: 0x04000E9A RID: 3738
			EncodingError
		}

		// Token: 0x020003A9 RID: 937
		private enum EncodingType
		{
			// Token: 0x04000E9C RID: 3740
			Primary,
			// Token: 0x04000E9D RID: 3741
			Secondary
		}
	}
}
