using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000283 RID: 643
	internal class UnionFacetsChecker : FacetsChecker
	{
		// Token: 0x06001D1C RID: 7452 RVA: 0x000A2DA4 File Offset: 0x000A0FA4
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			if ((((restriction != null && restriction.Flags != (RestrictionFlags)0) ? 1 : 0) & 16) != 0 && !this.MatchEnumeration(value, restriction.Enumeration, datatype))
			{
				return new XmlSchemaException("The Enumeration constraint failed.", string.Empty);
			}
			return null;
		}

		// Token: 0x06001D1D RID: 7453 RVA: 0x000A2DEC File Offset: 0x000A0FEC
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (datatype.Compare(value, enumeration[i]) == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
