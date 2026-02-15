using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000031 RID: 49
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class JsonSerializationException : JsonException
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00005168 File Offset: 0x00003368
		public int LineNumber { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00005170 File Offset: 0x00003370
		public int LinePosition { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005178 File Offset: 0x00003378
		[Nullable(2)]
		public string Path
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000050C2 File Offset: 0x000032C2
		public JsonSerializationException()
		{
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000050CA File Offset: 0x000032CA
		public JsonSerializationException(string message)
			: base(message)
		{
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000050D3 File Offset: 0x000032D3
		public JsonSerializationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000050DD File Offset: 0x000032DD
		public JsonSerializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005180 File Offset: 0x00003380
		public JsonSerializationException(string message, string path, int lineNumber, int linePosition, [Nullable(2)] Exception innerException)
			: base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000051A1 File Offset: 0x000033A1
		internal static JsonSerializationException Create(JsonReader reader, string message)
		{
			return JsonSerializationException.Create(reader, message, null);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000051AB File Offset: 0x000033AB
		internal static JsonSerializationException Create(JsonReader reader, string message, [Nullable(2)] Exception ex)
		{
			return JsonSerializationException.Create(reader as IJsonLineInfo, reader.Path, message, ex);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000051C0 File Offset: 0x000033C0
		internal static JsonSerializationException Create([Nullable(2)] IJsonLineInfo lineInfo, string path, string message, [Nullable(2)] Exception ex)
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
			return new JsonSerializationException(message, path, num, num2, ex);
		}
	}
}
