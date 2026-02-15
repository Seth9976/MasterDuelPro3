using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000241 RID: 577
	internal struct JsonParser
	{
		// Token: 0x06001513 RID: 5395 RVA: 0x0005FB05 File Offset: 0x0005DD05
		public JsonParser(string json)
		{
			this = default(JsonParser);
			if (json == null)
			{
				throw new ArgumentNullException("json");
			}
			this.m_Text = json;
			this.m_Length = json.Length;
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x0005FB2F File Offset: 0x0005DD2F
		public void Reset()
		{
			this.m_Position = 0;
			this.m_MatchAnyElementInArray = false;
			this.m_DryRun = false;
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x0005FB48 File Offset: 0x0005DD48
		public override string ToString()
		{
			if (this.m_Text != null)
			{
				return string.Format("{0}: {1}", this.m_Position, this.m_Text.Substring(this.m_Position));
			}
			return base.ToString();
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0005FB94 File Offset: 0x0005DD94
		public bool NavigateToProperty(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			int pathLength = path.Length;
			int pathPosition = 0;
			this.m_DryRun = true;
			if (!this.ParseToken('{'))
			{
				return false;
			}
			while (this.m_Position < this.m_Length && pathPosition < pathLength)
			{
				this.SkipWhitespace();
				if (this.m_Position == this.m_Length)
				{
					return false;
				}
				if (this.m_Text[this.m_Position] != '"')
				{
					return false;
				}
				this.m_Position++;
				int pathStartPosition = pathPosition;
				while (pathPosition < pathLength)
				{
					char ch = path[pathPosition];
					if (ch == '/' || ch == '[' || this.m_Text[this.m_Position] != ch)
					{
						break;
					}
					this.m_Position++;
					pathPosition++;
				}
				if (this.m_Position < this.m_Length && this.m_Text[this.m_Position] == '"' && (pathPosition >= pathLength || path[pathPosition] == '/' || path[pathPosition] == '['))
				{
					this.m_Position++;
					if (!this.SkipToValue())
					{
						return false;
					}
					if (pathPosition >= pathLength)
					{
						return true;
					}
					if (path[pathPosition] == '/')
					{
						pathPosition++;
						if (!this.ParseToken('{'))
						{
							return false;
						}
					}
					else if (path[pathPosition] == '[')
					{
						pathPosition++;
						if (pathPosition == pathLength)
						{
							throw new ArgumentException("Malformed JSON property path: " + path, "path");
						}
						if (path[pathPosition] != ']')
						{
							throw new NotImplementedException("Navigating to specific array element");
						}
						this.m_MatchAnyElementInArray = true;
						pathPosition++;
						if (pathPosition == pathLength)
						{
							return true;
						}
					}
				}
				else
				{
					pathPosition = pathStartPosition;
					while (this.m_Position < this.m_Length && this.m_Text[this.m_Position] != '"')
					{
						this.m_Position++;
					}
					if (this.m_Position == this.m_Length || this.m_Text[this.m_Position] != '"')
					{
						return false;
					}
					this.m_Position++;
					if (!this.SkipToValue() || !this.ParseValue())
					{
						return false;
					}
					this.SkipWhitespace();
					if (this.m_Position == this.m_Length || this.m_Text[this.m_Position] == '}' || this.m_Text[this.m_Position] != ',')
					{
						return false;
					}
					this.m_Position++;
				}
			}
			return false;
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0005FE04 File Offset: 0x0005E004
		public bool CurrentPropertyHasValueEqualTo(JsonParser.JsonValue expectedValue)
		{
			int savedPosition = this.m_Position;
			this.m_DryRun = false;
			JsonParser.JsonValue propertyValue;
			if (!this.ParseValue(out propertyValue))
			{
				this.m_Position = savedPosition;
				return false;
			}
			this.m_Position = savedPosition;
			bool isMatch = false;
			if (propertyValue.type == JsonParser.JsonValueType.Array && this.m_MatchAnyElementInArray)
			{
				List<JsonParser.JsonValue> array = propertyValue.arrayValue;
				int i = 0;
				while (!isMatch)
				{
					if (i >= array.Count)
					{
						break;
					}
					isMatch = array[i] == expectedValue;
					i++;
				}
			}
			else
			{
				isMatch = propertyValue == expectedValue;
			}
			return isMatch;
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x0005FE88 File Offset: 0x0005E088
		public bool ParseToken(char token)
		{
			this.SkipWhitespace();
			if (this.m_Position == this.m_Length)
			{
				return false;
			}
			if (this.m_Text[this.m_Position] != token)
			{
				return false;
			}
			this.m_Position++;
			this.SkipWhitespace();
			return this.m_Position < this.m_Length;
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0005FEE4 File Offset: 0x0005E0E4
		public bool ParseValue()
		{
			JsonParser.JsonValue result;
			return this.ParseValue(out result);
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0005FEFC File Offset: 0x0005E0FC
		public bool ParseValue(out JsonParser.JsonValue result)
		{
			result = default(JsonParser.JsonValue);
			this.SkipWhitespace();
			if (this.m_Position == this.m_Length)
			{
				return false;
			}
			char ch = this.m_Text[this.m_Position];
			if (ch <= 'f')
			{
				if (ch != '"')
				{
					if (ch != '[')
					{
						if (ch != 'f')
						{
							goto IL_008D;
						}
					}
					else
					{
						if (this.ParseArrayValue(out result))
						{
							return true;
						}
						return false;
					}
				}
				else
				{
					if (this.ParseStringValue(out result))
					{
						return true;
					}
					return false;
				}
			}
			else if (ch != 'n')
			{
				if (ch != 't')
				{
					if (ch != '{')
					{
						goto IL_008D;
					}
					if (this.ParseObjectValue(out result))
					{
						return true;
					}
					return false;
				}
			}
			else
			{
				if (this.ParseNullValue(out result))
				{
					return true;
				}
				return false;
			}
			if (this.ParseBooleanValue(out result))
			{
				return true;
			}
			return false;
			IL_008D:
			if (this.ParseNumber(out result))
			{
				return true;
			}
			return false;
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x0005FFA4 File Offset: 0x0005E1A4
		public bool ParseStringValue(out JsonParser.JsonValue result)
		{
			result = default(JsonParser.JsonValue);
			this.SkipWhitespace();
			if (this.m_Position == this.m_Length || this.m_Text[this.m_Position] != '"')
			{
				return false;
			}
			this.m_Position++;
			int startIndex = this.m_Position;
			bool hasEscapes = false;
			while (this.m_Position < this.m_Length)
			{
				char ch = this.m_Text[this.m_Position];
				if (ch == '\\')
				{
					this.m_Position++;
					if (this.m_Position == this.m_Length)
					{
						break;
					}
					hasEscapes = true;
				}
				else if (ch == '"')
				{
					this.m_Position++;
					result = new JsonParser.JsonString
					{
						text = new Substring(this.m_Text, startIndex, this.m_Position - startIndex - 1),
						hasEscapes = hasEscapes
					};
					return true;
				}
				this.m_Position++;
			}
			return false;
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x000600A4 File Offset: 0x0005E2A4
		public bool ParseArrayValue(out JsonParser.JsonValue result)
		{
			result = default(JsonParser.JsonValue);
			this.SkipWhitespace();
			if (this.m_Position == this.m_Length || this.m_Text[this.m_Position] != '[')
			{
				return false;
			}
			this.m_Position++;
			if (this.m_Position == this.m_Length)
			{
				return false;
			}
			if (this.m_Text[this.m_Position] == ']')
			{
				result = new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.Array
				};
				this.m_Position++;
				return true;
			}
			List<JsonParser.JsonValue> values = null;
			if (!this.m_DryRun)
			{
				values = new List<JsonParser.JsonValue>();
			}
			while (this.m_Position < this.m_Length)
			{
				JsonParser.JsonValue value;
				if (!this.ParseValue(out value))
				{
					return false;
				}
				if (!this.m_DryRun)
				{
					values.Add(value);
				}
				this.SkipWhitespace();
				if (this.m_Position == this.m_Length)
				{
					return false;
				}
				char ch = this.m_Text[this.m_Position];
				if (ch == ']')
				{
					this.m_Position++;
					if (!this.m_DryRun)
					{
						result = values;
					}
					return true;
				}
				if (ch == ',')
				{
					this.m_Position++;
				}
			}
			return false;
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x000601E4 File Offset: 0x0005E3E4
		public bool ParseObjectValue(out JsonParser.JsonValue result)
		{
			result = default(JsonParser.JsonValue);
			if (!this.ParseToken('{'))
			{
				return false;
			}
			if (this.m_Position < this.m_Length && this.m_Text[this.m_Position] == '}')
			{
				result = new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.Object
				};
				this.m_Position++;
				return true;
			}
			while (this.m_Position < this.m_Length)
			{
				JsonParser.JsonValue propertyName;
				if (!this.ParseStringValue(out propertyName))
				{
					return false;
				}
				if (!this.SkipToValue())
				{
					return false;
				}
				JsonParser.JsonValue propertyValue;
				if (!this.ParseValue(out propertyValue))
				{
					return false;
				}
				if (!this.m_DryRun)
				{
					throw new NotImplementedException();
				}
				this.SkipWhitespace();
				if (this.m_Position < this.m_Length && this.m_Text[this.m_Position] == '}')
				{
					if (!this.m_DryRun)
					{
						throw new NotImplementedException();
					}
					this.m_Position++;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x000602E0 File Offset: 0x0005E4E0
		public bool ParseNumber(out JsonParser.JsonValue result)
		{
			result = default(JsonParser.JsonValue);
			this.SkipWhitespace();
			if (this.m_Position == this.m_Length)
			{
				return false;
			}
			bool negative = false;
			bool haveFractionalPart = false;
			long integralPart = 0L;
			double fractionalPart = 0.0;
			double fractionalDivisor = 10.0;
			int exponent = 0;
			if (this.m_Text[this.m_Position] == '-')
			{
				negative = true;
				this.m_Position++;
			}
			if (this.m_Position == this.m_Length || !char.IsDigit(this.m_Text[this.m_Position]))
			{
				return false;
			}
			while (this.m_Position < this.m_Length)
			{
				char ch = this.m_Text[this.m_Position];
				if (ch == '.' || ch < '0' || ch > '9')
				{
					break;
				}
				integralPart = integralPart * 10L + (long)((ulong)ch) - 48L;
				this.m_Position++;
			}
			if (this.m_Position < this.m_Length && this.m_Text[this.m_Position] == '.')
			{
				haveFractionalPart = true;
				this.m_Position++;
				if (this.m_Position == this.m_Length || !char.IsDigit(this.m_Text[this.m_Position]))
				{
					return false;
				}
				while (this.m_Position < this.m_Length)
				{
					char ch2 = this.m_Text[this.m_Position];
					if (ch2 < '0' || ch2 > '9')
					{
						break;
					}
					fractionalPart = (double)(ch2 - '0') / fractionalDivisor + fractionalPart;
					fractionalDivisor *= 10.0;
					this.m_Position++;
				}
			}
			if (this.m_Position < this.m_Length && (this.m_Text[this.m_Position] == 'e' || this.m_Text[this.m_Position] == 'E'))
			{
				this.m_Position++;
				bool isNegative = false;
				if (this.m_Position < this.m_Length && this.m_Text[this.m_Position] == '-')
				{
					isNegative = true;
					this.m_Position++;
				}
				else if (this.m_Position < this.m_Length && this.m_Text[this.m_Position] == '+')
				{
					this.m_Position++;
				}
				int multiplier = 1;
				while (this.m_Position < this.m_Length && char.IsDigit(this.m_Text[this.m_Position]))
				{
					int digit = (int)(this.m_Text[this.m_Position] - '0');
					exponent *= multiplier;
					exponent += digit;
					multiplier *= 10;
					this.m_Position++;
				}
				if (isNegative)
				{
					exponent *= -1;
				}
			}
			if (!this.m_DryRun)
			{
				if (!haveFractionalPart && exponent == 0)
				{
					if (negative)
					{
						result = -integralPart;
					}
					else
					{
						result = integralPart;
					}
				}
				else
				{
					float value;
					if (negative)
					{
						value = (float)(-(float)((double)integralPart + fractionalPart));
					}
					else
					{
						value = (float)((double)integralPart + fractionalPart);
					}
					if (exponent != 0)
					{
						value *= Mathf.Pow(10f, (float)exponent);
					}
					result = (double)value;
				}
			}
			return true;
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x00060608 File Offset: 0x0005E808
		public bool ParseBooleanValue(out JsonParser.JsonValue result)
		{
			this.SkipWhitespace();
			if (this.SkipString("true"))
			{
				result = true;
				return true;
			}
			if (this.SkipString("false"))
			{
				result = false;
				return true;
			}
			result = default(JsonParser.JsonValue);
			return false;
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x00060659 File Offset: 0x0005E859
		public bool ParseNullValue(out JsonParser.JsonValue result)
		{
			result = default(JsonParser.JsonValue);
			return this.SkipString("null");
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00060670 File Offset: 0x0005E870
		public bool SkipToValue()
		{
			this.SkipWhitespace();
			if (this.m_Position == this.m_Length || this.m_Text[this.m_Position] != ':')
			{
				return false;
			}
			this.m_Position++;
			this.SkipWhitespace();
			return true;
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x000606C0 File Offset: 0x0005E8C0
		private bool SkipString(string text)
		{
			this.SkipWhitespace();
			int length = text.Length;
			if (this.m_Position + length >= this.m_Length)
			{
				return false;
			}
			for (int i = 0; i < length; i++)
			{
				if (this.m_Text[this.m_Position + i] != text[i])
				{
					return false;
				}
			}
			this.m_Position += length;
			return true;
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x00060725 File Offset: 0x0005E925
		private void SkipWhitespace()
		{
			while (this.m_Position < this.m_Length && char.IsWhiteSpace(this.m_Text[this.m_Position]))
			{
				this.m_Position++;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x0006075D File Offset: 0x0005E95D
		public bool isAtEnd
		{
			get
			{
				return this.m_Position >= this.m_Length;
			}
		}

		// Token: 0x04000C4E RID: 3150
		private readonly string m_Text;

		// Token: 0x04000C4F RID: 3151
		private readonly int m_Length;

		// Token: 0x04000C50 RID: 3152
		private int m_Position;

		// Token: 0x04000C51 RID: 3153
		private bool m_MatchAnyElementInArray;

		// Token: 0x04000C52 RID: 3154
		private bool m_DryRun;

		// Token: 0x02000242 RID: 578
		public enum JsonValueType
		{
			// Token: 0x04000C54 RID: 3156
			None,
			// Token: 0x04000C55 RID: 3157
			Bool,
			// Token: 0x04000C56 RID: 3158
			Real,
			// Token: 0x04000C57 RID: 3159
			Integer,
			// Token: 0x04000C58 RID: 3160
			String,
			// Token: 0x04000C59 RID: 3161
			Array,
			// Token: 0x04000C5A RID: 3162
			Object,
			// Token: 0x04000C5B RID: 3163
			Any
		}

		// Token: 0x02000243 RID: 579
		public struct JsonString : IEquatable<JsonParser.JsonString>
		{
			// Token: 0x06001525 RID: 5413 RVA: 0x00060770 File Offset: 0x0005E970
			public override string ToString()
			{
				if (!this.hasEscapes)
				{
					return this.text.ToString();
				}
				StringBuilder builder = new StringBuilder();
				int length = this.text.length;
				for (int i = 0; i < length; i++)
				{
					char ch = this.text[i];
					if (ch == '\\')
					{
						i++;
						if (i == length)
						{
							break;
						}
						ch = this.text[i];
					}
					builder.Append(ch);
				}
				return builder.ToString();
			}

			// Token: 0x06001526 RID: 5414 RVA: 0x000607EC File Offset: 0x0005E9EC
			public bool Equals(JsonParser.JsonString other)
			{
				if (this.hasEscapes == other.hasEscapes)
				{
					return Substring.Compare(this.text, other.text, StringComparison.InvariantCultureIgnoreCase) == 0;
				}
				int thisLength = this.text.length;
				int otherLength = other.text.length;
				int thisIndex = 0;
				int otherIndex = 0;
				while (thisIndex < thisLength && otherIndex < otherLength)
				{
					char thisChar = this.text[thisIndex];
					char otherChar = other.text[otherIndex];
					if (thisChar == '\\')
					{
						thisIndex++;
						if (thisIndex == thisLength)
						{
							return false;
						}
						thisChar = this.text[thisIndex];
					}
					if (otherChar == '\\')
					{
						otherIndex++;
						if (otherIndex == otherLength)
						{
							return false;
						}
						otherChar = other.text[otherIndex];
					}
					if (char.ToUpperInvariant(thisChar) != char.ToUpperInvariant(otherChar))
					{
						return false;
					}
					thisIndex++;
					otherIndex++;
				}
				return thisIndex == thisLength && otherIndex == otherLength;
			}

			// Token: 0x06001527 RID: 5415 RVA: 0x000608C4 File Offset: 0x0005EAC4
			public override bool Equals(object obj)
			{
				if (obj is JsonParser.JsonString)
				{
					JsonParser.JsonString other = (JsonParser.JsonString)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x06001528 RID: 5416 RVA: 0x000608E9 File Offset: 0x0005EAE9
			public override int GetHashCode()
			{
				return (this.text.GetHashCode() * 397) ^ this.hasEscapes.GetHashCode();
			}

			// Token: 0x06001529 RID: 5417 RVA: 0x0006090E File Offset: 0x0005EB0E
			public static bool operator ==(JsonParser.JsonString left, JsonParser.JsonString right)
			{
				return left.Equals(right);
			}

			// Token: 0x0600152A RID: 5418 RVA: 0x00060918 File Offset: 0x0005EB18
			public static bool operator !=(JsonParser.JsonString left, JsonParser.JsonString right)
			{
				return !left.Equals(right);
			}

			// Token: 0x0600152B RID: 5419 RVA: 0x00060928 File Offset: 0x0005EB28
			public static implicit operator JsonParser.JsonString(string str)
			{
				return new JsonParser.JsonString
				{
					text = str
				};
			}

			// Token: 0x04000C5C RID: 3164
			public Substring text;

			// Token: 0x04000C5D RID: 3165
			public bool hasEscapes;
		}

		// Token: 0x02000244 RID: 580
		public struct JsonValue : IEquatable<JsonParser.JsonValue>
		{
			// Token: 0x0600152C RID: 5420 RVA: 0x0006094C File Offset: 0x0005EB4C
			public bool ToBoolean()
			{
				switch (this.type)
				{
				case JsonParser.JsonValueType.Bool:
					return this.boolValue;
				case JsonParser.JsonValueType.Real:
					return NumberHelpers.Approximately(0.0, this.realValue);
				case JsonParser.JsonValueType.Integer:
					return this.integerValue != 0L;
				case JsonParser.JsonValueType.String:
					return Convert.ToBoolean(this.ToString());
				default:
					return false;
				}
			}

			// Token: 0x0600152D RID: 5421 RVA: 0x000609B4 File Offset: 0x0005EBB4
			public long ToInteger()
			{
				switch (this.type)
				{
				case JsonParser.JsonValueType.Bool:
					return this.boolValue ? 1L : 0L;
				case JsonParser.JsonValueType.Real:
					return (long)this.realValue;
				case JsonParser.JsonValueType.Integer:
					return this.integerValue;
				case JsonParser.JsonValueType.String:
					return Convert.ToInt64(this.ToString());
				default:
					return 0L;
				}
			}

			// Token: 0x0600152E RID: 5422 RVA: 0x00060A14 File Offset: 0x0005EC14
			public double ToDouble()
			{
				switch (this.type)
				{
				case JsonParser.JsonValueType.Bool:
					return (double)(this.boolValue ? 1 : 0);
				case JsonParser.JsonValueType.Real:
					return this.realValue;
				case JsonParser.JsonValueType.Integer:
					return (double)this.integerValue;
				case JsonParser.JsonValueType.String:
					return (double)Convert.ToSingle(this.ToString());
				default:
					return 0.0;
				}
			}

			// Token: 0x0600152F RID: 5423 RVA: 0x00060A7C File Offset: 0x0005EC7C
			public override string ToString()
			{
				switch (this.type)
				{
				case JsonParser.JsonValueType.None:
					return "null";
				case JsonParser.JsonValueType.Bool:
					return this.boolValue.ToString();
				case JsonParser.JsonValueType.Real:
					return this.realValue.ToString(CultureInfo.InvariantCulture);
				case JsonParser.JsonValueType.Integer:
					return this.integerValue.ToString(CultureInfo.InvariantCulture);
				case JsonParser.JsonValueType.String:
					return this.stringValue.ToString();
				case JsonParser.JsonValueType.Array:
					if (this.arrayValue == null)
					{
						return "[]";
					}
					return "[" + string.Join(",", this.arrayValue.Select((JsonParser.JsonValue x) => x.ToString())) + "]";
				case JsonParser.JsonValueType.Object:
				{
					if (this.objectValue == null)
					{
						return "{}";
					}
					IEnumerable<string> elements = this.objectValue.Select((KeyValuePair<string, JsonParser.JsonValue> pair) => string.Format("\"{0}\" : \"{1}\"", pair.Key, pair.Value));
					return "{" + string.Join(",", elements) + "}";
				}
				case JsonParser.JsonValueType.Any:
					return this.anyValue.ToString();
				default:
					return base.ToString();
				}
			}

			// Token: 0x06001530 RID: 5424 RVA: 0x00060BC4 File Offset: 0x0005EDC4
			public static implicit operator JsonParser.JsonValue(bool val)
			{
				return new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.Bool,
					boolValue = val
				};
			}

			// Token: 0x06001531 RID: 5425 RVA: 0x00060BEC File Offset: 0x0005EDEC
			public static implicit operator JsonParser.JsonValue(long val)
			{
				return new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.Integer,
					integerValue = val
				};
			}

			// Token: 0x06001532 RID: 5426 RVA: 0x00060C14 File Offset: 0x0005EE14
			public static implicit operator JsonParser.JsonValue(double val)
			{
				return new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.Real,
					realValue = val
				};
			}

			// Token: 0x06001533 RID: 5427 RVA: 0x00060C3C File Offset: 0x0005EE3C
			public static implicit operator JsonParser.JsonValue(string str)
			{
				return new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.String,
					stringValue = new JsonParser.JsonString
					{
						text = str
					}
				};
			}

			// Token: 0x06001534 RID: 5428 RVA: 0x00060C78 File Offset: 0x0005EE78
			public static implicit operator JsonParser.JsonValue(JsonParser.JsonString str)
			{
				return new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.String,
					stringValue = str
				};
			}

			// Token: 0x06001535 RID: 5429 RVA: 0x00060CA0 File Offset: 0x0005EEA0
			public static implicit operator JsonParser.JsonValue(List<JsonParser.JsonValue> array)
			{
				return new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.Array,
					arrayValue = array
				};
			}

			// Token: 0x06001536 RID: 5430 RVA: 0x00060CC8 File Offset: 0x0005EEC8
			public static implicit operator JsonParser.JsonValue(Dictionary<string, JsonParser.JsonValue> obj)
			{
				return new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.Object,
					objectValue = obj
				};
			}

			// Token: 0x06001537 RID: 5431 RVA: 0x00060CF0 File Offset: 0x0005EEF0
			public static implicit operator JsonParser.JsonValue(Enum val)
			{
				return new JsonParser.JsonValue
				{
					type = JsonParser.JsonValueType.Any,
					anyValue = val
				};
			}

			// Token: 0x06001538 RID: 5432 RVA: 0x00060D18 File Offset: 0x0005EF18
			public bool Equals(JsonParser.JsonValue other)
			{
				if (this.type == other.type)
				{
					switch (this.type)
					{
					case JsonParser.JsonValueType.None:
						return true;
					case JsonParser.JsonValueType.Bool:
						return this.boolValue == other.boolValue;
					case JsonParser.JsonValueType.Real:
						return NumberHelpers.Approximately(this.realValue, other.realValue);
					case JsonParser.JsonValueType.Integer:
						return this.integerValue == other.integerValue;
					case JsonParser.JsonValueType.String:
						return this.stringValue == other.stringValue;
					case JsonParser.JsonValueType.Array:
						throw new NotImplementedException();
					case JsonParser.JsonValueType.Object:
						throw new NotImplementedException();
					case JsonParser.JsonValueType.Any:
						return this.anyValue.Equals(other.anyValue);
					default:
						return false;
					}
				}
				else
				{
					if (this.anyValue != null)
					{
						return JsonParser.JsonValue.Equals(this.anyValue, other);
					}
					return other.anyValue != null && JsonParser.JsonValue.Equals(other.anyValue, this);
				}
			}

			// Token: 0x06001539 RID: 5433 RVA: 0x00060DFC File Offset: 0x0005EFFC
			private static bool Equals(object obj, JsonParser.JsonValue value)
			{
				if (obj == null)
				{
					return false;
				}
				Regex regex = obj as Regex;
				if (regex != null)
				{
					return regex.IsMatch(value.ToString());
				}
				string str = obj as string;
				if (str != null)
				{
					switch (value.type)
					{
					case JsonParser.JsonValueType.Bool:
						if (value.boolValue)
						{
							return str == "True" || str == "true" || str == "1";
						}
						return str == "False" || str == "false" || str == "0";
					case JsonParser.JsonValueType.Real:
					{
						double sf;
						return double.TryParse(str, out sf) && NumberHelpers.Approximately(sf, value.realValue);
					}
					case JsonParser.JsonValueType.Integer:
					{
						long si;
						return long.TryParse(str, out si) && si == value.integerValue;
					}
					case JsonParser.JsonValueType.String:
						return value.stringValue == str;
					}
				}
				if (obj is float)
				{
					float f = (float)obj;
					if (value.type == JsonParser.JsonValueType.Real)
					{
						return NumberHelpers.Approximately((double)f, value.realValue);
					}
					if (value.type == JsonParser.JsonValueType.String)
					{
						float otherF;
						return float.TryParse(value.ToString(), out otherF) && Mathf.Approximately(f, otherF);
					}
				}
				if (obj is double)
				{
					double d = (double)obj;
					if (value.type == JsonParser.JsonValueType.Real)
					{
						return NumberHelpers.Approximately(d, value.realValue);
					}
					if (value.type == JsonParser.JsonValueType.String)
					{
						double otherD;
						return double.TryParse(value.ToString(), out otherD) && NumberHelpers.Approximately(d, otherD);
					}
				}
				if (obj is int)
				{
					int i = (int)obj;
					if (value.type == JsonParser.JsonValueType.Integer)
					{
						return (long)i == value.integerValue;
					}
					if (value.type == JsonParser.JsonValueType.String)
					{
						int otherI;
						return int.TryParse(value.ToString(), out otherI) && i == otherI;
					}
				}
				if (obj is long)
				{
					long j = (long)obj;
					if (value.type == JsonParser.JsonValueType.Integer)
					{
						return j == value.integerValue;
					}
					if (value.type == JsonParser.JsonValueType.String)
					{
						long otherL;
						return long.TryParse(value.ToString(), out otherL) && j == otherL;
					}
				}
				if (obj is bool)
				{
					bool b = (bool)obj;
					if (value.type == JsonParser.JsonValueType.Bool)
					{
						return b == value.boolValue;
					}
					if (value.type == JsonParser.JsonValueType.String)
					{
						if (b)
						{
							return value.stringValue == "true" || value.stringValue == "True" || value.stringValue == "1";
						}
						return value.stringValue == "false" || value.stringValue == "False" || value.stringValue == "0";
					}
				}
				if (obj is Enum)
				{
					if (value.type == JsonParser.JsonValueType.Integer)
					{
						return Convert.ToInt64(obj) == value.integerValue;
					}
					if (value.type == JsonParser.JsonValueType.String)
					{
						return value.stringValue == Enum.GetName(obj.GetType(), obj);
					}
				}
				return false;
			}

			// Token: 0x0600153A RID: 5434 RVA: 0x0006113C File Offset: 0x0005F33C
			public override bool Equals(object obj)
			{
				if (obj is JsonParser.JsonValue)
				{
					JsonParser.JsonValue other = (JsonParser.JsonValue)obj;
					return this.Equals(other);
				}
				return false;
			}

			// Token: 0x0600153B RID: 5435 RVA: 0x00061164 File Offset: 0x0005F364
			public override int GetHashCode()
			{
				return (int)((((((((((((((this.type * (JsonParser.JsonValueType)397) ^ (JsonParser.JsonValueType)this.boolValue.GetHashCode()) * (JsonParser.JsonValueType)397) ^ (JsonParser.JsonValueType)this.realValue.GetHashCode()) * (JsonParser.JsonValueType)397) ^ (JsonParser.JsonValueType)this.integerValue.GetHashCode()) * (JsonParser.JsonValueType)397) ^ (JsonParser.JsonValueType)this.stringValue.GetHashCode()) * (JsonParser.JsonValueType)397) ^ (JsonParser.JsonValueType)((this.arrayValue != null) ? this.arrayValue.GetHashCode() : 0)) * (JsonParser.JsonValueType)397) ^ (JsonParser.JsonValueType)((this.objectValue != null) ? this.objectValue.GetHashCode() : 0)) * (JsonParser.JsonValueType)397) ^ (JsonParser.JsonValueType)((this.anyValue != null) ? this.anyValue.GetHashCode() : 0));
			}

			// Token: 0x0600153C RID: 5436 RVA: 0x0006121C File Offset: 0x0005F41C
			public static bool operator ==(JsonParser.JsonValue left, JsonParser.JsonValue right)
			{
				return left.Equals(right);
			}

			// Token: 0x0600153D RID: 5437 RVA: 0x00061226 File Offset: 0x0005F426
			public static bool operator !=(JsonParser.JsonValue left, JsonParser.JsonValue right)
			{
				return !left.Equals(right);
			}

			// Token: 0x04000C5E RID: 3166
			public JsonParser.JsonValueType type;

			// Token: 0x04000C5F RID: 3167
			public bool boolValue;

			// Token: 0x04000C60 RID: 3168
			public double realValue;

			// Token: 0x04000C61 RID: 3169
			public long integerValue;

			// Token: 0x04000C62 RID: 3170
			public JsonParser.JsonString stringValue;

			// Token: 0x04000C63 RID: 3171
			public List<JsonParser.JsonValue> arrayValue;

			// Token: 0x04000C64 RID: 3172
			public Dictionary<string, JsonParser.JsonValue> objectValue;

			// Token: 0x04000C65 RID: 3173
			public object anyValue;
		}
	}
}
