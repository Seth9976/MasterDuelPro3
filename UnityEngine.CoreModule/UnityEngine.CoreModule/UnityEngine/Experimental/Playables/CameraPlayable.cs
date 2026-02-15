using System;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Playables
{
	// Token: 0x020003F0 RID: 1008
	[NativeHeader("Runtime/Camera//Director/CameraPlayable.h")]
	[StaticAccessor("CameraPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Export/Director/CameraPlayable.bindings.h")]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[RequiredByNativeCode]
	public struct CameraPlayable : IPlayable, IEquatable<CameraPlayable>
	{
		// Token: 0x06001B40 RID: 6976 RVA: 0x0003C76C File Offset: 0x0003A96C
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x0003C784 File Offset: 0x0003A984
		public bool Equals(CameraPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x04000D80 RID: 3456
		private PlayableHandle m_Handle;
	}
}
