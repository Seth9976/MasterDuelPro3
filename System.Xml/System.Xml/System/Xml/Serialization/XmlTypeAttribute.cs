using System;

namespace System.Xml.Serialization
{
	/// <summary>Controls the XML schema that is generated when the attribute target is serialized by the <see cref="T:System.Xml.Serialization.XmlSerializer" />.</summary>
	// Token: 0x020001E6 RID: 486
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface)]
	public class XmlTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlTypeAttribute" /> class.</summary>
		// Token: 0x06001920 RID: 6432 RVA: 0x000962F6 File Offset: 0x000944F6
		public XmlTypeAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Serialization.XmlTypeAttribute" /> class and specifies the name of the XML type.</summary>
		/// <param name="typeName">The name of the XML type that the <see cref="T:System.Xml.Serialization.XmlSerializer" /> generates when it serializes the class instance (and recognizes when it deserializes the class instance). </param>
		// Token: 0x06001921 RID: 6433 RVA: 0x00096305 File Offset: 0x00094505
		public XmlTypeAttribute(string typeName)
		{
			this.typeName = typeName;
		}

		/// <summary>Gets or sets a value that determines whether the resulting schema type is an XSD anonymous type.</summary>
		/// <returns>true, if the resulting schema type is an XSD anonymous type; otherwise, false.</returns>
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001922 RID: 6434 RVA: 0x0009631B File Offset: 0x0009451B
		// (set) Token: 0x06001923 RID: 6435 RVA: 0x00096323 File Offset: 0x00094523
		public bool AnonymousType
		{
			get
			{
				return this.anonymousType;
			}
			set
			{
				this.anonymousType = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to include the type in XML schema documents.</summary>
		/// <returns>true to include the type in XML schema documents; otherwise, false.</returns>
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001924 RID: 6436 RVA: 0x0009632C File Offset: 0x0009452C
		// (set) Token: 0x06001925 RID: 6437 RVA: 0x00096334 File Offset: 0x00094534
		public bool IncludeInSchema
		{
			get
			{
				return this.includeInSchema;
			}
			set
			{
				this.includeInSchema = value;
			}
		}

		/// <summary>Gets or sets the name of the XML type.</summary>
		/// <returns>The name of the XML type.</returns>
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001926 RID: 6438 RVA: 0x0009633D File Offset: 0x0009453D
		// (set) Token: 0x06001927 RID: 6439 RVA: 0x00096353 File Offset: 0x00094553
		public string TypeName
		{
			get
			{
				if (this.typeName != null)
				{
					return this.typeName;
				}
				return string.Empty;
			}
			set
			{
				this.typeName = value;
			}
		}

		/// <summary>Gets or sets the namespace of the XML type.</summary>
		/// <returns>The namespace of the XML type.</returns>
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001928 RID: 6440 RVA: 0x0009635C File Offset: 0x0009455C
		// (set) Token: 0x06001929 RID: 6441 RVA: 0x00096364 File Offset: 0x00094564
		public string Namespace
		{
			get
			{
				return this.ns;
			}
			set
			{
				this.ns = value;
			}
		}

		// Token: 0x04000AA5 RID: 2725
		private bool includeInSchema = true;

		// Token: 0x04000AA6 RID: 2726
		private bool anonymousType;

		// Token: 0x04000AA7 RID: 2727
		private string ns;

		// Token: 0x04000AA8 RID: 2728
		private string typeName;
	}
}
