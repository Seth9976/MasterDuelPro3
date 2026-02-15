using System;

namespace System.Buffers
{
	// Token: 0x02000782 RID: 1922
	public interface IMemoryOwner<T> : IDisposable
	{
		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06003CEC RID: 15596
		Memory<T> Memory { get; }
	}
}
