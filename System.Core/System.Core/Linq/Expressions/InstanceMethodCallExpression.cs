using System;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x020000BD RID: 189
	internal class InstanceMethodCallExpression : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x0600062A RID: 1578 RVA: 0x0001645E File Offset: 0x0001465E
		public InstanceMethodCallExpression(MethodInfo method, Expression instance)
			: base(method)
		{
			this._instance = instance;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x0001646E File Offset: 0x0001466E
		internal override Expression GetInstance()
		{
			return this._instance;
		}

		// Token: 0x040001E0 RID: 480
		private readonly Expression _instance;
	}
}
