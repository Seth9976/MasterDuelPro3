using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace System.Xml
{
	/// <summary>Encodes and decodes XML names and provides methods for converting between common language runtime types and XML Schema definition language (XSD) types. When converting data types the values returned are locale independent.</summary>
	// Token: 0x02000115 RID: 277
	public class XmlConvert
	{
		/// <summary>Converts the name to a valid XML name.</summary>
		/// <returns>Returns the name with any invalid characters replaced by an escape string.</returns>
		/// <param name="name">A name to be translated. </param>
		// Token: 0x06000E35 RID: 3637 RVA: 0x00048FC8 File Offset: 0x000471C8
		public static string EncodeName(string name)
		{
			return XmlConvert.EncodeName(name, true, false);
		}

		/// <summary>Verifies the name is valid according to the XML specification.</summary>
		/// <returns>The encoded name.</returns>
		/// <param name="name">The name to be encoded. </param>
		// Token: 0x06000E36 RID: 3638 RVA: 0x00048FD2 File Offset: 0x000471D2
		public static string EncodeNmToken(string name)
		{
			return XmlConvert.EncodeName(name, false, false);
		}

		/// <summary>Converts the name to a valid XML local name.</summary>
		/// <returns>The encoded name.</returns>
		/// <param name="name">The name to be encoded. </param>
		// Token: 0x06000E37 RID: 3639 RVA: 0x00048FDC File Offset: 0x000471DC
		public static string EncodeLocalName(string name)
		{
			return XmlConvert.EncodeName(name, true, true);
		}

		/// <summary>Decodes a name. This method does the reverse of the <see cref="M:System.Xml.XmlConvert.EncodeName(System.String)" /> and <see cref="M:System.Xml.XmlConvert.EncodeLocalName(System.String)" /> methods.</summary>
		/// <returns>The decoded name.</returns>
		/// <param name="name">The name to be transformed. </param>
		// Token: 0x06000E38 RID: 3640 RVA: 0x00048FE8 File Offset: 0x000471E8
		public static string DecodeName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return name;
			}
			StringBuilder stringBuilder = null;
			int length = name.Length;
			int num = 0;
			int num2 = name.IndexOf('_');
			if (num2 < 0)
			{
				return name;
			}
			if (XmlConvert.c_DecodeCharPattern == null)
			{
				XmlConvert.c_DecodeCharPattern = new Regex("_[Xx]([0-9a-fA-F]{4}|[0-9a-fA-F]{8})_");
			}
			IEnumerator enumerator = XmlConvert.c_DecodeCharPattern.Matches(name, num2).GetEnumerator();
			int num3 = -1;
			if (enumerator != null && enumerator.MoveNext())
			{
				num3 = ((Match)enumerator.Current).Index;
			}
			for (int i = 0; i < length - XmlConvert.c_EncodedCharLength + 1; i++)
			{
				if (i == num3)
				{
					if (enumerator.MoveNext())
					{
						num3 = ((Match)enumerator.Current).Index;
					}
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(length + 20);
					}
					stringBuilder.Append(name, num, i - num);
					if (name[i + 6] != '_')
					{
						int num4 = XmlConvert.FromHex(name[i + 2]) * 268435456 + XmlConvert.FromHex(name[i + 3]) * 16777216 + XmlConvert.FromHex(name[i + 4]) * 1048576 + XmlConvert.FromHex(name[i + 5]) * 65536 + XmlConvert.FromHex(name[i + 6]) * 4096 + XmlConvert.FromHex(name[i + 7]) * 256 + XmlConvert.FromHex(name[i + 8]) * 16 + XmlConvert.FromHex(name[i + 9]);
						if (num4 >= 65536)
						{
							if (num4 <= 1114111)
							{
								num = i + XmlConvert.c_EncodedCharLength + 4;
								char c;
								char c2;
								XmlCharType.SplitSurrogateChar(num4, out c, out c2);
								stringBuilder.Append(c2);
								stringBuilder.Append(c);
							}
						}
						else
						{
							num = i + XmlConvert.c_EncodedCharLength + 4;
							stringBuilder.Append((char)num4);
						}
						i += XmlConvert.c_EncodedCharLength - 1 + 4;
					}
					else
					{
						num = i + XmlConvert.c_EncodedCharLength;
						stringBuilder.Append((char)(XmlConvert.FromHex(name[i + 2]) * 4096 + XmlConvert.FromHex(name[i + 3]) * 256 + XmlConvert.FromHex(name[i + 4]) * 16 + XmlConvert.FromHex(name[i + 5])));
						i += XmlConvert.c_EncodedCharLength - 1;
					}
				}
			}
			if (num == 0)
			{
				return name;
			}
			if (num < length)
			{
				stringBuilder.Append(name, num, length - num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00049274 File Offset: 0x00047474
		private static string EncodeName(string name, bool first, bool local)
		{
			if (string.IsNullOrEmpty(name))
			{
				return name;
			}
			StringBuilder stringBuilder = null;
			int length = name.Length;
			int num = 0;
			int i = 0;
			int num2 = name.IndexOf('_');
			IEnumerator enumerator = null;
			if (num2 >= 0)
			{
				if (XmlConvert.c_EncodeCharPattern == null)
				{
					XmlConvert.c_EncodeCharPattern = new Regex("(?<=_)[Xx]([0-9a-fA-F]{4}|[0-9a-fA-F]{8})_");
				}
				enumerator = XmlConvert.c_EncodeCharPattern.Matches(name, num2).GetEnumerator();
			}
			int num3 = -1;
			if (enumerator != null && enumerator.MoveNext())
			{
				num3 = ((Match)enumerator.Current).Index - 1;
			}
			if (first && ((!XmlConvert.xmlCharType.IsStartNCNameCharXml4e(name[0]) && (local || (!local && name[0] != ':'))) || num3 == 0))
			{
				if (stringBuilder == null)
				{
					stringBuilder = new StringBuilder(length + 20);
				}
				stringBuilder.Append("_x");
				if (length > 1 && XmlCharType.IsHighSurrogate((int)name[0]) && XmlCharType.IsLowSurrogate((int)name[1]))
				{
					int num4 = (int)name[0];
					stringBuilder.Append(XmlCharType.CombineSurrogateChar((int)name[1], num4).ToString("X8", CultureInfo.InvariantCulture));
					i++;
					num = 2;
				}
				else
				{
					stringBuilder.Append(((int)name[0]).ToString("X4", CultureInfo.InvariantCulture));
					num = 1;
				}
				stringBuilder.Append("_");
				i++;
				if (num3 == 0 && enumerator.MoveNext())
				{
					num3 = ((Match)enumerator.Current).Index - 1;
				}
			}
			while (i < length)
			{
				if ((local && !XmlConvert.xmlCharType.IsNCNameCharXml4e(name[i])) || (!local && !XmlConvert.xmlCharType.IsNameCharXml4e(name[i])) || num3 == i)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder(length + 20);
					}
					if (num3 == i && enumerator.MoveNext())
					{
						num3 = ((Match)enumerator.Current).Index - 1;
					}
					stringBuilder.Append(name, num, i - num);
					stringBuilder.Append("_x");
					if (length > i + 1 && XmlCharType.IsHighSurrogate((int)name[i]) && XmlCharType.IsLowSurrogate((int)name[i + 1]))
					{
						int num5 = (int)name[i];
						stringBuilder.Append(XmlCharType.CombineSurrogateChar((int)name[i + 1], num5).ToString("X8", CultureInfo.InvariantCulture));
						num = i + 2;
						i++;
					}
					else
					{
						stringBuilder.Append(((int)name[i]).ToString("X4", CultureInfo.InvariantCulture));
						num = i + 1;
					}
					stringBuilder.Append("_");
				}
				i++;
			}
			if (num == 0)
			{
				return name;
			}
			if (num < length)
			{
				stringBuilder.Append(name, num, length - num);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00049536 File Offset: 0x00047736
		private static int FromHex(char digit)
		{
			if (digit > '9')
			{
				return (int)(((digit <= 'F') ? (digit - 'A') : (digit - 'a')) + '\n');
			}
			return (int)(digit - '0');
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00049554 File Offset: 0x00047754
		internal static byte[] FromBinHexString(string s)
		{
			return XmlConvert.FromBinHexString(s, true);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0004955D File Offset: 0x0004775D
		internal static byte[] FromBinHexString(string s, bool allowOddCount)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return BinHexDecoder.Decode(s.ToCharArray(), allowOddCount);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00049579 File Offset: 0x00047779
		internal static string ToBinHexString(byte[] inArray)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return BinHexEncoder.Encode(inArray, 0, inArray.Length);
		}

		/// <summary>Verifies that the name is a valid name according to the W3C Extended Markup Language recommendation.</summary>
		/// <returns>The name, if it is a valid XML name.</returns>
		/// <param name="name">The name to verify. </param>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="name" /> is not a valid XML name. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null or String.Empty. </exception>
		// Token: 0x06000E3E RID: 3646 RVA: 0x00049594 File Offset: 0x00047794
		public static string VerifyName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentNullException("name", Res.GetString("The empty string '' is not a valid name."));
			}
			int num = ValidateNames.ParseNameNoNamespaces(name, 0);
			if (num != name.Length)
			{
				throw XmlConvert.CreateInvalidNameCharException(name, num, ExceptionType.XmlException);
			}
			return name;
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x000495E8 File Offset: 0x000477E8
		internal static Exception TryVerifyName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return new XmlException("The empty string '' is not a valid name.", string.Empty);
			}
			int num = ValidateNames.ParseNameNoNamespaces(name, 0);
			if (num != name.Length)
			{
				return new XmlException((num == 0) ? "Name cannot begin with the '{0}' character, hexadecimal value {1}." : "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, num));
			}
			return null;
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00049640 File Offset: 0x00047840
		internal static string VerifyQName(string name, ExceptionType exceptionType)
		{
			if (name == null || name.Length == 0)
			{
				throw new ArgumentNullException("name");
			}
			int num = -1;
			int num2 = ValidateNames.ParseQName(name, 0, out num);
			if (num2 != name.Length)
			{
				throw XmlConvert.CreateException("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, num2), exceptionType, 0, num2 + 1);
			}
			return name;
		}

		/// <summary>Verifies that the name is a valid NCName according to the W3C Extended Markup Language recommendation. An NCName is a name that cannot contain a colon.</summary>
		/// <returns>The name, if it is a valid NCName.</returns>
		/// <param name="name">The name to verify. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null or String.Empty. </exception>
		/// <exception cref="T:System.Xml.XmlException">
		///   <paramref name="name" /> is not a valid non-colon name. </exception>
		// Token: 0x06000E41 RID: 3649 RVA: 0x00049690 File Offset: 0x00047890
		public static string VerifyNCName(string name)
		{
			return XmlConvert.VerifyNCName(name, ExceptionType.XmlException);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0004969C File Offset: 0x0004789C
		internal static string VerifyNCName(string name, ExceptionType exceptionType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentNullException("name", Res.GetString("The empty string '' is not a valid local name."));
			}
			int num = ValidateNames.ParseNCName(name, 0);
			if (num != name.Length)
			{
				throw XmlConvert.CreateInvalidNameCharException(name, num, exceptionType);
			}
			return name;
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x000496F0 File Offset: 0x000478F0
		internal static Exception TryVerifyNCName(string name)
		{
			int num = ValidateNames.ParseNCName(name);
			if (num == 0 || num != name.Length)
			{
				return ValidateNames.GetInvalidNameException(name, 0, num);
			}
			return null;
		}

		/// <summary>Verifies that the string is a valid token according to the W3C XML Schema Part2: Datatypes recommendation.</summary>
		/// <returns>The token, if it is a valid token.</returns>
		/// <param name="token">The string value you wish to verify.</param>
		/// <exception cref="T:System.Xml.XmlException">The string value is not a valid token.</exception>
		// Token: 0x06000E44 RID: 3652 RVA: 0x0004971C File Offset: 0x0004791C
		public static string VerifyTOKEN(string token)
		{
			if (token == null || token.Length == 0)
			{
				return token;
			}
			if (token[0] == ' ' || token[token.Length - 1] == ' ' || token.IndexOfAny(XmlConvert.crt) != -1 || token.IndexOf("  ", StringComparison.Ordinal) != -1)
			{
				throw new XmlException("line-feed (#xA) or tab (#x9) characters, leading or trailing spaces and sequences of one or more spaces (#x20) are not allowed in 'xs:token'.", token);
			}
			return token;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00049780 File Offset: 0x00047980
		internal static Exception TryVerifyTOKEN(string token)
		{
			if (token == null || token.Length == 0)
			{
				return null;
			}
			if (token[0] == ' ' || token[token.Length - 1] == ' ' || token.IndexOfAny(XmlConvert.crt) != -1 || token.IndexOf("  ", StringComparison.Ordinal) != -1)
			{
				return new XmlException("line-feed (#xA) or tab (#x9) characters, leading or trailing spaces and sequences of one or more spaces (#x20) are not allowed in 'xs:token'.", token);
			}
			return null;
		}

		/// <summary>Verifies that the string is a valid NMTOKEN according to the W3C XML Schema Part2: Datatypes recommendation</summary>
		/// <returns>The name token, if it is a valid NMTOKEN.</returns>
		/// <param name="name">The string you wish to verify.</param>
		/// <exception cref="T:System.Xml.XmlException">The string is not a valid name token.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		// Token: 0x06000E46 RID: 3654 RVA: 0x000497E1 File Offset: 0x000479E1
		public static string VerifyNMTOKEN(string name)
		{
			return XmlConvert.VerifyNMTOKEN(name, ExceptionType.XmlException);
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x000497EC File Offset: 0x000479EC
		internal static string VerifyNMTOKEN(string name, ExceptionType exceptionType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw XmlConvert.CreateException("Invalid NmToken value '{0}'.", name, exceptionType);
			}
			int num = ValidateNames.ParseNmtokenNoNamespaces(name, 0);
			if (num != name.Length)
			{
				throw XmlConvert.CreateException("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, num), exceptionType, 0, num + 1);
			}
			return name;
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00049848 File Offset: 0x00047A48
		internal static Exception TryVerifyNMTOKEN(string name)
		{
			if (name == null || name.Length == 0)
			{
				return new XmlException("The empty string '' is not a valid name.", string.Empty);
			}
			int num = ValidateNames.ParseNmtokenNoNamespaces(name, 0);
			if (num != name.Length)
			{
				return new XmlException("The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, num));
			}
			return null;
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00049894 File Offset: 0x00047A94
		internal static Exception TryVerifyNormalizedString(string str)
		{
			if (str.IndexOfAny(XmlConvert.crt) != -1)
			{
				return new XmlSchemaException("Carriage return (#xD), line feed (#xA), and tab (#x9) characters are not allowed in xs:normalizedString.", str);
			}
			return null;
		}

		/// <summary>Returns the passed-in string if all the characters and surrogate pair characters in the string argument are valid XML characters, otherwise null. </summary>
		/// <returns>Returns the passed-in string if all the characters and surrogate-pair characters in the string argument are valid XML characters, otherwise null.</returns>
		/// <param name="content">
		///   <see cref="T:System.String" /> that contains characters to verify.</param>
		// Token: 0x06000E4A RID: 3658 RVA: 0x000498B1 File Offset: 0x00047AB1
		public static string VerifyXmlChars(string content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			XmlConvert.VerifyCharData(content, ExceptionType.XmlException);
			return content;
		}

		/// <summary>Returns the passed in string instance if all the characters in the string argument are valid public id characters.</summary>
		/// <returns>Returns the passed-in string if all the characters in the argument are valid public id characters.</returns>
		/// <param name="publicId">
		///   <see cref="T:System.String" /> that contains the id to validate.</param>
		// Token: 0x06000E4B RID: 3659 RVA: 0x000498CC File Offset: 0x00047ACC
		public static string VerifyPublicId(string publicId)
		{
			if (publicId == null)
			{
				throw new ArgumentNullException("publicId");
			}
			int num = XmlConvert.xmlCharType.IsPublicId(publicId);
			if (num != -1)
			{
				throw XmlConvert.CreateInvalidCharException(publicId, num, ExceptionType.XmlException);
			}
			return publicId;
		}

		/// <summary>Returns the passed-in string instance if all the characters in the string argument are valid whitespace characters. </summary>
		/// <returns>Returns the passed-in string instance if all the characters in the string argument are valid whitespace characters, otherwise null.</returns>
		/// <param name="content">
		///   <see cref="T:System.String" /> to verify.</param>
		// Token: 0x06000E4C RID: 3660 RVA: 0x00049904 File Offset: 0x00047B04
		public static string VerifyWhitespace(string content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			int num = XmlConvert.xmlCharType.IsOnlyWhitespaceWithPos(content);
			if (num != -1)
			{
				throw new XmlException("The Whitespace or SignificantWhitespace node can contain only XML white space characters. '{0}' is not an XML white space character.", XmlException.BuildCharExceptionArgs(content, num), 0, num + 1);
			}
			return content;
		}

		/// <summary>Checks if the passed-in character is a valid Start Name Character type.</summary>
		/// <returns>true if the character is a valid Start Name Character type; otherwise, false. </returns>
		/// <param name="ch">The character to validate.</param>
		// Token: 0x06000E4D RID: 3661 RVA: 0x00049946 File Offset: 0x00047B46
		public static bool IsStartNCNameChar(char ch)
		{
			return (XmlConvert.xmlCharType.charProperties[(int)ch] & 4) > 0;
		}

		/// <summary>Checks whether the passed-in character is a valid non-colon character type.</summary>
		/// <returns>Returns true if the character is a valid non-colon character type; otherwise, false.</returns>
		/// <param name="ch">The character to verify as a non-colon character.</param>
		// Token: 0x06000E4E RID: 3662 RVA: 0x00049959 File Offset: 0x00047B59
		public static bool IsNCNameChar(char ch)
		{
			return (XmlConvert.xmlCharType.charProperties[(int)ch] & 8) > 0;
		}

		/// <summary>Checks if the passed-in character is a valid XML character.</summary>
		/// <returns>true if the passed in character is a valid XML character; otherwise false.</returns>
		/// <param name="ch">The character to validate.</param>
		// Token: 0x06000E4F RID: 3663 RVA: 0x0004996C File Offset: 0x00047B6C
		public static bool IsXmlChar(char ch)
		{
			return (XmlConvert.xmlCharType.charProperties[(int)ch] & 16) > 0;
		}

		/// <summary>Checks if the passed-in surrogate pair of characters is a valid XML character.</summary>
		/// <returns>true if the passed in surrogate pair of characters is a valid XML character; otherwise false.</returns>
		/// <param name="lowChar">The surrogate character to validate.</param>
		/// <param name="highChar">The surrogate character to validate.</param>
		// Token: 0x06000E50 RID: 3664 RVA: 0x00049980 File Offset: 0x00047B80
		public static bool IsXmlSurrogatePair(char lowChar, char highChar)
		{
			return XmlCharType.IsHighSurrogate((int)highChar) && XmlCharType.IsLowSurrogate((int)lowChar);
		}

		/// <summary>Returns the passed-in character instance if the character in the argument is a valid public id character, otherwise null.</summary>
		/// <returns>Returns the passed-in character if the character is a valid public id character, otherwise null.</returns>
		/// <param name="ch">
		///   <see cref="T:System.Char" /> object to validate.</param>
		// Token: 0x06000E51 RID: 3665 RVA: 0x00049992 File Offset: 0x00047B92
		public static bool IsPublicIdChar(char ch)
		{
			return XmlConvert.xmlCharType.IsPubidChar(ch);
		}

		/// <summary>Checks if the passed-in character is a valid XML whitespace character.</summary>
		/// <returns>true if the passed in character is a valid XML whitespace character; otherwise false.</returns>
		/// <param name="ch">The character to validate.</param>
		// Token: 0x06000E52 RID: 3666 RVA: 0x0004999F File Offset: 0x00047B9F
		public static bool IsWhitespaceChar(char ch)
		{
			return (XmlConvert.xmlCharType.charProperties[(int)ch] & 1) > 0;
		}

		/// <summary>Converts the <see cref="T:System.Boolean" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Boolean, that is, "true" or "false".</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06000E53 RID: 3667 RVA: 0x000499B2 File Offset: 0x00047BB2
		public static string ToString(bool value)
		{
			if (!value)
			{
				return "false";
			}
			return "true";
		}

		/// <summary>Converts the <see cref="T:System.Char" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Char.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06000E54 RID: 3668 RVA: 0x000499C2 File Offset: 0x00047BC2
		public static string ToString(char value)
		{
			return value.ToString(null);
		}

		/// <summary>Converts the <see cref="T:System.Decimal" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Decimal.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06000E55 RID: 3669 RVA: 0x000499CC File Offset: 0x00047BCC
		public static string ToString(decimal value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.SByte" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the SByte.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06000E56 RID: 3670 RVA: 0x000499DB File Offset: 0x00047BDB
		[CLSCompliant(false)]
		public static string ToString(sbyte value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Int16" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Int16.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06000E57 RID: 3671 RVA: 0x000499EA File Offset: 0x00047BEA
		public static string ToString(short value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Int32" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Int32.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06000E58 RID: 3672 RVA: 0x000499F9 File Offset: 0x00047BF9
		public static string ToString(int value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Int64" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Int64.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06000E59 RID: 3673 RVA: 0x00049A08 File Offset: 0x00047C08
		public static string ToString(long value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Byte" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Byte.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06000E5A RID: 3674 RVA: 0x00049A17 File Offset: 0x00047C17
		public static string ToString(byte value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.UInt16" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the UInt16.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06000E5B RID: 3675 RVA: 0x00049A26 File Offset: 0x00047C26
		[CLSCompliant(false)]
		public static string ToString(ushort value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.UInt32" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the UInt32.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06000E5C RID: 3676 RVA: 0x00049A35 File Offset: 0x00047C35
		[CLSCompliant(false)]
		public static string ToString(uint value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.UInt64" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the UInt64.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06000E5D RID: 3677 RVA: 0x00049A44 File Offset: 0x00047C44
		[CLSCompliant(false)]
		public static string ToString(ulong value)
		{
			return value.ToString(null, NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Single" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Single.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06000E5E RID: 3678 RVA: 0x00049A53 File Offset: 0x00047C53
		public static string ToString(float value)
		{
			if (float.IsNegativeInfinity(value))
			{
				return "-INF";
			}
			if (float.IsPositiveInfinity(value))
			{
				return "INF";
			}
			if (XmlConvert.IsNegativeZero((double)value))
			{
				return "-0";
			}
			return value.ToString("R", NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Double" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Double.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06000E5F RID: 3679 RVA: 0x00049A91 File Offset: 0x00047C91
		public static string ToString(double value)
		{
			if (double.IsNegativeInfinity(value))
			{
				return "-INF";
			}
			if (double.IsPositiveInfinity(value))
			{
				return "INF";
			}
			if (XmlConvert.IsNegativeZero(value))
			{
				return "-0";
			}
			return value.ToString("R", NumberFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.TimeSpan" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the TimeSpan.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06000E60 RID: 3680 RVA: 0x00049AD0 File Offset: 0x00047CD0
		public static string ToString(TimeSpan value)
		{
			return new XsdDuration(value).ToString();
		}

		/// <summary>Converts the <see cref="T:System.DateTime" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the DateTime in the format yyyy-MM-ddTHH:mm:ss where 'T' is a constant literal.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06000E61 RID: 3681 RVA: 0x00049AF1 File Offset: 0x00047CF1
		[Obsolete("Use XmlConvert.ToString() that takes in XmlDateTimeSerializationMode")]
		public static string ToString(DateTime value)
		{
			return XmlConvert.ToString(value, "yyyy-MM-ddTHH:mm:ss.fffffffzzzzzz");
		}

		/// <summary>Converts the <see cref="T:System.DateTime" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the DateTime in the specified format.</returns>
		/// <param name="value">The value to convert. </param>
		/// <param name="format">The format structure that defines how to display the converted string. Valid formats include "yyyy-MM-ddTHH:mm:sszzzzzz" and its subsets. </param>
		// Token: 0x06000E62 RID: 3682 RVA: 0x00049AFE File Offset: 0x00047CFE
		public static string ToString(DateTime value, string format)
		{
			return value.ToString(format, DateTimeFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.DateTime" /> to a <see cref="T:System.String" /> using the <see cref="T:System.Xml.XmlDateTimeSerializationMode" /> specified.</summary>
		/// <returns>A <see cref="T:System.String" /> equivalent of the <see cref="T:System.DateTime" />.</returns>
		/// <param name="value">The <see cref="T:System.DateTime" /> value to convert.</param>
		/// <param name="dateTimeOption">One of the <see cref="T:System.Xml.XmlDateTimeSerializationMode" /> values that specify how to treat the <see cref="T:System.DateTime" /> value.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="dateTimeOption" /> value is not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> or <paramref name="dateTimeOption" /> value is null.</exception>
		// Token: 0x06000E63 RID: 3683 RVA: 0x00049B10 File Offset: 0x00047D10
		public static string ToString(DateTime value, XmlDateTimeSerializationMode dateTimeOption)
		{
			switch (dateTimeOption)
			{
			case XmlDateTimeSerializationMode.Local:
				value = XmlConvert.SwitchToLocalTime(value);
				break;
			case XmlDateTimeSerializationMode.Utc:
				value = XmlConvert.SwitchToUtcTime(value);
				break;
			case XmlDateTimeSerializationMode.Unspecified:
				value = new DateTime(value.Ticks, DateTimeKind.Unspecified);
				break;
			case XmlDateTimeSerializationMode.RoundtripKind:
				break;
			default:
				throw new ArgumentException(Res.GetString("The '{0}' value for the 'dateTimeOption' parameter is not an allowed value for the 'XmlDateTimeSerializationMode' enumeration.", new object[] { dateTimeOption, "dateTimeOption" }));
			}
			XsdDateTime xsdDateTime = new XsdDateTime(value, XsdDateTimeFlags.DateTime);
			return xsdDateTime.ToString();
		}

		/// <summary>Converts the supplied <see cref="T:System.DateTimeOffset" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> representation of the supplied <see cref="T:System.DateTimeOffset" />.</returns>
		/// <param name="value">The <see cref="T:System.DateTimeOffset" /> to be converted.</param>
		// Token: 0x06000E64 RID: 3684 RVA: 0x00049B98 File Offset: 0x00047D98
		public static string ToString(DateTimeOffset value)
		{
			XsdDateTime xsdDateTime = new XsdDateTime(value);
			return xsdDateTime.ToString();
		}

		/// <summary>Converts the supplied <see cref="T:System.DateTimeOffset" /> to a <see cref="T:System.String" /> in the specified format.</summary>
		/// <returns>A <see cref="T:System.String" /> representation in the specified format of the supplied <see cref="T:System.DateTimeOffset" />.</returns>
		/// <param name="value">The <see cref="T:System.DateTimeOffset" /> to be converted.</param>
		/// <param name="format">The format to which <paramref name="s" /> is converted. The format parameter can be any subset of the W3C Recommendation for the XML dateTime type. (For more information see http://www.w3.org/TR/xmlschema-2/#dateTime.)</param>
		// Token: 0x06000E65 RID: 3685 RVA: 0x00049BBA File Offset: 0x00047DBA
		public static string ToString(DateTimeOffset value, string format)
		{
			return value.ToString(format, DateTimeFormatInfo.InvariantInfo);
		}

		/// <summary>Converts the <see cref="T:System.Guid" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A string representation of the Guid.</returns>
		/// <param name="value">The value to convert. </param>
		// Token: 0x06000E66 RID: 3686 RVA: 0x00049BC9 File Offset: 0x00047DC9
		public static string ToString(Guid value)
		{
			return value.ToString();
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Boolean" /> equivalent.</summary>
		/// <returns>A Boolean value, that is, true or false.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> does not represent a Boolean value. </exception>
		// Token: 0x06000E67 RID: 3687 RVA: 0x00049BD8 File Offset: 0x00047DD8
		public static bool ToBoolean(string s)
		{
			s = XmlConvert.TrimString(s);
			if (s == "1" || s == "true")
			{
				return true;
			}
			if (s == "0" || s == "false")
			{
				return false;
			}
			throw new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Boolean" }));
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00049C48 File Offset: 0x00047E48
		internal static Exception TryToBoolean(string s, out bool result)
		{
			s = XmlConvert.TrimString(s);
			if (s == "0" || s == "false")
			{
				result = false;
				return null;
			}
			if (s == "1" || s == "true")
			{
				result = true;
				return null;
			}
			result = false;
			return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Boolean" }));
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Char" /> equivalent.</summary>
		/// <returns>A Char representing the single character.</returns>
		/// <param name="s">The string containing a single character to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="s" /> parameter is null. </exception>
		/// <exception cref="T:System.FormatException">The <paramref name="s" /> parameter contains more than one character. </exception>
		// Token: 0x06000E69 RID: 3689 RVA: 0x00049CBF File Offset: 0x00047EBF
		public static char ToChar(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length != 1)
			{
				throw new FormatException(Res.GetString("String must be exactly one character long."));
			}
			return s[0];
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00049CEF File Offset: 0x00047EEF
		internal static Exception TryToChar(string s, out char result)
		{
			if (!char.TryParse(s, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Char" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Decimal" /> equivalent.</summary>
		/// <returns>A Decimal equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. </exception>
		// Token: 0x06000E6B RID: 3691 RVA: 0x00049D1D File Offset: 0x00047F1D
		public static decimal ToDecimal(string s)
		{
			return decimal.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x00049D2C File Offset: 0x00047F2C
		internal static Exception TryToDecimal(string s, out decimal result)
		{
			if (!decimal.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Decimal" }));
			}
			return null;
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x00049D61 File Offset: 0x00047F61
		internal static decimal ToInteger(string s)
		{
			return decimal.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x00049D6F File Offset: 0x00047F6F
		internal static Exception TryToInteger(string s, out decimal result)
		{
			if (!decimal.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Integer" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.SByte" /> equivalent.</summary>
		/// <returns>An SByte equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />. </exception>
		// Token: 0x06000E6F RID: 3695 RVA: 0x00049DA3 File Offset: 0x00047FA3
		[CLSCompliant(false)]
		public static sbyte ToSByte(string s)
		{
			return sbyte.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x00049DB1 File Offset: 0x00047FB1
		internal static Exception TryToSByte(string s, out sbyte result)
		{
			if (!sbyte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "SByte" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Int16" /> equivalent.</summary>
		/// <returns>An Int16 equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />. </exception>
		// Token: 0x06000E71 RID: 3697 RVA: 0x00049DE5 File Offset: 0x00047FE5
		public static short ToInt16(string s)
		{
			return short.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00049DF3 File Offset: 0x00047FF3
		internal static Exception TryToInt16(string s, out short result)
		{
			if (!short.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Int16" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Int32" /> equivalent.</summary>
		/// <returns>An Int32 equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		// Token: 0x06000E73 RID: 3699 RVA: 0x00049E27 File Offset: 0x00048027
		public static int ToInt32(string s)
		{
			return int.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x00049E35 File Offset: 0x00048035
		internal static Exception TryToInt32(string s, out int result)
		{
			if (!int.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Int32" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Int64" /> equivalent.</summary>
		/// <returns>An Int64 equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />. </exception>
		// Token: 0x06000E75 RID: 3701 RVA: 0x00049E69 File Offset: 0x00048069
		public static long ToInt64(string s)
		{
			return long.Parse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x00049E77 File Offset: 0x00048077
		internal static Exception TryToInt64(string s, out long result)
		{
			if (!long.TryParse(s, NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Int64" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Byte" /> equivalent.</summary>
		/// <returns>A Byte equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />. </exception>
		// Token: 0x06000E77 RID: 3703 RVA: 0x00049EAB File Offset: 0x000480AB
		public static byte ToByte(string s)
		{
			return byte.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x00049EB9 File Offset: 0x000480B9
		internal static Exception TryToByte(string s, out byte result)
		{
			if (!byte.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Byte" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.UInt16" /> equivalent.</summary>
		/// <returns>A UInt16 equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />. </exception>
		// Token: 0x06000E79 RID: 3705 RVA: 0x00049EED File Offset: 0x000480ED
		[CLSCompliant(false)]
		public static ushort ToUInt16(string s)
		{
			return ushort.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x00049EFB File Offset: 0x000480FB
		internal static Exception TryToUInt16(string s, out ushort result)
		{
			if (!ushort.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "UInt16" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.UInt32" /> equivalent.</summary>
		/// <returns>A UInt32 equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />. </exception>
		// Token: 0x06000E7B RID: 3707 RVA: 0x00049F2F File Offset: 0x0004812F
		[CLSCompliant(false)]
		public static uint ToUInt32(string s)
		{
			return uint.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00049F3D File Offset: 0x0004813D
		internal static Exception TryToUInt32(string s, out uint result)
		{
			if (!uint.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "UInt32" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.UInt64" /> equivalent.</summary>
		/// <returns>A UInt64 equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />. </exception>
		// Token: 0x06000E7D RID: 3709 RVA: 0x00049F71 File Offset: 0x00048171
		[CLSCompliant(false)]
		public static ulong ToUInt64(string s)
		{
			return ulong.Parse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00049F7F File Offset: 0x0004817F
		internal static Exception TryToUInt64(string s, out ulong result)
		{
			if (!ulong.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "UInt64" }));
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Single" /> equivalent.</summary>
		/// <returns>A Single equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />. </exception>
		// Token: 0x06000E7F RID: 3711 RVA: 0x00049FB4 File Offset: 0x000481B4
		public static float ToSingle(string s)
		{
			s = XmlConvert.TrimString(s);
			if (s == "-INF")
			{
				return float.NegativeInfinity;
			}
			if (s == "INF")
			{
				return float.PositiveInfinity;
			}
			float num = float.Parse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo);
			if (num == 0f && s[0] == '-')
			{
				return -0f;
			}
			return num;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0004A01C File Offset: 0x0004821C
		internal static Exception TryToSingle(string s, out float result)
		{
			s = XmlConvert.TrimString(s);
			if (s == "-INF")
			{
				result = float.NegativeInfinity;
				return null;
			}
			if (s == "INF")
			{
				result = float.PositiveInfinity;
				return null;
			}
			if (!float.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Single" }));
			}
			if (result == 0f && s[0] == '-')
			{
				result = -0f;
			}
			return null;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Double" /> equivalent.</summary>
		/// <returns>A Double equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format. </exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />. </exception>
		// Token: 0x06000E81 RID: 3713 RVA: 0x0004A0B0 File Offset: 0x000482B0
		public static double ToDouble(string s)
		{
			s = XmlConvert.TrimString(s);
			if (s == "-INF")
			{
				return double.NegativeInfinity;
			}
			if (s == "INF")
			{
				return double.PositiveInfinity;
			}
			double num = double.Parse(s, NumberStyles.Float, NumberFormatInfo.InvariantInfo);
			if (num == 0.0 && s[0] == '-')
			{
				return -0.0;
			}
			return num;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0004A128 File Offset: 0x00048328
		internal static Exception TryToDouble(string s, out double result)
		{
			s = XmlConvert.TrimString(s);
			if (s == "-INF")
			{
				result = double.NegativeInfinity;
				return null;
			}
			if (s == "INF")
			{
				result = double.PositiveInfinity;
				return null;
			}
			if (!double.TryParse(s, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, NumberFormatInfo.InvariantInfo, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Double" }));
			}
			if (result == 0.0 && s[0] == '-')
			{
				result = -0.0;
			}
			return null;
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0004A1CC File Offset: 0x000483CC
		internal static double ToXPathDouble(object o)
		{
			string text = o as string;
			if (text != null)
			{
				text = XmlConvert.TrimString(text);
				double num;
				if (text.Length != 0 && text[0] != '+' && double.TryParse(text, NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo, out num))
				{
					return num;
				}
				return double.NaN;
			}
			else
			{
				if (o is double)
				{
					return (double)o;
				}
				if (!(o is bool))
				{
					try
					{
						return Convert.ToDouble(o, NumberFormatInfo.InvariantInfo);
					}
					catch (FormatException)
					{
					}
					catch (OverflowException)
					{
					}
					catch (ArgumentNullException)
					{
					}
					return double.NaN;
				}
				if (!(bool)o)
				{
					return 0.0;
				}
				return 1.0;
			}
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0004A298 File Offset: 0x00048498
		internal static double XPathRound(double value)
		{
			double num = Math.Round(value);
			if (value - num != 0.5)
			{
				return num;
			}
			return num + 1.0;
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.TimeSpan" /> equivalent.</summary>
		/// <returns>A TimeSpan equivalent of the string.</returns>
		/// <param name="s">The string to convert. The string format must conform to the W3C XML Schema Part 2: Datatypes recommendation for duration.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in correct format to represent a TimeSpan value. </exception>
		// Token: 0x06000E85 RID: 3717 RVA: 0x0004A2C8 File Offset: 0x000484C8
		public static TimeSpan ToTimeSpan(string s)
		{
			XsdDuration xsdDuration;
			try
			{
				xsdDuration = new XsdDuration(s);
			}
			catch (Exception)
			{
				throw new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "TimeSpan" }));
			}
			return xsdDuration.ToTimeSpan();
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x0004A318 File Offset: 0x00048518
		internal static Exception TryToTimeSpan(string s, out TimeSpan result)
		{
			XsdDuration xsdDuration;
			Exception ex = XsdDuration.TryParse(s, out xsdDuration);
			if (ex != null)
			{
				result = TimeSpan.MinValue;
				return ex;
			}
			return xsdDuration.TryToTimeSpan(out result);
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0004A346 File Offset: 0x00048546
		private static string[] AllDateTimeFormats
		{
			get
			{
				if (XmlConvert.s_allDateTimeFormats == null)
				{
					XmlConvert.CreateAllDateTimeFormats();
				}
				return XmlConvert.s_allDateTimeFormats;
			}
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x0004A360 File Offset: 0x00048560
		private static void CreateAllDateTimeFormats()
		{
			if (XmlConvert.s_allDateTimeFormats == null)
			{
				XmlConvert.s_allDateTimeFormats = new string[]
				{
					"yyyy-MM-ddTHH:mm:ss.FFFFFFFzzzzzz", "yyyy-MM-ddTHH:mm:ss.FFFFFFF", "yyyy-MM-ddTHH:mm:ss.FFFFFFFZ", "HH:mm:ss.FFFFFFF", "HH:mm:ss.FFFFFFFZ", "HH:mm:ss.FFFFFFFzzzzzz", "yyyy-MM-dd", "yyyy-MM-ddZ", "yyyy-MM-ddzzzzzz", "yyyy-MM",
					"yyyy-MMZ", "yyyy-MMzzzzzz", "yyyy", "yyyyZ", "yyyyzzzzzz", "--MM-dd", "--MM-ddZ", "--MM-ddzzzzzz", "---dd", "---ddZ",
					"---ddzzzzzz", "--MM--", "--MM--Z", "--MM--zzzzzz"
				};
			}
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.DateTime" /> equivalent.</summary>
		/// <returns>A DateTime equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is an empty string or is not in the correct format. </exception>
		// Token: 0x06000E89 RID: 3721 RVA: 0x0004A456 File Offset: 0x00048656
		[Obsolete("Use XmlConvert.ToDateTime() that takes in XmlDateTimeSerializationMode")]
		public static DateTime ToDateTime(string s)
		{
			return XmlConvert.ToDateTime(s, XmlConvert.AllDateTimeFormats);
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.DateTime" /> equivalent.</summary>
		/// <returns>A DateTime equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <param name="format">The format structure to apply to the converted DateTime. Valid formats include "yyyy-MM-ddTHH:mm:sszzzzzz" and its subsets. The string is validated against this format. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> or <paramref name="format" /> is String.Empty -or- <paramref name="s" /> does not contain a date and time that corresponds to <paramref name="format" />. </exception>
		// Token: 0x06000E8A RID: 3722 RVA: 0x0004A463 File Offset: 0x00048663
		public static DateTime ToDateTime(string s, string format)
		{
			return DateTime.ParseExact(s, format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite);
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.DateTime" /> equivalent.</summary>
		/// <returns>A DateTime equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		/// <param name="formats">An array containing the format structures to apply to the converted DateTime. Valid formats include "yyyy-MM-ddTHH:mm:sszzzzzz" and its subsets. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> or an element of <paramref name="formats" /> is String.Empty -or- <paramref name="s" /> does not contain a date and time that corresponds to any of the elements of <paramref name="formats" />. </exception>
		// Token: 0x06000E8B RID: 3723 RVA: 0x0004A472 File Offset: 0x00048672
		public static DateTime ToDateTime(string s, string[] formats)
		{
			return DateTime.ParseExact(s, formats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite);
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.DateTime" /> using the <see cref="T:System.Xml.XmlDateTimeSerializationMode" /> specified</summary>
		/// <returns>A <see cref="T:System.DateTime" /> equivalent of the <see cref="T:System.String" />.</returns>
		/// <param name="s">The <see cref="T:System.String" /> value to convert.</param>
		/// <param name="dateTimeOption">One of the <see cref="T:System.Xml.XmlDateTimeSerializationMode" /> values that specify whether the date should be converted to local time or preserved as Coordinated Universal Time (UTC), if it is a UTC date.</param>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="s" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="dateTimeOption" /> value is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is an empty string or is not in a valid format.</exception>
		// Token: 0x06000E8C RID: 3724 RVA: 0x0004A484 File Offset: 0x00048684
		public static DateTime ToDateTime(string s, XmlDateTimeSerializationMode dateTimeOption)
		{
			DateTime dateTime = new XsdDateTime(s, XsdDateTimeFlags.AllXsd);
			switch (dateTimeOption)
			{
			case XmlDateTimeSerializationMode.Local:
				dateTime = XmlConvert.SwitchToLocalTime(dateTime);
				break;
			case XmlDateTimeSerializationMode.Utc:
				dateTime = XmlConvert.SwitchToUtcTime(dateTime);
				break;
			case XmlDateTimeSerializationMode.Unspecified:
				dateTime = new DateTime(dateTime.Ticks, DateTimeKind.Unspecified);
				break;
			case XmlDateTimeSerializationMode.RoundtripKind:
				break;
			default:
				throw new ArgumentException(Res.GetString("The '{0}' value for the 'dateTimeOption' parameter is not an allowed value for the 'XmlDateTimeSerializationMode' enumeration.", new object[] { dateTimeOption, "dateTimeOption" }));
			}
			return dateTime;
		}

		/// <summary>Converts the supplied <see cref="T:System.String" /> to a <see cref="T:System.DateTimeOffset" /> equivalent.</summary>
		/// <returns>The <see cref="T:System.DateTimeOffset" /> equivalent of the supplied string.</returns>
		/// <param name="s">The string to convert.Note   The string must conform to a subset of the W3C Recommendation for the XML dateTime type. For more information see http://www.w3.org/TR/xmlschema-2/#dateTime.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The argument passed to this method is outside the range of allowable values. For information about allowable values, see <see cref="T:System.DateTimeOffset" />.</exception>
		/// <exception cref="T:System.FormatException">The argument passed to this method does not conform to a subset of the W3C Recommendations for the XML dateTime type. For more information see http://www.w3.org/TR/xmlschema-2/#dateTime.</exception>
		// Token: 0x06000E8D RID: 3725 RVA: 0x0004A505 File Offset: 0x00048705
		public static DateTimeOffset ToDateTimeOffset(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return new XsdDateTime(s, XsdDateTimeFlags.AllXsd);
		}

		/// <summary>Converts the supplied <see cref="T:System.String" /> to a <see cref="T:System.DateTimeOffset" /> equivalent.</summary>
		/// <returns>The <see cref="T:System.DateTimeOffset" /> equivalent of the supplied string.</returns>
		/// <param name="s">The string to convert.</param>
		/// <param name="format">The format from which <paramref name="s" /> is converted. The format parameter can be any subset of the W3C Recommendation for the XML dateTime type. (For more information see http://www.w3.org/TR/xmlschema-2/#dateTime.) The string <paramref name="s" /> is validated against this format.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> or <paramref name="format" /> is an empty string or is not in the specified format.</exception>
		// Token: 0x06000E8E RID: 3726 RVA: 0x0004A525 File Offset: 0x00048725
		public static DateTimeOffset ToDateTimeOffset(string s, string format)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return DateTimeOffset.ParseExact(s, format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite);
		}

		/// <summary>Converts the supplied <see cref="T:System.String" /> to a <see cref="T:System.DateTimeOffset" /> equivalent.</summary>
		/// <returns>The <see cref="T:System.DateTimeOffset" /> equivalent of the supplied string.</returns>
		/// <param name="s">The string to convert.</param>
		/// <param name="formats">An array of formats from which <paramref name="s" /> can be converted. Each format in <paramref name="formats" /> can be any subset of the W3C Recommendation for the XML dateTime type. (For more information see http://www.w3.org/TR/xmlschema-2/#dateTime.) The string <paramref name="s" /> is validated against one of these formats.</param>
		// Token: 0x06000E8F RID: 3727 RVA: 0x0004A542 File Offset: 0x00048742
		public static DateTimeOffset ToDateTimeOffset(string s, string[] formats)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return DateTimeOffset.ParseExact(s, formats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite);
		}

		/// <summary>Converts the <see cref="T:System.String" /> to a <see cref="T:System.Guid" /> equivalent.</summary>
		/// <returns>A Guid equivalent of the string.</returns>
		/// <param name="s">The string to convert. </param>
		// Token: 0x06000E90 RID: 3728 RVA: 0x0004A55F File Offset: 0x0004875F
		public static Guid ToGuid(string s)
		{
			return new Guid(s);
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x0004A568 File Offset: 0x00048768
		internal static Exception TryToGuid(string s, out Guid result)
		{
			Exception ex = null;
			result = Guid.Empty;
			try
			{
				result = new Guid(s);
			}
			catch (ArgumentException)
			{
				ex = new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Guid" }));
			}
			catch (FormatException)
			{
				ex = new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Guid" }));
			}
			return ex;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x0004A5F8 File Offset: 0x000487F8
		private static DateTime SwitchToLocalTime(DateTime value)
		{
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				return new DateTime(value.Ticks, DateTimeKind.Local);
			case DateTimeKind.Utc:
				return value.ToLocalTime();
			case DateTimeKind.Local:
				return value;
			default:
				return value;
			}
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x0004A63C File Offset: 0x0004883C
		private static DateTime SwitchToUtcTime(DateTime value)
		{
			switch (value.Kind)
			{
			case DateTimeKind.Unspecified:
				return new DateTime(value.Ticks, DateTimeKind.Utc);
			case DateTimeKind.Utc:
				return value;
			case DateTimeKind.Local:
				return value.ToUniversalTime();
			default:
				return value;
			}
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x0004A680 File Offset: 0x00048880
		internal static Uri ToUri(string s)
		{
			if (s != null && s.Length > 0)
			{
				s = XmlConvert.TrimString(s);
				if (s.Length == 0 || s.IndexOf("##", StringComparison.Ordinal) != -1)
				{
					throw new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Uri" }));
				}
			}
			Uri uri;
			if (!Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out uri))
			{
				throw new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Uri" }));
			}
			return uri;
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x0004A708 File Offset: 0x00048908
		internal static Exception TryToUri(string s, out Uri result)
		{
			result = null;
			if (s != null && s.Length > 0)
			{
				s = XmlConvert.TrimString(s);
				if (s.Length == 0 || s.IndexOf("##", StringComparison.Ordinal) != -1)
				{
					return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Uri" }));
				}
			}
			if (!Uri.TryCreate(s, UriKind.RelativeOrAbsolute, out result))
			{
				return new FormatException(Res.GetString("The string '{0}' is not a valid {1} value.", new object[] { s, "Uri" }));
			}
			return null;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x0004A794 File Offset: 0x00048994
		internal static bool StrEqual(char[] chars, int strPos1, int strLen1, string str2)
		{
			if (strLen1 != str2.Length)
			{
				return false;
			}
			int num = 0;
			while (num < strLen1 && chars[strPos1 + num] == str2[num])
			{
				num++;
			}
			return num == strLen1;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x0004A7CA File Offset: 0x000489CA
		internal static string TrimString(string value)
		{
			return value.Trim(XmlConvert.WhitespaceChars);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x0004A7D7 File Offset: 0x000489D7
		internal static string TrimStringStart(string value)
		{
			return value.TrimStart(XmlConvert.WhitespaceChars);
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x0004A7E4 File Offset: 0x000489E4
		internal static string TrimStringEnd(string value)
		{
			return value.TrimEnd(XmlConvert.WhitespaceChars);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x0004A7F1 File Offset: 0x000489F1
		internal static string[] SplitString(string value)
		{
			return value.Split(XmlConvert.WhitespaceChars, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x0004A7FF File Offset: 0x000489FF
		internal static bool IsNegativeZero(double value)
		{
			return value == 0.0 && XmlConvert.DoubleToInt64Bits(value) == XmlConvert.DoubleToInt64Bits(-0.0);
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x0004A826 File Offset: 0x00048A26
		private unsafe static long DoubleToInt64Bits(double value)
		{
			return *(long*)(&value);
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x0004A82C File Offset: 0x00048A2C
		internal static void VerifyCharData(string data, ExceptionType exceptionType)
		{
			XmlConvert.VerifyCharData(data, exceptionType, exceptionType);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0004A838 File Offset: 0x00048A38
		internal static void VerifyCharData(string data, ExceptionType invCharExceptionType, ExceptionType invSurrogateExceptionType)
		{
			if (data == null || data.Length == 0)
			{
				return;
			}
			int num = 0;
			int length = data.Length;
			for (;;)
			{
				if (num >= length || (XmlConvert.xmlCharType.charProperties[(int)data[num]] & 16) == 0)
				{
					if (num == length)
					{
						break;
					}
					if (!XmlCharType.IsHighSurrogate((int)data[num]))
					{
						goto IL_0090;
					}
					if (num + 1 == length)
					{
						goto Block_5;
					}
					if (!XmlCharType.IsLowSurrogate((int)data[num + 1]))
					{
						goto IL_0075;
					}
					num += 2;
				}
				else
				{
					num++;
				}
			}
			return;
			Block_5:
			throw XmlConvert.CreateException("The surrogate pair is invalid. Missing a low surrogate character.", invSurrogateExceptionType, 0, num + 1);
			IL_0075:
			throw XmlConvert.CreateInvalidSurrogatePairException(data[num + 1], data[num], invSurrogateExceptionType, 0, num + 1);
			IL_0090:
			throw XmlConvert.CreateInvalidCharException(data, num, invCharExceptionType);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x0004A8DD File Offset: 0x00048ADD
		internal static Exception CreateException(string res, ExceptionType exceptionType, int lineNo, int linePos)
		{
			if (exceptionType != ExceptionType.ArgumentException)
			{
				if (exceptionType != ExceptionType.XmlException)
				{
				}
				return new XmlException(res, string.Empty, lineNo, linePos);
			}
			return new ArgumentException(Res.GetString(res));
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x0004A901 File Offset: 0x00048B01
		internal static Exception CreateException(string res, string arg, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException(res, arg, exceptionType, 0, 0);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x0004A90D File Offset: 0x00048B0D
		internal static Exception CreateException(string res, string arg, ExceptionType exceptionType, int lineNo, int linePos)
		{
			if (exceptionType != ExceptionType.ArgumentException)
			{
				if (exceptionType != ExceptionType.XmlException)
				{
				}
				return new XmlException(res, arg, lineNo, linePos);
			}
			return new ArgumentException(Res.GetString(res, new object[] { arg }));
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x0004A938 File Offset: 0x00048B38
		internal static Exception CreateException(string res, string[] args, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException(res, args, exceptionType, 0, 0);
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x0004A944 File Offset: 0x00048B44
		internal static Exception CreateException(string res, string[] args, ExceptionType exceptionType, int lineNo, int linePos)
		{
			if (exceptionType != ExceptionType.ArgumentException)
			{
				if (exceptionType != ExceptionType.XmlException)
				{
				}
				return new XmlException(res, args, lineNo, linePos);
			}
			return new ArgumentException(Res.GetString(res, args));
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0004A973 File Offset: 0x00048B73
		internal static Exception CreateInvalidSurrogatePairException(char low, char hi)
		{
			return XmlConvert.CreateInvalidSurrogatePairException(low, hi, ExceptionType.ArgumentException);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0004A97D File Offset: 0x00048B7D
		internal static Exception CreateInvalidSurrogatePairException(char low, char hi, ExceptionType exceptionType)
		{
			return XmlConvert.CreateInvalidSurrogatePairException(low, hi, exceptionType, 0, 0);
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0004A98C File Offset: 0x00048B8C
		internal static Exception CreateInvalidSurrogatePairException(char low, char hi, ExceptionType exceptionType, int lineNo, int linePos)
		{
			string[] array = new string[2];
			int num = 0;
			uint num2 = (uint)hi;
			array[num] = num2.ToString("X", CultureInfo.InvariantCulture);
			int num3 = 1;
			num2 = (uint)low;
			array[num3] = num2.ToString("X", CultureInfo.InvariantCulture);
			string[] array2 = array;
			return XmlConvert.CreateException("The surrogate pair (0x{0}, 0x{1}) is invalid. A high surrogate character (0xD800 - 0xDBFF) must always be paired with a low surrogate character (0xDC00 - 0xDFFF).", array2, exceptionType, lineNo, linePos);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0004A9DB File Offset: 0x00048BDB
		internal static Exception CreateInvalidHighSurrogateCharException(char hi)
		{
			return XmlConvert.CreateInvalidHighSurrogateCharException(hi, ExceptionType.ArgumentException);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x0004A9E4 File Offset: 0x00048BE4
		internal static Exception CreateInvalidHighSurrogateCharException(char hi, ExceptionType exceptionType)
		{
			return XmlConvert.CreateInvalidHighSurrogateCharException(hi, exceptionType, 0, 0);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0004A9F0 File Offset: 0x00048BF0
		internal static Exception CreateInvalidHighSurrogateCharException(char hi, ExceptionType exceptionType, int lineNo, int linePos)
		{
			string text = "Invalid high surrogate character (0x{0}). A high surrogate character must have a value from range (0xD800 - 0xDBFF).";
			uint num = (uint)hi;
			return XmlConvert.CreateException(text, num.ToString("X", CultureInfo.InvariantCulture), exceptionType, lineNo, linePos);
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x0004AA1D File Offset: 0x00048C1D
		internal static Exception CreateInvalidCharException(string data, int invCharPos, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(data, invCharPos), exceptionType, 0, invCharPos + 1);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0004AA35 File Offset: 0x00048C35
		internal static Exception CreateInvalidCharException(char invChar, char nextChar)
		{
			return XmlConvert.CreateInvalidCharException(invChar, nextChar, ExceptionType.ArgumentException);
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x0004AA3F File Offset: 0x00048C3F
		internal static Exception CreateInvalidCharException(char invChar, char nextChar, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException("'{0}', hexadecimal value {1}, is an invalid character.", XmlException.BuildCharExceptionArgs(invChar, nextChar), exceptionType);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x0004AA53 File Offset: 0x00048C53
		internal static Exception CreateInvalidNameCharException(string name, int index, ExceptionType exceptionType)
		{
			return XmlConvert.CreateException((index == 0) ? "Name cannot begin with the '{0}' character, hexadecimal value {1}." : "The '{0}' character, hexadecimal value {1}, cannot be included in a name.", XmlException.BuildCharExceptionArgs(name, index), exceptionType, 0, index + 1);
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x0004AA75 File Offset: 0x00048C75
		internal static ArgumentException CreateInvalidNameArgumentException(string name, string argumentName)
		{
			if (name != null)
			{
				return new ArgumentException(Res.GetString("The empty string '' is not a valid name."), argumentName);
			}
			return new ArgumentNullException(argumentName);
		}

		// Token: 0x0400072D RID: 1837
		private static XmlCharType xmlCharType = XmlCharType.Instance;

		// Token: 0x0400072E RID: 1838
		internal static char[] crt = new char[] { '\n', '\r', '\t' };

		// Token: 0x0400072F RID: 1839
		private static readonly int c_EncodedCharLength = 7;

		// Token: 0x04000730 RID: 1840
		private static volatile Regex c_EncodeCharPattern;

		// Token: 0x04000731 RID: 1841
		private static volatile Regex c_DecodeCharPattern;

		// Token: 0x04000732 RID: 1842
		private static volatile string[] s_allDateTimeFormats;

		// Token: 0x04000733 RID: 1843
		internal static readonly char[] WhitespaceChars = new char[] { ' ', '\t', '\n', '\r' };
	}
}
