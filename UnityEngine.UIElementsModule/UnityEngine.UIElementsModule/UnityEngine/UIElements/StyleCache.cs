using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004EC RID: 1260
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class StyleCache
	{
		// Token: 0x0600233A RID: 9018 RVA: 0x00081928 File Offset: 0x0007FB28
		public static bool TryGetValue(long hash, out ComputedStyle data)
		{
			return StyleCache.s_ComputedStyleCache.TryGetValue(hash, out data);
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x00081946 File Offset: 0x0007FB46
		public static void SetValue(long hash, ref ComputedStyle data)
		{
			StyleCache.s_ComputedStyleCache[hash] = data;
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x0008195C File Offset: 0x0007FB5C
		public static bool TryGetValue(int hash, out StyleVariableContext data)
		{
			return StyleCache.s_StyleVariableContextCache.TryGetValue(hash, out data);
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x0008197A File Offset: 0x0007FB7A
		public static void SetValue(int hash, StyleVariableContext data)
		{
			StyleCache.s_StyleVariableContextCache[hash] = data;
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x0008198C File Offset: 0x0007FB8C
		public static bool TryGetValue(int hash, out ComputedTransitionProperty[] data)
		{
			return StyleCache.s_ComputedTransitionsCache.TryGetValue(hash, out data);
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x000819AA File Offset: 0x0007FBAA
		public static void SetValue(int hash, ComputedTransitionProperty[] data)
		{
			StyleCache.s_ComputedTransitionsCache[hash] = data;
		}

		// Token: 0x04001003 RID: 4099
		private static Dictionary<long, ComputedStyle> s_ComputedStyleCache = new Dictionary<long, ComputedStyle>();

		// Token: 0x04001004 RID: 4100
		private static Dictionary<int, StyleVariableContext> s_StyleVariableContextCache = new Dictionary<int, StyleVariableContext>();

		// Token: 0x04001005 RID: 4101
		private static Dictionary<int, ComputedTransitionProperty[]> s_ComputedTransitionsCache = new Dictionary<int, ComputedTransitionProperty[]>();
	}
}
