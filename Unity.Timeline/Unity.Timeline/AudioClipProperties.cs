using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200002C RID: 44
	[NotKeyable]
	[Serializable]
	internal class AudioClipProperties : PlayableBehaviour
	{
		// Token: 0x040000D4 RID: 212
		[Range(0f, 1f)]
		public float volume = 1f;
	}
}
