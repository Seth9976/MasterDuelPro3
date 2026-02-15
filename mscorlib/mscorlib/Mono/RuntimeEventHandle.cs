using System;

namespace Mono
{
	// Token: 0x02000036 RID: 54
	internal struct RuntimeEventHandle
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00002932 File Offset: 0x00000B32
		internal RuntimeEventHandle(IntPtr v)
		{
			this.value = v;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000293B File Offset: 0x00000B3B
		public IntPtr Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002944 File Offset: 0x00000B44
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeEventHandle)obj).Value;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000298C File Offset: 0x00000B8C
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x0400010F RID: 271
		private IntPtr value;
	}
}
