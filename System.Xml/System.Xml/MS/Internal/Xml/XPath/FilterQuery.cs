using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000347 RID: 839
	internal sealed class FilterQuery : BaseAxisQuery
	{
		// Token: 0x060025C6 RID: 9670 RVA: 0x000D4F81 File Offset: 0x000D3181
		public FilterQuery(Query qyParent, Query cond, bool noPosition)
			: base(qyParent)
		{
			this._cond = cond;
			this._noPosition = noPosition;
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x000D4F98 File Offset: 0x000D3198
		private FilterQuery(FilterQuery other)
			: base(other)
		{
			this._cond = Query.Clone(other._cond);
			this._noPosition = other._noPosition;
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x000D4FBE File Offset: 0x000D31BE
		public override void Reset()
		{
			this._cond.Reset();
			base.Reset();
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x060025C9 RID: 9673 RVA: 0x000D4FD1 File Offset: 0x000D31D1
		public Query Condition
		{
			get
			{
				return this._cond;
			}
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x000D4FDC File Offset: 0x000D31DC
		public override void SetXsltContext(XsltContext input)
		{
			base.SetXsltContext(input);
			this._cond.SetXsltContext(input);
			if (this._cond.StaticType != XPathResultType.Number && this._cond.StaticType != XPathResultType.Any && this._noPosition)
			{
				ReversePositionQuery reversePositionQuery = this.qyInput as ReversePositionQuery;
				if (reversePositionQuery != null)
				{
					this.qyInput = reversePositionQuery.input;
				}
			}
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x000D503C File Offset: 0x000D323C
		public override XPathNavigator Advance()
		{
			while ((this.currentNode = this.qyInput.Advance()) != null)
			{
				if (this.EvaluatePredicate())
				{
					this.position++;
					return this.currentNode;
				}
			}
			return null;
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x000D5080 File Offset: 0x000D3280
		internal bool EvaluatePredicate()
		{
			object obj = this._cond.Evaluate(this.qyInput);
			if (obj is XPathNodeIterator)
			{
				return this._cond.Advance() != null;
			}
			if (obj is string)
			{
				return ((string)obj).Length != 0;
			}
			if (obj is double)
			{
				return (double)obj == (double)this.qyInput.CurrentPosition;
			}
			return !(obj is bool) || (bool)obj;
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x060025CD RID: 9677 RVA: 0x000D50FA File Offset: 0x000D32FA
		public override QueryProps Properties
		{
			get
			{
				return QueryProps.Position | (this.qyInput.Properties & (QueryProps)24);
			}
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x000D510C File Offset: 0x000D330C
		public override XPathNodeIterator Clone()
		{
			return new FilterQuery(this);
		}

		// Token: 0x04001212 RID: 4626
		private Query _cond;

		// Token: 0x04001213 RID: 4627
		private bool _noPosition;
	}
}
