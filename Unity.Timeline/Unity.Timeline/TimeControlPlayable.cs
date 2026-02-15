using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000059 RID: 89
	public class TimeControlPlayable : PlayableBehaviour
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x00009F18 File Offset: 0x00008118
		public static ScriptPlayable<TimeControlPlayable> Create(PlayableGraph graph, ITimeControl timeControl)
		{
			if (timeControl == null)
			{
				return ScriptPlayable<TimeControlPlayable>.Null;
			}
			ScriptPlayable<TimeControlPlayable> handle = ScriptPlayable<TimeControlPlayable>.Create(graph, 0);
			handle.GetBehaviour().Initialize(timeControl);
			return handle;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00009F44 File Offset: 0x00008144
		public void Initialize(ITimeControl timeControl)
		{
			this.m_timeControl = timeControl;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00009F4D File Offset: 0x0000814D
		public override void PrepareFrame(Playable playable, FrameData info)
		{
			if (this.m_timeControl != null)
			{
				this.m_timeControl.SetTime(playable.GetTime<Playable>());
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00009F68 File Offset: 0x00008168
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			if (this.m_timeControl == null)
			{
				return;
			}
			if (!this.m_started)
			{
				this.m_timeControl.OnControlTimeStart();
				this.m_started = true;
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00009F8D File Offset: 0x0000818D
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (this.m_timeControl == null)
			{
				return;
			}
			if (this.m_started)
			{
				this.m_timeControl.OnControlTimeStop();
				this.m_started = false;
			}
		}

		// Token: 0x04000150 RID: 336
		private ITimeControl m_timeControl;

		// Token: 0x04000151 RID: 337
		private bool m_started;
	}
}
