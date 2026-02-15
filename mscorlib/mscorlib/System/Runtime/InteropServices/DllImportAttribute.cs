using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates that the attributed method is exposed by an unmanaged dynamic-link library (DLL) as a static entry point.</summary>
	// Token: 0x02000537 RID: 1335
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	public sealed class DllImportAttribute : Attribute
	{
		// Token: 0x0600291E RID: 10526 RVA: 0x000A7F4C File Offset: 0x000A614C
		internal static Attribute GetCustomAttribute(RuntimeMethodInfo method)
		{
			if ((method.Attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PrivateScope)
			{
				return null;
			}
			string text = null;
			int metadataToken = method.MetadataToken;
			PInvokeAttributes pinvokeAttributes = PInvokeAttributes.CharSetNotSpec;
			string text2;
			method.GetPInvoke(out pinvokeAttributes, out text2, out text);
			CharSet charSet = CharSet.None;
			switch (pinvokeAttributes & PInvokeAttributes.CharSetMask)
			{
			case PInvokeAttributes.CharSetNotSpec:
				charSet = CharSet.None;
				break;
			case PInvokeAttributes.CharSetAnsi:
				charSet = CharSet.Ansi;
				break;
			case PInvokeAttributes.CharSetUnicode:
				charSet = CharSet.Unicode;
				break;
			case PInvokeAttributes.CharSetMask:
				charSet = CharSet.Auto;
				break;
			}
			CallingConvention callingConvention = CallingConvention.Cdecl;
			PInvokeAttributes pinvokeAttributes2 = pinvokeAttributes & PInvokeAttributes.CallConvMask;
			if (pinvokeAttributes2 <= PInvokeAttributes.CallConvCdecl)
			{
				if (pinvokeAttributes2 != PInvokeAttributes.CallConvWinapi)
				{
					if (pinvokeAttributes2 == PInvokeAttributes.CallConvCdecl)
					{
						callingConvention = CallingConvention.Cdecl;
					}
				}
				else
				{
					callingConvention = CallingConvention.Winapi;
				}
			}
			else if (pinvokeAttributes2 != PInvokeAttributes.CallConvStdcall)
			{
				if (pinvokeAttributes2 != PInvokeAttributes.CallConvThiscall)
				{
					if (pinvokeAttributes2 == PInvokeAttributes.CallConvFastcall)
					{
						callingConvention = CallingConvention.FastCall;
					}
				}
				else
				{
					callingConvention = CallingConvention.ThisCall;
				}
			}
			else
			{
				callingConvention = CallingConvention.StdCall;
			}
			bool flag = (pinvokeAttributes & PInvokeAttributes.NoMangle) > PInvokeAttributes.CharSetNotSpec;
			bool flag2 = (pinvokeAttributes & PInvokeAttributes.SupportsLastError) > PInvokeAttributes.CharSetNotSpec;
			bool flag3 = (pinvokeAttributes & PInvokeAttributes.BestFitMask) == PInvokeAttributes.BestFitEnabled;
			bool flag4 = (pinvokeAttributes & PInvokeAttributes.ThrowOnUnmappableCharMask) == PInvokeAttributes.ThrowOnUnmappableCharEnabled;
			bool flag5 = (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > MethodImplAttributes.IL;
			return new DllImportAttribute(text, text2, charSet, flag, flag2, flag5, callingConvention, flag3, flag4);
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x000A8067 File Offset: 0x000A6267
		internal static bool IsDefined(RuntimeMethodInfo method)
		{
			return (method.Attributes & MethodAttributes.PinvokeImpl) > MethodAttributes.PrivateScope;
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x000A8078 File Offset: 0x000A6278
		internal DllImportAttribute(string dllName, string entryPoint, CharSet charSet, bool exactSpelling, bool setLastError, bool preserveSig, CallingConvention callingConvention, bool bestFitMapping, bool throwOnUnmappableChar)
		{
			this._val = dllName;
			this.EntryPoint = entryPoint;
			this.CharSet = charSet;
			this.ExactSpelling = exactSpelling;
			this.SetLastError = setLastError;
			this.PreserveSig = preserveSig;
			this.CallingConvention = callingConvention;
			this.BestFitMapping = bestFitMapping;
			this.ThrowOnUnmappableChar = throwOnUnmappableChar;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DllImportAttribute" /> class with the name of the DLL containing the method to import.</summary>
		/// <param name="dllName">The name of the DLL that contains the unmanaged method. This can include an assembly display name, if the DLL is included in an assembly.</param>
		// Token: 0x06002921 RID: 10529 RVA: 0x000A80D0 File Offset: 0x000A62D0
		public DllImportAttribute(string dllName)
		{
			this._val = dllName;
		}

		/// <summary>Gets the name of the DLL file that contains the entry point.</summary>
		/// <returns>The name of the DLL file that contains the entry point.</returns>
		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06002922 RID: 10530 RVA: 0x000A80DF File Offset: 0x000A62DF
		public string Value
		{
			get
			{
				return this._val;
			}
		}

		// Token: 0x0400156D RID: 5485
		internal string _val;

		/// <summary>Indicates the name or ordinal of the DLL entry point to be called.</summary>
		// Token: 0x0400156E RID: 5486
		public string EntryPoint;

		/// <summary>Indicates how to marshal string parameters to the method and controls name mangling.</summary>
		// Token: 0x0400156F RID: 5487
		public CharSet CharSet;

		/// <summary>Indicates whether the callee calls the SetLastError Win32 API function before returning from the attributed method.</summary>
		// Token: 0x04001570 RID: 5488
		public bool SetLastError;

		/// <summary>Controls whether the <see cref="F:System.Runtime.InteropServices.DllImportAttribute.CharSet" /> field causes the common language runtime to search an unmanaged DLL for entry-point names other than the one specified.</summary>
		// Token: 0x04001571 RID: 5489
		public bool ExactSpelling;

		/// <summary>Indicates whether unmanaged methods that have HRESULT or retval return values are directly translated or whether HRESULT or retval return values are automatically converted to exceptions.</summary>
		// Token: 0x04001572 RID: 5490
		public bool PreserveSig;

		/// <summary>Indicates the calling convention of an entry point.</summary>
		// Token: 0x04001573 RID: 5491
		public CallingConvention CallingConvention;

		/// <summary>Enables or disables best-fit mapping behavior when converting Unicode characters to ANSI characters.</summary>
		// Token: 0x04001574 RID: 5492
		public bool BestFitMapping;

		/// <summary>Enables or disables the throwing of an exception on an unmappable Unicode character that is converted to an ANSI "?" character.</summary>
		// Token: 0x04001575 RID: 5493
		public bool ThrowOnUnmappableChar;
	}
}
