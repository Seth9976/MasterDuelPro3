using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x0200005D RID: 93
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonTextWriter : JsonWriter
	{
		// Token: 0x060002B9 RID: 697 RVA: 0x00010D76 File Offset: 0x0000EF76
		public override Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.FlushAsync(cancellationToken);
			}
			return this.DoFlushAsync(cancellationToken);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00010D8F File Offset: 0x0000EF8F
		internal Task DoFlushAsync(CancellationToken cancellationToken)
		{
			return cancellationToken.CancelIfRequestedAsync() ?? this._writer.FlushAsync();
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00010DA6 File Offset: 0x0000EFA6
		protected override Task WriteValueDelimiterAsync(CancellationToken cancellationToken)
		{
			if (!this._safeAsync)
			{
				return base.WriteValueDelimiterAsync(cancellationToken);
			}
			return this.DoWriteValueDelimiterAsync(cancellationToken);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00010DBF File Offset: 0x0000EFBF
		internal Task DoWriteValueDelimiterAsync(CancellationToken cancellationToken)
		{
			return this._writer.WriteAsync(',', cancellationToken);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00010DCF File Offset: 0x0000EFCF
		protected override Task WriteEndAsync(JsonToken token, CancellationToken cancellationToken)
		{
			if (!this._safeAsync)
			{
				return base.WriteEndAsync(token, cancellationToken);
			}
			return this.DoWriteEndAsync(token, cancellationToken);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00010DEC File Offset: 0x0000EFEC
		internal Task DoWriteEndAsync(JsonToken token, CancellationToken cancellationToken)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				return this._writer.WriteAsync('}', cancellationToken);
			case JsonToken.EndArray:
				return this._writer.WriteAsync(']', cancellationToken);
			case JsonToken.EndConstructor:
				return this._writer.WriteAsync(')', cancellationToken);
			default:
				throw JsonWriterException.Create(this, "Invalid JsonToken: " + token.ToString(), null);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00010E5B File Offset: 0x0000F05B
		public override Task CloseAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.CloseAsync(cancellationToken);
			}
			return this.DoCloseAsync(cancellationToken);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00010E74 File Offset: 0x0000F074
		internal async Task DoCloseAsync(CancellationToken cancellationToken)
		{
			if (base.Top == 0)
			{
				cancellationToken.ThrowIfCancellationRequested();
			}
			while (base.Top > 0)
			{
				await this.WriteEndAsync(cancellationToken).ConfigureAwait(false);
			}
			await this.CloseBufferAndWriterAsync().ConfigureAwait(false);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00010EC0 File Offset: 0x0000F0C0
		private async Task CloseBufferAndWriterAsync()
		{
			if (this._writeBuffer != null)
			{
				BufferUtils.ReturnBuffer(this._arrayPool, this._writeBuffer);
				this._writeBuffer = null;
			}
			if (base.CloseOutput && this._writer != null)
			{
				await this._writer.FlushAsync().ConfigureAwait(false);
				this._writer.Close();
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00010F03 File Offset: 0x0000F103
		public override Task WriteEndAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteEndAsync(cancellationToken);
			}
			return base.WriteEndInternalAsync(cancellationToken);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00010F1C File Offset: 0x0000F11C
		protected override Task WriteIndentAsync(CancellationToken cancellationToken)
		{
			if (!this._safeAsync)
			{
				return base.WriteIndentAsync(cancellationToken);
			}
			return this.DoWriteIndentAsync(cancellationToken);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00010F38 File Offset: 0x0000F138
		internal Task DoWriteIndentAsync(CancellationToken cancellationToken)
		{
			int num = base.Top * this._indentation;
			int num2 = this.SetIndentChars();
			if (num <= 12)
			{
				return this._writer.WriteAsync(this._indentChars, 0, num2 + num, cancellationToken);
			}
			return this.WriteIndentAsync(num, num2, cancellationToken);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00010F80 File Offset: 0x0000F180
		private async Task WriteIndentAsync(int currentIndentCount, int newLineLen, CancellationToken cancellationToken)
		{
			await this._writer.WriteAsync(this._indentChars, 0, newLineLen + Math.Min(currentIndentCount, 12), cancellationToken).ConfigureAwait(false);
			while ((currentIndentCount -= 12) > 0)
			{
				await this._writer.WriteAsync(this._indentChars, newLineLen, Math.Min(currentIndentCount, 12), cancellationToken).ConfigureAwait(false);
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00010FDC File Offset: 0x0000F1DC
		private Task WriteValueInternalAsync(JsonToken token, string value, CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteValueAsync(token, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this._writer.WriteAsync(value, cancellationToken);
			}
			return this.WriteValueInternalAsync(task, value, cancellationToken);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00011014 File Offset: 0x0000F214
		private async Task WriteValueInternalAsync(Task task, string value, CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(false);
			await this._writer.WriteAsync(value, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0001106F File Offset: 0x0000F26F
		protected override Task WriteIndentSpaceAsync(CancellationToken cancellationToken)
		{
			if (!this._safeAsync)
			{
				return base.WriteIndentSpaceAsync(cancellationToken);
			}
			return this.DoWriteIndentSpaceAsync(cancellationToken);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00011088 File Offset: 0x0000F288
		internal Task DoWriteIndentSpaceAsync(CancellationToken cancellationToken)
		{
			return this._writer.WriteAsync(' ', cancellationToken);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00011098 File Offset: 0x0000F298
		public override Task WriteRawAsync([Nullable(2)] string json, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteRawAsync(json, cancellationToken);
			}
			return this.DoWriteRawAsync(json, cancellationToken);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000110B3 File Offset: 0x0000F2B3
		internal Task DoWriteRawAsync([Nullable(2)] string json, CancellationToken cancellationToken)
		{
			return this._writer.WriteAsync(json, cancellationToken);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000110C2 File Offset: 0x0000F2C2
		public override Task WriteNullAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteNullAsync(cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000110DB File Offset: 0x0000F2DB
		internal Task DoWriteNullAsync(CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.Null, JsonConvert.Null, cancellationToken);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000110EC File Offset: 0x0000F2EC
		private Task WriteDigitsAsync(ulong uvalue, bool negative, CancellationToken cancellationToken)
		{
			if ((uvalue <= 9UL) & !negative)
			{
				return this._writer.WriteAsync((char)(48UL + uvalue), cancellationToken);
			}
			int num = this.WriteNumberToBuffer(uvalue, negative);
			return this._writer.WriteAsync(this._writeBuffer, 0, num, cancellationToken);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0001113C File Offset: 0x0000F33C
		private Task WriteIntegerValueAsync(ulong uvalue, bool negative, CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteValueAsync(JsonToken.Integer, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this.WriteDigitsAsync(uvalue, negative, cancellationToken);
			}
			return this.WriteIntegerValueAsync(task, uvalue, negative, cancellationToken);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00011170 File Offset: 0x0000F370
		private async Task WriteIntegerValueAsync(Task task, ulong uvalue, bool negative, CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(false);
			await this.WriteDigitsAsync(uvalue, negative, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000111D4 File Offset: 0x0000F3D4
		internal Task WriteIntegerValueAsync(long value, CancellationToken cancellationToken)
		{
			bool flag = value < 0L;
			if (flag)
			{
				value = -value;
			}
			return this.WriteIntegerValueAsync((ulong)value, flag, cancellationToken);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000111F7 File Offset: 0x0000F3F7
		internal Task WriteIntegerValueAsync(ulong uvalue, CancellationToken cancellationToken)
		{
			return this.WriteIntegerValueAsync(uvalue, false, cancellationToken);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00011204 File Offset: 0x0000F404
		private Task WriteEscapedStringAsync(string value, bool quote, CancellationToken cancellationToken)
		{
			return JavaScriptUtils.WriteEscapedJavaScriptStringAsync(this._writer, value, this._quoteChar, quote, this._charEscapeFlags, base.StringEscapeHandling, this, this._writeBuffer, cancellationToken);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00011238 File Offset: 0x0000F438
		public override Task WritePropertyNameAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WritePropertyNameAsync(name, cancellationToken);
			}
			return this.DoWritePropertyNameAsync(name, cancellationToken);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00011254 File Offset: 0x0000F454
		internal Task DoWritePropertyNameAsync(string name, CancellationToken cancellationToken)
		{
			Task task = base.InternalWritePropertyNameAsync(name, cancellationToken);
			if (!task.IsCompletedSuccessfully())
			{
				return this.DoWritePropertyNameAsync(task, name, cancellationToken);
			}
			task = this.WriteEscapedStringAsync(name, this._quoteName, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this._writer.WriteAsync(':', cancellationToken);
			}
			return JavaScriptUtils.WriteCharAsync(task, this._writer, ':', cancellationToken);
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000112B4 File Offset: 0x0000F4B4
		private async Task DoWritePropertyNameAsync(Task task, string name, CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(false);
			await this.WriteEscapedStringAsync(name, this._quoteName, cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(':').ConfigureAwait(false);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0001130F File Offset: 0x0000F50F
		public override Task WritePropertyNameAsync(string name, bool escape, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WritePropertyNameAsync(name, escape, cancellationToken);
			}
			return this.DoWritePropertyNameAsync(name, escape, cancellationToken);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0001132C File Offset: 0x0000F52C
		internal async Task DoWritePropertyNameAsync(string name, bool escape, CancellationToken cancellationToken)
		{
			await base.InternalWritePropertyNameAsync(name, cancellationToken).ConfigureAwait(false);
			if (escape)
			{
				await this.WriteEscapedStringAsync(name, this._quoteName, cancellationToken).ConfigureAwait(false);
			}
			else
			{
				if (this._quoteName)
				{
					await this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false);
				}
				await this._writer.WriteAsync(name, cancellationToken).ConfigureAwait(false);
				if (this._quoteName)
				{
					await this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false);
				}
			}
			await this._writer.WriteAsync(':').ConfigureAwait(false);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00011387 File Offset: 0x0000F587
		public override Task WriteStartArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteStartArrayAsync(cancellationToken);
			}
			return this.DoWriteStartArrayAsync(cancellationToken);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000113A0 File Offset: 0x0000F5A0
		internal Task DoWriteStartArrayAsync(CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteStartAsync(JsonToken.StartArray, JsonContainerType.Array, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this._writer.WriteAsync('[', cancellationToken);
			}
			return this.DoWriteStartArrayAsync(task, cancellationToken);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000113D8 File Offset: 0x0000F5D8
		internal async Task DoWriteStartArrayAsync(Task task, CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(false);
			await this._writer.WriteAsync('[', cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0001142B File Offset: 0x0000F62B
		public override Task WriteStartObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteStartObjectAsync(cancellationToken);
			}
			return this.DoWriteStartObjectAsync(cancellationToken);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00011444 File Offset: 0x0000F644
		internal Task DoWriteStartObjectAsync(CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteStartAsync(JsonToken.StartObject, JsonContainerType.Object, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this._writer.WriteAsync('{', cancellationToken);
			}
			return this.DoWriteStartObjectAsync(task, cancellationToken);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0001147C File Offset: 0x0000F67C
		internal async Task DoWriteStartObjectAsync(Task task, CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(false);
			await this._writer.WriteAsync('{', cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000114CF File Offset: 0x0000F6CF
		public override Task WriteStartConstructorAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteStartConstructorAsync(name, cancellationToken);
			}
			return this.DoWriteStartConstructorAsync(name, cancellationToken);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000114EC File Offset: 0x0000F6EC
		internal async Task DoWriteStartConstructorAsync(string name, CancellationToken cancellationToken)
		{
			await base.InternalWriteStartAsync(JsonToken.StartConstructor, JsonContainerType.Constructor, cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync("new ", cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(name, cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync('(').ConfigureAwait(false);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0001153F File Offset: 0x0000F73F
		public override Task WriteUndefinedAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteUndefinedAsync(cancellationToken);
			}
			return this.DoWriteUndefinedAsync(cancellationToken);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00011558 File Offset: 0x0000F758
		internal Task DoWriteUndefinedAsync(CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteValueAsync(JsonToken.Undefined, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this._writer.WriteAsync(JsonConvert.Undefined, cancellationToken);
			}
			return this.DoWriteUndefinedAsync(task, cancellationToken);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00011594 File Offset: 0x0000F794
		private async Task DoWriteUndefinedAsync(Task task, CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(false);
			await this._writer.WriteAsync(JsonConvert.Undefined, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000115E7 File Offset: 0x0000F7E7
		public override Task WriteWhitespaceAsync(string ws, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteWhitespaceAsync(ws, cancellationToken);
			}
			return this.DoWriteWhitespaceAsync(ws, cancellationToken);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00011602 File Offset: 0x0000F802
		internal Task DoWriteWhitespaceAsync(string ws, CancellationToken cancellationToken)
		{
			base.InternalWriteWhitespace(ws);
			return this._writer.WriteAsync(ws, cancellationToken);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00011618 File Offset: 0x0000F818
		public override Task WriteValueAsync(bool value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00011633 File Offset: 0x0000F833
		internal Task DoWriteValueAsync(bool value, CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.Boolean, JsonConvert.ToString(value), cancellationToken);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00011644 File Offset: 0x0000F844
		public override Task WriteValueAsync(bool? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0001165F File Offset: 0x0000F85F
		internal Task DoWriteValueAsync(bool? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00011680 File Offset: 0x0000F880
		public override Task WriteValueAsync(byte value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)((ulong)value), cancellationToken);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0001169C File Offset: 0x0000F89C
		public override Task WriteValueAsync(byte? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000116B7 File Offset: 0x0000F8B7
		internal Task DoWriteValueAsync(byte? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)((ulong)value.GetValueOrDefault()), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000116D9 File Offset: 0x0000F8D9
		public override Task WriteValueAsync([Nullable(2)] byte[] value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (value != null)
			{
				return this.WriteValueNonNullAsync(value, cancellationToken);
			}
			return this.WriteNullAsync(cancellationToken);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00011700 File Offset: 0x0000F900
		internal async Task WriteValueNonNullAsync(byte[] value, CancellationToken cancellationToken)
		{
			await base.InternalWriteValueAsync(JsonToken.Bytes, cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false);
			await this.Base64Encoder.EncodeAsync(value, 0, value.Length, cancellationToken).ConfigureAwait(false);
			await this.Base64Encoder.FlushAsync(cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00011753 File Offset: 0x0000F953
		public override Task WriteValueAsync(char value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0001176E File Offset: 0x0000F96E
		internal Task DoWriteValueAsync(char value, CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.String, JsonConvert.ToString(value), cancellationToken);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0001177F File Offset: 0x0000F97F
		public override Task WriteValueAsync(char? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0001179A File Offset: 0x0000F99A
		internal Task DoWriteValueAsync(char? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000117BB File Offset: 0x0000F9BB
		public override Task WriteValueAsync(DateTime value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000117D8 File Offset: 0x0000F9D8
		internal async Task DoWriteValueAsync(DateTime value, CancellationToken cancellationToken)
		{
			await base.InternalWriteValueAsync(JsonToken.Date, cancellationToken).ConfigureAwait(false);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			if (StringUtils.IsNullOrEmpty(base.DateFormatString))
			{
				int num = this.WriteValueToBuffer(value);
				await this._writer.WriteAsync(this._writeBuffer, 0, num, cancellationToken).ConfigureAwait(false);
			}
			else
			{
				await this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false);
				await this._writer.WriteAsync(value.ToString(base.DateFormatString, base.Culture), cancellationToken).ConfigureAwait(false);
				await this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false);
			}
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0001182B File Offset: 0x0000FA2B
		public override Task WriteValueAsync(DateTime? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00011846 File Offset: 0x0000FA46
		internal Task DoWriteValueAsync(DateTime? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00011867 File Offset: 0x0000FA67
		public override Task WriteValueAsync(DateTimeOffset value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00011884 File Offset: 0x0000FA84
		internal async Task DoWriteValueAsync(DateTimeOffset value, CancellationToken cancellationToken)
		{
			await base.InternalWriteValueAsync(JsonToken.Date, cancellationToken).ConfigureAwait(false);
			if (StringUtils.IsNullOrEmpty(base.DateFormatString))
			{
				int num = this.WriteValueToBuffer(value);
				await this._writer.WriteAsync(this._writeBuffer, 0, num, cancellationToken).ConfigureAwait(false);
			}
			else
			{
				await this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false);
				await this._writer.WriteAsync(value.ToString(base.DateFormatString, base.Culture), cancellationToken).ConfigureAwait(false);
				await this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false);
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000118D7 File Offset: 0x0000FAD7
		public override Task WriteValueAsync(DateTimeOffset? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000118F2 File Offset: 0x0000FAF2
		internal Task DoWriteValueAsync(DateTimeOffset? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00011913 File Offset: 0x0000FB13
		public override Task WriteValueAsync(decimal value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0001192E File Offset: 0x0000FB2E
		internal Task DoWriteValueAsync(decimal value, CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.Float, JsonConvert.ToString(value), cancellationToken);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0001193E File Offset: 0x0000FB3E
		public override Task WriteValueAsync(decimal? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00011959 File Offset: 0x0000FB59
		internal Task DoWriteValueAsync(decimal? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0001197A File Offset: 0x0000FB7A
		public override Task WriteValueAsync(double value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteValueAsync(value, false, cancellationToken);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00011996 File Offset: 0x0000FB96
		internal Task WriteValueAsync(double value, bool nullable, CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.Float, JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, nullable), cancellationToken);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000119B3 File Offset: 0x0000FBB3
		public override Task WriteValueAsync(double? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (value == null)
			{
				return this.WriteNullAsync(cancellationToken);
			}
			return this.WriteValueAsync(value.GetValueOrDefault(), true, cancellationToken);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000119E6 File Offset: 0x0000FBE6
		public override Task WriteValueAsync(float value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteValueAsync(value, false, cancellationToken);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00011A02 File Offset: 0x0000FC02
		internal Task WriteValueAsync(float value, bool nullable, CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.Float, JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, nullable), cancellationToken);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00011A1F File Offset: 0x0000FC1F
		public override Task WriteValueAsync(float? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (value == null)
			{
				return this.WriteNullAsync(cancellationToken);
			}
			return this.WriteValueAsync(value.GetValueOrDefault(), true, cancellationToken);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00011A52 File Offset: 0x0000FC52
		public override Task WriteValueAsync(Guid value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00011A70 File Offset: 0x0000FC70
		internal async Task DoWriteValueAsync(Guid value, CancellationToken cancellationToken)
		{
			await base.InternalWriteValueAsync(JsonToken.String, cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false);
			await this._writer.WriteAsync(value.ToString("D", CultureInfo.InvariantCulture), cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(this._quoteChar).ConfigureAwait(false);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00011AC3 File Offset: 0x0000FCC3
		public override Task WriteValueAsync(Guid? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00011ADE File Offset: 0x0000FCDE
		internal Task DoWriteValueAsync(Guid? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00011AFF File Offset: 0x0000FCFF
		public override Task WriteValueAsync(int value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)value, cancellationToken);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00011B1B File Offset: 0x0000FD1B
		public override Task WriteValueAsync(int? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00011B36 File Offset: 0x0000FD36
		internal Task DoWriteValueAsync(int? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00011B58 File Offset: 0x0000FD58
		public override Task WriteValueAsync(long value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync(value, cancellationToken);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00011B73 File Offset: 0x0000FD73
		public override Task WriteValueAsync(long? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00011B8E File Offset: 0x0000FD8E
		internal Task DoWriteValueAsync(long? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00011BAF File Offset: 0x0000FDAF
		internal Task WriteValueAsync(BigInteger value, CancellationToken cancellationToken)
		{
			return this.WriteValueInternalAsync(JsonToken.Integer, value.ToString(CultureInfo.InvariantCulture), cancellationToken);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00011BC8 File Offset: 0x0000FDC8
		public override Task WriteValueAsync([Nullable(2)] object value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (value == null)
			{
				return this.WriteNullAsync(cancellationToken);
			}
			if (value is BigInteger)
			{
				BigInteger bigInteger = (BigInteger)value;
				return this.WriteValueAsync(bigInteger, cancellationToken);
			}
			return JsonWriter.WriteValueAsync(this, ConvertUtils.GetTypeCode(value.GetType()), value, cancellationToken);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00011C1C File Offset: 0x0000FE1C
		[CLSCompliant(false)]
		public override Task WriteValueAsync(sbyte value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)value, cancellationToken);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00011C38 File Offset: 0x0000FE38
		[CLSCompliant(false)]
		public override Task WriteValueAsync(sbyte? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00011C53 File Offset: 0x0000FE53
		internal Task DoWriteValueAsync(sbyte? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00011C75 File Offset: 0x0000FE75
		public override Task WriteValueAsync(short value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)value, cancellationToken);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00011C91 File Offset: 0x0000FE91
		public override Task WriteValueAsync(short? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00011CAC File Offset: 0x0000FEAC
		internal Task DoWriteValueAsync(short? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00011CCE File Offset: 0x0000FECE
		public override Task WriteValueAsync([Nullable(2)] string value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00011CEC File Offset: 0x0000FEEC
		internal Task DoWriteValueAsync([Nullable(2)] string value, CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteValueAsync(JsonToken.String, cancellationToken);
			if (!task.IsCompletedSuccessfully())
			{
				return this.DoWriteValueAsync(task, value, cancellationToken);
			}
			if (value != null)
			{
				return this.WriteEscapedStringAsync(value, true, cancellationToken);
			}
			return this._writer.WriteAsync(JsonConvert.Null, cancellationToken);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00011D34 File Offset: 0x0000FF34
		private async Task DoWriteValueAsync(Task task, [Nullable(2)] string value, CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(false);
			await ((value == null) ? this._writer.WriteAsync(JsonConvert.Null, cancellationToken) : this.WriteEscapedStringAsync(value, true, cancellationToken)).ConfigureAwait(false);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00011D8F File Offset: 0x0000FF8F
		public override Task WriteValueAsync(TimeSpan value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00011DAC File Offset: 0x0000FFAC
		internal async Task DoWriteValueAsync(TimeSpan value, CancellationToken cancellationToken)
		{
			await base.InternalWriteValueAsync(JsonToken.String, cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(this._quoteChar, cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(value.ToString(null, CultureInfo.InvariantCulture), cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(this._quoteChar, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00011DFF File Offset: 0x0000FFFF
		public override Task WriteValueAsync(TimeSpan? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00011E1A File Offset: 0x0001001A
		internal Task DoWriteValueAsync(TimeSpan? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00011E3B File Offset: 0x0001003B
		[CLSCompliant(false)]
		public override Task WriteValueAsync(uint value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)((ulong)value), cancellationToken);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00011E57 File Offset: 0x00010057
		[CLSCompliant(false)]
		public override Task WriteValueAsync(uint? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00011E72 File Offset: 0x00010072
		internal Task DoWriteValueAsync(uint? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)((ulong)value.GetValueOrDefault()), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00011E94 File Offset: 0x00010094
		[CLSCompliant(false)]
		public override Task WriteValueAsync(ulong value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync(value, cancellationToken);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00011EAF File Offset: 0x000100AF
		[CLSCompliant(false)]
		public override Task WriteValueAsync(ulong? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00011ECA File Offset: 0x000100CA
		internal Task DoWriteValueAsync(ulong? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00011EEB File Offset: 0x000100EB
		public override Task WriteValueAsync([Nullable(2)] Uri value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (!(value == null))
			{
				return this.WriteValueNotNullAsync(value, cancellationToken);
			}
			return this.WriteNullAsync(cancellationToken);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00011F18 File Offset: 0x00010118
		internal Task WriteValueNotNullAsync(Uri value, CancellationToken cancellationToken)
		{
			Task task = base.InternalWriteValueAsync(JsonToken.String, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this.WriteEscapedStringAsync(value.OriginalString, true, cancellationToken);
			}
			return this.WriteValueNotNullAsync(task, value, cancellationToken);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00011F50 File Offset: 0x00010150
		internal async Task WriteValueNotNullAsync(Task task, Uri value, CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(false);
			await this.WriteEscapedStringAsync(value.OriginalString, true, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00011FAB File Offset: 0x000101AB
		[CLSCompliant(false)]
		public override Task WriteValueAsync(ushort value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.WriteIntegerValueAsync((long)((ulong)value), cancellationToken);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00011FC7 File Offset: 0x000101C7
		[CLSCompliant(false)]
		public override Task WriteValueAsync(ushort? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return this.DoWriteValueAsync(value, cancellationToken);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00011FE2 File Offset: 0x000101E2
		internal Task DoWriteValueAsync(ushort? value, CancellationToken cancellationToken)
		{
			if (value != null)
			{
				return this.WriteIntegerValueAsync((long)((ulong)value.GetValueOrDefault()), cancellationToken);
			}
			return this.DoWriteNullAsync(cancellationToken);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00012004 File Offset: 0x00010204
		public override Task WriteCommentAsync([Nullable(2)] string text, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteCommentAsync(text, cancellationToken);
			}
			return this.DoWriteCommentAsync(text, cancellationToken);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00012020 File Offset: 0x00010220
		internal async Task DoWriteCommentAsync([Nullable(2)] string text, CancellationToken cancellationToken)
		{
			await base.InternalWriteCommentAsync(cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync("/*", cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync(text ?? string.Empty, cancellationToken).ConfigureAwait(false);
			await this._writer.WriteAsync("*/", cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00012073 File Offset: 0x00010273
		public override Task WriteEndArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteEndArrayAsync(cancellationToken);
			}
			return base.InternalWriteEndAsync(JsonContainerType.Array, cancellationToken);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0001208D File Offset: 0x0001028D
		public override Task WriteEndConstructorAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteEndConstructorAsync(cancellationToken);
			}
			return base.InternalWriteEndAsync(JsonContainerType.Constructor, cancellationToken);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000120A7 File Offset: 0x000102A7
		public override Task WriteEndObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteEndObjectAsync(cancellationToken);
			}
			return base.InternalWriteEndAsync(JsonContainerType.Object, cancellationToken);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x000120C1 File Offset: 0x000102C1
		public override Task WriteRawValueAsync([Nullable(2)] string json, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!this._safeAsync)
			{
				return base.WriteRawValueAsync(json, cancellationToken);
			}
			return this.DoWriteRawValueAsync(json, cancellationToken);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000120DC File Offset: 0x000102DC
		internal Task DoWriteRawValueAsync([Nullable(2)] string json, CancellationToken cancellationToken)
		{
			base.UpdateScopeWithFinishedValue();
			Task task = base.AutoCompleteAsync(JsonToken.Undefined, cancellationToken);
			if (task.IsCompletedSuccessfully())
			{
				return this.WriteRawAsync(json, cancellationToken);
			}
			return this.DoWriteRawValueAsync(task, json, cancellationToken);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00012114 File Offset: 0x00010314
		private async Task DoWriteRawValueAsync(Task task, [Nullable(2)] string json, CancellationToken cancellationToken)
		{
			await task.ConfigureAwait(false);
			await this.WriteRawAsync(json, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00012170 File Offset: 0x00010370
		internal char[] EnsureWriteBuffer(int length, int copyTo)
		{
			if (length < 35)
			{
				length = 35;
			}
			char[] writeBuffer = this._writeBuffer;
			if (writeBuffer == null)
			{
				return this._writeBuffer = BufferUtils.RentBuffer(this._arrayPool, length);
			}
			if (writeBuffer.Length >= length)
			{
				return writeBuffer;
			}
			char[] array = BufferUtils.RentBuffer(this._arrayPool, length);
			if (copyTo != 0)
			{
				Array.Copy(writeBuffer, array, copyTo);
			}
			BufferUtils.ReturnBuffer(this._arrayPool, writeBuffer);
			this._writeBuffer = array;
			return array;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000333 RID: 819 RVA: 0x000121DA File Offset: 0x000103DA
		private Base64Encoder Base64Encoder
		{
			get
			{
				if (this._base64Encoder == null)
				{
					this._base64Encoder = new Base64Encoder(this._writer);
				}
				return this._base64Encoder;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000334 RID: 820 RVA: 0x000121FB File Offset: 0x000103FB
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00012203 File Offset: 0x00010403
		[Nullable(2)]
		public IArrayPool<char> ArrayPool
		{
			[NullableContext(2)]
			get
			{
				return this._arrayPool;
			}
			[NullableContext(2)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._arrayPool = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0001221A File Offset: 0x0001041A
		// (set) Token: 0x06000337 RID: 823 RVA: 0x00012222 File Offset: 0x00010422
		public int Indentation
		{
			get
			{
				return this._indentation;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Indentation value must be greater than 0.");
				}
				this._indentation = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0001223A File Offset: 0x0001043A
		// (set) Token: 0x06000339 RID: 825 RVA: 0x00012242 File Offset: 0x00010442
		public char QuoteChar
		{
			get
			{
				return this._quoteChar;
			}
			set
			{
				if (value != '"' && value != '\'')
				{
					throw new ArgumentException("Invalid JavaScript string quote character. Valid quote characters are ' and \".");
				}
				this._quoteChar = value;
				this.UpdateCharEscapeFlags();
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00012266 File Offset: 0x00010466
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0001226E File Offset: 0x0001046E
		public char IndentChar
		{
			get
			{
				return this._indentChar;
			}
			set
			{
				if (value != this._indentChar)
				{
					this._indentChar = value;
					this._indentChars = null;
				}
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00012287 File Offset: 0x00010487
		// (set) Token: 0x0600033D RID: 829 RVA: 0x0001228F File Offset: 0x0001048F
		public bool QuoteName
		{
			get
			{
				return this._quoteName;
			}
			set
			{
				this._quoteName = value;
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00012298 File Offset: 0x00010498
		public JsonTextWriter(TextWriter textWriter)
		{
			if (textWriter == null)
			{
				throw new ArgumentNullException("textWriter");
			}
			this._writer = textWriter;
			this._quoteChar = '"';
			this._quoteName = true;
			this._indentChar = ' ';
			this._indentation = 2;
			this.UpdateCharEscapeFlags();
			this._safeAsync = base.GetType() == typeof(JsonTextWriter);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000122FF File Offset: 0x000104FF
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0001230C File Offset: 0x0001050C
		public override void Close()
		{
			base.Close();
			this.CloseBufferAndWriter();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001231A File Offset: 0x0001051A
		private void CloseBufferAndWriter()
		{
			if (this._writeBuffer != null)
			{
				BufferUtils.ReturnBuffer(this._arrayPool, this._writeBuffer);
				this._writeBuffer = null;
			}
			if (base.CloseOutput)
			{
				TextWriter writer = this._writer;
				if (writer == null)
				{
					return;
				}
				writer.Close();
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00012354 File Offset: 0x00010554
		public override void WriteStartObject()
		{
			base.InternalWriteStart(JsonToken.StartObject, JsonContainerType.Object);
			this._writer.Write('{');
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001236B File Offset: 0x0001056B
		public override void WriteStartArray()
		{
			base.InternalWriteStart(JsonToken.StartArray, JsonContainerType.Array);
			this._writer.Write('[');
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00012382 File Offset: 0x00010582
		public override void WriteStartConstructor(string name)
		{
			base.InternalWriteStart(JsonToken.StartConstructor, JsonContainerType.Constructor);
			this._writer.Write("new ");
			this._writer.Write(name);
			this._writer.Write('(');
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000123B8 File Offset: 0x000105B8
		protected override void WriteEnd(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				this._writer.Write('}');
				return;
			case JsonToken.EndArray:
				this._writer.Write(']');
				return;
			case JsonToken.EndConstructor:
				this._writer.Write(')');
				return;
			default:
				throw JsonWriterException.Create(this, "Invalid JsonToken: " + token.ToString(), null);
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00012424 File Offset: 0x00010624
		public override void WritePropertyName(string name)
		{
			base.InternalWritePropertyName(name);
			this.WriteEscapedString(name, this._quoteName);
			this._writer.Write(':');
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00012448 File Offset: 0x00010648
		public override void WritePropertyName(string name, bool escape)
		{
			base.InternalWritePropertyName(name);
			if (escape)
			{
				this.WriteEscapedString(name, this._quoteName);
			}
			else
			{
				if (this._quoteName)
				{
					this._writer.Write(this._quoteChar);
				}
				this._writer.Write(name);
				if (this._quoteName)
				{
					this._writer.Write(this._quoteChar);
				}
			}
			this._writer.Write(':');
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000124B9 File Offset: 0x000106B9
		internal override void OnStringEscapeHandlingChanged()
		{
			this.UpdateCharEscapeFlags();
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000124C1 File Offset: 0x000106C1
		private void UpdateCharEscapeFlags()
		{
			this._charEscapeFlags = JavaScriptUtils.GetCharEscapeFlags(base.StringEscapeHandling, this._quoteChar);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000124DC File Offset: 0x000106DC
		protected override void WriteIndent()
		{
			int num = base.Top * this._indentation;
			int num2 = this.SetIndentChars();
			this._writer.Write(this._indentChars, 0, num2 + Math.Min(num, 12));
			while ((num -= 12) > 0)
			{
				this._writer.Write(this._indentChars, num2, Math.Min(num, 12));
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00012540 File Offset: 0x00010740
		private int SetIndentChars()
		{
			string newLine = this._writer.NewLine;
			int length = newLine.Length;
			bool flag = this._indentChars != null && this._indentChars.Length == 12 + length;
			if (flag)
			{
				for (int num = 0; num != length; num++)
				{
					if (newLine[num] != this._indentChars[num])
					{
						flag = false;
						break;
					}
				}
			}
			if (!flag)
			{
				this._indentChars = (newLine + new string(this._indentChar, 12)).ToCharArray();
			}
			return length;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000125C0 File Offset: 0x000107C0
		protected override void WriteValueDelimiter()
		{
			this._writer.Write(',');
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000125CF File Offset: 0x000107CF
		protected override void WriteIndentSpace()
		{
			this._writer.Write(' ');
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000125DE File Offset: 0x000107DE
		private void WriteValueInternal(string value, JsonToken token)
		{
			this._writer.Write(value);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000125EC File Offset: 0x000107EC
		[NullableContext(2)]
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				BigInteger bigInteger = (BigInteger)value;
				base.InternalWriteValue(JsonToken.Integer);
				this.WriteValueInternal(bigInteger.ToString(CultureInfo.InvariantCulture), JsonToken.String);
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0001262B File Offset: 0x0001082B
		public override void WriteNull()
		{
			base.InternalWriteValue(JsonToken.Null);
			this.WriteValueInternal(JsonConvert.Null, JsonToken.Null);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00012642 File Offset: 0x00010842
		public override void WriteUndefined()
		{
			base.InternalWriteValue(JsonToken.Undefined);
			this.WriteValueInternal(JsonConvert.Undefined, JsonToken.Undefined);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00012659 File Offset: 0x00010859
		[NullableContext(2)]
		public override void WriteRaw(string json)
		{
			base.InternalWriteRaw();
			this._writer.Write(json);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001266D File Offset: 0x0001086D
		[NullableContext(2)]
		public override void WriteValue(string value)
		{
			base.InternalWriteValue(JsonToken.String);
			if (value == null)
			{
				this.WriteValueInternal(JsonConvert.Null, JsonToken.Null);
				return;
			}
			this.WriteEscapedString(value, true);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00012690 File Offset: 0x00010890
		private void WriteEscapedString(string value, bool quote)
		{
			this.EnsureWriteBuffer();
			JavaScriptUtils.WriteEscapedJavaScriptString(this._writer, value, this._quoteChar, quote, this._charEscapeFlags, base.StringEscapeHandling, this._arrayPool, ref this._writeBuffer);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000126C3 File Offset: 0x000108C3
		public override void WriteValue(int value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue(value);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000126D3 File Offset: 0x000108D3
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((long)((ulong)value));
		}

		// Token: 0x06000357 RID: 855 RVA: 0x000126E4 File Offset: 0x000108E4
		public override void WriteValue(long value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue(value);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000126F4 File Offset: 0x000108F4
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue(value, false);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00012705 File Offset: 0x00010905
		public override void WriteValue(float value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, false), JsonToken.Float);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00012728 File Offset: 0x00010928
		public override void WriteValue(float? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value.GetValueOrDefault(), base.FloatFormatHandling, this.QuoteChar, true), JsonToken.Float);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00012761 File Offset: 0x00010961
		public override void WriteValue(double value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, false), JsonToken.Float);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00012784 File Offset: 0x00010984
		public override void WriteValue(double? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value.GetValueOrDefault(), base.FloatFormatHandling, this.QuoteChar, true), JsonToken.Float);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x000127BD File Offset: 0x000109BD
		public override void WriteValue(bool value)
		{
			base.InternalWriteValue(JsonToken.Boolean);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Boolean);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000126C3 File Offset: 0x000108C3
		public override void WriteValue(short value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((int)value);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000126C3 File Offset: 0x000108C3
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((int)value);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000127D5 File Offset: 0x000109D5
		public override void WriteValue(char value)
		{
			base.InternalWriteValue(JsonToken.String);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.String);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000126C3 File Offset: 0x000108C3
		public override void WriteValue(byte value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((int)value);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000126C3 File Offset: 0x000108C3
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((int)value);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000127ED File Offset: 0x000109ED
		public override void WriteValue(decimal value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Float);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00012804 File Offset: 0x00010A04
		public override void WriteValue(DateTime value)
		{
			base.InternalWriteValue(JsonToken.Date);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			if (StringUtils.IsNullOrEmpty(base.DateFormatString))
			{
				int num = this.WriteValueToBuffer(value);
				this._writer.Write(this._writeBuffer, 0, num);
				return;
			}
			this._writer.Write(this._quoteChar);
			this._writer.Write(value.ToString(base.DateFormatString, base.Culture));
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00012890 File Offset: 0x00010A90
		private int WriteValueToBuffer(DateTime value)
		{
			this.EnsureWriteBuffer();
			int num = 0;
			this._writeBuffer[num++] = this._quoteChar;
			num = DateTimeUtils.WriteDateTimeString(this._writeBuffer, num, value, null, value.Kind, base.DateFormatHandling);
			this._writeBuffer[num++] = this._quoteChar;
			return num;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x000128F0 File Offset: 0x00010AF0
		[NullableContext(2)]
		public override void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Bytes);
			this._writer.Write(this._quoteChar);
			this.Base64Encoder.Encode(value, 0, value.Length);
			this.Base64Encoder.Flush();
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001294C File Offset: 0x00010B4C
		public override void WriteValue(DateTimeOffset value)
		{
			base.InternalWriteValue(JsonToken.Date);
			if (StringUtils.IsNullOrEmpty(base.DateFormatString))
			{
				int num = this.WriteValueToBuffer(value);
				this._writer.Write(this._writeBuffer, 0, num);
				return;
			}
			this._writer.Write(this._quoteChar);
			this._writer.Write(value.ToString(base.DateFormatString, base.Culture));
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000129CC File Offset: 0x00010BCC
		private int WriteValueToBuffer(DateTimeOffset value)
		{
			this.EnsureWriteBuffer();
			int num = 0;
			this._writeBuffer[num++] = this._quoteChar;
			num = DateTimeUtils.WriteDateTimeString(this._writeBuffer, num, (base.DateFormatHandling == DateFormatHandling.IsoDateFormat) ? value.DateTime : value.UtcDateTime, new TimeSpan?(value.Offset), DateTimeKind.Local, base.DateFormatHandling);
			this._writeBuffer[num++] = this._quoteChar;
			return num;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00012A40 File Offset: 0x00010C40
		public override void WriteValue(Guid value)
		{
			base.InternalWriteValue(JsonToken.String);
			string text = value.ToString("D", CultureInfo.InvariantCulture);
			this._writer.Write(this._quoteChar);
			this._writer.Write(text);
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00012A98 File Offset: 0x00010C98
		public override void WriteValue(TimeSpan value)
		{
			base.InternalWriteValue(JsonToken.String);
			string text = value.ToString(null, CultureInfo.InvariantCulture);
			this._writer.Write(this._quoteChar);
			this._writer.Write(text);
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00012AE9 File Offset: 0x00010CE9
		[NullableContext(2)]
		public override void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.String);
			this.WriteEscapedString(value.OriginalString, true);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00012B10 File Offset: 0x00010D10
		[NullableContext(2)]
		public override void WriteComment(string text)
		{
			base.InternalWriteComment();
			this._writer.Write("/*");
			this._writer.Write(text);
			this._writer.Write("*/");
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00012B44 File Offset: 0x00010D44
		public override void WriteWhitespace(string ws)
		{
			base.InternalWriteWhitespace(ws);
			this._writer.Write(ws);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00012B59 File Offset: 0x00010D59
		private void EnsureWriteBuffer()
		{
			if (this._writeBuffer == null)
			{
				this._writeBuffer = BufferUtils.RentBuffer(this._arrayPool, 35);
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00012B78 File Offset: 0x00010D78
		private void WriteIntegerValue(long value)
		{
			if (value >= 0L && value <= 9L)
			{
				this._writer.Write((char)(48L + value));
				return;
			}
			bool flag = value < 0L;
			this.WriteIntegerValue((ulong)(flag ? (-(ulong)value) : value), flag);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00012BB8 File Offset: 0x00010DB8
		private void WriteIntegerValue(ulong value, bool negative)
		{
			if (!negative & (value <= 9UL))
			{
				this._writer.Write((char)(48UL + value));
				return;
			}
			int num = this.WriteNumberToBuffer(value, negative);
			this._writer.Write(this._writeBuffer, 0, num);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00012C04 File Offset: 0x00010E04
		private int WriteNumberToBuffer(ulong value, bool negative)
		{
			if (value <= (ulong)(-1))
			{
				return this.WriteNumberToBuffer((uint)value, negative);
			}
			this.EnsureWriteBuffer();
			int num = MathUtils.IntLength(value);
			if (negative)
			{
				num++;
				this._writeBuffer[0] = '-';
			}
			int num2 = num;
			do
			{
				ulong num3 = value / 10UL;
				ulong num4 = value - num3 * 10UL;
				this._writeBuffer[--num2] = (char)(48UL + num4);
				value = num3;
			}
			while (value != 0UL);
			return num;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00012C68 File Offset: 0x00010E68
		private void WriteIntegerValue(int value)
		{
			if (value >= 0 && value <= 9)
			{
				this._writer.Write((char)(48 + value));
				return;
			}
			bool flag = value < 0;
			this.WriteIntegerValue((uint)(flag ? (-(uint)value) : value), flag);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00012CA4 File Offset: 0x00010EA4
		private void WriteIntegerValue(uint value, bool negative)
		{
			if (!negative & (value <= 9U))
			{
				this._writer.Write((char)(48U + value));
				return;
			}
			int num = this.WriteNumberToBuffer(value, negative);
			this._writer.Write(this._writeBuffer, 0, num);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00012CF0 File Offset: 0x00010EF0
		private int WriteNumberToBuffer(uint value, bool negative)
		{
			this.EnsureWriteBuffer();
			int num = MathUtils.IntLength((ulong)value);
			if (negative)
			{
				num++;
				this._writeBuffer[0] = '-';
			}
			int num2 = num;
			do
			{
				uint num3 = value / 10U;
				uint num4 = value - num3 * 10U;
				this._writeBuffer[--num2] = (char)(48U + num4);
				value = num3;
			}
			while (value != 0U);
			return num;
		}

		// Token: 0x04000214 RID: 532
		private readonly bool _safeAsync;

		// Token: 0x04000215 RID: 533
		private const int IndentCharBufferSize = 12;

		// Token: 0x04000216 RID: 534
		private readonly TextWriter _writer;

		// Token: 0x04000217 RID: 535
		[Nullable(2)]
		private Base64Encoder _base64Encoder;

		// Token: 0x04000218 RID: 536
		private char _indentChar;

		// Token: 0x04000219 RID: 537
		private int _indentation;

		// Token: 0x0400021A RID: 538
		private char _quoteChar;

		// Token: 0x0400021B RID: 539
		private bool _quoteName;

		// Token: 0x0400021C RID: 540
		[Nullable(2)]
		private bool[] _charEscapeFlags;

		// Token: 0x0400021D RID: 541
		[Nullable(2)]
		private char[] _writeBuffer;

		// Token: 0x0400021E RID: 542
		[Nullable(2)]
		private IArrayPool<char> _arrayPool;

		// Token: 0x0400021F RID: 543
		[Nullable(2)]
		private char[] _indentChars;
	}
}
