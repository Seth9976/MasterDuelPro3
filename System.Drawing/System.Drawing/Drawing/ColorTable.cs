using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Drawing
{
	// Token: 0x02000007 RID: 7
	internal static class ColorTable
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020A9 File Offset: 0x000002A9
		private static Dictionary<string, Color> GetColors()
		{
			Dictionary<string, Color> dictionary = new Dictionary<string, Color>(StringComparer.OrdinalIgnoreCase);
			ColorTable.FillConstants(dictionary, typeof(Color));
			ColorTable.FillConstants(dictionary, typeof(SystemColors));
			return dictionary;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020D5 File Offset: 0x000002D5
		internal static Dictionary<string, Color> Colors
		{
			get
			{
				return ColorTable.s_colorConstants.Value;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020E4 File Offset: 0x000002E4
		private static void FillConstants(Dictionary<string, Color> colors, Type enumType)
		{
			foreach (PropertyInfo propertyInfo in enumType.GetProperties())
			{
				if (propertyInfo.PropertyType == typeof(Color))
				{
					colors[propertyInfo.Name] = (Color)propertyInfo.GetValue(null, null);
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000213A File Offset: 0x0000033A
		internal static bool TryGetNamedColor(string name, out Color result)
		{
			return ColorTable.Colors.TryGetValue(name, out result);
		}

		// Token: 0x04000003 RID: 3
		private static readonly Lazy<Dictionary<string, Color>> s_colorConstants = new Lazy<Dictionary<string, Color>>(new Func<Dictionary<string, Color>>(ColorTable.GetColors));
	}
}
