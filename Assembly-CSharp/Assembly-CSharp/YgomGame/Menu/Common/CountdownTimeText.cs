using System;
using TMPro;
using UnityEngine;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B29 RID: 2857
	public class CountdownTimeText : MonoBehaviour
	{
		// Token: 0x06005341 RID: 21313 RVA: 0x0000216A File Offset: 0x0000036A
		public static CountdownTimeText Attach(TMP_Text target, long limitUnixTime, Action<TMP_Text, long> textUpdateCallback)
		{
			return null;
		}

		// Token: 0x06005342 RID: 21314 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Detach(TMP_Text target, bool isDestroy = true)
		{
		}

		// Token: 0x06005343 RID: 21315 RVA: 0x000F4C54 File Offset: 0x000F2E54
		public TimeSpan GetTimeSpan()
		{
			return default(TimeSpan);
		}

		// Token: 0x06005344 RID: 21316 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x06005345 RID: 21317 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetClampedMinutesRemainText(TMP_Text targetText, long remainSec)
		{
		}

		// Token: 0x06005346 RID: 21318 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetClampedDaysRemainText(TMP_Text targetText, long remainSec)
		{
		}

		// Token: 0x06005347 RID: 21319 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetClampedDaysRemainText(long remainSec)
		{
			return null;
		}

		// Token: 0x04009117 RID: 37143
		private TMP_Text m_TargetText;

		// Token: 0x04009118 RID: 37144
		private long m_LimitUnixTime;

		// Token: 0x04009119 RID: 37145
		private long m_LastUpdatedUnixTime;

		// Token: 0x0400911A RID: 37146
		private Action<TMP_Text, long> m_TextUpdateCallback;
	}
}
