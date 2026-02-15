using System;
using System.Collections;
using System.Diagnostics;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200036F RID: 879
	[DebuggerDisplay("Position={CurrentPosition}, Current={debuggerDisplayProxy, nq}")]
	internal class XPathArrayIterator : ResetableIterator
	{
		// Token: 0x060026D9 RID: 9945 RVA: 0x000D856E File Offset: 0x000D676E
		public XPathArrayIterator(XPathArrayIterator it)
		{
			this.list = it.list;
			this.index = it.index;
		}

		// Token: 0x060026DA RID: 9946 RVA: 0x000D858E File Offset: 0x000D678E
		public XPathArrayIterator(XPathNodeIterator nodeIterator)
		{
			this.list = new ArrayList();
			while (nodeIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = nodeIterator.Current;
				this.list.Add(xpathNavigator.Clone());
			}
		}

		// Token: 0x060026DB RID: 9947 RVA: 0x000D85C2 File Offset: 0x000D67C2
		public override XPathNodeIterator Clone()
		{
			return new XPathArrayIterator(this);
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060026DC RID: 9948 RVA: 0x000D85CA File Offset: 0x000D67CA
		public override XPathNavigator Current
		{
			get
			{
				if (this.index < 1)
				{
					throw new InvalidOperationException(SR.Format("Enumeration has not started. Call MoveNext.", string.Empty));
				}
				return (XPathNavigator)this.list[this.index - 1];
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x000D8602 File Offset: 0x000D6802
		public override int CurrentPosition
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060026DE RID: 9950 RVA: 0x000D860A File Offset: 0x000D680A
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x060026DF RID: 9951 RVA: 0x000D8617 File Offset: 0x000D6817
		public override bool MoveNext()
		{
			if (this.index == this.list.Count)
			{
				return false;
			}
			this.index++;
			return true;
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x000D863D File Offset: 0x000D683D
		public override void Reset()
		{
			this.index = 0;
		}

		// Token: 0x060026E1 RID: 9953 RVA: 0x000D8646 File Offset: 0x000D6846
		public override IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		// Token: 0x04001291 RID: 4753
		protected IList list;

		// Token: 0x04001292 RID: 4754
		protected int index;
	}
}
