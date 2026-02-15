using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000CD RID: 205
	[StaticAccessor("GizmoBindings", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Export/Gizmos/Gizmos.bindings.h")]
	public sealed class Gizmos
	{
		// Token: 0x0600054B RID: 1355 RVA: 0x0000BC1C File Offset: 0x00009E1C
		[NativeThrows]
		public static void DrawLine(Vector3 from, Vector3 to)
		{
			Gizmos.DrawLine_Injected(ref from, ref to);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000BC34 File Offset: 0x00009E34
		[NativeThrows]
		public static void DrawWireSphere(Vector3 center, float radius)
		{
			Gizmos.DrawWireSphere_Injected(ref center, radius);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0000BC4C File Offset: 0x00009E4C
		[NativeThrows]
		public static void DrawSphere(Vector3 center, float radius)
		{
			Gizmos.DrawSphere_Injected(ref center, radius);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0000BC64 File Offset: 0x00009E64
		[NativeThrows]
		public unsafe static void DrawIcon(Vector3 center, string name, [DefaultValue("true")] bool allowScaling, [DefaultValue("Color(255,255,255,255)")] Color tint)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Gizmos.DrawIcon_Injected(ref center, ref managedSpanWrapper, allowScaling, ref tint);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x170000ED RID: 237
		// (set) Token: 0x0600054F RID: 1359 RVA: 0x0000BCC0 File Offset: 0x00009EC0
		public static Color color
		{
			set
			{
				Gizmos.set_color_Injected(ref value);
			}
		}

		// Token: 0x170000EE RID: 238
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0000BCD4 File Offset: 0x00009ED4
		public static Matrix4x4 matrix
		{
			set
			{
				Gizmos.set_matrix_Injected(ref value);
			}
		}

		// Token: 0x06000551 RID: 1361
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawLine_Injected([In] ref Vector3 from, [In] ref Vector3 to);

		// Token: 0x06000552 RID: 1362
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawWireSphere_Injected([In] ref Vector3 center, float radius);

		// Token: 0x06000553 RID: 1363
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawSphere_Injected([In] ref Vector3 center, float radius);

		// Token: 0x06000554 RID: 1364
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawIcon_Injected([In] ref Vector3 center, ref ManagedSpanWrapper name, [DefaultValue("true")] bool allowScaling, [DefaultValue("Color(255,255,255,255)")] [In] ref Color tint);

		// Token: 0x06000555 RID: 1365
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_color_Injected([In] ref Color value);

		// Token: 0x06000556 RID: 1366
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_matrix_Injected([In] ref Matrix4x4 value);
	}
}
