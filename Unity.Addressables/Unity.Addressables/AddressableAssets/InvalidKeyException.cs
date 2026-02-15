using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace UnityEngine.AddressableAssets
{
	// Token: 0x0200000A RID: 10
	public class InvalidKeyException : Exception
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002529 File Offset: 0x00000729
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002531 File Offset: 0x00000731
		public object Key { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000253A File Offset: 0x0000073A
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00002542 File Offset: 0x00000742
		public Type Type { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000254B File Offset: 0x0000074B
		public Addressables.MergeMode? MergeMode { get; }

		// Token: 0x06000026 RID: 38 RVA: 0x00002553 File Offset: 0x00000753
		public InvalidKeyException(object key)
			: this(key, typeof(object))
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002566 File Offset: 0x00000766
		public InvalidKeyException(object key, Type type)
		{
			this.Key = key;
			this.Type = type;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000257C File Offset: 0x0000077C
		internal InvalidKeyException(object key, Type type, AddressablesImpl addr)
		{
			this.Key = key;
			this.Type = type;
			this.m_Addressables = addr;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002599 File Offset: 0x00000799
		public InvalidKeyException(object key, Type type, Addressables.MergeMode mergeMode)
		{
			this.Key = key;
			this.Type = type;
			this.MergeMode = new Addressables.MergeMode?(mergeMode);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025BB File Offset: 0x000007BB
		internal InvalidKeyException(object key, Type type, Addressables.MergeMode mergeMode, AddressablesImpl addr)
		{
			this.Key = key;
			this.Type = type;
			this.MergeMode = new Addressables.MergeMode?(mergeMode);
			this.m_Addressables = addr;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000025E5 File Offset: 0x000007E5
		public InvalidKeyException()
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025ED File Offset: 0x000007ED
		public InvalidKeyException(string message)
			: base(message)
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000025F6 File Offset: 0x000007F6
		public InvalidKeyException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002600 File Offset: 0x00000800
		protected InvalidKeyException(SerializationInfo message, StreamingContext context)
			: base(message, context)
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000260C File Offset: 0x0000080C
		internal string FormatMessage(InvalidKeyException.Format format, string foundWithTypeString = null)
		{
			switch (format)
			{
			case InvalidKeyException.Format.StandardMessage:
				return string.Format("{0}, Key={1}, Type={2}", base.Message, this.Key.ToString(), this.Type.FullName);
			case InvalidKeyException.Format.MultipleTypesRequested:
			{
				IEnumerable enumerable = this.Key as IEnumerable;
				string types = null;
				foreach (object o in enumerable)
				{
					if (types == null)
					{
						types = o.ToString();
					}
					else
					{
						types = types + ", " + o.ToString();
					}
				}
				return string.Format("{0} Enumerable key contains multiple Types. {1}, all Keys are expected to be strings", base.Message, types);
			}
			case InvalidKeyException.Format.NoLocation:
				return string.Format("{0} No Location found for Key={1}", base.Message, this.Key.ToString());
			case InvalidKeyException.Format.TypeMismatch:
				return string.Format("{0} No Asset found for Key={1} with Type={2}. Key exists as Type={3}, which is not assignable from the requested Type={2}", new object[]
				{
					base.Message,
					this.Key.ToString(),
					this.Type.FullName,
					foundWithTypeString
				});
			case InvalidKeyException.Format.MultipleTypeMismatch:
				return string.Format("{0} No Asset found for Key={1} with Type={2}. Key exists as multiple Types={3}, which is not assignable from the requested Type={2}", new object[]
				{
					base.Message,
					this.Key.ToString(),
					this.Type.FullName,
					foundWithTypeString
				});
			}
			throw new ArgumentOutOfRangeException("format", format, null);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000277C File Offset: 0x0000097C
		internal string FormatMergeModeMessage(InvalidKeyException.Format format, string keysAvailable = null, string keysUnavailable = null, string typeString = null)
		{
			switch (format)
			{
			case InvalidKeyException.Format.NoMergeMode:
				return string.Format("{0} No MergeMode is set to merge the multiple keys requested. {1}, Type={2}", base.Message, this.GetKeyString(), this.Type.FullName);
			case InvalidKeyException.Format.NoLocation:
				return string.Format("\nNo Location found for Key={0}", (keysUnavailable == null) ? this.GetKeyString() : keysUnavailable);
			case InvalidKeyException.Format.MergeModeBase:
				return string.Format("{0} No {1} of Assets between {2} with Type={3}", new object[]
				{
					base.Message,
					(this.MergeMode != null) ? this.MergeMode.Value : Addressables.MergeMode.None,
					this.GetKeyString(),
					this.Type.FullName
				});
			case InvalidKeyException.Format.UnionAvailableForKeys:
				return string.Format("\nUnion of Type={0} found with {1}", typeString, keysAvailable);
			case InvalidKeyException.Format.UnionAvailableForKeysWithoutOther:
				return string.Format("\nUnion of Type={0} found with {1}. Without {2}", typeString, keysAvailable, keysUnavailable);
			case InvalidKeyException.Format.IntersectionAvailable:
				return string.Format("\nAn Intersection exists for Type={0}", typeString);
			case InvalidKeyException.Format.KeyAvailableAsType:
				return string.Format("\nType={0} exists for {1}", typeString, keysAvailable);
			}
			throw new ArgumentOutOfRangeException("format", format, null);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000289C File Offset: 0x00000A9C
		public override string Message
		{
			get
			{
				string stringKey = this.Key as string;
				if (!string.IsNullOrEmpty(stringKey))
				{
					if (this.m_Addressables == null)
					{
						return this.FormatMessage(InvalidKeyException.Format.StandardMessage, null);
					}
					return this.GetMessageForSingleKey(stringKey);
				}
				else
				{
					IEnumerable enumerableKey = this.Key as IEnumerable;
					if (enumerableKey == null)
					{
						return this.FormatMessage(InvalidKeyException.Format.StandardMessage, null);
					}
					int keyCount = 0;
					List<string> stringKeys = new List<string>();
					HashSet<string> keyTypeNames = new HashSet<string>();
					foreach (object keyObj in enumerableKey)
					{
						keyCount++;
						keyTypeNames.Add(keyObj.GetType().ToString());
						if (keyObj is string)
						{
							stringKeys.Add(keyObj as string);
						}
					}
					if (this.MergeMode == null)
					{
						string keysCSV = InvalidKeyException.GetCSVString(stringKeys, "Key=", "Keys=");
						this.FormatMergeModeMessage(InvalidKeyException.Format.NoMergeMode, null, null, null);
						return string.Format("{0} No MergeMode is set to merge the multiple keys requested. {1}, Type={2}", base.Message, keysCSV, this.Type);
					}
					if (keyCount != stringKeys.Count)
					{
						string types = InvalidKeyException.GetCSVString(keyTypeNames, "Type=", "Types=");
						return this.FormatMessage(InvalidKeyException.Format.MultipleTypesRequested, types);
					}
					if (keyCount == 1)
					{
						return this.GetMessageForSingleKey(stringKeys[0]);
					}
					return this.GetMessageforMergeKeys(stringKeys);
				}
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000029F8 File Offset: 0x00000BF8
		private string GetMessageForSingleKey(string keyString)
		{
			HashSet<Type> typesAvailableForKey = this.GetTypesForKey(keyString);
			if (typesAvailableForKey.Count == 0)
			{
				return this.FormatNotFoundMessage(keyString);
			}
			if (typesAvailableForKey.Count == 1)
			{
				return this.FormatTypeNotAssignableMessage(keyString, typesAvailableForKey);
			}
			return this.FormatMultipleAssignableTypesMessage(keyString, typesAvailableForKey);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002A37 File Offset: 0x00000C37
		private string FormatNotFoundMessage(string keyString)
		{
			return this.FormatMessage(InvalidKeyException.Format.NoLocation, null);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002A44 File Offset: 0x00000C44
		private string FormatTypeNotAssignableMessage(string keyString, HashSet<Type> typesAvailableForKey)
		{
			Type availableType = null;
			using (HashSet<Type>.Enumerator enumerator = typesAvailableForKey.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					availableType = enumerator.Current;
				}
			}
			if (availableType == null)
			{
				return this.FormatMessage(InvalidKeyException.Format.StandardMessage, null);
			}
			return this.FormatMessage(InvalidKeyException.Format.TypeMismatch, availableType.ToString());
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002AAC File Offset: 0x00000CAC
		private string FormatMultipleAssignableTypesMessage(string keyString, HashSet<Type> typesAvailableForKey)
		{
			StringBuilder csv = new StringBuilder(512);
			int count = 0;
			foreach (Type type in typesAvailableForKey)
			{
				count++;
				csv.Append((count > 1) ? string.Format(", {0}", type) : type.ToString());
			}
			return this.FormatMessage(InvalidKeyException.Format.MultipleTypeMismatch, csv.ToString());
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B30 File Offset: 0x00000D30
		private string GetMessageforMergeKeys(List<string> keys)
		{
			StringBuilder messageBuilder = new StringBuilder(this.FormatMergeModeMessage(InvalidKeyException.Format.MergeModeBase, null, null, null));
			Addressables.MergeMode? mergeMode = this.MergeMode;
			if (mergeMode != null)
			{
				switch (mergeMode.GetValueOrDefault())
				{
				case Addressables.MergeMode.None:
					goto IL_0242;
				case Addressables.MergeMode.Union:
				{
					Dictionary<Type, List<string>> typeToKeys = new Dictionary<Type, List<string>>();
					foreach (string key in keys)
					{
						if (!this.GetTypeToKeys(key, typeToKeys))
						{
							messageBuilder.Append(this.FormatMergeModeMessage(InvalidKeyException.Format.NoLocation, null, key, null));
						}
					}
					using (Dictionary<Type, List<string>>.Enumerator enumerator2 = typeToKeys.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							KeyValuePair<Type, List<string>> pair = enumerator2.Current;
							string availableKeysString = InvalidKeyException.GetCSVString(pair.Value, "Key=", "Keys=");
							List<string> unavailableKeys = new List<string>();
							foreach (string key2 in keys)
							{
								if (!pair.Value.Contains(key2))
								{
									unavailableKeys.Add(key2);
								}
							}
							if (unavailableKeys.Count == 0)
							{
								messageBuilder.Append(this.FormatMergeModeMessage(InvalidKeyException.Format.UnionAvailableForKeys, availableKeysString, null, pair.Key.ToString()));
							}
							else
							{
								string unavailableKeysString = InvalidKeyException.GetCSVString(unavailableKeys, "Key=", "Keys=");
								messageBuilder.Append(this.FormatMergeModeMessage(InvalidKeyException.Format.UnionAvailableForKeysWithoutOther, availableKeysString, unavailableKeysString, pair.Key.ToString()));
							}
						}
						goto IL_0310;
					}
					break;
				}
				case Addressables.MergeMode.Intersection:
					break;
				default:
					goto IL_0310;
				}
				bool hasInvalidKeys = false;
				Dictionary<Type, List<string>> typeToKeys2 = new Dictionary<Type, List<string>>();
				foreach (string key3 in keys)
				{
					if (!this.GetTypeToKeys(key3, typeToKeys2))
					{
						hasInvalidKeys = true;
						messageBuilder.Append(this.FormatMergeModeMessage(InvalidKeyException.Format.NoLocation, null, key3, null));
					}
				}
				if (hasInvalidKeys)
				{
					goto IL_0310;
				}
				using (Dictionary<Type, List<string>>.Enumerator enumerator2 = typeToKeys2.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<Type, List<string>> pair2 = enumerator2.Current;
						if (pair2.Value.Count == keys.Count)
						{
							messageBuilder.Append(this.FormatMergeModeMessage(InvalidKeyException.Format.IntersectionAvailable, null, null, pair2.Key.ToString()));
						}
					}
					goto IL_0310;
				}
				IL_0242:
				Dictionary<Type, List<string>> typeToKeys3 = new Dictionary<Type, List<string>>();
				foreach (string key4 in keys)
				{
					if (!this.GetTypeToKeys(key4, typeToKeys3))
					{
						messageBuilder.Append(this.FormatMergeModeMessage(InvalidKeyException.Format.NoLocation, null, key4, null));
					}
				}
				foreach (KeyValuePair<Type, List<string>> pair3 in typeToKeys3)
				{
					foreach (string key5 in pair3.Value)
					{
						messageBuilder.Append(this.FormatMergeModeMessage(InvalidKeyException.Format.KeyAvailableAsType, key5, null, pair3.Key.ToString()));
					}
				}
			}
			IL_0310:
			return messageBuilder.ToString();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002EB8 File Offset: 0x000010B8
		private HashSet<Type> GetTypesForKey(string keyString)
		{
			HashSet<Type> typesAvailableForKey = new HashSet<Type>();
			using (IEnumerator<IResourceLocator> enumerator = this.m_Addressables.ResourceLocators.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IList<IResourceLocation> locations;
					if (enumerator.Current.Locate(keyString, null, out locations))
					{
						foreach (IResourceLocation location in locations)
						{
							typesAvailableForKey.Add(location.ResourceType);
						}
					}
				}
			}
			return typesAvailableForKey;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002F54 File Offset: 0x00001154
		private bool GetTypeToKeys(string key, Dictionary<Type, List<string>> typeToKeys)
		{
			HashSet<Type> types = this.GetTypesForKey(key);
			if (types.Count == 0)
			{
				return false;
			}
			foreach (Type type in types)
			{
				List<string> keysForType;
				if (!typeToKeys.TryGetValue(type, out keysForType))
				{
					typeToKeys.Add(type, new List<string> { key });
				}
				else
				{
					keysForType.Add(key);
				}
			}
			return true;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002FD8 File Offset: 0x000011D8
		internal string GetKeyString()
		{
			if (this.Key is string)
			{
				string text = "Key=";
				object key = this.Key;
				return text + ((key != null) ? key.ToString() : null);
			}
			IEnumerable enumerableKey = this.Key as IEnumerable;
			if (enumerableKey != null)
			{
				return InvalidKeyException.GetCSVString(enumerableKey, "Key=", "Keys=");
			}
			return this.Key.ToString();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000303C File Offset: 0x0000123C
		internal static string GetCSVString(IEnumerable enumerator, string prefixSingle, string prefixPlural)
		{
			StringBuilder keysCSVBuilder = new StringBuilder(prefixPlural);
			int count = 0;
			foreach (object key in enumerator)
			{
				count++;
				keysCSVBuilder.Append((count > 1) ? string.Format(", {0}", key) : key);
			}
			if (count == 1 && !string.IsNullOrEmpty(prefixPlural) && !string.IsNullOrEmpty(prefixSingle))
			{
				keysCSVBuilder.Replace(prefixPlural, prefixSingle);
			}
			return keysCSVBuilder.ToString();
		}

		// Token: 0x04000015 RID: 21
		private AddressablesImpl m_Addressables;

		// Token: 0x04000016 RID: 22
		internal const string BaseInvalidKeyMessageFormat = "{0}, Key={1}, Type={2}";

		// Token: 0x04000017 RID: 23
		internal const string NoLocationMessageFormat = "{0} No Location found for Key={1}";

		// Token: 0x04000018 RID: 24
		internal const string MultipleTypeMismatchMessageFormat = "{0} No Asset found for Key={1} with Type={2}. Key exists as multiple Types={3}, which is not assignable from the requested Type={2}";

		// Token: 0x04000019 RID: 25
		internal const string TypeMismatchMessageFormat = "{0} No Asset found for Key={1} with Type={2}. Key exists as Type={3}, which is not assignable from the requested Type={2}";

		// Token: 0x0400001A RID: 26
		internal const string MultipleTypesMessageFormat = "{0} Enumerable key contains multiple Types. {1}, all Keys are expected to be strings";

		// Token: 0x0400001B RID: 27
		internal const string MergeModeNoLocationMessageFormat = "\nNo Location found for Key={0}";

		// Token: 0x0400001C RID: 28
		internal const string NoMergeModeMessageFormat = "{0} No MergeMode is set to merge the multiple keys requested. {1}, Type={2}";

		// Token: 0x0400001D RID: 29
		internal const string MergeModeBaseMessageFormat = "{0} No {1} of Assets between {2} with Type={3}";

		// Token: 0x0400001E RID: 30
		internal const string UnionAvailableForKeysMessageFormat = "\nUnion of Type={0} found with {1}";

		// Token: 0x0400001F RID: 31
		internal const string UnionAvailableForKeysWithoutOtherMessageFormat = "\nUnion of Type={0} found with {1}. Without {2}";

		// Token: 0x04000020 RID: 32
		internal const string IntersectionAvailableMessageFormat = "\nAn Intersection exists for Type={0}";

		// Token: 0x04000021 RID: 33
		internal const string KeyAvailableAsTypeMessageFormat = "\nType={0} exists for {1}";

		// Token: 0x0200000B RID: 11
		internal enum Format
		{
			// Token: 0x04000023 RID: 35
			StandardMessage,
			// Token: 0x04000024 RID: 36
			NoMergeMode,
			// Token: 0x04000025 RID: 37
			MultipleTypesRequested,
			// Token: 0x04000026 RID: 38
			NoLocation,
			// Token: 0x04000027 RID: 39
			TypeMismatch,
			// Token: 0x04000028 RID: 40
			MultipleTypeMismatch,
			// Token: 0x04000029 RID: 41
			MergeModeBase,
			// Token: 0x0400002A RID: 42
			UnionAvailableForKeys,
			// Token: 0x0400002B RID: 43
			UnionAvailableForKeysWithoutOther,
			// Token: 0x0400002C RID: 44
			IntersectionAvailable,
			// Token: 0x0400002D RID: 45
			KeyAvailableAsType
		}
	}
}
