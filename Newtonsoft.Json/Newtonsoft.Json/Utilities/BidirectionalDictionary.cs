using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000098 RID: 152
	[NullableContext(1)]
	[Nullable(0)]
	internal class BidirectionalDictionary<TFirst, TSecond>
	{
		// Token: 0x060004F9 RID: 1273 RVA: 0x0001ADA2 File Offset: 0x00018FA2
		public BidirectionalDictionary()
			: this(EqualityComparer<TFirst>.Default, EqualityComparer<TSecond>.Default)
		{
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001ADB4 File Offset: 0x00018FB4
		public BidirectionalDictionary(IEqualityComparer<TFirst> firstEqualityComparer, IEqualityComparer<TSecond> secondEqualityComparer)
			: this(firstEqualityComparer, secondEqualityComparer, "Duplicate item already exists for '{0}'.", "Duplicate item already exists for '{0}'.")
		{
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0001ADC8 File Offset: 0x00018FC8
		public BidirectionalDictionary(IEqualityComparer<TFirst> firstEqualityComparer, IEqualityComparer<TSecond> secondEqualityComparer, string duplicateFirstErrorMessage, string duplicateSecondErrorMessage)
		{
			this._firstToSecond = new Dictionary<TFirst, TSecond>(firstEqualityComparer);
			this._secondToFirst = new Dictionary<TSecond, TFirst>(secondEqualityComparer);
			this._duplicateFirstErrorMessage = duplicateFirstErrorMessage;
			this._duplicateSecondErrorMessage = duplicateSecondErrorMessage;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001ADF8 File Offset: 0x00018FF8
		public void Set(TFirst first, TSecond second)
		{
			TSecond tsecond;
			if (this._firstToSecond.TryGetValue(first, out tsecond) && !tsecond.Equals(second))
			{
				throw new ArgumentException(this._duplicateFirstErrorMessage.FormatWith(CultureInfo.InvariantCulture, first));
			}
			TFirst tfirst;
			if (this._secondToFirst.TryGetValue(second, out tfirst) && !tfirst.Equals(first))
			{
				throw new ArgumentException(this._duplicateSecondErrorMessage.FormatWith(CultureInfo.InvariantCulture, second));
			}
			this._firstToSecond.Add(first, second);
			this._secondToFirst.Add(second, first);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0001AEA1 File Offset: 0x000190A1
		public bool TryGetByFirst(TFirst first, [Nullable(2)] [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out TSecond second)
		{
			return this._firstToSecond.TryGetValue(first, out second);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001AEB0 File Offset: 0x000190B0
		public bool TryGetBySecond(TSecond second, [Nullable(2)] [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out TFirst first)
		{
			return this._secondToFirst.TryGetValue(second, out first);
		}

		// Token: 0x0400037E RID: 894
		private readonly IDictionary<TFirst, TSecond> _firstToSecond;

		// Token: 0x0400037F RID: 895
		private readonly IDictionary<TSecond, TFirst> _secondToFirst;

		// Token: 0x04000380 RID: 896
		private readonly string _duplicateFirstErrorMessage;

		// Token: 0x04000381 RID: 897
		private readonly string _duplicateSecondErrorMessage;
	}
}
