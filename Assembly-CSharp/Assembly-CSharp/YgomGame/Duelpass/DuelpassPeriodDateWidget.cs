using System;
using TMPro;
using UnityEngine;

namespace YgomGame.Duelpass
{
	// Token: 0x02000C3B RID: 3131
	public class DuelpassPeriodDateWidget : MonoBehaviour
	{
		// Token: 0x06005950 RID: 22864 RVA: 0x0000216D File Offset: 0x0000036D
		public void Init()
		{
		}

		// Token: 0x06005951 RID: 22865 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnLimitGettingCloser()
		{
		}

		// Token: 0x06005952 RID: 22866 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnLimitNotClose()
		{
		}

		// Token: 0x06005953 RID: 22867 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06005954 RID: 22868 RVA: 0x0000216D File Offset: 0x0000036D
		private void CountStart()
		{
		}

		// Token: 0x0400951D RID: 38173
		private int endTimeStamp;

		// Token: 0x0400951E RID: 38174
		[SerializeField]
		private TMP_Text periodDateText;

		// Token: 0x0400951F RID: 38175
		[SerializeField]
		private GameObject panelColor;
	}
}
