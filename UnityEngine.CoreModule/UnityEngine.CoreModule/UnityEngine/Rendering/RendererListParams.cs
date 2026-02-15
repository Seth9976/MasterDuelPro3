using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x020003BA RID: 954
	public struct RendererListParams : IEquatable<RendererListParams>
	{
		// Token: 0x060019B2 RID: 6578 RVA: 0x00038110 File Offset: 0x00036310
		public RendererListParams(CullingResults cullingResults, DrawingSettings drawSettings, FilteringSettings filteringSettings)
		{
			this.cullingResults = cullingResults;
			this.drawSettings = drawSettings;
			this.filteringSettings = filteringSettings;
			this.tagName = ShaderTagId.none;
			this.isPassTagName = false;
			this.tagValues = null;
			this.stateBlocks = null;
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x00038160 File Offset: 0x00036360
		internal int numStateBlocks
		{
			get
			{
				bool flag = this.tagValues != null;
				int num;
				if (flag)
				{
					num = this.tagValues.Value.Length;
				}
				else
				{
					num = 0;
				}
				return num;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x00038198 File Offset: 0x00036398
		internal IntPtr stateBlocksPtr
		{
			get
			{
				bool flag = this.stateBlocks == null;
				IntPtr intPtr;
				if (flag)
				{
					intPtr = IntPtr.Zero;
				}
				else
				{
					intPtr = (IntPtr)this.stateBlocks.Value.GetUnsafeReadOnlyPtr<RenderStateBlock>();
				}
				return intPtr;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x000381DC File Offset: 0x000363DC
		internal IntPtr tagsValuePtr
		{
			get
			{
				bool flag = this.tagValues == null;
				IntPtr intPtr;
				if (flag)
				{
					intPtr = IntPtr.Zero;
				}
				else
				{
					intPtr = (IntPtr)this.tagValues.Value.GetUnsafeReadOnlyPtr<ShaderTagId>();
				}
				return intPtr;
			}
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x00038220 File Offset: 0x00036420
		internal void Validate()
		{
			bool flag = this.tagValues != null && this.stateBlocks != null;
			if (flag)
			{
				bool flag2 = this.tagValues.Value.Length != this.stateBlocks.Value.Length;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Arrays {0} and {1} should have same length, but {2} had length {3} while {4} had length {5}.", new object[]
					{
						"tagValues",
						"stateBlocks",
						"tagValues",
						this.tagValues.Value.Length,
						"stateBlocks",
						this.stateBlocks.Value.Length
					}));
				}
			}
			else
			{
				bool flag3 = (this.tagValues != null && this.stateBlocks == null) || (this.tagValues == null && this.stateBlocks != null);
				if (flag3)
				{
					throw new ArgumentException(string.Format("Arrays {0} and {1} should have same length, but one of them is null ({2} : {3}, {4} : {5}).", new object[]
					{
						"tagValues",
						"stateBlocks",
						"tagValues",
						this.tagValues != null,
						"stateBlocks",
						this.stateBlocks != null
					}));
				}
			}
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x00038390 File Offset: 0x00036590
		public bool Equals(RendererListParams other)
		{
			return this.cullingResults == other.cullingResults && this.drawSettings == other.drawSettings && this.filteringSettings == other.filteringSettings && this.tagName == other.tagName && this.isPassTagName == other.isPassTagName && this.tagValues == other.tagValues && this.stateBlocks == other.stateBlocks;
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x00038494 File Offset: 0x00036694
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RendererListParams && this.Equals((RendererListParams)obj);
		}

		// Token: 0x060019B9 RID: 6585 RVA: 0x000384CC File Offset: 0x000366CC
		public override int GetHashCode()
		{
			int hashCode = this.cullingResults.GetHashCode();
			hashCode = (hashCode * 397) ^ this.drawSettings.GetHashCode();
			hashCode = (hashCode * 397) ^ this.filteringSettings.GetHashCode();
			hashCode = (hashCode * 397) ^ this.tagName.GetHashCode();
			hashCode = (hashCode * 397) ^ (this.isPassTagName ? 0 : 1);
			hashCode = (hashCode * 397) ^ this.tagValues.GetHashCode();
			return (hashCode * 397) ^ this.stateBlocks.GetHashCode();
		}

		// Token: 0x04000C33 RID: 3123
		public static readonly RendererListParams Invalid = default(RendererListParams);

		// Token: 0x04000C34 RID: 3124
		public CullingResults cullingResults;

		// Token: 0x04000C35 RID: 3125
		public DrawingSettings drawSettings;

		// Token: 0x04000C36 RID: 3126
		public FilteringSettings filteringSettings;

		// Token: 0x04000C37 RID: 3127
		public ShaderTagId tagName;

		// Token: 0x04000C38 RID: 3128
		public bool isPassTagName;

		// Token: 0x04000C39 RID: 3129
		public NativeArray<ShaderTagId>? tagValues;

		// Token: 0x04000C3A RID: 3130
		public NativeArray<RenderStateBlock>? stateBlocks;
	}
}
