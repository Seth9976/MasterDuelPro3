using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200001A RID: 26
	public abstract class SqliteFunction : IDisposable
	{
		// Token: 0x06000133 RID: 307 RVA: 0x0000AC20 File Offset: 0x00008E20
		static SqliteFunction()
		{
			try
			{
				Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
				int num = assemblies.Length;
				AssemblyName name = Assembly.GetCallingAssembly().GetName();
				int i = 0;
				while (i < num)
				{
					bool flag = false;
					Type[] array;
					try
					{
						AssemblyName[] referencedAssemblies = assemblies[i].GetReferencedAssemblies();
						int num2 = referencedAssemblies.Length;
						for (int j = 0; j < num2; j++)
						{
							if (referencedAssemblies[j].Name == name.Name)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							goto IL_012C;
						}
						array = assemblies[i].GetTypes();
					}
					catch (ReflectionTypeLoadException ex)
					{
						array = ex.Types;
					}
					goto IL_00A5;
					IL_012C:
					i++;
					continue;
					IL_00A5:
					int num3 = array.Length;
					for (int k = 0; k < num3; k++)
					{
						if (array[k] != null)
						{
							object[] customAttributes = array[k].GetCustomAttributes(typeof(SqliteFunctionAttribute), false);
							int num4 = customAttributes.Length;
							for (int l = 0; l < num4; l++)
							{
								SqliteFunctionAttribute sqliteFunctionAttribute = customAttributes[l] as SqliteFunctionAttribute;
								if (sqliteFunctionAttribute != null)
								{
									sqliteFunctionAttribute._instanceType = array[k];
									SqliteFunction._registeredFunctions.Add(sqliteFunctionAttribute);
								}
							}
						}
					}
					goto IL_012C;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		public virtual object Invoke(object[] args)
		{
			return null;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000309E File Offset: 0x0000129E
		public virtual void Step(object[] args, int stepNumber, ref object contextData)
		{
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000ADA8 File Offset: 0x00008FA8
		public virtual object Final(object contextData)
		{
			return null;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000ADAB File Offset: 0x00008FAB
		public virtual int Compare(string param1, string param2)
		{
			return 0;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000ADB0 File Offset: 0x00008FB0
		internal object[] ConvertParams(int nArgs, IntPtr argsptr)
		{
			object[] array = new object[nArgs];
			IntPtr[] array2 = new IntPtr[nArgs];
			Marshal.Copy(argsptr, array2, 0, nArgs);
			for (int i = 0; i < nArgs; i++)
			{
				switch (this._base.GetParamValueType(array2[i]))
				{
				case TypeAffinity.Int64:
					array[i] = this._base.GetParamValueInt64(array2[i]);
					break;
				case TypeAffinity.Double:
					array[i] = this._base.GetParamValueDouble(array2[i]);
					break;
				case TypeAffinity.Text:
					array[i] = this._base.GetParamValueText(array2[i]);
					break;
				case TypeAffinity.Blob:
				{
					int num = (int)this._base.GetParamValueBytes(array2[i], 0, null, 0, 0);
					byte[] array3 = new byte[num];
					this._base.GetParamValueBytes(array2[i], 0, array3, 0, num);
					array[i] = array3;
					break;
				}
				case TypeAffinity.Null:
					array[i] = DBNull.Value;
					break;
				case TypeAffinity.DateTime:
					array[i] = this._base.ToDateTime(this._base.GetParamValueText(array2[i]));
					break;
				}
			}
			return array;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000AEE8 File Offset: 0x000090E8
		private void SetReturnValue(IntPtr context, object returnValue)
		{
			if (returnValue == null || returnValue == DBNull.Value)
			{
				this._base.ReturnNull(context);
				return;
			}
			Type type = returnValue.GetType();
			if (type == typeof(DateTime))
			{
				this._base.ReturnText(context, this._base.ToString((DateTime)returnValue));
				return;
			}
			Exception ex = returnValue as Exception;
			if (ex != null)
			{
				this._base.ReturnError(context, ex.Message);
				return;
			}
			switch (SqliteConvert.TypeToAffinity(type))
			{
			case TypeAffinity.Int64:
				this._base.ReturnInt64(context, Convert.ToInt64(returnValue, CultureInfo.CurrentCulture));
				return;
			case TypeAffinity.Double:
				this._base.ReturnDouble(context, Convert.ToDouble(returnValue, CultureInfo.CurrentCulture));
				return;
			case TypeAffinity.Text:
				this._base.ReturnText(context, returnValue.ToString());
				return;
			case TypeAffinity.Blob:
				this._base.ReturnBlob(context, (byte[])returnValue);
				return;
			case TypeAffinity.Null:
				this._base.ReturnNull(context);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000AFF3 File Offset: 0x000091F3
		internal void ScalarCallback(IntPtr context, int nArgs, IntPtr argsptr)
		{
			this._context = context;
			this.SetReturnValue(context, this.Invoke(this.ConvertParams(nArgs, argsptr)));
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000B011 File Offset: 0x00009211
		internal int CompareCallback(IntPtr ptr, int len1, IntPtr ptr1, int len2, IntPtr ptr2)
		{
			return this.Compare(SqliteConvert.UTF8ToString(ptr1, len1), SqliteConvert.UTF8ToString(ptr2, len2));
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000B029 File Offset: 0x00009229
		internal int CompareCallback16(IntPtr ptr, int len1, IntPtr ptr1, int len2, IntPtr ptr2)
		{
			return this.Compare(SQLite3_UTF16.UTF16ToString(ptr1, len1), SQLite3_UTF16.UTF16ToString(ptr2, len2));
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000B044 File Offset: 0x00009244
		internal void StepCallback(IntPtr context, int nArgs, IntPtr argsptr)
		{
			long num = (long)this._base.AggregateContext(context);
			SqliteFunction.AggregateData aggregateData;
			if (!this._contextDataList.TryGetValue(num, out aggregateData))
			{
				aggregateData = new SqliteFunction.AggregateData();
				this._contextDataList[num] = aggregateData;
			}
			try
			{
				this._context = context;
				this.Step(this.ConvertParams(nArgs, argsptr), aggregateData._count, ref aggregateData._data);
			}
			finally
			{
				aggregateData._count++;
			}
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000B0D0 File Offset: 0x000092D0
		internal void FinalCallback(IntPtr context)
		{
			long num = (long)this._base.AggregateContext(context);
			object obj = null;
			if (this._contextDataList.ContainsKey(num))
			{
				obj = this._contextDataList[num]._data;
				this._contextDataList.Remove(num);
			}
			this._context = context;
			this.SetReturnValue(context, this.Final(obj));
			IDisposable disposable = obj as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000B14C File Offset: 0x0000934C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (KeyValuePair<long, SqliteFunction.AggregateData> keyValuePair in this._contextDataList)
				{
					IDisposable disposable = keyValuePair.Value._data as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				this._contextDataList.Clear();
				this._InvokeFunc = null;
				this._StepFunc = null;
				this._FinalFunc = null;
				this._CompareFunc = null;
				this._base = null;
				this._contextDataList = null;
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000B1F8 File Offset: 0x000093F8
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000B204 File Offset: 0x00009404
		internal static SqliteFunction[] BindFunctions(SQLiteBase sqlbase)
		{
			List<SqliteFunction> list = new List<SqliteFunction>();
			foreach (SqliteFunctionAttribute sqliteFunctionAttribute in SqliteFunction._registeredFunctions)
			{
				SqliteFunction sqliteFunction = (SqliteFunction)Activator.CreateInstance(sqliteFunctionAttribute._instanceType);
				sqliteFunction._base = sqlbase;
				sqliteFunction._InvokeFunc = ((sqliteFunctionAttribute.FuncType != FunctionType.Scalar) ? null : new SQLiteCallback(sqliteFunction.ScalarCallback));
				sqliteFunction._StepFunc = ((sqliteFunctionAttribute.FuncType != FunctionType.Aggregate) ? null : new SQLiteCallback(sqliteFunction.StepCallback));
				sqliteFunction._FinalFunc = ((sqliteFunctionAttribute.FuncType != FunctionType.Aggregate) ? null : new SQLiteFinalCallback(sqliteFunction.FinalCallback));
				sqliteFunction._CompareFunc = ((sqliteFunctionAttribute.FuncType != FunctionType.Collation) ? null : new SQLiteCollation(sqliteFunction.CompareCallback));
				sqliteFunction._CompareFunc16 = ((sqliteFunctionAttribute.FuncType != FunctionType.Collation) ? null : new SQLiteCollation(sqliteFunction.CompareCallback16));
				if (sqliteFunctionAttribute.FuncType != FunctionType.Collation)
				{
					sqlbase.CreateFunction(sqliteFunctionAttribute.Name, sqliteFunctionAttribute.Arguments, sqliteFunction is SqliteFunctionEx, sqliteFunction._InvokeFunc, sqliteFunction._StepFunc, sqliteFunction._FinalFunc);
				}
				else
				{
					sqlbase.CreateCollation(sqliteFunctionAttribute.Name, sqliteFunction._CompareFunc, sqliteFunction._CompareFunc16);
				}
				list.Add(sqliteFunction);
			}
			SqliteFunction[] array = new SqliteFunction[list.Count];
			list.CopyTo(array, 0);
			return array;
		}

		// Token: 0x04000088 RID: 136
		internal SQLiteBase _base;

		// Token: 0x04000089 RID: 137
		private Dictionary<long, SqliteFunction.AggregateData> _contextDataList;

		// Token: 0x0400008A RID: 138
		private SQLiteCallback _InvokeFunc;

		// Token: 0x0400008B RID: 139
		private SQLiteCallback _StepFunc;

		// Token: 0x0400008C RID: 140
		private SQLiteFinalCallback _FinalFunc;

		// Token: 0x0400008D RID: 141
		private SQLiteCollation _CompareFunc;

		// Token: 0x0400008E RID: 142
		private SQLiteCollation _CompareFunc16;

		// Token: 0x0400008F RID: 143
		internal IntPtr _context;

		// Token: 0x04000090 RID: 144
		private static List<SqliteFunctionAttribute> _registeredFunctions = new List<SqliteFunctionAttribute>();

		// Token: 0x0200001B RID: 27
		private class AggregateData
		{
			// Token: 0x04000091 RID: 145
			internal int _count = 1;

			// Token: 0x04000092 RID: 146
			internal object _data;
		}
	}
}
