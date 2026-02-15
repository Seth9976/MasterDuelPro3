using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000373 RID: 883
	internal sealed class XPathEmptyIterator : ResetableIterator
	{
		// Token: 0x060026F3 RID: 9971 RVA: 0x000D6D96 File Offset: 0x000D4F96
		private XPathEmptyIterator()
		{
		}

		// Token: 0x060026F4 RID: 9972 RVA: 0x00035F33 File Offset: 0x00034133
		public override XPathNodeIterator Clone()
		{
			return this;
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060026F5 RID: 9973 RVA: 0x00014C6C File Offset: 0x00012E6C
		public override XPathNavigator Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060026F6 RID: 9974 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override int CurrentPosition
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060026F7 RID: 9975 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060026F8 RID: 9976 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		public override bool MoveNext()
		{
			return false;
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x0000A558 File Offset: 0x00008758
		public override void Reset()
		{
		}

		// Token: 0x0400129B RID: 4763
		public static XPathEmptyIterator Instance = new XPathEmptyIterator();
	}
}
