using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001CE RID: 462
	[NullableContext(2)]
	[Nullable(0)]
	internal class XProcessingInstructionWrapper : XObjectWrapper
	{
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x00042D3A File Offset: 0x00040F3A
		[Nullable(1)]
		private XProcessingInstruction ProcessingInstruction
		{
			[NullableContext(1)]
			get
			{
				return (XProcessingInstruction)base.WrappedNode;
			}
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00042C9A File Offset: 0x00040E9A
		[NullableContext(1)]
		public XProcessingInstructionWrapper(XProcessingInstruction processingInstruction)
			: base(processingInstruction)
		{
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x00042D47 File Offset: 0x00040F47
		public override string LocalName
		{
			get
			{
				return this.ProcessingInstruction.Target;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x00042D54 File Offset: 0x00040F54
		// (set) Token: 0x06000F80 RID: 3968 RVA: 0x00042D61 File Offset: 0x00040F61
		public override string Value
		{
			get
			{
				return this.ProcessingInstruction.Data;
			}
			set
			{
				this.ProcessingInstruction.Data = value ?? string.Empty;
			}
		}
	}
}
