using System;
using System.Globalization;

// Token: 0x02000002 RID: 2
internal static class SR
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	internal static string GetString(string name, params object[] args)
	{
		return SR.GetString(CultureInfo.InvariantCulture, name, args);
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000205E File Offset: 0x0000025E
	internal static string GetString(CultureInfo culture, string name, params object[] args)
	{
		return string.Format(culture, name, args);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
	internal static string Format(string resourceFormat, object p1)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1);
	}
}
