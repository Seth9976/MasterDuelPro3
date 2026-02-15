using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000C5 RID: 197
	[NullableContext(2)]
	[Nullable(0)]
	internal class FSharpFunction
	{
		// Token: 0x0600060C RID: 1548 RVA: 0x00020103 File Offset: 0x0001E303
		public FSharpFunction(object instance, [Nullable(new byte[] { 1, 2, 1 })] MethodCall<object, object> invoker)
		{
			this._instance = instance;
			this._invoker = invoker;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00020119 File Offset: 0x0001E319
		[NullableContext(1)]
		public object Invoke(params object[] args)
		{
			return this._invoker(this._instance, args);
		}

		// Token: 0x04000449 RID: 1097
		private readonly object _instance;

		// Token: 0x0400044A RID: 1098
		[Nullable(new byte[] { 1, 2, 1 })]
		private readonly MethodCall<object, object> _invoker;
	}
}
