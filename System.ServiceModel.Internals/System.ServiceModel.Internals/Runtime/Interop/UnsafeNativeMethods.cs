using System;
using System.Runtime.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Runtime.Interop
{
	// Token: 0x0200002C RID: 44
	internal static class UnsafeNativeMethods
	{
		// Token: 0x060000C2 RID: 194
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern uint EventRegister([In] ref Guid providerId, [In] UnsafeNativeMethods.EtwEnableCallback enableCallback, [In] void* callbackContext, [In] [Out] ref long registrationHandle);

		// Token: 0x060000C3 RID: 195
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern uint EventUnregister([In] long registrationHandle);

		// Token: 0x060000C4 RID: 196
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern bool EventEnabled([In] long registrationHandle, [In] ref EventDescriptor eventDescriptor);

		// Token: 0x060000C5 RID: 197
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern uint EventWrite([In] long registrationHandle, [In] ref EventDescriptor eventDescriptor, [In] uint userDataCount, [In] UnsafeNativeMethods.EventData* userData);

		// Token: 0x060000C6 RID: 198
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern uint EventActivityIdControl([In] int ControlCode, [In] [Out] ref Guid ActivityId);

		// Token: 0x060000C7 RID: 199
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool ReportEvent(SafeHandle hEventLog, ushort type, ushort category, uint eventID, byte[] userSID, ushort numStrings, uint dataLen, HandleRef strings, byte[] rawData);

		// Token: 0x060000C8 RID: 200
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeEventLogWriteHandle RegisterEventSource(string uncServerName, string sourceName);

		// Token: 0x0200002D RID: 45
		[StructLayout(LayoutKind.Explicit, Size = 16)]
		public struct EventData
		{
			// Token: 0x04000060 RID: 96
			[FieldOffset(0)]
			internal ulong DataPointer;

			// Token: 0x04000061 RID: 97
			[FieldOffset(8)]
			internal uint Size;

			// Token: 0x04000062 RID: 98
			[FieldOffset(12)]
			internal int Reserved;
		}

		// Token: 0x0200002E RID: 46
		// (Invoke) Token: 0x060000CA RID: 202
		internal unsafe delegate void EtwEnableCallback([In] ref Guid sourceId, [In] int isEnabled, [In] byte level, [In] long matchAnyKeywords, [In] long matchAllKeywords, [In] void* filterData, [In] void* callbackContext);
	}
}
