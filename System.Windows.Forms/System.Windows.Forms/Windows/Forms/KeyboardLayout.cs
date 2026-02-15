using System;

namespace System.Windows.Forms
{
	// Token: 0x020000F4 RID: 244
	[Serializable]
	internal class KeyboardLayout
	{
		// Token: 0x04000574 RID: 1396
		public int Lcid;

		// Token: 0x04000575 RID: 1397
		public string Name;

		// Token: 0x04000576 RID: 1398
		public ScanTableIndex ScanIndex;

		// Token: 0x04000577 RID: 1399
		public VKeyTableIndex VKeyIndex;

		// Token: 0x04000578 RID: 1400
		public uint[][] Keys;
	}
}
