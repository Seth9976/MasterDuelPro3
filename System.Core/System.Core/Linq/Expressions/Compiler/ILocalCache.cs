using System;
using System.Reflection.Emit;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020000FD RID: 253
	internal interface ILocalCache
	{
		// Token: 0x060008D0 RID: 2256
		LocalBuilder GetLocal(Type type);

		// Token: 0x060008D1 RID: 2257
		void FreeLocal(LocalBuilder local);
	}
}
