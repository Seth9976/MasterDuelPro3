using System;
using System.Linq;
using System.Reflection;

namespace UnityEngine.Rendering
{
	// Token: 0x020000ED RID: 237
	public static class DocumentationUtils
	{
		// Token: 0x060007B0 RID: 1968 RVA: 0x000128A4 File Offset: 0x00010AA4
		public static string GetHelpURL<TEnum>(TEnum mask = default(TEnum)) where TEnum : struct, IConvertible
		{
			HelpURLAttribute helpURLAttribute = (HelpURLAttribute)mask.GetType().GetCustomAttributes(typeof(HelpURLAttribute), false).FirstOrDefault<object>();
			if (helpURLAttribute != null)
			{
				return string.Format("{0}#{1}", helpURLAttribute.URL, mask);
			}
			return string.Empty;
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x000128F8 File Offset: 0x00010AF8
		public static bool TryGetHelpURL(Type type, out string url)
		{
			HelpURLAttribute attribute = type.GetCustomAttribute(false);
			url = ((attribute != null) ? attribute.URL : null);
			return attribute != null;
		}
	}
}
