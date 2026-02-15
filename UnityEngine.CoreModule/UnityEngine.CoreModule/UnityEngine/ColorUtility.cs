using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000144 RID: 324
	[NativeHeader("Runtime/Math/ColorUtility.h")]
	public class ColorUtility
	{
		// Token: 0x06000D83 RID: 3459 RVA: 0x0001A604 File Offset: 0x00018804
		[FreeFunction("TryParseHtmlColor", true)]
		internal unsafe static bool DoTryParseHtmlColor(string htmlString, out Color32 color)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(htmlString, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = htmlString.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = ColorUtility.DoTryParseHtmlColor_Injected(ref managedSpanWrapper, out color);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0001A65C File Offset: 0x0001885C
		public static bool TryParseHtmlString(string htmlString, out Color color)
		{
			Color32 c;
			bool ret = ColorUtility.DoTryParseHtmlColor(htmlString, out c);
			color = c;
			return ret;
		}

		// Token: 0x06000D85 RID: 3461
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool DoTryParseHtmlColor_Injected(ref ManagedSpanWrapper htmlString, out Color32 color);
	}
}
