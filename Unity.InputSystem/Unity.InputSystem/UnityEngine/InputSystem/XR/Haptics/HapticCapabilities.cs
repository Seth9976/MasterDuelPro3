using System;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x020000F8 RID: 248
	public struct HapticCapabilities
	{
		// Token: 0x06000C7D RID: 3197 RVA: 0x0003F845 File Offset: 0x0003DA45
		public HapticCapabilities(uint numChannels, bool supportsImpulse, bool supportsBuffer, uint frequencyHz, uint maxBufferSize, uint optimalBufferSize)
		{
			this.numChannels = numChannels;
			this.supportsImpulse = supportsImpulse;
			this.supportsBuffer = supportsBuffer;
			this.frequencyHz = frequencyHz;
			this.maxBufferSize = maxBufferSize;
			this.optimalBufferSize = optimalBufferSize;
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0003F874 File Offset: 0x0003DA74
		public HapticCapabilities(uint numChannels, uint frequencyHz, uint maxBufferSize)
		{
			this = new HapticCapabilities(numChannels, false, false, frequencyHz, maxBufferSize, 0U);
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0003F882 File Offset: 0x0003DA82
		public readonly uint numChannels { get; }

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0003F88A File Offset: 0x0003DA8A
		public readonly bool supportsImpulse { get; }

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0003F892 File Offset: 0x0003DA92
		public readonly bool supportsBuffer { get; }

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0003F89A File Offset: 0x0003DA9A
		public readonly uint frequencyHz { get; }

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0003F8A2 File Offset: 0x0003DAA2
		public readonly uint maxBufferSize { get; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0003F8AA File Offset: 0x0003DAAA
		public readonly uint optimalBufferSize { get; }
	}
}
