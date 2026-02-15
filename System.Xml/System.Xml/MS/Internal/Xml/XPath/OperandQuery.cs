using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200035B RID: 859
	internal sealed class OperandQuery : ValueQuery
	{
		// Token: 0x0600265B RID: 9819 RVA: 0x000D6A00 File Offset: 0x000D4C00
		public OperandQuery(object val)
		{
			this.val = val;
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x000D6A0F File Offset: 0x000D4C0F
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			return this.val;
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x0600265D RID: 9821 RVA: 0x000D6A17 File Offset: 0x000D4C17
		public override XPathResultType StaticType
		{
			get
			{
				return base.GetXPathType(this.val);
			}
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x00035F33 File Offset: 0x00034133
		public override XPathNodeIterator Clone()
		{
			return this;
		}

		// Token: 0x04001254 RID: 4692
		internal object val;
	}
}
