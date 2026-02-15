using System;
using UnityEngine.Bindings;

namespace UnityEngine.Playables
{
	// Token: 0x02000308 RID: 776
	public struct PlayableBinding
	{
		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x0002D1D4 File Offset: 0x0002B3D4
		public string streamName
		{
			get
			{
				return this.m_StreamName;
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x0002D1EC File Offset: 0x0002B3EC
		public Object sourceObject
		{
			get
			{
				return this.m_SourceObject;
			}
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0002D204 File Offset: 0x0002B404
		internal PlayableOutput CreateOutput(PlayableGraph graph)
		{
			bool flag = this.m_CreateOutputMethod != null;
			PlayableOutput playableOutput;
			if (flag)
			{
				playableOutput = this.m_CreateOutputMethod(graph, this.m_StreamName);
			}
			else
			{
				playableOutput = PlayableOutput.Null;
			}
			return playableOutput;
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0002D240 File Offset: 0x0002B440
		[VisibleToOtherModules]
		internal static PlayableBinding CreateInternal(string name, Object sourceObject, Type sourceType, PlayableBinding.CreateOutputMethod createFunction)
		{
			return new PlayableBinding
			{
				m_StreamName = name,
				m_SourceObject = sourceObject,
				m_SourceBindingType = sourceType,
				m_CreateOutputMethod = createFunction
			};
		}

		// Token: 0x04000813 RID: 2067
		private string m_StreamName;

		// Token: 0x04000814 RID: 2068
		private Object m_SourceObject;

		// Token: 0x04000815 RID: 2069
		private Type m_SourceBindingType;

		// Token: 0x04000816 RID: 2070
		private PlayableBinding.CreateOutputMethod m_CreateOutputMethod;

		// Token: 0x04000817 RID: 2071
		public static readonly PlayableBinding[] None = new PlayableBinding[0];

		// Token: 0x04000818 RID: 2072
		public static readonly double DefaultDuration = double.PositiveInfinity;

		// Token: 0x02000309 RID: 777
		// (Invoke) Token: 0x0600156A RID: 5482
		[VisibleToOtherModules]
		internal delegate PlayableOutput CreateOutputMethod(PlayableGraph graph, string name);
	}
}
