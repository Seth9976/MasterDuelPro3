using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000281 RID: 641
	internal class BinaryFacetsChecker : FacetsChecker
	{
		// Token: 0x06001D14 RID: 7444 RVA: 0x000A2BA8 File Offset: 0x000A0DA8
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			byte[] array = (byte[])value;
			return this.CheckValueFacets(array, datatype);
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x000A2BC4 File Offset: 0x000A0DC4
		internal override Exception CheckValueFacets(byte[] value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			int num = value.Length;
			RestrictionFlags restrictionFlags = ((restriction != null) ? restriction.Flags : ((RestrictionFlags)0));
			if (restrictionFlags != (RestrictionFlags)0)
			{
				if ((restrictionFlags & RestrictionFlags.Length) != (RestrictionFlags)0 && restriction.Length != num)
				{
					return new XmlSchemaException("The actual length is not equal to the specified length.", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.MinLength) != (RestrictionFlags)0 && num < restriction.MinLength)
				{
					return new XmlSchemaException("The actual length is less than the MinLength value.", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.MaxLength) != (RestrictionFlags)0 && restriction.MaxLength < num)
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

		// Token: 0x06001D16 RID: 7446 RVA: 0x000A2C70 File Offset: 0x000A0E70
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration((byte[])value, enumeration, datatype);
		}

		// Token: 0x06001D17 RID: 7447 RVA: 0x000A2C80 File Offset: 0x000A0E80
		private bool MatchEnumeration(byte[] value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (datatype.Compare(value, (byte[])enumeration[i]) == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
