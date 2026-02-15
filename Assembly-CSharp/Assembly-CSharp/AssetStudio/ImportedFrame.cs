using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x02000151 RID: 337
	public class ImportedFrame
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00015173 File Offset: 0x00013373
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x0001517B File Offset: 0x0001337B
		public string Name { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00015184 File Offset: 0x00013384
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x0001518C File Offset: 0x0001338C
		public Vector3 LocalRotation { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00015195 File Offset: 0x00013395
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x0001519D File Offset: 0x0001339D
		public Vector3 LocalPosition { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x000151A6 File Offset: 0x000133A6
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x000151AE File Offset: 0x000133AE
		public Vector3 LocalScale { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x000151B7 File Offset: 0x000133B7
		// (set) Token: 0x060003DA RID: 986 RVA: 0x000151BF File Offset: 0x000133BF
		public ImportedFrame Parent { get; set; }

		// Token: 0x17000040 RID: 64
		public ImportedFrame this[int i]
		{
			get
			{
				return this.children[i];
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060003DC RID: 988 RVA: 0x000151D6 File Offset: 0x000133D6
		public int Count
		{
			get
			{
				return this.children.Count;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060003DD RID: 989 RVA: 0x000151E4 File Offset: 0x000133E4
		public string Path
		{
			get
			{
				ImportedFrame frame = this;
				string path = frame.Name;
				while (frame.Parent != null)
				{
					frame = frame.Parent;
					path = frame.Name + "/" + path;
				}
				return path;
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001521E File Offset: 0x0001341E
		public ImportedFrame(int childrenCount = 0)
		{
			this.children = new List<ImportedFrame>(childrenCount);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00015232 File Offset: 0x00013432
		public void AddChild(ImportedFrame obj)
		{
			this.children.Add(obj);
			ImportedFrame parent = obj.Parent;
			if (parent != null)
			{
				parent.Remove(obj);
			}
			obj.Parent = this;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00015259 File Offset: 0x00013459
		public void Remove(ImportedFrame frame)
		{
			this.children.Remove(frame);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00015268 File Offset: 0x00013468
		public ImportedFrame FindFrameByPath(string path)
		{
			string name = path.Substring(path.LastIndexOf('/') + 1);
			foreach (ImportedFrame frame in this.FindChilds(name))
			{
				if (frame.Path.EndsWith(path, StringComparison.Ordinal))
				{
					return frame;
				}
			}
			return null;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000152D8 File Offset: 0x000134D8
		public ImportedFrame FindRelativeFrameWithPath(string path)
		{
			string[] subs = path.Split(new char[] { '/' }, 2);
			foreach (ImportedFrame child in this.children)
			{
				if (child.Name == subs[0])
				{
					if (subs.Length == 1)
					{
						return child;
					}
					ImportedFrame result = child.FindRelativeFrameWithPath(subs[1]);
					if (result != null)
					{
						return result;
					}
				}
			}
			return null;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00015368 File Offset: 0x00013568
		public ImportedFrame FindFrame(string name)
		{
			if (this.Name == name)
			{
				return this;
			}
			foreach (ImportedFrame importedFrame in this.children)
			{
				ImportedFrame frame = importedFrame.FindFrame(name);
				if (frame != null)
				{
					return frame;
				}
			}
			return null;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000153D4 File Offset: 0x000135D4
		public ImportedFrame FindChild(string name, bool recursive = true)
		{
			foreach (ImportedFrame child in this.children)
			{
				if (recursive)
				{
					ImportedFrame frame = child.FindFrame(name);
					if (frame != null)
					{
						return frame;
					}
				}
				else if (child.Name == name)
				{
					return child;
				}
			}
			return null;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00015448 File Offset: 0x00013648
		public IEnumerable<ImportedFrame> FindChilds(string name)
		{
			if (this.Name == name)
			{
				yield return this;
			}
			foreach (ImportedFrame child in this.children)
			{
				foreach (ImportedFrame item in child.FindChilds(name))
				{
					yield return item;
				}
				IEnumerator<ImportedFrame> enumerator2 = null;
			}
			List<ImportedFrame>.Enumerator enumerator = default(List<ImportedFrame>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x0400093A RID: 2362
		private List<ImportedFrame> children;
	}
}
