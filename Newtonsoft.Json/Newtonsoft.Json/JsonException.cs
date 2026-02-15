using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000020 RID: 32
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class JsonException : Exception
	{
		// Token: 0x06000095 RID: 149 RVA: 0x00002F8C File Offset: 0x0000118C
		public JsonException()
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002F94 File Offset: 0x00001194
		public JsonException(string message)
			: base(message)
		{
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002F9D File Offset: 0x0000119D
		public JsonException(string message, [Nullable(2)] Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00002FA7 File Offset: 0x000011A7
		public JsonException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00002FB1 File Offset: 0x000011B1
		internal static JsonException Create(IJsonLineInfo lineInfo, string path, string message)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			return new JsonException(message);
		}
	}
}
