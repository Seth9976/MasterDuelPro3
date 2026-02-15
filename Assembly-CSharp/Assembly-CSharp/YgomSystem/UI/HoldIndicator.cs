using System;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace YgomSystem.UI
{
	// Token: 0x0200059A RID: 1434
	public class HoldIndicator : MonoBehaviour
	{
		// Token: 0x06002D73 RID: 11635 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show(bool resetIndicator = true)
		{
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x0000216D File Offset: 0x0000036D
		public void Hide()
		{
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDispRoot(bool disp)
		{
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetIndication(float time, float max)
		{
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPoint(Vector2 screenPoint)
		{
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPointUseCurrentScreenPoint()
		{
		}

		// Token: 0x04002B6C RID: 11116
		private ElementObjectManager ui;

		// Token: 0x04002B6D RID: 11117
		private GameObject indicatorRoot;

		// Token: 0x04002B6E RID: 11118
		private RectTransform indicatorPosition;

		// Token: 0x04002B6F RID: 11119
		private Image indicator;

		// Token: 0x04002B70 RID: 11120
		private float ignoreRate;

		// Token: 0x04002B71 RID: 11121
		private bool show;
	}
}
