using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000082 RID: 130
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class JsonWriterException : JsonException
	{
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0001A596 File Offset: 0x00018796
		[Nullable(2)]
		public string Path
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000050C2 File Offset: 0x000032C2
		public JsonWriterException()
		{
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000050CA File Offset: 0x000032CA
		public JsonWriterException(string message)
			: base(message)
		{
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000050D3 File Offset: 0x000032D3
		public JsonWriterException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000050DD File Offset: 0x000032DD
		public JsonWriterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001A59E File Offset: 0x0001879E
		public JsonWriterException(string message, string path, [Nullable(2)] Exception innerException)
			: base(message, innerException)
		{
			this.Path = path;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001A5AF File Offset: 0x000187AF
		internal static JsonWriterException Create(JsonWriter writer, string message, [Nullable(2)] Exception ex)
		{
			return JsonWriterException.Create(writer.ContainerPath, message, ex);
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001A5BE File Offset: 0x000187BE
		internal static JsonWriterException Create(string path, string message, [Nullable(2)] Exception ex)
		{
			message = JsonPosition.FormatMessage(null, path, message);
			return new JsonWriterException(message, path, ex);
		}
	}
}
