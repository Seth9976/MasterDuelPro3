using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace Unity.XR.Oculus.Input
{
	// Token: 0x02000010 RID: 16
	[InputControlLayout(displayName = "Oculus Headset (w/ on-headset controls)", hideInUI = true)]
	public class OculusHMDExtended : OculusHMD
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00002CA6 File Offset: 0x00000EA6
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00002CAE File Offset: 0x00000EAE
		[InputControl]
		public ButtonControl back { get; protected set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00002CB7 File Offset: 0x00000EB7
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00002CBF File Offset: 0x00000EBF
		[InputControl]
		public Vector2Control touchpad { get; protected set; }

		// Token: 0x060000C1 RID: 193 RVA: 0x00002CC8 File Offset: 0x00000EC8
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.back = base.GetChildControl<ButtonControl>("back");
			this.touchpad = base.GetChildControl<Vector2Control>("touchpad");
		}
	}
}
