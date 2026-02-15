using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200027C RID: 636
	internal class DurationFacetsChecker : FacetsChecker
	{
		// Token: 0x06001CFC RID: 7420 RVA: 0x000A24E4 File Offset: 0x000A06E4
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			TimeSpan timeSpan = (TimeSpan)datatype.ValueConverter.ChangeType(value, typeof(TimeSpan));
			return this.CheckValueFacets(timeSpan, datatype);
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x000A2518 File Offset: 0x000A0718
		internal override Exception CheckValueFacets(TimeSpan value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = ((restriction != null) ? restriction.Flags : ((RestrictionFlags)0));
			if ((restrictionFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && TimeSpan.Compare(value, (TimeSpan)restriction.MaxInclusive) > 0)
			{
				return new XmlSchemaException("The MaxInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && TimeSpan.Compare(value, (TimeSpan)restriction.MaxExclusive) >= 0)
			{
				return new XmlSchemaException("The MaxExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && TimeSpan.Compare(value, (TimeSpan)restriction.MinInclusive) < 0)
			{
				return new XmlSchemaException("The MinInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && TimeSpan.Compare(value, (TimeSpan)restriction.MinExclusive) <= 0)
			{
				return new XmlSchemaException("The MinExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration))
			{
				return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
			}
			return null;
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x000A2610 File Offset: 0x000A0810
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration((TimeSpan)value, enumeration);
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x000A2620 File Offset: 0x000A0820
		private bool MatchEnumeration(TimeSpan value, ArrayList enumeration)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (TimeSpan.Compare(value, (TimeSpan)enumeration[i]) == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
