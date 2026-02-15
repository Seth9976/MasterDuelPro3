using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;

namespace System.Xml.Schema
{
	// Token: 0x0200027E RID: 638
	internal class StringFacetsChecker : FacetsChecker
	{
		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x000A27F8 File Offset: 0x000A09F8
		private static Regex LanguagePattern
		{
			get
			{
				if (StringFacetsChecker.languagePattern == null)
				{
					Regex regex = new Regex("^([a-zA-Z]{1,8})(-[a-zA-Z0-9]{1,8})*$", RegexOptions.None);
					Interlocked.CompareExchange<Regex>(ref StringFacetsChecker.languagePattern, regex, null);
				}
				return StringFacetsChecker.languagePattern;
			}
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x000A282C File Offset: 0x000A0A2C
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			string text = datatype.ValueConverter.ToString(value);
			return this.CheckValueFacets(text, datatype, true);
		}

		// Token: 0x06001D08 RID: 7432 RVA: 0x000A284F File Offset: 0x000A0A4F
		internal override Exception CheckValueFacets(string value, XmlSchemaDatatype datatype)
		{
			return this.CheckValueFacets(value, datatype, true);
		}

		// Token: 0x06001D09 RID: 7433 RVA: 0x000A285C File Offset: 0x000A0A5C
		internal Exception CheckValueFacets(string value, XmlSchemaDatatype datatype, bool verifyUri)
		{
			int length = value.Length;
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = ((restriction != null) ? restriction.Flags : ((RestrictionFlags)0));
			Exception ex = this.CheckBuiltInFacets(value, datatype.TypeCode, verifyUri);
			if (ex != null)
			{
				return ex;
			}
			if (restrictionFlags != (RestrictionFlags)0)
			{
				if ((restrictionFlags & RestrictionFlags.Length) != (RestrictionFlags)0 && restriction.Length != length)
				{
					return new XmlSchemaException("The actual length is not equal to the specified length.", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.MinLength) != (RestrictionFlags)0 && length < restriction.MinLength)
				{
					return new XmlSchemaException("The actual length is less than the MinLength value.", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.MaxLength) != (RestrictionFlags)0 && restriction.MaxLength < length)
				{
					return new XmlSchemaException("The actual length is greater than the MaxLength value.", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, datatype))
				{
					return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
				}
			}
			return null;
		}

		// Token: 0x06001D0A RID: 7434 RVA: 0x000A291F File Offset: 0x000A0B1F
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration(datatype.ValueConverter.ToString(value), enumeration, datatype);
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x000A2938 File Offset: 0x000A0B38
		private bool MatchEnumeration(string value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			if (datatype.TypeCode == XmlTypeCode.AnyUri)
			{
				for (int i = 0; i < enumeration.Count; i++)
				{
					if (value.Equals(((Uri)enumeration[i]).OriginalString))
					{
						return true;
					}
				}
			}
			else
			{
				for (int j = 0; j < enumeration.Count; j++)
				{
					if (value.Equals((string)enumeration[j]))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001D0C RID: 7436 RVA: 0x000A29A8 File Offset: 0x000A0BA8
		private Exception CheckBuiltInFacets(string s, XmlTypeCode typeCode, bool verifyUri)
		{
			Exception ex = null;
			switch (typeCode)
			{
			case XmlTypeCode.AnyUri:
				if (verifyUri)
				{
					Uri uri;
					ex = XmlConvert.TryToUri(s, out uri);
				}
				break;
			case XmlTypeCode.NormalizedString:
				ex = XmlConvert.TryVerifyNormalizedString(s);
				break;
			case XmlTypeCode.Token:
				ex = XmlConvert.TryVerifyTOKEN(s);
				break;
			case XmlTypeCode.Language:
				if (s == null || s.Length == 0)
				{
					return new XmlSchemaException("The attribute value cannot be empty.", string.Empty);
				}
				if (!StringFacetsChecker.LanguagePattern.IsMatch(s))
				{
					return new XmlSchemaException("'{0}' is an invalid language identifier.", string.Empty);
				}
				break;
			case XmlTypeCode.NmToken:
				ex = XmlConvert.TryVerifyNMTOKEN(s);
				break;
			case XmlTypeCode.Name:
				ex = XmlConvert.TryVerifyName(s);
				break;
			case XmlTypeCode.NCName:
			case XmlTypeCode.Id:
			case XmlTypeCode.Idref:
			case XmlTypeCode.Entity:
				ex = XmlConvert.TryVerifyNCName(s);
				break;
			}
			return ex;
		}

		// Token: 0x04000C72 RID: 3186
		private static Regex languagePattern;
	}
}
