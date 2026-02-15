using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x02000018 RID: 24
	internal static class ExceptionBuilder
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x0000468F File Offset: 0x0000288F
		private static void TraceException(string trace, Exception e)
		{
			if (e != null)
			{
				DataCommonEventSource.Log.Trace<Exception>(trace, e);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000046A0 File Offset: 0x000028A0
		internal static Exception TraceExceptionAsReturnValue(Exception e)
		{
			ExceptionBuilder.TraceException("<comm.ADP.TraceException|ERR|THROW> '{0}'", e);
			return e;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000046AE File Offset: 0x000028AE
		internal static Exception TraceExceptionForCapture(Exception e)
		{
			ExceptionBuilder.TraceException("<comm.ADP.TraceException|ERR|CATCH> '{0}'", e);
			return e;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000046AE File Offset: 0x000028AE
		internal static Exception TraceExceptionWithoutRethrow(Exception e)
		{
			ExceptionBuilder.TraceException("<comm.ADP.TraceException|ERR|CATCH> '{0}'", e);
			return e;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000046BC File Offset: 0x000028BC
		internal static Exception _Argument(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ArgumentException(error));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000046C9 File Offset: 0x000028C9
		internal static Exception _Argument(string error, Exception innerException)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ArgumentException(error, innerException));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000046D7 File Offset: 0x000028D7
		private static Exception _ArgumentNull(string paramName, string msg)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ArgumentNullException(paramName, msg));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000046E5 File Offset: 0x000028E5
		internal static Exception _ArgumentOutOfRange(string paramName, string msg)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ArgumentOutOfRangeException(paramName, msg));
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000046F3 File Offset: 0x000028F3
		private static Exception _IndexOutOfRange(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new IndexOutOfRangeException(error));
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00004700 File Offset: 0x00002900
		private static Exception _InvalidOperation(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new InvalidOperationException(error));
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000470D File Offset: 0x0000290D
		private static Exception _InvalidEnumArgumentException(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new InvalidEnumArgumentException(error));
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000471A File Offset: 0x0000291A
		private static Exception _InvalidEnumArgumentException<T>(T value)
		{
			return ExceptionBuilder._InvalidEnumArgumentException(SR.Format("The {0} enumeration value, {1}, is invalid.", typeof(T).Name, value.ToString()));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004747 File Offset: 0x00002947
		private static void ThrowDataException(string error, Exception innerException)
		{
			throw ExceptionBuilder.TraceExceptionAsReturnValue(new DataException(error, innerException));
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004755 File Offset: 0x00002955
		private static Exception _Data(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new DataException(error));
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004762 File Offset: 0x00002962
		private static Exception _Constraint(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ConstraintException(error));
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000476F File Offset: 0x0000296F
		private static Exception _InvalidConstraint(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new InvalidConstraintException(error));
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000477C File Offset: 0x0000297C
		private static Exception _DeletedRowInaccessible(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new DeletedRowInaccessibleException(error));
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004789 File Offset: 0x00002989
		private static Exception _DuplicateName(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new DuplicateNameException(error));
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004796 File Offset: 0x00002996
		private static Exception _InRowChangingEvent(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new InRowChangingEventException(error));
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000047A3 File Offset: 0x000029A3
		private static Exception _NoNullAllowed(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new NoNullAllowedException(error));
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000047B0 File Offset: 0x000029B0
		private static Exception _ReadOnly(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new ReadOnlyException(error));
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000047BD File Offset: 0x000029BD
		private static Exception _RowNotInTable(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new RowNotInTableException(error));
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000047CA File Offset: 0x000029CA
		private static Exception _VersionNotFound(string error)
		{
			return ExceptionBuilder.TraceExceptionAsReturnValue(new VersionNotFoundException(error));
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000047D7 File Offset: 0x000029D7
		public static Exception ArgumentNull(string paramName)
		{
			return ExceptionBuilder._ArgumentNull(paramName, SR.Format("'{0}' argument cannot be null.", paramName));
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000047EA File Offset: 0x000029EA
		public static Exception ArgumentOutOfRange(string paramName)
		{
			return ExceptionBuilder._ArgumentOutOfRange(paramName, SR.Format("'{0}' argument is out of range.", paramName));
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000047FD File Offset: 0x000029FD
		public static Exception BadObjectPropertyAccess(string error)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Property not accessible because '{0}'.", error));
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000480F File Offset: 0x00002A0F
		public static Exception TypeNotAllowed(Type type)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Type '{0}' is not allowed here. See https://go.microsoft.com/fwlink/?linkid=2132227 for more details.", type.AssemblyQualifiedName));
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004826 File Offset: 0x00002A26
		public static Exception CannotModifyCollection()
		{
			return ExceptionBuilder._Argument("Collection itself is not modifiable.");
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004832 File Offset: 0x00002A32
		public static Exception CaseInsensitiveNameConflict(string name)
		{
			return ExceptionBuilder._Argument(SR.Format("The given name '{0}' matches at least two names in the collection object with different cases, but does not match either of them with the same case.", name));
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004844 File Offset: 0x00002A44
		public static Exception NamespaceNameConflict(string name)
		{
			return ExceptionBuilder._Argument(SR.Format("The given name '{0}' matches at least two names in the collection object with different namespaces.", name));
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004856 File Offset: 0x00002A56
		public static Exception InvalidOffsetLength()
		{
			return ExceptionBuilder._Argument("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004862 File Offset: 0x00002A62
		public static Exception ColumnNotInTheTable(string column, string table)
		{
			return ExceptionBuilder._Argument(SR.Format("Column '{0}' does not belong to table {1}.", column, table));
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004875 File Offset: 0x00002A75
		public static Exception ColumnNotInAnyTable()
		{
			return ExceptionBuilder._Argument("Column must belong to a table.");
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004881 File Offset: 0x00002A81
		public static Exception ColumnOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Cannot find column {0}.", index.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000489E File Offset: 0x00002A9E
		public static Exception ColumnOutOfRange(string column)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Cannot find column {0}.", column));
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000048B0 File Offset: 0x00002AB0
		public static Exception CannotAddColumn1(string column)
		{
			return ExceptionBuilder._Argument(SR.Format("Column '{0}' already belongs to this DataTable.", column));
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000048C2 File Offset: 0x00002AC2
		public static Exception CannotAddColumn2(string column)
		{
			return ExceptionBuilder._Argument(SR.Format("Column '{0}' already belongs to another DataTable.", column));
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000048D4 File Offset: 0x00002AD4
		public static Exception CannotAddColumn3()
		{
			return ExceptionBuilder._Argument("Cannot have more than one SimpleContent columns in a DataTable.");
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000048E0 File Offset: 0x00002AE0
		public static Exception CannotAddColumn4(string column)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot add a SimpleContent column to a table containing element columns or nested relations.", column));
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000048F2 File Offset: 0x00002AF2
		public static Exception CannotAddDuplicate(string column)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A column named '{0}' already belongs to this DataTable.", column));
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004904 File Offset: 0x00002B04
		public static Exception CannotAddDuplicate2(string table)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("Cannot add a column named '{0}': a nested table with the same name already belongs to this DataTable.", table));
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004916 File Offset: 0x00002B16
		public static Exception CannotAddDuplicate3(string table)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A column named '{0}' already belongs to this DataTable: cannot set a nested table name to the same name.", table));
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004928 File Offset: 0x00002B28
		public static Exception CannotRemoveColumn()
		{
			return ExceptionBuilder._Argument("Cannot remove a column that doesn't belong to this table.");
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004934 File Offset: 0x00002B34
		public static Exception CannotRemovePrimaryKey()
		{
			return ExceptionBuilder._Argument("Cannot remove this column, because it's part of the primary key.");
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004940 File Offset: 0x00002B40
		public static Exception CannotRemoveChildKey(string relation)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot remove this column, because it is part of the parent key for relationship {0}.", relation));
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004952 File Offset: 0x00002B52
		public static Exception CannotRemoveConstraint(string constraint, string table)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot remove this column, because it is a part of the constraint {0} on the table {1}.", constraint, table));
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004965 File Offset: 0x00002B65
		public static Exception CannotRemoveExpression(string column, string expression)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot remove this column, because it is part of an expression: {0} = {1}.", column, expression));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004978 File Offset: 0x00002B78
		public static Exception AddPrimaryKeyConstraint()
		{
			return ExceptionBuilder._Argument("Cannot add primary key constraint since primary key is already set for the table.");
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004984 File Offset: 0x00002B84
		public static Exception NoConstraintName()
		{
			return ExceptionBuilder._Argument("Cannot change the name of a constraint to empty string when it is in the ConstraintCollection.");
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004990 File Offset: 0x00002B90
		public static Exception ConstraintViolation(string constraint)
		{
			return ExceptionBuilder._Constraint(SR.Format("Cannot enforce constraints on constraint {0}.", constraint));
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000049A4 File Offset: 0x00002BA4
		public static string KeysToString(object[] keys)
		{
			string text = string.Empty;
			for (int i = 0; i < keys.Length; i++)
			{
				text = text + Convert.ToString(keys[i], null) + ((i < keys.Length - 1) ? ", " : string.Empty);
			}
			return text;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000049EC File Offset: 0x00002BEC
		public static string UniqueConstraintViolationText(DataColumn[] columns, object[] values)
		{
			if (columns.Length > 1)
			{
				string text = string.Empty;
				for (int i = 0; i < columns.Length; i++)
				{
					text = text + columns[i].ColumnName + ((i < columns.Length - 1) ? ", " : "");
				}
				return SR.Format("Column '{0}' is constrained to be unique.  Value '{1}' is already present.", text, ExceptionBuilder.KeysToString(values));
			}
			return SR.Format("Column '{0}' is constrained to be unique.  Value '{1}' is already present.", columns[0].ColumnName, Convert.ToString(values[0], null));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004A63 File Offset: 0x00002C63
		public static Exception ConstraintViolation(DataColumn[] columns, object[] values)
		{
			return ExceptionBuilder._Constraint(ExceptionBuilder.UniqueConstraintViolationText(columns, values));
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004A71 File Offset: 0x00002C71
		public static Exception ConstraintOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Cannot find constraint {0}.", index.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004A8E File Offset: 0x00002C8E
		public static Exception DuplicateConstraint(string constraint)
		{
			return ExceptionBuilder._Data(SR.Format("Constraint matches constraint named {0} already in collection.", constraint));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00004AA0 File Offset: 0x00002CA0
		public static Exception DuplicateConstraintName(string constraint)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A Constraint named '{0}' already belongs to this DataTable.", constraint));
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004AB2 File Offset: 0x00002CB2
		public static Exception NeededForForeignKeyConstraint(UniqueConstraint key, ForeignKeyConstraint fk)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot remove unique constraint '{0}'. Remove foreign key constraint '{1}' first.", key.ConstraintName, fk.ConstraintName));
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004ACF File Offset: 0x00002CCF
		public static Exception UniqueConstraintViolation()
		{
			return ExceptionBuilder._Argument("These columns don't currently have unique values.");
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004ADB File Offset: 0x00002CDB
		public static Exception ConstraintForeignTable()
		{
			return ExceptionBuilder._Argument("These columns don't point to this table.");
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004AE7 File Offset: 0x00002CE7
		public static Exception ConstraintParentValues()
		{
			return ExceptionBuilder._Argument("This constraint cannot be enabled as not all values have corresponding parent values.");
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004AF3 File Offset: 0x00002CF3
		public static Exception ConstraintAddFailed(DataTable table)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("This constraint cannot be added since ForeignKey doesn't belong to table {0}.", table.TableName));
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004B0A File Offset: 0x00002D0A
		public static Exception ConstraintRemoveFailed()
		{
			return ExceptionBuilder._Argument("Cannot remove a constraint that doesn't belong to this table.");
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004B16 File Offset: 0x00002D16
		public static Exception FailedCascadeDelete(string constraint)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot delete this row because constraints are enforced on relation {0}, and deleting this row will strand child rows.", constraint));
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004B28 File Offset: 0x00002D28
		public static Exception FailedCascadeUpdate(string constraint)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot make this change because constraints are enforced on relation {0}, and changing this value will strand child rows.", constraint));
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004B3A File Offset: 0x00002D3A
		public static Exception FailedClearParentTable(string table, string constraint, string childTable)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot clear table {0} because ForeignKeyConstraint {1} enforces constraints and there are child rows in {2}.", table, constraint, childTable));
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004B4E File Offset: 0x00002D4E
		public static Exception ForeignKeyViolation(string constraint, object[] keys)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("ForeignKeyConstraint {0} requires the child key values ({1}) to exist in the parent table.", constraint, ExceptionBuilder.KeysToString(keys)));
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004B66 File Offset: 0x00002D66
		public static Exception RemoveParentRow(ForeignKeyConstraint constraint)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot remove this row because it has child rows, and constraints on relation {0} are enforced.", constraint.ConstraintName));
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004B7D File Offset: 0x00002D7D
		public static string MaxLengthViolationText(string columnName)
		{
			return SR.Format("Column '{0}' exceeds the MaxLength limit.", columnName);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004B8A File Offset: 0x00002D8A
		public static string NotAllowDBNullViolationText(string columnName)
		{
			return SR.Format("Column '{0}' does not allow DBNull.Value.", columnName);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004B97 File Offset: 0x00002D97
		public static Exception CantAddConstraintToMultipleNestedTable(string tableName)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot add constraint to DataTable '{0}' which is a child table in two nested relations.", tableName));
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004BA9 File Offset: 0x00002DA9
		public static Exception AutoIncrementAndExpression()
		{
			return ExceptionBuilder._Argument("Cannot set AutoIncrement property for a computed column.");
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004BB5 File Offset: 0x00002DB5
		public static Exception AutoIncrementAndDefaultValue()
		{
			return ExceptionBuilder._Argument("Cannot set AutoIncrement property for a column with DefaultValue set.");
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004BC1 File Offset: 0x00002DC1
		public static Exception AutoIncrementSeed()
		{
			return ExceptionBuilder._Argument("AutoIncrementStep must be a non-zero value.");
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004BCD File Offset: 0x00002DCD
		public static Exception CantChangeDataType()
		{
			return ExceptionBuilder._Argument("Cannot change DataType of a column once it has data.");
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004BD9 File Offset: 0x00002DD9
		public static Exception NullDataType()
		{
			return ExceptionBuilder._Argument("Column requires a valid DataType.");
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00004BE5 File Offset: 0x00002DE5
		public static Exception ColumnNameRequired()
		{
			return ExceptionBuilder._Argument("ColumnName is required when it is part of a DataTable.");
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004BF1 File Offset: 0x00002DF1
		public static Exception DefaultValueAndAutoIncrement()
		{
			return ExceptionBuilder._Argument("Cannot set a DefaultValue on an AutoIncrement column.");
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00004C00 File Offset: 0x00002E00
		public static Exception DefaultValueDataType(string column, Type defaultType, Type columnType, Exception inner)
		{
			if (column.Length != 0)
			{
				return ExceptionBuilder._Argument(SR.Format("The DefaultValue for column {0} is of type {1} and cannot be converted to {2}.", column, defaultType.FullName, columnType.FullName), inner);
			}
			return ExceptionBuilder._Argument(SR.Format("The DefaultValue for the column is of type {0} and cannot be converted to {1}.", defaultType.FullName, columnType.FullName), inner);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004C4F File Offset: 0x00002E4F
		public static Exception DefaultValueColumnDataType(string column, Type defaultType, Type columnType, Exception inner)
		{
			return ExceptionBuilder._Argument(SR.Format("The DefaultValue for column {0} is of type {1}, but the column is of type {2}.", column, defaultType.FullName, columnType.FullName), inner);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004C6E File Offset: 0x00002E6E
		public static Exception ExpressionAndUnique()
		{
			return ExceptionBuilder._Argument("Cannot create an expression on a column that has AutoIncrement or Unique.");
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004C7A File Offset: 0x00002E7A
		public static Exception ExpressionAndReadOnly()
		{
			return ExceptionBuilder._Argument("Cannot set expression because column cannot be made ReadOnly.");
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004C86 File Offset: 0x00002E86
		public static Exception ExpressionAndConstraint(DataColumn column, Constraint constraint)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Expression property on column {0}, because it is a part of a constraint.", column.ColumnName, constraint.ConstraintName));
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004CA3 File Offset: 0x00002EA3
		public static Exception ExpressionInConstraint(DataColumn column)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot create a constraint based on Expression column {0}.", column.ColumnName));
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004CBA File Offset: 0x00002EBA
		public static Exception ExpressionCircular()
		{
			return ExceptionBuilder._Argument("Cannot set Expression property due to circular reference in the expression.");
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004CC6 File Offset: 0x00002EC6
		public static Exception NonUniqueValues(string column)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Column '{0}' contains non-unique values.", column));
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004CD8 File Offset: 0x00002ED8
		public static Exception NullKeyValues(string column)
		{
			return ExceptionBuilder._Data(SR.Format("Column '{0}' has null values in it.", column));
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004CEA File Offset: 0x00002EEA
		public static Exception NullValues(string column)
		{
			return ExceptionBuilder._NoNullAllowed(SR.Format("Column '{0}' does not allow nulls.", column));
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004CFC File Offset: 0x00002EFC
		public static Exception ReadOnlyAndExpression()
		{
			return ExceptionBuilder._ReadOnly("Cannot change ReadOnly property for the expression column.");
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004D08 File Offset: 0x00002F08
		public static Exception ReadOnly(string column)
		{
			return ExceptionBuilder._ReadOnly(SR.Format("Column '{0}' is read only.", column));
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004D1A File Offset: 0x00002F1A
		public static Exception UniqueAndExpression()
		{
			return ExceptionBuilder._Argument("Cannot change Unique property for the expression column.");
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004D26 File Offset: 0x00002F26
		public static Exception SetFailed(object value, DataColumn column, Type type, Exception innerException)
		{
			return ExceptionBuilder._Argument(innerException.Message + SR.Format("Couldn't store <{0}> in {1} Column.  Expected type is {2}.", value.ToString(), column.ColumnName, type.Name), innerException);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004D55 File Offset: 0x00002F55
		public static Exception CannotSetToNull(DataColumn column)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Column '{0}' to be null. Please use DBNull instead.", column.ColumnName));
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004D6C File Offset: 0x00002F6C
		public static Exception LongerThanMaxLength(DataColumn column)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set column '{0}'. The value violates the MaxLength limit of this column.", column.ColumnName));
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004D83 File Offset: 0x00002F83
		public static Exception CannotSetMaxLength(DataColumn column, int value)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Column '{0}' property MaxLength to '{1}'. There is at least one string in the table longer than the new limit.", column.ColumnName, value.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004DA6 File Offset: 0x00002FA6
		public static Exception CannotSetMaxLength2(DataColumn column)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Column '{0}' property MaxLength. The Column is SimpleContent.", column.ColumnName));
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004DBD File Offset: 0x00002FBD
		public static Exception CannotSetSimpleContentType(string columnName, Type type)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Column '{0}' property DataType to {1}. The Column is SimpleContent.", columnName, type));
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004DD0 File Offset: 0x00002FD0
		public static Exception CannotSetSimpleContent(string columnName, Type type)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot set Column '{0}' property MappingType to SimpleContent. The Column DataType is {1}.", columnName, type));
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004DE3 File Offset: 0x00002FE3
		public static Exception CannotChangeNamespace(string columnName)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot change the Column '{0}' property Namespace. The Column is SimpleContent.", columnName));
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004DF5 File Offset: 0x00002FF5
		public static Exception HasToBeStringType(DataColumn column)
		{
			return ExceptionBuilder._Argument(SR.Format("MaxLength applies to string data type only. You cannot set Column '{0}' property MaxLength to be non-negative number.", column.ColumnName));
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004E0C File Offset: 0x0000300C
		public static Exception AutoIncrementCannotSetIfHasData(string typeName)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot change AutoIncrement of a DataColumn with type '{0}' once it has data.", typeName));
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004E1E File Offset: 0x0000301E
		public static Exception INullableUDTwithoutStaticNull(string typeName)
		{
			return ExceptionBuilder._Argument(SR.Format("Type '{0}' does not contain static Null property or field.", typeName));
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004E30 File Offset: 0x00003030
		public static Exception IComparableNotImplemented(string typeName)
		{
			return ExceptionBuilder._Data(SR.Format(" Type '{0}' does not implement IComparable interface. Comparison cannot be done.", typeName));
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004E42 File Offset: 0x00003042
		public static Exception UDTImplementsIChangeTrackingButnotIRevertible(string typeName)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Type '{0}' does not implement IRevertibleChangeTracking; therefore can not proceed with RejectChanges().", typeName));
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004E54 File Offset: 0x00003054
		public static Exception InvalidDataColumnMapping(Type type)
		{
			return ExceptionBuilder._Argument(SR.Format("DataColumn with type '{0}' is a complexType. Can not serialize value of a complex type as Attribute", type.AssemblyQualifiedName));
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004E6B File Offset: 0x0000306B
		public static Exception CannotSetDateTimeModeForNonDateTimeColumns()
		{
			return ExceptionBuilder._InvalidOperation("The DateTimeMode can be set only on DataColumns of type DateTime.");
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004E77 File Offset: 0x00003077
		public static Exception InvalidDateTimeMode(DataSetDateTime mode)
		{
			return ExceptionBuilder._InvalidEnumArgumentException<DataSetDateTime>(mode);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004E7F File Offset: 0x0000307F
		public static Exception CantChangeDateTimeMode(DataSetDateTime oldValue, DataSetDateTime newValue)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Cannot change DateTimeMode from '{0}' to '{1}' once the table has data.", oldValue.ToString(), newValue.ToString()));
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004EAA File Offset: 0x000030AA
		public static Exception ColumnTypeNotSupported()
		{
			return ADP.NotSupported("DataSet does not support System.Nullable<>.");
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004EB6 File Offset: 0x000030B6
		public static Exception SetFailed(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Cannot set {0}.", name));
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004EC8 File Offset: 0x000030C8
		public static Exception CanNotUseDataViewManager()
		{
			return ExceptionBuilder._Data("DataSet must be set prior to using DataViewManager.");
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004ED4 File Offset: 0x000030D4
		public static Exception CanNotUse()
		{
			return ExceptionBuilder._Data("DataTable must be set prior to using DataView.");
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004EE0 File Offset: 0x000030E0
		public static Exception SetIListObject()
		{
			return ExceptionBuilder._Argument("Cannot set an object into this list.");
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004EEC File Offset: 0x000030EC
		public static Exception AddNewNotAllowNull()
		{
			return ExceptionBuilder._Data("Cannot call AddNew on a DataView where AllowNew is false.");
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004EF8 File Offset: 0x000030F8
		public static Exception NotOpen()
		{
			return ExceptionBuilder._Data("DataView is not open.");
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004F04 File Offset: 0x00003104
		public static Exception CreateChildView()
		{
			return ExceptionBuilder._Argument("The relation is not parented to the table to which this DataView points.");
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004F10 File Offset: 0x00003110
		public static Exception CanNotDelete()
		{
			return ExceptionBuilder._Data("Cannot delete on a DataSource where AllowDelete is false.");
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004F1C File Offset: 0x0000311C
		public static Exception GetElementIndex(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Index {0} is either negative or above rows count.", index.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004F39 File Offset: 0x00003139
		public static Exception AddExternalObject()
		{
			return ExceptionBuilder._Argument("Cannot add external objects to this list.");
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004F45 File Offset: 0x00003145
		public static Exception CanNotClear()
		{
			return ExceptionBuilder._Argument("Cannot clear this list.");
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004F51 File Offset: 0x00003151
		public static Exception InsertExternalObject()
		{
			return ExceptionBuilder._Argument("Cannot insert external objects to this list.");
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004F5D File Offset: 0x0000315D
		public static Exception RemoveExternalObject()
		{
			return ExceptionBuilder._Argument("Cannot remove objects not in the list.");
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004F69 File Offset: 0x00003169
		public static Exception KeyTableMismatch()
		{
			return ExceptionBuilder._InvalidConstraint("Cannot create a Key from Columns that belong to different tables.");
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004F75 File Offset: 0x00003175
		public static Exception KeyNoColumns()
		{
			return ExceptionBuilder._InvalidConstraint("Cannot have 0 columns.");
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004F81 File Offset: 0x00003181
		public static Exception KeyTooManyColumns(int cols)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot have more than {0} columns.", cols.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004F9E File Offset: 0x0000319E
		public static Exception KeyDuplicateColumns(string columnName)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("Cannot create a Key when the same column is listed more than once: '{0}'", columnName));
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004FB0 File Offset: 0x000031B0
		public static Exception RelationDataSetMismatch()
		{
			return ExceptionBuilder._InvalidConstraint("Cannot have a relationship between tables in different DataSets.");
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004FBC File Offset: 0x000031BC
		public static Exception ColumnsTypeMismatch()
		{
			return ExceptionBuilder._InvalidConstraint("Parent Columns and Child Columns don't have type-matching columns.");
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004FC8 File Offset: 0x000031C8
		public static Exception KeyLengthMismatch()
		{
			return ExceptionBuilder._Argument("ParentColumns and ChildColumns should be the same length.");
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004FD4 File Offset: 0x000031D4
		public static Exception KeyLengthZero()
		{
			return ExceptionBuilder._Argument("ParentColumns and ChildColumns must not be zero length.");
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004FE0 File Offset: 0x000031E0
		public static Exception ForeignRelation()
		{
			return ExceptionBuilder._Argument("This relation should connect two tables in this DataSet to be added to this DataSet.");
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004FEC File Offset: 0x000031EC
		public static Exception KeyColumnsIdentical()
		{
			return ExceptionBuilder._InvalidConstraint("ParentKey and ChildKey are identical.");
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004FF8 File Offset: 0x000031F8
		public static Exception RelationForeignTable(string t1, string t2)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("GetChildRows requires a row whose Table is {0}, but the specified row's Table is {1}.", t1, t2));
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000500B File Offset: 0x0000320B
		public static Exception GetParentRowTableMismatch(string t1, string t2)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("GetParentRow requires a row whose Table is {0}, but the specified row's Table is {1}.", t1, t2));
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000501E File Offset: 0x0000321E
		public static Exception SetParentRowTableMismatch(string t1, string t2)
		{
			return ExceptionBuilder._InvalidConstraint(SR.Format("SetParentRow requires a child row whose Table is {0}, but the specified row's Table is {1}.", t1, t2));
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005031 File Offset: 0x00003231
		public static Exception RelationForeignRow()
		{
			return ExceptionBuilder._Argument("The row doesn't belong to the same DataSet as this relation.");
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000503D File Offset: 0x0000323D
		public static Exception RelationNestedReadOnly()
		{
			return ExceptionBuilder._Argument("Cannot set the 'Nested' property to false for this relation.");
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005049 File Offset: 0x00003249
		public static Exception TableCantBeNestedInTwoTables(string tableName)
		{
			return ExceptionBuilder._Argument(SR.Format("The same table '{0}' cannot be the child table in two nested relations.", tableName));
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000505B File Offset: 0x0000325B
		public static Exception LoopInNestedRelations(string tableName)
		{
			return ExceptionBuilder._Argument(SR.Format("The table ({0}) cannot be the child table to itself in nested relations.", tableName));
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000506D File Offset: 0x0000326D
		public static Exception RelationDoesNotExist()
		{
			return ExceptionBuilder._Argument("This relation doesn't belong to this relation collection.");
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00005079 File Offset: 0x00003279
		public static Exception ParentOrChildColumnsDoNotHaveDataSet()
		{
			return ExceptionBuilder._InvalidConstraint("Cannot create a DataRelation if Parent or Child Columns are not in a DataSet.");
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005085 File Offset: 0x00003285
		public static Exception InValidNestedRelation(string childTableName)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Nested table '{0}' which inherits its namespace cannot have multiple parent tables in different namespaces.", childTableName));
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005097 File Offset: 0x00003297
		public static Exception InvalidParentNamespaceinNestedRelation(string childTableName)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Nested table '{0}' with empty namespace cannot have multiple parent tables in different namespaces.", childTableName));
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005031 File Offset: 0x00003231
		public static Exception RowNotInTheDataSet()
		{
			return ExceptionBuilder._Argument("The row doesn't belong to the same DataSet as this relation.");
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000050A9 File Offset: 0x000032A9
		public static Exception RowNotInTheTable()
		{
			return ExceptionBuilder._RowNotInTable("Cannot perform this operation on a row not in the table.");
		}

		// Token: 0x06000154 RID: 340 RVA: 0x000050B5 File Offset: 0x000032B5
		public static Exception EditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent("Cannot change a proposed value in the RowChanging event.");
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000050C1 File Offset: 0x000032C1
		public static Exception EndEditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent("Cannot call EndEdit() inside an OnRowChanging event.");
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000050CD File Offset: 0x000032CD
		public static Exception BeginEditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent("Cannot call BeginEdit() inside the RowChanging event.");
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000050D9 File Offset: 0x000032D9
		public static Exception CancelEditInRowChanging()
		{
			return ExceptionBuilder._InRowChangingEvent("Cannot call CancelEdit() inside an OnRowChanging event.  Throw an exception to cancel this update.");
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000050E5 File Offset: 0x000032E5
		public static Exception DeleteInRowDeleting()
		{
			return ExceptionBuilder._InRowChangingEvent("Cannot call Delete inside an OnRowDeleting event.  Throw an exception to cancel this delete.");
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000050F1 File Offset: 0x000032F1
		public static Exception ValueArrayLength()
		{
			return ExceptionBuilder._Argument("Input array is longer than the number of columns in this table.");
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000050FD File Offset: 0x000032FD
		public static Exception NoCurrentData()
		{
			return ExceptionBuilder._VersionNotFound("There is no Current data to access.");
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005109 File Offset: 0x00003309
		public static Exception NoOriginalData()
		{
			return ExceptionBuilder._VersionNotFound("There is no Original data to access.");
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005115 File Offset: 0x00003315
		public static Exception NoProposedData()
		{
			return ExceptionBuilder._VersionNotFound("There is no Proposed data to access.");
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00005121 File Offset: 0x00003321
		public static Exception RowRemovedFromTheTable()
		{
			return ExceptionBuilder._RowNotInTable("This row has been removed from a table and does not have any data.  BeginEdit() will allow creation of new data in this row.");
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000512D File Offset: 0x0000332D
		public static Exception DeletedRowInaccessible()
		{
			return ExceptionBuilder._DeletedRowInaccessible("Deleted row information cannot be accessed through the row.");
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005139 File Offset: 0x00003339
		public static Exception RowAlreadyDeleted()
		{
			return ExceptionBuilder._DeletedRowInaccessible("Cannot delete this row since it's already deleted.");
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005145 File Offset: 0x00003345
		public static Exception RowEmpty()
		{
			return ExceptionBuilder._Argument("This row is empty.");
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00005151 File Offset: 0x00003351
		public static Exception InvalidRowVersion()
		{
			return ExceptionBuilder._Data("Version must be Original, Current, or Proposed.");
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000515D File Offset: 0x0000335D
		public static Exception RowOutOfRange()
		{
			return ExceptionBuilder._IndexOutOfRange("The given DataRow is not in the current DataRowCollection.");
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005169 File Offset: 0x00003369
		public static Exception RowOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("There is no row at position {0}.", index.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005186 File Offset: 0x00003386
		public static Exception RowInsertTwice(int index, string tableName)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("The rowOrder value={0} has been found twice for table named '{1}'.", index.ToString(CultureInfo.InvariantCulture), tableName));
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000051A4 File Offset: 0x000033A4
		public static Exception RowInsertMissing(string tableName)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Values are missing in the rowOrder sequence for table '{0}'.", tableName));
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000051B6 File Offset: 0x000033B6
		public static Exception RowAlreadyRemoved()
		{
			return ExceptionBuilder._Data("Cannot remove a row that's already been removed.");
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000051C2 File Offset: 0x000033C2
		public static Exception MultipleParents()
		{
			return ExceptionBuilder._Data("A child row has multiple parents.");
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000051CE File Offset: 0x000033CE
		public static Exception InvalidRowState(DataRowState state)
		{
			return ExceptionBuilder._InvalidEnumArgumentException<DataRowState>(state);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000051D6 File Offset: 0x000033D6
		public static Exception InvalidRowBitPattern()
		{
			return ExceptionBuilder._Argument("Unrecognized row state bit pattern.");
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000051E2 File Offset: 0x000033E2
		internal static Exception SetDataSetNameToEmpty()
		{
			return ExceptionBuilder._Argument("Cannot change the name of the DataSet to an empty string.");
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000051EE File Offset: 0x000033EE
		internal static Exception SetDataSetNameConflicting(string name)
		{
			return ExceptionBuilder._Argument(SR.Format("The name '{0}' is invalid. A DataSet cannot have the same name of the DataTable.", name));
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00005200 File Offset: 0x00003400
		public static Exception DataSetUnsupportedSchema(string ns)
		{
			return ExceptionBuilder._Argument(SR.Format("The schema namespace is invalid. Please use this one instead: {0}.", ns));
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00005212 File Offset: 0x00003412
		public static Exception MergeMissingDefinition(string obj)
		{
			return ExceptionBuilder._Argument(SR.Format("Target DataSet missing definition for {0}.", obj));
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00005224 File Offset: 0x00003424
		public static Exception TablesInDifferentSets()
		{
			return ExceptionBuilder._Argument("Cannot create a relation between tables in different DataSets.");
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00005230 File Offset: 0x00003430
		public static Exception RelationAlreadyExists()
		{
			return ExceptionBuilder._Argument("A relation already exists for these child columns.");
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000523C File Offset: 0x0000343C
		public static Exception RowAlreadyInOtherCollection()
		{
			return ExceptionBuilder._Argument("This row already belongs to another table.");
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005248 File Offset: 0x00003448
		public static Exception RowAlreadyInTheCollection()
		{
			return ExceptionBuilder._Argument("This row already belongs to this table.");
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005254 File Offset: 0x00003454
		public static Exception RecordStateRange()
		{
			return ExceptionBuilder._Argument("The RowStates parameter must be set to a valid combination of values from the DataViewRowState enumeration.");
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005260 File Offset: 0x00003460
		public static Exception IndexKeyLength(int length, int keyLength)
		{
			if (length != 0)
			{
				return ExceptionBuilder._Argument(SR.Format("Expecting {0} value(s) for the key being indexed, but received {1} value(s).", length.ToString(CultureInfo.InvariantCulture), keyLength.ToString(CultureInfo.InvariantCulture)));
			}
			return ExceptionBuilder._Argument("Find finds a row based on a Sort order, and no Sort order is specified.");
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005297 File Offset: 0x00003497
		public static Exception RemovePrimaryKey(DataTable table)
		{
			if (table.TableName.Length != 0)
			{
				return ExceptionBuilder._Argument(SR.Format("Cannot remove unique constraint since it's the primary key of table {0}.", table.TableName));
			}
			return ExceptionBuilder._Argument("Cannot remove unique constraint since it's the primary key of a table.");
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000052C6 File Offset: 0x000034C6
		public static Exception RelationAlreadyInOtherDataSet()
		{
			return ExceptionBuilder._Argument("This relation already belongs to another DataSet.");
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000052D2 File Offset: 0x000034D2
		public static Exception RelationAlreadyInTheDataSet()
		{
			return ExceptionBuilder._Argument("This relation already belongs to this DataSet.");
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000052DE File Offset: 0x000034DE
		public static Exception RelationNotInTheDataSet(string relation)
		{
			return ExceptionBuilder._Argument(SR.Format("Relation {0} does not belong to this DataSet.", relation));
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000052F0 File Offset: 0x000034F0
		public static Exception RelationOutOfRange(object index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Cannot find relation {0}.", Convert.ToString(index, null)));
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005308 File Offset: 0x00003508
		public static Exception DuplicateRelation(string relation)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A Relation named '{0}' already belongs to this DataSet.", relation));
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000531A File Offset: 0x0000351A
		public static Exception RelationTableNull()
		{
			return ExceptionBuilder._Argument("Cannot create a collection on a null table.");
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000531A File Offset: 0x0000351A
		public static Exception RelationDataSetNull()
		{
			return ExceptionBuilder._Argument("Cannot create a collection on a null table.");
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005326 File Offset: 0x00003526
		public static Exception RelationTableWasRemoved()
		{
			return ExceptionBuilder._Argument("The table this collection displays relations for has been removed from its DataSet.");
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005332 File Offset: 0x00003532
		public static Exception ParentTableMismatch()
		{
			return ExceptionBuilder._Argument("Cannot add a relation to this table's ChildRelation collection where this table isn't the parent table.");
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000533E File Offset: 0x0000353E
		public static Exception ChildTableMismatch()
		{
			return ExceptionBuilder._Argument("Cannot add a relation to this table's ParentRelation collection where this table isn't the child table.");
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000534A File Offset: 0x0000354A
		public static Exception EnforceConstraint()
		{
			return ExceptionBuilder._Constraint("Failed to enable constraints. One or more rows contain values violating non-null, unique, or foreign-key constraints.");
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005356 File Offset: 0x00003556
		public static Exception CaseLocaleMismatch()
		{
			return ExceptionBuilder._Argument("Cannot add a DataRelation or Constraint that has different Locale or CaseSensitive settings between its parent and child tables.");
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005362 File Offset: 0x00003562
		public static Exception CannotChangeCaseLocale()
		{
			return ExceptionBuilder.CannotChangeCaseLocale(null);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000536A File Offset: 0x0000356A
		public static Exception CannotChangeCaseLocale(Exception innerException)
		{
			return ExceptionBuilder._Argument("Cannot change CaseSensitive or Locale property. This change would lead to at least one DataRelation or Constraint to have different Locale or CaseSensitive settings between its related tables.", innerException);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005377 File Offset: 0x00003577
		public static Exception InvalidRemotingFormat(SerializationFormat mode)
		{
			return ExceptionBuilder._InvalidEnumArgumentException<SerializationFormat>(mode);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000537F File Offset: 0x0000357F
		public static Exception TableForeignPrimaryKey()
		{
			return ExceptionBuilder._Argument("PrimaryKey columns do not belong to this table.");
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000538B File Offset: 0x0000358B
		public static Exception TableCannotAddToSimpleContent()
		{
			return ExceptionBuilder._Argument("Cannot add a nested relation or an element column to a table containing a SimpleContent column.");
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00005397 File Offset: 0x00003597
		public static Exception NoTableName()
		{
			return ExceptionBuilder._Argument("TableName is required when it is part of a DataSet.");
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000053A3 File Offset: 0x000035A3
		public static Exception MultipleTextOnlyColumns()
		{
			return ExceptionBuilder._Argument("DataTable already has a simple content column.");
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000053AF File Offset: 0x000035AF
		public static Exception InvalidSortString(string sort)
		{
			return ExceptionBuilder._Argument(SR.Format(" {0} isn't a valid Sort string entry.", sort));
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000053C1 File Offset: 0x000035C1
		public static Exception DuplicateTableName(string table)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A DataTable named '{0}' already belongs to this DataSet.", table));
		}

		// Token: 0x0600018A RID: 394 RVA: 0x000053D3 File Offset: 0x000035D3
		public static Exception DuplicateTableName2(string table, string ns)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("A DataTable named '{0}' with the same Namespace '{1}' already belongs to this DataSet.", table, ns));
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000053E6 File Offset: 0x000035E6
		public static Exception SelfnestedDatasetConflictingName(string table)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("The table ({0}) cannot be the child table to itself in a nested relation: the DataSet name conflicts with the table name.", table));
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000053F8 File Offset: 0x000035F8
		public static Exception DatasetConflictingName(string table)
		{
			return ExceptionBuilder._DuplicateName(SR.Format("The name '{0}' is invalid. A DataTable cannot have the same name of the DataSet.", table));
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000540A File Offset: 0x0000360A
		public static Exception TableAlreadyInOtherDataSet()
		{
			return ExceptionBuilder._Argument("DataTable already belongs to another DataSet.");
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005416 File Offset: 0x00003616
		public static Exception TableAlreadyInTheDataSet()
		{
			return ExceptionBuilder._Argument("DataTable already belongs to this DataSet.");
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005422 File Offset: 0x00003622
		public static Exception TableOutOfRange(int index)
		{
			return ExceptionBuilder._IndexOutOfRange(SR.Format("Cannot find table {0}.", index.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000543F File Offset: 0x0000363F
		public static Exception TableNotInTheDataSet(string table)
		{
			return ExceptionBuilder._Argument(SR.Format("Table {0} does not belong to this DataSet.", table));
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005451 File Offset: 0x00003651
		public static Exception TableInRelation()
		{
			return ExceptionBuilder._Argument("Cannot remove a table that has existing relations.  Remove relations first.");
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000545D File Offset: 0x0000365D
		public static Exception TableInConstraint(DataTable table, Constraint constraint)
		{
			return ExceptionBuilder._Argument(SR.Format("Cannot remove table {0}, because it referenced in ForeignKeyConstraint {1}.  Remove the constraint first.", table.TableName, constraint.ConstraintName));
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000547A File Offset: 0x0000367A
		public static Exception CanNotSerializeDataTableHierarchy()
		{
			return ExceptionBuilder._InvalidOperation("Cannot serialize the DataTable. A DataTable being used in one or more DataColumn expressions is not a descendant of current DataTable.");
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00005486 File Offset: 0x00003686
		public static Exception CanNotRemoteDataTable()
		{
			return ExceptionBuilder._InvalidOperation("This DataTable can only be remoted as part of DataSet. One or more Expression Columns has reference to other DataTable(s).");
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00005492 File Offset: 0x00003692
		public static Exception CanNotSetRemotingFormat()
		{
			return ExceptionBuilder._Argument("Cannot have different remoting format property value for DataSet and DataTable.");
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000549E File Offset: 0x0000369E
		public static Exception CanNotSerializeDataTableWithEmptyName()
		{
			return ExceptionBuilder._InvalidOperation("Cannot serialize the DataTable. DataTable name is not set.");
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000054AA File Offset: 0x000036AA
		public static Exception TableNotFound(string tableName)
		{
			return ExceptionBuilder._Argument(SR.Format("DataTable '{0}' does not match to any DataTable in source.", tableName));
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000054BC File Offset: 0x000036BC
		public static Exception AggregateException(AggregateType aggregateType, Type type)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid usage of aggregate function {0}() and Type: {1}.", aggregateType.ToString(), type.Name));
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000054E0 File Offset: 0x000036E0
		public static Exception InvalidStorageType(TypeCode typecode)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid storage type: {0}.", typecode.ToString()));
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000054FE File Offset: 0x000036FE
		public static Exception RangeArgument(int min, int max)
		{
			return ExceptionBuilder._Argument(SR.Format("Min ({0}) must be less than or equal to max ({1}) in a Range object.", min.ToString(CultureInfo.InvariantCulture), max.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005527 File Offset: 0x00003727
		public static Exception NullRange()
		{
			return ExceptionBuilder._Data("This is a null range.");
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00005533 File Offset: 0x00003733
		public static Exception NegativeMinimumCapacity()
		{
			return ExceptionBuilder._Argument("MinimumCapacity must be non-negative.");
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005540 File Offset: 0x00003740
		public static Exception ProblematicChars(char charValue)
		{
			string text = "The DataSet Xml persistency does not support the value '{0}' as Char value, please use Byte storage instead.";
			string text2 = "0x";
			ushort num = (ushort)charValue;
			return ExceptionBuilder._Argument(SR.Format(text, text2 + num.ToString("X", CultureInfo.InvariantCulture)));
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005579 File Offset: 0x00003779
		public static Exception StorageSetFailed()
		{
			return ExceptionBuilder._Argument("Type of value has a mismatch with column type");
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005585 File Offset: 0x00003785
		public static Exception SimpleTypeNotSupported()
		{
			return ExceptionBuilder._Data("DataSet doesn't support 'union' or 'list' as simpleType.");
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00005591 File Offset: 0x00003791
		public static Exception MissingAttribute(string attribute)
		{
			return ExceptionBuilder.MissingAttribute(string.Empty, attribute);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000559E File Offset: 0x0000379E
		public static Exception MissingAttribute(string element, string attribute)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid {0} syntax: missing required '{1}' attribute.", element, attribute));
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000055B1 File Offset: 0x000037B1
		public static Exception InvalidAttributeValue(string name, string value)
		{
			return ExceptionBuilder._Data(SR.Format("Value '{1}' is invalid for attribute '{0}'.", name, value));
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000055C4 File Offset: 0x000037C4
		public static Exception AttributeValues(string name, string value1, string value2)
		{
			return ExceptionBuilder._Data(SR.Format("The value of attribute '{0}' should be '{1}' or '{2}'.", name, value1, value2));
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000055D8 File Offset: 0x000037D8
		public static Exception ElementTypeNotFound(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Cannot find ElementType name='{0}'.", name));
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000055EA File Offset: 0x000037EA
		public static Exception RelationParentNameMissing(string rel)
		{
			return ExceptionBuilder._Data(SR.Format("Parent table name is missing in relation '{0}'.", rel));
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000055FC File Offset: 0x000037FC
		public static Exception RelationChildNameMissing(string rel)
		{
			return ExceptionBuilder._Data(SR.Format("Child table name is missing in relation '{0}'.", rel));
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000560E File Offset: 0x0000380E
		public static Exception RelationTableKeyMissing(string rel)
		{
			return ExceptionBuilder._Data(SR.Format("Parent table key is missing in relation '{0}'.", rel));
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005620 File Offset: 0x00003820
		public static Exception RelationChildKeyMissing(string rel)
		{
			return ExceptionBuilder._Data(SR.Format("Child table key is missing in relation '{0}'.", rel));
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00005632 File Offset: 0x00003832
		public static Exception UndefinedDatatype(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Undefined data type: '{0}'.", name));
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00005644 File Offset: 0x00003844
		public static Exception DatatypeNotDefined()
		{
			return ExceptionBuilder._Data("Data type not defined.");
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00005650 File Offset: 0x00003850
		public static Exception MismatchKeyLength()
		{
			return ExceptionBuilder._Data("Invalid Relation definition: different length keys.");
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000565C File Offset: 0x0000385C
		public static Exception InvalidField(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid XPath selection inside field node. Cannot find: {0}.", name));
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000566E File Offset: 0x0000386E
		public static Exception InvalidSelector(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid XPath selection inside selector node: {0}.", name));
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005680 File Offset: 0x00003880
		public static Exception CircularComplexType(string name)
		{
			return ExceptionBuilder._Data(SR.Format("DataSet doesn't allow the circular reference in the ComplexType named '{0}'.", name));
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00005692 File Offset: 0x00003892
		public static Exception CannotInstantiateAbstract(string name)
		{
			return ExceptionBuilder._Data(SR.Format("DataSet cannot instantiate an abstract ComplexType for the node {0}.", name));
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000056A4 File Offset: 0x000038A4
		public static Exception InvalidKey(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Invalid 'Key' node inside constraint named: {0}.", name));
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000056B6 File Offset: 0x000038B6
		public static Exception DiffgramMissingTable(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Cannot load diffGram. Table '{0}' is missing in the destination dataset.", name));
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000056C8 File Offset: 0x000038C8
		public static Exception DiffgramMissingSQL()
		{
			return ExceptionBuilder._Data("Cannot load diffGram. The 'sql' node is missing.");
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000056D4 File Offset: 0x000038D4
		public static Exception DuplicateConstraintRead(string str)
		{
			return ExceptionBuilder._Data(SR.Format("The constraint name {0} is already used in the schema.", str));
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000056E6 File Offset: 0x000038E6
		public static Exception ColumnTypeConflict(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Column name '{0}' is defined for different mapping types.", name));
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000056F8 File Offset: 0x000038F8
		public static Exception CannotConvert(string name, string type)
		{
			return ExceptionBuilder._Data(SR.Format(" Cannot convert '{0}' to type '{1}'.", name, type));
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000570B File Offset: 0x0000390B
		public static Exception MissingRefer(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Missing '{0}' part in '{1}' constraint named '{2}'.", "refer", "keyref", name));
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00005727 File Offset: 0x00003927
		public static Exception InvalidPrefix(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Prefix '{0}' is not valid, because it contains special characters.", name));
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00005739 File Offset: 0x00003939
		public static Exception CanNotDeserializeObjectType()
		{
			return ExceptionBuilder._InvalidOperation("Unable to proceed with deserialization. Data does not implement IXMLSerializable, therefore polymorphism is not supported.");
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00005745 File Offset: 0x00003945
		public static Exception IsDataSetAttributeMissingInSchema()
		{
			return ExceptionBuilder._Data("IsDataSet attribute is missing in input Schema.");
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00005751 File Offset: 0x00003951
		public static Exception TooManyIsDataSetAtributeInSchema()
		{
			return ExceptionBuilder._Data("Cannot determine the DataSet Element. IsDataSet attribute exist more than once.");
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000575D File Offset: 0x0000395D
		public static Exception NestedCircular(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Circular reference in self-nested table '{0}'.", name));
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000576F File Offset: 0x0000396F
		public static Exception MultipleParentRows(string tableQName)
		{
			return ExceptionBuilder._Data(SR.Format("Cannot proceed with serializing DataTable '{0}'. It contains a DataRow which has multiple parent rows on the same Foreign Key.", tableQName));
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005781 File Offset: 0x00003981
		public static Exception PolymorphismNotSupported(string typeName)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("Type '{0}' does not implement IXmlSerializable interface therefore can not proceed with serialization.", typeName));
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00005793 File Offset: 0x00003993
		public static Exception DataTableInferenceNotSupported()
		{
			return ExceptionBuilder._InvalidOperation("DataTable does not support schema inference from Xml.");
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000579F File Offset: 0x0000399F
		internal static void ThrowMultipleTargetConverter(Exception innerException)
		{
			ExceptionBuilder.ThrowDataException((innerException != null) ? "An error occurred with the multiple target converter while writing an Xml Schema.  See the inner exception for details." : "An error occurred with the multiple target converter while writing an Xml Schema.  A null or empty string was returned.", innerException);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x000057B6 File Offset: 0x000039B6
		public static Exception DuplicateDeclaration(string name)
		{
			return ExceptionBuilder._Data(SR.Format("Duplicated declaration '{0}'.", name));
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000057C8 File Offset: 0x000039C8
		public static Exception FoundEntity()
		{
			return ExceptionBuilder._Data("DataSet cannot expand entities. Use XmlValidatingReader and set the EntityHandling property accordingly.");
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000057D4 File Offset: 0x000039D4
		public static Exception MergeFailed(string name)
		{
			return ExceptionBuilder._Data(name);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000057DC File Offset: 0x000039DC
		public static Exception ConvertFailed(Type type1, Type type2)
		{
			return ExceptionBuilder._Data(SR.Format(" Cannot convert object of type '{0}' to object of type '{1}'.", type1.FullName, type2.FullName));
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000057F9 File Offset: 0x000039F9
		internal static Exception InvalidDuplicateNamedSimpleTypeDelaration(string stName, string errorStr)
		{
			return ExceptionBuilder._Argument(SR.Format("Simple type '{0}' has already be declared with different '{1}'.", stName, errorStr));
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000580C File Offset: 0x00003A0C
		internal static Exception InternalRBTreeError(RBTreeError internalError)
		{
			return ExceptionBuilder._InvalidOperation(SR.Format("DataTable internal index is corrupted: '{0}'.", (int)internalError));
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00005823 File Offset: 0x00003A23
		public static Exception EnumeratorModified()
		{
			return ExceptionBuilder._InvalidOperation("Collection was modified; enumeration operation might not execute.");
		}
	}
}
