using System;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200033A RID: 826
	internal abstract class CacheOutputQuery : Query
	{
		// Token: 0x06002570 RID: 9584 RVA: 0x000D4668 File Offset: 0x000D2868
		public CacheOutputQuery(Query input)
		{
			this.input = input;
			this.outputBuffer = new List<XPathNavigator>();
			this.count = 0;
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000D4689 File Offset: 0x000D2889
		protected CacheOutputQuery(CacheOutputQuery other)
			: base(other)
		{
			this.input = Query.Clone(other.input);
			this.outputBuffer = new List<XPathNavigator>(other.outputBuffer);
			this.count = other.count;
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x000D439C File Offset: 0x000D259C
		public override void Reset()
		{
			this.count = 0;
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x000D46C0 File Offset: 0x000D28C0
		public override void SetXsltContext(XsltContext context)
		{
			this.input.SetXsltContext(context);
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x000D46CE File Offset: 0x000D28CE
		public override object Evaluate(XPathNodeIterator context)
		{
			this.outputBuffer.Clear();
			this.count = 0;
			return this.input.Evaluate(context);
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x000D46F0 File Offset: 0x000D28F0
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

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06002576 RID: 9590 RVA: 0x000D472E File Offset: 0x000D292E
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

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x00042FC9 File Offset: 0x000411C9
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06002578 RID: 9592 RVA: 0x000D4419 File Offset: 0x000D2619
		public override int CurrentPosition
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06002579 RID: 9593 RVA: 0x000D474D File Offset: 0x000D294D
		public override int Count
		{
			get
			{
				return this.outputBuffer.Count;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x0600257A RID: 9594 RVA: 0x0009EA15 File Offset: 0x0009CC15
		public override QueryProps Properties
		{
			get
			{
				return (QueryProps)23;
			}
		}

		// Token: 0x04001200 RID: 4608
		internal Query input;

		// Token: 0x04001201 RID: 4609
		protected List<XPathNavigator> outputBuffer;
	}
}
