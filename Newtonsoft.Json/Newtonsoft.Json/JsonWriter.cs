using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000077 RID: 119
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JsonWriter : IDisposable
	{
		// Token: 0x060003ED RID: 1005 RVA: 0x00016B98 File Offset: 0x00014D98
		internal Task AutoCompleteAsync(JsonToken tokenBeingWritten, CancellationToken cancellationToken)
		{
			JsonWriter.State currentState = this._currentState;
			JsonWriter.State state = JsonWriter.StateArray[(int)tokenBeingWritten][(int)currentState];
			if (state == JsonWriter.State.Error)
			{
				throw JsonWriterException.Create(this, "Token {0} in state {1} would result in an invalid JSON object.".FormatWith(CultureInfo.InvariantCulture, tokenBeingWritten.ToString(), currentState.ToString()), null);
			}
			this._currentState = state;
			if (this._formatting == Formatting.Indented)
			{
				switch (currentState)
				{
				case JsonWriter.State.Start:
					goto IL_00F3;
				case JsonWriter.State.Property:
					return this.WriteIndentSpaceAsync(cancellationToken);
				case JsonWriter.State.Object:
					if (tokenBeingWritten == JsonToken.PropertyName)
					{
						return this.AutoCompleteAsync(cancellationToken);
					}
					if (tokenBeingWritten != JsonToken.Comment)
					{
						return this.WriteValueDelimiterAsync(cancellationToken);
					}
					goto IL_00F3;
				case JsonWriter.State.ArrayStart:
				case JsonWriter.State.ConstructorStart:
					return this.WriteIndentAsync(cancellationToken);
				case JsonWriter.State.Array:
				case JsonWriter.State.Constructor:
					if (tokenBeingWritten != JsonToken.Comment)
					{
						return this.AutoCompleteAsync(cancellationToken);
					}
					return this.WriteIndentAsync(cancellationToken);
				}
				if (tokenBeingWritten == JsonToken.PropertyName)
				{
					return this.WriteIndentAsync(cancellationToken);
				}
			}
			else if (tokenBeingWritten != JsonToken.Comment)
			{
				switch (currentState)
				{
				case JsonWriter.State.Object:
				case JsonWriter.State.Array:
				case JsonWriter.State.Constructor:
					return this.WriteValueDelimiterAsync(cancellationToken);
				}
			}
			IL_00F3:
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00016CA0 File Offset: 0x00014EA0
		private async Task AutoCompleteAsync(CancellationToken cancellationToken)
		{
			await this.WriteValueDelimiterAsync(cancellationToken).ConfigureAwait(false);
			await this.WriteIndentAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00016CEB File Offset: 0x00014EEB
		public virtual Task CloseAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.Close();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00016D08 File Offset: 0x00014F08
		public virtual Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.Flush();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00016D25 File Offset: 0x00014F25
		protected virtual Task WriteEndAsync(JsonToken token, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteEnd(token);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00016D43 File Offset: 0x00014F43
		protected virtual Task WriteIndentAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteIndent();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00016D60 File Offset: 0x00014F60
		protected virtual Task WriteValueDelimiterAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValueDelimiter();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00016D7D File Offset: 0x00014F7D
		protected virtual Task WriteIndentSpaceAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteIndentSpace();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00016D9A File Offset: 0x00014F9A
		public virtual Task WriteRawAsync([Nullable(2)] string json, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteRaw(json);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00016DB8 File Offset: 0x00014FB8
		public virtual Task WriteEndAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteEnd();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00016DD8 File Offset: 0x00014FD8
		internal Task WriteEndInternalAsync(CancellationToken cancellationToken)
		{
			JsonContainerType jsonContainerType = this.Peek();
			switch (jsonContainerType)
			{
			case JsonContainerType.Object:
				return this.WriteEndObjectAsync(cancellationToken);
			case JsonContainerType.Array:
				return this.WriteEndArrayAsync(cancellationToken);
			case JsonContainerType.Constructor:
				return this.WriteEndConstructorAsync(cancellationToken);
			default:
				if (cancellationToken.IsCancellationRequested)
				{
					return cancellationToken.FromCanceled();
				}
				throw JsonWriterException.Create(this, "Unexpected type when writing end: " + jsonContainerType.ToString(), null);
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00016E48 File Offset: 0x00015048
		internal Task InternalWriteEndAsync(JsonContainerType type, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			int num = this.CalculateLevelsToComplete(type);
			while (num-- > 0)
			{
				JsonToken closeTokenForType = this.GetCloseTokenForType(this.Pop());
				Task task;
				if (this._currentState == JsonWriter.State.Property)
				{
					task = this.WriteNullAsync(cancellationToken);
					if (!task.IsCompletedSuccessfully())
					{
						return this.<InternalWriteEndAsync>g__AwaitProperty|11_0(task, num, closeTokenForType, cancellationToken);
					}
				}
				if (this._formatting == Formatting.Indented && this._currentState != JsonWriter.State.ObjectStart && this._currentState != JsonWriter.State.ArrayStart)
				{
					task = this.WriteIndentAsync(cancellationToken);
					if (!task.IsCompletedSuccessfully())
					{
						return this.<InternalWriteEndAsync>g__AwaitIndent|11_1(task, num, closeTokenForType, cancellationToken);
					}
				}
				task = this.WriteEndAsync(closeTokenForType, cancellationToken);
				if (!task.IsCompletedSuccessfully())
				{
					return this.<InternalWriteEndAsync>g__AwaitEnd|11_2(task, num, cancellationToken);
				}
				this.UpdateCurrentState();
			}
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00016F0A File Offset: 0x0001510A
		public virtual Task WriteEndArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteEndArray();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00016F27 File Offset: 0x00015127
		public virtual Task WriteEndConstructorAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteEndConstructor();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00016F44 File Offset: 0x00015144
		public virtual Task WriteEndObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteEndObject();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00016F61 File Offset: 0x00015161
		public virtual Task WriteNullAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteNull();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00016F7E File Offset: 0x0001517E
		public virtual Task WritePropertyNameAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WritePropertyName(name);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00016F9C File Offset: 0x0001519C
		public virtual Task WritePropertyNameAsync(string name, bool escape, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WritePropertyName(name, escape);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00016FBB File Offset: 0x000151BB
		internal Task InternalWritePropertyNameAsync(string name, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this._currentPosition.PropertyName = name;
			return this.AutoCompleteAsync(JsonToken.PropertyName, cancellationToken);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00016FE1 File Offset: 0x000151E1
		public virtual Task WriteStartArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteStartArray();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00017000 File Offset: 0x00015200
		internal async Task InternalWriteStartAsync(JsonToken token, JsonContainerType container, CancellationToken cancellationToken)
		{
			this.UpdateScopeWithFinishedValue();
			await this.AutoCompleteAsync(token, cancellationToken).ConfigureAwait(false);
			this.Push(container);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001705B File Offset: 0x0001525B
		public virtual Task WriteCommentAsync([Nullable(2)] string text, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteComment(text);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00017079 File Offset: 0x00015279
		internal Task InternalWriteCommentAsync(CancellationToken cancellationToken)
		{
			return this.AutoCompleteAsync(JsonToken.Comment, cancellationToken);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00017083 File Offset: 0x00015283
		public virtual Task WriteRawValueAsync([Nullable(2)] string json, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteRawValue(json);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000170A1 File Offset: 0x000152A1
		public virtual Task WriteStartConstructorAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteStartConstructor(name);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000170BF File Offset: 0x000152BF
		public virtual Task WriteStartObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteStartObject();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000170DC File Offset: 0x000152DC
		public Task WriteTokenAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.WriteTokenAsync(reader, true, cancellationToken);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000170E7 File Offset: 0x000152E7
		public Task WriteTokenAsync(JsonReader reader, bool writeChildren, CancellationToken cancellationToken = default(CancellationToken))
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			return this.WriteTokenAsync(reader, writeChildren, true, true, cancellationToken);
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000170FF File Offset: 0x000152FF
		public Task WriteTokenAsync(JsonToken token, CancellationToken cancellationToken = default(CancellationToken))
		{
			return this.WriteTokenAsync(token, null, cancellationToken);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001710C File Offset: 0x0001530C
		public Task WriteTokenAsync(JsonToken token, [Nullable(2)] object value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			switch (token)
			{
			case JsonToken.None:
				return AsyncUtils.CompletedTask;
			case JsonToken.StartObject:
				return this.WriteStartObjectAsync(cancellationToken);
			case JsonToken.StartArray:
				return this.WriteStartArrayAsync(cancellationToken);
			case JsonToken.StartConstructor:
				ValidationUtils.ArgumentNotNull(value, "value");
				return this.WriteStartConstructorAsync(value.ToString(), cancellationToken);
			case JsonToken.PropertyName:
				ValidationUtils.ArgumentNotNull(value, "value");
				return this.WritePropertyNameAsync(value.ToString(), cancellationToken);
			case JsonToken.Comment:
				return this.WriteCommentAsync((value != null) ? value.ToString() : null, cancellationToken);
			case JsonToken.Raw:
				return this.WriteRawValueAsync((value != null) ? value.ToString() : null, cancellationToken);
			case JsonToken.Integer:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value;
					return this.WriteValueAsync(bigInteger, cancellationToken);
				}
				return this.WriteValueAsync(Convert.ToInt64(value, CultureInfo.InvariantCulture), cancellationToken);
			case JsonToken.Float:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is decimal)
				{
					decimal num = (decimal)value;
					return this.WriteValueAsync(num, cancellationToken);
				}
				if (value is double)
				{
					double num2 = (double)value;
					return this.WriteValueAsync(num2, cancellationToken);
				}
				if (value is float)
				{
					float num3 = (float)value;
					return this.WriteValueAsync(num3, cancellationToken);
				}
				return this.WriteValueAsync(Convert.ToDouble(value, CultureInfo.InvariantCulture), cancellationToken);
			case JsonToken.String:
				ValidationUtils.ArgumentNotNull(value, "value");
				return this.WriteValueAsync(value.ToString(), cancellationToken);
			case JsonToken.Boolean:
				ValidationUtils.ArgumentNotNull(value, "value");
				return this.WriteValueAsync(Convert.ToBoolean(value, CultureInfo.InvariantCulture), cancellationToken);
			case JsonToken.Null:
				return this.WriteNullAsync(cancellationToken);
			case JsonToken.Undefined:
				return this.WriteUndefinedAsync(cancellationToken);
			case JsonToken.EndObject:
				return this.WriteEndObjectAsync(cancellationToken);
			case JsonToken.EndArray:
				return this.WriteEndArrayAsync(cancellationToken);
			case JsonToken.EndConstructor:
				return this.WriteEndConstructorAsync(cancellationToken);
			case JsonToken.Date:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is DateTimeOffset)
				{
					DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
					return this.WriteValueAsync(dateTimeOffset, cancellationToken);
				}
				return this.WriteValueAsync(Convert.ToDateTime(value, CultureInfo.InvariantCulture), cancellationToken);
			case JsonToken.Bytes:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is Guid)
				{
					Guid guid = (Guid)value;
					return this.WriteValueAsync(guid, cancellationToken);
				}
				return this.WriteValueAsync((byte[])value, cancellationToken);
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("token", token, "Unexpected token type.");
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00017370 File Offset: 0x00015570
		internal virtual async Task WriteTokenAsync(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments, CancellationToken cancellationToken)
		{
			int initialDepth = this.CalculateWriteTokenInitialDepth(reader);
			for (;;)
			{
				if (!writeDateConstructorAsDate || reader.TokenType != JsonToken.StartConstructor)
				{
					goto IL_00E7;
				}
				object value = reader.Value;
				if (!string.Equals((value != null) ? value.ToString() : null, "Date", StringComparison.Ordinal))
				{
					goto IL_00E7;
				}
				await this.WriteConstructorDateAsync(reader, cancellationToken).ConfigureAwait(false);
				IL_0180:
				bool flag = initialDepth - 1 < reader.Depth - (JsonTokenUtils.IsEndToken(reader.TokenType) ? 1 : 0) && writeChildren;
				if (flag)
				{
					flag = await reader.ReadAsync(cancellationToken).ConfigureAwait(false);
				}
				if (!flag)
				{
					break;
				}
				continue;
				IL_00E7:
				if (writeComments || reader.TokenType != JsonToken.Comment)
				{
					await this.WriteTokenAsync(reader.TokenType, reader.Value, cancellationToken).ConfigureAwait(false);
					goto IL_0180;
				}
				goto IL_0180;
			}
			if (this.IsWriteTokenIncomplete(reader, writeChildren, initialDepth))
			{
				throw JsonWriterException.Create(this, "Unexpected end when reading token.", null);
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000173E0 File Offset: 0x000155E0
		internal async Task WriteTokenSyncReadingAsync(JsonReader reader, CancellationToken cancellationToken)
		{
			int initialDepth = this.CalculateWriteTokenInitialDepth(reader);
			for (;;)
			{
				if (reader.TokenType != JsonToken.StartConstructor)
				{
					goto IL_0066;
				}
				object value = reader.Value;
				if (!string.Equals((value != null) ? value.ToString() : null, "Date", StringComparison.Ordinal))
				{
					goto IL_0066;
				}
				this.WriteConstructorDate(reader);
				IL_0082:
				bool flag = initialDepth - 1 < reader.Depth - (JsonTokenUtils.IsEndToken(reader.TokenType) ? 1 : 0);
				if (flag)
				{
					flag = await reader.ReadAsync(cancellationToken).ConfigureAwait(false);
				}
				if (!flag)
				{
					break;
				}
				continue;
				IL_0066:
				this.WriteToken(reader.TokenType, reader.Value);
				goto IL_0082;
			}
			if (initialDepth < this.CalculateWriteTokenFinalDepth(reader))
			{
				throw JsonWriterException.Create(this, "Unexpected end when reading token.", null);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00017434 File Offset: 0x00015634
		private async Task WriteConstructorDateAsync(JsonReader reader, CancellationToken cancellationToken)
		{
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter = reader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw JsonWriterException.Create(this, "Unexpected end when reading date constructor.", null);
			}
			if (reader.TokenType != JsonToken.Integer)
			{
				throw JsonWriterException.Create(this, "Unexpected token when reading date constructor. Expected Integer, got " + reader.TokenType.ToString(), null);
			}
			DateTime date = DateTimeUtils.ConvertJavaScriptTicksToDateTime((long)reader.Value);
			configuredTaskAwaiter = reader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
			if (!configuredTaskAwaiter.IsCompleted)
			{
				await configuredTaskAwaiter;
				configuredTaskAwaiter = configuredTaskAwaiter2;
				configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
			}
			if (!configuredTaskAwaiter.GetResult())
			{
				throw JsonWriterException.Create(this, "Unexpected end when reading date constructor.", null);
			}
			if (reader.TokenType != JsonToken.EndConstructor)
			{
				throw JsonWriterException.Create(this, "Unexpected token when reading date constructor. Expected EndConstructor, got " + reader.TokenType.ToString(), null);
			}
			await this.WriteValueAsync(date, cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00017487 File Offset: 0x00015687
		public virtual Task WriteValueAsync(bool value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000174A5 File Offset: 0x000156A5
		public virtual Task WriteValueAsync(bool? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000174C3 File Offset: 0x000156C3
		public virtual Task WriteValueAsync(byte value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000174E1 File Offset: 0x000156E1
		public virtual Task WriteValueAsync(byte? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000174FF File Offset: 0x000156FF
		public virtual Task WriteValueAsync([Nullable(2)] byte[] value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001751D File Offset: 0x0001571D
		public virtual Task WriteValueAsync(char value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001753B File Offset: 0x0001573B
		public virtual Task WriteValueAsync(char? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00017559 File Offset: 0x00015759
		public virtual Task WriteValueAsync(DateTime value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00017577 File Offset: 0x00015777
		public virtual Task WriteValueAsync(DateTime? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00017595 File Offset: 0x00015795
		public virtual Task WriteValueAsync(DateTimeOffset value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000175B3 File Offset: 0x000157B3
		public virtual Task WriteValueAsync(DateTimeOffset? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000175D1 File Offset: 0x000157D1
		public virtual Task WriteValueAsync(decimal value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000175EF File Offset: 0x000157EF
		public virtual Task WriteValueAsync(decimal? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001760D File Offset: 0x0001580D
		public virtual Task WriteValueAsync(double value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001762B File Offset: 0x0001582B
		public virtual Task WriteValueAsync(double? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x00017649 File Offset: 0x00015849
		public virtual Task WriteValueAsync(float value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00017667 File Offset: 0x00015867
		public virtual Task WriteValueAsync(float? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00017685 File Offset: 0x00015885
		public virtual Task WriteValueAsync(Guid value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000176A3 File Offset: 0x000158A3
		public virtual Task WriteValueAsync(Guid? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000176C1 File Offset: 0x000158C1
		public virtual Task WriteValueAsync(int value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000176DF File Offset: 0x000158DF
		public virtual Task WriteValueAsync(int? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000176FD File Offset: 0x000158FD
		public virtual Task WriteValueAsync(long value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001771B File Offset: 0x0001591B
		public virtual Task WriteValueAsync(long? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00017739 File Offset: 0x00015939
		public virtual Task WriteValueAsync([Nullable(2)] object value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00017757 File Offset: 0x00015957
		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(sbyte value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00017775 File Offset: 0x00015975
		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(sbyte? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00017793 File Offset: 0x00015993
		public virtual Task WriteValueAsync(short value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000177B1 File Offset: 0x000159B1
		public virtual Task WriteValueAsync(short? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000177CF File Offset: 0x000159CF
		public virtual Task WriteValueAsync([Nullable(2)] string value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000177ED File Offset: 0x000159ED
		public virtual Task WriteValueAsync(TimeSpan value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0001780B File Offset: 0x00015A0B
		public virtual Task WriteValueAsync(TimeSpan? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00017829 File Offset: 0x00015A29
		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(uint value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00017847 File Offset: 0x00015A47
		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(uint? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00017865 File Offset: 0x00015A65
		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(ulong value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00017883 File Offset: 0x00015A83
		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(ulong? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000178A1 File Offset: 0x00015AA1
		public virtual Task WriteValueAsync([Nullable(2)] Uri value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000178BF File Offset: 0x00015ABF
		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(ushort value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000178DD File Offset: 0x00015ADD
		[CLSCompliant(false)]
		public virtual Task WriteValueAsync(ushort? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteValue(value);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000178FB File Offset: 0x00015AFB
		public virtual Task WriteUndefinedAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteUndefined();
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00017918 File Offset: 0x00015B18
		public virtual Task WriteWhitespaceAsync(string ws, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.WriteWhitespace(ws);
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00017936 File Offset: 0x00015B36
		internal Task InternalWriteValueAsync(JsonToken token, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			this.UpdateScopeWithFinishedValue();
			return this.AutoCompleteAsync(token, cancellationToken);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00017958 File Offset: 0x00015B58
		protected Task SetWriteStateAsync(JsonToken token, object value, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			switch (token)
			{
			case JsonToken.StartObject:
				return this.InternalWriteStartAsync(token, JsonContainerType.Object, cancellationToken);
			case JsonToken.StartArray:
				return this.InternalWriteStartAsync(token, JsonContainerType.Array, cancellationToken);
			case JsonToken.StartConstructor:
				return this.InternalWriteStartAsync(token, JsonContainerType.Constructor, cancellationToken);
			case JsonToken.PropertyName:
			{
				string text = value as string;
				if (text == null)
				{
					throw new ArgumentException("A name is required when setting property name state.", "value");
				}
				return this.InternalWritePropertyNameAsync(text, cancellationToken);
			}
			case JsonToken.Comment:
				return this.InternalWriteCommentAsync(cancellationToken);
			case JsonToken.Raw:
				return AsyncUtils.CompletedTask;
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				return this.InternalWriteValueAsync(token, cancellationToken);
			case JsonToken.EndObject:
				return this.InternalWriteEndAsync(JsonContainerType.Object, cancellationToken);
			case JsonToken.EndArray:
				return this.InternalWriteEndAsync(JsonContainerType.Array, cancellationToken);
			case JsonToken.EndConstructor:
				return this.InternalWriteEndAsync(JsonContainerType.Constructor, cancellationToken);
			default:
				throw new ArgumentOutOfRangeException("token");
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00017A40 File Offset: 0x00015C40
		internal static Task WriteValueAsync(JsonWriter writer, PrimitiveTypeCode typeCode, object value, CancellationToken cancellationToken)
		{
			for (;;)
			{
				switch (typeCode)
				{
				case PrimitiveTypeCode.Char:
					goto IL_00AD;
				case PrimitiveTypeCode.CharNullable:
					goto IL_00BB;
				case PrimitiveTypeCode.Boolean:
					goto IL_00DC;
				case PrimitiveTypeCode.BooleanNullable:
					goto IL_00EA;
				case PrimitiveTypeCode.SByte:
					goto IL_010B;
				case PrimitiveTypeCode.SByteNullable:
					goto IL_0119;
				case PrimitiveTypeCode.Int16:
					goto IL_013A;
				case PrimitiveTypeCode.Int16Nullable:
					goto IL_0148;
				case PrimitiveTypeCode.UInt16:
					goto IL_016A;
				case PrimitiveTypeCode.UInt16Nullable:
					goto IL_0178;
				case PrimitiveTypeCode.Int32:
					goto IL_019A;
				case PrimitiveTypeCode.Int32Nullable:
					goto IL_01A8;
				case PrimitiveTypeCode.Byte:
					goto IL_01CA;
				case PrimitiveTypeCode.ByteNullable:
					goto IL_01D8;
				case PrimitiveTypeCode.UInt32:
					goto IL_01FA;
				case PrimitiveTypeCode.UInt32Nullable:
					goto IL_0208;
				case PrimitiveTypeCode.Int64:
					goto IL_022A;
				case PrimitiveTypeCode.Int64Nullable:
					goto IL_0238;
				case PrimitiveTypeCode.UInt64:
					goto IL_025A;
				case PrimitiveTypeCode.UInt64Nullable:
					goto IL_0268;
				case PrimitiveTypeCode.Single:
					goto IL_028A;
				case PrimitiveTypeCode.SingleNullable:
					goto IL_0298;
				case PrimitiveTypeCode.Double:
					goto IL_02BA;
				case PrimitiveTypeCode.DoubleNullable:
					goto IL_02C8;
				case PrimitiveTypeCode.DateTime:
					goto IL_02EA;
				case PrimitiveTypeCode.DateTimeNullable:
					goto IL_02F8;
				case PrimitiveTypeCode.DateTimeOffset:
					goto IL_031A;
				case PrimitiveTypeCode.DateTimeOffsetNullable:
					goto IL_0328;
				case PrimitiveTypeCode.Decimal:
					goto IL_034A;
				case PrimitiveTypeCode.DecimalNullable:
					goto IL_0358;
				case PrimitiveTypeCode.Guid:
					goto IL_037A;
				case PrimitiveTypeCode.GuidNullable:
					goto IL_0388;
				case PrimitiveTypeCode.TimeSpan:
					goto IL_03AA;
				case PrimitiveTypeCode.TimeSpanNullable:
					goto IL_03B8;
				case PrimitiveTypeCode.BigInteger:
					goto IL_03DA;
				case PrimitiveTypeCode.BigIntegerNullable:
					goto IL_03ED;
				case PrimitiveTypeCode.Uri:
					goto IL_0414;
				case PrimitiveTypeCode.String:
					goto IL_0422;
				case PrimitiveTypeCode.Bytes:
					goto IL_0430;
				case PrimitiveTypeCode.DBNull:
					goto IL_043E;
				default:
				{
					IConvertible convertible = value as IConvertible;
					if (convertible == null)
					{
						goto IL_045F;
					}
					JsonWriter.ResolveConvertibleValue(convertible, out typeCode, out value);
					break;
				}
				}
			}
			IL_00AD:
			return writer.WriteValueAsync((char)value, cancellationToken);
			IL_00BB:
			return writer.WriteValueAsync((value == null) ? null : new char?((char)value), cancellationToken);
			IL_00DC:
			return writer.WriteValueAsync((bool)value, cancellationToken);
			IL_00EA:
			return writer.WriteValueAsync((value == null) ? null : new bool?((bool)value), cancellationToken);
			IL_010B:
			return writer.WriteValueAsync((sbyte)value, cancellationToken);
			IL_0119:
			return writer.WriteValueAsync((value == null) ? null : new sbyte?((sbyte)value), cancellationToken);
			IL_013A:
			return writer.WriteValueAsync((short)value, cancellationToken);
			IL_0148:
			return writer.WriteValueAsync((value == null) ? null : new short?((short)value), cancellationToken);
			IL_016A:
			return writer.WriteValueAsync((ushort)value, cancellationToken);
			IL_0178:
			return writer.WriteValueAsync((value == null) ? null : new ushort?((ushort)value), cancellationToken);
			IL_019A:
			return writer.WriteValueAsync((int)value, cancellationToken);
			IL_01A8:
			return writer.WriteValueAsync((value == null) ? null : new int?((int)value), cancellationToken);
			IL_01CA:
			return writer.WriteValueAsync((byte)value, cancellationToken);
			IL_01D8:
			return writer.WriteValueAsync((value == null) ? null : new byte?((byte)value), cancellationToken);
			IL_01FA:
			return writer.WriteValueAsync((uint)value, cancellationToken);
			IL_0208:
			return writer.WriteValueAsync((value == null) ? null : new uint?((uint)value), cancellationToken);
			IL_022A:
			return writer.WriteValueAsync((long)value, cancellationToken);
			IL_0238:
			return writer.WriteValueAsync((value == null) ? null : new long?((long)value), cancellationToken);
			IL_025A:
			return writer.WriteValueAsync((ulong)value, cancellationToken);
			IL_0268:
			return writer.WriteValueAsync((value == null) ? null : new ulong?((ulong)value), cancellationToken);
			IL_028A:
			return writer.WriteValueAsync((float)value, cancellationToken);
			IL_0298:
			return writer.WriteValueAsync((value == null) ? null : new float?((float)value), cancellationToken);
			IL_02BA:
			return writer.WriteValueAsync((double)value, cancellationToken);
			IL_02C8:
			return writer.WriteValueAsync((value == null) ? null : new double?((double)value), cancellationToken);
			IL_02EA:
			return writer.WriteValueAsync((DateTime)value, cancellationToken);
			IL_02F8:
			return writer.WriteValueAsync((value == null) ? null : new DateTime?((DateTime)value), cancellationToken);
			IL_031A:
			return writer.WriteValueAsync((DateTimeOffset)value, cancellationToken);
			IL_0328:
			return writer.WriteValueAsync((value == null) ? null : new DateTimeOffset?((DateTimeOffset)value), cancellationToken);
			IL_034A:
			return writer.WriteValueAsync((decimal)value, cancellationToken);
			IL_0358:
			return writer.WriteValueAsync((value == null) ? null : new decimal?((decimal)value), cancellationToken);
			IL_037A:
			return writer.WriteValueAsync((Guid)value, cancellationToken);
			IL_0388:
			return writer.WriteValueAsync((value == null) ? null : new Guid?((Guid)value), cancellationToken);
			IL_03AA:
			return writer.WriteValueAsync((TimeSpan)value, cancellationToken);
			IL_03B8:
			return writer.WriteValueAsync((value == null) ? null : new TimeSpan?((TimeSpan)value), cancellationToken);
			IL_03DA:
			return writer.WriteValueAsync((BigInteger)value, cancellationToken);
			IL_03ED:
			return writer.WriteValueAsync((value == null) ? null : new BigInteger?((BigInteger)value), cancellationToken);
			IL_0414:
			return writer.WriteValueAsync((Uri)value, cancellationToken);
			IL_0422:
			return writer.WriteValueAsync((string)value, cancellationToken);
			IL_0430:
			return writer.WriteValueAsync((byte[])value, cancellationToken);
			IL_043E:
			return writer.WriteNullAsync(cancellationToken);
			IL_045F:
			if (value == null)
			{
				return writer.WriteNullAsync(cancellationToken);
			}
			throw JsonWriter.CreateUnsupportedTypeException(writer, value);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00017EC0 File Offset: 0x000160C0
		internal static JsonWriter.State[][] BuildStateArray()
		{
			List<JsonWriter.State[]> list = JsonWriter.StateArrayTemplate.ToList<JsonWriter.State[]>();
			JsonWriter.State[] array = JsonWriter.StateArrayTemplate[0];
			JsonWriter.State[] array2 = JsonWriter.StateArrayTemplate[7];
			foreach (ulong num in EnumUtils.GetEnumValuesAndNames(typeof(JsonToken)).Values)
			{
				if (list.Count <= (int)num)
				{
					JsonToken jsonToken = (JsonToken)num;
					if (jsonToken - JsonToken.Integer <= 5 || jsonToken - JsonToken.Date <= 1)
					{
						list.Add(array2);
					}
					else
					{
						list.Add(array);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00018016 File Offset: 0x00016216
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x0001801E File Offset: 0x0001621E
		public bool CloseOutput { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x00018027 File Offset: 0x00016227
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0001802F File Offset: 0x0001622F
		public bool AutoCompleteOnClose { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00018038 File Offset: 0x00016238
		protected internal int Top
		{
			get
			{
				List<JsonPosition> stack = this._stack;
				int num = ((stack != null) ? stack.Count : 0);
				if (this.Peek() != JsonContainerType.None)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x00018068 File Offset: 0x00016268
		public WriteState WriteState
		{
			get
			{
				switch (this._currentState)
				{
				case JsonWriter.State.Start:
					return WriteState.Start;
				case JsonWriter.State.Property:
					return WriteState.Property;
				case JsonWriter.State.ObjectStart:
				case JsonWriter.State.Object:
					return WriteState.Object;
				case JsonWriter.State.ArrayStart:
				case JsonWriter.State.Array:
					return WriteState.Array;
				case JsonWriter.State.ConstructorStart:
				case JsonWriter.State.Constructor:
					return WriteState.Constructor;
				case JsonWriter.State.Closed:
					return WriteState.Closed;
				case JsonWriter.State.Error:
					return WriteState.Error;
				default:
					throw JsonWriterException.Create(this, "Invalid state: " + this._currentState.ToString(), null);
				}
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x000180DC File Offset: 0x000162DC
		internal string ContainerPath
		{
			get
			{
				if (this._currentPosition.Type == JsonContainerType.None || this._stack == null)
				{
					return string.Empty;
				}
				return JsonPosition.BuildPath(this._stack, null);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x00018118 File Offset: 0x00016318
		public string Path
		{
			get
			{
				if (this._currentPosition.Type == JsonContainerType.None)
				{
					return string.Empty;
				}
				JsonPosition? jsonPosition = ((this._currentState != JsonWriter.State.ArrayStart && this._currentState != JsonWriter.State.ConstructorStart && this._currentState != JsonWriter.State.ObjectStart) ? new JsonPosition?(this._currentPosition) : null);
				return JsonPosition.BuildPath(this._stack, jsonPosition);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0001817E File Offset: 0x0001637E
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x00018186 File Offset: 0x00016386
		public Formatting Formatting
		{
			get
			{
				return this._formatting;
			}
			set
			{
				if (value < Formatting.None || value > Formatting.Indented)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._formatting = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x000181A2 File Offset: 0x000163A2
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x000181AA File Offset: 0x000163AA
		public DateFormatHandling DateFormatHandling
		{
			get
			{
				return this._dateFormatHandling;
			}
			set
			{
				if (value < DateFormatHandling.IsoDateFormat || value > DateFormatHandling.MicrosoftDateFormat)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._dateFormatHandling = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x000181C6 File Offset: 0x000163C6
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x000181CE File Offset: 0x000163CE
		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return this._dateTimeZoneHandling;
			}
			set
			{
				if (value < DateTimeZoneHandling.Local || value > DateTimeZoneHandling.RoundtripKind)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._dateTimeZoneHandling = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x000181EA File Offset: 0x000163EA
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x000181F2 File Offset: 0x000163F2
		public StringEscapeHandling StringEscapeHandling
		{
			get
			{
				return this._stringEscapeHandling;
			}
			set
			{
				if (value < StringEscapeHandling.Default || value > StringEscapeHandling.EscapeHtml)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._stringEscapeHandling = value;
				this.OnStringEscapeHandlingChanged();
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000153AD File Offset: 0x000135AD
		internal virtual void OnStringEscapeHandlingChanged()
		{
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00018214 File Offset: 0x00016414
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x0001821C File Offset: 0x0001641C
		public FloatFormatHandling FloatFormatHandling
		{
			get
			{
				return this._floatFormatHandling;
			}
			set
			{
				if (value < FloatFormatHandling.String || value > FloatFormatHandling.DefaultValue)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._floatFormatHandling = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00018238 File Offset: 0x00016438
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x00018240 File Offset: 0x00016440
		[Nullable(2)]
		public string DateFormatString
		{
			[NullableContext(2)]
			get
			{
				return this._dateFormatString;
			}
			[NullableContext(2)]
			set
			{
				this._dateFormatString = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00018249 File Offset: 0x00016449
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x0001825A File Offset: 0x0001645A
		public CultureInfo Culture
		{
			get
			{
				return this._culture ?? CultureInfo.InvariantCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00018263 File Offset: 0x00016463
		protected JsonWriter()
		{
			this._currentState = JsonWriter.State.Start;
			this._formatting = Formatting.None;
			this._dateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
			this.CloseOutput = true;
			this.AutoCompleteOnClose = true;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001828E File Offset: 0x0001648E
		internal void UpdateScopeWithFinishedValue()
		{
			if (this._currentPosition.HasIndex)
			{
				this._currentPosition.Position = this._currentPosition.Position + 1;
			}
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000182AD File Offset: 0x000164AD
		private void Push(JsonContainerType value)
		{
			if (this._currentPosition.Type != JsonContainerType.None)
			{
				if (this._stack == null)
				{
					this._stack = new List<JsonPosition>();
				}
				this._stack.Add(this._currentPosition);
			}
			this._currentPosition = new JsonPosition(value);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000182EC File Offset: 0x000164EC
		private JsonContainerType Pop()
		{
			ref JsonPosition currentPosition = this._currentPosition;
			if (this._stack != null && this._stack.Count > 0)
			{
				this._currentPosition = this._stack[this._stack.Count - 1];
				this._stack.RemoveAt(this._stack.Count - 1);
			}
			else
			{
				this._currentPosition = default(JsonPosition);
			}
			return currentPosition.Type;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001835E File Offset: 0x0001655E
		private JsonContainerType Peek()
		{
			return this._currentPosition.Type;
		}

		// Token: 0x06000457 RID: 1111
		public abstract void Flush();

		// Token: 0x06000458 RID: 1112 RVA: 0x0001836B File Offset: 0x0001656B
		public virtual void Close()
		{
			if (this.AutoCompleteOnClose)
			{
				this.AutoCompleteAll();
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001837B File Offset: 0x0001657B
		public virtual void WriteStartObject()
		{
			this.InternalWriteStart(JsonToken.StartObject, JsonContainerType.Object);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00018385 File Offset: 0x00016585
		public virtual void WriteEndObject()
		{
			this.InternalWriteEnd(JsonContainerType.Object);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001838E File Offset: 0x0001658E
		public virtual void WriteStartArray()
		{
			this.InternalWriteStart(JsonToken.StartArray, JsonContainerType.Array);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00018398 File Offset: 0x00016598
		public virtual void WriteEndArray()
		{
			this.InternalWriteEnd(JsonContainerType.Array);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000183A1 File Offset: 0x000165A1
		public virtual void WriteStartConstructor(string name)
		{
			this.InternalWriteStart(JsonToken.StartConstructor, JsonContainerType.Constructor);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000183AB File Offset: 0x000165AB
		public virtual void WriteEndConstructor()
		{
			this.InternalWriteEnd(JsonContainerType.Constructor);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000183B4 File Offset: 0x000165B4
		public virtual void WritePropertyName(string name)
		{
			this.InternalWritePropertyName(name);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000183BD File Offset: 0x000165BD
		public virtual void WritePropertyName(string name, bool escape)
		{
			this.WritePropertyName(name);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000183C6 File Offset: 0x000165C6
		public virtual void WriteEnd()
		{
			this.WriteEnd(this.Peek());
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000183D4 File Offset: 0x000165D4
		public void WriteToken(JsonReader reader)
		{
			this.WriteToken(reader, true);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000183DE File Offset: 0x000165DE
		public void WriteToken(JsonReader reader, bool writeChildren)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			this.WriteToken(reader, writeChildren, true, true);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000183F8 File Offset: 0x000165F8
		[NullableContext(2)]
		public void WriteToken(JsonToken token, object value)
		{
			switch (token)
			{
			case JsonToken.None:
				return;
			case JsonToken.StartObject:
				this.WriteStartObject();
				return;
			case JsonToken.StartArray:
				this.WriteStartArray();
				return;
			case JsonToken.StartConstructor:
				ValidationUtils.ArgumentNotNull(value, "value");
				this.WriteStartConstructor(value.ToString());
				return;
			case JsonToken.PropertyName:
				ValidationUtils.ArgumentNotNull(value, "value");
				this.WritePropertyName(value.ToString());
				return;
			case JsonToken.Comment:
				this.WriteComment((value != null) ? value.ToString() : null);
				return;
			case JsonToken.Raw:
				this.WriteRawValue((value != null) ? value.ToString() : null);
				return;
			case JsonToken.Integer:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value;
					this.WriteValue(bigInteger);
					return;
				}
				this.WriteValue(Convert.ToInt64(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.Float:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is decimal)
				{
					decimal num = (decimal)value;
					this.WriteValue(num);
					return;
				}
				if (value is double)
				{
					double num2 = (double)value;
					this.WriteValue(num2);
					return;
				}
				if (value is float)
				{
					float num3 = (float)value;
					this.WriteValue(num3);
					return;
				}
				this.WriteValue(Convert.ToDouble(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.String:
				this.WriteValue((value != null) ? value.ToString() : null);
				return;
			case JsonToken.Boolean:
				ValidationUtils.ArgumentNotNull(value, "value");
				this.WriteValue(Convert.ToBoolean(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.Null:
				this.WriteNull();
				return;
			case JsonToken.Undefined:
				this.WriteUndefined();
				return;
			case JsonToken.EndObject:
				this.WriteEndObject();
				return;
			case JsonToken.EndArray:
				this.WriteEndArray();
				return;
			case JsonToken.EndConstructor:
				this.WriteEndConstructor();
				return;
			case JsonToken.Date:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is DateTimeOffset)
				{
					DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
					this.WriteValue(dateTimeOffset);
					return;
				}
				this.WriteValue(Convert.ToDateTime(value, CultureInfo.InvariantCulture));
				return;
			case JsonToken.Bytes:
				ValidationUtils.ArgumentNotNull(value, "value");
				if (value is Guid)
				{
					Guid guid = (Guid)value;
					this.WriteValue(guid);
					return;
				}
				this.WriteValue((byte[])value);
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("token", token, "Unexpected token type.");
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00018627 File Offset: 0x00016827
		public void WriteToken(JsonToken token)
		{
			this.WriteToken(token, null);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00018634 File Offset: 0x00016834
		internal virtual void WriteToken(JsonReader reader, bool writeChildren, bool writeDateConstructorAsDate, bool writeComments)
		{
			int num = this.CalculateWriteTokenInitialDepth(reader);
			for (;;)
			{
				if (!writeDateConstructorAsDate || reader.TokenType != JsonToken.StartConstructor)
				{
					goto IL_003C;
				}
				object value = reader.Value;
				if (!string.Equals((value != null) ? value.ToString() : null, "Date", StringComparison.Ordinal))
				{
					goto IL_003C;
				}
				this.WriteConstructorDate(reader);
				IL_005B:
				if (num - 1 >= reader.Depth - (JsonTokenUtils.IsEndToken(reader.TokenType) ? 1 : 0) || !writeChildren || !reader.Read())
				{
					break;
				}
				continue;
				IL_003C:
				if (writeComments || reader.TokenType != JsonToken.Comment)
				{
					this.WriteToken(reader.TokenType, reader.Value);
					goto IL_005B;
				}
				goto IL_005B;
			}
			if (this.IsWriteTokenIncomplete(reader, writeChildren, num))
			{
				throw JsonWriterException.Create(this, "Unexpected end when reading token.", null);
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000186E0 File Offset: 0x000168E0
		private bool IsWriteTokenIncomplete(JsonReader reader, bool writeChildren, int initialDepth)
		{
			int num = this.CalculateWriteTokenFinalDepth(reader);
			return initialDepth < num || (writeChildren && initialDepth == num && JsonTokenUtils.IsStartToken(reader.TokenType));
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00018710 File Offset: 0x00016910
		private int CalculateWriteTokenInitialDepth(JsonReader reader)
		{
			JsonToken tokenType = reader.TokenType;
			if (tokenType == JsonToken.None)
			{
				return -1;
			}
			if (!JsonTokenUtils.IsStartToken(tokenType))
			{
				return reader.Depth + 1;
			}
			return reader.Depth;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00018740 File Offset: 0x00016940
		private int CalculateWriteTokenFinalDepth(JsonReader reader)
		{
			JsonToken tokenType = reader.TokenType;
			if (tokenType == JsonToken.None)
			{
				return -1;
			}
			if (!JsonTokenUtils.IsEndToken(tokenType))
			{
				return reader.Depth;
			}
			return reader.Depth - 1;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00018770 File Offset: 0x00016970
		private void WriteConstructorDate(JsonReader reader)
		{
			DateTime dateTime;
			string text;
			if (!JavaScriptUtils.TryGetDateFromConstructorJson(reader, out dateTime, out text))
			{
				throw JsonWriterException.Create(this, text, null);
			}
			this.WriteValue(dateTime);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001879C File Offset: 0x0001699C
		private void WriteEnd(JsonContainerType type)
		{
			switch (type)
			{
			case JsonContainerType.Object:
				this.WriteEndObject();
				return;
			case JsonContainerType.Array:
				this.WriteEndArray();
				return;
			case JsonContainerType.Constructor:
				this.WriteEndConstructor();
				return;
			default:
				throw JsonWriterException.Create(this, "Unexpected type when writing end: " + type.ToString(), null);
			}
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000187F2 File Offset: 0x000169F2
		private void AutoCompleteAll()
		{
			while (this.Top > 0)
			{
				this.WriteEnd();
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00018805 File Offset: 0x00016A05
		private JsonToken GetCloseTokenForType(JsonContainerType type)
		{
			switch (type)
			{
			case JsonContainerType.Object:
				return JsonToken.EndObject;
			case JsonContainerType.Array:
				return JsonToken.EndArray;
			case JsonContainerType.Constructor:
				return JsonToken.EndConstructor;
			default:
				throw JsonWriterException.Create(this, "No close token for type: " + type.ToString(), null);
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00018844 File Offset: 0x00016A44
		private void AutoCompleteClose(JsonContainerType type)
		{
			int num = this.CalculateLevelsToComplete(type);
			for (int i = 0; i < num; i++)
			{
				JsonToken closeTokenForType = this.GetCloseTokenForType(this.Pop());
				if (this._currentState == JsonWriter.State.Property)
				{
					this.WriteNull();
				}
				if (this._formatting == Formatting.Indented && this._currentState != JsonWriter.State.ObjectStart && this._currentState != JsonWriter.State.ArrayStart)
				{
					this.WriteIndent();
				}
				this.WriteEnd(closeTokenForType);
				this.UpdateCurrentState();
			}
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x000188B0 File Offset: 0x00016AB0
		private int CalculateLevelsToComplete(JsonContainerType type)
		{
			int num = 0;
			if (this._currentPosition.Type == type)
			{
				num = 1;
			}
			else
			{
				int num2 = this.Top - 2;
				for (int i = num2; i >= 0; i--)
				{
					int num3 = num2 - i;
					if (this._stack[num3].Type == type)
					{
						num = i + 2;
						break;
					}
				}
			}
			if (num == 0)
			{
				throw JsonWriterException.Create(this, "No token to close.", null);
			}
			return num;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x00018918 File Offset: 0x00016B18
		private void UpdateCurrentState()
		{
			JsonContainerType jsonContainerType = this.Peek();
			switch (jsonContainerType)
			{
			case JsonContainerType.None:
				this._currentState = JsonWriter.State.Start;
				return;
			case JsonContainerType.Object:
				this._currentState = JsonWriter.State.Object;
				return;
			case JsonContainerType.Array:
				this._currentState = JsonWriter.State.Array;
				return;
			case JsonContainerType.Constructor:
				this._currentState = JsonWriter.State.Array;
				return;
			default:
				throw JsonWriterException.Create(this, "Unknown JsonType: " + jsonContainerType.ToString(), null);
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000153AD File Offset: 0x000135AD
		protected virtual void WriteEnd(JsonToken token)
		{
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000153AD File Offset: 0x000135AD
		protected virtual void WriteIndent()
		{
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000153AD File Offset: 0x000135AD
		protected virtual void WriteValueDelimiter()
		{
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000153AD File Offset: 0x000135AD
		protected virtual void WriteIndentSpace()
		{
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00018984 File Offset: 0x00016B84
		internal void AutoComplete(JsonToken tokenBeingWritten)
		{
			JsonWriter.State state = JsonWriter.StateArray[(int)tokenBeingWritten][(int)this._currentState];
			if (state == JsonWriter.State.Error)
			{
				throw JsonWriterException.Create(this, "Token {0} in state {1} would result in an invalid JSON object.".FormatWith(CultureInfo.InvariantCulture, tokenBeingWritten.ToString(), this._currentState.ToString()), null);
			}
			if ((this._currentState == JsonWriter.State.Object || this._currentState == JsonWriter.State.Array || this._currentState == JsonWriter.State.Constructor) && tokenBeingWritten != JsonToken.Comment)
			{
				this.WriteValueDelimiter();
			}
			if (this._formatting == Formatting.Indented)
			{
				if (this._currentState == JsonWriter.State.Property)
				{
					this.WriteIndentSpace();
				}
				if (this._currentState == JsonWriter.State.Array || this._currentState == JsonWriter.State.ArrayStart || this._currentState == JsonWriter.State.Constructor || this._currentState == JsonWriter.State.ConstructorStart || (tokenBeingWritten == JsonToken.PropertyName && this._currentState != JsonWriter.State.Start))
				{
					this.WriteIndent();
				}
			}
			this._currentState = state;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00018A54 File Offset: 0x00016C54
		public virtual void WriteNull()
		{
			this.InternalWriteValue(JsonToken.Null);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00018A5E File Offset: 0x00016C5E
		public virtual void WriteUndefined()
		{
			this.InternalWriteValue(JsonToken.Undefined);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00018A68 File Offset: 0x00016C68
		[NullableContext(2)]
		public virtual void WriteRaw(string json)
		{
			this.InternalWriteRaw();
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00018A70 File Offset: 0x00016C70
		[NullableContext(2)]
		public virtual void WriteRawValue(string json)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(JsonToken.Undefined);
			this.WriteRaw(json);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00018A87 File Offset: 0x00016C87
		[NullableContext(2)]
		public virtual void WriteValue(string value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00018A91 File Offset: 0x00016C91
		public virtual void WriteValue(int value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00018A91 File Offset: 0x00016C91
		[CLSCompliant(false)]
		public virtual void WriteValue(uint value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00018A91 File Offset: 0x00016C91
		public virtual void WriteValue(long value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00018A91 File Offset: 0x00016C91
		[CLSCompliant(false)]
		public virtual void WriteValue(ulong value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00018A9A File Offset: 0x00016C9A
		public virtual void WriteValue(float value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00018A9A File Offset: 0x00016C9A
		public virtual void WriteValue(double value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00018AA3 File Offset: 0x00016CA3
		public virtual void WriteValue(bool value)
		{
			this.InternalWriteValue(JsonToken.Boolean);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00018A91 File Offset: 0x00016C91
		public virtual void WriteValue(short value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00018A91 File Offset: 0x00016C91
		[CLSCompliant(false)]
		public virtual void WriteValue(ushort value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00018A87 File Offset: 0x00016C87
		public virtual void WriteValue(char value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00018A91 File Offset: 0x00016C91
		public virtual void WriteValue(byte value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00018A91 File Offset: 0x00016C91
		[CLSCompliant(false)]
		public virtual void WriteValue(sbyte value)
		{
			this.InternalWriteValue(JsonToken.Integer);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00018A9A File Offset: 0x00016C9A
		public virtual void WriteValue(decimal value)
		{
			this.InternalWriteValue(JsonToken.Float);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00018AAD File Offset: 0x00016CAD
		public virtual void WriteValue(DateTime value)
		{
			this.InternalWriteValue(JsonToken.Date);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00018AAD File Offset: 0x00016CAD
		public virtual void WriteValue(DateTimeOffset value)
		{
			this.InternalWriteValue(JsonToken.Date);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00018A87 File Offset: 0x00016C87
		public virtual void WriteValue(Guid value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00018A87 File Offset: 0x00016C87
		public virtual void WriteValue(TimeSpan value)
		{
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00018AB7 File Offset: 0x00016CB7
		public virtual void WriteValue(int? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00018AD6 File Offset: 0x00016CD6
		[CLSCompliant(false)]
		public virtual void WriteValue(uint? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00018AF5 File Offset: 0x00016CF5
		public virtual void WriteValue(long? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00018B14 File Offset: 0x00016D14
		[CLSCompliant(false)]
		public virtual void WriteValue(ulong? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00018B33 File Offset: 0x00016D33
		public virtual void WriteValue(float? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00018B52 File Offset: 0x00016D52
		public virtual void WriteValue(double? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00018B71 File Offset: 0x00016D71
		public virtual void WriteValue(bool? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00018B90 File Offset: 0x00016D90
		public virtual void WriteValue(short? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00018BAF File Offset: 0x00016DAF
		[CLSCompliant(false)]
		public virtual void WriteValue(ushort? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00018BCE File Offset: 0x00016DCE
		public virtual void WriteValue(char? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00018BED File Offset: 0x00016DED
		public virtual void WriteValue(byte? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00018C0C File Offset: 0x00016E0C
		[CLSCompliant(false)]
		public virtual void WriteValue(sbyte? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00018C2B File Offset: 0x00016E2B
		public virtual void WriteValue(decimal? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00018C4A File Offset: 0x00016E4A
		public virtual void WriteValue(DateTime? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00018C69 File Offset: 0x00016E69
		public virtual void WriteValue(DateTimeOffset? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00018C88 File Offset: 0x00016E88
		public virtual void WriteValue(Guid? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00018CA7 File Offset: 0x00016EA7
		public virtual void WriteValue(TimeSpan? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.WriteValue(value.GetValueOrDefault());
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00018CC6 File Offset: 0x00016EC6
		[NullableContext(2)]
		public virtual void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.InternalWriteValue(JsonToken.Bytes);
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00018CDA File Offset: 0x00016EDA
		[NullableContext(2)]
		public virtual void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			this.InternalWriteValue(JsonToken.String);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00018CF4 File Offset: 0x00016EF4
		[NullableContext(2)]
		public virtual void WriteValue(object value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			if (value is BigInteger)
			{
				throw JsonWriter.CreateUnsupportedTypeException(this, value);
			}
			JsonWriter.WriteValue(this, ConvertUtils.GetTypeCode(value.GetType()), value);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00018D22 File Offset: 0x00016F22
		[NullableContext(2)]
		public virtual void WriteComment(string text)
		{
			this.InternalWriteComment();
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00018D2A File Offset: 0x00016F2A
		public virtual void WriteWhitespace(string ws)
		{
			this.InternalWriteWhitespace(ws);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00018D33 File Offset: 0x00016F33
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00018D42 File Offset: 0x00016F42
		protected virtual void Dispose(bool disposing)
		{
			if (this._currentState != JsonWriter.State.Closed && disposing)
			{
				this.Close();
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00018D5C File Offset: 0x00016F5C
		internal static void WriteValue(JsonWriter writer, PrimitiveTypeCode typeCode, object value)
		{
			for (;;)
			{
				switch (typeCode)
				{
				case PrimitiveTypeCode.Char:
					goto IL_00AD;
				case PrimitiveTypeCode.CharNullable:
					goto IL_00BA;
				case PrimitiveTypeCode.Boolean:
					goto IL_00DA;
				case PrimitiveTypeCode.BooleanNullable:
					goto IL_00E7;
				case PrimitiveTypeCode.SByte:
					goto IL_0107;
				case PrimitiveTypeCode.SByteNullable:
					goto IL_0114;
				case PrimitiveTypeCode.Int16:
					goto IL_0134;
				case PrimitiveTypeCode.Int16Nullable:
					goto IL_0141;
				case PrimitiveTypeCode.UInt16:
					goto IL_0162;
				case PrimitiveTypeCode.UInt16Nullable:
					goto IL_016F;
				case PrimitiveTypeCode.Int32:
					goto IL_0190;
				case PrimitiveTypeCode.Int32Nullable:
					goto IL_019D;
				case PrimitiveTypeCode.Byte:
					goto IL_01BE;
				case PrimitiveTypeCode.ByteNullable:
					goto IL_01CB;
				case PrimitiveTypeCode.UInt32:
					goto IL_01EC;
				case PrimitiveTypeCode.UInt32Nullable:
					goto IL_01F9;
				case PrimitiveTypeCode.Int64:
					goto IL_021A;
				case PrimitiveTypeCode.Int64Nullable:
					goto IL_0227;
				case PrimitiveTypeCode.UInt64:
					goto IL_0248;
				case PrimitiveTypeCode.UInt64Nullable:
					goto IL_0255;
				case PrimitiveTypeCode.Single:
					goto IL_0276;
				case PrimitiveTypeCode.SingleNullable:
					goto IL_0283;
				case PrimitiveTypeCode.Double:
					goto IL_02A4;
				case PrimitiveTypeCode.DoubleNullable:
					goto IL_02B1;
				case PrimitiveTypeCode.DateTime:
					goto IL_02D2;
				case PrimitiveTypeCode.DateTimeNullable:
					goto IL_02DF;
				case PrimitiveTypeCode.DateTimeOffset:
					goto IL_0300;
				case PrimitiveTypeCode.DateTimeOffsetNullable:
					goto IL_030D;
				case PrimitiveTypeCode.Decimal:
					goto IL_032E;
				case PrimitiveTypeCode.DecimalNullable:
					goto IL_033B;
				case PrimitiveTypeCode.Guid:
					goto IL_035C;
				case PrimitiveTypeCode.GuidNullable:
					goto IL_0369;
				case PrimitiveTypeCode.TimeSpan:
					goto IL_038A;
				case PrimitiveTypeCode.TimeSpanNullable:
					goto IL_0397;
				case PrimitiveTypeCode.BigInteger:
					goto IL_03B8;
				case PrimitiveTypeCode.BigIntegerNullable:
					goto IL_03CA;
				case PrimitiveTypeCode.Uri:
					goto IL_03F0;
				case PrimitiveTypeCode.String:
					goto IL_03FD;
				case PrimitiveTypeCode.Bytes:
					goto IL_040A;
				case PrimitiveTypeCode.DBNull:
					goto IL_0417;
				default:
				{
					IConvertible convertible = value as IConvertible;
					if (convertible == null)
					{
						goto IL_0437;
					}
					JsonWriter.ResolveConvertibleValue(convertible, out typeCode, out value);
					break;
				}
				}
			}
			IL_00AD:
			writer.WriteValue((char)value);
			return;
			IL_00BA:
			writer.WriteValue((value == null) ? null : new char?((char)value));
			return;
			IL_00DA:
			writer.WriteValue((bool)value);
			return;
			IL_00E7:
			writer.WriteValue((value == null) ? null : new bool?((bool)value));
			return;
			IL_0107:
			writer.WriteValue((sbyte)value);
			return;
			IL_0114:
			writer.WriteValue((value == null) ? null : new sbyte?((sbyte)value));
			return;
			IL_0134:
			writer.WriteValue((short)value);
			return;
			IL_0141:
			writer.WriteValue((value == null) ? null : new short?((short)value));
			return;
			IL_0162:
			writer.WriteValue((ushort)value);
			return;
			IL_016F:
			writer.WriteValue((value == null) ? null : new ushort?((ushort)value));
			return;
			IL_0190:
			writer.WriteValue((int)value);
			return;
			IL_019D:
			writer.WriteValue((value == null) ? null : new int?((int)value));
			return;
			IL_01BE:
			writer.WriteValue((byte)value);
			return;
			IL_01CB:
			writer.WriteValue((value == null) ? null : new byte?((byte)value));
			return;
			IL_01EC:
			writer.WriteValue((uint)value);
			return;
			IL_01F9:
			writer.WriteValue((value == null) ? null : new uint?((uint)value));
			return;
			IL_021A:
			writer.WriteValue((long)value);
			return;
			IL_0227:
			writer.WriteValue((value == null) ? null : new long?((long)value));
			return;
			IL_0248:
			writer.WriteValue((ulong)value);
			return;
			IL_0255:
			writer.WriteValue((value == null) ? null : new ulong?((ulong)value));
			return;
			IL_0276:
			writer.WriteValue((float)value);
			return;
			IL_0283:
			writer.WriteValue((value == null) ? null : new float?((float)value));
			return;
			IL_02A4:
			writer.WriteValue((double)value);
			return;
			IL_02B1:
			writer.WriteValue((value == null) ? null : new double?((double)value));
			return;
			IL_02D2:
			writer.WriteValue((DateTime)value);
			return;
			IL_02DF:
			writer.WriteValue((value == null) ? null : new DateTime?((DateTime)value));
			return;
			IL_0300:
			writer.WriteValue((DateTimeOffset)value);
			return;
			IL_030D:
			writer.WriteValue((value == null) ? null : new DateTimeOffset?((DateTimeOffset)value));
			return;
			IL_032E:
			writer.WriteValue((decimal)value);
			return;
			IL_033B:
			writer.WriteValue((value == null) ? null : new decimal?((decimal)value));
			return;
			IL_035C:
			writer.WriteValue((Guid)value);
			return;
			IL_0369:
			writer.WriteValue((value == null) ? null : new Guid?((Guid)value));
			return;
			IL_038A:
			writer.WriteValue((TimeSpan)value);
			return;
			IL_0397:
			writer.WriteValue((value == null) ? null : new TimeSpan?((TimeSpan)value));
			return;
			IL_03B8:
			writer.WriteValue((BigInteger)value);
			return;
			IL_03CA:
			writer.WriteValue((value == null) ? null : new BigInteger?((BigInteger)value));
			return;
			IL_03F0:
			writer.WriteValue((Uri)value);
			return;
			IL_03FD:
			writer.WriteValue((string)value);
			return;
			IL_040A:
			writer.WriteValue((byte[])value);
			return;
			IL_0417:
			writer.WriteNull();
			return;
			IL_0437:
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			throw JsonWriter.CreateUnsupportedTypeException(writer, value);
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x000191B4 File Offset: 0x000173B4
		private static void ResolveConvertibleValue(IConvertible convertible, out PrimitiveTypeCode typeCode, out object value)
		{
			TypeInformation typeInformation = ConvertUtils.GetTypeInformation(convertible);
			typeCode = ((typeInformation.TypeCode == PrimitiveTypeCode.Object) ? PrimitiveTypeCode.String : typeInformation.TypeCode);
			Type type = ((typeInformation.TypeCode == PrimitiveTypeCode.Object) ? typeof(string) : typeInformation.Type);
			value = convertible.ToType(type, CultureInfo.InvariantCulture);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00019207 File Offset: 0x00017407
		private static JsonWriterException CreateUnsupportedTypeException(JsonWriter writer, object value)
		{
			return JsonWriterException.Create(writer, "Unsupported type: {0}. Use the JsonSerializer class to get the object's JSON representation.".FormatWith(CultureInfo.InvariantCulture, value.GetType()), null);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00019228 File Offset: 0x00017428
		protected void SetWriteState(JsonToken token, object value)
		{
			switch (token)
			{
			case JsonToken.StartObject:
				this.InternalWriteStart(token, JsonContainerType.Object);
				return;
			case JsonToken.StartArray:
				this.InternalWriteStart(token, JsonContainerType.Array);
				return;
			case JsonToken.StartConstructor:
				this.InternalWriteStart(token, JsonContainerType.Constructor);
				return;
			case JsonToken.PropertyName:
			{
				string text = value as string;
				if (text == null)
				{
					throw new ArgumentException("A name is required when setting property name state.", "value");
				}
				this.InternalWritePropertyName(text);
				return;
			}
			case JsonToken.Comment:
				this.InternalWriteComment();
				return;
			case JsonToken.Raw:
				this.InternalWriteRaw();
				return;
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this.InternalWriteValue(token);
				return;
			case JsonToken.EndObject:
				this.InternalWriteEnd(JsonContainerType.Object);
				return;
			case JsonToken.EndArray:
				this.InternalWriteEnd(JsonContainerType.Array);
				return;
			case JsonToken.EndConstructor:
				this.InternalWriteEnd(JsonContainerType.Constructor);
				return;
			default:
				throw new ArgumentOutOfRangeException("token");
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000192F8 File Offset: 0x000174F8
		internal void InternalWriteEnd(JsonContainerType container)
		{
			this.AutoCompleteClose(container);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x00019301 File Offset: 0x00017501
		internal void InternalWritePropertyName(string name)
		{
			this._currentPosition.PropertyName = name;
			this.AutoComplete(JsonToken.PropertyName);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000153AD File Offset: 0x000135AD
		internal void InternalWriteRaw()
		{
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00019316 File Offset: 0x00017516
		internal void InternalWriteStart(JsonToken token, JsonContainerType container)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(token);
			this.Push(container);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001932C File Offset: 0x0001752C
		internal void InternalWriteValue(JsonToken token)
		{
			this.UpdateScopeWithFinishedValue();
			this.AutoComplete(token);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001933B File Offset: 0x0001753B
		internal void InternalWriteWhitespace(string ws)
		{
			if (ws != null && !StringUtils.IsWhiteSpace(ws))
			{
				throw JsonWriterException.Create(this, "Only white space characters should be used.", null);
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00019355 File Offset: 0x00017555
		internal void InternalWriteComment()
		{
			this.AutoComplete(JsonToken.Comment);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00019360 File Offset: 0x00017560
		[CompilerGenerated]
		private async Task <InternalWriteEndAsync>g__AwaitProperty|11_0(Task task, int LevelsToComplete, JsonToken token, CancellationToken CancellationToken)
		{
			await task.ConfigureAwait(false);
			if (this._formatting == Formatting.Indented && this._currentState != JsonWriter.State.ObjectStart && this._currentState != JsonWriter.State.ArrayStart)
			{
				await this.WriteIndentAsync(CancellationToken).ConfigureAwait(false);
			}
			await this.WriteEndAsync(token, CancellationToken).ConfigureAwait(false);
			this.UpdateCurrentState();
			await this.<InternalWriteEndAsync>g__AwaitRemaining|11_3(LevelsToComplete, CancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000193C4 File Offset: 0x000175C4
		[CompilerGenerated]
		private async Task <InternalWriteEndAsync>g__AwaitIndent|11_1(Task task, int LevelsToComplete, JsonToken token, CancellationToken CancellationToken)
		{
			await task.ConfigureAwait(false);
			await this.WriteEndAsync(token, CancellationToken).ConfigureAwait(false);
			this.UpdateCurrentState();
			await this.<InternalWriteEndAsync>g__AwaitRemaining|11_3(LevelsToComplete, CancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00019428 File Offset: 0x00017628
		[CompilerGenerated]
		private async Task <InternalWriteEndAsync>g__AwaitEnd|11_2(Task task, int LevelsToComplete, CancellationToken CancellationToken)
		{
			await task.ConfigureAwait(false);
			this.UpdateCurrentState();
			await this.<InternalWriteEndAsync>g__AwaitRemaining|11_3(LevelsToComplete, CancellationToken).ConfigureAwait(false);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00019484 File Offset: 0x00017684
		[CompilerGenerated]
		private async Task <InternalWriteEndAsync>g__AwaitRemaining|11_3(int LevelsToComplete, CancellationToken CancellationToken)
		{
			while (LevelsToComplete-- > 0)
			{
				JsonToken token = this.GetCloseTokenForType(this.Pop());
				if (this._currentState == JsonWriter.State.Property)
				{
					await this.WriteNullAsync(CancellationToken).ConfigureAwait(false);
				}
				if (this._formatting == Formatting.Indented && this._currentState != JsonWriter.State.ObjectStart && this._currentState != JsonWriter.State.ArrayStart)
				{
					await this.WriteIndentAsync(CancellationToken).ConfigureAwait(false);
				}
				await this.WriteEndAsync(token, CancellationToken).ConfigureAwait(false);
				this.UpdateCurrentState();
			}
		}

		// Token: 0x040002CD RID: 717
		private static readonly JsonWriter.State[][] StateArray = JsonWriter.BuildStateArray();

		// Token: 0x040002CE RID: 718
		internal static readonly JsonWriter.State[][] StateArrayTemplate = new JsonWriter.State[][]
		{
			new JsonWriter.State[]
			{
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.ConstructorStart,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Property,
				JsonWriter.State.Error,
				JsonWriter.State.Property,
				JsonWriter.State.Property,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Property,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Object,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Property,
				JsonWriter.State.ObjectStart,
				JsonWriter.State.Object,
				JsonWriter.State.ArrayStart,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			},
			new JsonWriter.State[]
			{
				JsonWriter.State.Start,
				JsonWriter.State.Object,
				JsonWriter.State.Error,
				JsonWriter.State.Error,
				JsonWriter.State.Array,
				JsonWriter.State.Array,
				JsonWriter.State.Constructor,
				JsonWriter.State.Constructor,
				JsonWriter.State.Error,
				JsonWriter.State.Error
			}
		};

		// Token: 0x040002CF RID: 719
		[Nullable(2)]
		private List<JsonPosition> _stack;

		// Token: 0x040002D0 RID: 720
		private JsonPosition _currentPosition;

		// Token: 0x040002D1 RID: 721
		private JsonWriter.State _currentState;

		// Token: 0x040002D2 RID: 722
		private Formatting _formatting;

		// Token: 0x040002D5 RID: 725
		private DateFormatHandling _dateFormatHandling;

		// Token: 0x040002D6 RID: 726
		private DateTimeZoneHandling _dateTimeZoneHandling;

		// Token: 0x040002D7 RID: 727
		private StringEscapeHandling _stringEscapeHandling;

		// Token: 0x040002D8 RID: 728
		private FloatFormatHandling _floatFormatHandling;

		// Token: 0x040002D9 RID: 729
		[Nullable(2)]
		private string _dateFormatString;

		// Token: 0x040002DA RID: 730
		[Nullable(2)]
		private CultureInfo _culture;

		// Token: 0x02000078 RID: 120
		[NullableContext(0)]
		internal enum State
		{
			// Token: 0x040002DC RID: 732
			Start,
			// Token: 0x040002DD RID: 733
			Property,
			// Token: 0x040002DE RID: 734
			ObjectStart,
			// Token: 0x040002DF RID: 735
			Object,
			// Token: 0x040002E0 RID: 736
			ArrayStart,
			// Token: 0x040002E1 RID: 737
			Array,
			// Token: 0x040002E2 RID: 738
			ConstructorStart,
			// Token: 0x040002E3 RID: 739
			Constructor,
			// Token: 0x040002E4 RID: 740
			Closed,
			// Token: 0x040002E5 RID: 741
			Error
		}
	}
}
