using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200027F RID: 639
	internal class QNameFacetsChecker : FacetsChecker
	{
		// Token: 0x06001D0E RID: 7438 RVA: 0x000A2A64 File Offset: 0x000A0C64
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)datatype.ValueConverter.ChangeType(value, typeof(XmlQualifiedName));
			return this.CheckValueFacets(xmlQualifiedName, datatype);
		}

		// Token: 0x06001D0F RID: 7439 RVA: 0x000A2A98 File Offset: 0x000A0C98
		internal override Exception CheckValueFacets(XmlQualifiedName value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = ((restriction != null) ? restriction.Flags : ((RestrictionFlags)0));
			if (restrictionFlags != (RestrictionFlags)0)
			{
				int length = value.ToString().Length;
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
				if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration))
				{
					return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
				}
			}
			return null;
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x000A2B4B File Offset: 0x000A0D4B
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration((XmlQualifiedName)datatype.ValueConverter.ChangeType(value, typeof(XmlQualifiedName)), enumeration);
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x000A2B70 File Offset: 0x000A0D70
		private bool MatchEnumeration(XmlQualifiedName value, ArrayList enumeration)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (value.Equals((XmlQualifiedName)enumeration[i]))
				{
					return true;
				}
			}
			return false;
		}
	}
}
