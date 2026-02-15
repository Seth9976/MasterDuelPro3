using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200011C RID: 284
	public interface IPromise<T> : IResolvePromise<T>, IRejectPromise, ICancelPromise
	{
	}
}
