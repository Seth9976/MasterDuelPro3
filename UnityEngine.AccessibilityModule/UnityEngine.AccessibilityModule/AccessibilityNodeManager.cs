using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000009 RID: 9
	[NativeHeader("Modules/Accessibility/Native/AccessibilityNodeManager.h")]
	internal static class AccessibilityNodeManager
	{
		// Token: 0x0600003A RID: 58
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DestroyNativeNode(int id, int parentId);

		// Token: 0x0600003B RID: 59 RVA: 0x00002764 File Offset: 0x00000964
		internal static void SetFrame(int id, Rect frame)
		{
			AccessibilityNodeManager.SetFrame_Injected(id, ref frame);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000277C File Offset: 0x0000097C
		internal unsafe static void SetChildren(int id, int[] childIds)
		{
			Span<int> span = new Span<int>(childIds);
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				AccessibilityNodeManager.SetChildren_Injected(id, ref managedSpanWrapper);
			}
		}

		// Token: 0x0600003D RID: 61
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetActions(int id, AccessibilityAction[] actions);

		// Token: 0x0600003E RID: 62 RVA: 0x000027B8 File Offset: 0x000009B8
		[RequiredByNativeCode]
		internal static void Internal_InvokeFocusChanged(int id, bool isNodeFocused)
		{
			AccessibilityHierarchyService service = AssistiveSupport.GetService<AccessibilityHierarchyService>();
			bool flag = service == null;
			if (!flag)
			{
				AccessibilityNode node;
				bool flag2 = service.TryGetNode(id, out node);
				if (flag2)
				{
					node.NotifyFocusChanged(isNodeFocused);
				}
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000027F0 File Offset: 0x000009F0
		[RequiredByNativeCode]
		internal static bool Internal_InvokeSelected(int id)
		{
			AccessibilityHierarchyService service = AssistiveSupport.GetService<AccessibilityHierarchyService>();
			bool flag = service == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				AccessibilityNode node;
				bool flag3 = service.TryGetNode(id, out node);
				flag2 = flag3 && node.InvokeSelected();
			}
			return flag2;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002830 File Offset: 0x00000A30
		[RequiredByNativeCode]
		internal static void Internal_InvokeIncremented(int id)
		{
			AccessibilityHierarchyService service = AssistiveSupport.GetService<AccessibilityHierarchyService>();
			bool flag = service == null;
			if (!flag)
			{
				AccessibilityNode node;
				bool flag2 = service.TryGetNode(id, out node);
				if (flag2)
				{
					node.InvokeIncremented();
				}
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002864 File Offset: 0x00000A64
		[RequiredByNativeCode]
		internal static void Internal_InvokeDecremented(int id)
		{
			AccessibilityHierarchyService service = AssistiveSupport.GetService<AccessibilityHierarchyService>();
			bool flag = service == null;
			if (!flag)
			{
				AccessibilityNode node;
				bool flag2 = service.TryGetNode(id, out node);
				if (flag2)
				{
					node.InvokeDecremented();
				}
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002898 File Offset: 0x00000A98
		[RequiredByNativeCode]
		internal static bool Internal_InvokeDismissed(int id)
		{
			AccessibilityHierarchyService service = AssistiveSupport.GetService<AccessibilityHierarchyService>();
			bool flag = service == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				AccessibilityNode node;
				bool flag3 = service.TryGetNode(id, out node);
				flag2 = flag3 && node.Dismissed();
			}
			return flag2;
		}

		// Token: 0x06000043 RID: 67
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFrame_Injected(int id, [In] ref Rect frame);

		// Token: 0x06000044 RID: 68
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetChildren_Injected(int id, ref ManagedSpanWrapper childIds);
	}
}
