using System;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x02000033 RID: 51
	internal struct RuntimeClassHandle
	{
		// Token: 0x06000066 RID: 102 RVA: 0x000027D8 File Offset: 0x000009D8
		internal unsafe RuntimeClassHandle(RuntimeStructs.MonoClass* value)
		{
			this.value = value;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000027E1 File Offset: 0x000009E1
		internal unsafe RuntimeClassHandle(IntPtr ptr)
		{
			this.value = (RuntimeStructs.MonoClass*)(void*)ptr;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000027EF File Offset: 0x000009EF
		internal unsafe RuntimeStructs.MonoClass* Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000027F8 File Offset: 0x000009F8
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.value == ((RuntimeClassHandle)obj).Value;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002840 File Offset: 0x00000A40
		public unsafe override int GetHashCode()
		{
			return ((IntPtr)((void*)this.value)).GetHashCode();
		}

		// Token: 0x0600006B RID: 107
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern IntPtr GetTypeFromClass(RuntimeStructs.MonoClass* klass);

		// Token: 0x0600006C RID: 108 RVA: 0x00002860 File Offset: 0x00000A60
		internal RuntimeTypeHandle GetTypeHandle()
		{
			return new RuntimeTypeHandle(RuntimeClassHandle.GetTypeFromClass(this.value));
		}

		// Token: 0x0400010C RID: 268
		private unsafe RuntimeStructs.MonoClass* value;
	}
}
