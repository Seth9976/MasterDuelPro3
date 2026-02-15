using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200006F RID: 111
	public interface ITaskPoolNode<T>
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600017D RID: 381
		ref T NextNode { get; }
	}
}
