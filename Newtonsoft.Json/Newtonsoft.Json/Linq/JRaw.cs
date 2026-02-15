using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x0200017A RID: 378
	[NullableContext(1)]
	[Nullable(0)]
	public class JRaw : JValue
	{
		// Token: 0x06000C8A RID: 3210 RVA: 0x00037D68 File Offset: 0x00035F68
		public static async Task<JRaw> CreateAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			JRaw jraw;
			using (StringWriter sw = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonWriter = new JsonTextWriter(sw))
				{
					await jsonWriter.WriteTokenSyncReadingAsync(reader, cancellationToken).ConfigureAwait(false);
					jraw = new JRaw(sw.ToString());
				}
			}
			return jraw;
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x00037DB3 File Offset: 0x00035FB3
		public JRaw(JRaw other)
			: base(other, null)
		{
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00037DBD File Offset: 0x00035FBD
		internal JRaw(JRaw other, [Nullable(2)] JsonCloneSettings settings)
			: base(other, settings)
		{
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x00037DC7 File Offset: 0x00035FC7
		[NullableContext(2)]
		public JRaw(object rawJson)
			: base(rawJson, JTokenType.Raw)
		{
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x00037DD4 File Offset: 0x00035FD4
		public static JRaw Create(JsonReader reader)
		{
			JRaw jraw;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
				{
					jsonTextWriter.WriteToken(reader);
					jraw = new JRaw(stringWriter.ToString());
				}
			}
			return jraw;
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x00037E3C File Offset: 0x0003603C
		internal override JToken CloneToken([Nullable(2)] JsonCloneSettings settings)
		{
			return new JRaw(this, settings);
		}
	}
}
