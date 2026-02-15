using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x020000AE RID: 174
	internal struct IntersectionCompare : IComparer<int2>
	{
		// Token: 0x060003DF RID: 991 RVA: 0x0001C984 File Offset: 0x0001AB84
		public unsafe int Compare(int2 a, int2 b)
		{
			int2 e1a = this.edges[a.x];
			int2 e1b = this.edges[a.y];
			int2 e2a = this.edges[b.x];
			int2 e2b = this.edges[b.y];
			this.xvasort.FixedElementField = this.points[e1a.x].x;
			*((ref this.xvasort.FixedElementField) + 8) = this.points[e1a.y].x;
			*((ref this.xvasort.FixedElementField) + (IntPtr)2 * 8) = this.points[e1b.x].x;
			*((ref this.xvasort.FixedElementField) + (IntPtr)3 * 8) = this.points[e1b.y].x;
			this.xvbsort.FixedElementField = this.points[e2a.x].x;
			*((ref this.xvbsort.FixedElementField) + 8) = this.points[e2a.y].x;
			*((ref this.xvbsort.FixedElementField) + (IntPtr)2 * 8) = this.points[e2b.x].x;
			*((ref this.xvbsort.FixedElementField) + (IntPtr)3 * 8) = this.points[e2b.y].x;
			fixed (double* ptr = &this.xvasort.FixedElementField)
			{
				ModuleHandle.InsertionSort<double, XCompare>((void*)ptr, 0, 3, default(XCompare));
			}
			fixed (double* ptr = &this.xvbsort.FixedElementField)
			{
				ModuleHandle.InsertionSort<double, XCompare>((void*)ptr, 0, 3, default(XCompare));
			}
			int i = 0;
			while (i < 4)
			{
				if (*((ref this.xvasort.FixedElementField) + (IntPtr)i * 8) - *((ref this.xvbsort.FixedElementField) + (IntPtr)i * 8) != 0.0)
				{
					if (*((ref this.xvasort.FixedElementField) + (IntPtr)i * 8) >= *((ref this.xvbsort.FixedElementField) + (IntPtr)i * 8))
					{
						return 1;
					}
					return -1;
				}
				else
				{
					i++;
				}
			}
			if (this.points[e1a.x].y >= this.points[e1a.x].y)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x04000324 RID: 804
		public NativeArray<double2> points;

		// Token: 0x04000325 RID: 805
		public NativeArray<int2> edges;

		// Token: 0x04000326 RID: 806
		[FixedBuffer(typeof(double), 4)]
		public IntersectionCompare.<xvasort>e__FixedBuffer xvasort;

		// Token: 0x04000327 RID: 807
		[FixedBuffer(typeof(double), 4)]
		public IntersectionCompare.<xvbsort>e__FixedBuffer xvbsort;

		// Token: 0x020000AF RID: 175
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 32)]
		public struct <xvasort>e__FixedBuffer
		{
			// Token: 0x04000328 RID: 808
			public double FixedElementField;
		}

		// Token: 0x020000B0 RID: 176
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 32)]
		public struct <xvbsort>e__FixedBuffer
		{
			// Token: 0x04000329 RID: 809
			public double FixedElementField;
		}
	}
}
