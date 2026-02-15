using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace YgomSystem.UI
{
	// Token: 0x0200058D RID: 1421
	public class DeviceIcon : MonoBehaviour
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06002CDF RID: 11487 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002CE0 RID: 11488 RVA: 0x0000216D File Offset: 0x0000036D
		public UnityEvent onChanged
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002CE2 RID: 11490 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isActivate
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Awake()
		{
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void OnDestroy()
		{
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void Setup()
		{
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x0000216D File Offset: 0x0000036D
		protected virtual void UpdateDisplay(Action onCompleted = null)
		{
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDisp(bool disp)
		{
		}

		// Token: 0x04002B1D RID: 11037
		private UnityAction<SelectorManager.InputDevice> changeDeviceAction;

		// Token: 0x04002B1E RID: 11038
		public SelectorManager.InputDevice displayInputDevice;

		// Token: 0x04002B1F RID: 11039
		public bool alwaysShowOnConsole;

		// Token: 0x04002B20 RID: 11040
		public DeviceIcon.DispType dispTarget;

		// Token: 0x04002B21 RID: 11041
		protected bool isDisp;

		// Token: 0x04002B22 RID: 11042
		protected bool setup;

		// Token: 0x04002B23 RID: 11043
		protected bool _isActivate;

		// Token: 0x0200058E RID: 1422
		public enum DispType
		{
			// Token: 0x04002B25 RID: 11045
			Graphic,
			// Token: 0x04002B26 RID: 11046
			GameObject
		}
	}
}
