using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000304 RID: 772
	[RequiredByNativeCode]
	public struct Playable : IPlayable, IEquatable<Playable>
	{
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x0002D048 File Offset: 0x0002B248
		public static Playable Null
		{
			get
			{
				return Playable.m_NullPlayable;
			}
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x0002D060 File Offset: 0x0002B260
		public static Playable Create(PlayableGraph graph, int inputCount = 0)
		{
			Playable playable = new Playable(graph.CreatePlayableHandle());
			playable.SetInputCount(inputCount);
			return playable;
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x0002D089 File Offset: 0x0002B289
		[VisibleToOtherModules]
		internal Playable(PlayableHandle handle)
		{
			this.m_Handle = handle;
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0002D094 File Offset: 0x0002B294
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0002D0AC File Offset: 0x0002B2AC
		public bool IsPlayableOfType<T>() where T : struct, IPlayable
		{
			return this.GetHandle().IsPlayableOfType<T>();
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x0002D0CC File Offset: 0x0002B2CC
		public Type GetPlayableType()
		{
			return this.GetHandle().GetPlayableType();
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x0002D0EC File Offset: 0x0002B2EC
		public bool Equals(Playable other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x04000811 RID: 2065
		private PlayableHandle m_Handle;

		// Token: 0x04000812 RID: 2066
		private static readonly Playable m_NullPlayable = new Playable(PlayableHandle.Null);
	}
}
