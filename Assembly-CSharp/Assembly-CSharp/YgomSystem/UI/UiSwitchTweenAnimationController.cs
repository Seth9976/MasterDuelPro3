using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace YgomSystem.UI
{
	// Token: 0x0200064C RID: 1612
	public class UiSwitchTweenAnimationController : MonoBehaviour
	{
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x0600326B RID: 12907 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600326C RID: 12908 RVA: 0x0000216D File Offset: 0x0000036D
		public event UnityAction onShow
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x1400003A RID: 58
		// (add) Token: 0x0600326D RID: 12909 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x0600326E RID: 12910 RVA: 0x0000216D File Offset: 0x0000036D
		public event UnityAction onHide
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x1400003B RID: 59
		// (add) Token: 0x0600326F RID: 12911 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06003270 RID: 12912 RVA: 0x0000216D File Offset: 0x0000036D
		public event UnityAction<int> onChain
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show()
		{
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x0000216D File Offset: 0x0000036D
		public void Hide()
		{
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x0000216D File Offset: 0x0000036D
		public void Chain()
		{
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnShow()
		{
		}

		// Token: 0x06003276 RID: 12918 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnHide()
		{
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChain(int index)
		{
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x0000216D File Offset: 0x0000036D
		private void RegistChainTween(bool isEventTween)
		{
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x0000216D File Offset: 0x0000036D
		private void StopAllTween()
		{
		}

		// Token: 0x0600327A RID: 12922 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x04002EFC RID: 12028
		public bool activeControl;

		// Token: 0x04002EFD RID: 12029
		public string labelShow;

		// Token: 0x04002EFE RID: 12030
		public string labelShow_Event;

		// Token: 0x04002EFF RID: 12031
		public string labelHide;

		// Token: 0x04002F00 RID: 12032
		public string labelHide_Event;

		// Token: 0x04002F01 RID: 12033
		public string labelChain;

		// Token: 0x04002F02 RID: 12034
		public string labelChain_Event;
	}
}
