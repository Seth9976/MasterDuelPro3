using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001CC RID: 460
	[NullableContext(2)]
	[Nullable(0)]
	internal class XTextWrapper : XObjectWrapper
	{
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x00042C8D File Offset: 0x00040E8D
		[Nullable(1)]
		private XText Text
		{
			[NullableContext(1)]
			get
			{
				return (XText)base.WrappedNode;
			}
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x00042C9A File Offset: 0x00040E9A
		[NullableContext(1)]
		public XTextWrapper(XText text)
			: base(text)
		{
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x00042CA3 File Offset: 0x00040EA3
		// (set) Token: 0x06000F75 RID: 3957 RVA: 0x00042CB0 File Offset: 0x00040EB0
		public override string Value
		{
			get
			{
				return this.Text.Value;
			}
			set
			{
				this.Text.Value = value ?? string.Empty;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x00042CC7 File Offset: 0x00040EC7
		public override IXmlNode ParentNode
		{
			get
			{
				if (this.Text.Parent == null)
				{
					return null;
				}
				return XContainerWrapper.WrapNode(this.Text.Parent);
			}
		}
	}
}
