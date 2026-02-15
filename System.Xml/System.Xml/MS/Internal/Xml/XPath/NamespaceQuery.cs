using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000356 RID: 854
	internal sealed class NamespaceQuery : BaseAxisQuery
	{
		// Token: 0x06002636 RID: 9782 RVA: 0x000D3D38 File Offset: 0x000D1F38
		public NamespaceQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type)
			: base(qyParent, Name, Prefix, Type)
		{
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x000D63FD File Offset: 0x000D45FD
		private NamespaceQuery(NamespaceQuery other)
			: base(other)
		{
			this._onNamespace = other._onNamespace;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x000D6412 File Offset: 0x000D4612
		public override void Reset()
		{
			this._onNamespace = false;
			base.Reset();
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x000D6424 File Offset: 0x000D4624
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (!this._onNamespace)
				{
					this.currentNode = this.qyInput.Advance();
					if (this.currentNode == null)
					{
						break;
					}
					this.position = 0;
					this.currentNode = this.currentNode.Clone();
					this._onNamespace = this.currentNode.MoveToFirstNamespace();
				}
				else
				{
					this._onNamespace = this.currentNode.MoveToNextNamespace();
				}
				if (this._onNamespace && this.matches(this.currentNode))
				{
					goto Block_3;
				}
			}
			return null;
			Block_3:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x0600263A RID: 9786 RVA: 0x000D64BA File Offset: 0x000D46BA
		public override bool matches(XPathNavigator e)
		{
			return e.Value.Length != 0 && (!base.NameTest || base.Name.Equals(e.LocalName));
		}

		// Token: 0x0600263B RID: 9787 RVA: 0x000D64E6 File Offset: 0x000D46E6
		public override XPathNodeIterator Clone()
		{
			return new NamespaceQuery(this);
		}

		// Token: 0x04001249 RID: 4681
		private bool _onNamespace;
	}
}
