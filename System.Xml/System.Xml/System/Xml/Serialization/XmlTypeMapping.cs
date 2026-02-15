using System;
using Unity;

namespace System.Xml.Serialization
{
	/// <summary>Contains a mapping of one type to another.</summary>
	// Token: 0x020001E7 RID: 487
	public class XmlTypeMapping : XmlMapping
	{
		// Token: 0x0600192A RID: 6442 RVA: 0x0009636D File Offset: 0x0009456D
		internal XmlTypeMapping(TypeScope scope, ElementAccessor accessor)
			: base(scope, accessor)
		{
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600192B RID: 6443 RVA: 0x00096377 File Offset: 0x00094577
		internal TypeMapping Mapping
		{
			get
			{
				return base.Accessor.Mapping;
			}
		}

		/// <summary>Gets the type name of the mapped object.</summary>
		/// <returns>The type name of the mapped object.</returns>
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x0600192C RID: 6444 RVA: 0x00096384 File Offset: 0x00094584
		public string TypeName
		{
			get
			{
				return this.Mapping.TypeDesc.Name;
			}
		}

		/// <summary>The fully qualified type name that includes the namespace (or namespaces) and type.</summary>
		/// <returns>The fully qualified type name.</returns>
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x00096396 File Offset: 0x00094596
		public string TypeFullName
		{
			get
			{
				return this.Mapping.TypeDesc.FullName;
			}
		}

		/// <summary>Gets the XML element name of the mapped object.</summary>
		/// <returns>The XML element name of the mapped object. The default is the class name of the object.</returns>
		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x000963A8 File Offset: 0x000945A8
		public string XsdTypeName
		{
			get
			{
				return this.Mapping.TypeName;
			}
		}

		/// <summary>Gets the XML namespace of the mapped object.</summary>
		/// <returns>The XML namespace of the mapped object. The default is an empty string ("").</returns>
		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x0600192F RID: 6447 RVA: 0x000963B5 File Offset: 0x000945B5
		public string XsdTypeNamespace
		{
			get
			{
				return this.Mapping.Namespace;
			}
		}

		// Token: 0x06001930 RID: 6448 RVA: 0x0004EAD2 File Offset: 0x0004CCD2
		internal XmlTypeMapping()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}
