using System;
using System.Collections.Generic;
using UnityEngine.XR;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x020000EF RID: 239
	[Serializable]
	public class XRDeviceDescriptor
	{
		// Token: 0x06000C3A RID: 3130 RVA: 0x0003EF63 File Offset: 0x0003D163
		public string ToJson()
		{
			return JsonUtility.ToJson(this);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x0003EF6B File Offset: 0x0003D16B
		public static XRDeviceDescriptor FromJson(string json)
		{
			return JsonUtility.FromJson<XRDeviceDescriptor>(json);
		}

		// Token: 0x04000584 RID: 1412
		public string deviceName;

		// Token: 0x04000585 RID: 1413
		public string manufacturer;

		// Token: 0x04000586 RID: 1414
		public string serialNumber;

		// Token: 0x04000587 RID: 1415
		public InputDeviceCharacteristics characteristics;

		// Token: 0x04000588 RID: 1416
		public int deviceId;

		// Token: 0x04000589 RID: 1417
		public List<XRFeatureDescriptor> inputFeatures;
	}
}
