using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020000EA RID: 234
	internal sealed class ParameterList : IReadOnlyList<ParameterExpression>, IReadOnlyCollection<ParameterExpression>, IEnumerable<ParameterExpression>, IEnumerable
	{
		// Token: 0x060007AF RID: 1967 RVA: 0x000193E0 File Offset: 0x000175E0
		public ParameterList(IParameterProvider provider)
		{
			this._provider = provider;
		}

		// Token: 0x1700017C RID: 380
		public ParameterExpression this[int index]
		{
			get
			{
				return this._provider.GetParameter(index);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x000193FD File Offset: 0x000175FD
		public int Count
		{
			get
			{
				return this._provider.ParameterCount;
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0001940A File Offset: 0x0001760A
		public IEnumerator<ParameterExpression> GetEnumerator()
		{
			int i = 0;
			int j = this._provider.ParameterCount;
			while (i < j)
			{
				yield return this._provider.GetParameter(i);
				int num = i;
				i = num + 1;
			}
			yield break;
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00019419 File Offset: 0x00017619
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400025B RID: 603
		private readonly IParameterProvider _provider;
	}
}
