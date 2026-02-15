using System;
using System.Runtime.Serialization;

namespace ICSharpCode.SharpZipLib
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public class ValueOutOfRangeException : StreamDecodingException
	{
		// Token: 0x06000011 RID: 17 RVA: 0x000020D6 File Offset: 0x000002D6
		public ValueOutOfRangeException(string nameOfValue)
			: base(nameOfValue + " out of range")
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000020E9 File Offset: 0x000002E9
		public ValueOutOfRangeException(string nameOfValue, long value, long maxValue, long minValue = 0L)
			: this(nameOfValue, value.ToString(), maxValue.ToString(), minValue.ToString())
		{
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002107 File Offset: 0x00000307
		public ValueOutOfRangeException(string nameOfValue, string value, string maxValue, string minValue = "0")
			: base(string.Concat(new string[] { nameOfValue, " out of range: ", value, ", should be ", minValue, "..", maxValue }))
		{
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002143 File Offset: 0x00000343
		private ValueOutOfRangeException()
		{
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000020B5 File Offset: 0x000002B5
		private ValueOutOfRangeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000020BF File Offset: 0x000002BF
		protected ValueOutOfRangeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
