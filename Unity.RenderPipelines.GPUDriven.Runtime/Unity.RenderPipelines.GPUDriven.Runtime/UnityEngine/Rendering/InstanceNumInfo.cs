using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.Rendering
{
	// Token: 0x0200008C RID: 140
	internal struct InstanceNumInfo
	{
		// Token: 0x0600026C RID: 620 RVA: 0x0000FF70 File Offset: 0x0000E170
		public unsafe void InitDefault()
		{
			for (int i = 0; i < 2; i++)
			{
				*((ref this.InstanceNums.FixedElementField) + (IntPtr)i * 4) = 0;
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000FF9B File Offset: 0x0000E19B
		public unsafe InstanceNumInfo(InstanceType type, int instanceNum)
		{
			this.InitDefault();
			*((ref this.InstanceNums.FixedElementField) + (IntPtr)type * 4) = instanceNum;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000FFB5 File Offset: 0x0000E1B5
		public unsafe InstanceNumInfo(int meshRendererNum = 0, int speedTreeNum = 0)
		{
			this.InitDefault();
			this.InstanceNums.FixedElementField = meshRendererNum;
			*((ref this.InstanceNums.FixedElementField) + 4) = speedTreeNum;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000FFD9 File Offset: 0x0000E1D9
		public unsafe int GetInstanceNum(InstanceType type)
		{
			return *((ref this.InstanceNums.FixedElementField) + (IntPtr)type * 4);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000FFEC File Offset: 0x0000E1EC
		public int GetInstanceNumIncludingChildren(InstanceType type)
		{
			int numInstances = this.GetInstanceNum(type);
			foreach (InstanceType childType in InstanceTypeInfo.GetChildTypes(type))
			{
				numInstances += this.GetInstanceNumIncludingChildren(childType);
			}
			return numInstances;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0001004C File Offset: 0x0000E24C
		public unsafe int GetTotalInstanceNum()
		{
			int totalInstanceNum = 0;
			for (int i = 0; i < 2; i++)
			{
				totalInstanceNum += *((ref this.InstanceNums.FixedElementField) + (IntPtr)i * 4);
			}
			return totalInstanceNum;
		}

		// Token: 0x040002EF RID: 751
		[FixedBuffer(typeof(int), 2)]
		public InstanceNumInfo.<InstanceNums>e__FixedBuffer InstanceNums;

		// Token: 0x0200008D RID: 141
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 8)]
		public struct <InstanceNums>e__FixedBuffer
		{
			// Token: 0x040002F0 RID: 752
			public int FixedElementField;
		}
	}
}
