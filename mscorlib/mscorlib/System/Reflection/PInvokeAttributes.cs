using System;

namespace System.Reflection
{
	// Token: 0x0200062F RID: 1583
	[Flags]
	[Serializable]
	internal enum PInvokeAttributes
	{
		// Token: 0x04001805 RID: 6149
		NoMangle = 1,
		// Token: 0x04001806 RID: 6150
		CharSetMask = 6,
		// Token: 0x04001807 RID: 6151
		CharSetNotSpec = 0,
		// Token: 0x04001808 RID: 6152
		CharSetAnsi = 2,
		// Token: 0x04001809 RID: 6153
		CharSetUnicode = 4,
		// Token: 0x0400180A RID: 6154
		CharSetAuto = 6,
		// Token: 0x0400180B RID: 6155
		BestFitUseAssem = 0,
		// Token: 0x0400180C RID: 6156
		BestFitEnabled = 16,
		// Token: 0x0400180D RID: 6157
		BestFitDisabled = 32,
		// Token: 0x0400180E RID: 6158
		BestFitMask = 48,
		// Token: 0x0400180F RID: 6159
		ThrowOnUnmappableCharUseAssem = 0,
		// Token: 0x04001810 RID: 6160
		ThrowOnUnmappableCharEnabled = 4096,
		// Token: 0x04001811 RID: 6161
		ThrowOnUnmappableCharDisabled = 8192,
		// Token: 0x04001812 RID: 6162
		ThrowOnUnmappableCharMask = 12288,
		// Token: 0x04001813 RID: 6163
		SupportsLastError = 64,
		// Token: 0x04001814 RID: 6164
		CallConvMask = 1792,
		// Token: 0x04001815 RID: 6165
		CallConvWinapi = 256,
		// Token: 0x04001816 RID: 6166
		CallConvCdecl = 512,
		// Token: 0x04001817 RID: 6167
		CallConvStdcall = 768,
		// Token: 0x04001818 RID: 6168
		CallConvThiscall = 1024,
		// Token: 0x04001819 RID: 6169
		CallConvFastcall = 1280,
		// Token: 0x0400181A RID: 6170
		MaxValue = 65535
	}
}
