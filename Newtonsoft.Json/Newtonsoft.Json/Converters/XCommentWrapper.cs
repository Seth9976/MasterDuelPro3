using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001CD RID: 461
	[NullableContext(2)]
	[Nullable(0)]
	internal class XCommentWrapper : XObjectWrapper
	{
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x00042CE8 File Offset: 0x00040EE8
		[Nullable(1)]
		private XComment Text
		{
			[NullableContext(1)]
			get
			{
				return (XComment)base.WrappedNode;
			}
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00042C9A File Offset: 0x00040E9A
		[NullableContext(1)]
		public XCommentWrapper(XComment text)
			: base(text)
		{
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x00042CF5 File Offset: 0x00040EF5
		// (set) Token: 0x06000F7A RID: 3962 RVA: 0x00042D02 File Offset: 0x00040F02
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

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x00042D19 File Offset: 0x00040F19
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
