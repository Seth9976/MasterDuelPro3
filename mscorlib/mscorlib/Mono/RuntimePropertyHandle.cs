using System;

namespace Mono
{
	// Token: 0x02000037 RID: 55
	internal struct RuntimePropertyHandle
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00002999 File Offset: 0x00000B99
		internal RuntimePropertyHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000029A2 File Offset: 0x00000BA2
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000029AC File Offset: 0x00000BAC
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimePropertyHandle)obj).Value;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000029F4 File Offset: 0x00000BF4
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x04000110 RID: 272
		private IntPtr value;
	}
}
