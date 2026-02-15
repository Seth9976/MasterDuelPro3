using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Abstract class for all facets that are used when simple types are derived by restriction.</summary>
	// Token: 0x020002D2 RID: 722
	public abstract class XmlSchemaFacet : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the value attribute of the facet.</summary>
		/// <returns>The value attribute.</returns>
		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x0600212C RID: 8492 RVA: 0x000C01A7 File Offset: 0x000BE3A7
		// (set) Token: 0x0600212D RID: 8493 RVA: 0x000C01AF File Offset: 0x000BE3AF
		[XmlAttribute("value")]
		public string Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		/// <summary>Gets or sets information that indicates that this facet is fixed.</summary>
		/// <returns>If true, value is fixed; otherwise, false. The default is false.Optional.</returns>
		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x000C01B8 File Offset: 0x000BE3B8
		// (set) Token: 0x0600212F RID: 8495 RVA: 0x000C01C0 File Offset: 0x000BE3C0
		[XmlAttribute("fixed")]
		[DefaultValue(false)]
		public virtual bool IsFixed
		{
			get
			{
				return this.isFixed;
			}
			set
			{
				if (!(this is XmlSchemaEnumerationFacet) && !(this is XmlSchemaPatternFacet))
				{
					this.isFixed = value;
				}
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x000C01D9 File Offset: 0x000BE3D9
		// (set) Token: 0x06002131 RID: 8497 RVA: 0x000C01E1 File Offset: 0x000BE3E1
		internal FacetType FacetType
		{
			get
			{
				return this.facetType;
			}
			set
			{
				this.facetType = value;
			}
		}

		// Token: 0x04000F84 RID: 3972
		private string value;

		// Token: 0x04000F85 RID: 3973
		private bool isFixed;

		// Token: 0x04000F86 RID: 3974
		private FacetType facetType;
	}
}
