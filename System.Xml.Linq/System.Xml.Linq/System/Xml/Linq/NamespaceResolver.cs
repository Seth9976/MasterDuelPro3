using System;

namespace System.Xml.Linq
{
	// Token: 0x02000017 RID: 23
	internal struct NamespaceResolver
	{
		// Token: 0x06000098 RID: 152 RVA: 0x00004834 File Offset: 0x00002A34
		public void PushScope()
		{
			this._scope++;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004844 File Offset: 0x00002A44
		public void PopScope()
		{
			NamespaceResolver.NamespaceDeclaration namespaceDeclaration = this._declaration;
			if (namespaceDeclaration != null)
			{
				do
				{
					namespaceDeclaration = namespaceDeclaration.prev;
					if (namespaceDeclaration.scope != this._scope)
					{
						break;
					}
					if (namespaceDeclaration == this._declaration)
					{
						this._declaration = null;
					}
					else
					{
						this._declaration.prev = namespaceDeclaration.prev;
					}
					this._rover = null;
				}
				while (namespaceDeclaration != this._declaration && this._declaration != null);
			}
			this._scope--;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000048BC File Offset: 0x00002ABC
		public void Add(string prefix, XNamespace ns)
		{
			NamespaceResolver.NamespaceDeclaration namespaceDeclaration = new NamespaceResolver.NamespaceDeclaration();
			namespaceDeclaration.prefix = prefix;
			namespaceDeclaration.ns = ns;
			namespaceDeclaration.scope = this._scope;
			if (this._declaration == null)
			{
				this._declaration = namespaceDeclaration;
			}
			else
			{
				namespaceDeclaration.prev = this._declaration.prev;
			}
			this._declaration.prev = namespaceDeclaration;
			this._rover = null;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004920 File Offset: 0x00002B20
		public void AddFirst(string prefix, XNamespace ns)
		{
			NamespaceResolver.NamespaceDeclaration namespaceDeclaration = new NamespaceResolver.NamespaceDeclaration();
			namespaceDeclaration.prefix = prefix;
			namespaceDeclaration.ns = ns;
			namespaceDeclaration.scope = this._scope;
			if (this._declaration == null)
			{
				namespaceDeclaration.prev = namespaceDeclaration;
			}
			else
			{
				namespaceDeclaration.prev = this._declaration.prev;
				this._declaration.prev = namespaceDeclaration;
			}
			this._declaration = namespaceDeclaration;
			this._rover = null;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000498C File Offset: 0x00002B8C
		public string GetPrefixOfNamespace(XNamespace ns, bool allowDefaultNamespace)
		{
			if (this._rover != null && this._rover.ns == ns && (allowDefaultNamespace || this._rover.prefix.Length > 0))
			{
				return this._rover.prefix;
			}
			NamespaceResolver.NamespaceDeclaration namespaceDeclaration = this._declaration;
			if (namespaceDeclaration != null)
			{
				for (;;)
				{
					namespaceDeclaration = namespaceDeclaration.prev;
					if (namespaceDeclaration.ns == ns)
					{
						NamespaceResolver.NamespaceDeclaration namespaceDeclaration2 = this._declaration.prev;
						while (namespaceDeclaration2 != namespaceDeclaration && namespaceDeclaration2.prefix != namespaceDeclaration.prefix)
						{
							namespaceDeclaration2 = namespaceDeclaration2.prev;
						}
						if (namespaceDeclaration2 == namespaceDeclaration)
						{
							if (allowDefaultNamespace)
							{
								break;
							}
							if (namespaceDeclaration.prefix.Length > 0)
							{
								goto Block_8;
							}
						}
					}
					if (namespaceDeclaration == this._declaration)
					{
						goto IL_00BB;
					}
				}
				this._rover = namespaceDeclaration;
				return namespaceDeclaration.prefix;
				Block_8:
				return namespaceDeclaration.prefix;
			}
			IL_00BB:
			return null;
		}

		// Token: 0x04000030 RID: 48
		private int _scope;

		// Token: 0x04000031 RID: 49
		private NamespaceResolver.NamespaceDeclaration _declaration;

		// Token: 0x04000032 RID: 50
		private NamespaceResolver.NamespaceDeclaration _rover;

		// Token: 0x02000018 RID: 24
		private class NamespaceDeclaration
		{
			// Token: 0x04000033 RID: 51
			public string prefix;

			// Token: 0x04000034 RID: 52
			public XNamespace ns;

			// Token: 0x04000035 RID: 53
			public int scope;

			// Token: 0x04000036 RID: 54
			public NamespaceResolver.NamespaceDeclaration prev;
		}
	}
}
