using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000310 RID: 784
	[RequiredByNativeCode]
	public struct PlayableOutput : IPlayableOutput, IEquatable<PlayableOutput>
	{
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x0002DA40 File Offset: 0x0002BC40
		public static PlayableOutput Null
		{
			get
			{
				return PlayableOutput.m_NullPlayableOutput;
			}
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x0002DA57 File Offset: 0x0002BC57
		[VisibleToOtherModules]
		internal PlayableOutput(PlayableOutputHandle handle)
		{
			this.m_Handle = handle;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0002DA64 File Offset: 0x0002BC64
		public PlayableOutputHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x0002DA7C File Offset: 0x0002BC7C
		public bool IsPlayableOutputOfType<T>() where T : struct, IPlayableOutput
		{
			return this.GetHandle().IsPlayableOutputOfType<T>();
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x0002DA9C File Offset: 0x0002BC9C
		public bool Equals(PlayableOutput other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x0400082A RID: 2090
		private PlayableOutputHandle m_Handle;

		// Token: 0x0400082B RID: 2091
		private static readonly PlayableOutput m_NullPlayableOutput = new PlayableOutput(PlayableOutputHandle.Null);
	}
}
