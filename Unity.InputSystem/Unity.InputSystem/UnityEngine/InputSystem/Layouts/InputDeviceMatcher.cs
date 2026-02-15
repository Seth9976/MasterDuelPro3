using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Layouts
{
	// Token: 0x0200020E RID: 526
	public struct InputDeviceMatcher : IEquatable<InputDeviceMatcher>
	{
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x06001387 RID: 4999 RVA: 0x0005A57E File Offset: 0x0005877E
		public bool empty
		{
			get
			{
				return this.m_Patterns == null;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06001388 RID: 5000 RVA: 0x0005A589 File Offset: 0x00058789
		public IEnumerable<KeyValuePair<string, object>> patterns
		{
			get
			{
				if (this.m_Patterns == null)
				{
					yield break;
				}
				int count = this.m_Patterns.Length;
				int num;
				for (int i = 0; i < count; i = num)
				{
					yield return new KeyValuePair<string, object>(this.m_Patterns[i].Key.ToString(), this.m_Patterns[i].Value);
					num = i + 1;
				}
				yield break;
			}
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0005A59E File Offset: 0x0005879E
		public InputDeviceMatcher WithInterface(string pattern, bool supportRegex = true)
		{
			return this.With(InputDeviceMatcher.kInterfaceKey, pattern, supportRegex);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0005A5AD File Offset: 0x000587AD
		public InputDeviceMatcher WithDeviceClass(string pattern, bool supportRegex = true)
		{
			return this.With(InputDeviceMatcher.kDeviceClassKey, pattern, supportRegex);
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0005A5BC File Offset: 0x000587BC
		public InputDeviceMatcher WithManufacturer(string pattern, bool supportRegex = true)
		{
			return this.With(InputDeviceMatcher.kManufacturerKey, pattern, supportRegex);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0005A5CB File Offset: 0x000587CB
		public InputDeviceMatcher WithManufacturerContains(string noRegExPattern)
		{
			return this.With(InputDeviceMatcher.kManufacturerContainsKey, noRegExPattern, false);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0005A5DA File Offset: 0x000587DA
		public InputDeviceMatcher WithProduct(string pattern, bool supportRegex = true)
		{
			return this.With(InputDeviceMatcher.kProductKey, pattern, supportRegex);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0005A5E9 File Offset: 0x000587E9
		public InputDeviceMatcher WithVersion(string pattern, bool supportRegex = true)
		{
			return this.With(InputDeviceMatcher.kVersionKey, pattern, supportRegex);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0005A5F8 File Offset: 0x000587F8
		public InputDeviceMatcher WithCapability<TValue>(string path, TValue value)
		{
			return this.With(new InternedString(path), value, true);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0005A610 File Offset: 0x00058810
		private InputDeviceMatcher With(InternedString key, object value, bool supportRegex = true)
		{
			if (supportRegex)
			{
				string str = value as string;
				if (str != null)
				{
					double num;
					if (!str.All((char ch) => char.IsLetterOrDigit(ch) || char.IsWhiteSpace(ch)) && !double.TryParse(str, out num))
					{
						value = new Regex(str, RegexOptions.IgnoreCase);
					}
				}
			}
			InputDeviceMatcher result = this;
			ArrayHelpers.Append<KeyValuePair<InternedString, object>>(ref result.m_Patterns, new KeyValuePair<InternedString, object>(key, value));
			return result;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0005A688 File Offset: 0x00058888
		public float MatchPercentage(InputDeviceDescription deviceDescription)
		{
			if (this.empty)
			{
				return 0f;
			}
			int numPatterns = this.m_Patterns.Length;
			for (int i = 0; i < numPatterns; i++)
			{
				InternedString key = this.m_Patterns[i].Key;
				object pattern = this.m_Patterns[i].Value;
				if (key == InputDeviceMatcher.kInterfaceKey)
				{
					if (string.IsNullOrEmpty(deviceDescription.interfaceName) || !InputDeviceMatcher.MatchSingleProperty(pattern, deviceDescription.interfaceName))
					{
						return 0f;
					}
				}
				else if (key == InputDeviceMatcher.kDeviceClassKey)
				{
					if (string.IsNullOrEmpty(deviceDescription.deviceClass) || !InputDeviceMatcher.MatchSingleProperty(pattern, deviceDescription.deviceClass))
					{
						return 0f;
					}
				}
				else if (key == InputDeviceMatcher.kManufacturerKey)
				{
					if (string.IsNullOrEmpty(deviceDescription.manufacturer) || !InputDeviceMatcher.MatchSingleProperty(pattern, deviceDescription.manufacturer))
					{
						return 0f;
					}
				}
				else if (key == InputDeviceMatcher.kManufacturerContainsKey)
				{
					if (string.IsNullOrEmpty(deviceDescription.manufacturer) || !InputDeviceMatcher.MatchSinglePropertyContains(pattern, deviceDescription.manufacturer))
					{
						return 0f;
					}
				}
				else if (key == InputDeviceMatcher.kProductKey)
				{
					if (string.IsNullOrEmpty(deviceDescription.product) || !InputDeviceMatcher.MatchSingleProperty(pattern, deviceDescription.product))
					{
						return 0f;
					}
				}
				else if (key == InputDeviceMatcher.kVersionKey)
				{
					if (string.IsNullOrEmpty(deviceDescription.version) || !InputDeviceMatcher.MatchSingleProperty(pattern, deviceDescription.version))
					{
						return 0f;
					}
				}
				else
				{
					if (string.IsNullOrEmpty(deviceDescription.capabilities))
					{
						return 0f;
					}
					JsonParser graph = new JsonParser(deviceDescription.capabilities);
					if (!graph.NavigateToProperty(key.ToString()) || !graph.CurrentPropertyHasValueEqualTo(new JsonParser.JsonValue
					{
						type = JsonParser.JsonValueType.Any,
						anyValue = pattern
					}))
					{
						return 0f;
					}
				}
			}
			int propertyCountInDescription = InputDeviceMatcher.GetNumPropertiesIn(deviceDescription);
			float scorePerProperty = 1f / (float)propertyCountInDescription;
			return (float)numPatterns * scorePerProperty;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0005A898 File Offset: 0x00058A98
		private static bool MatchSingleProperty(object pattern, string value)
		{
			string str = pattern as string;
			if (str != null)
			{
				return string.Compare(str, value, StringComparison.OrdinalIgnoreCase) == 0;
			}
			Regex regex = pattern as Regex;
			return regex != null && regex.IsMatch(value);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0005A8D0 File Offset: 0x00058AD0
		private static bool MatchSinglePropertyContains(object pattern, string value)
		{
			string str = pattern as string;
			return str != null && value.Contains(str, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0005A8F4 File Offset: 0x00058AF4
		private static int GetNumPropertiesIn(InputDeviceDescription description)
		{
			int count = 0;
			if (!string.IsNullOrEmpty(description.interfaceName))
			{
				count++;
			}
			if (!string.IsNullOrEmpty(description.deviceClass))
			{
				count++;
			}
			if (!string.IsNullOrEmpty(description.manufacturer))
			{
				count++;
			}
			if (!string.IsNullOrEmpty(description.product))
			{
				count++;
			}
			if (!string.IsNullOrEmpty(description.version))
			{
				count++;
			}
			if (!string.IsNullOrEmpty(description.capabilities))
			{
				count++;
			}
			return count;
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0005A970 File Offset: 0x00058B70
		public static InputDeviceMatcher FromDeviceDescription(InputDeviceDescription deviceDescription)
		{
			InputDeviceMatcher matcher = default(InputDeviceMatcher);
			if (!string.IsNullOrEmpty(deviceDescription.interfaceName))
			{
				matcher = matcher.WithInterface(deviceDescription.interfaceName, false);
			}
			if (!string.IsNullOrEmpty(deviceDescription.deviceClass))
			{
				matcher = matcher.WithDeviceClass(deviceDescription.deviceClass, false);
			}
			if (!string.IsNullOrEmpty(deviceDescription.manufacturer))
			{
				matcher = matcher.WithManufacturer(deviceDescription.manufacturer, false);
			}
			if (!string.IsNullOrEmpty(deviceDescription.product))
			{
				matcher = matcher.WithProduct(deviceDescription.product, false);
			}
			if (!string.IsNullOrEmpty(deviceDescription.version))
			{
				matcher = matcher.WithVersion(deviceDescription.version, false);
			}
			return matcher;
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0005AA1C File Offset: 0x00058C1C
		public override string ToString()
		{
			if (this.empty)
			{
				return "<empty>";
			}
			string result = string.Empty;
			foreach (KeyValuePair<InternedString, object> pattern in this.m_Patterns)
			{
				if (result.Length > 0)
				{
					result += string.Format(",{0}={1}", pattern.Key, pattern.Value);
				}
				else
				{
					result += string.Format("{0}={1}", pattern.Key, pattern.Value);
				}
			}
			return result;
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0005AAB0 File Offset: 0x00058CB0
		public bool Equals(InputDeviceMatcher other)
		{
			if (this.m_Patterns == other.m_Patterns)
			{
				return true;
			}
			if (this.m_Patterns == null || other.m_Patterns == null)
			{
				return false;
			}
			if (this.m_Patterns.Length != other.m_Patterns.Length)
			{
				return false;
			}
			for (int i = 0; i < this.m_Patterns.Length; i++)
			{
				KeyValuePair<InternedString, object> thisPattern = this.m_Patterns[i];
				bool foundPattern = false;
				int j = 0;
				while (j < this.m_Patterns.Length)
				{
					KeyValuePair<InternedString, object> otherPattern = other.m_Patterns[j];
					if (!(thisPattern.Key != otherPattern.Key))
					{
						if (!thisPattern.Value.Equals(otherPattern.Value))
						{
							return false;
						}
						foundPattern = true;
						break;
					}
					else
					{
						j++;
					}
				}
				if (!foundPattern)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0005AB6C File Offset: 0x00058D6C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InputDeviceMatcher)
			{
				InputDeviceMatcher matcher = (InputDeviceMatcher)obj;
				return this.Equals(matcher);
			}
			return false;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0005AB96 File Offset: 0x00058D96
		public static bool operator ==(InputDeviceMatcher left, InputDeviceMatcher right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0005ABA0 File Offset: 0x00058DA0
		public static bool operator !=(InputDeviceMatcher left, InputDeviceMatcher right)
		{
			return !(left == right);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0005ABAC File Offset: 0x00058DAC
		public override int GetHashCode()
		{
			if (this.m_Patterns == null)
			{
				return 0;
			}
			return this.m_Patterns.GetHashCode();
		}

		// Token: 0x04000BA8 RID: 2984
		private KeyValuePair<InternedString, object>[] m_Patterns;

		// Token: 0x04000BA9 RID: 2985
		private static readonly InternedString kInterfaceKey = new InternedString("interface");

		// Token: 0x04000BAA RID: 2986
		private static readonly InternedString kDeviceClassKey = new InternedString("deviceClass");

		// Token: 0x04000BAB RID: 2987
		private static readonly InternedString kManufacturerKey = new InternedString("manufacturer");

		// Token: 0x04000BAC RID: 2988
		private static readonly InternedString kManufacturerContainsKey = new InternedString("manufacturerContains");

		// Token: 0x04000BAD RID: 2989
		private static readonly InternedString kProductKey = new InternedString("product");

		// Token: 0x04000BAE RID: 2990
		private static readonly InternedString kVersionKey = new InternedString("version");

		// Token: 0x0200020F RID: 527
		[Serializable]
		internal struct MatcherJson
		{
			// Token: 0x0600139D RID: 5021 RVA: 0x0005AC2C File Offset: 0x00058E2C
			public static InputDeviceMatcher.MatcherJson FromMatcher(InputDeviceMatcher matcher)
			{
				if (matcher.empty)
				{
					return default(InputDeviceMatcher.MatcherJson);
				}
				InputDeviceMatcher.MatcherJson json = default(InputDeviceMatcher.MatcherJson);
				foreach (KeyValuePair<InternedString, object> pattern in matcher.m_Patterns)
				{
					InternedString key = pattern.Key;
					string value = pattern.Value.ToString();
					if (key == InputDeviceMatcher.kInterfaceKey)
					{
						if (json.@interface == null)
						{
							json.@interface = value;
						}
						else
						{
							ArrayHelpers.Append<string>(ref json.interfaces, value);
						}
					}
					else if (key == InputDeviceMatcher.kDeviceClassKey)
					{
						if (json.deviceClass == null)
						{
							json.deviceClass = value;
						}
						else
						{
							ArrayHelpers.Append<string>(ref json.deviceClasses, value);
						}
					}
					else if (key == InputDeviceMatcher.kManufacturerKey)
					{
						if (json.manufacturer == null)
						{
							json.manufacturer = value;
						}
						else
						{
							ArrayHelpers.Append<string>(ref json.manufacturers, value);
						}
					}
					else if (key == InputDeviceMatcher.kProductKey)
					{
						if (json.product == null)
						{
							json.product = value;
						}
						else
						{
							ArrayHelpers.Append<string>(ref json.products, value);
						}
					}
					else if (key == InputDeviceMatcher.kVersionKey)
					{
						if (json.version == null)
						{
							json.version = value;
						}
						else
						{
							ArrayHelpers.Append<string>(ref json.versions, value);
						}
					}
					else
					{
						ArrayHelpers.Append<InputDeviceMatcher.MatcherJson.Capability>(ref json.capabilities, new InputDeviceMatcher.MatcherJson.Capability
						{
							path = key,
							value = value
						});
					}
				}
				return json;
			}

			// Token: 0x0600139E RID: 5022 RVA: 0x0005ADCC File Offset: 0x00058FCC
			public InputDeviceMatcher ToMatcher()
			{
				InputDeviceMatcher matcher = default(InputDeviceMatcher);
				if (!string.IsNullOrEmpty(this.@interface))
				{
					matcher = matcher.WithInterface(this.@interface, true);
				}
				if (this.interfaces != null)
				{
					foreach (string value in this.interfaces)
					{
						matcher = matcher.WithInterface(value, true);
					}
				}
				if (!string.IsNullOrEmpty(this.deviceClass))
				{
					matcher = matcher.WithDeviceClass(this.deviceClass, true);
				}
				if (this.deviceClasses != null)
				{
					foreach (string value2 in this.deviceClasses)
					{
						matcher = matcher.WithDeviceClass(value2, true);
					}
				}
				if (!string.IsNullOrEmpty(this.manufacturer))
				{
					matcher = matcher.WithManufacturer(this.manufacturer, true);
				}
				if (this.manufacturers != null)
				{
					foreach (string value3 in this.manufacturers)
					{
						matcher = matcher.WithManufacturer(value3, true);
					}
				}
				if (!string.IsNullOrEmpty(this.manufacturerContains))
				{
					matcher = matcher.WithManufacturerContains(this.manufacturerContains);
				}
				if (!string.IsNullOrEmpty(this.product))
				{
					matcher = matcher.WithProduct(this.product, true);
				}
				if (this.products != null)
				{
					foreach (string value4 in this.products)
					{
						matcher = matcher.WithProduct(value4, true);
					}
				}
				if (!string.IsNullOrEmpty(this.version))
				{
					matcher = matcher.WithVersion(this.version, true);
				}
				if (this.versions != null)
				{
					foreach (string value5 in this.versions)
					{
						matcher = matcher.WithVersion(value5, true);
					}
				}
				if (this.capabilities != null)
				{
					foreach (InputDeviceMatcher.MatcherJson.Capability value6 in this.capabilities)
					{
						matcher = matcher.WithCapability<string>(value6.path, value6.value);
					}
				}
				return matcher;
			}

			// Token: 0x04000BAF RID: 2991
			public string @interface;

			// Token: 0x04000BB0 RID: 2992
			public string[] interfaces;

			// Token: 0x04000BB1 RID: 2993
			public string deviceClass;

			// Token: 0x04000BB2 RID: 2994
			public string[] deviceClasses;

			// Token: 0x04000BB3 RID: 2995
			public string manufacturer;

			// Token: 0x04000BB4 RID: 2996
			public string manufacturerContains;

			// Token: 0x04000BB5 RID: 2997
			public string[] manufacturers;

			// Token: 0x04000BB6 RID: 2998
			public string product;

			// Token: 0x04000BB7 RID: 2999
			public string[] products;

			// Token: 0x04000BB8 RID: 3000
			public string version;

			// Token: 0x04000BB9 RID: 3001
			public string[] versions;

			// Token: 0x04000BBA RID: 3002
			public InputDeviceMatcher.MatcherJson.Capability[] capabilities;

			// Token: 0x02000210 RID: 528
			public struct Capability
			{
				// Token: 0x04000BBB RID: 3003
				public string path;

				// Token: 0x04000BBC RID: 3004
				public string value;
			}
		}
	}
}
