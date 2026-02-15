using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000057 RID: 87
	internal struct GPUInstanceComponentDesc
	{
		// Token: 0x06000167 RID: 359 RVA: 0x0000A223 File Offset: 0x00008423
		public GPUInstanceComponentDesc(int inPropertyID, int inByteSize, bool inIsOverriden, bool inPerInstance, InstanceType inInstanceType, InstanceComponentGroup inComponentType)
		{
			this.propertyID = inPropertyID;
			this.byteSize = inByteSize;
			this.isOverriden = inIsOverriden;
			this.isPerInstance = inPerInstance;
			this.instanceType = inInstanceType;
			this.componentGroup = inComponentType;
		}

		// Token: 0x04000190 RID: 400
		public int propertyID;

		// Token: 0x04000191 RID: 401
		public int byteSize;

		// Token: 0x04000192 RID: 402
		public bool isOverriden;

		// Token: 0x04000193 RID: 403
		public bool isPerInstance;

		// Token: 0x04000194 RID: 404
		public InstanceType instanceType;

		// Token: 0x04000195 RID: 405
		public InstanceComponentGroup componentGroup;
	}
}
