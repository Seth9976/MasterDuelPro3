using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Burst
{
	// Token: 0x02000029 RID: 41
	public readonly struct FunctionPointer<T> : IFunctionPointer
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00005805 File Offset: 0x00003A05
		public FunctionPointer(IntPtr ptr)
		{
			this._ptr = ptr;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x0000580E File Offset: 0x00003A0E
		public IntPtr Value
		{
			get
			{
				return this._ptr;
			}
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00005816 File Offset: 0x00003A16
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIsCreated()
		{
			if (!this.IsCreated)
			{
				throw new NullReferenceException("Object reference not set to an instance of an object");
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000582B File Offset: 0x00003A2B
		public T Invoke
		{
			get
			{
				return Marshal.GetDelegateForFunctionPointer<T>(this._ptr);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00005838 File Offset: 0x00003A38
		public bool IsCreated
		{
			get
			{
				return this._ptr != IntPtr.Zero;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000584A File Offset: 0x00003A4A
		IFunctionPointer IFunctionPointer.FromIntPtr(IntPtr ptr)
		{
			return new FunctionPointer<T>(ptr);
		}

		// Token: 0x04000172 RID: 370
		[NativeDisableUnsafePtrRestriction]
		private readonly IntPtr _ptr;
	}
}
