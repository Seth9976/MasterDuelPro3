using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000AB RID: 171
	[NullableContext(1)]
	[Nullable(0)]
	internal class DynamicProxy<[Nullable(2)] T>
	{
		// Token: 0x060005A4 RID: 1444 RVA: 0x0001EB40 File Offset: 0x0001CD40
		public virtual IEnumerable<string> GetDynamicMemberNames(T instance)
		{
			return CollectionUtils.ArrayEmpty<string>();
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001EB47 File Offset: 0x0001CD47
		public virtual bool TryBinaryOperation(T instance, BinaryOperationBinder binder, object arg, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001EB4E File Offset: 0x0001CD4E
		public virtual bool TryConvert(T instance, ConvertBinder binder, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001EB47 File Offset: 0x0001CD47
		public virtual bool TryCreateInstance(T instance, CreateInstanceBinder binder, object[] args, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00016B42 File Offset: 0x00014D42
		public virtual bool TryDeleteIndex(T instance, DeleteIndexBinder binder, object[] indexes)
		{
			return false;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00016B42 File Offset: 0x00014D42
		public virtual bool TryDeleteMember(T instance, DeleteMemberBinder binder)
		{
			return false;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001EB47 File Offset: 0x0001CD47
		public virtual bool TryGetIndex(T instance, GetIndexBinder binder, object[] indexes, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001EB4E File Offset: 0x0001CD4E
		public virtual bool TryGetMember(T instance, GetMemberBinder binder, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001EB47 File Offset: 0x0001CD47
		public virtual bool TryInvoke(T instance, InvokeBinder binder, object[] args, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001EB47 File Offset: 0x0001CD47
		public virtual bool TryInvokeMember(T instance, InvokeMemberBinder binder, object[] args, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00016B42 File Offset: 0x00014D42
		public virtual bool TrySetIndex(T instance, SetIndexBinder binder, object[] indexes, object value)
		{
			return false;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00016B42 File Offset: 0x00014D42
		public virtual bool TrySetMember(T instance, SetMemberBinder binder, object value)
		{
			return false;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001EB4E File Offset: 0x0001CD4E
		public virtual bool TryUnaryOperation(T instance, UnaryOperationBinder binder, [Nullable(2)] out object result)
		{
			result = null;
			return false;
		}
	}
}
