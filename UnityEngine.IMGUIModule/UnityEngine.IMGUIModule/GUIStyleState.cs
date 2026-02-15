using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000017 RID: 23
	[NativeHeader("Modules/IMGUI/GUIStyle.bindings.h")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class GUIStyleState
	{
		// Token: 0x1700004B RID: 75
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x0000581C File Offset: 0x00003A1C
		[NativeProperty("textColor", false, TargetType.Field)]
		public Color textColor
		{
			set
			{
				IntPtr intPtr = GUIStyleState.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GUIStyleState.set_textColor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x060000F7 RID: 247
		[FreeFunction(Name = "GUIStyleState_Bindings::Init", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Init();

		// Token: 0x060000F8 RID: 248 RVA: 0x00005840 File Offset: 0x00003A40
		[FreeFunction(Name = "GUIStyleState_Bindings::Cleanup", IsThreadSafe = true, HasExplicitThis = true)]
		private void Cleanup()
		{
			IntPtr intPtr = GUIStyleState.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GUIStyleState.Cleanup_Injected(intPtr);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005862 File Offset: 0x00003A62
		public GUIStyleState()
		{
			this.m_Ptr = GUIStyleState.Init();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005877 File Offset: 0x00003A77
		private GUIStyleState(GUIStyle sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005890 File Offset: 0x00003A90
		internal static GUIStyleState GetGUIStyleState(GUIStyle sourceStyle, IntPtr source)
		{
			return new GUIStyleState(sourceStyle, source);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000058AC File Offset: 0x00003AAC
		protected override void Finalize()
		{
			try
			{
				bool flag = this.m_SourceStyle == null;
				if (flag)
				{
					this.Cleanup();
					this.m_Ptr = IntPtr.Zero;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x060000FD RID: 253
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_textColor_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x060000FE RID: 254
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Cleanup_Injected(IntPtr _unity_self);

		// Token: 0x04000099 RID: 153
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x0400009A RID: 154
		private readonly GUIStyle m_SourceStyle;

		// Token: 0x02000018 RID: 24
		internal static class BindingsMarshaller
		{
			// Token: 0x060000FF RID: 255 RVA: 0x000058F8 File Offset: 0x00003AF8
			public static IntPtr ConvertToNative(GUIStyleState guiStyleState)
			{
				return guiStyleState.m_Ptr;
			}
		}
	}
}
