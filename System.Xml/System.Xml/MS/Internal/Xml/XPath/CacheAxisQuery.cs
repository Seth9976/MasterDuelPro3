using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000338 RID: 824
	internal abstract class CacheAxisQuery : BaseAxisQuery
	{
		// Token: 0x06002560 RID: 9568 RVA: 0x000D4357 File Offset: 0x000D2557
		public CacheAxisQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest)
			: base(qyInput, name, prefix, typeTest)
		{
			this.outputBuffer = new List<XPathNavigator>();
			this.count = 0;
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000D4376 File Offset: 0x000D2576
		protected CacheAxisQuery(CacheAxisQuery other)
			: base(other)
		{
			this.outputBuffer = new List<XPathNavigator>(other.outputBuffer);
			this.count = other.count;
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x000D439C File Offset: 0x000D259C
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x000D43A5 File Offset: 0x000D25A5
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			this.outputBuffer.Clear();
			return this;
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x000D43BC File Offset: 0x000D25BC
		public override XPathNavigator Advance()
		{
			if (this.count < this.outputBuffer.Count)
			{
				List<XPathNavigator> list = this.outputBuffer;
				int count = this.count;
				this.count = count + 1;
				return list[count];
			}
			return null;
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x000D43FA File Offset: 0x000D25FA
		public override XPathNavigator Current
		{
			get
			{
				if (this.count == 0)
				{
					return null;
				}
				return this.outputBuffer[this.count - 1];
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x000D4419 File Offset: 0x000D2619
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06002567 RID: 9575 RVA: 0x000D4421 File Offset: 0x000D2621
		public override int Count
		{
			get
			{
				return this.outputBuffer.Count;
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06002568 RID: 9576 RVA: 0x0009EA15 File Offset: 0x0009CC15
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x040011FB RID: 4603
		protected List<XPathNavigator> outputBuffer;
	}
}
