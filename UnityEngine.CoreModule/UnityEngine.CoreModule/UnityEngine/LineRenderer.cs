using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000EA RID: 234
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/LineRenderer.h")]
	public sealed class LineRenderer : Renderer
	{
		// Token: 0x06000623 RID: 1571 RVA: 0x0000CDE3 File Offset: 0x0000AFE3
		[Obsolete("Use startColor, endColor or colorGradient instead.", false)]
		public void SetColors(Color start, Color end)
		{
			this.startColor = start;
			this.endColor = end;
		}

		// Token: 0x17000126 RID: 294
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x0000CDF8 File Offset: 0x0000AFF8
		public Color startColor
		{
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_startColor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000127 RID: 295
		// (set) Token: 0x06000625 RID: 1573 RVA: 0x0000CE1C File Offset: 0x0000B01C
		public Color endColor
		{
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LineRenderer.set_endColor_Injected(intPtr, ref value);
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0000CE40 File Offset: 0x0000B040
		[FreeFunction(Name = "LineRendererScripting::SetPositions", HasExplicitThis = true)]
		public unsafe void SetPositions([NotNull] Vector3[] positions)
		{
			if (positions == null)
			{
				ThrowHelper.ThrowArgumentNullException(positions, "positions");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<LineRenderer>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector3> span = new Span<Vector3>(positions);
			fixed (Vector3* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				LineRenderer.SetPositions_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x06000627 RID: 1575
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_startColor_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x06000628 RID: 1576
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_endColor_Injected(IntPtr _unity_self, [In] ref Color value);

		// Token: 0x06000629 RID: 1577
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPositions_Injected(IntPtr _unity_self, ref ManagedSpanWrapper positions);
	}
}
