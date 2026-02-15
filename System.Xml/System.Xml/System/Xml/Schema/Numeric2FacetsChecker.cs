using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200027B RID: 635
	internal class Numeric2FacetsChecker : FacetsChecker
	{
		// Token: 0x06001CF6 RID: 7414 RVA: 0x000A2364 File Offset: 0x000A0564
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			double num = datatype.ValueConverter.ToDouble(value);
			return this.CheckValueFacets(num, datatype);
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x000A2388 File Offset: 0x000A0588
		internal override Exception CheckValueFacets(double value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = ((restriction != null) ? restriction.Flags : ((RestrictionFlags)0));
			XmlValueConverter valueConverter = datatype.ValueConverter;
			if ((restrictionFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && value > valueConverter.ToDouble(restriction.MaxInclusive))
			{
				return new XmlSchemaException("The MaxInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && value >= valueConverter.ToDouble(restriction.MaxExclusive))
			{
				return new XmlSchemaException("The MaxExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && value < valueConverter.ToDouble(restriction.MinInclusive))
			{
				return new XmlSchemaException("The MinInclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && value <= valueConverter.ToDouble(restriction.MinExclusive))
			{
				return new XmlSchemaException("The MinExclusive constraint failed.", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, valueConverter))
			{
				return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
			}
			return null;
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x000A2474 File Offset: 0x000A0674
		internal override Exception CheckValueFacets(float value, XmlSchemaDatatype datatype)
		{
			double num = (double)value;
			return this.CheckValueFacets(num, datatype);
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x000A248C File Offset: 0x000A068C
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration(datatype.ValueConverter.ToDouble(value), enumeration, datatype.ValueConverter);
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x000A24A8 File Offset: 0x000A06A8
		private bool MatchEnumeration(double value, ArrayList enumeration, XmlValueConverter valueConverter)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (value == valueConverter.ToDouble(enumeration[i]))
				{
					return true;
				}
			}
			return false;
		}
	}
}
