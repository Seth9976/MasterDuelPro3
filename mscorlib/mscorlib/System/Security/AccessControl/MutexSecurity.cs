using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Security.AccessControl
{
	/// <summary>Represents the Windows access control security for a named mutex. This class cannot be inherited. </summary>
	// Token: 0x020003FA RID: 1018
	public sealed class MutexSecurity : NativeObjectSecurity
	{
		// Token: 0x06002251 RID: 8785 RVA: 0x0008E352 File Offset: 0x0008C552
		internal MutexSecurity(SafeHandle handle, AccessControlSections includeSections)
			: base(false, ResourceType.KernelObject, handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(MutexSecurity.MutexExceptionFromErrorCode), null)
		{
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x0008E36B File Offset: 0x0008C56B
		private static Exception MutexExceptionFromErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			if (errorCode == 2)
			{
				return new WaitHandleCannotBeOpenedException();
			}
			return NativeObjectSecurity.DefaultExceptionFromErrorCode(errorCode, name, handle, context);
		}
	}
}
