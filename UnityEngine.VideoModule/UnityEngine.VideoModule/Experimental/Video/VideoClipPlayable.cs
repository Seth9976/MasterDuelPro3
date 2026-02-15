using System;
using UnityEngine.Bindings;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Video
{
	// Token: 0x02000002 RID: 2
	[RequiredByNativeCode]
	[StaticAccessor("VideoClipPlayableBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Modules/Video/Public/VideoClip.h")]
	[NativeHeader("Modules/Video/Public/Director/VideoClipPlayable.h")]
	[NativeHeader("Modules/Video/Public/ScriptBindings/VideoClipPlayable.bindings.h")]
	public struct VideoClipPlayable : IPlayable, IEquatable<VideoClipPlayable>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		public bool Equals(VideoClipPlayable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x04000001 RID: 1
		private PlayableHandle m_Handle;
	}
}
