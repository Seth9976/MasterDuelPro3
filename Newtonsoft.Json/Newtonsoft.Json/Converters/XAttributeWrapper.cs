using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001D1 RID: 465
	[NullableContext(2)]
	[Nullable(0)]
	internal class XAttributeWrapper : XObjectWrapper
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00042F42 File Offset: 0x00041142
		[Nullable(1)]
		private XAttribute Attribute
		{
			[NullableContext(1)]
			get
			{
				return (XAttribute)base.WrappedNode;
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00042C9A File Offset: 0x00040E9A
		[NullableContext(1)]
		public XAttributeWrapper(XAttribute attribute)
			: base(attribute)
		{
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x00042F4F File Offset: 0x0004114F
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x00042F5C File Offset: 0x0004115C
		public override string Value
		{
			get
			{
				return this.Attribute.Value;
			}
			set
			{
				this.Attribute.Value = value ?? string.Empty;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x00042F73 File Offset: 0x00041173
		public override string LocalName
		{
			get
			{
				return this.Attribute.Name.LocalName;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x00042F85 File Offset: 0x00041185
		public override string NamespaceUri
		{
			get
			{
				return this.Attribute.Name.NamespaceName;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00042F97 File Offset: 0x00041197
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Attribute.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Attribute.Parent);
			}
		}
	}
}
