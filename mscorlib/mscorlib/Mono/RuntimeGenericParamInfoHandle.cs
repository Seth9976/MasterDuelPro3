using System;
using System.Reflection;

namespace Mono
{
	// Token: 0x02000035 RID: 53
	internal struct RuntimeGenericParamInfoHandle
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00002884 File Offset: 0x00000A84
		internal unsafe RuntimeGenericParamInfoHandle(IntPtr ptr)
		{
			this.value = (RuntimeStructs.GenericParamInfo*)(void*)ptr;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002892 File Offset: 0x00000A92
		internal Type[] Constraints
		{
			get
			{
				return this.GetConstraints();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000070 RID: 112 RVA: 0x0000289A File Offset: 0x00000A9A
		internal unsafe GenericParameterAttributes Attributes
		{
			get
			{
				return (GenericParameterAttributes)this.value->flags;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000028A8 File Offset: 0x00000AA8
		private unsafe Type[] GetConstraints()
		{
			int constraintsCount = this.GetConstraintsCount();
			Type[] array = new Type[constraintsCount];
			for (int i = 0; i < constraintsCount; i++)
			{
				RuntimeClassHandle runtimeClassHandle = new RuntimeClassHandle(*(IntPtr*)(this.value->constraints + (IntPtr)i * (IntPtr)sizeof(RuntimeStructs.MonoClass*) / (IntPtr)sizeof(RuntimeStructs.MonoClass*)));
				array[i] = Type.GetTypeFromHandle(runtimeClassHandle.GetTypeHandle());
			}
			return array;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000028FC File Offset: 0x00000AFC
		private unsafe int GetConstraintsCount()
		{
			int num = 0;
			RuntimeStructs.MonoClass** ptr = this.value->constraints;
			while (ptr != null && *(IntPtr*)ptr != (IntPtr)((UIntPtr)0))
			{
				ptr += sizeof(RuntimeStructs.MonoClass*) / sizeof(RuntimeStructs.MonoClass*);
				num++;
			}
			return num;
		}

		// Token: 0x0400010E RID: 270
		private unsafe RuntimeStructs.GenericParamInfo* value;
	}
}
