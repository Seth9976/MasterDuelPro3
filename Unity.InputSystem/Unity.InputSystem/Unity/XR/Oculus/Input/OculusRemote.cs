using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace Unity.XR.Oculus.Input
{
	// Token: 0x0200000F RID: 15
	[InputControlLayout(displayName = "Oculus Remote", hideInUI = true)]
	public class OculusRemote : InputDevice
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002C30 File Offset: 0x00000E30
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00002C38 File Offset: 0x00000E38
		[InputControl]
		public ButtonControl back { get; protected set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002C41 File Offset: 0x00000E41
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002C49 File Offset: 0x00000E49
		[InputControl]
		public ButtonControl start { get; protected set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002C52 File Offset: 0x00000E52
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00002C5A File Offset: 0x00000E5A
		[InputControl]
		public Vector2Control touchpad { get; protected set; }

		// Token: 0x060000BB RID: 187 RVA: 0x00002C63 File Offset: 0x00000E63
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.back = base.GetChildControl<ButtonControl>("back");
			this.start = base.GetChildControl<ButtonControl>("start");
			this.touchpad = base.GetChildControl<Vector2Control>("touchpad");
		}
	}
}
