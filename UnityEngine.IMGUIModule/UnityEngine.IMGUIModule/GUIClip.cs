using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200000A RID: 10
	[NativeHeader("Modules/IMGUI/GUIClip.h")]
	[NativeHeader("Modules/IMGUI/GUIState.h")]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEditor.UIBuilderModule" })]
	internal sealed class GUIClip
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003EBC File Offset: 0x000020BC
		internal static Rect visibleRect
		{
			[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetVisibleRect")]
			get
			{
				Rect rect;
				GUIClip.get_visibleRect_Injected(out rect);
				return rect;
			}
		}

		// Token: 0x06000081 RID: 129
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Pop();

		// Token: 0x06000082 RID: 130
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Internal_GetCount();

		// Token: 0x06000083 RID: 131 RVA: 0x00003ED4 File Offset: 0x000020D4
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetUserMatrix")]
		internal static Matrix4x4 GetMatrix()
		{
			Matrix4x4 matrix4x;
			GUIClip.GetMatrix_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003EEC File Offset: 0x000020EC
		internal static void SetMatrix(Matrix4x4 m)
		{
			GUIClip.SetMatrix_Injected(ref m);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003F00 File Offset: 0x00002100
		internal static void Internal_PushParentClip(Matrix4x4 objectTransform, Rect clipRect)
		{
			GUIClip.Internal_PushParentClip(objectTransform, objectTransform, clipRect);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003F0C File Offset: 0x0000210C
		internal static void Internal_PushParentClip(Matrix4x4 renderTransform, Matrix4x4 inputTransform, Rect clipRect)
		{
			GUIClip.Internal_PushParentClip_Injected(ref renderTransform, ref inputTransform, ref clipRect);
		}

		// Token: 0x06000087 RID: 135
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_PopParentClip();

		// Token: 0x06000088 RID: 136
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_visibleRect_Injected(out Rect ret);

		// Token: 0x06000089 RID: 137
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x0600008A RID: 138
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrix_Injected([In] ref Matrix4x4 m);

		// Token: 0x0600008B RID: 139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_PushParentClip_Injected([In] ref Matrix4x4 renderTransform, [In] ref Matrix4x4 inputTransform, [In] ref Rect clipRect);

		// Token: 0x0200000B RID: 11
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEditor.UIBuilderModule" })]
		internal struct ParentClipScope : IDisposable
		{
			// Token: 0x0600008C RID: 140 RVA: 0x00003F24 File Offset: 0x00002124
			public ParentClipScope(Matrix4x4 objectTransform, Rect clipRect)
			{
				this.m_Disposed = false;
				GUIClip.Internal_PushParentClip(objectTransform, clipRect);
			}

			// Token: 0x0600008D RID: 141 RVA: 0x00003F38 File Offset: 0x00002138
			public void Dispose()
			{
				bool disposed = this.m_Disposed;
				if (!disposed)
				{
					this.m_Disposed = true;
					GUIClip.Internal_PopParentClip();
				}
			}

			// Token: 0x0400004A RID: 74
			private bool m_Disposed;
		}
	}
}
