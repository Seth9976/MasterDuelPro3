using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000367 RID: 871
	internal sealed class ReversePositionQuery : ForwardPositionQuery
	{
		// Token: 0x06002699 RID: 9881 RVA: 0x000D79F7 File Offset: 0x000D5BF7
		public ReversePositionQuery(Query input)
			: base(input)
		{
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x000D7A00 File Offset: 0x000D5C00
		private ReversePositionQuery(ReversePositionQuery other)
			: base(other)
		{
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x000D7A09 File Offset: 0x000D5C09
		public override XPathNodeIterator Clone()
		{
			return new ReversePositionQuery(this);
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x0600269C RID: 9884 RVA: 0x000D7A11 File Offset: 0x000D5C11
		public override int CurrentPosition
		{
			get
			{
				return this.outputBuffer.Count - this.count + 1;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x0600269D RID: 9885 RVA: 0x000D7A27 File Offset: 0x000D5C27
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}
	}
}
