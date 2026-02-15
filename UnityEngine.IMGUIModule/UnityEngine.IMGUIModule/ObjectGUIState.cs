using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000025 RID: 37
	[NativeHeader("Modules/IMGUI/GUIState.h")]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal class ObjectGUIState : IDisposable
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00008B07 File Offset: 0x00006D07
		public ObjectGUIState()
		{
			this.m_Ptr = ObjectGUIState.Internal_Create();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00008B1C File Offset: 0x00006D1C
		public void Dispose()
		{
			this.Destroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008B30 File Offset: 0x00006D30
		~ObjectGUIState()
		{
			this.Destroy();
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008B60 File Offset: 0x00006D60
		private void Destroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				ObjectGUIState.Internal_Destroy(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x060001C5 RID: 453
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create();

		// Token: 0x060001C6 RID: 454
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x040000F6 RID: 246
		internal IntPtr m_Ptr;

		// Token: 0x02000026 RID: 38
		internal static class BindingsMarshaller
		{
			// Token: 0x060001C7 RID: 455 RVA: 0x00008B9B File Offset: 0x00006D9B
			public static IntPtr ConvertToNative(ObjectGUIState objectGUIState)
			{
				return objectGUIState.m_Ptr;
			}
		}
	}
}
