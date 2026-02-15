using System;

namespace System.Buffers
{
	// Token: 0x0200077D RID: 1917
	public abstract class ArrayPool<T>
	{
		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06003CD7 RID: 15575 RVA: 0x000EA64C File Offset: 0x000E884C
		public static ArrayPool<T> Shared { get; } = new TlsOverPerCoreLockedStacksArrayPool<T>();

		// Token: 0x06003CD8 RID: 15576 RVA: 0x000EA653 File Offset: 0x000E8853
		public static ArrayPool<T> Create(int maxArrayLength, int maxArraysPerBucket)
		{
			return new ConfigurableArrayPool<T>(maxArrayLength, maxArraysPerBucket);
		}

		// Token: 0x06003CD9 RID: 15577
		public abstract T[] Rent(int minimumLength);

		// Token: 0x06003CDA RID: 15578
		public abstract void Return(T[] array, bool clearArray = false);
	}
}
