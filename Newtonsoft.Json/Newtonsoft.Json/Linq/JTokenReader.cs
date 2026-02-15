using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x02000189 RID: 393
	[NullableContext(1)]
	[Nullable(0)]
	public class JTokenReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0003B110 File Offset: 0x00039310
		[Nullable(2)]
		public JToken CurrentToken
		{
			[NullableContext(2)]
			get
			{
				return this._current;
			}
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x0003B118 File Offset: 0x00039318
		public JTokenReader(JToken token)
		{
			ValidationUtils.ArgumentNotNull(token, "token");
			this._root = token;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x0003B132 File Offset: 0x00039332
		public JTokenReader(JToken token, string initialPath)
			: this(token)
		{
			this._initialPath = initialPath;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0003B144 File Offset: 0x00039344
		public override bool Read()
		{
			if (base.CurrentState != JsonReader.State.Start)
			{
				if (this._current == null)
				{
					return false;
				}
				JContainer jcontainer = this._current as JContainer;
				if (jcontainer != null && this._parent != jcontainer)
				{
					return this.ReadInto(jcontainer);
				}
				return this.ReadOver(this._current);
			}
			else
			{
				if (this._current == this._root)
				{
					return false;
				}
				this._current = this._root;
				this.SetToken(this._current);
				return true;
			}
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x0003B1BC File Offset: 0x000393BC
		private bool ReadOver(JToken t)
		{
			if (t == this._root)
			{
				return this.ReadToEnd();
			}
			JToken next = t.Next;
			if (next != null && next != t && t != t.Parent.Last)
			{
				this._current = next;
				this.SetToken(this._current);
				return true;
			}
			if (t.Parent == null)
			{
				return this.ReadToEnd();
			}
			return this.SetEnd(t.Parent);
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x0003B225 File Offset: 0x00039425
		private bool ReadToEnd()
		{
			this._current = null;
			base.SetToken(JsonToken.None);
			return false;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0003B238 File Offset: 0x00039438
		private JsonToken? GetEndToken(JContainer c)
		{
			switch (c.Type)
			{
			case JTokenType.Object:
				return new JsonToken?(JsonToken.EndObject);
			case JTokenType.Array:
				return new JsonToken?(JsonToken.EndArray);
			case JTokenType.Constructor:
				return new JsonToken?(JsonToken.EndConstructor);
			case JTokenType.Property:
				return null;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", c.Type, "Unexpected JContainer type.");
			}
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x0003B2A4 File Offset: 0x000394A4
		private bool ReadInto(JContainer c)
		{
			JToken first = c.First;
			if (first == null)
			{
				return this.SetEnd(c);
			}
			this.SetToken(first);
			this._current = first;
			this._parent = c;
			return true;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x0003B2DC File Offset: 0x000394DC
		private bool SetEnd(JContainer c)
		{
			JsonToken? endToken = this.GetEndToken(c);
			if (endToken != null)
			{
				base.SetToken(endToken.GetValueOrDefault());
				this._current = c;
				this._parent = c;
				return true;
			}
			return this.ReadOver(c);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0003B320 File Offset: 0x00039520
		private void SetToken(JToken token)
		{
			switch (token.Type)
			{
			case JTokenType.Object:
				base.SetToken(JsonToken.StartObject);
				return;
			case JTokenType.Array:
				base.SetToken(JsonToken.StartArray);
				return;
			case JTokenType.Constructor:
				base.SetToken(JsonToken.StartConstructor, ((JConstructor)token).Name);
				return;
			case JTokenType.Property:
				base.SetToken(JsonToken.PropertyName, ((JProperty)token).Name);
				return;
			case JTokenType.Comment:
				base.SetToken(JsonToken.Comment, ((JValue)token).Value);
				return;
			case JTokenType.Integer:
				base.SetToken(JsonToken.Integer, ((JValue)token).Value);
				return;
			case JTokenType.Float:
				base.SetToken(JsonToken.Float, ((JValue)token).Value);
				return;
			case JTokenType.String:
				base.SetToken(JsonToken.String, ((JValue)token).Value);
				return;
			case JTokenType.Boolean:
				base.SetToken(JsonToken.Boolean, ((JValue)token).Value);
				return;
			case JTokenType.Null:
				base.SetToken(JsonToken.Null, ((JValue)token).Value);
				return;
			case JTokenType.Undefined:
				base.SetToken(JsonToken.Undefined, ((JValue)token).Value);
				return;
			case JTokenType.Date:
			{
				object obj = ((JValue)token).Value;
				if (obj is DateTime)
				{
					DateTime dateTime = (DateTime)obj;
					obj = DateTimeUtils.EnsureDateTime(dateTime, base.DateTimeZoneHandling);
				}
				base.SetToken(JsonToken.Date, obj);
				return;
			}
			case JTokenType.Raw:
				base.SetToken(JsonToken.Raw, ((JValue)token).Value);
				return;
			case JTokenType.Bytes:
				base.SetToken(JsonToken.Bytes, ((JValue)token).Value);
				return;
			case JTokenType.Guid:
				base.SetToken(JsonToken.String, this.SafeToString(((JValue)token).Value));
				return;
			case JTokenType.Uri:
			{
				object value = ((JValue)token).Value;
				JsonToken jsonToken = JsonToken.String;
				Uri uri = value as Uri;
				base.SetToken(jsonToken, (uri != null) ? uri.OriginalString : this.SafeToString(value));
				return;
			}
			case JTokenType.TimeSpan:
				base.SetToken(JsonToken.String, this.SafeToString(((JValue)token).Value));
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", token.Type, "Unexpected JTokenType.");
			}
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0003B521 File Offset: 0x00039721
		[NullableContext(2)]
		private string SafeToString(object value)
		{
			if (value == null)
			{
				return null;
			}
			return value.ToString();
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0003B530 File Offset: 0x00039730
		bool IJsonLineInfo.HasLineInfo()
		{
			if (base.CurrentState == JsonReader.State.Start)
			{
				return false;
			}
			IJsonLineInfo current = this._current;
			return current != null && current.HasLineInfo();
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x0003B55C File Offset: 0x0003975C
		int IJsonLineInfo.LineNumber
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start)
				{
					return 0;
				}
				IJsonLineInfo current = this._current;
				if (current != null)
				{
					return current.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000D85 RID: 3461 RVA: 0x0003B588 File Offset: 0x00039788
		int IJsonLineInfo.LinePosition
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start)
				{
					return 0;
				}
				IJsonLineInfo current = this._current;
				if (current != null)
				{
					return current.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x0003B5B4 File Offset: 0x000397B4
		public override string Path
		{
			get
			{
				string text = base.Path;
				if (this._initialPath == null)
				{
					this._initialPath = this._root.Path;
				}
				if (!StringUtils.IsNullOrEmpty(this._initialPath))
				{
					if (StringUtils.IsNullOrEmpty(text))
					{
						return this._initialPath;
					}
					if (text.StartsWith('['))
					{
						text = this._initialPath + text;
					}
					else
					{
						text = this._initialPath + "." + text;
					}
				}
				return text;
			}
		}

		// Token: 0x0400073E RID: 1854
		private readonly JToken _root;

		// Token: 0x0400073F RID: 1855
		[Nullable(2)]
		private string _initialPath;

		// Token: 0x04000740 RID: 1856
		[Nullable(2)]
		private JToken _parent;

		// Token: 0x04000741 RID: 1857
		[Nullable(2)]
		private JToken _current;
	}
}
