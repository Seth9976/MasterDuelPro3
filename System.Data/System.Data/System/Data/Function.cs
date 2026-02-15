using System;

namespace System.Data
{
	// Token: 0x0200001F RID: 31
	internal sealed class Function
	{
		// Token: 0x0600030D RID: 781 RVA: 0x00011718 File Offset: 0x0000F918
		internal Function(string name, FunctionId id, Type result, bool IsValidateArguments, bool IsVariantArgumentList, int argumentCount, Type a1, Type a2, Type a3)
		{
			this._name = name;
			this._id = id;
			this._result = result;
			this._isValidateArguments = IsValidateArguments;
			this._isVariantArgumentList = IsVariantArgumentList;
			this._argumentCount = argumentCount;
			if (a1 != null)
			{
				this._parameters[0] = a1;
			}
			if (a2 != null)
			{
				this._parameters[1] = a2;
			}
			if (a3 != null)
			{
				this._parameters[2] = a3;
			}
		}

		// Token: 0x040000CB RID: 203
		internal readonly string _name;

		// Token: 0x040000CC RID: 204
		internal readonly FunctionId _id;

		// Token: 0x040000CD RID: 205
		internal readonly Type _result;

		// Token: 0x040000CE RID: 206
		internal readonly bool _isValidateArguments;

		// Token: 0x040000CF RID: 207
		internal readonly bool _isVariantArgumentList;

		// Token: 0x040000D0 RID: 208
		internal readonly int _argumentCount;

		// Token: 0x040000D1 RID: 209
		internal readonly Type[] _parameters = new Type[3];

		// Token: 0x040000D2 RID: 210
		internal static string[] s_functionName = new string[]
		{
			"Unknown", "Ascii", "Char", "CharIndex", "Difference", "Len", "Lower", "LTrim", "Patindex", "Replicate",
			"Reverse", "Right", "RTrim", "Soundex", "Space", "Str", "Stuff", "Substring", "Upper", "IsNull",
			"Iif", "Convert", "cInt", "cBool", "cDate", "cDbl", "cStr", "Abs", "Acos", "In",
			"Trim", "Sum", "Avg", "Min", "Max", "Count", "StDev", "Var", "DateTimeOffset"
		};
	}
}
