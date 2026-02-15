using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200036B RID: 875
	internal abstract class ValueQuery : Query
	{
		// Token: 0x060026C1 RID: 9921 RVA: 0x000D4CD8 File Offset: 0x000D2ED8
		public ValueQuery()
		{
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x000D8374 File Offset: 0x000D6574
		protected ValueQuery(ValueQuery other)
			: base(other)
		{
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x0000A558 File Offset: 0x00008758
		public sealed override void Reset()
		{
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x000D837D File Offset: 0x000D657D
		public sealed override XPathNavigator Current
		{
			get
			{
				throw XPathException.Create("Expression must evaluate to a node-set.");
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x000D837D File Offset: 0x000D657D
		public sealed override int CurrentPosition
		{
			get
			{
				throw XPathException.Create("Expression must evaluate to a node-set.");
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x060026C6 RID: 9926 RVA: 0x000D837D File Offset: 0x000D657D
		public sealed override int Count
		{
			get
			{
				throw XPathException.Create("Expression must evaluate to a node-set.");
			}
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x000D837D File Offset: 0x000D657D
		public sealed override XPathNavigator Advance()
		{
			throw XPathException.Create("Expression must evaluate to a node-set.");
		}
	}
}
