using System;

namespace System.Data
{
	// Token: 0x02000038 RID: 56
	internal sealed class DataError
	{
		// Token: 0x060003C4 RID: 964 RVA: 0x00014533 File Offset: 0x00012733
		internal DataError()
		{
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00014546 File Offset: 0x00012746
		internal DataError(string rowError)
		{
			this.SetText(rowError);
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00014560 File Offset: 0x00012760
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00014568 File Offset: 0x00012768
		internal string Text
		{
			get
			{
				return this._rowError;
			}
			set
			{
				this.SetText(value);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00014571 File Offset: 0x00012771
		internal bool HasErrors
		{
			get
			{
				return this._rowError.Length != 0 || this._count != 0;
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001458C File Offset: 0x0001278C
		internal void SetColumnError(DataColumn column, string error)
		{
			if (error == null || error.Length == 0)
			{
				this.Clear(column);
				return;
			}
			if (this._errorList == null)
			{
				this._errorList = new DataError.ColumnError[1];
			}
			int num = this.IndexOf(column);
			this._errorList[num]._column = column;
			this._errorList[num]._error = error;
			column._errors++;
			if (num == this._count)
			{
				this._count++;
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00014614 File Offset: 0x00012814
		internal string GetColumnError(DataColumn column)
		{
			for (int i = 0; i < this._count; i++)
			{
				if (this._errorList[i]._column == column)
				{
					return this._errorList[i]._error;
				}
			}
			return string.Empty;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00014660 File Offset: 0x00012860
		internal void Clear(DataColumn column)
		{
			if (this._count == 0)
			{
				return;
			}
			for (int i = 0; i < this._count; i++)
			{
				if (this._errorList[i]._column == column)
				{
					Array.Copy(this._errorList, i + 1, this._errorList, i, this._count - i - 1);
					this._count--;
					column._errors--;
				}
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000146D8 File Offset: 0x000128D8
		internal void Clear()
		{
			for (int i = 0; i < this._count; i++)
			{
				this._errorList[i]._column._errors--;
			}
			this._count = 0;
			this._rowError = string.Empty;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00014728 File Offset: 0x00012928
		internal DataColumn[] GetColumnsInError()
		{
			DataColumn[] array = new DataColumn[this._count];
			for (int i = 0; i < this._count; i++)
			{
				array[i] = this._errorList[i]._column;
			}
			return array;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00014767 File Offset: 0x00012967
		private void SetText(string errorText)
		{
			if (errorText == null)
			{
				errorText = string.Empty;
			}
			this._rowError = errorText;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001477C File Offset: 0x0001297C
		internal int IndexOf(DataColumn column)
		{
			for (int i = 0; i < this._count; i++)
			{
				if (this._errorList[i]._column == column)
				{
					return i;
				}
			}
			if (this._count >= this._errorList.Length)
			{
				DataError.ColumnError[] array = new DataError.ColumnError[Math.Min(this._count * 2, column.Table.Columns.Count)];
				Array.Copy(this._errorList, 0, array, 0, this._count);
				this._errorList = array;
			}
			return this._count;
		}

		// Token: 0x04000126 RID: 294
		private string _rowError = string.Empty;

		// Token: 0x04000127 RID: 295
		private int _count;

		// Token: 0x04000128 RID: 296
		private DataError.ColumnError[] _errorList;

		// Token: 0x02000039 RID: 57
		internal struct ColumnError
		{
			// Token: 0x04000129 RID: 297
			internal DataColumn _column;

			// Token: 0x0400012A RID: 298
			internal string _error;
		}
	}
}
