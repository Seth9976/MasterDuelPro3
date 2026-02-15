using System;

namespace System.Data.ProviderBase
{
	// Token: 0x020000D6 RID: 214
	internal class BasicFieldNameLookup
	{
		// Token: 0x06000B37 RID: 2871 RVA: 0x0003EA60 File Offset: 0x0003CC60
		public BasicFieldNameLookup(IDataReader reader)
		{
			int fieldCount = reader.FieldCount;
			string[] array = new string[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				array[i] = reader.GetName(i);
			}
			this._fieldNames = array;
		}

		// Token: 0x0400048F RID: 1167
		private readonly string[] _fieldNames;
	}
}
