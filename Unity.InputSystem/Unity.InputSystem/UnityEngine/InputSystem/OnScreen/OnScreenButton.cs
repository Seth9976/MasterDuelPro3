using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.OnScreen
{
	// Token: 0x02000131 RID: 305
	[AddComponentMenu("Input/On-Screen Button")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.11/manual/OnScreen.html#on-screen-buttons")]
	public class OnScreenButton : OnScreenControl, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		// Token: 0x06000E1A RID: 3610 RVA: 0x00047263 File Offset: 0x00045463
		public void OnPointerUp(PointerEventData eventData)
		{
			base.SendValueToControl<float>(0f);
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00047270 File Offset: 0x00045470
		public void OnPointerDown(PointerEventData eventData)
		{
			base.SendValueToControl<float>(1f);
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x0004727D File Offset: 0x0004547D
		// (set) Token: 0x06000E1D RID: 3613 RVA: 0x00047285 File Offset: 0x00045485
		protected override string controlPathInternal
		{
			get
			{
				return this.m_ControlPath;
			}
			set
			{
				this.m_ControlPath = value;
			}
		}

		// Token: 0x0400071E RID: 1822
		[InputControl(layout = "Button")]
		[SerializeField]
		private string m_ControlPath;
	}
}
