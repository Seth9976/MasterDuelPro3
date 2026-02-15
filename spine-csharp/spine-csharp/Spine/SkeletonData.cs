using System;

namespace Spine
{
	// Token: 0x02000079 RID: 121
	public class SkeletonData
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00016A44 File Offset: 0x00014C44
		// (set) Token: 0x06000467 RID: 1127 RVA: 0x00016A4C File Offset: 0x00014C4C
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00016A55 File Offset: 0x00014C55
		public ExposedList<BoneData> Bones
		{
			get
			{
				return this.bones;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00016A5D File Offset: 0x00014C5D
		public ExposedList<SlotData> Slots
		{
			get
			{
				return this.slots;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x00016A65 File Offset: 0x00014C65
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x00016A6D File Offset: 0x00014C6D
		public ExposedList<Skin> Skins
		{
			get
			{
				return this.skins;
			}
			set
			{
				this.skins = value;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00016A76 File Offset: 0x00014C76
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x00016A7E File Offset: 0x00014C7E
		public Skin DefaultSkin
		{
			get
			{
				return this.defaultSkin;
			}
			set
			{
				this.defaultSkin = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00016A87 File Offset: 0x00014C87
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x00016A8F File Offset: 0x00014C8F
		public ExposedList<EventData> Events
		{
			get
			{
				return this.events;
			}
			set
			{
				this.events = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00016A98 File Offset: 0x00014C98
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x00016AA0 File Offset: 0x00014CA0
		public ExposedList<Animation> Animations
		{
			get
			{
				return this.animations;
			}
			set
			{
				this.animations = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00016AA9 File Offset: 0x00014CA9
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x00016AB1 File Offset: 0x00014CB1
		public ExposedList<IkConstraintData> IkConstraints
		{
			get
			{
				return this.ikConstraints;
			}
			set
			{
				this.ikConstraints = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00016ABA File Offset: 0x00014CBA
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x00016AC2 File Offset: 0x00014CC2
		public ExposedList<TransformConstraintData> TransformConstraints
		{
			get
			{
				return this.transformConstraints;
			}
			set
			{
				this.transformConstraints = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00016ACB File Offset: 0x00014CCB
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x00016AD3 File Offset: 0x00014CD3
		public ExposedList<PathConstraintData> PathConstraints
		{
			get
			{
				return this.pathConstraints;
			}
			set
			{
				this.pathConstraints = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00016ADC File Offset: 0x00014CDC
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x00016AE4 File Offset: 0x00014CE4
		public ExposedList<PhysicsConstraintData> PhysicsConstraints
		{
			get
			{
				return this.physicsConstraints;
			}
			set
			{
				this.physicsConstraints = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00016AED File Offset: 0x00014CED
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x00016AF5 File Offset: 0x00014CF5
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

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00016AFE File Offset: 0x00014CFE
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00016B06 File Offset: 0x00014D06
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

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00016B0F File Offset: 0x00014D0F
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x00016B17 File Offset: 0x00014D17
		public float Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x00016B20 File Offset: 0x00014D20
		// (set) Token: 0x06000481 RID: 1153 RVA: 0x00016B28 File Offset: 0x00014D28
		public float Height
		{
			get
			{
				return this.height;
			}
			set
			{
				this.height = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x00016B31 File Offset: 0x00014D31
		// (set) Token: 0x06000483 RID: 1155 RVA: 0x00016B39 File Offset: 0x00014D39
		public float ReferenceScale
		{
			get
			{
				return this.referenceScale;
			}
			set
			{
				this.referenceScale = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x00016B42 File Offset: 0x00014D42
		// (set) Token: 0x06000485 RID: 1157 RVA: 0x00016B4A File Offset: 0x00014D4A
		public string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x00016B53 File Offset: 0x00014D53
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x00016B5B File Offset: 0x00014D5B
		public string Hash
		{
			get
			{
				return this.hash;
			}
			set
			{
				this.hash = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x00016B64 File Offset: 0x00014D64
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x00016B6C File Offset: 0x00014D6C
		public string ImagesPath
		{
			get
			{
				return this.imagesPath;
			}
			set
			{
				this.imagesPath = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00016B75 File Offset: 0x00014D75
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x00016B7D File Offset: 0x00014D7D
		public string AudioPath
		{
			get
			{
				return this.audioPath;
			}
			set
			{
				this.audioPath = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00016B86 File Offset: 0x00014D86
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x00016B8E File Offset: 0x00014D8E
		public float Fps
		{
			get
			{
				return this.fps;
			}
			set
			{
				this.fps = value;
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00016B98 File Offset: 0x00014D98
		public BoneData FindBone(string boneName)
		{
			if (boneName == null)
			{
				throw new ArgumentNullException("boneName", "boneName cannot be null.");
			}
			BoneData[] bones = this.bones.Items;
			int i = 0;
			int j = this.bones.Count;
			while (i < j)
			{
				BoneData bone = bones[i];
				if (bone.name == boneName)
				{
					return bone;
				}
				i++;
			}
			return null;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00016BF4 File Offset: 0x00014DF4
		public SlotData FindSlot(string slotName)
		{
			if (slotName == null)
			{
				throw new ArgumentNullException("slotName", "slotName cannot be null.");
			}
			SlotData[] slots = this.slots.Items;
			int i = 0;
			int j = this.slots.Count;
			while (i < j)
			{
				SlotData slot = slots[i];
				if (slot.name == slotName)
				{
					return slot;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00016C50 File Offset: 0x00014E50
		public Skin FindSkin(string skinName)
		{
			if (skinName == null)
			{
				throw new ArgumentNullException("skinName", "skinName cannot be null.");
			}
			foreach (Skin skin in this.skins)
			{
				if (skin.name == skinName)
				{
					return skin;
				}
			}
			return null;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00016CC4 File Offset: 0x00014EC4
		public EventData FindEvent(string eventDataName)
		{
			if (eventDataName == null)
			{
				throw new ArgumentNullException("eventDataName", "eventDataName cannot be null.");
			}
			foreach (EventData eventData in this.events)
			{
				if (eventData.name == eventDataName)
				{
					return eventData;
				}
			}
			return null;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00016D38 File Offset: 0x00014F38
		public Animation FindAnimation(string animationName)
		{
			if (animationName == null)
			{
				throw new ArgumentNullException("animationName", "animationName cannot be null.");
			}
			Animation[] animations = this.animations.Items;
			int i = 0;
			int j = this.animations.Count;
			while (i < j)
			{
				Animation animation = animations[i];
				if (animation.name == animationName)
				{
					return animation;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00016D94 File Offset: 0x00014F94
		public IkConstraintData FindIkConstraint(string constraintName)
		{
			if (constraintName == null)
			{
				throw new ArgumentNullException("constraintName", "constraintName cannot be null.");
			}
			IkConstraintData[] ikConstraints = this.ikConstraints.Items;
			int i = 0;
			int j = this.ikConstraints.Count;
			while (i < j)
			{
				IkConstraintData ikConstraint = ikConstraints[i];
				if (ikConstraint.name == constraintName)
				{
					return ikConstraint;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00016DF0 File Offset: 0x00014FF0
		public TransformConstraintData FindTransformConstraint(string constraintName)
		{
			if (constraintName == null)
			{
				throw new ArgumentNullException("constraintName", "constraintName cannot be null.");
			}
			TransformConstraintData[] transformConstraints = this.transformConstraints.Items;
			int i = 0;
			int j = this.transformConstraints.Count;
			while (i < j)
			{
				TransformConstraintData transformConstraint = transformConstraints[i];
				if (transformConstraint.name == constraintName)
				{
					return transformConstraint;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00016E4C File Offset: 0x0001504C
		public PathConstraintData FindPathConstraint(string constraintName)
		{
			if (constraintName == null)
			{
				throw new ArgumentNullException("constraintName", "constraintName cannot be null.");
			}
			PathConstraintData[] pathConstraints = this.pathConstraints.Items;
			int i = 0;
			int j = this.pathConstraints.Count;
			while (i < j)
			{
				PathConstraintData constraint = pathConstraints[i];
				if (constraint.name.Equals(constraintName))
				{
					return constraint;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00016EA8 File Offset: 0x000150A8
		public PhysicsConstraintData FindPhysicsConstraint(string constraintName)
		{
			if (constraintName == null)
			{
				throw new ArgumentNullException("constraintName", "constraintName cannot be null.");
			}
			PhysicsConstraintData[] physicsConstraints = this.physicsConstraints.Items;
			int i = 0;
			int j = this.physicsConstraints.Count;
			while (i < j)
			{
				PhysicsConstraintData constraint = physicsConstraints[i];
				if (constraint.name.Equals(constraintName))
				{
					return constraint;
				}
				i++;
			}
			return null;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00016F01 File Offset: 0x00015101
		public override string ToString()
		{
			return this.name ?? base.ToString();
		}

		// Token: 0x04000297 RID: 663
		internal string name;

		// Token: 0x04000298 RID: 664
		internal ExposedList<BoneData> bones = new ExposedList<BoneData>();

		// Token: 0x04000299 RID: 665
		internal ExposedList<SlotData> slots = new ExposedList<SlotData>();

		// Token: 0x0400029A RID: 666
		internal ExposedList<Skin> skins = new ExposedList<Skin>();

		// Token: 0x0400029B RID: 667
		internal Skin defaultSkin;

		// Token: 0x0400029C RID: 668
		internal ExposedList<EventData> events = new ExposedList<EventData>();

		// Token: 0x0400029D RID: 669
		internal ExposedList<Animation> animations = new ExposedList<Animation>();

		// Token: 0x0400029E RID: 670
		internal ExposedList<IkConstraintData> ikConstraints = new ExposedList<IkConstraintData>();

		// Token: 0x0400029F RID: 671
		internal ExposedList<TransformConstraintData> transformConstraints = new ExposedList<TransformConstraintData>();

		// Token: 0x040002A0 RID: 672
		internal ExposedList<PathConstraintData> pathConstraints = new ExposedList<PathConstraintData>();

		// Token: 0x040002A1 RID: 673
		internal ExposedList<PhysicsConstraintData> physicsConstraints = new ExposedList<PhysicsConstraintData>();

		// Token: 0x040002A2 RID: 674
		internal float x;

		// Token: 0x040002A3 RID: 675
		internal float y;

		// Token: 0x040002A4 RID: 676
		internal float width;

		// Token: 0x040002A5 RID: 677
		internal float height;

		// Token: 0x040002A6 RID: 678
		internal float referenceScale = 100f;

		// Token: 0x040002A7 RID: 679
		internal string version;

		// Token: 0x040002A8 RID: 680
		internal string hash;

		// Token: 0x040002A9 RID: 681
		internal float fps;

		// Token: 0x040002AA RID: 682
		internal string imagesPath;

		// Token: 0x040002AB RID: 683
		internal string audioPath;
	}
}
