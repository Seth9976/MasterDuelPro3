using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	[RequiredByNativeCode]
	[Serializable]
	public sealed class AnimationEvent
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002668 File Offset: 0x00000868
		public AnimationEvent()
		{
			this.m_Time = 0f;
			this.m_FunctionName = "";
			this.m_StringParameter = "";
			this.m_ObjectReferenceParameter = null;
			this.m_FloatParameter = 0f;
			this.m_IntParameter = 0;
			this.m_MessageOptions = 0;
			this.m_Source = AnimationEventSource.NoSource;
			this.m_StateSender = null;
		}

		// Token: 0x04000016 RID: 22
		internal float m_Time;

		// Token: 0x04000017 RID: 23
		internal string m_FunctionName;

		// Token: 0x04000018 RID: 24
		internal string m_StringParameter;

		// Token: 0x04000019 RID: 25
		internal Object m_ObjectReferenceParameter;

		// Token: 0x0400001A RID: 26
		internal float m_FloatParameter;

		// Token: 0x0400001B RID: 27
		internal int m_IntParameter;

		// Token: 0x0400001C RID: 28
		internal int m_MessageOptions;

		// Token: 0x0400001D RID: 29
		internal AnimationEventSource m_Source;

		// Token: 0x0400001E RID: 30
		internal AnimationState m_StateSender;

		// Token: 0x0400001F RID: 31
		internal AnimatorStateInfo m_AnimatorStateInfo;

		// Token: 0x04000020 RID: 32
		internal AnimatorClipInfo m_AnimatorClipInfo;
	}
}
