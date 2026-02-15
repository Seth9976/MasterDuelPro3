using System;

// Token: 0x0200002C RID: 44
internal sealed class Locale
{
	// Token: 0x06000053 RID: 83 RVA: 0x00002645 File Offset: 0x00000845
	public static string GetText(string msg)
	{
		return msg;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00002648 File Offset: 0x00000848
	public static string GetText(string fmt, params object[] args)
	{
		return string.Format(fmt, args);
	}
}
