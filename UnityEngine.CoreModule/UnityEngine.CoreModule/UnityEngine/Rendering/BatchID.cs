using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000370 RID: 880
	[NativeClass("BatchID")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeHeader("Runtime/Camera/BatchRendererGroup.h")]
	public struct BatchID : IEquatable<BatchID>
	{
		// Token: 0x060018BD RID: 6333 RVA: 0x00034F8C File Offset: 0x0003318C
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00034FAC File Offset: 0x000331AC
		public override bool Equals(object obj)
		{
			bool flag = obj is BatchID;
			return flag && this.Equals((BatchID)obj);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00034FDC File Offset: 0x000331DC
		public bool Equals(BatchID other)
		{
			return this.value == other.value;
		}

		// Token: 0x04000A57 RID: 2647
		public static readonly BatchID Null = new BatchID
		{
			value = 0U
		};

		// Token: 0x04000A58 RID: 2648
		public uint value;
	}
}
