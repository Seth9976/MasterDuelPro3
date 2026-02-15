using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200027D RID: 637
	internal class DateTimeFacetsChecker : FacetsChecker
	{
		// Token: 0x06001D01 RID: 7425 RVA: 0x000A2658 File Offset: 0x000A0858
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			DateTime dateTime = datatype.ValueConverter.ToDateTime(value);
			return this.CheckValueFacets(dateTime, datatype);
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x000A267C File Offset: 0x000A087C
		internal override Exception CheckValueFacets(DateTime value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = ((restriction != null) ? restriction.Flags : ((RestrictionFlags)0));
			if ((restrictionFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && datatype.Compare(value, (DateTime)restriction.MaxInclusive) > 0)
			{
				return new XmlSchemaException("The MaxInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && datatype.Compare(value, (DateTime)restriction.MaxExclusive) >= 0)
			{
				return new XmlSchemaException("The MaxExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && datatype.Compare(value, (DateTime)restriction.MinInclusive) < 0)
			{
				return new XmlSchemaException("The MinInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && datatype.Compare(value, (DateTime)restriction.MinExclusive) <= 0)
			{
				return new XmlSchemaException("The MinExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, datatype))
			{
				return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
			}
			return null;
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x000A27A1 File Offset: 0x000A09A1
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration(datatype.ValueConverter.ToDateTime(value), enumeration, datatype);
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x000A27B8 File Offset: 0x000A09B8
		private bool MatchEnumeration(DateTime value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (datatype.Compare(value, (DateTime)enumeration[i]) == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
