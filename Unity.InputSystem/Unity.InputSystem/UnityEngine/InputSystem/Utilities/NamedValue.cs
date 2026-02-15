using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200024C RID: 588
	public struct NamedValue : IEquatable<NamedValue>
	{
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x00061FD4 File Offset: 0x000601D4
		// (set) Token: 0x06001573 RID: 5491 RVA: 0x00061FDC File Offset: 0x000601DC
		public string name { readonly get; set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x00061FE5 File Offset: 0x000601E5
		// (set) Token: 0x06001575 RID: 5493 RVA: 0x00061FED File Offset: 0x000601ED
		public PrimitiveValue value { readonly get; set; }

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x00061FF8 File Offset: 0x000601F8
		public TypeCode type
		{
			get
			{
				return this.value.type;
			}
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x00062014 File Offset: 0x00060214
		public NamedValue ConvertTo(TypeCode type)
		{
			return new NamedValue
			{
				name = this.name,
				value = this.value.ConvertTo(type)
			};
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x00062050 File Offset: 0x00060250
		public static NamedValue From<TValue>(string name, TValue value) where TValue : struct
		{
			return new NamedValue
			{
				name = name,
				value = PrimitiveValue.From<TValue>(value)
			};
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0006207B File Offset: 0x0006027B
		public override string ToString()
		{
			return string.Format("{0}={1}", this.name, this.value);
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x00062098 File Offset: 0x00060298
		public bool Equals(NamedValue other)
		{
			return string.Equals(this.name, other.name, StringComparison.InvariantCultureIgnoreCase) && this.value == other.value;
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x000620C4 File Offset: 0x000602C4
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is NamedValue)
			{
				NamedValue parameterValue = (NamedValue)obj;
				return this.Equals(parameterValue);
			}
			return false;
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x000620F0 File Offset: 0x000602F0
		public override int GetHashCode()
		{
			return (((this.name != null) ? this.name.GetHashCode() : 0) * 397) ^ this.value.GetHashCode();
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0006212E File Offset: 0x0006032E
		public static bool operator ==(NamedValue left, NamedValue right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x00062138 File Offset: 0x00060338
		public static bool operator !=(NamedValue left, NamedValue right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00062148 File Offset: 0x00060348
		public static NamedValue[] ParseMultiple(string parameterString)
		{
			if (parameterString == null)
			{
				throw new ArgumentNullException("parameterString");
			}
			parameterString = parameterString.Trim();
			if (string.IsNullOrEmpty(parameterString))
			{
				return null;
			}
			int parameterCount = parameterString.CountOccurrences(","[0]) + 1;
			NamedValue[] parameters = new NamedValue[parameterCount];
			int index = 0;
			for (int i = 0; i < parameterCount; i++)
			{
				NamedValue parameter = NamedValue.ParseParameter(parameterString, ref index);
				parameters[i] = parameter;
			}
			return parameters;
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x000621B4 File Offset: 0x000603B4
		public static NamedValue Parse(string str)
		{
			int index = 0;
			return NamedValue.ParseParameter(str, ref index);
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x000621CC File Offset: 0x000603CC
		private static NamedValue ParseParameter(string parameterString, ref int index)
		{
			NamedValue parameter = default(NamedValue);
			int parameterStringLength = parameterString.Length;
			while (index < parameterStringLength && char.IsWhiteSpace(parameterString[index]))
			{
				index++;
			}
			int nameStart = index;
			while (index < parameterStringLength)
			{
				char nextChar = parameterString[index];
				if (nextChar == '=' || nextChar == ","[0] || char.IsWhiteSpace(nextChar))
				{
					break;
				}
				index++;
			}
			parameter.name = parameterString.Substring(nameStart, index - nameStart);
			while (index < parameterStringLength && char.IsWhiteSpace(parameterString[index]))
			{
				index++;
			}
			if (index == parameterStringLength || parameterString[index] != '=')
			{
				parameter.value = true;
			}
			else
			{
				index++;
				while (index < parameterStringLength && char.IsWhiteSpace(parameterString[index]))
				{
					index++;
				}
				int valueStart = index;
				while (index < parameterStringLength && parameterString[index] != ","[0] && !char.IsWhiteSpace(parameterString[index]))
				{
					index++;
				}
				string value = parameterString.Substring(valueStart, index - valueStart);
				parameter.value = PrimitiveValue.FromString(value);
			}
			if (index < parameterStringLength && parameterString[index] == ","[0])
			{
				index++;
			}
			return parameter;
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0006231C File Offset: 0x0006051C
		public void ApplyToObject(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			Type instanceType = instance.GetType();
			FieldInfo field = instanceType.GetField(this.name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field == null)
			{
				throw new ArgumentException(string.Concat(new string[] { "Cannot find public field '", this.name, "' in '", instanceType.Name, "' (while trying to apply parameter)" }), "instance");
			}
			TypeCode fieldTypeCode = Type.GetTypeCode(field.FieldType);
			field.SetValue(instance, this.value.ConvertTo(fieldTypeCode).ToObject());
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x000623C0 File Offset: 0x000605C0
		public static void ApplyAllToObject<TParameterList>(object instance, TParameterList parameters) where TParameterList : IEnumerable<NamedValue>
		{
			foreach (NamedValue parameter in parameters)
			{
				parameter.ApplyToObject(instance);
			}
		}

		// Token: 0x04000C7A RID: 3194
		public const string Separator = ",";
	}
}
