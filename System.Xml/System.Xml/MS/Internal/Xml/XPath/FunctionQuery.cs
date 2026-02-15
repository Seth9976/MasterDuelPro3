using System;
using System.Collections.Generic;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200034D RID: 845
	internal sealed class FunctionQuery : ExtensionQuery
	{
		// Token: 0x060025E9 RID: 9705 RVA: 0x000D5596 File Offset: 0x000D3796
		public FunctionQuery(string prefix, string name, List<Query> args)
			: base(prefix, name)
		{
			this._args = args;
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x000D55A8 File Offset: 0x000D37A8
		private FunctionQuery(FunctionQuery other)
			: base(other)
		{
			this._function = other._function;
			Query[] array = new Query[other._args.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Query.Clone(other._args[i]);
			}
			this._args = array;
			this._args = array;
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000D560C File Offset: 0x000D380C
		public override void SetXsltContext(XsltContext context)
		{
			if (context == null)
			{
				throw XPathException.Create("Namespace Manager or XsltContext needed. This query has a prefix, variable, or user-defined function.");
			}
			if (this.xsltContext != context)
			{
				this.xsltContext = context;
				foreach (Query query in this._args)
				{
					query.SetXsltContext(context);
				}
				XPathResultType[] array = new XPathResultType[this._args.Count];
				for (int i = 0; i < this._args.Count; i++)
				{
					array[i] = this._args[i].StaticType;
				}
				this._function = this.xsltContext.ResolveFunction(this.prefix, this.name, array);
				if (this._function == null)
				{
					throw XPathException.Create("The function '{0}()' is undefined.", base.QName);
				}
			}
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x000D56EC File Offset: 0x000D38EC
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			if (this.xsltContext == null)
			{
				throw XPathException.Create("Namespace Manager or XsltContext needed. This query has a prefix, variable, or user-defined function.");
			}
			object[] array = new object[this._args.Count];
			for (int i = 0; i < this._args.Count; i++)
			{
				array[i] = this._args[i].Evaluate(nodeIterator);
				if (array[i] is XPathNodeIterator)
				{
					array[i] = new XPathSelectionIterator(nodeIterator.Current, this._args[i]);
				}
			}
			object obj;
			try
			{
				obj = base.ProcessResult(this._function.Invoke(this.xsltContext, array, nodeIterator.Current));
			}
			catch (Exception ex)
			{
				throw XPathException.Create("Function '{0}()' has failed.", base.QName, ex);
			}
			return obj;
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x000D57B4 File Offset: 0x000D39B4
		public override XPathResultType StaticType
		{
			get
			{
				XPathResultType xpathResultType = ((this._function != null) ? this._function.ReturnType : XPathResultType.Any);
				if (xpathResultType == XPathResultType.Error)
				{
					xpathResultType = XPathResultType.Any;
				}
				return xpathResultType;
			}
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x000D57DF File Offset: 0x000D39DF
		public override XPathNodeIterator Clone()
		{
			return new FunctionQuery(this);
		}

		// Token: 0x0400123B RID: 4667
		private IList<Query> _args;

		// Token: 0x0400123C RID: 4668
		private IXsltContextFunction _function;
	}
}
