using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x0200057D RID: 1405
	internal struct LayoutComputedData
	{
		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x0009A410 File Offset: 0x00098610
		public unsafe static LayoutComputedData Default
		{
			get
			{
				LayoutComputedData r = new LayoutComputedData
				{
					Direction = LayoutDirection.Inherit,
					ComputedFlexBasisGeneration = 0U,
					ComputedFlexBasis = float.NaN,
					HadOverflow = false,
					GenerationCount = 0U,
					LastParentDirection = (LayoutDirection)(-1),
					LastPointScaleFactor = 1f
				};
				r.Dimensions.FixedElementField = LayoutDefaults.DimensionValues[0];
				*((ref r.Dimensions.FixedElementField) + 4) = LayoutDefaults.DimensionValues[1];
				r.MeasuredDimensions.FixedElementField = LayoutDefaults.DimensionValues[0];
				*((ref r.MeasuredDimensions.FixedElementField) + 4) = LayoutDefaults.DimensionValues[1];
				return r;
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x0009A4C4 File Offset: 0x000986C4
		public unsafe float* MarginBuffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				fixed (float* ptr = &this.Margin.FixedElementField)
				{
					return ptr;
				}
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060026C1 RID: 9921 RVA: 0x0009A4E8 File Offset: 0x000986E8
		public unsafe float* BorderBuffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				fixed (float* ptr = &this.Border.FixedElementField)
				{
					return ptr;
				}
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x0009A50C File Offset: 0x0009870C
		public unsafe float* PaddingBuffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				fixed (float* ptr = &this.Padding.FixedElementField)
				{
					return ptr;
				}
			}
		}

		// Token: 0x0400138F RID: 5007
		[FixedBuffer(typeof(float), 4)]
		public LayoutComputedData.<Position>e__FixedBuffer Position;

		// Token: 0x04001390 RID: 5008
		[FixedBuffer(typeof(float), 2)]
		public LayoutComputedData.<Dimensions>e__FixedBuffer Dimensions;

		// Token: 0x04001391 RID: 5009
		[FixedBuffer(typeof(float), 6)]
		public LayoutComputedData.<Margin>e__FixedBuffer Margin;

		// Token: 0x04001392 RID: 5010
		[FixedBuffer(typeof(float), 6)]
		public LayoutComputedData.<Border>e__FixedBuffer Border;

		// Token: 0x04001393 RID: 5011
		[FixedBuffer(typeof(float), 6)]
		public LayoutComputedData.<Padding>e__FixedBuffer Padding;

		// Token: 0x04001394 RID: 5012
		public LayoutDirection Direction;

		// Token: 0x04001395 RID: 5013
		public uint ComputedFlexBasisGeneration;

		// Token: 0x04001396 RID: 5014
		public float ComputedFlexBasis;

		// Token: 0x04001397 RID: 5015
		public bool HadOverflow;

		// Token: 0x04001398 RID: 5016
		public uint GenerationCount;

		// Token: 0x04001399 RID: 5017
		public LayoutDirection LastParentDirection;

		// Token: 0x0400139A RID: 5018
		public float LastPointScaleFactor;

		// Token: 0x0400139B RID: 5019
		[FixedBuffer(typeof(float), 2)]
		public LayoutComputedData.<MeasuredDimensions>e__FixedBuffer MeasuredDimensions;

		// Token: 0x0200057E RID: 1406
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 24)]
		public struct <Border>e__FixedBuffer
		{
			// Token: 0x0400139C RID: 5020
			public float FixedElementField;
		}

		// Token: 0x0200057F RID: 1407
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 8)]
		public struct <Dimensions>e__FixedBuffer
		{
			// Token: 0x0400139D RID: 5021
			public float FixedElementField;
		}

		// Token: 0x02000580 RID: 1408
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 24)]
		public struct <Margin>e__FixedBuffer
		{
			// Token: 0x0400139E RID: 5022
			public float FixedElementField;
		}

		// Token: 0x02000581 RID: 1409
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 8)]
		public struct <MeasuredDimensions>e__FixedBuffer
		{
			// Token: 0x0400139F RID: 5023
			public float FixedElementField;
		}

		// Token: 0x02000582 RID: 1410
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 24)]
		public struct <Padding>e__FixedBuffer
		{
			// Token: 0x040013A0 RID: 5024
			public float FixedElementField;
		}

		// Token: 0x02000583 RID: 1411
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 16)]
		public struct <Position>e__FixedBuffer
		{
			// Token: 0x040013A1 RID: 5025
			public float FixedElementField;
		}
	}
}
