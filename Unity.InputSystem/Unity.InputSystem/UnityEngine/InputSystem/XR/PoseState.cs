using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.XR;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x020000E0 RID: 224
	[StructLayout(LayoutKind.Explicit, Size = 60)]
	public struct PoseState : IInputStateTypeInfo
	{
		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0003D99D File Offset: 0x0003BB9D
		public FourCC format
		{
			get
			{
				return PoseState.s_Format;
			}
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x0003D9A4 File Offset: 0x0003BBA4
		public PoseState(bool isTracked, InputTrackingState trackingState, Vector3 position, Quaternion rotation, Vector3 velocity, Vector3 angularVelocity)
		{
			this.isTracked = isTracked;
			this.trackingState = trackingState;
			this.position = position;
			this.rotation = rotation;
			this.velocity = velocity;
			this.angularVelocity = angularVelocity;
		}

		// Token: 0x0400053C RID: 1340
		internal const int kSizeInBytes = 60;

		// Token: 0x0400053D RID: 1341
		internal static readonly FourCC s_Format = new FourCC('P', 'o', 's', 'e');

		// Token: 0x0400053E RID: 1342
		[InputControl(displayName = "Is Tracked", layout = "Button", sizeInBits = 8U)]
		[FieldOffset(0)]
		public bool isTracked;

		// Token: 0x0400053F RID: 1343
		[InputControl(displayName = "Tracking State", layout = "Integer")]
		[FieldOffset(4)]
		public InputTrackingState trackingState;

		// Token: 0x04000540 RID: 1344
		[InputControl(displayName = "Position", noisy = true)]
		[FieldOffset(8)]
		public Vector3 position;

		// Token: 0x04000541 RID: 1345
		[InputControl(displayName = "Rotation", noisy = true)]
		[FieldOffset(20)]
		public Quaternion rotation;

		// Token: 0x04000542 RID: 1346
		[InputControl(displayName = "Velocity", noisy = true)]
		[FieldOffset(36)]
		public Vector3 velocity;

		// Token: 0x04000543 RID: 1347
		[InputControl(displayName = "Angular Velocity", noisy = true)]
		[FieldOffset(48)]
		public Vector3 angularVelocity;
	}
}
