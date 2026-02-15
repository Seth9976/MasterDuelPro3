using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x0200002F RID: 47
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class JsonReaderException : JsonException
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000050AA File Offset: 0x000032AA
		public int LineNumber { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000050B2 File Offset: 0x000032B2
		public int LinePosition { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000050BA File Offset: 0x000032BA
		[Nullable(2)]
		public string Path
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000050C2 File Offset: 0x000032C2
		public JsonReaderException()
		{
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000050CA File Offset: 0x000032CA
		public JsonReaderException(string message)
			: base(message)
		{
		}

		// Token: 0x06000138 RID: 312 RVA: 0x000050D3 File Offset: 0x000032D3
		public JsonReaderException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000050DD File Offset: 0x000032DD
		public JsonReaderException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000050E7 File Offset: 0x000032E7
		public JsonReaderException(string message, string path, int lineNumber, int linePosition, [Nullable(2)] Exception innerException)
			: base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00005108 File Offset: 0x00003308
		internal static JsonReaderException Create(JsonReader reader, string message)
		{
			return JsonReaderException.Create(reader, message, null);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005112 File Offset: 0x00003312
		internal static JsonReaderException Create(JsonReader reader, string message, [Nullable(2)] Exception ex)
		{
			return JsonReaderException.Create(reader as IJsonLineInfo, reader.Path, message, ex);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00005128 File Offset: 0x00003328
		internal static JsonReaderException Create([Nullable(2)] IJsonLineInfo lineInfo, string path, string message, [Nullable(2)] Exception ex)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			int num;
			int num2;
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				num = lineInfo.LineNumber;
				num2 = lineInfo.LinePosition;
			}
			else
			{
				num = 0;
				num2 = 0;
			}
			return new JsonReaderException(message, path, num, num2, ex);
		}
	}
}
