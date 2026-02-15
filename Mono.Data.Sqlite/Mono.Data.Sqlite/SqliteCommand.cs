using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000008 RID: 8
	[Designer("SQLite.Designer.SqliteCommandDesigner, SQLite.Designer, Version=1.0.36.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139")]
	[ToolboxItem(true)]
	public sealed class SqliteCommand : DbCommand, ICloneable
	{
		// Token: 0x0600009F RID: 159 RVA: 0x000035E4 File Offset: 0x000017E4
		public SqliteCommand(string commandText, SqliteConnection connection)
			: this(commandText, connection, null)
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000035EF File Offset: 0x000017EF
		public SqliteCommand(SqliteConnection connection)
			: this(null, connection, null)
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000035FC File Offset: 0x000017FC
		private SqliteCommand(SqliteCommand source)
			: this(source.CommandText, source.Connection, source.Transaction)
		{
			this.CommandTimeout = source.CommandTimeout;
			this.DesignTimeVisible = source.DesignTimeVisible;
			this.UpdatedRowSource = source.UpdatedRowSource;
			foreach (object obj in source._parameterCollection)
			{
				SqliteParameter sqliteParameter = (SqliteParameter)obj;
				this.Parameters.Add(sqliteParameter.Clone());
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000036A8 File Offset: 0x000018A8
		public SqliteCommand(string commandText, SqliteConnection connection, SqliteTransaction transaction)
		{
			this._statementList = null;
			this._activeReader = null;
			this._commandTimeout = 30;
			this._parameterCollection = new SqliteParameterCollection(this);
			this._designTimeVisible = true;
			this._updateRowSource = UpdateRowSource.None;
			this._transaction = null;
			if (commandText != null)
			{
				this.CommandText = commandText;
			}
			if (connection != null)
			{
				this.DbConnection = connection;
				this._commandTimeout = connection.DefaultTimeout;
			}
			if (transaction != null)
			{
				this.Transaction = transaction;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003728 File Offset: 0x00001928
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				SqliteDataReader sqliteDataReader = null;
				if (this._activeReader != null)
				{
					try
					{
						sqliteDataReader = this._activeReader.Target as SqliteDataReader;
					}
					catch
					{
					}
				}
				if (sqliteDataReader != null)
				{
					sqliteDataReader._disposeCommand = true;
					this._activeReader = null;
					return;
				}
				this.Connection = null;
				this._parameterCollection.Clear();
				this._commandText = null;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000037AC File Offset: 0x000019AC
		internal void ClearCommands()
		{
			if (this._activeReader != null)
			{
				SqliteDataReader sqliteDataReader = null;
				try
				{
					sqliteDataReader = this._activeReader.Target as SqliteDataReader;
				}
				catch
				{
				}
				if (sqliteDataReader != null)
				{
					sqliteDataReader.Close();
				}
				this._activeReader = null;
			}
			if (this._statementList == null)
			{
				return;
			}
			int count = this._statementList.Count;
			for (int i = 0; i < count; i++)
			{
				this._statementList[i].Dispose();
			}
			this._statementList = null;
			this._parameterCollection.Unbind();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003854 File Offset: 0x00001A54
		internal SqliteStatement BuildNextCommand()
		{
			SqliteStatement sqliteStatement = null;
			SqliteStatement sqliteStatement2;
			try
			{
				if (this._statementList == null)
				{
					this._remainingText = this._commandText;
				}
				sqliteStatement = this._cnn._sql.Prepare(this._cnn, this._remainingText, (this._statementList != null) ? this._statementList[this._statementList.Count - 1] : null, (uint)(this._commandTimeout * 1000), out this._remainingText);
				if (sqliteStatement != null)
				{
					sqliteStatement._command = this;
					if (this._statementList == null)
					{
						this._statementList = new List<SqliteStatement>();
					}
					this._statementList.Add(sqliteStatement);
					this._parameterCollection.MapParameters(sqliteStatement);
					sqliteStatement.BindParameters();
				}
				sqliteStatement2 = sqliteStatement;
			}
			catch (Exception)
			{
				if (sqliteStatement != null)
				{
					if (this._statementList.Contains(sqliteStatement))
					{
						this._statementList.Remove(sqliteStatement);
					}
					sqliteStatement.Dispose();
				}
				this._remainingText = null;
				throw;
			}
			return sqliteStatement2;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000396C File Offset: 0x00001B6C
		internal SqliteStatement GetStatement(int index)
		{
			if (this._statementList == null)
			{
				return this.BuildNextCommand();
			}
			if (index != this._statementList.Count)
			{
				SqliteStatement sqliteStatement = this._statementList[index];
				sqliteStatement.BindParameters();
				return sqliteStatement;
			}
			if (!string.IsNullOrEmpty(this._remainingText))
			{
				return this.BuildNextCommand();
			}
			return null;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000039C9 File Offset: 0x00001BC9
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000039D4 File Offset: 0x00001BD4
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		[Editor("Microsoft.VSDesigner.Data.SQL.Design.SqlCommandTextEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public override string CommandText
		{
			get
			{
				return this._commandText;
			}
			set
			{
				if (this._commandText == value)
				{
					return;
				}
				if (this._activeReader != null && this._activeReader.IsAlive)
				{
					throw new InvalidOperationException("Cannot set CommandText while a DataReader is active");
				}
				this.ClearCommands();
				this._commandText = value;
				if (this._cnn == null)
				{
					return;
				}
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003A32 File Offset: 0x00001C32
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00003A3A File Offset: 0x00001C3A
		[DefaultValue(30)]
		public override int CommandTimeout
		{
			get
			{
				return this._commandTimeout;
			}
			set
			{
				this._commandTimeout = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00003A43 File Offset: 0x00001C43
		[DefaultValue(CommandType.Text)]
		[RefreshProperties(RefreshProperties.All)]
		public override CommandType CommandType
		{
			set
			{
				if (value != CommandType.Text)
				{
					throw new NotSupportedException();
				}
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003A52 File Offset: 0x00001C52
		protected override DbParameter CreateDbParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003A5A File Offset: 0x00001C5A
		public new SqliteParameter CreateParameter()
		{
			return new SqliteParameter();
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003A61 File Offset: 0x00001C61
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00003A6C File Offset: 0x00001C6C
		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DbConnectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new SqliteConnection Connection
		{
			get
			{
				return this._cnn;
			}
			set
			{
				if (this._activeReader != null && this._activeReader.IsAlive)
				{
					throw new InvalidOperationException("Cannot set Connection while a DataReader is active");
				}
				if (this._cnn != null)
				{
					this.ClearCommands();
				}
				this._cnn = value;
				if (this._cnn != null)
				{
					this._version = this._cnn._version;
				}
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003AD3 File Offset: 0x00001CD3
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00003ADB File Offset: 0x00001CDB
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (SqliteConnection)value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003AE9 File Offset: 0x00001CE9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new SqliteParameterCollection Parameters
		{
			get
			{
				return this._parameterCollection;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003AF1 File Offset: 0x00001CF1
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003AF9 File Offset: 0x00001CF9
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003B04 File Offset: 0x00001D04
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new SqliteTransaction Transaction
		{
			get
			{
				return this._transaction;
			}
			set
			{
				if (this._cnn != null)
				{
					if (this._activeReader != null && this._activeReader.IsAlive)
					{
						throw new InvalidOperationException("Cannot set Transaction while a DataReader is active");
					}
					if (value != null && value._cnn != this._cnn)
					{
						throw new ArgumentException("Transaction is not associated with the command's connection");
					}
					this._transaction = value;
				}
				else
				{
					this.Connection = value.Connection;
					this._transaction = value;
				}
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003B83 File Offset: 0x00001D83
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00003B8B File Offset: 0x00001D8B
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this.Transaction;
			}
			set
			{
				this.Transaction = (SqliteTransaction)value;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003B9C File Offset: 0x00001D9C
		private void InitializeForReader()
		{
			if (this._activeReader != null && this._activeReader.IsAlive)
			{
				throw new InvalidOperationException("DataReader already active on this command");
			}
			if (this._cnn == null)
			{
				throw new InvalidOperationException("No connection associated with this command");
			}
			if (this._cnn.State != ConnectionState.Open)
			{
				throw new InvalidOperationException("Database is not open");
			}
			if (this._cnn._version != this._version)
			{
				this._version = this._cnn._version;
				this.ClearCommands();
			}
			this._parameterCollection.MapParameters(null);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003C3A File Offset: 0x00001E3A
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003C44 File Offset: 0x00001E44
		public new SqliteDataReader ExecuteReader(CommandBehavior behavior)
		{
			this.InitializeForReader();
			SqliteDataReader sqliteDataReader = new SqliteDataReader(this, behavior);
			this._activeReader = new WeakReference(sqliteDataReader, false);
			return sqliteDataReader;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003C6D File Offset: 0x00001E6D
		public SqliteDataReader ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003C76 File Offset: 0x00001E76
		internal void ClearDataReader()
		{
			this._activeReader = null;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003C80 File Offset: 0x00001E80
		public override int ExecuteNonQuery()
		{
			int recordsAffected;
			using (SqliteDataReader sqliteDataReader = this.ExecuteReader(CommandBehavior.SingleResult | CommandBehavior.SingleRow))
			{
				while (sqliteDataReader.NextResult())
				{
				}
				recordsAffected = sqliteDataReader.RecordsAffected;
			}
			return recordsAffected;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003CD8 File Offset: 0x00001ED8
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003CE0 File Offset: 0x00001EE0
		[DefaultValue(UpdateRowSource.None)]
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this._updateRowSource;
			}
			set
			{
				this._updateRowSource = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003CE9 File Offset: 0x00001EE9
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003CF1 File Offset: 0x00001EF1
		[DesignOnly(true)]
		[Browsable(false)]
		[DefaultValue(true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool DesignTimeVisible
		{
			get
			{
				return this._designTimeVisible;
			}
			set
			{
				this._designTimeVisible = value;
				TypeDescriptor.Refresh(this);
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003D00 File Offset: 0x00001F00
		public object Clone()
		{
			return new SqliteCommand(this);
		}

		// Token: 0x0400000F RID: 15
		private string _commandText;

		// Token: 0x04000010 RID: 16
		private SqliteConnection _cnn;

		// Token: 0x04000011 RID: 17
		private long _version;

		// Token: 0x04000012 RID: 18
		private WeakReference _activeReader;

		// Token: 0x04000013 RID: 19
		internal int _commandTimeout;

		// Token: 0x04000014 RID: 20
		private bool _designTimeVisible;

		// Token: 0x04000015 RID: 21
		private UpdateRowSource _updateRowSource;

		// Token: 0x04000016 RID: 22
		private SqliteParameterCollection _parameterCollection;

		// Token: 0x04000017 RID: 23
		internal List<SqliteStatement> _statementList;

		// Token: 0x04000018 RID: 24
		internal string _remainingText;

		// Token: 0x04000019 RID: 25
		private SqliteTransaction _transaction;
	}
}
