using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace System.Dynamic.Utils
{
	// Token: 0x02000146 RID: 326
	internal static class ContractUtils
	{
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0002A7E2 File Offset: 0x000289E2
		[ExcludeFromCodeCoverage]
		public static Exception Unreachable
		{
			get
			{
				return new InvalidOperationException("Code supposed to be unreachable");
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0002A7EE File Offset: 0x000289EE
		public static void Requires(bool precondition, string paramName)
		{
			if (!precondition)
			{
				throw Error.InvalidArgumentValue(paramName);
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0002A7FA File Offset: 0x000289FA
		public static void RequiresNotNull(object value, string paramName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(paramName);
			}
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0002A806 File Offset: 0x00028A06
		public static void RequiresNotNull(object value, string paramName, int index)
		{
			if (value == null)
			{
				throw new ArgumentNullException(ContractUtils.GetParamName(paramName, index));
			}
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0002A818 File Offset: 0x00028A18
		public static void RequiresNotEmpty<T>(ICollection<T> collection, string paramName)
		{
			ContractUtils.RequiresNotNull(collection, paramName);
			if (collection.Count == 0)
			{
				throw Error.NonEmptyCollectionRequired(paramName);
			}
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0002A830 File Offset: 0x00028A30
		public static void RequiresNotNullItems<T>(IList<T> array, string arrayName)
		{
			ContractUtils.RequiresNotNull(array, arrayName);
			int i = 0;
			int count = array.Count;
			while (i < count)
			{
				if (array[i] == null)
				{
					throw new ArgumentNullException(ContractUtils.GetParamName(arrayName, i));
				}
				i++;
			}
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0002A872 File Offset: 0x00028A72
		private static string GetParamName(string paramName, int index)
		{
			if (index < 0)
			{
				return paramName;
			}
			return string.Format("{0}[{1}]", paramName, index);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0002A88B File Offset: 0x00028A8B
		public static void RequiresArrayRange<T>(IList<T> array, int offset, int count, string offsetName, string countName)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(countName);
			}
			if (offset < 0 || array.Count - offset < count)
			{
				throw new ArgumentOutOfRangeException(offsetName);
			}
		}
	}
}
