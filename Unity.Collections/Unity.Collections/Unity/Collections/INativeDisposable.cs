using System;
using Unity.Jobs;

namespace Unity.Collections
{
	// Token: 0x0200003C RID: 60
	public interface INativeDisposable : IDisposable
	{
		// Token: 0x0600013A RID: 314
		JobHandle Dispose(JobHandle inputDeps);
	}
}
