using System;
using System.Globalization;

// Token: 0x02000005 RID: 5
internal static class SR
{
	// Token: 0x06000004 RID: 4 RVA: 0x0000207F File Offset: 0x0000027F
	internal static string GetString(string name)
	{
		return name;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002082 File Offset: 0x00000282
	internal static string Format(string resourceFormat, params object[] args)
	{
		if (args != null)
		{
			return string.Format(CultureInfo.InvariantCulture, resourceFormat, args);
		}
		return resourceFormat;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002095 File Offset: 0x00000295
	internal static string Format(string resourceFormat, object p1)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000020A3 File Offset: 0x000002A3
	internal static string Format(string resourceFormat, object p1, object p2)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000020B2 File Offset: 0x000002B2
	internal static string Format(string resourceFormat, object p1, object p2, object p3)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2, p3);
	}
}
