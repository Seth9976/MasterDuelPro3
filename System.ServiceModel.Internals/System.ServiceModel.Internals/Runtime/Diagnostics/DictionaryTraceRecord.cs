using System;
using System.Collections;
using System.Xml;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000033 RID: 51
	internal class DictionaryTraceRecord : TraceRecord
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00004F1F File Offset: 0x0000311F
		internal DictionaryTraceRecord(IDictionary dictionary)
		{
			this.dictionary = dictionary;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004F30 File Offset: 0x00003130
		internal override void WriteTo(XmlWriter xml)
		{
			if (this.dictionary != null)
			{
				foreach (object obj in this.dictionary.Keys)
				{
					object obj2 = this.dictionary[obj];
					xml.WriteElementString(obj.ToString(), (obj2 == null) ? string.Empty : obj2.ToString());
				}
			}
		}

		// Token: 0x0400007A RID: 122
		private IDictionary dictionary;
	}
}
