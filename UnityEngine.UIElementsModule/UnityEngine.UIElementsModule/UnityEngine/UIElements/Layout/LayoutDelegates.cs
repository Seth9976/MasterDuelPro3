using System;
using System.Runtime.InteropServices;
using AOT;
using Unity.Profiling;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x0200057A RID: 1402
	internal static class LayoutDelegates
	{
		// Token: 0x060026BA RID: 9914 RVA: 0x0009A1F0 File Offset: 0x000983F0
		[MonoPInvokeCallback(typeof(InvokeMeasureFunctionDelegate))]
		private static void InvokeMeasureFunction(ref LayoutNode node, float width, LayoutMeasureMode widthMode, float height, LayoutMeasureMode heightMode, ref IntPtr exception, out LayoutSize result)
		{
			LayoutMeasureFunction measureFunction = node.Measure;
			bool flag = measureFunction == null;
			if (flag)
			{
				Debug.Assert(false, "Measure called on a null method");
				result = default(LayoutSize);
			}
			else
			{
				try
				{
					using (LayoutDelegates.s_InvokeMeasureFunctionMarker.Auto())
					{
						measureFunction(node.GetOwner(), ref node, width, widthMode, height, heightMode, out result);
					}
				}
				catch (Exception e)
				{
					GCHandle handle = GCHandle.Alloc(e);
					exception = GCHandle.ToIntPtr(handle);
					result = default(LayoutSize);
				}
			}
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x0009A29C File Offset: 0x0009849C
		[MonoPInvokeCallback(typeof(InvokeBaselineFunctionDelegate))]
		private static float InvokeBaselineFunction(ref LayoutNode node, float width, float height)
		{
			LayoutBaselineFunction baselineFunction = node.Baseline;
			bool flag = baselineFunction == null;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				using (LayoutDelegates.s_InvokeBaselineFunctionMarker.Auto())
				{
					num = baselineFunction(ref node, width, height);
				}
			}
			return num;
		}

		// Token: 0x0400137C RID: 4988
		private static readonly ProfilerMarker s_InvokeMeasureFunctionMarker = new ProfilerMarker("InvokeMeasureFunction");

		// Token: 0x0400137D RID: 4989
		private static readonly ProfilerMarker s_InvokeBaselineFunctionMarker = new ProfilerMarker("InvokeBaselineFunction");

		// Token: 0x0400137E RID: 4990
		private static readonly InvokeMeasureFunctionDelegate s_InvokeMeasureDelegate = new InvokeMeasureFunctionDelegate(LayoutDelegates.InvokeMeasureFunction);

		// Token: 0x0400137F RID: 4991
		private static readonly InvokeBaselineFunctionDelegate s_InvokeBaselineDelegate = new InvokeBaselineFunctionDelegate(LayoutDelegates.InvokeBaselineFunction);

		// Token: 0x04001380 RID: 4992
		internal static readonly IntPtr s_InvokeMeasureFunction = Marshal.GetFunctionPointerForDelegate<InvokeMeasureFunctionDelegate>(LayoutDelegates.s_InvokeMeasureDelegate);

		// Token: 0x04001381 RID: 4993
		internal static readonly IntPtr s_InvokeBaselineFunction = Marshal.GetFunctionPointerForDelegate<InvokeBaselineFunctionDelegate>(LayoutDelegates.s_InvokeBaselineDelegate);
	}
}
