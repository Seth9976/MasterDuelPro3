using System;
using UnityEngine;
using UnityEngine.Events;

namespace YgomSystem.UI
{
	// Token: 0x020005B3 RID: 1459
	public abstract class PlatformVisibleIconBase : MonoBehaviour
	{
		// Token: 0x06002E03 RID: 11779 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06002E04 RID: 11780 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x0000216D File Offset: 0x0000036D
		private void Setup()
		{
		}

		// Token: 0x06002E06 RID: 11782
		protected abstract bool IsDispPlatform();

		// Token: 0x06002E07 RID: 11783 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateDisplay()
		{
		}

		// Token: 0x06002E08 RID: 11784 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDisp(bool disp)
		{
		}

		// Token: 0x04002BBD RID: 11197
		public PlatformVisibleIconBase.DispType dispTarget;

		// Token: 0x04002BBE RID: 11198
		private UnityAction<SelectorManager.InputDevice> changeDeviceAction;

		// Token: 0x04002BBF RID: 11199
		protected bool isDisp;

		// Token: 0x04002BC0 RID: 11200
		protected bool setup;

		// Token: 0x020005B4 RID: 1460
		public enum DispType
		{
			// Token: 0x04002BC2 RID: 11202
			Graphic,
			// Token: 0x04002BC3 RID: 11203
			GameObject
		}
	}
}
