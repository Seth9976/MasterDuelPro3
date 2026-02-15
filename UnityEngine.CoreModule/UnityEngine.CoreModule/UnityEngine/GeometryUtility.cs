using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000C5 RID: 197
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[StaticAccessor("GeometryUtilityScripting", StaticAccessorType.DoubleColon)]
	public sealed class GeometryUtility
	{
		// Token: 0x060004D8 RID: 1240 RVA: 0x0000AADB File Offset: 0x00008CDB
		public static void CalculateFrustumPlanes(Camera camera, Plane[] planes)
		{
			GeometryUtility.CalculateFrustumPlanes(camera.projectionMatrix * camera.worldToCameraMatrix, planes);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000AAF8 File Offset: 0x00008CF8
		public static void CalculateFrustumPlanes(Matrix4x4 worldToProjectionMatrix, Plane[] planes)
		{
			bool flag = planes == null;
			if (flag)
			{
				throw new ArgumentNullException("planes");
			}
			bool flag2 = planes.Length != 6;
			if (flag2)
			{
				throw new ArgumentException("Planes array must be of length 6.", "planes");
			}
			GeometryUtility.Internal_ExtractPlanes(planes, worldToProjectionMatrix);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0000AB40 File Offset: 0x00008D40
		public unsafe static bool TestPlanesAABB(Plane[] planes, Bounds bounds)
		{
			Span<Plane> span = new Span<Plane>(planes);
			bool flag;
			fixed (Plane* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				flag = GeometryUtility.TestPlanesAABB_Injected(ref managedSpanWrapper, ref bounds);
			}
			return flag;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0000AB7C File Offset: 0x00008D7C
		[NativeName("ExtractPlanes")]
		private unsafe static void Internal_ExtractPlanes([Out] Plane[] planes, Matrix4x4 worldToProjectionMatrix)
		{
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				if (planes != null)
				{
					fixed (Plane[] array = planes)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				GeometryUtility.Internal_ExtractPlanes_Injected(out blittableArrayWrapper, ref worldToProjectionMatrix);
			}
			finally
			{
				Plane[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Plane>(ref array);
			}
		}

		// Token: 0x060004DC RID: 1244
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool TestPlanesAABB_Injected(ref ManagedSpanWrapper planes, [In] ref Bounds bounds);

		// Token: 0x060004DD RID: 1245
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ExtractPlanes_Injected(out BlittableArrayWrapper planes, [In] ref Matrix4x4 worldToProjectionMatrix);
	}
}
