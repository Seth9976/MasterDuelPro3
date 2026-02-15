using System;
using UnityEngine;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x020006B6 RID: 1718
	public class PlayableLoopFlag : MonoBehaviour
	{
		// Token: 0x060035B7 RID: 13751 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool GetLoopFlag(PlayableDirector director)
		{
			return false;
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetLoopFlag(PlayableDirector director, bool value)
		{
		}

		// Token: 0x040030F1 RID: 12529
		[SerializeField]
		private bool m_IsLoop;
	}
}
