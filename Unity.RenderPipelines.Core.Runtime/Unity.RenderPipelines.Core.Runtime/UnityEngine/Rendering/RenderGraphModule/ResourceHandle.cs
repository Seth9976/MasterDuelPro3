using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000276 RID: 630
	internal struct ResourceHandle : IEquatable<ResourceHandle>
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x0003E25D File Offset: 0x0003C45D
		public int index
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (int)(this.m_Value & 65535U);
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x0003E26B File Offset: 0x0003C46B
		public int iType
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (int)this.type;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x0003E273 File Offset: 0x0003C473
		// (set) Token: 0x06001129 RID: 4393 RVA: 0x0003E27B File Offset: 0x0003C47B
		public int version
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Version;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x0003E284 File Offset: 0x0003C484
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x0003E28C File Offset: 0x0003C48C
		public RenderGraphResourceType type { readonly get; private set; }

		// Token: 0x0600112C RID: 4396 RVA: 0x0003E295 File Offset: 0x0003C495
		internal ResourceHandle(int value, RenderGraphResourceType type, bool shared)
		{
			this.m_Value = (uint)((value & 65535) | (int)(shared ? ResourceHandle.s_SharedResourceValidBit : ResourceHandle.s_CurrentValidBit));
			this.type = type;
			this.m_Version = -1;
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0003E2C2 File Offset: 0x0003C4C2
		internal ResourceHandle(in ResourceHandle h, int version)
		{
			this.m_Value = h.m_Value;
			this.type = h.type;
			this.m_Version = version;
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0003E2E4 File Offset: 0x0003C4E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsValid()
		{
			uint validity = this.m_Value & 4294901760U;
			return validity != 0U && (validity == ResourceHandle.s_CurrentValidBit || validity == ResourceHandle.s_SharedResourceValidBit);
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0003E315 File Offset: 0x0003C515
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsNull()
		{
			return this.index == 0;
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0003E324 File Offset: 0x0003C524
		public static void NewFrame(int executionIndex)
		{
			uint previousValidBit = ResourceHandle.s_CurrentValidBit;
			ResourceHandle.s_CurrentValidBit = (uint)((uint)((executionIndex >> 16) ^ ((executionIndex & 65535) * 58546883)) << 16);
			if (ResourceHandle.s_CurrentValidBit == 0U || ResourceHandle.s_CurrentValidBit == ResourceHandle.s_SharedResourceValidBit)
			{
				uint value = 1U;
				while (previousValidBit == value << 16)
				{
					value += 1U;
				}
				ResourceHandle.s_CurrentValidBit = value << 16;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x0003E37C File Offset: 0x0003C57C
		public bool IsVersioned
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Version >= 0;
			}
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0003E38A File Offset: 0x0003C58A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(ResourceHandle hdl)
		{
			return hdl.m_Value == this.m_Value && hdl.m_Version == this.m_Version && hdl.type == this.type;
		}

		// Token: 0x04000AE1 RID: 2785
		private const uint kValidityMask = 4294901760U;

		// Token: 0x04000AE2 RID: 2786
		private const uint kIndexMask = 65535U;

		// Token: 0x04000AE3 RID: 2787
		private uint m_Value;

		// Token: 0x04000AE4 RID: 2788
		private int m_Version;

		// Token: 0x04000AE5 RID: 2789
		private static uint s_CurrentValidBit = 65536U;

		// Token: 0x04000AE6 RID: 2790
		private static uint s_SharedResourceValidBit = 2147418112U;
	}
}
