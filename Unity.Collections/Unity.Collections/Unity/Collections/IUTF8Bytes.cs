using System;

namespace Unity.Collections
{
	// Token: 0x0200007B RID: 123
	public interface IUTF8Bytes
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060006C2 RID: 1730
		bool IsEmpty { get; }

		// Token: 0x060006C3 RID: 1731
		unsafe byte* GetUnsafePtr();

		// Token: 0x060006C4 RID: 1732
		bool TryResize(int newLength, NativeArrayOptions clearOptions = NativeArrayOptions.ClearMemory);
	}
}
