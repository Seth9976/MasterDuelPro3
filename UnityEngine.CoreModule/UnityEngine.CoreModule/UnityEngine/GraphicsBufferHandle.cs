using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000F1 RID: 241
	[NativeClass("GfxBufferID")]
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	public readonly struct GraphicsBufferHandle : IEquatable<GraphicsBufferHandle>
	{
		// Token: 0x06000923 RID: 2339 RVA: 0x000116F0 File Offset: 0x0000F8F0
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00011710 File Offset: 0x0000F910
		public override bool Equals(object obj)
		{
			bool flag = obj is GraphicsBufferHandle;
			return flag && this.Equals((GraphicsBufferHandle)obj);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00011740 File Offset: 0x0000F940
		public bool Equals(GraphicsBufferHandle other)
		{
			return this.value == other.value;
		}

		// Token: 0x040002B9 RID: 697
		public readonly uint value;
	}
}
