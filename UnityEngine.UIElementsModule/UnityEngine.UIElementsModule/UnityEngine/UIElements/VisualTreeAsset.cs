using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Assertions;
using UnityEngine.Bindings;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x020004B3 RID: 1203
	[HelpURL("UIE-VisualTree-landing")]
	[Serializable]
	public class VisualTreeAsset : ScriptableObject
	{
		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x0007D9DC File Offset: 0x0007BBDC
		// (set) Token: 0x0600224C RID: 8780 RVA: 0x0007D9F4 File Offset: 0x0007BBF4
		public bool importedWithErrors
		{
			get
			{
				return this.m_ImportedWithErrors;
			}
			internal set
			{
				this.m_ImportedWithErrors = value;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x0007DA00 File Offset: 0x0007BC00
		// (set) Token: 0x0600224E RID: 8782 RVA: 0x0007DA18 File Offset: 0x0007BC18
		public bool importedWithWarnings
		{
			get
			{
				return this.m_ImportedWithWarnings;
			}
			internal set
			{
				this.m_ImportedWithWarnings = value;
			}
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x0007DA24 File Offset: 0x0007BC24
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal int GetNextChildSerialNumber()
		{
			List<VisualElementAsset> visualElementAssets = this.m_VisualElementAssets;
			int i = ((visualElementAssets != null) ? visualElementAssets.Count : 0);
			int num = i;
			List<TemplateAsset> templateAssets = this.m_TemplateAssets;
			i = num + ((templateAssets != null) ? templateAssets.Count : 0);
			bool flag = this.m_UxmlObjectEntries != null;
			if (flag)
			{
				i += this.m_UxmlObjectEntries.Count;
				foreach (VisualTreeAsset.UxmlObjectEntry entry in this.m_UxmlObjectEntries)
				{
					bool flag2 = entry.uxmlObjectAssets != null;
					if (flag2)
					{
						i += entry.uxmlObjectAssets.Count;
					}
				}
			}
			return i;
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x0007DAE0 File Offset: 0x0007BCE0
		internal List<VisualTreeAsset.UsingEntry> usings
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_Usings;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x0007DAE8 File Offset: 0x0007BCE8
		public IEnumerable<VisualTreeAsset> templateDependencies
		{
			get
			{
				bool flag = this.m_Usings.Count == 0;
				if (flag)
				{
					yield break;
				}
				HashSet<VisualTreeAsset> sent = new HashSet<VisualTreeAsset>();
				foreach (VisualTreeAsset.UsingEntry entry in this.m_Usings)
				{
					bool flag2 = entry.asset != null && !sent.Contains(entry.asset);
					if (flag2)
					{
						sent.Add(entry.asset);
						yield return entry.asset;
					}
					else
					{
						bool flag3 = !string.IsNullOrEmpty(entry.path);
						if (flag3)
						{
							VisualTreeAsset vta = Panel.LoadResource(entry.path, typeof(VisualTreeAsset), 1f) as VisualTreeAsset;
							bool flag4 = vta != null && !sent.Contains(entry.asset);
							if (flag4)
							{
								sent.Add(entry.asset);
								yield return vta;
							}
							vta = null;
						}
					}
					entry = default(VisualTreeAsset.UsingEntry);
				}
				List<VisualTreeAsset.UsingEntry>.Enumerator enumerator = default(List<VisualTreeAsset.UsingEntry>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x0007DB08 File Offset: 0x0007BD08
		public IEnumerable<StyleSheet> stylesheets
		{
			get
			{
				HashSet<StyleSheet> sent = new HashSet<StyleSheet>();
				foreach (VisualElementAsset vea in this.m_VisualElementAssets)
				{
					bool hasStylesheets = vea.hasStylesheets;
					if (hasStylesheets)
					{
						foreach (StyleSheet stylesheet in vea.stylesheets)
						{
							bool flag = !sent.Contains(stylesheet);
							if (flag)
							{
								sent.Add(stylesheet);
								yield return stylesheet;
							}
							stylesheet = null;
						}
						List<StyleSheet>.Enumerator enumerator2 = default(List<StyleSheet>.Enumerator);
					}
					bool hasStylesheetPaths = vea.hasStylesheetPaths;
					if (hasStylesheetPaths)
					{
						foreach (string stylesheetPath in vea.stylesheetPaths)
						{
							StyleSheet stylesheet2 = Panel.LoadResource(stylesheetPath, typeof(StyleSheet), 1f) as StyleSheet;
							bool flag2 = stylesheet2 != null && !sent.Contains(stylesheet2);
							if (flag2)
							{
								sent.Add(stylesheet2);
								yield return stylesheet2;
							}
							stylesheet2 = null;
							stylesheetPath = null;
						}
						List<string>.Enumerator enumerator3 = default(List<string>.Enumerator);
					}
					vea = null;
				}
				List<VisualElementAsset>.Enumerator enumerator = default(List<VisualElementAsset>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x0007DB27 File Offset: 0x0007BD27
		internal List<VisualElementAsset> visualElementAssets
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_VisualElementAssets;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x0007DB2F File Offset: 0x0007BD2F
		internal List<TemplateAsset> templateAssets
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_TemplateAssets;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x0007DB37 File Offset: 0x0007BD37
		internal List<VisualTreeAsset.UxmlObjectEntry> uxmlObjectEntries
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_UxmlObjectEntries;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x0007DB3F File Offset: 0x0007BD3F
		internal List<int> uxmlObjectIds
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_UxmlObjectIds;
			}
		}

		// Token: 0x06002257 RID: 8791 RVA: 0x0007DB48 File Offset: 0x0007BD48
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void RemoveElementAndDependencies(VisualElementAsset asset)
		{
			bool flag = asset == null;
			if (!flag)
			{
				this.m_VisualElementAssets.Remove(asset);
				this.RemoveUxmlObjectEntryDependencies(asset.id);
			}
		}

		// Token: 0x06002258 RID: 8792 RVA: 0x0007DB7C File Offset: 0x0007BD7C
		internal void RegisterUxmlObject(UxmlObjectAsset uxmlObjectAsset)
		{
			VisualTreeAsset.UxmlObjectEntry entry = this.GetUxmlObjectEntry(uxmlObjectAsset.parentId);
			bool flag = entry.uxmlObjectAssets != null;
			if (flag)
			{
				entry.uxmlObjectAssets.Add(uxmlObjectAsset);
			}
			else
			{
				this.m_UxmlObjectEntries.Add(new VisualTreeAsset.UxmlObjectEntry(uxmlObjectAsset.parentId, new List<UxmlObjectAsset> { uxmlObjectAsset }));
				this.m_UxmlObjectIds.Add(uxmlObjectAsset.id);
			}
		}

		// Token: 0x06002259 RID: 8793 RVA: 0x0007DBF0 File Offset: 0x0007BDF0
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal UxmlObjectAsset AddUxmlObject(UxmlAsset parent, string fieldUxmlName, string fullTypeName, UxmlNamespaceDefinition xmlNamespace = default(UxmlNamespaceDefinition))
		{
			VisualTreeAsset.UxmlObjectEntry entry = this.GetUxmlObjectEntry(parent.id);
			bool flag = entry.uxmlObjectAssets == null;
			if (flag)
			{
				entry = new VisualTreeAsset.UxmlObjectEntry(parent.id, new List<UxmlObjectAsset>());
				this.m_UxmlObjectEntries.Add(entry);
			}
			bool flag2 = string.IsNullOrEmpty(fieldUxmlName);
			UxmlObjectAsset uxmlObjectAsset;
			if (flag2)
			{
				UxmlObjectAsset newAsset = new UxmlObjectAsset(fullTypeName, false, xmlNamespace);
				newAsset.parentId = parent.id;
				newAsset.id = this.GetNextUxmlAssetId(parent.id);
				this.m_UxmlObjectIds.Add(newAsset.id);
				entry.uxmlObjectAssets.Add(newAsset);
				uxmlObjectAsset = newAsset;
			}
			else
			{
				UxmlObjectAsset fieldAsset = entry.GetField(fieldUxmlName);
				bool flag3 = fieldAsset == null;
				if (flag3)
				{
					fieldAsset = new UxmlObjectAsset(fieldUxmlName, true, xmlNamespace);
					entry.uxmlObjectAssets.Add(fieldAsset);
					fieldAsset.parentId = parent.id;
					fieldAsset.id = this.GetNextUxmlAssetId(parent.id);
					this.m_UxmlObjectIds.Add(fieldAsset.id);
				}
				uxmlObjectAsset = this.AddUxmlObject(fieldAsset, null, fullTypeName, xmlNamespace);
			}
			return uxmlObjectAsset;
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x0007DD08 File Offset: 0x0007BF08
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal int GetNextUxmlAssetId(int parentId)
		{
			int guid = Guid.NewGuid().GetHashCode();
			return (this.GetNextChildSerialNumber() + 585386304) * -1521134295 + parentId + guid;
		}

		// Token: 0x0600225B RID: 8795 RVA: 0x0007DD44 File Offset: 0x0007BF44
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void RemoveUxmlObject(int id, bool onlyIfIsField = false)
		{
			for (int i = 0; i < this.m_UxmlObjectEntries.Count; i++)
			{
				VisualTreeAsset.UxmlObjectEntry entry = this.m_UxmlObjectEntries[i];
				int j = 0;
				while (j < entry.uxmlObjectAssets.Count)
				{
					UxmlObjectAsset asset = entry.uxmlObjectAssets[j];
					bool flag = asset.id == id;
					if (flag)
					{
						bool flag2 = onlyIfIsField && !asset.isField;
						if (flag2)
						{
							return;
						}
						entry.uxmlObjectAssets.RemoveAt(j);
						this.RemoveUxmlObjectEntryDependencies(asset.id);
						bool flag3 = entry.uxmlObjectAssets.Count == 0;
						if (flag3)
						{
							int index = this.m_UxmlObjectEntries.IndexOf(entry);
							this.m_UxmlObjectEntries.RemoveAt(index);
							this.m_UxmlObjectIds.RemoveAt(index);
							this.RemoveUxmlObject(entry.parentId, true);
						}
						return;
					}
					else
					{
						j++;
					}
				}
			}
		}

		// Token: 0x0600225C RID: 8796 RVA: 0x0007DE4C File Offset: 0x0007C04C
		private void RemoveUxmlObjectEntryDependencies(int parentId)
		{
			bool flag = this.m_UxmlObjectEntries.Count == 0;
			if (!flag)
			{
				List<VisualTreeAsset.UxmlObjectEntry> uxmlObjectRoots = CollectionPool<List<VisualTreeAsset.UxmlObjectEntry>, VisualTreeAsset.UxmlObjectEntry>.Get();
				foreach (VisualTreeAsset.UxmlObjectEntry child in this.m_UxmlObjectEntries)
				{
					bool flag2 = parentId == child.parentId;
					if (flag2)
					{
						uxmlObjectRoots.Add(child);
					}
				}
				foreach (VisualTreeAsset.UxmlObjectEntry entry in uxmlObjectRoots)
				{
					int index = this.m_UxmlObjectEntries.IndexOf(entry);
					this.m_UxmlObjectEntries.RemoveAt(index);
					this.m_UxmlObjectIds.RemoveAt(index);
					foreach (UxmlObjectAsset asset in entry.uxmlObjectAssets)
					{
						this.RemoveUxmlObjectEntryDependencies(asset.id);
					}
				}
				CollectionPool<List<VisualTreeAsset.UxmlObjectEntry>, VisualTreeAsset.UxmlObjectEntry>.Release(uxmlObjectRoots);
			}
		}

		// Token: 0x0600225D RID: 8797 RVA: 0x0007DF94 File Offset: 0x0007C194
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void CollectUxmlObjectAssets(UxmlAsset parent, string fieldName, List<UxmlObjectAsset> foundEntries)
		{
			bool flag = parent == null;
			if (!flag)
			{
				foreach (VisualTreeAsset.UxmlObjectEntry e in this.m_UxmlObjectEntries)
				{
					bool flag2 = e.parentId == parent.id;
					if (flag2)
					{
						bool flag3 = !string.IsNullOrEmpty(fieldName);
						if (flag3)
						{
							UxmlObjectAsset fieldAsset = e.GetField(fieldName);
							bool flag4 = fieldAsset != null;
							if (flag4)
							{
								this.CollectUxmlObjectAssets(fieldAsset, null, foundEntries);
							}
						}
						else
						{
							foreach (UxmlObjectAsset asset in e.uxmlObjectAssets)
							{
								bool flag5 = !asset.isField;
								if (flag5)
								{
									foundEntries.Add(asset);
								}
							}
						}
						break;
					}
				}
			}
		}

		// Token: 0x0600225E RID: 8798 RVA: 0x0007E0A4 File Offset: 0x0007C2A4
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void SetUxmlObjectAssets(UxmlAsset parent, string fieldName, List<UxmlObjectAsset> entries)
		{
			foreach (VisualTreeAsset.UxmlObjectEntry e in this.m_UxmlObjectEntries)
			{
				bool flag = e.parentId == parent.id;
				if (flag)
				{
					bool flag2 = !string.IsNullOrEmpty(fieldName);
					if (flag2)
					{
						UxmlObjectAsset fieldAsset = e.GetField(fieldName);
						bool flag3 = fieldAsset != null;
						if (flag3)
						{
							this.SetUxmlObjectAssets(fieldAsset, null, entries);
						}
					}
					else
					{
						for (int i = e.uxmlObjectAssets.Count - 1; i >= 0; i--)
						{
							bool flag4 = !e.uxmlObjectAssets[i].isField;
							if (flag4)
							{
								e.uxmlObjectAssets.RemoveAt(i);
							}
						}
						e.uxmlObjectAssets.AddRange(entries);
						bool flag5 = e.uxmlObjectAssets.Count == 0;
						if (flag5)
						{
							int index = this.m_UxmlObjectEntries.IndexOf(e);
							this.m_UxmlObjectEntries.RemoveAt(index);
							this.m_UxmlObjectIds.RemoveAt(index);
							this.RemoveUxmlObject(e.parentId, true);
						}
					}
					break;
				}
			}
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x0007E204 File Offset: 0x0007C404
		internal List<T> GetUxmlObjects<T>(IUxmlAttributes asset, CreationContext cc) where T : new()
		{
			UxmlAsset ua = asset as UxmlAsset;
			bool flag = ua != null;
			if (flag)
			{
				VisualTreeAsset.UxmlObjectEntry entry = this.GetUxmlObjectEntry(ua.id);
				bool flag2 = entry.uxmlObjectAssets != null;
				if (flag2)
				{
					List<T> uxmlObjects = null;
					foreach (UxmlObjectAsset uxmlObjectAsset in entry.uxmlObjectAssets)
					{
						IBaseUxmlObjectFactory factory = this.GetUxmlObjectFactory(uxmlObjectAsset);
						IUxmlObjectFactory<T> typedFactory = factory as IUxmlObjectFactory<T>;
						bool flag3 = typedFactory == null;
						if (!flag3)
						{
							T obj = typedFactory.CreateObject(uxmlObjectAsset, cc);
							bool flag4 = uxmlObjects == null;
							if (flag4)
							{
								uxmlObjects = new List<T> { obj };
							}
							else
							{
								uxmlObjects.Add(obj);
							}
						}
					}
					return uxmlObjects;
				}
			}
			return null;
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x0007E2F0 File Offset: 0x0007C4F0
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool AssetEntryExists(string path, Type type)
		{
			foreach (VisualTreeAsset.AssetEntry entry in this.m_AssetEntries)
			{
				bool flag = entry.path == path && entry.type == type;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002261 RID: 8801 RVA: 0x0007E370 File Offset: 0x0007C570
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void RegisterAssetEntry(string path, Type type, Object asset)
		{
			this.m_AssetEntries.Add(new VisualTreeAsset.AssetEntry(path, type, asset));
		}

		// Token: 0x06002262 RID: 8802 RVA: 0x0007E387 File Offset: 0x0007C587
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void TransferAssetEntries(VisualTreeAsset otherVta)
		{
			this.m_AssetEntries.Clear();
			this.m_AssetEntries.AddRange(otherVta.m_AssetEntries);
		}

		// Token: 0x06002263 RID: 8803 RVA: 0x0007E3A8 File Offset: 0x0007C5A8
		internal T GetAsset<T>(string path) where T : Object
		{
			return this.GetAsset(path, typeof(T)) as T;
		}

		// Token: 0x06002264 RID: 8804 RVA: 0x0007E3C8 File Offset: 0x0007C5C8
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal Object GetAsset(string path, Type type)
		{
			foreach (VisualTreeAsset.AssetEntry entry in this.m_AssetEntries)
			{
				bool flag = entry.path == path && type.IsAssignableFrom(entry.type);
				if (flag)
				{
					return entry.asset;
				}
			}
			return null;
		}

		// Token: 0x06002265 RID: 8805 RVA: 0x0007E44C File Offset: 0x0007C64C
		internal Type GetAssetType(string path)
		{
			foreach (VisualTreeAsset.AssetEntry entry in this.m_AssetEntries)
			{
				bool flag = entry.path == path;
				if (flag)
				{
					return entry.type;
				}
			}
			return null;
		}

		// Token: 0x06002266 RID: 8806 RVA: 0x0007E4C0 File Offset: 0x0007C6C0
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal VisualTreeAsset.UxmlObjectEntry GetUxmlObjectEntry(int id)
		{
			bool flag = this.m_UxmlObjectEntries != null;
			if (flag)
			{
				foreach (VisualTreeAsset.UxmlObjectEntry e in this.m_UxmlObjectEntries)
				{
					bool flag2 = e.parentId == id;
					if (flag2)
					{
						return e;
					}
				}
			}
			return default(VisualTreeAsset.UxmlObjectEntry);
		}

		// Token: 0x06002267 RID: 8807 RVA: 0x0007E544 File Offset: 0x0007C744
		internal IBaseUxmlObjectFactory GetUxmlObjectFactory(UxmlObjectAsset uxmlObjectAsset)
		{
			List<IBaseUxmlObjectFactory> factories;
			bool flag = !UxmlObjectFactoryRegistry.factories.TryGetValue(uxmlObjectAsset.fullTypeName, out factories);
			IBaseUxmlObjectFactory baseUxmlObjectFactory;
			if (flag)
			{
				Debug.LogErrorFormat("Element '{0}' has no registered factory method.", new object[] { uxmlObjectAsset.fullTypeName });
				baseUxmlObjectFactory = null;
			}
			else
			{
				IBaseUxmlObjectFactory factory = null;
				CreationContext ctx = new CreationContext(this);
				foreach (IBaseUxmlObjectFactory f in factories)
				{
					bool flag2 = f.AcceptsAttributeBag(uxmlObjectAsset, ctx);
					if (flag2)
					{
						factory = f;
						break;
					}
				}
				bool flag3 = factory == null;
				if (flag3)
				{
					Debug.LogErrorFormat("Element '{0}' has a no factory that accept the set of XML attributes specified.", new object[] { uxmlObjectAsset.fullTypeName });
					baseUxmlObjectFactory = null;
				}
				else
				{
					baseUxmlObjectFactory = factory;
				}
			}
			return baseUxmlObjectFactory;
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x0007E61C File Offset: 0x0007C81C
		internal List<VisualTreeAsset.SlotDefinition> slots
		{
			get
			{
				return this.m_Slots;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002269 RID: 8809 RVA: 0x0007E624 File Offset: 0x0007C824
		// (set) Token: 0x0600226A RID: 8810 RVA: 0x0007E63C File Offset: 0x0007C83C
		internal int contentContainerId
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_ContentContainerId;
			}
			set
			{
				this.m_ContentContainerId = value;
			}
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x0007E648 File Offset: 0x0007C848
		public TemplateContainer Instantiate()
		{
			TemplateContainer target = new TemplateContainer(base.name, this);
			try
			{
				CreationContext cc = new CreationContext(VisualTreeAsset.s_TemporarySlotInsertionPoints, null, null, null, null, VisualTreeAsset.s_VeaIdsPath, null);
				this.CloneTree(target, cc);
			}
			finally
			{
				VisualTreeAsset.s_TemporarySlotInsertionPoints.Clear();
				VisualTreeAsset.s_VeaIdsPath.Clear();
			}
			return target;
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x0007E6B4 File Offset: 0x0007C8B4
		public TemplateContainer Instantiate(string bindingPath)
		{
			TemplateContainer tc = this.Instantiate();
			tc.bindingPath = bindingPath;
			return tc;
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x0007E6D8 File Offset: 0x0007C8D8
		public TemplateContainer CloneTree()
		{
			return this.Instantiate();
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x0007E6F0 File Offset: 0x0007C8F0
		public TemplateContainer CloneTree(string bindingPath)
		{
			return this.Instantiate(bindingPath);
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x0007E70C File Offset: 0x0007C90C
		public void CloneTree(VisualElement target)
		{
			int num;
			int num2;
			this.CloneTree(target, out num, out num2);
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x0007E728 File Offset: 0x0007C928
		public void CloneTree(VisualElement target, out int firstElementIndex, out int elementAddedCount)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			firstElementIndex = target.childCount;
			try
			{
				CreationContext cc = new CreationContext(VisualTreeAsset.s_TemporarySlotInsertionPoints, null, null, null, null, VisualTreeAsset.s_VeaIdsPath, null);
				this.CloneTree(target, cc);
			}
			finally
			{
				elementAddedCount = target.childCount - firstElementIndex;
				VisualTreeAsset.s_TemporarySlotInsertionPoints.Clear();
				VisualTreeAsset.s_VeaIdsPath.Clear();
			}
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x0007E7A8 File Offset: 0x0007C9A8
		internal void CloneTree(VisualElement target, CreationContext cc)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			bool flag2 = (this.visualElementAssets == null || this.visualElementAssets.Count <= 0) && (this.templateAssets == null || this.templateAssets.Count <= 0);
			if (!flag2)
			{
				Dictionary<int, List<VisualElementAsset>> idToChildren = new Dictionary<int, List<VisualElementAsset>>();
				int eltcount = ((this.visualElementAssets == null) ? 0 : this.visualElementAssets.Count);
				int tplcount = ((this.templateAssets == null) ? 0 : this.templateAssets.Count);
				for (int i = 0; i < eltcount + tplcount; i++)
				{
					VisualElementAsset asset = ((i < eltcount) ? this.visualElementAssets[i] : this.templateAssets[i - eltcount]);
					List<VisualElementAsset> children;
					bool flag3 = !idToChildren.TryGetValue(asset.parentId, out children);
					if (flag3)
					{
						children = new List<VisualElementAsset>();
						idToChildren[asset.parentId] = children;
					}
					children.Add(asset);
				}
				List<VisualElementAsset> rootAssets;
				idToChildren.TryGetValue(0, out rootAssets);
				bool flag4 = rootAssets == null || rootAssets.Count == 0;
				if (!flag4)
				{
					Debug.Assert(rootAssets.Count == 1);
					VisualElementAsset root = rootAssets[0];
					VisualTreeAsset.AssignClassListFromAssetToElement(root, target);
					VisualTreeAsset.AssignStyleSheetFromAssetToElement(root, target);
					rootAssets.Clear();
					idToChildren.TryGetValue(root.id, out rootAssets);
					bool flag5 = rootAssets == null || rootAssets.Count == 0;
					if (!flag5)
					{
						rootAssets.Sort(new Comparison<VisualElementAsset>(VisualTreeAsset.CompareForOrder));
						foreach (VisualElementAsset rootElement in rootAssets)
						{
							Assert.IsNotNull<VisualElementAsset>(rootElement);
							bool isTemplate = false;
							bool flag6 = rootElement is TemplateAsset;
							if (flag6)
							{
								cc.veaIdsPath.Add(rootElement.id);
								isTemplate = true;
							}
							CreationContext newCc = new CreationContext(cc.slotInsertionPoints, cc.attributeOverrides, cc.serializedDataOverrides, this, target, cc.veaIdsPath, null);
							VisualElement rootVe = this.CloneSetupRecursively(rootElement, idToChildren, newCc);
							bool flag7 = isTemplate;
							if (flag7)
							{
								cc.veaIdsPath.Remove(rootElement.id);
							}
							bool flag8 = rootVe == null;
							if (!flag8)
							{
								rootVe.visualTreeAssetSource = this;
								target.hierarchy.Add(rootVe);
							}
						}
					}
				}
			}
		}

		// Token: 0x06002272 RID: 8818 RVA: 0x0007EA40 File Offset: 0x0007CC40
		private VisualElement CloneSetupRecursively(VisualElementAsset root, Dictionary<int, List<VisualElementAsset>> idToChildren, CreationContext context)
		{
			bool skipClone = root.skipClone;
			VisualElement visualElement;
			if (skipClone)
			{
				visualElement = null;
			}
			else
			{
				VisualElement ve = VisualTreeAsset.Create(root, context);
				bool flag = ve == null;
				if (flag)
				{
					visualElement = null;
				}
				else
				{
					bool flag2 = root.id == context.visualTreeAsset.contentContainerId;
					if (flag2)
					{
						TemplateContainer tc = context.target as TemplateContainer;
						bool flag3 = tc != null;
						if (flag3)
						{
							tc.SetContentContainer(ve);
						}
						else
						{
							Debug.LogError("Trying to clone a VisualTreeAsset with a custom content container into a element which is not a template container");
						}
					}
					string slotName;
					bool flag4 = context.slotInsertionPoints != null && this.TryGetSlotInsertionPoint(root.id, out slotName);
					if (flag4)
					{
						context.slotInsertionPoints.Add(slotName, ve);
					}
					bool flag5 = root.ruleIndex != -1;
					if (flag5)
					{
						bool flag6 = this.inlineSheet == null;
						if (flag6)
						{
							Debug.LogWarning("VisualElementAsset has a RuleIndex but no inlineStyleSheet");
						}
						else
						{
							StyleRule r = this.inlineSheet.rules[root.ruleIndex];
							ve.SetInlineRule(this.inlineSheet, r);
						}
					}
					TemplateAsset templateAsset = root as TemplateAsset;
					List<VisualElementAsset> children;
					bool flag7 = idToChildren.TryGetValue(root.id, out children);
					if (flag7)
					{
						children.Sort(new Comparison<VisualElementAsset>(VisualTreeAsset.CompareForOrder));
						using (List<VisualElementAsset>.Enumerator enumerator = children.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								VisualElementAsset childVea = enumerator.Current;
								bool isTemplate = false;
								bool flag8 = childVea is TemplateAsset;
								if (flag8)
								{
									context.veaIdsPath.Add(childVea.id);
									isTemplate = true;
								}
								VisualElement childVe = this.CloneSetupRecursively(childVea, idToChildren, context);
								bool flag9 = isTemplate;
								if (flag9)
								{
									context.veaIdsPath.Remove(childVea.id);
								}
								bool flag10 = childVe == null;
								if (!flag10)
								{
									bool flag11 = templateAsset == null;
									if (flag11)
									{
										ve.Add(childVe);
									}
									else
									{
										int index = ((templateAsset.slotUsages == null) ? (-1) : templateAsset.slotUsages.FindIndex((VisualTreeAsset.SlotUsageEntry u) => u.assetId == childVea.id));
										bool flag12 = index != -1;
										if (flag12)
										{
											string key = templateAsset.slotUsages[index].slotName;
											Assert.IsFalse(string.IsNullOrEmpty(key), "a lost name should not be null or empty, this probably points to an importer or serialization bug");
											VisualElement parentSlot;
											bool flag13 = context.slotInsertionPoints == null || !context.slotInsertionPoints.TryGetValue(key, out parentSlot);
											if (flag13)
											{
												Debug.LogErrorFormat("Slot '{0}' was not found. Existing slots: {1}", new object[]
												{
													key,
													(context.slotInsertionPoints == null) ? string.Empty : string.Join(", ", context.slotInsertionPoints.Keys.ToArray<string>())
												});
												ve.Add(childVe);
											}
											else
											{
												parentSlot.Add(childVe);
											}
										}
										else
										{
											ve.Add(childVe);
										}
									}
								}
							}
						}
					}
					bool flag14 = templateAsset != null && context.slotInsertionPoints != null;
					if (flag14)
					{
						context.slotInsertionPoints.Clear();
					}
					visualElement = ve;
				}
			}
			return visualElement;
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x0007ED88 File Offset: 0x0007CF88
		internal static int CompareForOrder(VisualElementAsset a, VisualElementAsset b)
		{
			return a.orderInDocument.CompareTo(b.orderInDocument);
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x0007EDAC File Offset: 0x0007CFAC
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool TryGetSlotInsertionPoint(int insertionPointId, out string slotName)
		{
			for (int index = 0; index < this.m_Slots.Count; index++)
			{
				VisualTreeAsset.SlotDefinition slotDefinition = this.m_Slots[index];
				bool flag = slotDefinition.insertionPointId == insertionPointId;
				if (flag)
				{
					slotName = slotDefinition.name;
					return true;
				}
			}
			slotName = null;
			return false;
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x0007EE08 File Offset: 0x0007D008
		internal bool TryGetUsingEntry(string templateName, out VisualTreeAsset.UsingEntry entry)
		{
			entry = default(VisualTreeAsset.UsingEntry);
			bool flag = this.m_Usings.Count == 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				int index = this.m_Usings.BinarySearch(new VisualTreeAsset.UsingEntry(templateName, string.Empty), VisualTreeAsset.UsingEntry.comparer);
				bool flag3 = index < 0;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					entry = this.m_Usings[index];
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x0007EE74 File Offset: 0x0007D074
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal VisualTreeAsset ResolveTemplate(string templateName)
		{
			VisualTreeAsset.UsingEntry entry;
			bool flag = !this.TryGetUsingEntry(templateName, out entry);
			VisualTreeAsset visualTreeAsset;
			if (flag)
			{
				visualTreeAsset = null;
			}
			else
			{
				bool flag2 = entry.asset;
				if (flag2)
				{
					visualTreeAsset = entry.asset;
				}
				else
				{
					string path = entry.path;
					visualTreeAsset = Panel.LoadResource(path, typeof(VisualTreeAsset), 1f) as VisualTreeAsset;
				}
			}
			return visualTreeAsset;
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x0007EED8 File Offset: 0x0007D0D8
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static VisualElement Create(VisualElementAsset asset, CreationContext ctx)
		{
			VisualTreeAsset.<>c__DisplayClass77_0 CS$<>8__locals1;
			CS$<>8__locals1.asset = asset;
			bool flag = CS$<>8__locals1.asset.serializedData != null;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = CS$<>8__locals1.asset.Instantiate(ctx);
			}
			else
			{
				List<IUxmlFactory> factoryList;
				bool flag2 = !VisualElementFactoryRegistry.TryGetValue(CS$<>8__locals1.asset.fullTypeName, out factoryList);
				if (flag2)
				{
					bool flag3 = CS$<>8__locals1.asset.fullTypeName.StartsWith("UnityEngine.Experimental.UIElements.") || CS$<>8__locals1.asset.fullTypeName.StartsWith("UnityEditor.Experimental.UIElements.");
					if (flag3)
					{
						string experimentalTypeName = CS$<>8__locals1.asset.fullTypeName.Replace(".Experimental.UIElements", ".UIElements");
						bool flag4 = !VisualElementFactoryRegistry.TryGetValue(experimentalTypeName, out factoryList);
						if (flag4)
						{
							return VisualTreeAsset.<Create>g__CreateError|77_0(ref CS$<>8__locals1);
						}
					}
					else
					{
						bool flag5 = CS$<>8__locals1.asset.fullTypeName == "UXML";
						if (!flag5)
						{
							return VisualTreeAsset.<Create>g__CreateError|77_0(ref CS$<>8__locals1);
						}
						VisualElementFactoryRegistry.TryGetValue(typeof(UxmlRootElementFactory).Namespace + "." + CS$<>8__locals1.asset.fullTypeName, out factoryList);
					}
				}
				IUxmlFactory factory = null;
				foreach (IUxmlFactory f in factoryList)
				{
					bool flag6 = f.AcceptsAttributeBag(CS$<>8__locals1.asset, ctx);
					if (flag6)
					{
						factory = f;
						break;
					}
				}
				bool flag7 = factory == null;
				if (flag7)
				{
					Debug.LogErrorFormat("Element '{0}' has a no factory that accept the set of XML attributes specified.", new object[] { CS$<>8__locals1.asset.fullTypeName });
					visualElement = new Label(string.Format("Type with no factory: '{0}'", CS$<>8__locals1.asset.fullTypeName));
				}
				else
				{
					VisualElement ve = factory.Create(CS$<>8__locals1.asset, ctx);
					bool flag8 = ve != null;
					if (flag8)
					{
						VisualTreeAsset.AssignClassListFromAssetToElement(CS$<>8__locals1.asset, ve);
						VisualTreeAsset.AssignStyleSheetFromAssetToElement(CS$<>8__locals1.asset, ve);
					}
					visualElement = ve;
				}
			}
			return visualElement;
		}

		// Token: 0x06002278 RID: 8824 RVA: 0x0007F0E8 File Offset: 0x0007D2E8
		private static void AssignClassListFromAssetToElement(VisualElementAsset asset, VisualElement element)
		{
			bool flag = asset.classes != null;
			if (flag)
			{
				for (int i = 0; i < asset.classes.Length; i++)
				{
					element.AddToClassList(asset.classes[i]);
				}
			}
		}

		// Token: 0x06002279 RID: 8825 RVA: 0x0007F130 File Offset: 0x0007D330
		private static void AssignStyleSheetFromAssetToElement(VisualElementAsset asset, VisualElement element)
		{
			bool hasStylesheetPaths = asset.hasStylesheetPaths;
			if (hasStylesheetPaths)
			{
				for (int i = 0; i < asset.stylesheetPaths.Count; i++)
				{
					element.AddStyleSheetPath(asset.stylesheetPaths[i]);
				}
			}
			bool hasStylesheets = asset.hasStylesheets;
			if (hasStylesheets)
			{
				for (int j = 0; j < asset.stylesheets.Count; j++)
				{
					bool flag = asset.stylesheets[j] != null;
					if (flag)
					{
						element.styleSheets.Add(asset.stylesheets[j]);
					}
				}
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x0600227A RID: 8826 RVA: 0x0007F1E0 File Offset: 0x0007D3E0
		// (set) Token: 0x0600227B RID: 8827 RVA: 0x0007F1F8 File Offset: 0x0007D3F8
		public int contentHash
		{
			get
			{
				return this.m_ContentHash;
			}
			set
			{
				this.m_ContentHash = value;
			}
		}

		// Token: 0x0600227E RID: 8830 RVA: 0x0007F290 File Offset: 0x0007D490
		[CompilerGenerated]
		internal static VisualElement <Create>g__CreateError|77_0(ref VisualTreeAsset.<>c__DisplayClass77_0 A_0)
		{
			Debug.LogErrorFormat(VisualTreeAsset.NoRegisteredFactoryErrorMessage, new object[] { A_0.asset.fullTypeName });
			return new Label(string.Format("Unknown type: '{0}'", A_0.asset.fullTypeName));
		}

		// Token: 0x04000F2C RID: 3884
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static string LinkedVEAInTemplatePropertyName = "--unity-linked-vea-in-template";

		// Token: 0x04000F2D RID: 3885
		internal static string NoRegisteredFactoryErrorMessage = "Element '{0}' is missing a UxmlElementAttribute and has no registered factory method. Please ensure that you have the correct namespace imported.";

		// Token: 0x04000F2E RID: 3886
		[SerializeField]
		private bool m_ImportedWithErrors;

		// Token: 0x04000F2F RID: 3887
		[SerializeField]
		private bool m_ImportedWithWarnings;

		// Token: 0x04000F30 RID: 3888
		private static readonly Dictionary<string, VisualElement> s_TemporarySlotInsertionPoints = new Dictionary<string, VisualElement>();

		// Token: 0x04000F31 RID: 3889
		private static readonly List<int> s_VeaIdsPath = new List<int>();

		// Token: 0x04000F32 RID: 3890
		[SerializeField]
		private List<VisualTreeAsset.UsingEntry> m_Usings = new List<VisualTreeAsset.UsingEntry>();

		// Token: 0x04000F33 RID: 3891
		[SerializeField]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal StyleSheet inlineSheet;

		// Token: 0x04000F34 RID: 3892
		[SerializeField]
		internal List<VisualElementAsset> m_VisualElementAssets = new List<VisualElementAsset>();

		// Token: 0x04000F35 RID: 3893
		[SerializeField]
		internal List<TemplateAsset> m_TemplateAssets = new List<TemplateAsset>();

		// Token: 0x04000F36 RID: 3894
		[SerializeField]
		private List<VisualTreeAsset.UxmlObjectEntry> m_UxmlObjectEntries = new List<VisualTreeAsset.UxmlObjectEntry>();

		// Token: 0x04000F37 RID: 3895
		[SerializeField]
		private List<int> m_UxmlObjectIds = new List<int>();

		// Token: 0x04000F38 RID: 3896
		[SerializeField]
		private List<VisualTreeAsset.AssetEntry> m_AssetEntries = new List<VisualTreeAsset.AssetEntry>();

		// Token: 0x04000F39 RID: 3897
		[SerializeField]
		private List<VisualTreeAsset.SlotDefinition> m_Slots = new List<VisualTreeAsset.SlotDefinition>();

		// Token: 0x04000F3A RID: 3898
		[SerializeField]
		private int m_ContentContainerId;

		// Token: 0x04000F3B RID: 3899
		[SerializeField]
		private int m_ContentHash;

		// Token: 0x020004B4 RID: 1204
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[Serializable]
		internal struct UsingEntry
		{
			// Token: 0x0600227F RID: 8831 RVA: 0x0007F2DB File Offset: 0x0007D4DB
			public UsingEntry(string alias, string path)
			{
				this.alias = alias;
				this.path = path;
				this.asset = null;
			}

			// Token: 0x04000F3C RID: 3900
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal static readonly IComparer<VisualTreeAsset.UsingEntry> comparer = new VisualTreeAsset.UsingEntryComparer();

			// Token: 0x04000F3D RID: 3901
			[SerializeField]
			public string alias;

			// Token: 0x04000F3E RID: 3902
			[SerializeField]
			public string path;

			// Token: 0x04000F3F RID: 3903
			[SerializeField]
			public VisualTreeAsset asset;
		}

		// Token: 0x020004B5 RID: 1205
		private class UsingEntryComparer : IComparer<VisualTreeAsset.UsingEntry>
		{
			// Token: 0x06002281 RID: 8833 RVA: 0x0007F300 File Offset: 0x0007D500
			public int Compare(VisualTreeAsset.UsingEntry x, VisualTreeAsset.UsingEntry y)
			{
				return string.CompareOrdinal(x.alias, y.alias);
			}
		}

		// Token: 0x020004B6 RID: 1206
		[Serializable]
		internal struct SlotDefinition
		{
			// Token: 0x04000F40 RID: 3904
			[SerializeField]
			public string name;

			// Token: 0x04000F41 RID: 3905
			[SerializeField]
			public int insertionPointId;
		}

		// Token: 0x020004B7 RID: 1207
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[Serializable]
		internal struct SlotUsageEntry
		{
			// Token: 0x04000F42 RID: 3906
			[SerializeField]
			public string slotName;

			// Token: 0x04000F43 RID: 3907
			[SerializeField]
			public int assetId;
		}

		// Token: 0x020004B8 RID: 1208
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[Serializable]
		internal struct UxmlObjectEntry
		{
			// Token: 0x06002283 RID: 8835 RVA: 0x0007F323 File Offset: 0x0007D523
			public UxmlObjectEntry(int parentId, List<UxmlObjectAsset> uxmlObjectAssets)
			{
				this.parentId = parentId;
				this.uxmlObjectAssets = uxmlObjectAssets;
			}

			// Token: 0x06002284 RID: 8836 RVA: 0x0007F334 File Offset: 0x0007D534
			public UxmlObjectAsset GetField(string fieldName)
			{
				foreach (UxmlObjectAsset asset in this.uxmlObjectAssets)
				{
					bool flag = asset.isField && asset.fullTypeName == fieldName;
					if (flag)
					{
						return asset;
					}
				}
				return null;
			}

			// Token: 0x06002285 RID: 8837 RVA: 0x0007F3AC File Offset: 0x0007D5AC
			public override string ToString()
			{
				string text = "UxmlObjectEntry parent:{0} ({1})";
				object obj = this.parentId;
				List<UxmlObjectAsset> list = this.uxmlObjectAssets;
				return string.Format(text, obj, (list != null) ? new int?(list.Count) : null);
			}

			// Token: 0x04000F44 RID: 3908
			[SerializeField]
			public int parentId;

			// Token: 0x04000F45 RID: 3909
			[SerializeField]
			public List<UxmlObjectAsset> uxmlObjectAssets;
		}

		// Token: 0x020004B9 RID: 1209
		[Serializable]
		private struct AssetEntry
		{
			// Token: 0x1700092B RID: 2347
			// (get) Token: 0x06002286 RID: 8838 RVA: 0x0007F3F4 File Offset: 0x0007D5F4
			public Type type
			{
				get
				{
					Type type;
					if ((type = this.m_CachedType) == null)
					{
						type = (this.m_CachedType = Type.GetType(this.m_TypeFullName));
					}
					return type;
				}
			}

			// Token: 0x1700092C RID: 2348
			// (get) Token: 0x06002287 RID: 8839 RVA: 0x0007F41F File Offset: 0x0007D61F
			public string path
			{
				get
				{
					return this.m_Path;
				}
			}

			// Token: 0x1700092D RID: 2349
			// (get) Token: 0x06002288 RID: 8840 RVA: 0x0007F428 File Offset: 0x0007D628
			public Object asset
			{
				get
				{
					bool isSet = this.m_AssetReference.isSet;
					Object @object;
					if (isSet)
					{
						@object = this.m_AssetReference.asset;
					}
					else
					{
						@object = null;
					}
					return @object;
				}
			}

			// Token: 0x06002289 RID: 8841 RVA: 0x0007F459 File Offset: 0x0007D659
			public AssetEntry(string path, Type type, Object asset)
			{
				this.m_Path = path;
				this.m_TypeFullName = type.AssemblyQualifiedName;
				this.m_CachedType = type;
				this.m_AssetReference = asset;
				this.m_InstanceID = ((asset != null) ? asset.GetInstanceID() : 0);
			}

			// Token: 0x04000F46 RID: 3910
			[SerializeField]
			private string m_Path;

			// Token: 0x04000F47 RID: 3911
			[SerializeField]
			private string m_TypeFullName;

			// Token: 0x04000F48 RID: 3912
			[SerializeField]
			private LazyLoadReference<Object> m_AssetReference;

			// Token: 0x04000F49 RID: 3913
			[SerializeField]
			private int m_InstanceID;

			// Token: 0x04000F4A RID: 3914
			private Type m_CachedType;
		}
	}
}
