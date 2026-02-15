using System;
using System.Collections.Generic;

namespace Spine
{
	// Token: 0x0200007D RID: 125
	public class Skin
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0001B5B7 File Offset: 0x000197B7
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0001B5BF File Offset: 0x000197BF
		public ICollection<Skin.SkinEntry> Attachments
		{
			get
			{
				return this.attachments.Values;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0001B5CC File Offset: 0x000197CC
		public ExposedList<BoneData> Bones
		{
			get
			{
				return this.bones;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0001B5D4 File Offset: 0x000197D4
		public ExposedList<ConstraintData> Constraints
		{
			get
			{
				return this.constraints;
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001B5DC File Offset: 0x000197DC
		public Skin(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name", "name cannot be null.");
			}
			this.name = name;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001B62F File Offset: 0x0001982F
		public void SetAttachment(int slotIndex, string name, Attachment attachment)
		{
			if (attachment == null)
			{
				throw new ArgumentNullException("attachment", "attachment cannot be null.");
			}
			this.attachments[new Skin.SkinKey(slotIndex, name)] = new Skin.SkinEntry(slotIndex, name, attachment);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001B660 File Offset: 0x00019860
		public void AddSkin(Skin skin)
		{
			foreach (BoneData data in skin.bones)
			{
				if (!this.bones.Contains(data))
				{
					this.bones.Add(data);
				}
			}
			foreach (ConstraintData data2 in skin.constraints)
			{
				if (!this.constraints.Contains(data2))
				{
					this.constraints.Add(data2);
				}
			}
			foreach (KeyValuePair<Skin.SkinKey, Skin.SkinEntry> item in skin.attachments)
			{
				Skin.SkinEntry entry = item.Value;
				this.SetAttachment(entry.slotIndex, entry.name, entry.attachment);
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001B77C File Offset: 0x0001997C
		public void CopySkin(Skin skin)
		{
			foreach (BoneData data in skin.bones)
			{
				if (!this.bones.Contains(data))
				{
					this.bones.Add(data);
				}
			}
			foreach (ConstraintData data2 in skin.constraints)
			{
				if (!this.constraints.Contains(data2))
				{
					this.constraints.Add(data2);
				}
			}
			foreach (KeyValuePair<Skin.SkinKey, Skin.SkinEntry> item in skin.attachments)
			{
				Skin.SkinEntry entry = item.Value;
				if (entry.attachment is MeshAttachment)
				{
					this.SetAttachment(entry.slotIndex, entry.name, (entry.attachment != null) ? ((MeshAttachment)entry.attachment).NewLinkedMesh() : null);
				}
				else
				{
					this.SetAttachment(entry.slotIndex, entry.name, (entry.attachment != null) ? entry.attachment.Copy() : null);
				}
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001B8F0 File Offset: 0x00019AF0
		public Attachment GetAttachment(int slotIndex, string name)
		{
			Skin.SkinEntry entry;
			if (!this.attachments.TryGetValue(new Skin.SkinKey(slotIndex, name), out entry))
			{
				return null;
			}
			return entry.attachment;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001B91B File Offset: 0x00019B1B
		public void RemoveAttachment(int slotIndex, string name)
		{
			this.attachments.Remove(new Skin.SkinKey(slotIndex, name));
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001B930 File Offset: 0x00019B30
		public void GetAttachments(int slotIndex, List<Skin.SkinEntry> attachments)
		{
			if (slotIndex < 0)
			{
				throw new ArgumentException("slotIndex must be >= 0.");
			}
			if (attachments == null)
			{
				throw new ArgumentNullException("attachments", "attachments cannot be null.");
			}
			foreach (KeyValuePair<Skin.SkinKey, Skin.SkinEntry> item in this.attachments)
			{
				Skin.SkinEntry entry = item.Value;
				if (entry.slotIndex == slotIndex)
				{
					attachments.Add(entry);
				}
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001B9B8 File Offset: 0x00019BB8
		public void Clear()
		{
			this.attachments.Clear();
			this.bones.Clear(true);
			this.constraints.Clear(true);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001B5B7 File Offset: 0x000197B7
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001B9E0 File Offset: 0x00019BE0
		internal void AttachAll(Skeleton skeleton, Skin oldSkin)
		{
			Slot[] slots = skeleton.slots.Items;
			foreach (KeyValuePair<Skin.SkinKey, Skin.SkinEntry> item in oldSkin.attachments)
			{
				Skin.SkinEntry entry = item.Value;
				int slotIndex = entry.slotIndex;
				Slot slot = slots[slotIndex];
				if (slot.Attachment == entry.attachment)
				{
					Attachment attachment = this.GetAttachment(slotIndex, entry.name);
					if (attachment != null)
					{
						slot.Attachment = attachment;
					}
				}
			}
		}

		// Token: 0x040002B4 RID: 692
		internal string name;

		// Token: 0x040002B5 RID: 693
		private Dictionary<Skin.SkinKey, Skin.SkinEntry> attachments = new Dictionary<Skin.SkinKey, Skin.SkinEntry>(Skin.SkinKeyComparer.Instance);

		// Token: 0x040002B6 RID: 694
		internal readonly ExposedList<BoneData> bones = new ExposedList<BoneData>();

		// Token: 0x040002B7 RID: 695
		internal readonly ExposedList<ConstraintData> constraints = new ExposedList<ConstraintData>();

		// Token: 0x0200007E RID: 126
		public struct SkinEntry
		{
			// Token: 0x060004C2 RID: 1218 RVA: 0x0001BA7C File Offset: 0x00019C7C
			public SkinEntry(int slotIndex, string name, Attachment attachment)
			{
				this.slotIndex = slotIndex;
				this.name = name;
				this.attachment = attachment;
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0001BA93 File Offset: 0x00019C93
			public int SlotIndex
			{
				get
				{
					return this.slotIndex;
				}
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x060004C4 RID: 1220 RVA: 0x0001BA9B File Offset: 0x00019C9B
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0001BAA3 File Offset: 0x00019CA3
			public Attachment Attachment
			{
				get
				{
					return this.attachment;
				}
			}

			// Token: 0x040002B8 RID: 696
			internal readonly int slotIndex;

			// Token: 0x040002B9 RID: 697
			internal readonly string name;

			// Token: 0x040002BA RID: 698
			internal readonly Attachment attachment;
		}

		// Token: 0x0200007F RID: 127
		private struct SkinKey
		{
			// Token: 0x060004C6 RID: 1222 RVA: 0x0001BAAC File Offset: 0x00019CAC
			public SkinKey(int slotIndex, string name)
			{
				if (slotIndex < 0)
				{
					throw new ArgumentException("slotIndex must be >= 0.");
				}
				if (name == null)
				{
					throw new ArgumentNullException("name", "name cannot be null");
				}
				this.slotIndex = slotIndex;
				this.name = name;
				this.hashCode = name.GetHashCode() + slotIndex * 37;
			}

			// Token: 0x040002BB RID: 699
			internal readonly int slotIndex;

			// Token: 0x040002BC RID: 700
			internal readonly string name;

			// Token: 0x040002BD RID: 701
			internal readonly int hashCode;
		}

		// Token: 0x02000080 RID: 128
		private class SkinKeyComparer : IEqualityComparer<Skin.SkinKey>
		{
			// Token: 0x060004C7 RID: 1223 RVA: 0x0001BAFA File Offset: 0x00019CFA
			bool IEqualityComparer<Skin.SkinKey>.Equals(Skin.SkinKey e1, Skin.SkinKey e2)
			{
				return e1.slotIndex == e2.slotIndex && string.Equals(e1.name, e2.name, StringComparison.Ordinal);
			}

			// Token: 0x060004C8 RID: 1224 RVA: 0x0001BB1E File Offset: 0x00019D1E
			int IEqualityComparer<Skin.SkinKey>.GetHashCode(Skin.SkinKey e)
			{
				return e.hashCode;
			}

			// Token: 0x040002BE RID: 702
			internal static readonly Skin.SkinKeyComparer Instance = new Skin.SkinKeyComparer();
		}
	}
}
