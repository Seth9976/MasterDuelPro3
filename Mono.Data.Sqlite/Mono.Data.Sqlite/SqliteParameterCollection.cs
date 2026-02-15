using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000021 RID: 33
	[Editor("Microsoft.VSDesigner.Data.Design.DBParametersEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ListBindable(false)]
	public sealed class SqliteParameterCollection : DbParameterCollection
	{
		// Token: 0x06000157 RID: 343 RVA: 0x0000C4D9 File Offset: 0x0000A6D9
		internal SqliteParameterCollection(SqliteCommand cmd)
		{
			this._command = cmd;
			this._parameterList = new List<SqliteParameter>();
			this._unboundFlag = true;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000C4FA File Offset: 0x0000A6FA
		public override bool IsSynchronized
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000ADAB File Offset: 0x00008FAB
		public override bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000ADAB File Offset: 0x00008FAB
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		public override object SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000C4FD File Offset: 0x0000A6FD
		public override IEnumerator GetEnumerator()
		{
			return this._parameterList.GetEnumerator();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000C510 File Offset: 0x0000A710
		public int Add(SqliteParameter parameter)
		{
			int num = -1;
			if (!string.IsNullOrEmpty(parameter.ParameterName))
			{
				num = this.IndexOf(parameter.ParameterName);
			}
			if (num == -1)
			{
				num = this._parameterList.Count;
				this._parameterList.Add(parameter);
			}
			this.SetParameter(num, parameter);
			return num;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000C564 File Offset: 0x0000A764
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int Add(object value)
		{
			return this.Add((SqliteParameter)value);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000C574 File Offset: 0x0000A774
		public SqliteParameter AddWithValue(string parameterName, object value)
		{
			SqliteParameter sqliteParameter = new SqliteParameter(parameterName, value);
			this.Add(sqliteParameter);
			return sqliteParameter;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000C592 File Offset: 0x0000A792
		public override void Clear()
		{
			this._unboundFlag = true;
			this._parameterList.Clear();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000C5A6 File Offset: 0x0000A7A6
		public override bool Contains(object value)
		{
			return this._parameterList.Contains((SqliteParameter)value);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000C5B9 File Offset: 0x0000A7B9
		public override void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		public override int Count
		{
			get
			{
				return this._parameterList.Count;
			}
		}

		// Token: 0x17000028 RID: 40
		public SqliteParameter this[int index]
		{
			get
			{
				return (SqliteParameter)this.GetParameter(index);
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000C5DB File Offset: 0x0000A7DB
		protected override DbParameter GetParameter(int index)
		{
			return this._parameterList[index];
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000C5EC File Offset: 0x0000A7EC
		public override int IndexOf(string parameterName)
		{
			int count = this._parameterList.Count;
			for (int i = 0; i < count; i++)
			{
				if (string.Compare(parameterName, this._parameterList[i].ParameterName, true, CultureInfo.InvariantCulture) == 0)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000C63C File Offset: 0x0000A83C
		public override int IndexOf(object value)
		{
			return this._parameterList.IndexOf((SqliteParameter)value);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000C64F File Offset: 0x0000A84F
		public override void Insert(int index, object value)
		{
			this._unboundFlag = true;
			this._parameterList.Insert(index, (SqliteParameter)value);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000C66A File Offset: 0x0000A86A
		public override void Remove(object value)
		{
			this._unboundFlag = true;
			this._parameterList.Remove((SqliteParameter)value);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000C685 File Offset: 0x0000A885
		public override void RemoveAt(int index)
		{
			this._unboundFlag = true;
			this._parameterList.RemoveAt(index);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000C69A File Offset: 0x0000A89A
		protected override void SetParameter(int index, DbParameter value)
		{
			this._unboundFlag = true;
			this._parameterList[index] = (SqliteParameter)value;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000C6B5 File Offset: 0x0000A8B5
		internal void Unbind()
		{
			this._unboundFlag = true;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000C6C0 File Offset: 0x0000A8C0
		internal void MapParameters(SqliteStatement activeStatement)
		{
			if (!this._unboundFlag || this._parameterList.Count == 0 || this._command._statementList == null)
			{
				return;
			}
			int num = 0;
			int num2 = -1;
			foreach (SqliteParameter sqliteParameter in this._parameterList)
			{
				num2++;
				string text = sqliteParameter.ParameterName;
				if (text == null)
				{
					text = string.Format(CultureInfo.InvariantCulture, ";{0}", new object[] { num });
					num++;
				}
				bool flag = false;
				int num3;
				if (activeStatement == null)
				{
					num3 = this._command._statementList.Count;
				}
				else
				{
					num3 = 1;
				}
				SqliteStatement sqliteStatement = activeStatement;
				for (int i = 0; i < num3; i++)
				{
					flag = false;
					if (sqliteStatement == null)
					{
						sqliteStatement = this._command._statementList[i];
					}
					if (sqliteStatement._paramNames != null && sqliteStatement.MapParameter(text, sqliteParameter))
					{
						flag = true;
					}
					sqliteStatement = null;
				}
				if (!flag)
				{
					text = string.Format(CultureInfo.InvariantCulture, ";{0}", new object[] { num2 });
					sqliteStatement = activeStatement;
					for (int i = 0; i < num3; i++)
					{
						if (sqliteStatement == null)
						{
							sqliteStatement = this._command._statementList[i];
						}
						if (sqliteStatement._paramNames == null || sqliteStatement.MapParameter(text, sqliteParameter))
						{
						}
						sqliteStatement = null;
					}
				}
			}
			if (activeStatement == null)
			{
				this._unboundFlag = false;
			}
		}

		// Token: 0x040000A4 RID: 164
		private SqliteCommand _command;

		// Token: 0x040000A5 RID: 165
		private List<SqliteParameter> _parameterList;

		// Token: 0x040000A6 RID: 166
		private bool _unboundFlag;
	}
}
