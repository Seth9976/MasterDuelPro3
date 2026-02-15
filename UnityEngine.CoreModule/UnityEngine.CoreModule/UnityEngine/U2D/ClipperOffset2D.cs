using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	// Token: 0x02000409 RID: 1033
	[NativeType(Header = "Runtime/2D/Common/ClipperOffsetWrapper.h")]
	internal struct ClipperOffset2D
	{
		// Token: 0x06001B87 RID: 7047 RVA: 0x0003CE90 File Offset: 0x0003B090
		public unsafe static void Execute(ref ClipperOffset2D.Solution solution, NativeArray<Vector2> inPoints, NativeArray<int> inPathSizes, NativeArray<ClipperOffset2D.PathArguments> inPathArguments, Allocator inSolutionAllocator, double inDelta = 0.0, double inMiterLimit = 2.0, double inRoundPrecision = 0.25, double inArcTolerance = 0.0, double inIntScale = 65536.0, bool useRounding = false)
		{
			IntPtr clipperPoints;
			int clipperPointCount;
			IntPtr clipperPathSizes;
			int clipperPathCount;
			ClipperOffset2D.Internal_Execute(out clipperPoints, out clipperPointCount, out clipperPathSizes, out clipperPathCount, new IntPtr(inPoints.m_Buffer), inPoints.Length, new IntPtr(inPathSizes.m_Buffer), new IntPtr(inPathArguments.m_Buffer), inPathSizes.Length, inDelta, inMiterLimit, inRoundPrecision, inArcTolerance, inIntScale, useRounding);
			bool flag = !solution.pathSizes.IsCreated;
			if (flag)
			{
				solution.pathSizes = new NativeArray<int>(clipperPathCount, inSolutionAllocator, NativeArrayOptions.ClearMemory);
			}
			bool flag2 = !solution.points.IsCreated;
			if (flag2)
			{
				solution.points = new NativeArray<Vector2>(clipperPointCount, inSolutionAllocator, NativeArrayOptions.ClearMemory);
			}
			bool flag3 = solution.points.Length >= clipperPointCount && solution.pathSizes.Length >= clipperPathCount;
			if (flag3)
			{
				UnsafeUtility.MemCpy(solution.points.m_Buffer, clipperPoints.ToPointer(), (long)(clipperPointCount * sizeof(Vector2)));
				UnsafeUtility.MemCpy(solution.pathSizes.m_Buffer, clipperPathSizes.ToPointer(), (long)(clipperPathCount * 4));
				ClipperOffset2D.Internal_Execute_Cleanup(clipperPoints, clipperPathSizes);
				return;
			}
			ClipperOffset2D.Internal_Execute_Cleanup(clipperPoints, clipperPathSizes);
			throw new IndexOutOfRangeException();
		}

		// Token: 0x06001B88 RID: 7048
		[NativeMethod(Name = "ClipperOffset2D::Execute", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Execute(out IntPtr outClippedPoints, out int outClippedPointsCount, out IntPtr outClippedPathSizes, out int outClippedPathCount, IntPtr inPoints, int inPointCount, IntPtr inPathSizes, IntPtr inPathArguments, int inPathCount, double inDelta, double inMiterLimit, double inRoundPrecision, double inArcTolerance, double inIntScale, bool useRounding);

		// Token: 0x06001B89 RID: 7049
		[NativeMethod(Name = "ClipperOffset2D::Execute_Cleanup", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Execute_Cleanup(IntPtr inPoints, IntPtr inPathSizes);

		// Token: 0x0200040A RID: 1034
		public enum JoinType
		{
			// Token: 0x04000E77 RID: 3703
			jtSquare,
			// Token: 0x04000E78 RID: 3704
			jtRound,
			// Token: 0x04000E79 RID: 3705
			jtMiter
		}

		// Token: 0x0200040B RID: 1035
		public enum EndType
		{
			// Token: 0x04000E7B RID: 3707
			etClosedPolygon,
			// Token: 0x04000E7C RID: 3708
			etClosedLine,
			// Token: 0x04000E7D RID: 3709
			etOpenButt,
			// Token: 0x04000E7E RID: 3710
			etOpenSquare,
			// Token: 0x04000E7F RID: 3711
			etOpenRound
		}

		// Token: 0x0200040C RID: 1036
		[NativeType(Header = "Runtime/2D/Common/ClipperOffsetWrapper.h")]
		public struct PathArguments
		{
			// Token: 0x04000E80 RID: 3712
			public ClipperOffset2D.JoinType joinType;

			// Token: 0x04000E81 RID: 3713
			public ClipperOffset2D.EndType endType;
		}

		// Token: 0x0200040D RID: 1037
		public struct Solution
		{
			// Token: 0x06001B8A RID: 7050 RVA: 0x0003CFB0 File Offset: 0x0003B1B0
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
			}

			// Token: 0x04000E82 RID: 3714
			public NativeArray<Vector2> points;

			// Token: 0x04000E83 RID: 3715
			public NativeArray<int> pathSizes;
		}
	}
}
