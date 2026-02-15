using System;
using System.Linq.Expressions;

namespace System.Dynamic.Utils
{
	// Token: 0x0200014D RID: 333
	internal sealed class ListParameterProvider : ListProvider<ParameterExpression>
	{
		// Token: 0x06000AEC RID: 2796 RVA: 0x0002B035 File Offset: 0x00029235
		internal ListParameterProvider(IParameterProvider provider, ParameterExpression arg0)
		{
			this._provider = provider;
			this._arg0 = arg0;
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x0002B04B File Offset: 0x0002924B
		protected override ParameterExpression First
		{
			get
			{
				return this._arg0;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x0002B053 File Offset: 0x00029253
		protected override int ElementCount
		{
			get
			{
				return this._provider.ParameterCount;
			}
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0002B060 File Offset: 0x00029260
		protected override ParameterExpression GetElement(int index)
		{
			return this._provider.GetParameter(index);
		}

		// Token: 0x04000354 RID: 852
		private readonly IParameterProvider _provider;

		// Token: 0x04000355 RID: 853
		private readonly ParameterExpression _arg0;
	}
}
