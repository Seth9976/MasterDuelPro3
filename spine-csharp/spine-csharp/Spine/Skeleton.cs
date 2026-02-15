using System;

namespace Spine
{
	// Token: 0x02000070 RID: 112
	public class Skeleton
	{
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00010A2D File Offset: 0x0000EC2D
		public SkeletonData Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00010A35 File Offset: 0x0000EC35
		public ExposedList<Bone> Bones
		{
			get
			{
				return this.bones;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00010A3D File Offset: 0x0000EC3D
		public ExposedList<IUpdatable> UpdateCacheList
		{
			get
			{
				return this.updateCache;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00010A45 File Offset: 0x0000EC45
		public ExposedList<Slot> Slots
		{
			get
			{
				return this.slots;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00010A4D File Offset: 0x0000EC4D
		public ExposedList<Slot> DrawOrder
		{
			get
			{
				return this.drawOrder;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00010A55 File Offset: 0x0000EC55
		public ExposedList<IkConstraint> IkConstraints
		{
			get
			{
				return this.ikConstraints;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00010A5D File Offset: 0x0000EC5D
		public ExposedList<PathConstraint> PathConstraints
		{
			get
			{
				return this.pathConstraints;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00010A65 File Offset: 0x0000EC65
		public ExposedList<PhysicsConstraint> PhysicsConstraints
		{
			get
			{
				return this.physicsConstraints;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00010A6D File Offset: 0x0000EC6D
		public ExposedList<TransformConstraint> TransformConstraints
		{
			get
			{
				return this.transformConstraints;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00010A75 File Offset: 0x0000EC75
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x00010A7D File Offset: 0x0000EC7D
		public Skin Skin
		{
			get
			{
				return this.skin;
			}
			set
			{
				this.SetSkin(value);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00010A86 File Offset: 0x0000EC86
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x00010A8E File Offset: 0x0000EC8E
		public float R
		{
			get
			{
				return this.r;
			}
			set
			{
				this.r = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00010A97 File Offset: 0x0000EC97
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x00010A9F File Offset: 0x0000EC9F
		public float G
		{
			get
			{
				return this.g;
			}
			set
			{
				this.g = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00010AA8 File Offset: 0x0000ECA8
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x00010AB0 File Offset: 0x0000ECB0
		public float B
		{
			get
			{
				return this.b;
			}
			set
			{
				this.b = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00010AB9 File Offset: 0x0000ECB9
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x00010AC1 File Offset: 0x0000ECC1
		public float A
		{
			get
			{
				return this.a;
			}
			set
			{
				this.a = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00010ACA File Offset: 0x0000ECCA
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x00010AD2 File Offset: 0x0000ECD2
		public float X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00010ADB File Offset: 0x0000ECDB
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x00010AE3 File Offset: 0x0000ECE3
		public float Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x00010AEC File Offset: 0x0000ECEC
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x00010AF4 File Offset: 0x0000ECF4
		public float ScaleX
		{
			get
			{
				return this.scaleX;
			}
			set
			{
				this.scaleX = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00010AFD File Offset: 0x0000ECFD
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x00010B12 File Offset: 0x0000ED12
		public float ScaleY
		{
			get
			{
				return this.scaleY * (float)(Bone.yDown ? (-1) : 1);
			}
			set
			{
				this.scaleY = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00010B1B File Offset: 0x0000ED1B
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x00010B2A File Offset: 0x0000ED2A
		[Obsolete("Use ScaleX instead. FlipX is when ScaleX is negative.")]
		public bool FlipX
		{
			get
			{
				return this.scaleX < 0f;
			}
			set
			{
				this.scaleX = (value ? (-1f) : 1f);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00010B41 File Offset: 0x0000ED41
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x00010B50 File Offset: 0x0000ED50
		[Obsolete("Use ScaleY instead. FlipY is when ScaleY is negative.")]
		public bool FlipY
		{
			get
			{
				return this.scaleY < 0f;
			}
			set
			{
				this.scaleY = (value ? (-1f) : 1f);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00010B67 File Offset: 0x0000ED67
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x00010B6F File Offset: 0x0000ED6F
		public float Time
		{
			get
			{
				return this.time;
			}
			set
			{
				this.time = value;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00010B78 File Offset: 0x0000ED78
		public Bone RootBone
		{
			get
			{
				if (this.bones.Count != 0)
				{
					return this.bones.Items[0];
				}
				return null;
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00010B98 File Offset: 0x0000ED98
		public Skeleton(SkeletonData data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data", "data cannot be null.");
			}
			this.data = data;
			this.bones = new ExposedList<Bone>(data.bones.Count);
			Bone[] bonesItems = this.bones.Items;
			foreach (BoneData boneData in data.bones)
			{
				Bone bone;
				if (boneData.parent == null)
				{
					bone = new Bone(boneData, this, null);
				}
				else
				{
					Bone parent = bonesItems[boneData.parent.index];
					bone = new Bone(boneData, this, parent);
					parent.children.Add(bone);
				}
				this.bones.Add(bone);
			}
			this.slots = new ExposedList<Slot>(data.slots.Count);
			this.drawOrder = new ExposedList<Slot>(data.slots.Count);
			foreach (SlotData slotData in data.slots)
			{
				Bone bone2 = bonesItems[slotData.boneData.index];
				Slot slot = new Slot(slotData, bone2);
				this.slots.Add(slot);
				this.drawOrder.Add(slot);
			}
			this.ikConstraints = new ExposedList<IkConstraint>(data.ikConstraints.Count);
			foreach (IkConstraintData ikConstraintData in data.ikConstraints)
			{
				this.ikConstraints.Add(new IkConstraint(ikConstraintData, this));
			}
			this.transformConstraints = new ExposedList<TransformConstraint>(data.transformConstraints.Count);
			foreach (TransformConstraintData transformConstraintData in data.transformConstraints)
			{
				this.transformConstraints.Add(new TransformConstraint(transformConstraintData, this));
			}
			this.pathConstraints = new ExposedList<PathConstraint>(data.pathConstraints.Count);
			foreach (PathConstraintData pathConstraintData in data.pathConstraints)
			{
				this.pathConstraints.Add(new PathConstraint(pathConstraintData, this));
			}
			this.physicsConstraints = new ExposedList<PhysicsConstraint>(data.physicsConstraints.Count);
			foreach (PhysicsConstraintData physicsConstraintData in data.physicsConstraints)
			{
				this.physicsConstraints.Add(new PhysicsConstraint(physicsConstraintData, this));
			}
			this.UpdateCache();
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00010EF4 File Offset: 0x0000F0F4
		public Skeleton(Skeleton skeleton)
		{
			if (skeleton == null)
			{
				throw new ArgumentNullException("skeleton", "skeleton cannot be null.");
			}
			this.data = skeleton.data;
			this.bones = new ExposedList<Bone>(skeleton.bones.Count);
			foreach (Bone bone in skeleton.bones)
			{
				Bone newBone;
				if (bone.parent == null)
				{
					newBone = new Bone(bone, this, null);
				}
				else
				{
					Bone parent = this.bones.Items[bone.parent.data.index];
					newBone = new Bone(bone, this, parent);
					parent.children.Add(newBone);
				}
				this.bones.Add(newBone);
			}
			this.slots = new ExposedList<Slot>(skeleton.slots.Count);
			Bone[] bonesItems = this.bones.Items;
			foreach (Slot slot in skeleton.slots)
			{
				Bone bone2 = bonesItems[slot.bone.data.index];
				this.slots.Add(new Slot(slot, bone2));
			}
			this.drawOrder = new ExposedList<Slot>(this.slots.Count);
			Slot[] slotsItems = this.slots.Items;
			foreach (Slot slot2 in skeleton.drawOrder)
			{
				this.drawOrder.Add(slotsItems[slot2.data.index]);
			}
			this.ikConstraints = new ExposedList<IkConstraint>(skeleton.ikConstraints.Count);
			foreach (IkConstraint ikConstraint in skeleton.ikConstraints)
			{
				this.ikConstraints.Add(new IkConstraint(ikConstraint, skeleton));
			}
			this.transformConstraints = new ExposedList<TransformConstraint>(skeleton.transformConstraints.Count);
			foreach (TransformConstraint transformConstraint in skeleton.transformConstraints)
			{
				this.transformConstraints.Add(new TransformConstraint(transformConstraint, skeleton));
			}
			this.pathConstraints = new ExposedList<PathConstraint>(skeleton.pathConstraints.Count);
			foreach (PathConstraint pathConstraint in skeleton.pathConstraints)
			{
				this.pathConstraints.Add(new PathConstraint(pathConstraint, skeleton));
			}
			this.physicsConstraints = new ExposedList<PhysicsConstraint>(skeleton.physicsConstraints.Count);
			foreach (PhysicsConstraint physicsConstraint in skeleton.physicsConstraints)
			{
				this.physicsConstraints.Add(new PhysicsConstraint(physicsConstraint, skeleton));
			}
			this.skin = skeleton.skin;
			this.r = skeleton.r;
			this.g = skeleton.g;
			this.b = skeleton.b;
			this.a = skeleton.a;
			this.x = skeleton.x;
			this.y = skeleton.y;
			this.scaleX = skeleton.scaleX;
			this.scaleY = skeleton.scaleY;
			this.time = skeleton.time;
			this.UpdateCache();
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00011338 File Offset: 0x0000F538
		public void UpdateCache()
		{
			this.updateCache.Clear(true);
			int boneCount = this.bones.Count;
			Bone[] bones = this.bones.Items;
			for (int i = 0; i < boneCount; i++)
			{
				Bone bone2 = bones[i];
				bone2.sorted = bone2.data.skinRequired;
				bone2.active = !bone2.sorted;
			}
			if (this.skin != null)
			{
				BoneData[] skinBones = this.skin.bones.Items;
				int j = 0;
				int k = this.skin.bones.Count;
				while (j < k)
				{
					Bone bone = bones[skinBones[j].index];
					do
					{
						bone.sorted = false;
						bone.active = true;
						bone = bone.parent;
					}
					while (bone != null);
					j++;
				}
			}
			int ikCount = this.ikConstraints.Count;
			int transformCount = this.transformConstraints.Count;
			int pathCount = this.pathConstraints.Count;
			int physicsCount = this.physicsConstraints.Count;
			IkConstraint[] ikConstraints = this.ikConstraints.Items;
			TransformConstraint[] transformConstraints = this.transformConstraints.Items;
			PathConstraint[] pathConstraints = this.pathConstraints.Items;
			PhysicsConstraint[] physicsConstraints = this.physicsConstraints.Items;
			int constraintCount = ikCount + transformCount + pathCount + physicsCount;
			int l = 0;
			IL_0207:
			while (l < constraintCount)
			{
				for (int ii = 0; ii < ikCount; ii++)
				{
					IkConstraint constraint = ikConstraints[ii];
					if (constraint.data.order == l)
					{
						this.SortIkConstraint(constraint);
						IL_0201:
						l++;
						goto IL_0207;
					}
				}
				for (int ii2 = 0; ii2 < transformCount; ii2++)
				{
					TransformConstraint constraint2 = transformConstraints[ii2];
					if (constraint2.data.order == l)
					{
						this.SortTransformConstraint(constraint2);
						goto IL_0201;
					}
				}
				for (int ii3 = 0; ii3 < pathCount; ii3++)
				{
					PathConstraint constraint3 = pathConstraints[ii3];
					if (constraint3.data.order == l)
					{
						this.SortPathConstraint(constraint3);
						goto IL_0201;
					}
				}
				for (int ii4 = 0; ii4 < physicsCount; ii4++)
				{
					PhysicsConstraint constraint4 = physicsConstraints[ii4];
					if (constraint4.data.order == l)
					{
						this.SortPhysicsConstraint(constraint4);
						break;
					}
				}
				goto IL_0201;
			}
			for (int m = 0; m < boneCount; m++)
			{
				this.SortBone(bones[m]);
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00011570 File Offset: 0x0000F770
		private void SortIkConstraint(IkConstraint constraint)
		{
			constraint.active = constraint.target.active && (!constraint.data.skinRequired || (this.skin != null && this.skin.constraints.Contains(constraint.data)));
			if (!constraint.active)
			{
				return;
			}
			Bone target = constraint.target;
			this.SortBone(target);
			ExposedList<Bone> constrained = constraint.bones;
			Bone parent = constrained.Items[0];
			this.SortBone(parent);
			if (constrained.Count == 1)
			{
				this.updateCache.Add(constraint);
				Skeleton.SortReset(parent.children);
				return;
			}
			Bone child = constrained.Items[constrained.Count - 1];
			this.SortBone(child);
			this.updateCache.Add(constraint);
			Skeleton.SortReset(parent.children);
			child.sorted = true;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00011648 File Offset: 0x0000F848
		private void SortTransformConstraint(TransformConstraint constraint)
		{
			constraint.active = constraint.target.active && (!constraint.data.skinRequired || (this.skin != null && this.skin.constraints.Contains(constraint.data)));
			if (!constraint.active)
			{
				return;
			}
			this.SortBone(constraint.target);
			Bone[] constrained = constraint.bones.Items;
			int boneCount = constraint.bones.Count;
			if (constraint.data.local)
			{
				for (int i = 0; i < boneCount; i++)
				{
					Bone child = constrained[i];
					this.SortBone(child.parent);
					this.SortBone(child);
				}
			}
			else
			{
				for (int j = 0; j < boneCount; j++)
				{
					this.SortBone(constrained[j]);
				}
			}
			this.updateCache.Add(constraint);
			for (int k = 0; k < boneCount; k++)
			{
				Skeleton.SortReset(constrained[k].children);
			}
			for (int l = 0; l < boneCount; l++)
			{
				constrained[l].sorted = true;
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0001175C File Offset: 0x0000F95C
		private void SortPathConstraint(PathConstraint constraint)
		{
			constraint.active = constraint.target.bone.active && (!constraint.data.skinRequired || (this.skin != null && this.skin.constraints.Contains(constraint.data)));
			if (!constraint.active)
			{
				return;
			}
			Slot target = constraint.target;
			int slotIndex = target.data.index;
			Bone slotBone = target.bone;
			if (this.skin != null)
			{
				this.SortPathConstraintAttachment(this.skin, slotIndex, slotBone);
			}
			if (this.data.defaultSkin != null && this.data.defaultSkin != this.skin)
			{
				this.SortPathConstraintAttachment(this.data.defaultSkin, slotIndex, slotBone);
			}
			Attachment attachment = target.attachment;
			if (attachment is PathAttachment)
			{
				this.SortPathConstraintAttachment(attachment, slotBone);
			}
			Bone[] constrained = constraint.bones.Items;
			int boneCount = constraint.bones.Count;
			for (int i = 0; i < boneCount; i++)
			{
				this.SortBone(constrained[i]);
			}
			this.updateCache.Add(constraint);
			for (int j = 0; j < boneCount; j++)
			{
				Skeleton.SortReset(constrained[j].children);
			}
			for (int k = 0; k < boneCount; k++)
			{
				constrained[k].sorted = true;
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000118B0 File Offset: 0x0000FAB0
		private void SortPathConstraintAttachment(Skin skin, int slotIndex, Bone slotBone)
		{
			foreach (Skin.SkinEntry entry in skin.Attachments)
			{
				if (entry.SlotIndex == slotIndex)
				{
					this.SortPathConstraintAttachment(entry.Attachment, slotBone);
				}
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00011910 File Offset: 0x0000FB10
		private void SortPathConstraintAttachment(Attachment attachment, Bone slotBone)
		{
			if (!(attachment is PathAttachment))
			{
				return;
			}
			int[] pathBones = ((PathAttachment)attachment).bones;
			if (pathBones == null)
			{
				this.SortBone(slotBone);
				return;
			}
			Bone[] bones = this.bones.Items;
			int i = 0;
			int j = pathBones.Length;
			while (i < j)
			{
				int nn = pathBones[i++];
				nn += i;
				while (i < nn)
				{
					this.SortBone(bones[pathBones[i++]]);
				}
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001197C File Offset: 0x0000FB7C
		private void SortPhysicsConstraint(PhysicsConstraint constraint)
		{
			Bone bone = constraint.bone;
			constraint.active = bone.active && (!constraint.data.skinRequired || (this.skin != null && this.skin.constraints.Contains(constraint.data)));
			if (!constraint.active)
			{
				return;
			}
			this.SortBone(bone);
			this.updateCache.Add(constraint);
			Skeleton.SortReset(bone.children);
			bone.sorted = true;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00011A00 File Offset: 0x0000FC00
		private void SortBone(Bone bone)
		{
			if (bone.sorted)
			{
				return;
			}
			Bone parent = bone.parent;
			if (parent != null)
			{
				this.SortBone(parent);
			}
			bone.sorted = true;
			this.updateCache.Add(bone);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00011A3C File Offset: 0x0000FC3C
		private static void SortReset(ExposedList<Bone> bones)
		{
			Bone[] bonesItems = bones.Items;
			int i = 0;
			int j = bones.Count;
			while (i < j)
			{
				Bone bone = bonesItems[i];
				if (bone.active)
				{
					if (bone.sorted)
					{
						Skeleton.SortReset(bone.children);
					}
					bone.sorted = false;
				}
				i++;
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00011A8C File Offset: 0x0000FC8C
		public void UpdateWorldTransform(Skeleton.Physics physics)
		{
			Bone[] bones = this.bones.Items;
			int i = 0;
			int j = this.bones.Count;
			while (i < j)
			{
				Bone bone = bones[i];
				bone.ax = bone.x;
				bone.ay = bone.y;
				bone.arotation = bone.rotation;
				bone.ascaleX = bone.scaleX;
				bone.ascaleY = bone.scaleY;
				bone.ashearX = bone.shearX;
				bone.ashearY = bone.shearY;
				i++;
			}
			IUpdatable[] updateCache = this.updateCache.Items;
			int k = 0;
			int l = this.updateCache.Count;
			while (k < l)
			{
				updateCache[k].Update(physics);
				k++;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00011B48 File Offset: 0x0000FD48
		public void UpdateWorldTransform(Skeleton.Physics physics, Bone parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent", "parent cannot be null.");
			}
			Bone rootBone = this.RootBone;
			float pa = parent.a;
			float pb = parent.b;
			float pc = parent.c;
			float pd = parent.d;
			rootBone.worldX = pa * this.x + pb * this.y + parent.worldX;
			rootBone.worldY = pc * this.x + pd * this.y + parent.worldY;
			float rx = (rootBone.rotation + rootBone.shearX) * 0.017453292f;
			float num = (rootBone.rotation + 90f + rootBone.shearY) * 0.017453292f;
			float la = (float)Math.Cos((double)rx) * rootBone.scaleX;
			float lb = (float)Math.Cos((double)num) * rootBone.scaleY;
			float lc = (float)Math.Sin((double)rx) * rootBone.scaleX;
			float ld = (float)Math.Sin((double)num) * rootBone.scaleY;
			rootBone.a = (pa * la + pb * lc) * this.scaleX;
			rootBone.b = (pa * lb + pb * ld) * this.scaleX;
			rootBone.c = (pc * la + pd * lc) * this.scaleY;
			rootBone.d = (pc * lb + pd * ld) * this.scaleY;
			IUpdatable[] updateCache = this.updateCache.Items;
			int i = 0;
			int j = this.updateCache.Count;
			while (i < j)
			{
				IUpdatable updatable = updateCache[i];
				if (updatable != rootBone)
				{
					updatable.Update(physics);
				}
				i++;
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00011CD8 File Offset: 0x0000FED8
		public void PhysicsTranslate(float x, float y)
		{
			PhysicsConstraint[] physicsConstraints = this.physicsConstraints.Items;
			int i = 0;
			int j = this.physicsConstraints.Count;
			while (i < j)
			{
				physicsConstraints[i].Translate(x, y);
				i++;
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00011D14 File Offset: 0x0000FF14
		public void PhysicsRotate(float x, float y, float degrees)
		{
			PhysicsConstraint[] physicsConstraints = this.physicsConstraints.Items;
			int i = 0;
			int j = this.physicsConstraints.Count;
			while (i < j)
			{
				physicsConstraints[i].Rotate(x, y, degrees);
				i++;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00011D50 File Offset: 0x0000FF50
		public void Update(float delta)
		{
			this.time += delta;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00011D60 File Offset: 0x0000FF60
		public void SetToSetupPose()
		{
			this.SetBonesToSetupPose();
			this.SetSlotsToSetupPose();
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00011D70 File Offset: 0x0000FF70
		public void SetBonesToSetupPose()
		{
			Bone[] bones = this.bones.Items;
			int i = 0;
			int j = this.bones.Count;
			while (i < j)
			{
				bones[i].SetToSetupPose();
				i++;
			}
			IkConstraint[] ikConstraints = this.ikConstraints.Items;
			int k = 0;
			int l = this.ikConstraints.Count;
			while (k < l)
			{
				ikConstraints[k].SetToSetupPose();
				k++;
			}
			TransformConstraint[] transformConstraints = this.transformConstraints.Items;
			int m = 0;
			int n = this.transformConstraints.Count;
			while (m < n)
			{
				transformConstraints[m].SetToSetupPose();
				m++;
			}
			PathConstraint[] pathConstraints = this.pathConstraints.Items;
			int i2 = 0;
			int n2 = this.pathConstraints.Count;
			while (i2 < n2)
			{
				pathConstraints[i2].SetToSetupPose();
				i2++;
			}
			PhysicsConstraint[] physicsConstraints = this.physicsConstraints.Items;
			int i3 = 0;
			int n3 = this.physicsConstraints.Count;
			while (i3 < n3)
			{
				physicsConstraints[i3].SetToSetupPose();
				i3++;
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00011E80 File Offset: 0x00010080
		public void SetSlotsToSetupPose()
		{
			Slot[] slots = this.slots.Items;
			int i = this.slots.Count;
			Array.Copy(slots, 0, this.drawOrder.Items, 0, i);
			for (int j = 0; j < i; j++)
			{
				slots[j].SetToSetupPose();
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00011ED0 File Offset: 0x000100D0
		public Bone FindBone(string boneName)
		{
			if (boneName == null)
			{
				throw new ArgumentNullException("boneName", "boneName cannot be null.");
			}
			Bone[] bones = this.bones.Items;
			int i = 0;
			int j = this.bones.Count;
			while (i < j)
			{
				Bone bone = bones[i];
				if (bone.data.name == boneName)
				{
					return bone;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00011F30 File Offset: 0x00010130
		public Slot FindSlot(string slotName)
		{
			if (slotName == null)
			{
				throw new ArgumentNullException("slotName", "slotName cannot be null.");
			}
			Slot[] slots = this.slots.Items;
			int i = 0;
			int j = this.slots.Count;
			while (i < j)
			{
				Slot slot = slots[i];
				if (slot.data.name == slotName)
				{
					return slot;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00011F90 File Offset: 0x00010190
		public void SetSkin(string skinName)
		{
			Skin foundSkin = this.data.FindSkin(skinName);
			if (foundSkin == null)
			{
				throw new ArgumentException("Skin not found: " + skinName, "skinName");
			}
			this.SetSkin(foundSkin);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00011FCC File Offset: 0x000101CC
		public void SetSkin(Skin newSkin)
		{
			if (newSkin == this.skin)
			{
				return;
			}
			if (newSkin != null)
			{
				if (this.skin != null)
				{
					newSkin.AttachAll(this, this.skin);
				}
				else
				{
					Slot[] slots = this.slots.Items;
					int i = 0;
					int j = this.slots.Count;
					while (i < j)
					{
						Slot slot = slots[i];
						string name = slot.data.attachmentName;
						if (name != null)
						{
							Attachment attachment = newSkin.GetAttachment(i, name);
							if (attachment != null)
							{
								slot.Attachment = attachment;
							}
						}
						i++;
					}
				}
			}
			this.skin = newSkin;
			this.UpdateCache();
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001205A File Offset: 0x0001025A
		public Attachment GetAttachment(string slotName, string attachmentName)
		{
			return this.GetAttachment(this.data.FindSlot(slotName).index, attachmentName);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00012074 File Offset: 0x00010274
		public Attachment GetAttachment(int slotIndex, string attachmentName)
		{
			if (attachmentName == null)
			{
				throw new ArgumentNullException("attachmentName", "attachmentName cannot be null.");
			}
			if (this.skin != null)
			{
				Attachment attachment = this.skin.GetAttachment(slotIndex, attachmentName);
				if (attachment != null)
				{
					return attachment;
				}
			}
			if (this.data.defaultSkin == null)
			{
				return null;
			}
			return this.data.defaultSkin.GetAttachment(slotIndex, attachmentName);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000120D0 File Offset: 0x000102D0
		public void SetAttachment(string slotName, string attachmentName)
		{
			if (slotName == null)
			{
				throw new ArgumentNullException("slotName", "slotName cannot be null.");
			}
			Slot[] slots = this.slots.Items;
			int i = 0;
			int j = this.slots.Count;
			while (i < j)
			{
				Slot slot = slots[i];
				if (slot.data.name == slotName)
				{
					Attachment attachment = null;
					if (attachmentName != null)
					{
						attachment = this.GetAttachment(i, attachmentName);
						if (attachment == null)
						{
							throw new Exception("Attachment not found: " + attachmentName + ", for slot: " + slotName);
						}
					}
					slot.Attachment = attachment;
					return;
				}
				i++;
			}
			throw new Exception("Slot not found: " + slotName);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00012170 File Offset: 0x00010370
		public IkConstraint FindIkConstraint(string constraintName)
		{
			if (constraintName == null)
			{
				throw new ArgumentNullException("constraintName", "constraintName cannot be null.");
			}
			IkConstraint[] ikConstraints = this.ikConstraints.Items;
			int i = 0;
			int j = this.ikConstraints.Count;
			while (i < j)
			{
				IkConstraint ikConstraint = ikConstraints[i];
				if (ikConstraint.data.name == constraintName)
				{
					return ikConstraint;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000121D0 File Offset: 0x000103D0
		public TransformConstraint FindTransformConstraint(string constraintName)
		{
			if (constraintName == null)
			{
				throw new ArgumentNullException("constraintName", "constraintName cannot be null.");
			}
			TransformConstraint[] transformConstraints = this.transformConstraints.Items;
			int i = 0;
			int j = this.transformConstraints.Count;
			while (i < j)
			{
				TransformConstraint transformConstraint = transformConstraints[i];
				if (transformConstraint.data.Name == constraintName)
				{
					return transformConstraint;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00012230 File Offset: 0x00010430
		public PathConstraint FindPathConstraint(string constraintName)
		{
			if (constraintName == null)
			{
				throw new ArgumentNullException("constraintName", "constraintName cannot be null.");
			}
			PathConstraint[] pathConstraints = this.pathConstraints.Items;
			int i = 0;
			int j = this.pathConstraints.Count;
			while (i < j)
			{
				PathConstraint constraint = pathConstraints[i];
				if (constraint.data.Name.Equals(constraintName))
				{
					return constraint;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00012290 File Offset: 0x00010490
		public PhysicsConstraint FindPhysicsConstraint(string constraintName)
		{
			if (constraintName == null)
			{
				throw new ArgumentNullException("constraintName", "constraintName cannot be null.");
			}
			PhysicsConstraint[] physicsConstraints = this.physicsConstraints.Items;
			int i = 0;
			int j = this.physicsConstraints.Count;
			while (i < j)
			{
				PhysicsConstraint constraint = physicsConstraints[i];
				if (constraint.data.name.Equals(constraintName))
				{
					return constraint;
				}
				i++;
			}
			return null;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000122F0 File Offset: 0x000104F0
		public void GetBounds(out float x, out float y, out float width, out float height, ref float[] vertexBuffer, SkeletonClipping clipper = null)
		{
			float[] temp = vertexBuffer;
			temp = temp ?? new float[8];
			Slot[] drawOrder = this.drawOrder.Items;
			float minX = 2.1474836E+09f;
			float minY = 2.1474836E+09f;
			float maxX = -2.1474836E+09f;
			float maxY = -2.1474836E+09f;
			int i = 0;
			int j = this.drawOrder.Count;
			while (i < j)
			{
				Slot slot = drawOrder[i];
				if (slot.bone.active)
				{
					int verticesLength = 0;
					float[] vertices = null;
					int[] triangles = null;
					Attachment attachment = slot.attachment;
					RegionAttachment region = attachment as RegionAttachment;
					if (region != null)
					{
						verticesLength = 8;
						vertices = temp;
						if (vertices.Length < 8)
						{
							temp = (vertices = new float[8]);
						}
						region.ComputeWorldVertices(slot, temp, 0, 2);
						triangles = Skeleton.quadTriangles;
					}
					else
					{
						MeshAttachment mesh = attachment as MeshAttachment;
						if (mesh != null)
						{
							verticesLength = mesh.WorldVerticesLength;
							vertices = temp;
							if (vertices.Length < verticesLength)
							{
								temp = (vertices = new float[verticesLength]);
							}
							mesh.ComputeWorldVertices(slot, 0, verticesLength, temp, 0, 2);
							triangles = mesh.Triangles;
						}
						else if (clipper != null)
						{
							ClippingAttachment clip = attachment as ClippingAttachment;
							if (clip != null)
							{
								clipper.ClipStart(slot, clip);
								goto IL_01AA;
							}
						}
					}
					if (vertices != null)
					{
						if (clipper != null && clipper.IsClipping)
						{
							clipper.ClipTriangles(vertices, triangles, triangles.Length);
							vertices = clipper.ClippedVertices.Items;
							verticesLength = clipper.ClippedVertices.Count;
						}
						for (int ii = 0; ii < verticesLength; ii += 2)
						{
							float vx = vertices[ii];
							float vy = vertices[ii + 1];
							minX = Math.Min(minX, vx);
							minY = Math.Min(minY, vy);
							maxX = Math.Max(maxX, vx);
							maxY = Math.Max(maxY, vy);
						}
					}
					if (clipper != null)
					{
						clipper.ClipEnd(slot);
					}
				}
				IL_01AA:
				i++;
			}
			if (clipper != null)
			{
				clipper.ClipEnd();
			}
			x = minX;
			y = minY;
			width = maxX - minX;
			height = maxY - minY;
			vertexBuffer = temp;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000124D8 File Offset: 0x000106D8
		public override string ToString()
		{
			return this.data.name;
		}

		// Token: 0x0400023E RID: 574
		private static readonly int[] quadTriangles = new int[] { 0, 1, 2, 2, 3, 0 };

		// Token: 0x0400023F RID: 575
		internal SkeletonData data;

		// Token: 0x04000240 RID: 576
		internal ExposedList<Bone> bones;

		// Token: 0x04000241 RID: 577
		internal ExposedList<Slot> slots;

		// Token: 0x04000242 RID: 578
		internal ExposedList<Slot> drawOrder;

		// Token: 0x04000243 RID: 579
		internal ExposedList<IkConstraint> ikConstraints;

		// Token: 0x04000244 RID: 580
		internal ExposedList<TransformConstraint> transformConstraints;

		// Token: 0x04000245 RID: 581
		internal ExposedList<PathConstraint> pathConstraints;

		// Token: 0x04000246 RID: 582
		internal ExposedList<PhysicsConstraint> physicsConstraints;

		// Token: 0x04000247 RID: 583
		internal ExposedList<IUpdatable> updateCache = new ExposedList<IUpdatable>();

		// Token: 0x04000248 RID: 584
		internal Skin skin;

		// Token: 0x04000249 RID: 585
		internal float r = 1f;

		// Token: 0x0400024A RID: 586
		internal float g = 1f;

		// Token: 0x0400024B RID: 587
		internal float b = 1f;

		// Token: 0x0400024C RID: 588
		internal float a = 1f;

		// Token: 0x0400024D RID: 589
		internal float x;

		// Token: 0x0400024E RID: 590
		internal float y;

		// Token: 0x0400024F RID: 591
		internal float scaleX = 1f;

		// Token: 0x04000250 RID: 592
		internal float time;

		// Token: 0x04000251 RID: 593
		private float scaleY = 1f;

		// Token: 0x02000071 RID: 113
		public enum Physics
		{
			// Token: 0x04000253 RID: 595
			None,
			// Token: 0x04000254 RID: 596
			Reset,
			// Token: 0x04000255 RID: 597
			Update,
			// Token: 0x04000256 RID: 598
			Pose
		}
	}
}
