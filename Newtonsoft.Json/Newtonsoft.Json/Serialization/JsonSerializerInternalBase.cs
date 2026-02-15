using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000125 RID: 293
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class JsonSerializerInternalBase
	{
		// Token: 0x060008A9 RID: 2217 RVA: 0x00028689 File Offset: 0x00026889
		protected JsonSerializerInternalBase(JsonSerializer serializer)
		{
			ValidationUtils.ArgumentNotNull(serializer, "serializer");
			this.Serializer = serializer;
			this.TraceWriter = serializer.TraceWriter;
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x000286AF File Offset: 0x000268AF
		internal BidirectionalDictionary<string, object> DefaultReferenceMappings
		{
			get
			{
				if (this._mappings == null)
				{
					this._mappings = new BidirectionalDictionary<string, object>(EqualityComparer<string>.Default, new JsonSerializerInternalBase.ReferenceEqualsEqualityComparer(), "A different value already has the Id '{0}'.", "A different Id has already been assigned for value '{0}'. This error may be caused by an object being reused multiple times during deserialization and can be fixed with the setting ObjectCreationHandling.Replace.");
				}
				return this._mappings;
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x000286E0 File Offset: 0x000268E0
		protected NullValueHandling ResolvedNullValueHandling([Nullable(2)] JsonObjectContract containerContract, JsonProperty property)
		{
			NullValueHandling? nullValueHandling = property.NullValueHandling;
			if (nullValueHandling != null)
			{
				return nullValueHandling.GetValueOrDefault();
			}
			NullValueHandling? nullValueHandling2 = ((containerContract != null) ? containerContract.ItemNullValueHandling : null);
			if (nullValueHandling2 == null)
			{
				return this.Serializer._nullValueHandling;
			}
			return nullValueHandling2.GetValueOrDefault();
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00028736 File Offset: 0x00026936
		private ErrorContext GetErrorContext([Nullable(2)] object currentObject, [Nullable(2)] object member, string path, Exception error)
		{
			if (this._currentErrorContext == null)
			{
				this._currentErrorContext = new ErrorContext(currentObject, member, path, error);
			}
			if (this._currentErrorContext.Error != error)
			{
				throw new InvalidOperationException("Current error context error is different to requested error.");
			}
			return this._currentErrorContext;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00028770 File Offset: 0x00026970
		protected void ClearErrorContext()
		{
			if (this._currentErrorContext == null)
			{
				throw new InvalidOperationException("Could not clear error context. Error context is already null.");
			}
			this._currentErrorContext = null;
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0002878C File Offset: 0x0002698C
		[NullableContext(2)]
		protected bool IsErrorHandled(object currentObject, JsonContract contract, object keyValue, IJsonLineInfo lineInfo, [Nullable(1)] string path, [Nullable(1)] Exception ex)
		{
			ErrorContext errorContext = this.GetErrorContext(currentObject, keyValue, path, ex);
			if (this.TraceWriter != null && this.TraceWriter.LevelFilter >= TraceLevel.Error && !errorContext.Traced)
			{
				errorContext.Traced = true;
				string text = ((base.GetType() == typeof(JsonSerializerInternalWriter)) ? "Error serializing" : "Error deserializing");
				if (contract != null)
				{
					string text2 = text;
					string text3 = " ";
					Type underlyingType = contract.UnderlyingType;
					text = text2 + text3 + ((underlyingType != null) ? underlyingType.ToString() : null);
				}
				text = text + ". " + ex.Message;
				if (!(ex is JsonException))
				{
					text = JsonPosition.FormatMessage(lineInfo, path, text);
				}
				this.TraceWriter.Trace(TraceLevel.Error, text, ex);
			}
			if (contract != null && currentObject != null)
			{
				contract.InvokeOnError(currentObject, this.Serializer.Context, errorContext);
			}
			if (!errorContext.Handled)
			{
				this.Serializer.OnError(new ErrorEventArgs(currentObject, errorContext));
			}
			return errorContext.Handled;
		}

		// Token: 0x0400058A RID: 1418
		[Nullable(2)]
		private ErrorContext _currentErrorContext;

		// Token: 0x0400058B RID: 1419
		[Nullable(new byte[] { 2, 1, 1 })]
		private BidirectionalDictionary<string, object> _mappings;

		// Token: 0x0400058C RID: 1420
		internal readonly JsonSerializer Serializer;

		// Token: 0x0400058D RID: 1421
		[Nullable(2)]
		internal readonly ITraceWriter TraceWriter;

		// Token: 0x0400058E RID: 1422
		[Nullable(2)]
		protected JsonSerializerProxy InternalSerializer;

		// Token: 0x02000126 RID: 294
		[NullableContext(0)]
		private class ReferenceEqualsEqualityComparer : IEqualityComparer<object>
		{
			// Token: 0x060008AF RID: 2223 RVA: 0x00028888 File Offset: 0x00026A88
			[NullableContext(2)]
			bool IEqualityComparer<object>.Equals(object x, object y)
			{
				return x == y;
			}

			// Token: 0x060008B0 RID: 2224 RVA: 0x0002888E File Offset: 0x00026A8E
			[NullableContext(1)]
			int IEqualityComparer<object>.GetHashCode(object obj)
			{
				return RuntimeHelpers.GetHashCode(obj);
			}
		}
	}
}
