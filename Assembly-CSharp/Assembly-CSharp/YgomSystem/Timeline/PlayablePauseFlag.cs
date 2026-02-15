using System;
using UnityEngine;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x020006B7 RID: 1719
	public class PlayablePauseFlag : MonoBehaviour
	{
		// Token: 0x060035BA RID: 13754 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetPauseFlag(PlayableDirector director)
		{
			return false;
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPauseFlag(PlayableDirector director, bool value)
		{
		}

		// Token: 0x040030F2 RID: 12530
		[SerializeField]
		private bool m_IsPause;
	}
}
