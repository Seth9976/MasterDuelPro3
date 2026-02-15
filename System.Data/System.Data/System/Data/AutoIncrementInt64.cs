using System;
using System.Data.Common;
using System.Globalization;
using System.Numerics;

namespace System.Data
{
	// Token: 0x0200000C RID: 12
	internal sealed class AutoIncrementInt64 : AutoIncrementValue
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000041E5 File Offset: 0x000023E5
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000041F2 File Offset: 0x000023F2
		internal override object Current
		{
			get
			{
				return this._current;
			}
			set
			{
				this._current = (long)value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004200 File Offset: 0x00002400
		internal override Type DataType
		{
			get
			{
				return typeof(long);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000420C File Offset: 0x0000240C
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00004214 File Offset: 0x00002414
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

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004240 File Offset: 0x00002440
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00004248 File Offset: 0x00002448
		internal override long Step
		{
			get
			{
				return this._step;
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

		// Token: 0x06000094 RID: 148 RVA: 0x00004286 File Offset: 0x00002486
		internal override void MoveAfter()
		{
			this._current += this._step;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000429B File Offset: 0x0000249B
		internal override void SetCurrent(object value, IFormatProvider formatProvider)
		{
			this._current = Convert.ToInt64(value, formatProvider);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000042AC File Offset: 0x000024AC
		internal override void SetCurrentAndIncrement(object value)
		{
			long num = (long)SqlConvert.ChangeType2(value, StorageType.Int64, typeof(long), CultureInfo.InvariantCulture);
			if (this.BoundaryCheck(num))
			{
				this._current = num + this._step;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000042F2 File Offset: 0x000024F2
		private bool BoundaryCheck(BigInteger value)
		{
			return (this._step < 0L && value <= this._current) || (0L < this._step && this._current <= value);
		}

		// Token: 0x0400002D RID: 45
		private long _current;

		// Token: 0x0400002E RID: 46
		private long _seed;

		// Token: 0x0400002F RID: 47
		private long _step = 1L;
	}
}
