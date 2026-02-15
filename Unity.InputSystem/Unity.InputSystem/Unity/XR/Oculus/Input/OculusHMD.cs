using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;

namespace Unity.XR.Oculus.Input
{
	// Token: 0x0200000C RID: 12
	[InputControlLayout(displayName = "Oculus Headset", hideInUI = true)]
	public class OculusHMD : XRHMD
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000027BE File Offset: 0x000009BE
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000027C6 File Offset: 0x000009C6
		[InputControl]
		[InputControl(name = "trackingState", layout = "Integer", aliases = new string[] { "devicetrackingstate" })]
		[InputControl(name = "isTracked", layout = "Button", aliases = new string[] { "deviceistracked" })]
		public ButtonControl userPresence { get; protected set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000027CF File Offset: 0x000009CF
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000027D7 File Offset: 0x000009D7
		[InputControl(noisy = true)]
		public Vector3Control deviceAngularVelocity { get; protected set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000027E0 File Offset: 0x000009E0
		// (set) Token: 0x06000074 RID: 116 RVA: 0x000027E8 File Offset: 0x000009E8
		[InputControl(noisy = true)]
		public Vector3Control deviceAcceleration { get; protected set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000075 RID: 117 RVA: 0x000027F1 File Offset: 0x000009F1
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000027F9 File Offset: 0x000009F9
		[InputControl(noisy = true)]
		public Vector3Control deviceAngularAcceleration { get; protected set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002802 File Offset: 0x00000A02
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000280A File Offset: 0x00000A0A
		[InputControl(noisy = true)]
		public Vector3Control leftEyeAngularVelocity { get; protected set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002813 File Offset: 0x00000A13
		// (set) Token: 0x0600007A RID: 122 RVA: 0x0000281B File Offset: 0x00000A1B
		[InputControl(noisy = true)]
		public Vector3Control leftEyeAcceleration { get; protected set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002824 File Offset: 0x00000A24
		// (set) Token: 0x0600007C RID: 124 RVA: 0x0000282C File Offset: 0x00000A2C
		[InputControl(noisy = true)]
		public Vector3Control leftEyeAngularAcceleration { get; protected set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002835 File Offset: 0x00000A35
		// (set) Token: 0x0600007E RID: 126 RVA: 0x0000283D File Offset: 0x00000A3D
		[InputControl(noisy = true)]
		public Vector3Control rightEyeAngularVelocity { get; protected set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002846 File Offset: 0x00000A46
		// (set) Token: 0x06000080 RID: 128 RVA: 0x0000284E File Offset: 0x00000A4E
		[InputControl(noisy = true)]
		public Vector3Control rightEyeAcceleration { get; protected set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002857 File Offset: 0x00000A57
		// (set) Token: 0x06000082 RID: 130 RVA: 0x0000285F File Offset: 0x00000A5F
		[InputControl(noisy = true)]
		public Vector3Control rightEyeAngularAcceleration { get; protected set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002868 File Offset: 0x00000A68
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002870 File Offset: 0x00000A70
		[InputControl(noisy = true)]
		public Vector3Control centerEyeAngularVelocity { get; protected set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002879 File Offset: 0x00000A79
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002881 File Offset: 0x00000A81
		[InputControl(noisy = true)]
		public Vector3Control centerEyeAcceleration { get; protected set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000288A File Offset: 0x00000A8A
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00002892 File Offset: 0x00000A92
		[InputControl(noisy = true)]
		public Vector3Control centerEyeAngularAcceleration { get; protected set; }

		// Token: 0x06000089 RID: 137 RVA: 0x0000289C File Offset: 0x00000A9C
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.userPresence = base.GetChildControl<ButtonControl>("userPresence");
			this.deviceAngularVelocity = base.GetChildControl<Vector3Control>("deviceAngularVelocity");
			this.deviceAcceleration = base.GetChildControl<Vector3Control>("deviceAcceleration");
			this.deviceAngularAcceleration = base.GetChildControl<Vector3Control>("deviceAngularAcceleration");
			this.leftEyeAngularVelocity = base.GetChildControl<Vector3Control>("leftEyeAngularVelocity");
			this.leftEyeAcceleration = base.GetChildControl<Vector3Control>("leftEyeAcceleration");
			this.leftEyeAngularAcceleration = base.GetChildControl<Vector3Control>("leftEyeAngularAcceleration");
			this.rightEyeAngularVelocity = base.GetChildControl<Vector3Control>("rightEyeAngularVelocity");
			this.rightEyeAcceleration = base.GetChildControl<Vector3Control>("rightEyeAcceleration");
			this.rightEyeAngularAcceleration = base.GetChildControl<Vector3Control>("rightEyeAngularAcceleration");
			this.centerEyeAngularVelocity = base.GetChildControl<Vector3Control>("centerEyeAngularVelocity");
			this.centerEyeAcceleration = base.GetChildControl<Vector3Control>("centerEyeAcceleration");
			this.centerEyeAngularAcceleration = base.GetChildControl<Vector3Control>("centerEyeAngularAcceleration");
		}
	}
}
