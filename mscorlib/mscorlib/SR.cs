using System;
using System.Globalization;

// Token: 0x0200002D RID: 45
internal static class SR
{
	// Token: 0x06000055 RID: 85 RVA: 0x00002651 File Offset: 0x00000851
	internal static string GetString(string name, params object[] args)
	{
		return SR.GetString(CultureInfo.InvariantCulture, name, args);
	}

	// Token: 0x06000056 RID: 86 RVA: 0x0000265F File Offset: 0x0000085F
	internal static string GetString(CultureInfo culture, string name, params object[] args)
	{
		return string.Format(culture, name, args);
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00002645 File Offset: 0x00000845
	internal static string GetString(string name)
	{
		return name;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00002669 File Offset: 0x00000869
	internal static string Format(string resourceFormat, params object[] args)
	{
		if (args != null)
		{
			return string.Format(CultureInfo.InvariantCulture, resourceFormat, args);
		}
		return resourceFormat;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x0000267C File Offset: 0x0000087C
	internal static string Format(string resourceFormat, object p1)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1);
	}

	// Token: 0x0600005A RID: 90 RVA: 0x0000268A File Offset: 0x0000088A
	internal static string Format(string resourceFormat, object p1, object p2)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2);
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00002699 File Offset: 0x00000899
	internal static string Format(string resourceFormat, object p1, object p2, object p3)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2, p3);
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00002645 File Offset: 0x00000845
	internal static string GetResourceString(string str)
	{
		return str;
	}
}
