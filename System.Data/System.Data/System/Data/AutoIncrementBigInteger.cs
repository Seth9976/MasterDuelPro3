using System;
using System.Data.Common;
using System.Numerics;

namespace System.Data
{
	// Token: 0x0200000D RID: 13
	internal sealed class AutoIncrementBigInteger : AutoIncrementValue
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004336 File Offset: 0x00002536
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00004343 File Offset: 0x00002543
		internal override object Current
		{
			get
			{
				return this._current;
			}
			set
			{
				this._current = (BigInteger)value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004351 File Offset: 0x00002551
		internal override Type DataType
		{
			get
			{
				return typeof(BigInteger);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000435D File Offset: 0x0000255D
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00004365 File Offset: 0x00002565
		internal override long Seed
		{
			get
			{
				return this._seed;
			}
			set
			{
				if (this._current == this._seed || this.BoundaryCheck(value))
				{
					this._current = value;
				}
				this._seed = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000439B File Offset: 0x0000259B
		// (set) Token: 0x0600009F RID: 159 RVA: 0x000043A8 File Offset: 0x000025A8
		internal override long Step
		{
			get
			{
				return (long)this._step;
			}
			set
			{
				if (value == 0L)
				{
					throw ExceptionBuilder.AutoIncrementSeed();
				}
				if (this._step != value)
				{
					if (this._current != this.Seed)
					{
						this._current = this._current - this._step + value;
					}
					this._step = value;
				}
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000440D File Offset: 0x0000260D
		internal override void MoveAfter()
		{
			this._current += this._step;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004426 File Offset: 0x00002626
		internal override void SetCurrent(object value, IFormatProvider formatProvider)
		{
			this._current = BigIntegerStorage.ConvertToBigInteger(value, formatProvider);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004438 File Offset: 0x00002638
		internal override void SetCurrentAndIncrement(object value)
		{
			BigInteger bigInteger = (BigInteger)value;
			if (this.BoundaryCheck(bigInteger))
			{
				this._current = bigInteger + this._step;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004467 File Offset: 0x00002667
		private bool BoundaryCheck(BigInteger value)
		{
			return (this._step < 0L && value <= this._current) || (0L < this._step && this._current <= value);
		}

		// Token: 0x04000030 RID: 48
		private BigInteger _current;

		// Token: 0x04000031 RID: 49
		private long _seed;

		// Token: 0x04000032 RID: 50
		private BigInteger _step = 1;
	}
}
