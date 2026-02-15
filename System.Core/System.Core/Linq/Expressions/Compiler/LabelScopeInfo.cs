using System;
using System.Collections.Generic;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020000F4 RID: 244
	internal sealed class LabelScopeInfo
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x0001B13D File Offset: 0x0001933D
		internal LabelScopeInfo(LabelScopeInfo parent, LabelScopeKind kind)
		{
			this.Parent = parent;
			this.Kind = kind;
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0001B154 File Offset: 0x00019354
		internal bool CanJumpInto
		{
			get
			{
				LabelScopeKind kind = this.Kind;
				return kind <= LabelScopeKind.Lambda;
			}
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001B16F File Offset: 0x0001936F
		internal bool ContainsTarget(LabelTarget target)
		{
			return this._labels != null && this._labels.ContainsKey(target);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001B187 File Offset: 0x00019387
		internal bool TryGetLabelInfo(LabelTarget target, out LabelInfo info)
		{
			if (this._labels == null)
			{
				info = null;
				return false;
			}
			return this._labels.TryGetValue(target, out info);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0001B1A3 File Offset: 0x000193A3
		internal void AddLabelInfo(LabelTarget target, LabelInfo info)
		{
			if (this._labels == null)
			{
				this._labels = new Dictionary<LabelTarget, LabelInfo>();
			}
			this._labels.Add(target, info);
		}

		// Token: 0x0400027E RID: 638
		private Dictionary<LabelTarget, LabelInfo> _labels;

		// Token: 0x0400027F RID: 639
		internal readonly LabelScopeKind Kind;

		// Token: 0x04000280 RID: 640
		internal readonly LabelScopeInfo Parent;
	}
}
