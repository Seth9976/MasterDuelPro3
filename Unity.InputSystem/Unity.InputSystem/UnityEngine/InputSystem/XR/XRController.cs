using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.XR;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x020000E3 RID: 227
	[InputControlLayout(commonUsages = new string[] { "LeftHand", "RightHand" }, isGenericTypeOfDevice = true, displayName = "XR Controller")]
	public class XRController : TrackedDevice
	{
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x0003DED1 File Offset: 0x0003C0D1
		public static XRController leftHand
		{
			get
			{
				return InputSystem.GetDevice<XRController>(CommonUsages.LeftHand);
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x0003DEDD File Offset: 0x0003C0DD
		public static XRController rightHand
		{
			get
			{
				return InputSystem.GetDevice<XRController>(CommonUsages.RightHand);
			}
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0003DEEC File Offset: 0x0003C0EC
		protected override void FinishSetup()
		{
			base.FinishSetup();
			XRDeviceDescriptor deviceDescriptor = XRDeviceDescriptor.FromJson(base.description.capabilities);
			if (deviceDescriptor != null)
			{
				if ((deviceDescriptor.characteristics & InputDeviceCharacteristics.Left) != InputDeviceCharacteristics.None)
				{
					InputSystem.SetDeviceUsage(this, CommonUsages.LeftHand);
					return;
				}
				if ((deviceDescriptor.characteristics & InputDeviceCharacteristics.Right) != InputDeviceCharacteristics.None)
				{
					InputSystem.SetDeviceUsage(this, CommonUsages.RightHand);
				}
			}
		}
	}
}
