using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	// Token: 0x02000401 RID: 1025
	[NativeType(Header = "Runtime/2D/Common/ClipperWrapper.h")]
	internal struct Clipper2D
	{
		// Token: 0x06001B80 RID: 7040 RVA: 0x0003CC18 File Offset: 0x0003AE18
		public unsafe static void Execute(ref Clipper2D.Solution solution, NativeArray<Vector2> inPoints, NativeArray<int> inPathSizes, NativeArray<Clipper2D.PathArguments> inPathArguments, Clipper2D.ExecuteArguments inExecuteArguments, Allocator inSolutionAllocator, int inIntScale = 65536, bool useRounding = false)
		{
			bool flag = !solution.boundingRect.IsCreated;
			if (flag)
			{
				solution.boundingRect = new NativeArray<Rect>(1, inSolutionAllocator, NativeArrayOptions.ClearMemory);
			}
			IntPtr clipperPoints;
			int clipperPointCount;
			IntPtr clipperPathSizes;
			int clipperPathCount;
			solution.boundingRect[0] = Clipper2D.Internal_Execute(out clipperPoints, out clipperPointCount, out clipperPathSizes, out clipperPathCount, new IntPtr(inPoints.m_Buffer), inPoints.Length, new IntPtr(inPathSizes.m_Buffer), new IntPtr(inPathArguments.m_Buffer), inPathSizes.Length, inExecuteArguments, (float)inIntScale, useRounding);
			bool flag2 = clipperPointCount > 0;
			if (flag2)
			{
				bool flag3 = !solution.pathSizes.IsCreated;
				if (flag3)
				{
					solution.pathSizes = new NativeArray<int>(clipperPathCount, inSolutionAllocator, NativeArrayOptions.ClearMemory);
				}
				bool flag4 = !solution.points.IsCreated;
				if (flag4)
				{
					solution.points = new NativeArray<Vector2>(clipperPointCount, inSolutionAllocator, NativeArrayOptions.ClearMemory);
				}
				bool flag5 = solution.points.Length >= clipperPointCount && solution.pathSizes.Length >= clipperPathCount;
				if (!flag5)
				{
					Clipper2D.Internal_Execute_Cleanup(clipperPoints, clipperPathSizes);
					throw new IndexOutOfRangeException();
				}
				UnsafeUtility.MemCpy(solution.points.m_Buffer, clipperPoints.ToPointer(), (long)(clipperPointCount * sizeof(Vector2)));
				UnsafeUtility.MemCpy(solution.pathSizes.m_Buffer, clipperPathSizes.ToPointer(), (long)(clipperPathCount * 4));
				Clipper2D.Internal_Execute_Cleanup(clipperPoints, clipperPathSizes);
			}
			else
			{
				bool flag6 = !solution.pathSizes.IsCreated;
				if (flag6)
				{
					solution.points = new NativeArray<Vector2>(0, inSolutionAllocator, NativeArrayOptions.ClearMemory);
				}
				bool flag7 = !solution.points.IsCreated;
				if (flag7)
				{
					solution.pathSizes = new NativeArray<int>(0, inSolutionAllocator, NativeArrayOptions.ClearMemory);
				}
			}
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x0003CDBC File Offset: 0x0003AFBC
		[NativeMethod(Name = "Clipper2D::Execute", IsFreeFunction = true, IsThreadSafe = true)]
		private static Rect Internal_Execute(out IntPtr outClippedPoints, out int outClippedPointsCount, out IntPtr outClippedPathSizes, out int outClippedPathCount, IntPtr inPoints, int inPointCount, IntPtr inPathSizes, IntPtr inPathArguments, int inPathCount, Clipper2D.ExecuteArguments inExecuteArguments, float inIntScale, bool useRounding)
		{
			Rect rect;
			Clipper2D.Internal_Execute_Injected(out outClippedPoints, out outClippedPointsCount, out outClippedPathSizes, out outClippedPathCount, inPoints, inPointCount, inPathSizes, inPathArguments, inPathCount, ref inExecuteArguments, inIntScale, useRounding, out rect);
			return rect;
		}

		// Token: 0x06001B82 RID: 7042
		[NativeMethod(Name = "Clipper2D::Execute_Cleanup", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Execute_Cleanup(IntPtr inPoints, IntPtr inPathSizes);

		// Token: 0x06001B83 RID: 7043
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Execute_Injected(out IntPtr outClippedPoints, out int outClippedPointsCount, out IntPtr outClippedPathSizes, out int outClippedPathCount, IntPtr inPoints, int inPointCount, IntPtr inPathSizes, IntPtr inPathArguments, int inPathCount, [In] ref Clipper2D.ExecuteArguments inExecuteArguments, float inIntScale, bool useRounding, out Rect ret);

		// Token: 0x02000402 RID: 1026
		public enum ClipType
		{
			// Token: 0x04000E59 RID: 3673
			ctIntersection,
			// Token: 0x04000E5A RID: 3674
			ctUnion,
			// Token: 0x04000E5B RID: 3675
			ctDifference,
			// Token: 0x04000E5C RID: 3676
			ctXor
		}

		// Token: 0x02000403 RID: 1027
		public enum PolyType
		{
			// Token: 0x04000E5E RID: 3678
			ptSubject,
			// Token: 0x04000E5F RID: 3679
			ptClip
		}

		// Token: 0x02000404 RID: 1028
		public enum PolyFillType
		{
			// Token: 0x04000E61 RID: 3681
			pftEvenOdd,
			// Token: 0x04000E62 RID: 3682
			pftNonZero,
			// Token: 0x04000E63 RID: 3683
			pftPositive,
			// Token: 0x04000E64 RID: 3684
			pftNegative
		}

		// Token: 0x02000405 RID: 1029
		public enum InitOptions
		{
			// Token: 0x04000E66 RID: 3686
			ioDefault,
			// Token: 0x04000E67 RID: 3687
			oReverseSolution,
			// Token: 0x04000E68 RID: 3688
			ioStrictlySimple,
			// Token: 0x04000E69 RID: 3689
			ioPreserveCollinear = 4
		}

		// Token: 0x02000406 RID: 1030
		[NativeType(Header = "Runtime/2D/Common/ClipperWrapper.h")]
		public struct PathArguments
		{
			// Token: 0x06001B84 RID: 7044 RVA: 0x0003CDE5 File Offset: 0x0003AFE5
			public PathArguments(Clipper2D.PolyType inPolyType = Clipper2D.PolyType.ptSubject, bool inClosed = false)
			{
				this.polyType = inPolyType;
				this.closed = inClosed;
			}

			// Token: 0x04000E6A RID: 3690
			public Clipper2D.PolyType polyType;

			// Token: 0x04000E6B RID: 3691
			public bool closed;
		}

		// Token: 0x02000407 RID: 1031
		[NativeType(Header = "Runtime/2D/Common/ClipperWrapper.h")]
		public struct ExecuteArguments
		{
			// Token: 0x06001B85 RID: 7045 RVA: 0x0003CDF6 File Offset: 0x0003AFF6
			public ExecuteArguments(Clipper2D.InitOptions inInitOption = Clipper2D.InitOptions.ioDefault, Clipper2D.ClipType inClipType = Clipper2D.ClipType.ctIntersection, Clipper2D.PolyFillType inSubjFillType = Clipper2D.PolyFillType.pftEvenOdd, Clipper2D.PolyFillType inClipFillType = Clipper2D.PolyFillType.pftEvenOdd, bool inReverseSolution = false, bool inStrictlySimple = false, bool inPreserveColinear = false)
			{
				this.initOption = inInitOption;
				this.clipType = inClipType;
				this.subjFillType = inSubjFillType;
				this.clipFillType = inClipFillType;
				this.reverseSolution = inReverseSolution;
				this.strictlySimple = inStrictlySimple;
				this.preserveColinear = inPreserveColinear;
			}

			// Token: 0x04000E6C RID: 3692
			public Clipper2D.InitOptions initOption;

			// Token: 0x04000E6D RID: 3693
			public Clipper2D.ClipType clipType;

			// Token: 0x04000E6E RID: 3694
			public Clipper2D.PolyFillType subjFillType;

			// Token: 0x04000E6F RID: 3695
			public Clipper2D.PolyFillType clipFillType;

			// Token: 0x04000E70 RID: 3696
			public bool reverseSolution;

			// Token: 0x04000E71 RID: 3697
			public bool strictlySimple;

			// Token: 0x04000E72 RID: 3698
			public bool preserveColinear;
		}

		// Token: 0x02000408 RID: 1032
		public struct Solution : IDisposable
		{
			// Token: 0x06001B86 RID: 7046 RVA: 0x0003CE30 File Offset: 0x0003B030
			public void Dispose()
			{
				bool isCreated = this.points.IsCreated;
				if (isCreated)
				{
					this.points.Dispose();
				}
				bool isCreated2 = this.pathSizes.IsCreated;
				if (isCreated2)
				{
					this.pathSizes.Dispose();
				}
				bool isCreated3 = this.boundingRect.IsCreated;
				if (isCreated3)
				{
					this.boundingRect.Dispose();
				}
			}

			// Token: 0x04000E73 RID: 3699
			public NativeArray<Vector2> points;

			// Token: 0x04000E74 RID: 3700
			public NativeArray<int> pathSizes;

			// Token: 0x04000E75 RID: 3701
			public NativeArray<Rect> boundingRect;
		}
	}
}
