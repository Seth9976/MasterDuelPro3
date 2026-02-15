using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.AddressableAssets.ResourceLocators
{
	// Token: 0x0200004A RID: 74
	[Serializable]
	public class ContentCatalogData
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x000083CD File Offset: 0x000065CD
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x000083D5 File Offset: 0x000065D5
		public string BuildResultHash
		{
			get
			{
				return this.m_BuildResultHash;
			}
			set
			{
				this.m_BuildResultHash = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000083DE File Offset: 0x000065DE
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x000083E6 File Offset: 0x000065E6
		public string ProviderId
		{
			get
			{
				return this.m_LocatorId;
			}
			internal set
			{
				this.m_LocatorId = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000083EF File Offset: 0x000065EF
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x000083F7 File Offset: 0x000065F7
		public ObjectInitializationData InstanceProviderData
		{
			get
			{
				return this.m_InstanceProviderData;
			}
			set
			{
				this.m_InstanceProviderData = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00008400 File Offset: 0x00006600
		// (set) Token: 0x060001EB RID: 491 RVA: 0x00008408 File Offset: 0x00006608
		public ObjectInitializationData SceneProviderData
		{
			get
			{
				return this.m_SceneProviderData;
			}
			set
			{
				this.m_SceneProviderData = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008411 File Offset: 0x00006611
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00008419 File Offset: 0x00006619
		public List<ObjectInitializationData> ResourceProviderData
		{
			get
			{
				return this.m_ResourceProviderData;
			}
			set
			{
				this.m_ResourceProviderData = value;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00008422 File Offset: 0x00006622
		public ContentCatalogData(string id)
		{
			this.m_LocatorId = id;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000843C File Offset: 0x0000663C
		public ContentCatalogData()
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000844F File Offset: 0x0000664F
		internal void CleanData()
		{
			this.m_LocatorId = null;
			this.m_Reader = null;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00008460 File Offset: 0x00006660
		internal void CopyToFile(string path)
		{
			byte[] bytes = this.m_Reader.GetBuffer();
			File.WriteAllBytes(path, bytes);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008480 File Offset: 0x00006680
		internal ContentCatalogData(BinaryStorageBuffer.Reader reader)
		{
			this.m_Reader = reader;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000849A File Offset: 0x0000669A
		internal byte[] GetBytes()
		{
			return this.m_Reader.GetBuffer();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000084A7 File Offset: 0x000066A7
		internal IResourceLocator CreateCustomLocator(string overrideId = "", string providerSuffix = null, int locatorCacheSize = 100)
		{
			this.m_LocatorId = overrideId;
			return new ContentCatalogData.ResourceLocator(this.m_LocatorId, this.m_Reader, locatorCacheSize, providerSuffix);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000084C3 File Offset: 0x000066C3
		internal static ContentCatalogData LoadFromFile(string path, int cacheSize = 1024)
		{
			return new ContentCatalogData(new BinaryStorageBuffer.Reader(File.ReadAllBytes(path), cacheSize, new BinaryStorageBuffer.ISerializationAdapter[]
			{
				new ContentCatalogData.Serializer()
			}));
		}

		// Token: 0x04000105 RID: 261
		private static int kMagic = "ContentCatalogData".GetHashCode();

		// Token: 0x04000106 RID: 262
		private const int kVersion = 2;

		// Token: 0x04000107 RID: 263
		[NonSerialized]
		public string LocalHash;

		// Token: 0x04000108 RID: 264
		[NonSerialized]
		internal IResourceLocation location;

		// Token: 0x04000109 RID: 265
		[SerializeField]
		internal string m_LocatorId;

		// Token: 0x0400010A RID: 266
		[SerializeField]
		internal string m_BuildResultHash;

		// Token: 0x0400010B RID: 267
		[SerializeField]
		private ObjectInitializationData m_InstanceProviderData;

		// Token: 0x0400010C RID: 268
		[SerializeField]
		private ObjectInitializationData m_SceneProviderData;

		// Token: 0x0400010D RID: 269
		[SerializeField]
		internal List<ObjectInitializationData> m_ResourceProviderData = new List<ObjectInitializationData>();

		// Token: 0x0400010E RID: 270
		private IList<ContentCatalogDataEntry> m_Entries;

		// Token: 0x0400010F RID: 271
		private BinaryStorageBuffer.Reader m_Reader;

		// Token: 0x0200004B RID: 75
		internal class Serializer : BinaryStorageBuffer.ISerializationAdapter<ContentCatalogData>, BinaryStorageBuffer.ISerializationAdapter
		{
			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060001F7 RID: 503 RVA: 0x000084F5 File Offset: 0x000066F5
			public IEnumerable<BinaryStorageBuffer.ISerializationAdapter> Dependencies
			{
				get
				{
					return new BinaryStorageBuffer.ISerializationAdapter[]
					{
						new ObjectInitializationData.Serializer(),
						new ContentCatalogData.AssetBundleRequestOptionsSerializationAdapter(),
						new ContentCatalogData.ResourceLocator.ResourceLocation.Serializer()
					};
				}
			}

			// Token: 0x060001F8 RID: 504 RVA: 0x00008518 File Offset: 0x00006718
			public object Deserialize(BinaryStorageBuffer.Reader reader, Type t, uint offset)
			{
				ContentCatalogData contentCatalogData = new ContentCatalogData(reader);
				ContentCatalogData.ResourceLocator.Header h = reader.ReadValue<ContentCatalogData.ResourceLocator.Header>(offset);
				if (h.magic != ContentCatalogData.kMagic)
				{
					throw new Exception("Invalid header data!!!");
				}
				if (h.version != 2)
				{
					throw new Exception(string.Format("Expected catalog data version {0}, but file was written with version {1}.", 2, h.version));
				}
				contentCatalogData.InstanceProviderData = reader.ReadObject<ObjectInitializationData>(h.instanceProvider, true);
				contentCatalogData.SceneProviderData = reader.ReadObject<ObjectInitializationData>(h.sceneProvider, true);
				contentCatalogData.ResourceProviderData = reader.ReadObjectArray<ObjectInitializationData>(h.initObjectsArray, true).ToList<ObjectInitializationData>();
				contentCatalogData.BuildResultHash = reader.ReadString(h.buildResultHash, '\0', true);
				return contentCatalogData;
			}

			// Token: 0x060001F9 RID: 505 RVA: 0x000085C8 File Offset: 0x000067C8
			public uint Serialize(BinaryStorageBuffer.Writer writer, object val)
			{
				ContentCatalogData cd = val as ContentCatalogData;
				IList<ContentCatalogDataEntry> entries = cd.m_Entries;
				Dictionary<object, List<int>> keyToEntryIndices = new Dictionary<object, List<int>>();
				for (int m = 0; m < entries.Count; m++)
				{
					foreach (object j in entries[m].Keys)
					{
						List<int> indices;
						if (!keyToEntryIndices.TryGetValue(j, out indices))
						{
							keyToEntryIndices.Add(j, indices = new List<int>());
						}
						indices.Add(m);
					}
				}
				uint headerOffset = writer.Reserve<ContentCatalogData.ResourceLocator.Header>();
				uint keysOffset = writer.Reserve<ContentCatalogData.ResourceLocator.KeyData>((uint)keyToEntryIndices.Count);
				ContentCatalogData.ResourceLocator.Header header = new ContentCatalogData.ResourceLocator.Header
				{
					magic = ContentCatalogData.kMagic,
					version = 2,
					keysOffset = keysOffset,
					idOffset = writer.WriteString(cd.ProviderId, '\0'),
					instanceProvider = writer.WriteObject(cd.InstanceProviderData, false),
					sceneProvider = writer.WriteObject(cd.SceneProviderData, false),
					initObjectsArray = writer.WriteObjects<ObjectInitializationData>(cd.m_ResourceProviderData, false),
					buildResultHash = writer.WriteString(cd.BuildResultHash, '\0')
				};
				writer.Write<ContentCatalogData.ResourceLocator.Header>(headerOffset, in header);
				uint[] locationIds = new uint[entries.Count];
				for (int k = 0; k < entries.Count; k++)
				{
					locationIds[k] = writer.WriteObject(new ContentCatalogData.ResourceLocator.ContentCatalogDataEntrySerializationContext
					{
						entry = entries[k],
						allEntries = entries,
						keyToEntryIndices = keyToEntryIndices
					}, false);
				}
				int keyIndex = 0;
				ContentCatalogData.ResourceLocator.KeyData[] allKeys = new ContentCatalogData.ResourceLocator.KeyData[keyToEntryIndices.Count];
				Func<int, uint> <>9__0;
				foreach (KeyValuePair<object, List<int>> l in keyToEntryIndices)
				{
					IEnumerable<int> value = l.Value;
					Func<int, uint> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (int i) => locationIds[i]);
					}
					uint[] locationOffsets = value.Select(func).ToArray<uint>();
					allKeys[keyIndex++] = new ContentCatalogData.ResourceLocator.KeyData
					{
						keyNameOffset = writer.WriteObject(l.Key, true),
						locationSetOffset = writer.Write<uint>(locationOffsets, true)
					};
				}
				writer.Write<ContentCatalogData.ResourceLocator.KeyData>(keysOffset, allKeys, true);
				return headerOffset;
			}
		}

		// Token: 0x0200004D RID: 77
		internal class ResourceLocator : IResourceLocator
		{
			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060001FD RID: 509 RVA: 0x00008856 File Offset: 0x00006A56
			// (set) Token: 0x060001FE RID: 510 RVA: 0x0000885E File Offset: 0x00006A5E
			public string LocatorId { get; private set; }

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060001FF RID: 511 RVA: 0x00008867 File Offset: 0x00006A67
			public IEnumerable<object> Keys
			{
				get
				{
					return this.keyData.Keys;
				}
			}

			// Token: 0x17000048 RID: 72
			// (get) Token: 0x06000200 RID: 512 RVA: 0x00008874 File Offset: 0x00006A74
			public IEnumerable<IResourceLocation> AllLocations
			{
				get
				{
					HashSet<IResourceLocation> allLocs = new HashSet<IResourceLocation>(new ResourceLocationComparer());
					foreach (KeyValuePair<object, uint> kvp in this.keyData)
					{
						IList<IResourceLocation> locs;
						if (this.Locate(kvp.Key, null, out locs))
						{
							foreach (IResourceLocation i in locs)
							{
								allLocs.Add(i);
							}
						}
					}
					return allLocs;
				}
			}

			// Token: 0x06000201 RID: 513 RVA: 0x00008920 File Offset: 0x00006B20
			internal ResourceLocator(string id, BinaryStorageBuffer.Reader reader, int cacheLimit, string providerSuffix)
			{
				this.LocatorId = id;
				this.providerSuffix = providerSuffix;
				this.reader = reader;
				this.m_Cache = new LRUCache<ContentCatalogData.ResourceLocator.CacheKey, IList<IResourceLocation>>(cacheLimit);
				this.keyData = new Dictionary<object, uint>();
				ContentCatalogData.ResourceLocator.Header header = reader.ReadValue<ContentCatalogData.ResourceLocator.Header>(0U);
				ContentCatalogData.ResourceLocator.KeyData[] array = reader.ReadValueArray<ContentCatalogData.ResourceLocator.KeyData>(header.keysOffset, false);
				int index = 0;
				foreach (ContentCatalogData.ResourceLocator.KeyData i in array)
				{
					object key = reader.ReadObject(i.keyNameOffset, true);
					this.keyData.Add(key, i.locationSetOffset);
					index++;
				}
			}

			// Token: 0x06000202 RID: 514 RVA: 0x000089B8 File Offset: 0x00006BB8
			public bool Locate(object key, Type type, out IList<IResourceLocation> locations)
			{
				ContentCatalogData.ResourceLocator.CacheKey cacheKey = new ContentCatalogData.ResourceLocator.CacheKey(key, type);
				if (this.m_Cache.TryGet(cacheKey, out locations))
				{
					return true;
				}
				uint locationSetOffset;
				if (!this.keyData.TryGetValue(key, out locationSetOffset))
				{
					locations = null;
					return false;
				}
				ContentCatalogData.ResourceLocator.ResourceLocation[] locs = this.reader.ReadObjectArray<ContentCatalogData.ResourceLocator.ResourceLocation>(locationSetOffset, true);
				if (this.providerSuffix != null)
				{
					foreach (ContentCatalogData.ResourceLocator.ResourceLocation i in locs)
					{
						if (!i.ProviderId.EndsWith(this.providerSuffix))
						{
							i.ProviderId += this.providerSuffix;
						}
					}
				}
				if (type == null || type == typeof(object))
				{
					locations = new List<IResourceLocation>(locs);
					this.m_Cache.TryAdd(cacheKey, locations);
					return true;
				}
				int validTypeCount = 0;
				foreach (ContentCatalogData.ResourceLocator.ResourceLocation j in locs)
				{
					if (type.IsAssignableFrom(j.ResourceType))
					{
						validTypeCount++;
					}
				}
				if (validTypeCount == 0)
				{
					locations = null;
					this.m_Cache.TryAdd(cacheKey, locations);
					return false;
				}
				if (validTypeCount == locs.Length)
				{
					locations = new List<IResourceLocation>(locs);
					this.m_Cache.TryAdd(cacheKey, locations);
					return true;
				}
				locations = new List<IResourceLocation>();
				foreach (ContentCatalogData.ResourceLocator.ResourceLocation k in locs)
				{
					if (type.IsAssignableFrom(k.ResourceType))
					{
						locations.Add(k);
					}
				}
				this.m_Cache.TryAdd(cacheKey, locations);
				return locations != null;
			}

			// Token: 0x04000112 RID: 274
			private LRUCache<ContentCatalogData.ResourceLocator.CacheKey, IList<IResourceLocation>> m_Cache;

			// Token: 0x04000113 RID: 275
			private Dictionary<object, uint> keyData;

			// Token: 0x04000114 RID: 276
			private BinaryStorageBuffer.Reader reader;

			// Token: 0x04000116 RID: 278
			private string providerSuffix;

			// Token: 0x0200004E RID: 78
			public struct Header
			{
				// Token: 0x04000117 RID: 279
				public int magic;

				// Token: 0x04000118 RID: 280
				public int version;

				// Token: 0x04000119 RID: 281
				public uint keysOffset;

				// Token: 0x0400011A RID: 282
				public uint idOffset;

				// Token: 0x0400011B RID: 283
				public uint instanceProvider;

				// Token: 0x0400011C RID: 284
				public uint sceneProvider;

				// Token: 0x0400011D RID: 285
				public uint initObjectsArray;

				// Token: 0x0400011E RID: 286
				public uint buildResultHash;
			}

			// Token: 0x0200004F RID: 79
			public struct KeyData
			{
				// Token: 0x0400011F RID: 287
				public uint keyNameOffset;

				// Token: 0x04000120 RID: 288
				public uint locationSetOffset;
			}

			// Token: 0x02000050 RID: 80
			internal class ContentCatalogDataEntrySerializationContext
			{
				// Token: 0x04000121 RID: 289
				public ContentCatalogDataEntry entry;

				// Token: 0x04000122 RID: 290
				public Dictionary<object, List<int>> keyToEntryIndices;

				// Token: 0x04000123 RID: 291
				public IList<ContentCatalogDataEntry> allEntries;
			}

			// Token: 0x02000051 RID: 81
			internal class ResourceLocation : IResourceLocation
			{
				// Token: 0x06000204 RID: 516 RVA: 0x00008B40 File Offset: 0x00006D40
				public ResourceLocation(BinaryStorageBuffer.Reader r, uint id)
				{
					ContentCatalogData.ResourceLocator.ResourceLocation.Serializer.Data d = r.ReadValue<ContentCatalogData.ResourceLocator.ResourceLocation.Serializer.Data>(id);
					this.PrimaryKey = r.ReadString(d.primaryKeyOffset, '/', false);
					this.InternalId = Addressables.ResolveInternalId(r.ReadString(d.internalIdOffset, '/', false));
					this.Data = r.ReadObject(d.extraDataOffset, false);
					this.ProviderId = r.ReadString(d.providerOffset, '.', true);
					this.Dependencies = r.ReadObjectArray<ContentCatalogData.ResourceLocator.ResourceLocation>(d.dependencySetOffset, true);
					this.DependencyHashCode = (int)d.dependencySetOffset;
					this.ResourceType = r.ReadObject<Type>(d.typeId, true);
				}

				// Token: 0x06000205 RID: 517 RVA: 0x00008BE4 File Offset: 0x00006DE4
				public override string ToString()
				{
					return this.InternalId;
				}

				// Token: 0x17000049 RID: 73
				// (get) Token: 0x06000207 RID: 519 RVA: 0x00008BF5 File Offset: 0x00006DF5
				// (set) Token: 0x06000206 RID: 518 RVA: 0x00008BEC File Offset: 0x00006DEC
				public string PrimaryKey { get; private set; }

				// Token: 0x1700004A RID: 74
				// (get) Token: 0x06000209 RID: 521 RVA: 0x00008C06 File Offset: 0x00006E06
				// (set) Token: 0x06000208 RID: 520 RVA: 0x00008BFD File Offset: 0x00006DFD
				public string InternalId { get; private set; }

				// Token: 0x1700004B RID: 75
				// (get) Token: 0x0600020B RID: 523 RVA: 0x00008C17 File Offset: 0x00006E17
				// (set) Token: 0x0600020A RID: 522 RVA: 0x00008C0E File Offset: 0x00006E0E
				public object Data { get; private set; }

				// Token: 0x1700004C RID: 76
				// (get) Token: 0x0600020D RID: 525 RVA: 0x00008C28 File Offset: 0x00006E28
				// (set) Token: 0x0600020C RID: 524 RVA: 0x00008C1F File Offset: 0x00006E1F
				public string ProviderId { get; set; }

				// Token: 0x1700004D RID: 77
				// (get) Token: 0x0600020F RID: 527 RVA: 0x00008C39 File Offset: 0x00006E39
				// (set) Token: 0x0600020E RID: 526 RVA: 0x00008C30 File Offset: 0x00006E30
				public IList<IResourceLocation> Dependencies { get; private set; }

				// Token: 0x1700004E RID: 78
				// (get) Token: 0x06000211 RID: 529 RVA: 0x00008C4A File Offset: 0x00006E4A
				// (set) Token: 0x06000210 RID: 528 RVA: 0x00008C41 File Offset: 0x00006E41
				public int DependencyHashCode { get; private set; }

				// Token: 0x1700004F RID: 79
				// (get) Token: 0x06000212 RID: 530 RVA: 0x00008C52 File Offset: 0x00006E52
				public bool HasDependencies
				{
					get
					{
						return this.DependencyHashCode >= 0;
					}
				}

				// Token: 0x17000050 RID: 80
				// (get) Token: 0x06000214 RID: 532 RVA: 0x00008C69 File Offset: 0x00006E69
				// (set) Token: 0x06000213 RID: 531 RVA: 0x00008C60 File Offset: 0x00006E60
				public Type ResourceType { get; private set; }

				// Token: 0x06000215 RID: 533 RVA: 0x00008C71 File Offset: 0x00006E71
				public int Hash(Type t)
				{
					return (this.InternalId.GetHashCode() * 31 + t.GetHashCode()) * 31 + this.DependencyHashCode;
				}

				// Token: 0x02000052 RID: 82
				public class Serializer : BinaryStorageBuffer.ISerializationAdapter<ContentCatalogData.ResourceLocator.ResourceLocation>, BinaryStorageBuffer.ISerializationAdapter, BinaryStorageBuffer.ISerializationAdapter<ContentCatalogData.ResourceLocator.ContentCatalogDataEntrySerializationContext>
				{
					// Token: 0x17000051 RID: 81
					// (get) Token: 0x06000216 RID: 534 RVA: 0x00008C92 File Offset: 0x00006E92
					public IEnumerable<BinaryStorageBuffer.ISerializationAdapter> Dependencies
					{
						get
						{
							return null;
						}
					}

					// Token: 0x06000217 RID: 535 RVA: 0x00008C95 File Offset: 0x00006E95
					public object Deserialize(BinaryStorageBuffer.Reader reader, Type t, uint offset)
					{
						return new ContentCatalogData.ResourceLocator.ResourceLocation(reader, offset);
					}

					// Token: 0x06000218 RID: 536 RVA: 0x00008CA0 File Offset: 0x00006EA0
					public uint Serialize(BinaryStorageBuffer.Writer writer, object val)
					{
						ContentCatalogData.ResourceLocator.ContentCatalogDataEntrySerializationContext ec = val as ContentCatalogData.ResourceLocator.ContentCatalogDataEntrySerializationContext;
						ContentCatalogDataEntry e = ec.entry;
						uint depId = uint.MaxValue;
						if (e.Dependencies != null && e.Dependencies.Count > 0)
						{
							HashSet<uint> depIds = new HashSet<uint>();
							foreach (object i in e.Dependencies)
							{
								foreach (int j in ec.keyToEntryIndices[i])
								{
									depIds.Add(writer.WriteObject(new ContentCatalogData.ResourceLocator.ContentCatalogDataEntrySerializationContext
									{
										entry = ec.allEntries[j],
										allEntries = ec.allEntries,
										keyToEntryIndices = ec.keyToEntryIndices
									}, false));
								}
							}
							depId = writer.Write<uint>(depIds.ToArray<uint>(), false);
						}
						ContentCatalogData.ResourceLocator.ResourceLocation.Serializer.Data data = new ContentCatalogData.ResourceLocator.ResourceLocation.Serializer.Data
						{
							primaryKeyOffset = writer.WriteString(e.Keys[0] as string, '/'),
							internalIdOffset = writer.WriteString(e.InternalId, '/'),
							providerOffset = writer.WriteString(e.Provider, '.'),
							dependencySetOffset = depId,
							extraDataOffset = writer.WriteObject(e.Data, true),
							typeId = writer.WriteObject(e.ResourceType, false)
						};
						return writer.Write<ContentCatalogData.ResourceLocator.ResourceLocation.Serializer.Data>(data);
					}

					// Token: 0x02000053 RID: 83
					public struct Data
					{
						// Token: 0x0400012B RID: 299
						public uint primaryKeyOffset;

						// Token: 0x0400012C RID: 300
						public uint internalIdOffset;

						// Token: 0x0400012D RID: 301
						public uint providerOffset;

						// Token: 0x0400012E RID: 302
						public uint dependencySetOffset;

						// Token: 0x0400012F RID: 303
						public int dependencyHashValue;

						// Token: 0x04000130 RID: 304
						public uint extraDataOffset;

						// Token: 0x04000131 RID: 305
						public uint typeId;
					}
				}
			}

			// Token: 0x02000054 RID: 84
			private struct CacheKey : IEquatable<ContentCatalogData.ResourceLocator.CacheKey>
			{
				// Token: 0x0600021A RID: 538 RVA: 0x00008E48 File Offset: 0x00007048
				public CacheKey(object o, Type t)
				{
					this.key = o;
					this.type = t;
					this.hashCode = ((this.type == null) ? this.key.GetHashCode() : (this.key.GetHashCode() * 31 + this.type.GetHashCode()));
				}

				// Token: 0x0600021B RID: 539 RVA: 0x00008EA0 File Offset: 0x000070A0
				public bool Equals(ContentCatalogData.ResourceLocator.CacheKey other)
				{
					return this.key.Equals(other.key) && ((this.type == null && other.type == null) || this.type.Equals(other.type));
				}

				// Token: 0x0600021C RID: 540 RVA: 0x00008EF1 File Offset: 0x000070F1
				public override int GetHashCode()
				{
					return this.hashCode;
				}

				// Token: 0x04000132 RID: 306
				public object key;

				// Token: 0x04000133 RID: 307
				public Type type;

				// Token: 0x04000134 RID: 308
				private int hashCode;
			}
		}

		// Token: 0x02000055 RID: 85
		internal class AssetBundleRequestOptionsSerializationAdapter : BinaryStorageBuffer.ISerializationAdapter<AssetBundleRequestOptions>, BinaryStorageBuffer.ISerializationAdapter
		{
			// Token: 0x17000052 RID: 82
			// (get) Token: 0x0600021D RID: 541 RVA: 0x00008C92 File Offset: 0x00006E92
			public IEnumerable<BinaryStorageBuffer.ISerializationAdapter> Dependencies
			{
				get
				{
					return null;
				}
			}

			// Token: 0x0600021E RID: 542 RVA: 0x00008EFC File Offset: 0x000070FC
			public object Deserialize(BinaryStorageBuffer.Reader reader, Type type, uint offset)
			{
				if (type != typeof(AssetBundleRequestOptions))
				{
					return null;
				}
				ContentCatalogData.AssetBundleRequestOptionsSerializationAdapter.SerializedData sd = reader.ReadValue<ContentCatalogData.AssetBundleRequestOptionsSerializationAdapter.SerializedData>(offset);
				ContentCatalogData.AssetBundleRequestOptionsSerializationAdapter.SerializedData.Common com = reader.ReadValue<ContentCatalogData.AssetBundleRequestOptionsSerializationAdapter.SerializedData.Common>(sd.commonId);
				return new AssetBundleRequestOptions
				{
					Hash = reader.ReadValue<Hash128>(sd.hashId).ToString(),
					BundleName = reader.ReadString(sd.bundleNameId, '_', true),
					Crc = sd.crc,
					BundleSize = (long)((ulong)sd.bundleSize),
					Timeout = (int)com.timeout,
					RetryCount = (int)com.retryCount,
					RedirectLimit = (int)com.redirectLimit,
					AssetLoadMode = com.assetLoadMode,
					ChunkedTransfer = com.chunkedTransfer,
					UseUnityWebRequestForLocalBundles = com.useUnityWebRequestForLocalBundles,
					UseCrcForCachedBundle = com.useCrcForCachedBundle,
					ClearOtherCachedVersionsWhenLoaded = com.clearOtherCachedVersionsWhenLoaded
				};
			}

			// Token: 0x0600021F RID: 543 RVA: 0x00008FEC File Offset: 0x000071EC
			public uint Serialize(BinaryStorageBuffer.Writer writer, object obj)
			{
				AssetBundleRequestOptions options = obj as AssetBundleRequestOptions;
				Hash128 hash = Hash128.Parse(options.Hash);
				short timeout = (short)Mathf.Clamp(options.Timeout, 0, 32767);
				byte retryCount = (byte)Mathf.Clamp(options.RetryCount, 0, 128);
				byte redirectLimit = ((options.RedirectLimit < 0) ? 32 : ((byte)Mathf.Clamp(options.RedirectLimit, 0, 128)));
				ContentCatalogData.AssetBundleRequestOptionsSerializationAdapter.SerializedData sd = new ContentCatalogData.AssetBundleRequestOptionsSerializationAdapter.SerializedData
				{
					hashId = writer.Write<Hash128>(hash),
					bundleNameId = writer.WriteString(options.BundleName, '_'),
					crc = options.Crc,
					bundleSize = (uint)options.BundleSize,
					commonId = writer.Write<ContentCatalogData.AssetBundleRequestOptionsSerializationAdapter.SerializedData.Common>(new ContentCatalogData.AssetBundleRequestOptionsSerializationAdapter.SerializedData.Common
					{
						timeout = timeout,
						redirectLimit = redirectLimit,
						retryCount = retryCount,
						assetLoadMode = options.AssetLoadMode,
						chunkedTransfer = options.ChunkedTransfer,
						clearOtherCachedVersionsWhenLoaded = options.ClearOtherCachedVersionsWhenLoaded,
						useCrcForCachedBundle = options.UseCrcForCachedBundle,
						useUnityWebRequestForLocalBundles = options.UseUnityWebRequestForLocalBundles
					})
				};
				return writer.Write<ContentCatalogData.AssetBundleRequestOptionsSerializationAdapter.SerializedData>(sd);
			}

			// Token: 0x02000056 RID: 86
			private struct SerializedData
			{
				// Token: 0x04000135 RID: 309
				public uint hashId;

				// Token: 0x04000136 RID: 310
				public uint bundleNameId;

				// Token: 0x04000137 RID: 311
				public uint crc;

				// Token: 0x04000138 RID: 312
				public uint bundleSize;

				// Token: 0x04000139 RID: 313
				public uint commonId;

				// Token: 0x02000057 RID: 87
				public struct Common
				{
					// Token: 0x17000053 RID: 83
					// (get) Token: 0x06000221 RID: 545 RVA: 0x00009116 File Offset: 0x00007316
					// (set) Token: 0x06000222 RID: 546 RVA: 0x00009126 File Offset: 0x00007326
					public AssetLoadMode assetLoadMode
					{
						get
						{
							if ((this.flags & 1) != 1)
							{
								return AssetLoadMode.RequestedAssetAndDependencies;
							}
							return AssetLoadMode.AllPackedAssetsAndDependencies;
						}
						set
						{
							this.flags = (this.flags & -2) | (int)value;
						}
					}

					// Token: 0x17000054 RID: 84
					// (get) Token: 0x06000223 RID: 547 RVA: 0x00009139 File Offset: 0x00007339
					// (set) Token: 0x06000224 RID: 548 RVA: 0x00009146 File Offset: 0x00007346
					public bool chunkedTransfer
					{
						get
						{
							return (this.flags & 2) == 2;
						}
						set
						{
							this.flags = (this.flags & -3) | (value ? 2 : 0);
						}
					}

					// Token: 0x17000055 RID: 85
					// (get) Token: 0x06000225 RID: 549 RVA: 0x0000915F File Offset: 0x0000735F
					// (set) Token: 0x06000226 RID: 550 RVA: 0x0000916C File Offset: 0x0000736C
					public bool useCrcForCachedBundle
					{
						get
						{
							return (this.flags & 4) == 4;
						}
						set
						{
							this.flags = (this.flags & -5) | (value ? 4 : 0);
						}
					}

					// Token: 0x17000056 RID: 86
					// (get) Token: 0x06000227 RID: 551 RVA: 0x00009185 File Offset: 0x00007385
					// (set) Token: 0x06000228 RID: 552 RVA: 0x00009192 File Offset: 0x00007392
					public bool useUnityWebRequestForLocalBundles
					{
						get
						{
							return (this.flags & 8) == 8;
						}
						set
						{
							this.flags = (this.flags & -9) | (value ? 8 : 0);
						}
					}

					// Token: 0x17000057 RID: 87
					// (get) Token: 0x06000229 RID: 553 RVA: 0x000091AB File Offset: 0x000073AB
					// (set) Token: 0x0600022A RID: 554 RVA: 0x000091BA File Offset: 0x000073BA
					public bool clearOtherCachedVersionsWhenLoaded
					{
						get
						{
							return (this.flags & 16) == 16;
						}
						set
						{
							this.flags = (this.flags & -17) | (value ? 16 : 0);
						}
					}

					// Token: 0x0400013A RID: 314
					public short timeout;

					// Token: 0x0400013B RID: 315
					public byte redirectLimit;

					// Token: 0x0400013C RID: 316
					public byte retryCount;

					// Token: 0x0400013D RID: 317
					public int flags;
				}
			}
		}
	}
}
