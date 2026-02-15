using System;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020000EC RID: 236
	internal static class DelegateHelpers
	{
		// Token: 0x060007BA RID: 1978 RVA: 0x000194C4 File Offset: 0x000176C4
		internal static Type GetFuncType(Type[] types)
		{
			switch (types.Length)
			{
			case 1:
				return typeof(Func<>).MakeGenericType(types);
			case 2:
				return typeof(Func<, >).MakeGenericType(types);
			case 3:
				return typeof(Func<, , >).MakeGenericType(types);
			case 4:
				return typeof(Func<, , , >).MakeGenericType(types);
			case 5:
				return typeof(Func<, , , , >).MakeGenericType(types);
			case 6:
				return typeof(Func<, , , , , >).MakeGenericType(types);
			case 7:
				return typeof(Func<, , , , , , >).MakeGenericType(types);
			case 8:
				return typeof(Func<, , , , , , , >).MakeGenericType(types);
			case 9:
				return typeof(Func<, , , , , , , , >).MakeGenericType(types);
			case 10:
				return typeof(Func<, , , , , , , , , >).MakeGenericType(types);
			case 11:
				return typeof(Func<, , , , , , , , , , >).MakeGenericType(types);
			case 12:
				return typeof(Func<, , , , , , , , , , , >).MakeGenericType(types);
			case 13:
				return typeof(Func<, , , , , , , , , , , , >).MakeGenericType(types);
			case 14:
				return typeof(Func<, , , , , , , , , , , , , >).MakeGenericType(types);
			case 15:
				return typeof(Func<, , , , , , , , , , , , , , >).MakeGenericType(types);
			case 16:
				return typeof(Func<, , , , , , , , , , , , , , , >).MakeGenericType(types);
			case 17:
				return typeof(Func<, , , , , , , , , , , , , , , , >).MakeGenericType(types);
			default:
				return null;
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00019648 File Offset: 0x00017848
		internal static Type GetActionType(Type[] types)
		{
			switch (types.Length)
			{
			case 0:
				return typeof(Action);
			case 1:
				return typeof(Action<>).MakeGenericType(types);
			case 2:
				return typeof(Action<, >).MakeGenericType(types);
			case 3:
				return typeof(Action<, , >).MakeGenericType(types);
			case 4:
				return typeof(Action<, , , >).MakeGenericType(types);
			case 5:
				return typeof(Action<, , , , >).MakeGenericType(types);
			case 6:
				return typeof(Action<, , , , , >).MakeGenericType(types);
			case 7:
				return typeof(Action<, , , , , , >).MakeGenericType(types);
			case 8:
				return typeof(Action<, , , , , , , >).MakeGenericType(types);
			case 9:
				return typeof(Action<, , , , , , , , >).MakeGenericType(types);
			case 10:
				return typeof(Action<, , , , , , , , , >).MakeGenericType(types);
			case 11:
				return typeof(Action<, , , , , , , , , , >).MakeGenericType(types);
			case 12:
				return typeof(Action<, , , , , , , , , , , >).MakeGenericType(types);
			case 13:
				return typeof(Action<, , , , , , , , , , , , >).MakeGenericType(types);
			case 14:
				return typeof(Action<, , , , , , , , , , , , , >).MakeGenericType(types);
			case 15:
				return typeof(Action<, , , , , , , , , , , , , , >).MakeGenericType(types);
			case 16:
				return typeof(Action<, , , , , , , , , , , , , , , >).MakeGenericType(types);
			default:
				return null;
			}
		}

		// Token: 0x04000261 RID: 609
		private static DelegateHelpers.TypeInfo _DelegateCache = new DelegateHelpers.TypeInfo();

		// Token: 0x04000262 RID: 610
		private static readonly Type[] s_delegateCtorSignature = new Type[]
		{
			typeof(object),
			typeof(IntPtr)
		};

		// Token: 0x020000ED RID: 237
		internal class TypeInfo
		{
		}
	}
}
