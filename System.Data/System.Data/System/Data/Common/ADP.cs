using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Globalization;
using System.Security;
using System.Text;
using System.Threading;

namespace System.Data.Common
{
	// Token: 0x020000DB RID: 219
	internal static class ADP
	{
		// Token: 0x06000B54 RID: 2900 RVA: 0x0000468F File Offset: 0x0000288F
		private static void TraceException(string trace, Exception e)
		{
			if (e != null)
			{
				DataCommonEventSource.Log.Trace<Exception>(trace, e);
			}
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0003F890 File Offset: 0x0003DA90
		internal static void TraceExceptionAsReturnValue(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|THROW> '{0}'", e);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0003F89D File Offset: 0x0003DA9D
		internal static void TraceExceptionWithoutRethrow(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|CATCH> '%ls'\n", e);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0003F8AA File Offset: 0x0003DAAA
		internal static ArgumentException Argument(string error)
		{
			ArgumentException ex = new ArgumentException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0003F8B8 File Offset: 0x0003DAB8
		internal static ArgumentException Argument(string error, string parameter)
		{
			ArgumentException ex = new ArgumentException(error, parameter);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0003F8C7 File Offset: 0x0003DAC7
		internal static ArgumentNullException ArgumentNull(string parameter)
		{
			ArgumentNullException ex = new ArgumentNullException(parameter);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0003F8D5 File Offset: 0x0003DAD5
		internal static ArgumentNullException ArgumentNull(string parameter, string error)
		{
			ArgumentNullException ex = new ArgumentNullException(parameter, error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0003F8E4 File Offset: 0x0003DAE4
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(parameterName);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0003F8F2 File Offset: 0x0003DAF2
		internal static ArgumentOutOfRangeException ArgumentOutOfRange(string message, string parameterName)
		{
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException(parameterName, message);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0003F901 File Offset: 0x0003DB01
		internal static IndexOutOfRangeException IndexOutOfRange(string error)
		{
			IndexOutOfRangeException ex = new IndexOutOfRangeException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0003F90F File Offset: 0x0003DB0F
		internal static InvalidCastException InvalidCast(string error)
		{
			return ADP.InvalidCast(error, null);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0003F918 File Offset: 0x0003DB18
		internal static InvalidCastException InvalidCast(string error, Exception inner)
		{
			InvalidCastException ex = new InvalidCastException(error, inner);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0003F927 File Offset: 0x0003DB27
		internal static InvalidOperationException InvalidOperation(string error)
		{
			InvalidOperationException ex = new InvalidOperationException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0003F935 File Offset: 0x0003DB35
		internal static NotSupportedException NotSupported()
		{
			NotSupportedException ex = new NotSupportedException();
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0003F942 File Offset: 0x0003DB42
		internal static NotSupportedException NotSupported(string error)
		{
			NotSupportedException ex = new NotSupportedException(error);
			ADP.TraceExceptionAsReturnValue(ex);
			return ex;
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0003F950 File Offset: 0x0003DB50
		internal static bool IsCatchableExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != ADP.s_stackOverflowType && type != ADP.s_outOfMemoryType && type != ADP.s_threadAbortType && type != ADP.s_nullReferenceType && type != ADP.s_accessViolationType && !ADP.s_securityType.IsAssignableFrom(type);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0003F9B8 File Offset: 0x0003DBB8
		internal static bool IsCatchableOrSecurityExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != ADP.s_stackOverflowType && type != ADP.s_outOfMemoryType && type != ADP.s_threadAbortType && type != ADP.s_nullReferenceType && type != ADP.s_accessViolationType;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0003FA0D File Offset: 0x0003DC0D
		internal static ArgumentOutOfRangeException InvalidEnumerationValue(Type type, int value)
		{
			return ADP.ArgumentOutOfRange(SR.Format("The {0} enumeration value, {1}, is invalid.", type.Name, value.ToString(CultureInfo.InvariantCulture)), type.Name);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0003FA36 File Offset: 0x0003DC36
		internal static ArgumentException CollectionRemoveInvalidObject(Type itemType, ICollection collection)
		{
			return ADP.Argument(SR.Format("Attempted to remove an {0} that is not contained by this {1}.", itemType.Name, collection.GetType().Name));
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0003FA58 File Offset: 0x0003DC58
		internal static ArgumentNullException CollectionNullValue(string parameter, Type collection, Type itemType)
		{
			return ADP.ArgumentNull(parameter, SR.Format("The {0} only accepts non-null {1} type objects.", collection.Name, itemType.Name));
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0003FA76 File Offset: 0x0003DC76
		internal static IndexOutOfRangeException CollectionIndexInt32(int index, Type collection, int count)
		{
			return ADP.IndexOutOfRange(SR.Format("Invalid index {0} for this {1} with Count={2}.", index.ToString(CultureInfo.InvariantCulture), collection.Name, count.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0003FAA5 File Offset: 0x0003DCA5
		internal static InvalidCastException CollectionInvalidType(Type collection, Type itemType, object invalidValue)
		{
			return ADP.InvalidCast(SR.Format("The {0} only accepts non-null {1} type objects, not {2} objects.", collection.Name, itemType.Name, invalidValue.GetType().Name));
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0003FAD0 File Offset: 0x0003DCD0
		internal static string BuildQuotedString(string quotePrefix, string quoteSuffix, string unQuotedString)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(quotePrefix))
			{
				stringBuilder.Append(quotePrefix);
			}
			if (!string.IsNullOrEmpty(quoteSuffix))
			{
				stringBuilder.Append(unQuotedString.Replace(quoteSuffix, quoteSuffix + quoteSuffix));
				stringBuilder.Append(quoteSuffix);
			}
			else
			{
				stringBuilder.Append(unQuotedString);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0003FB28 File Offset: 0x0003DD28
		internal static ArgumentException ParametersIsNotParent(Type parameterType, ICollection collection)
		{
			return ADP.Argument(SR.Format("The {0} is already contained by another {1}.", parameterType.Name, collection.GetType().Name));
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0003FB28 File Offset: 0x0003DD28
		internal static ArgumentException ParametersIsParent(Type parameterType, ICollection collection)
		{
			return ADP.Argument(SR.Format("The {0} is already contained by another {1}.", parameterType.Name, collection.GetType().Name));
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0003FB4A File Offset: 0x0003DD4A
		internal static bool CompareInsensitiveInvariant(string strvalue, string strconst)
		{
			return CultureInfo.InvariantCulture.CompareInfo.Compare(strvalue, strconst, CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0003FB64 File Offset: 0x0003DD64
		internal static bool IsNull(object value)
		{
			if (value == null || DBNull.Value == value)
			{
				return true;
			}
			INullable nullable = value as INullable;
			return nullable != null && nullable.IsNull;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0003FB90 File Offset: 0x0003DD90
		internal static Exception InvalidSeekOrigin(string parameterName)
		{
			return ADP.ArgumentOutOfRange("Specified SeekOrigin value is invalid.", parameterName);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0003FB9D File Offset: 0x0003DD9D
		internal static void TraceExceptionForCapture(Exception e)
		{
			ADP.TraceException("<comm.ADP.TraceException|ERR|CATCH> '{0}'", e);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0003FBAA File Offset: 0x0003DDAA
		internal static ArgumentOutOfRangeException InvalidAcceptRejectRule(AcceptRejectRule value)
		{
			return ADP.InvalidEnumerationValue(typeof(AcceptRejectRule), (int)value);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0003FBBC File Offset: 0x0003DDBC
		internal static ArgumentOutOfRangeException InvalidConflictOptions(ConflictOption value)
		{
			return ADP.InvalidEnumerationValue(typeof(ConflictOption), (int)value);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0003FBCE File Offset: 0x0003DDCE
		internal static ArgumentOutOfRangeException InvalidMissingMappingAction(MissingMappingAction value)
		{
			return ADP.InvalidEnumerationValue(typeof(MissingMappingAction), (int)value);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0003FBE0 File Offset: 0x0003DDE0
		internal static ArgumentOutOfRangeException InvalidMissingSchemaAction(MissingSchemaAction value)
		{
			return ADP.InvalidEnumerationValue(typeof(MissingSchemaAction), (int)value);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0003FBF2 File Offset: 0x0003DDF2
		internal static ArgumentOutOfRangeException InvalidRule(Rule value)
		{
			return ADP.InvalidEnumerationValue(typeof(Rule), (int)value);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0003FC04 File Offset: 0x0003DE04
		internal static ArgumentOutOfRangeException InvalidStatementType(StatementType value)
		{
			return ADP.InvalidEnumerationValue(typeof(StatementType), (int)value);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0003FC16 File Offset: 0x0003DE16
		internal static ArgumentOutOfRangeException InvalidUpdateStatus(UpdateStatus value)
		{
			return ADP.InvalidEnumerationValue(typeof(UpdateStatus), (int)value);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0003FC28 File Offset: 0x0003DE28
		internal static Exception WrongType(Type got, Type expected)
		{
			return ADP.Argument(SR.Format("Expecting argument of type {1}, but received type {0}.", got.ToString(), expected.ToString()));
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0003FC45 File Offset: 0x0003DE45
		internal static Exception CollectionUniqueValue(Type itemType, string propertyName, string propertyValue)
		{
			return ADP.Argument(SR.Format("The {0}.{1} is required to be unique, '{2}' already exists in the collection.", itemType.Name, propertyName, propertyValue));
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0003FC5E File Offset: 0x0003DE5E
		private static InvalidOperationException DataMapping(string error)
		{
			return ADP.InvalidOperation(error);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0003FC66 File Offset: 0x0003DE66
		internal static InvalidOperationException ColumnSchemaExpression(string srcColumn, string cacheColumn)
		{
			return ADP.DataMapping(SR.Format("The column mapping from SourceColumn '{0}' failed because the DataColumn '{1}' is a computed column.", srcColumn, cacheColumn));
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0003FC79 File Offset: 0x0003DE79
		internal static InvalidOperationException ColumnSchemaMismatch(string srcColumn, Type srcType, DataColumn column)
		{
			return ADP.DataMapping(SR.Format("Inconvertible type mismatch between SourceColumn '{0}' of {1} and the DataColumn '{2}' of {3}.", new object[]
			{
				srcColumn,
				srcType.Name,
				column.ColumnName,
				column.DataType.Name
			}));
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0003FCB4 File Offset: 0x0003DEB4
		internal static InvalidOperationException ColumnSchemaMissing(string cacheColumn, string tableName, string srcColumn)
		{
			if (string.IsNullOrEmpty(tableName))
			{
				return ADP.InvalidOperation(SR.Format("Missing the DataColumn '{0}' for the SourceColumn '{2}'.", cacheColumn, tableName, srcColumn));
			}
			return ADP.DataMapping(SR.Format("Missing the DataColumn '{0}' in the DataTable '{1}' for the SourceColumn '{2}'.", cacheColumn, tableName, srcColumn));
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0003FCE3 File Offset: 0x0003DEE3
		internal static InvalidOperationException MissingColumnMapping(string srcColumn)
		{
			return ADP.DataMapping(SR.Format("Missing SourceColumn mapping for '{0}'.", srcColumn));
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0003FCF5 File Offset: 0x0003DEF5
		internal static Exception InvalidSourceColumn(string parameter)
		{
			return ADP.Argument("SourceColumn is required to be a non-empty string.", parameter);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0003FD02 File Offset: 0x0003DF02
		internal static Exception ColumnsAddNullAttempt(string parameter)
		{
			return ADP.CollectionNullValue(parameter, typeof(DataColumnMappingCollection), typeof(DataColumnMapping));
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0003FD1E File Offset: 0x0003DF1E
		internal static Exception ColumnsIndexInt32(int index, IColumnMappingCollection collection)
		{
			return ADP.CollectionIndexInt32(index, collection.GetType(), collection.Count);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0003FD32 File Offset: 0x0003DF32
		internal static Exception ColumnsIsNotParent(ICollection collection)
		{
			return ADP.ParametersIsNotParent(typeof(DataColumnMapping), collection);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0003FD44 File Offset: 0x0003DF44
		internal static Exception ColumnsIsParent(ICollection collection)
		{
			return ADP.ParametersIsParent(typeof(DataColumnMapping), collection);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0003FD56 File Offset: 0x0003DF56
		internal static Exception ColumnsUniqueSourceColumn(string srcColumn)
		{
			return ADP.CollectionUniqueValue(typeof(DataColumnMapping), "SourceColumn", srcColumn);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0003FD6D File Offset: 0x0003DF6D
		internal static Exception NotADataColumnMapping(object value)
		{
			return ADP.CollectionInvalidType(typeof(DataColumnMappingCollection), typeof(DataColumnMapping), value);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0003FD89 File Offset: 0x0003DF89
		internal static Exception TablesUniqueSourceTable(string srcTable)
		{
			return ADP.CollectionUniqueValue(typeof(DataTableMapping), "SourceTable", srcTable);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0003FDA0 File Offset: 0x0003DFA0
		internal static InvalidOperationException DynamicSQLJoinUnsupported()
		{
			return ADP.InvalidOperation("Dynamic SQL generation is not supported against multiple base tables.");
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0003FDAC File Offset: 0x0003DFAC
		internal static InvalidOperationException DynamicSQLNoTableInfo()
		{
			return ADP.InvalidOperation("Dynamic SQL generation is not supported against a SelectCommand that does not return any base table information.");
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0003FDB8 File Offset: 0x0003DFB8
		internal static InvalidOperationException DynamicSQLNoKeyInfoDelete()
		{
			return ADP.InvalidOperation("Dynamic SQL generation for the DeleteCommand is not supported against a SelectCommand that does not return any key column information.");
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0003FDC4 File Offset: 0x0003DFC4
		internal static InvalidOperationException DynamicSQLNoKeyInfoUpdate()
		{
			return ADP.InvalidOperation("Dynamic SQL generation for the UpdateCommand is not supported against a SelectCommand that does not return any key column information.");
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0003FDD0 File Offset: 0x0003DFD0
		internal static InvalidOperationException DynamicSQLNoKeyInfoRowVersionDelete()
		{
			return ADP.InvalidOperation("Dynamic SQL generation for the DeleteCommand is not supported against a SelectCommand that does not contain a row version column.");
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0003FDDC File Offset: 0x0003DFDC
		internal static InvalidOperationException DynamicSQLNoKeyInfoRowVersionUpdate()
		{
			return ADP.InvalidOperation("Dynamic SQL generation for the UpdateCommand is not supported against a SelectCommand that does not contain a row version column.");
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0003FDE8 File Offset: 0x0003DFE8
		internal static InvalidOperationException DynamicSQLNestedQuote(string name, string quote)
		{
			return ADP.InvalidOperation(SR.Format("Dynamic SQL generation not supported against table names '{0}' that contain the QuotePrefix or QuoteSuffix character '{1}'.", name, quote));
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0003FDFB File Offset: 0x0003DFFB
		internal static InvalidOperationException NoQuoteChange()
		{
			return ADP.InvalidOperation("The QuotePrefix and QuoteSuffix properties cannot be changed once an Insert, Update, or Delete command has been generated.");
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0003FE07 File Offset: 0x0003E007
		internal static InvalidOperationException MissingSourceCommand()
		{
			return ADP.InvalidOperation("The DataAdapter.SelectCommand property needs to be initialized.");
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x0003FE13 File Offset: 0x0003E013
		internal static InvalidOperationException MissingSourceCommandConnection()
		{
			return ADP.InvalidOperation("The DataAdapter.SelectCommand.Connection property needs to be initialized;");
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0003FE20 File Offset: 0x0003E020
		internal static void BuildSchemaTableInfoTableNames(string[] columnNameArray)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>(columnNameArray.Length);
			int num = columnNameArray.Length;
			int num2 = columnNameArray.Length - 1;
			while (0 <= num2)
			{
				string text = columnNameArray[num2];
				if (text != null && 0 < text.Length)
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					int num3;
					if (dictionary.TryGetValue(text, out num3))
					{
						num = Math.Min(num, num3);
					}
					dictionary[text] = num2;
				}
				else
				{
					columnNameArray[num2] = string.Empty;
					num = num2;
				}
				num2--;
			}
			int num4 = 1;
			for (int i = num; i < columnNameArray.Length; i++)
			{
				string text2 = columnNameArray[i];
				if (text2.Length == 0)
				{
					columnNameArray[i] = "Column";
					num4 = ADP.GenerateUniqueName(dictionary, ref columnNameArray[i], i, num4);
				}
				else
				{
					text2 = text2.ToLower(CultureInfo.InvariantCulture);
					if (i != dictionary[text2])
					{
						ADP.GenerateUniqueName(dictionary, ref columnNameArray[i], i, 1);
					}
				}
			}
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0003FF04 File Offset: 0x0003E104
		private static int GenerateUniqueName(Dictionary<string, int> hash, ref string columnName, int index, int uniqueIndex)
		{
			string text;
			for (;;)
			{
				text = columnName + uniqueIndex.ToString(CultureInfo.InvariantCulture);
				string text2 = text.ToLower(CultureInfo.InvariantCulture);
				if (hash.TryAdd(text2, index))
				{
					break;
				}
				uniqueIndex++;
			}
			columnName = text;
			return uniqueIndex;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0003FF48 File Offset: 0x0003E148
		internal static int SrcCompare(string strA, string strB)
		{
			if (!(strA == strB))
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0400049E RID: 1182
		private static readonly Type s_stackOverflowType = typeof(StackOverflowException);

		// Token: 0x0400049F RID: 1183
		private static readonly Type s_outOfMemoryType = typeof(OutOfMemoryException);

		// Token: 0x040004A0 RID: 1184
		private static readonly Type s_threadAbortType = typeof(ThreadAbortException);

		// Token: 0x040004A1 RID: 1185
		private static readonly Type s_nullReferenceType = typeof(NullReferenceException);

		// Token: 0x040004A2 RID: 1186
		private static readonly Type s_accessViolationType = typeof(AccessViolationException);

		// Token: 0x040004A3 RID: 1187
		private static readonly Type s_securityType = typeof(SecurityException);

		// Token: 0x040004A4 RID: 1188
		internal static readonly string StrEmpty = "";

		// Token: 0x040004A5 RID: 1189
		internal static readonly string[] AzureSqlServerEndpoints = new string[]
		{
			SR.GetString(".database.windows.net"),
			SR.GetString(".database.cloudapi.de"),
			SR.GetString(".database.usgovcloudapi.net"),
			SR.GetString(".database.chinacloudapi.cn")
		};

		// Token: 0x040004A6 RID: 1190
		internal static readonly IntPtr PtrZero = new IntPtr(0);

		// Token: 0x040004A7 RID: 1191
		internal static readonly int PtrSize = IntPtr.Size;
	}
}
