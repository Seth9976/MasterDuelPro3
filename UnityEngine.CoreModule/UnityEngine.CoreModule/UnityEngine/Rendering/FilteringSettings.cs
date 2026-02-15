using System;
using UnityEngine.Internal;

namespace UnityEngine.Rendering
{
	// Token: 0x020003B0 RID: 944
	public struct FilteringSettings : IEquatable<FilteringSettings>
	{
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001993 RID: 6547 RVA: 0x00037BDB File Offset: 0x00035DDB
		public static FilteringSettings defaultValue
		{
			get
			{
				return new FilteringSettings(new RenderQueueRange?(RenderQueueRange.all), -1, uint.MaxValue, 0);
			}
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00037BF0 File Offset: 0x00035DF0
		public FilteringSettings([DefaultValue("RenderQueueRange.all")] RenderQueueRange? renderQueueRange = null, int layerMask = -1, uint renderingLayerMask = 4294967295U, int excludeMotionVectorObjects = 0)
		{
			this = default(FilteringSettings);
			this.m_RenderQueueRange = renderQueueRange ?? RenderQueueRange.all;
			this.m_LayerMask = layerMask;
			this.m_RenderingLayerMask = renderingLayerMask;
			this.m_BatchLayerMask = uint.MaxValue;
			this.m_ExcludeMotionVectorObjects = excludeMotionVectorObjects;
			this.m_ForceAllMotionVectorObjects = 0;
			this.m_SortingLayerRange = SortingLayerRange.all;
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001995 RID: 6549 RVA: 0x00037C54 File Offset: 0x00035E54
		// (set) Token: 0x06001996 RID: 6550 RVA: 0x00037C6C File Offset: 0x00035E6C
		public RenderQueueRange renderQueueRange
		{
			get
			{
				return this.m_RenderQueueRange;
			}
			set
			{
				this.m_RenderQueueRange = value;
			}
		}

		// Token: 0x170003BD RID: 957
		// (set) Token: 0x06001997 RID: 6551 RVA: 0x00037C76 File Offset: 0x00035E76
		public int layerMask
		{
			set
			{
				this.m_LayerMask = value;
			}
		}

		// Token: 0x170003BE RID: 958
		// (set) Token: 0x06001998 RID: 6552 RVA: 0x00037C80 File Offset: 0x00035E80
		public uint renderingLayerMask
		{
			set
			{
				this.m_RenderingLayerMask = value;
			}
		}

		// Token: 0x170003BF RID: 959
		// (set) Token: 0x06001999 RID: 6553 RVA: 0x00037C8A File Offset: 0x00035E8A
		public uint batchLayerMask
		{
			set
			{
				this.m_BatchLayerMask = value;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (set) Token: 0x0600199A RID: 6554 RVA: 0x00037C94 File Offset: 0x00035E94
		public bool excludeMotionVectorObjects
		{
			set
			{
				this.m_ExcludeMotionVectorObjects = (value ? 1 : 0);
			}
		}

		// Token: 0x170003C1 RID: 961
		// (set) Token: 0x0600199B RID: 6555 RVA: 0x00037CA4 File Offset: 0x00035EA4
		public bool forceAllMotionVectorObjects
		{
			set
			{
				this.m_ForceAllMotionVectorObjects = (value ? 1 : 0);
			}
		}

		// Token: 0x170003C2 RID: 962
		// (set) Token: 0x0600199C RID: 6556 RVA: 0x00037CB4 File Offset: 0x00035EB4
		public SortingLayerRange sortingLayerRange
		{
			set
			{
				this.m_SortingLayerRange = value;
			}
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00037CC0 File Offset: 0x00035EC0
		public bool Equals(FilteringSettings other)
		{
			return this.m_RenderQueueRange.Equals(other.m_RenderQueueRange) && this.m_LayerMask == other.m_LayerMask && this.m_RenderingLayerMask == other.m_RenderingLayerMask && this.m_BatchLayerMask == other.m_BatchLayerMask && this.m_ExcludeMotionVectorObjects == other.m_ExcludeMotionVectorObjects && this.m_ForceAllMotionVectorObjects == other.m_ForceAllMotionVectorObjects;
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00037D30 File Offset: 0x00035F30
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is FilteringSettings && this.Equals((FilteringSettings)obj);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00037D68 File Offset: 0x00035F68
		public override int GetHashCode()
		{
			int hashCode = this.m_RenderQueueRange.GetHashCode();
			hashCode = (hashCode * 397) ^ this.m_LayerMask;
			hashCode = (hashCode * 397) ^ (int)this.m_RenderingLayerMask;
			hashCode = (hashCode * 397) ^ (int)this.m_BatchLayerMask;
			hashCode = (hashCode * 397) ^ this.m_ExcludeMotionVectorObjects;
			return (hashCode * 397) ^ this.m_ForceAllMotionVectorObjects;
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00037DDC File Offset: 0x00035FDC
		public static bool operator ==(FilteringSettings left, FilteringSettings right)
		{
			return left.Equals(right);
		}

		// Token: 0x04000BFE RID: 3070
		private RenderQueueRange m_RenderQueueRange;

		// Token: 0x04000BFF RID: 3071
		private int m_LayerMask;

		// Token: 0x04000C00 RID: 3072
		private uint m_RenderingLayerMask;

		// Token: 0x04000C01 RID: 3073
		private uint m_BatchLayerMask;

		// Token: 0x04000C02 RID: 3074
		private int m_ExcludeMotionVectorObjects;

		// Token: 0x04000C03 RID: 3075
		private int m_ForceAllMotionVectorObjects;

		// Token: 0x04000C04 RID: 3076
		private SortingLayerRange m_SortingLayerRange;
	}
}
