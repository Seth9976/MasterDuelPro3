using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x02000087 RID: 135
	internal class BlockExpressionList : IList<Expression>, ICollection<Expression>, IEnumerable<Expression>, IEnumerable
	{
		// Token: 0x06000418 RID: 1048 RVA: 0x00012E0E File Offset: 0x0001100E
		internal BlockExpressionList(BlockExpression provider, Expression arg0)
		{
			this._block = provider;
			this._arg0 = arg0;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00012E24 File Offset: 0x00011024
		public int IndexOf(Expression item)
		{
			if (this._arg0 == item)
			{
				return 0;
			}
			for (int i = 1; i < this._block.ExpressionCount; i++)
			{
				if (this._block.GetExpression(i) == item)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		public void Insert(int index, Expression item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		public void RemoveAt(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x1700008A RID: 138
		public Expression this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this._arg0;
				}
				return this._block.GetExpression(index);
			}
			[ExcludeFromCodeCoverage]
			set
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		public void Add(Expression item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		public void Clear()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00012E7C File Offset: 0x0001107C
		public bool Contains(Expression item)
		{
			return this.IndexOf(item) != -1;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00012E8C File Offset: 0x0001108C
		public void CopyTo(Expression[] array, int index)
		{
			ContractUtils.RequiresNotNull(array, "array");
			if (index < 0)
			{
				throw Error.ArgumentOutOfRange("index");
			}
			int expressionCount = this._block.ExpressionCount;
			if (index + expressionCount > array.Length)
			{
				throw new ArgumentException();
			}
			array[index++] = this._arg0;
			for (int i = 1; i < expressionCount; i++)
			{
				array[index++] = this._block.GetExpression(i);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x00012EFB File Offset: 0x000110FB
		public int Count
		{
			get
			{
				return this._block.ExpressionCount;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		public bool IsReadOnly
		{
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00012A02 File Offset: 0x00010C02
		[ExcludeFromCodeCoverage]
		public bool Remove(Expression item)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00012F08 File Offset: 0x00011108
		public IEnumerator<Expression> GetEnumerator()
		{
			yield return this._arg0;
			int num;
			for (int i = 1; i < this._block.ExpressionCount; i = num + 1)
			{
				yield return this._block.GetExpression(i);
				num = i;
			}
			yield break;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00012F17 File Offset: 0x00011117
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000130 RID: 304
		private readonly BlockExpression _block;

		// Token: 0x04000131 RID: 305
		private readonly Expression _arg0;
	}
}
