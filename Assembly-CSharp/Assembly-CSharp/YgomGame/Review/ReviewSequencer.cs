using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.UI;

namespace YgomGame.Review
{
	// Token: 0x02000A09 RID: 2569
	public class ReviewSequencer : MonoBehaviour
	{
		// Token: 0x06004A88 RID: 19080 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Launch(IReadOnlyDictionary<string, object> reviewData, Action callback)
		{
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06004A8A RID: 19082 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToReview()
		{
		}

		// Token: 0x06004A8D RID: 19085 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToSelectEnqueteOrSupport()
		{
		}

		// Token: 0x06004A8E RID: 19086 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToEnquete()
		{
		}

		// Token: 0x06004A8F RID: 19087 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToSupport()
		{
		}

		// Token: 0x06004A90 RID: 19088 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToCompleteAnswer()
		{
		}

		// Token: 0x06004A91 RID: 19089 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToFinish()
		{
		}

		// Token: 0x040088AF RID: 34991
		private readonly string k_ReviewEntryDialogUIPrefPath;

		// Token: 0x040088B0 RID: 34992
		private readonly string k_SelectEnqueteOrSuportPrefPath;

		// Token: 0x040088B1 RID: 34993
		private IReadOnlyDictionary<string, object> m_ReviewData;

		// Token: 0x040088B2 RID: 34994
		private int m_EnqueteId;

		// Token: 0x040088B3 RID: 34995
		private string m_ReviewURL;

		// Token: 0x040088B4 RID: 34996
		private Action m_Callback;

		// Token: 0x040088B5 RID: 34997
		private ViewController m_RootContent;

		// Token: 0x040088B6 RID: 34998
		private ViewController m_RootDialog;
	}
}
