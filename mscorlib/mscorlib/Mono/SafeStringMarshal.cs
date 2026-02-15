using System;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x02000048 RID: 72
	internal struct SafeStringMarshal : IDisposable
	{
		// Token: 0x0600008B RID: 139
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr StringToUtf8_icall(ref string str);

		// Token: 0x0600008C RID: 140 RVA: 0x00002BD3 File Offset: 0x00000DD3
		public static IntPtr StringToUtf8(string str)
		{
			return SafeStringMarshal.StringToUtf8_icall(ref str);
		}

		// Token: 0x0600008D RID: 141
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GFree(IntPtr ptr);

		// Token: 0x0600008E RID: 142 RVA: 0x00002BDC File Offset: 0x00000DDC
		public SafeStringMarshal(string str)
		{
			this.str = str;
			this.marshaled_string = IntPtr.Zero;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public IntPtr Value
		{
			get
			{
				if (this.marshaled_string == IntPtr.Zero && this.str != null)
				{
					this.marshaled_string = SafeStringMarshal.StringToUtf8(this.str);
				}
				return this.marshaled_string;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002C23 File Offset: 0x00000E23
		public void Dispose()
		{
			if (this.marshaled_string != IntPtr.Zero)
			{
				SafeStringMarshal.GFree(this.marshaled_string);
				this.marshaled_string = IntPtr.Zero;
			}
		}

		// Token: 0x0400013C RID: 316
		private readonly string str;

		// Token: 0x0400013D RID: 317
		private IntPtr marshaled_string;
	}
}
