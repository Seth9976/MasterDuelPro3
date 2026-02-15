using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000345 RID: 837
	internal abstract class ExtensionQuery : Query
	{
		// Token: 0x060025B7 RID: 9655 RVA: 0x000D4CE0 File Offset: 0x000D2EE0
		public ExtensionQuery(string prefix, string name)
		{
			this.prefix = prefix;
			this.name = name;
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000D4CF8 File Offset: 0x000D2EF8
		protected ExtensionQuery(ExtensionQuery other)
			: base(other)
		{
			this.prefix = other.prefix;
			this.name = other.name;
			this.xsltContext = other.xsltContext;
			this._queryIterator = (ResetableIterator)Query.Clone(other._queryIterator);
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x000D4D46 File Offset: 0x000D2F46
		public override void Reset()
		{
			if (this._queryIterator != null)
			{
				this._queryIterator.Reset();
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x060025BA RID: 9658 RVA: 0x000D4D5B File Offset: 0x000D2F5B
		public override XPathNavigator Current
		{
			get
			{
				if (this._queryIterator == null)
				{
					throw XPathException.Create("Expression must evaluate to a node-set.");
				}
				if (this._queryIterator.CurrentPosition == 0)
				{
					this.Advance();
				}
				return this._queryIterator.Current;
			}
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x000D4D8F File Offset: 0x000D2F8F
		public override XPathNavigator Advance()
		{
			if (this._queryIterator == null)
			{
				throw XPathException.Create("Expression must evaluate to a node-set.");
			}
			if (this._queryIterator.MoveNext())
			{
				return this._queryIterator.Current;
			}
			return null;
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x000D4DBE File Offset: 0x000D2FBE
		public override int CurrentPosition
		{
			get
			{
				if (this._queryIterator != null)
				{
					return this._queryIterator.CurrentPosition;
				}
				return 0;
			}
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x000D4DD8 File Offset: 0x000D2FD8
		protected object ProcessResult(object value)
		{
			if (value is string)
			{
				return value;
			}
			if (value is double)
			{
				return value;
			}
			if (value is bool)
			{
				return value;
			}
			if (value is XPathNavigator)
			{
				return value;
			}
			if (value is int)
			{
				return (double)((int)value);
			}
			if (value == null)
			{
				this._queryIterator = XPathEmptyIterator.Instance;
				return this;
			}
			ResetableIterator resetableIterator = value as ResetableIterator;
			if (resetableIterator != null)
			{
				this._queryIterator = (ResetableIterator)resetableIterator.Clone();
				return this;
			}
			XPathNodeIterator xpathNodeIterator = value as XPathNodeIterator;
			if (xpathNodeIterator != null)
			{
				this._queryIterator = new XPathArrayIterator(xpathNodeIterator);
				return this;
			}
			IXPathNavigable ixpathNavigable = value as IXPathNavigable;
			if (ixpathNavigable != null)
			{
				return ixpathNavigable.CreateNavigator();
			}
			if (value is short)
			{
				return (double)((short)value);
			}
			if (value is long)
			{
				return (double)((long)value);
			}
			if (value is uint)
			{
				return (uint)value;
			}
			if (value is ushort)
			{
				return (double)((ushort)value);
			}
			if (value is ulong)
			{
				return (ulong)value;
			}
			if (value is float)
			{
				return (double)((float)value);
			}
			if (value is decimal)
			{
				return (double)((decimal)value);
			}
			return value.ToString();
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060025BE RID: 9662 RVA: 0x000D4F18 File Offset: 0x000D3118
		protected string QName
		{
			get
			{
				if (this.prefix.Length == 0)
				{
					return this.name;
				}
				return this.prefix + ":" + this.name;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060025BF RID: 9663 RVA: 0x000D4F44 File Offset: 0x000D3144
		public override int Count
		{
			get
			{
				if (this._queryIterator != null)
				{
					return this._queryIterator.Count;
				}
				return 1;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x060025C0 RID: 9664 RVA: 0x0003D9A6 File Offset: 0x0003BBA6
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Any;
			}
		}

		// Token: 0x0400120C RID: 4620
		protected string prefix;

		// Token: 0x0400120D RID: 4621
		protected string name;

		// Token: 0x0400120E RID: 4622
		protected XsltContext xsltContext;

		// Token: 0x0400120F RID: 4623
		private ResetableIterator _queryIterator;
	}
}
