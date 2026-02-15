using System;
using System.Globalization;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	[VisibleToOtherModules]
	internal sealed class UnityString
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000020CC File Offset: 0x000002CC
		public static string Format(string fmt, params object[] args)
		{
			return string.Format(CultureInfo.InvariantCulture.NumberFormat, fmt, args);
		}
	}
}
