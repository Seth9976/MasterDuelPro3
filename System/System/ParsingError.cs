using System;

namespace System
{
	// Token: 0x020000FC RID: 252
	internal enum ParsingError
	{
		// Token: 0x04000422 RID: 1058
		None,
		// Token: 0x04000423 RID: 1059
		BadFormat,
		// Token: 0x04000424 RID: 1060
		BadScheme,
		// Token: 0x04000425 RID: 1061
		BadAuthority,
		// Token: 0x04000426 RID: 1062
		EmptyUriString,
		// Token: 0x04000427 RID: 1063
		LastRelativeUriOkErrIndex = 4,
		// Token: 0x04000428 RID: 1064
		SchemeLimit,
		// Token: 0x04000429 RID: 1065
		SizeLimit,
		// Token: 0x0400042A RID: 1066
		MustRootedPath,
		// Token: 0x0400042B RID: 1067
		BadHostName,
		// Token: 0x0400042C RID: 1068
		NonEmptyHost,
		// Token: 0x0400042D RID: 1069
		BadPort,
		// Token: 0x0400042E RID: 1070
		BadAuthorityTerminator,
		// Token: 0x0400042F RID: 1071
		CannotCreateRelative
	}
}
