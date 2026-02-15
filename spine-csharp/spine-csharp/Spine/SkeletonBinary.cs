using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace Spine
{
	// Token: 0x02000072 RID: 114
	public class SkeletonBinary : SkeletonLoader
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x000124FD File Offset: 0x000106FD
		public SkeletonBinary(AttachmentLoader attachmentLoader)
			: base(attachmentLoader)
		{
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x00012511 File Offset: 0x00010711
		public SkeletonBinary(params Atlas[] atlasArray)
			: base(atlasArray)
		{
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00012528 File Offset: 0x00010728
		public override SkeletonData ReadSkeletonData(string path)
		{
			SkeletonData skeletonData2;
			using (FileStream input = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				SkeletonData skeletonData = this.ReadSkeletonData(input);
				skeletonData.name = Path.GetFileNameWithoutExtension(path);
				skeletonData2 = skeletonData;
			}
			return skeletonData2;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00012570 File Offset: 0x00010770
		public static string GetVersionString(Stream file)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}
			return new SkeletonBinary.SkeletonInput(file).GetVersionString();
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001258C File Offset: 0x0001078C
		public SkeletonData ReadSkeletonData(Stream file)
		{
			if (file == null)
			{
				throw new ArgumentNullException("file");
			}
			float scale = this.scale;
			SkeletonData skeletonData = new SkeletonData();
			SkeletonBinary.SkeletonInput input = new SkeletonBinary.SkeletonInput(file);
			long hash = input.ReadLong();
			skeletonData.hash = ((hash == 0L) ? null : hash.ToString());
			skeletonData.version = input.ReadString();
			if (skeletonData.version.Length == 0)
			{
				skeletonData.version = null;
			}
			if (skeletonData.version.Length > 13)
			{
				return null;
			}
			skeletonData.x = input.ReadFloat();
			skeletonData.y = input.ReadFloat();
			skeletonData.width = input.ReadFloat();
			skeletonData.height = input.ReadFloat();
			skeletonData.referenceScale = input.ReadFloat() * scale;
			bool nonessential = input.ReadBoolean();
			if (nonessential)
			{
				skeletonData.fps = input.ReadFloat();
				skeletonData.imagesPath = input.ReadString();
				if (string.IsNullOrEmpty(skeletonData.imagesPath))
				{
					skeletonData.imagesPath = null;
				}
				skeletonData.audioPath = input.ReadString();
				if (string.IsNullOrEmpty(skeletonData.audioPath))
				{
					skeletonData.audioPath = null;
				}
			}
			int i;
			object[] array = (input.strings = new string[i = input.ReadInt(true)]);
			object[] o = array;
			for (int j = 0; j < i; j++)
			{
				o[j] = input.ReadString();
			}
			BoneData[] bones = skeletonData.bones.Resize(i = input.ReadInt(true)).Items;
			for (int k = 0; k < i; k++)
			{
				string name = input.ReadString();
				BoneData parent = ((k == 0) ? null : bones[input.ReadInt(true)]);
				BoneData data = new BoneData(k, name, parent);
				data.rotation = input.ReadFloat();
				data.x = input.ReadFloat() * scale;
				data.y = input.ReadFloat() * scale;
				data.scaleX = input.ReadFloat();
				data.scaleY = input.ReadFloat();
				data.shearX = input.ReadFloat();
				data.shearY = input.ReadFloat();
				data.Length = input.ReadFloat() * scale;
				data.inherit = InheritEnum.Values[input.ReadInt(true)];
				data.skinRequired = input.ReadBoolean();
				if (nonessential)
				{
					input.ReadInt();
					input.ReadString();
					input.ReadBoolean();
				}
				bones[k] = data;
			}
			SlotData[] slots = skeletonData.slots.Resize(i = input.ReadInt(true)).Items;
			for (int l = 0; l < i; l++)
			{
				string slotName = input.ReadString();
				BoneData boneData = bones[input.ReadInt(true)];
				SlotData slotData = new SlotData(l, slotName, boneData);
				int color = input.ReadInt();
				slotData.r = (float)(((long)color & (long)((ulong)(-16777216))) >> 24) / 255f;
				slotData.g = (float)((color & 16711680) >> 16) / 255f;
				slotData.b = (float)((color & 65280) >> 8) / 255f;
				slotData.a = (float)(color & 255) / 255f;
				int darkColor = input.ReadInt();
				if (darkColor != -1)
				{
					slotData.hasSecondColor = true;
					slotData.r2 = (float)((darkColor & 16711680) >> 16) / 255f;
					slotData.g2 = (float)((darkColor & 65280) >> 8) / 255f;
					slotData.b2 = (float)(darkColor & 255) / 255f;
				}
				slotData.attachmentName = input.ReadStringRef();
				slotData.blendMode = (BlendMode)input.ReadInt(true);
				if (nonessential)
				{
					input.ReadBoolean();
				}
				slots[l] = slotData;
			}
			array = skeletonData.ikConstraints.Resize(i = input.ReadInt(true)).Items;
			o = array;
			for (int m = 0; m < i; m++)
			{
				IkConstraintData data2 = new IkConstraintData(input.ReadString());
				data2.order = input.ReadInt(true);
				int nn;
				BoneData[] constraintBones = data2.bones.Resize(nn = input.ReadInt(true)).Items;
				for (int ii = 0; ii < nn; ii++)
				{
					constraintBones[ii] = bones[input.ReadInt(true)];
				}
				data2.target = bones[input.ReadInt(true)];
				int flags = input.Read();
				data2.skinRequired = (flags & 1) != 0;
				data2.bendDirection = (((flags & 2) != 0) ? 1 : (-1));
				data2.compress = (flags & 4) != 0;
				data2.stretch = (flags & 8) != 0;
				data2.uniform = (flags & 16) != 0;
				if ((flags & 32) != 0)
				{
					data2.mix = (((flags & 64) != 0) ? input.ReadFloat() : 1f);
				}
				if ((flags & 128) != 0)
				{
					data2.softness = input.ReadFloat() * scale;
				}
				o[m] = data2;
			}
			array = skeletonData.transformConstraints.Resize(i = input.ReadInt(true)).Items;
			o = array;
			for (int n = 0; n < i; n++)
			{
				TransformConstraintData data3 = new TransformConstraintData(input.ReadString());
				data3.order = input.ReadInt(true);
				int nn2;
				BoneData[] constraintBones2 = data3.bones.Resize(nn2 = input.ReadInt(true)).Items;
				for (int ii2 = 0; ii2 < nn2; ii2++)
				{
					constraintBones2[ii2] = bones[input.ReadInt(true)];
				}
				data3.target = bones[input.ReadInt(true)];
				int flags2 = input.Read();
				data3.skinRequired = (flags2 & 1) != 0;
				data3.local = (flags2 & 2) != 0;
				data3.relative = (flags2 & 4) != 0;
				if ((flags2 & 8) != 0)
				{
					data3.offsetRotation = input.ReadFloat();
				}
				if ((flags2 & 16) != 0)
				{
					data3.offsetX = input.ReadFloat() * scale;
				}
				if ((flags2 & 32) != 0)
				{
					data3.offsetY = input.ReadFloat() * scale;
				}
				if ((flags2 & 64) != 0)
				{
					data3.offsetScaleX = input.ReadFloat();
				}
				if ((flags2 & 128) != 0)
				{
					data3.offsetScaleY = input.ReadFloat();
				}
				flags2 = input.Read();
				if ((flags2 & 1) != 0)
				{
					data3.offsetShearY = input.ReadFloat();
				}
				if ((flags2 & 2) != 0)
				{
					data3.mixRotate = input.ReadFloat();
				}
				if ((flags2 & 4) != 0)
				{
					data3.mixX = input.ReadFloat();
				}
				if ((flags2 & 8) != 0)
				{
					data3.mixY = input.ReadFloat();
				}
				if ((flags2 & 16) != 0)
				{
					data3.mixScaleX = input.ReadFloat();
				}
				if ((flags2 & 32) != 0)
				{
					data3.mixScaleY = input.ReadFloat();
				}
				if ((flags2 & 64) != 0)
				{
					data3.mixShearY = input.ReadFloat();
				}
				o[n] = data3;
			}
			array = skeletonData.pathConstraints.Resize(i = input.ReadInt(true)).Items;
			o = array;
			for (int i2 = 0; i2 < i; i2++)
			{
				PathConstraintData data4 = new PathConstraintData(input.ReadString());
				data4.order = input.ReadInt(true);
				data4.skinRequired = input.ReadBoolean();
				int nn3;
				BoneData[] constraintBones3 = data4.bones.Resize(nn3 = input.ReadInt(true)).Items;
				for (int ii3 = 0; ii3 < nn3; ii3++)
				{
					constraintBones3[ii3] = bones[input.ReadInt(true)];
				}
				data4.target = slots[input.ReadInt(true)];
				int flags3 = input.Read();
				data4.positionMode = (PositionMode)Enum.GetValues(typeof(PositionMode)).GetValue(flags3 & 1);
				data4.spacingMode = (SpacingMode)Enum.GetValues(typeof(SpacingMode)).GetValue((flags3 >> 1) & 3);
				data4.rotateMode = (RotateMode)Enum.GetValues(typeof(RotateMode)).GetValue((flags3 >> 3) & 3);
				if ((flags3 & 128) != 0)
				{
					data4.offsetRotation = input.ReadFloat();
				}
				data4.position = input.ReadFloat();
				if (data4.positionMode == PositionMode.Fixed)
				{
					data4.position *= scale;
				}
				data4.spacing = input.ReadFloat();
				if (data4.spacingMode == SpacingMode.Length || data4.spacingMode == SpacingMode.Fixed)
				{
					data4.spacing *= scale;
				}
				data4.mixRotate = input.ReadFloat();
				data4.mixX = input.ReadFloat();
				data4.mixY = input.ReadFloat();
				o[i2] = data4;
			}
			array = skeletonData.physicsConstraints.Resize(i = input.ReadInt(true)).Items;
			o = array;
			for (int i3 = 0; i3 < i; i3++)
			{
				PhysicsConstraintData data5 = new PhysicsConstraintData(input.ReadString());
				data5.order = input.ReadInt(true);
				data5.bone = bones[input.ReadInt(true)];
				int flags4 = input.Read();
				data5.skinRequired = (flags4 & 1) != 0;
				if ((flags4 & 2) != 0)
				{
					data5.x = input.ReadFloat();
				}
				if ((flags4 & 4) != 0)
				{
					data5.y = input.ReadFloat();
				}
				if ((flags4 & 8) != 0)
				{
					data5.rotate = input.ReadFloat();
				}
				if ((flags4 & 16) != 0)
				{
					data5.scaleX = input.ReadFloat();
				}
				if ((flags4 & 32) != 0)
				{
					data5.shearX = input.ReadFloat();
				}
				data5.limit = (((flags4 & 64) != 0) ? input.ReadFloat() : 5000f) * scale;
				data5.step = 1f / (float)input.ReadUByte();
				data5.inertia = input.ReadFloat();
				data5.strength = input.ReadFloat();
				data5.damping = input.ReadFloat();
				data5.massInverse = (((flags4 & 128) != 0) ? input.ReadFloat() : 1f);
				data5.wind = input.ReadFloat();
				data5.gravity = input.ReadFloat();
				flags4 = input.Read();
				if ((flags4 & 1) != 0)
				{
					data5.inertiaGlobal = true;
				}
				if ((flags4 & 2) != 0)
				{
					data5.strengthGlobal = true;
				}
				if ((flags4 & 4) != 0)
				{
					data5.dampingGlobal = true;
				}
				if ((flags4 & 8) != 0)
				{
					data5.massGlobal = true;
				}
				if ((flags4 & 16) != 0)
				{
					data5.windGlobal = true;
				}
				if ((flags4 & 32) != 0)
				{
					data5.gravityGlobal = true;
				}
				if ((flags4 & 64) != 0)
				{
					data5.mixGlobal = true;
				}
				data5.mix = (((flags4 & 128) != 0) ? input.ReadFloat() : 1f);
				o[i3] = data5;
			}
			Skin defaultSkin = this.ReadSkin(input, skeletonData, true, nonessential);
			if (defaultSkin != null)
			{
				skeletonData.defaultSkin = defaultSkin;
				skeletonData.skins.Add(defaultSkin);
			}
			int i4 = skeletonData.skins.Count;
			array = skeletonData.skins.Resize(i = i4 + input.ReadInt(true)).Items;
			o = array;
			while (i4 < i)
			{
				o[i4] = this.ReadSkin(input, skeletonData, false, nonessential);
				i4++;
			}
			i = this.linkedMeshes.Count;
			for (int i5 = 0; i5 < i; i5++)
			{
				SkeletonBinary.LinkedMesh linkedMesh = this.linkedMeshes[i5];
				Attachment parent2 = skeletonData.skins.Items[linkedMesh.skinIndex].GetAttachment(linkedMesh.slotIndex, linkedMesh.parent);
				if (parent2 == null)
				{
					throw new Exception("Parent mesh not found: " + linkedMesh.parent);
				}
				linkedMesh.mesh.TimelineAttachment = (linkedMesh.inheritTimelines ? ((VertexAttachment)parent2) : linkedMesh.mesh);
				linkedMesh.mesh.ParentMesh = (MeshAttachment)parent2;
				if (linkedMesh.mesh.Sequence == null)
				{
					linkedMesh.mesh.UpdateRegion();
				}
			}
			this.linkedMeshes.Clear();
			array = skeletonData.events.Resize(i = input.ReadInt(true)).Items;
			o = array;
			for (int i6 = 0; i6 < i; i6++)
			{
				EventData data6 = new EventData(input.ReadString());
				data6.Int = input.ReadInt(false);
				data6.Float = input.ReadFloat();
				data6.String = input.ReadString();
				data6.AudioPath = input.ReadString();
				if (data6.AudioPath != null)
				{
					data6.Volume = input.ReadFloat();
					data6.Balance = input.ReadFloat();
				}
				o[i6] = data6;
			}
			array = skeletonData.animations.Resize(i = input.ReadInt(true)).Items;
			o = array;
			for (int i7 = 0; i7 < i; i7++)
			{
				o[i7] = this.ReadAnimation(input.ReadString(), input, skeletonData);
			}
			return skeletonData;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00013214 File Offset: 0x00011414
		private Skin ReadSkin(SkeletonBinary.SkeletonInput input, SkeletonData skeletonData, bool defaultSkin, bool nonessential)
		{
			int slotCount;
			Skin skin;
			if (defaultSkin)
			{
				slotCount = input.ReadInt(true);
				if (slotCount == 0)
				{
					return null;
				}
				skin = new Skin("default");
			}
			else
			{
				skin = new Skin(input.ReadString());
				if (nonessential)
				{
					input.ReadInt();
				}
				object[] items = skin.bones.Resize(input.ReadInt(true)).Items;
				object[] bones = items;
				BoneData[] bonesItems = skeletonData.bones.Items;
				int i = 0;
				int j = skin.bones.Count;
				while (i < j)
				{
					bones[i] = bonesItems[input.ReadInt(true)];
					i++;
				}
				IkConstraintData[] ikConstraintsItems = skeletonData.ikConstraints.Items;
				int k = 0;
				int l = input.ReadInt(true);
				while (k < l)
				{
					skin.constraints.Add(ikConstraintsItems[input.ReadInt(true)]);
					k++;
				}
				TransformConstraintData[] transformConstraintsItems = skeletonData.transformConstraints.Items;
				int m = 0;
				int n = input.ReadInt(true);
				while (m < n)
				{
					skin.constraints.Add(transformConstraintsItems[input.ReadInt(true)]);
					m++;
				}
				PathConstraintData[] pathConstraintsItems = skeletonData.pathConstraints.Items;
				int i2 = 0;
				int n2 = input.ReadInt(true);
				while (i2 < n2)
				{
					skin.constraints.Add(pathConstraintsItems[input.ReadInt(true)]);
					i2++;
				}
				PhysicsConstraintData[] physicsConstraintsItems = skeletonData.physicsConstraints.Items;
				int i3 = 0;
				int n3 = input.ReadInt(true);
				while (i3 < n3)
				{
					skin.constraints.Add(physicsConstraintsItems[input.ReadInt(true)]);
					i3++;
				}
				skin.constraints.TrimExcess();
				slotCount = input.ReadInt(true);
			}
			for (int i4 = 0; i4 < slotCount; i4++)
			{
				int slotIndex = input.ReadInt(true);
				int ii = 0;
				int nn = input.ReadInt(true);
				while (ii < nn)
				{
					string name = input.ReadStringRef();
					Attachment attachment = this.ReadAttachment(input, skeletonData, skin, slotIndex, name, nonessential);
					if (attachment != null)
					{
						skin.SetAttachment(slotIndex, name, attachment);
					}
					ii++;
				}
			}
			return skin;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001340C File Offset: 0x0001160C
		private Attachment ReadAttachment(SkeletonBinary.SkeletonInput input, SkeletonData skeletonData, Skin skin, int slotIndex, string attachmentName, bool nonessential)
		{
			float scale = this.scale;
			int flags = (int)input.ReadUByte();
			string name = (((flags & 8) != 0) ? input.ReadStringRef() : attachmentName);
			switch (flags & 7)
			{
			case 0:
			{
				string path = (((flags & 16) != 0) ? input.ReadStringRef() : null);
				uint color = (uint)(((flags & 32) != 0) ? input.ReadInt() : (-1));
				Sequence sequence = (((flags & 64) != 0) ? this.ReadSequence(input) : null);
				float rotation = (((flags & 128) != 0) ? input.ReadFloat() : 0f);
				float x = input.ReadFloat();
				float y = input.ReadFloat();
				float scaleX = input.ReadFloat();
				float scaleY = input.ReadFloat();
				float width = input.ReadFloat();
				float height = input.ReadFloat();
				if (path == null)
				{
					path = name;
				}
				RegionAttachment region = this.attachmentLoader.NewRegionAttachment(skin, name, path, sequence);
				if (region == null)
				{
					return null;
				}
				region.Path = path;
				region.x = x * scale;
				region.y = y * scale;
				region.scaleX = scaleX;
				region.scaleY = scaleY;
				region.rotation = rotation;
				region.width = width * scale;
				region.height = height * scale;
				region.r = ((color & 4278190080U) >> 24) / 255f;
				region.g = ((color & 16711680U) >> 16) / 255f;
				region.b = ((color & 65280U) >> 8) / 255f;
				region.a = (color & 255U) / 255f;
				region.sequence = sequence;
				if (sequence == null)
				{
					region.UpdateRegion();
				}
				return region;
			}
			case 1:
			{
				SkeletonBinary.Vertices vertices = this.ReadVertices(input, (flags & 16) != 0);
				if (nonessential)
				{
					input.ReadInt();
				}
				BoundingBoxAttachment box = this.attachmentLoader.NewBoundingBoxAttachment(skin, name);
				if (box == null)
				{
					return null;
				}
				box.worldVerticesLength = vertices.length;
				box.vertices = vertices.vertices;
				box.bones = vertices.bones;
				return box;
			}
			case 2:
			{
				string path2 = (((flags & 16) != 0) ? input.ReadStringRef() : name);
				uint color2 = (uint)(((flags & 32) != 0) ? input.ReadInt() : (-1));
				Sequence sequence2 = (((flags & 64) != 0) ? this.ReadSequence(input) : null);
				int hullLength = input.ReadInt(true);
				SkeletonBinary.Vertices vertices2 = this.ReadVertices(input, (flags & 128) != 0);
				float[] uvs = this.ReadFloatArray(input, vertices2.length, 1f);
				int[] triangles = this.ReadShortArray(input, (vertices2.length - hullLength - 2) * 3);
				int[] edges = null;
				float width2 = 0f;
				float height2 = 0f;
				if (nonessential)
				{
					edges = this.ReadShortArray(input, input.ReadInt(true));
					width2 = input.ReadFloat();
					height2 = input.ReadFloat();
				}
				MeshAttachment mesh = this.attachmentLoader.NewMeshAttachment(skin, name, path2, sequence2);
				if (mesh == null)
				{
					return null;
				}
				mesh.Path = path2;
				mesh.r = ((color2 & 4278190080U) >> 24) / 255f;
				mesh.g = ((color2 & 16711680U) >> 16) / 255f;
				mesh.b = ((color2 & 65280U) >> 8) / 255f;
				mesh.a = (color2 & 255U) / 255f;
				mesh.bones = vertices2.bones;
				mesh.vertices = vertices2.vertices;
				mesh.WorldVerticesLength = vertices2.length;
				mesh.triangles = triangles;
				mesh.regionUVs = uvs;
				if (sequence2 == null)
				{
					mesh.UpdateRegion();
				}
				mesh.HullLength = hullLength << 1;
				mesh.Sequence = sequence2;
				if (nonessential)
				{
					mesh.Edges = edges;
					mesh.Width = width2 * scale;
					mesh.Height = height2 * scale;
				}
				return mesh;
			}
			case 3:
			{
				string path3 = (((flags & 16) != 0) ? input.ReadStringRef() : name);
				uint color3 = (uint)(((flags & 32) != 0) ? input.ReadInt() : (-1));
				Sequence sequence3 = (((flags & 64) != 0) ? this.ReadSequence(input) : null);
				bool inheritTimelines = (flags & 128) != 0;
				int skinIndex = input.ReadInt(true);
				string parent = input.ReadStringRef();
				float width3 = 0f;
				float height3 = 0f;
				if (nonessential)
				{
					width3 = input.ReadFloat();
					height3 = input.ReadFloat();
				}
				MeshAttachment mesh2 = this.attachmentLoader.NewMeshAttachment(skin, name, path3, sequence3);
				if (mesh2 == null)
				{
					return null;
				}
				mesh2.Path = path3;
				mesh2.r = ((color3 & 4278190080U) >> 24) / 255f;
				mesh2.g = ((color3 & 16711680U) >> 16) / 255f;
				mesh2.b = ((color3 & 65280U) >> 8) / 255f;
				mesh2.a = (color3 & 255U) / 255f;
				mesh2.Sequence = sequence3;
				if (nonessential)
				{
					mesh2.Width = width3 * scale;
					mesh2.Height = height3 * scale;
				}
				this.linkedMeshes.Add(new SkeletonBinary.LinkedMesh(mesh2, skinIndex, slotIndex, parent, inheritTimelines));
				return mesh2;
			}
			case 4:
			{
				bool closed = (flags & 16) != 0;
				bool constantSpeed = (flags & 32) != 0;
				SkeletonBinary.Vertices vertices3 = this.ReadVertices(input, (flags & 64) != 0);
				float[] lengths = new float[vertices3.length / 6];
				int i = 0;
				int j = lengths.Length;
				while (i < j)
				{
					lengths[i] = input.ReadFloat() * scale;
					i++;
				}
				if (nonessential)
				{
					input.ReadInt();
				}
				PathAttachment path4 = this.attachmentLoader.NewPathAttachment(skin, name);
				if (path4 == null)
				{
					return null;
				}
				path4.closed = closed;
				path4.constantSpeed = constantSpeed;
				path4.worldVerticesLength = vertices3.length;
				path4.vertices = vertices3.vertices;
				path4.bones = vertices3.bones;
				path4.lengths = lengths;
				return path4;
			}
			case 5:
			{
				float rotation2 = input.ReadFloat();
				float x2 = input.ReadFloat();
				float y2 = input.ReadFloat();
				if (nonessential)
				{
					input.ReadInt();
				}
				PointAttachment point = this.attachmentLoader.NewPointAttachment(skin, name);
				if (point == null)
				{
					return null;
				}
				point.x = x2 * scale;
				point.y = y2 * scale;
				point.rotation = rotation2;
				return point;
			}
			case 6:
			{
				int endSlotIndex = input.ReadInt(true);
				SkeletonBinary.Vertices vertices4 = this.ReadVertices(input, (flags & 16) != 0);
				if (nonessential)
				{
					input.ReadInt();
				}
				ClippingAttachment clip = this.attachmentLoader.NewClippingAttachment(skin, name);
				if (clip == null)
				{
					return null;
				}
				clip.EndSlot = skeletonData.slots.Items[endSlotIndex];
				clip.worldVerticesLength = vertices4.length;
				clip.vertices = vertices4.vertices;
				clip.bones = vertices4.bones;
				return clip;
			}
			default:
				return null;
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00013AAB File Offset: 0x00011CAB
		private Sequence ReadSequence(SkeletonBinary.SkeletonInput input)
		{
			return new Sequence(input.ReadInt(true))
			{
				Start = input.ReadInt(true),
				Digits = input.ReadInt(true),
				SetupIndex = input.ReadInt(true)
			};
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x00013AE0 File Offset: 0x00011CE0
		private SkeletonBinary.Vertices ReadVertices(SkeletonBinary.SkeletonInput input, bool weighted)
		{
			float scale = this.scale;
			int vertexCount = input.ReadInt(true);
			SkeletonBinary.Vertices vertices = new SkeletonBinary.Vertices();
			vertices.length = vertexCount << 1;
			if (!weighted)
			{
				vertices.vertices = this.ReadFloatArray(input, vertices.length, scale);
				return vertices;
			}
			ExposedList<float> weights = new ExposedList<float>(vertices.length * 3 * 3);
			ExposedList<int> bonesArray = new ExposedList<int>(vertices.length * 3);
			for (int i = 0; i < vertexCount; i++)
			{
				int boneCount = input.ReadInt(true);
				bonesArray.Add(boneCount);
				for (int ii = 0; ii < boneCount; ii++)
				{
					bonesArray.Add(input.ReadInt(true));
					weights.Add(input.ReadFloat() * scale);
					weights.Add(input.ReadFloat() * scale);
					weights.Add(input.ReadFloat());
				}
			}
			vertices.vertices = weights.ToArray();
			vertices.bones = bonesArray.ToArray();
			return vertices;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00013BC8 File Offset: 0x00011DC8
		private float[] ReadFloatArray(SkeletonBinary.SkeletonInput input, int n, float scale)
		{
			float[] array = new float[n];
			if (scale == 1f)
			{
				for (int i = 0; i < n; i++)
				{
					array[i] = input.ReadFloat();
				}
			}
			else
			{
				for (int j = 0; j < n; j++)
				{
					array[j] = input.ReadFloat() * scale;
				}
			}
			return array;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00013C14 File Offset: 0x00011E14
		private int[] ReadShortArray(SkeletonBinary.SkeletonInput input, int n)
		{
			int[] array = new int[n];
			for (int i = 0; i < n; i++)
			{
				array[i] = input.ReadInt(true);
			}
			return array;
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00013C40 File Offset: 0x00011E40
		private Animation ReadAnimation(string name, SkeletonBinary.SkeletonInput input, SkeletonData skeletonData)
		{
			ExposedList<Timeline> timelines = new ExposedList<Timeline>(input.ReadInt(true));
			float scale = this.scale;
			int i = 0;
			int j = input.ReadInt(true);
			while (i < j)
			{
				int slotIndex = input.ReadInt(true);
				int ii = 0;
				int nn = input.ReadInt(true);
				while (ii < nn)
				{
					int timelineType = (int)input.ReadUByte();
					int frameCount = input.ReadInt(true);
					int frameLast = frameCount - 1;
					switch (timelineType)
					{
					case 0:
					{
						AttachmentTimeline timeline = new AttachmentTimeline(frameCount, slotIndex);
						for (int frame = 0; frame < frameCount; frame++)
						{
							timeline.SetFrame(frame, input.ReadFloat(), input.ReadStringRef());
						}
						timelines.Add(timeline);
						break;
					}
					case 1:
					{
						RGBATimeline timeline2 = new RGBATimeline(frameCount, input.ReadInt(true), slotIndex);
						float time = input.ReadFloat();
						float r = (float)input.Read() / 255f;
						float g = (float)input.Read() / 255f;
						float b = (float)input.Read() / 255f;
						float a = (float)input.Read() / 255f;
						int frame2 = 0;
						int bezier = 0;
						for (;;)
						{
							timeline2.SetFrame(frame2, time, r, g, b, a);
							if (frame2 == frameLast)
							{
								break;
							}
							float time2 = input.ReadFloat();
							float r2 = (float)input.Read() / 255f;
							float g2 = (float)input.Read() / 255f;
							float b2 = (float)input.Read() / 255f;
							float a2 = (float)input.Read() / 255f;
							byte b9 = input.ReadUByte();
							if (b9 != 1)
							{
								if (b9 == 2)
								{
									this.SetBezier(input, timeline2, bezier++, frame2, 0, time, time2, r, r2, 1f);
									this.SetBezier(input, timeline2, bezier++, frame2, 1, time, time2, g, g2, 1f);
									this.SetBezier(input, timeline2, bezier++, frame2, 2, time, time2, b, b2, 1f);
									this.SetBezier(input, timeline2, bezier++, frame2, 3, time, time2, a, a2, 1f);
								}
							}
							else
							{
								timeline2.SetStepped(frame2);
							}
							time = time2;
							r = r2;
							g = g2;
							b = b2;
							a = a2;
							frame2++;
						}
						timelines.Add(timeline2);
						break;
					}
					case 2:
					{
						RGBTimeline timeline3 = new RGBTimeline(frameCount, input.ReadInt(true), slotIndex);
						float time3 = input.ReadFloat();
						float r3 = (float)input.Read() / 255f;
						float g3 = (float)input.Read() / 255f;
						float b3 = (float)input.Read() / 255f;
						int frame3 = 0;
						int bezier2 = 0;
						for (;;)
						{
							timeline3.SetFrame(frame3, time3, r3, g3, b3);
							if (frame3 == frameLast)
							{
								break;
							}
							float time4 = input.ReadFloat();
							float r4 = (float)input.Read() / 255f;
							float g4 = (float)input.Read() / 255f;
							float b4 = (float)input.Read() / 255f;
							byte b9 = input.ReadUByte();
							if (b9 != 1)
							{
								if (b9 == 2)
								{
									this.SetBezier(input, timeline3, bezier2++, frame3, 0, time3, time4, r3, r4, 1f);
									this.SetBezier(input, timeline3, bezier2++, frame3, 1, time3, time4, g3, g4, 1f);
									this.SetBezier(input, timeline3, bezier2++, frame3, 2, time3, time4, b3, b4, 1f);
								}
							}
							else
							{
								timeline3.SetStepped(frame3);
							}
							time3 = time4;
							r3 = r4;
							g3 = g4;
							b3 = b4;
							frame3++;
						}
						timelines.Add(timeline3);
						break;
					}
					case 3:
					{
						RGBA2Timeline timeline4 = new RGBA2Timeline(frameCount, input.ReadInt(true), slotIndex);
						float time5 = input.ReadFloat();
						float r5 = (float)input.Read() / 255f;
						float g5 = (float)input.Read() / 255f;
						float b5 = (float)input.Read() / 255f;
						float a3 = (float)input.Read() / 255f;
						float r6 = (float)input.Read() / 255f;
						float g6 = (float)input.Read() / 255f;
						float b6 = (float)input.Read() / 255f;
						int frame4 = 0;
						int bezier3 = 0;
						for (;;)
						{
							timeline4.SetFrame(frame4, time5, r5, g5, b5, a3, r6, g6, b6);
							if (frame4 == frameLast)
							{
								break;
							}
							float time6 = input.ReadFloat();
							float nr = (float)input.Read() / 255f;
							float ng = (float)input.Read() / 255f;
							float nb = (float)input.Read() / 255f;
							float na = (float)input.Read() / 255f;
							float nr2 = (float)input.Read() / 255f;
							float ng2 = (float)input.Read() / 255f;
							float nb2 = (float)input.Read() / 255f;
							byte b9 = input.ReadUByte();
							if (b9 != 1)
							{
								if (b9 == 2)
								{
									this.SetBezier(input, timeline4, bezier3++, frame4, 0, time5, time6, r5, nr, 1f);
									this.SetBezier(input, timeline4, bezier3++, frame4, 1, time5, time6, g5, ng, 1f);
									this.SetBezier(input, timeline4, bezier3++, frame4, 2, time5, time6, b5, nb, 1f);
									this.SetBezier(input, timeline4, bezier3++, frame4, 3, time5, time6, a3, na, 1f);
									this.SetBezier(input, timeline4, bezier3++, frame4, 4, time5, time6, r6, nr2, 1f);
									this.SetBezier(input, timeline4, bezier3++, frame4, 5, time5, time6, g6, ng2, 1f);
									this.SetBezier(input, timeline4, bezier3++, frame4, 6, time5, time6, b6, nb2, 1f);
								}
							}
							else
							{
								timeline4.SetStepped(frame4);
							}
							time5 = time6;
							r5 = nr;
							g5 = ng;
							b5 = nb;
							a3 = na;
							r6 = nr2;
							g6 = ng2;
							b6 = nb2;
							frame4++;
						}
						timelines.Add(timeline4);
						break;
					}
					case 4:
					{
						RGB2Timeline timeline5 = new RGB2Timeline(frameCount, input.ReadInt(true), slotIndex);
						float time7 = input.ReadFloat();
						float r7 = (float)input.Read() / 255f;
						float g7 = (float)input.Read() / 255f;
						float b7 = (float)input.Read() / 255f;
						float r8 = (float)input.Read() / 255f;
						float g8 = (float)input.Read() / 255f;
						float b8 = (float)input.Read() / 255f;
						int frame5 = 0;
						int bezier4 = 0;
						for (;;)
						{
							timeline5.SetFrame(frame5, time7, r7, g7, b7, r8, g8, b8);
							if (frame5 == frameLast)
							{
								break;
							}
							float time8 = input.ReadFloat();
							float nr3 = (float)input.Read() / 255f;
							float ng3 = (float)input.Read() / 255f;
							float nb3 = (float)input.Read() / 255f;
							float nr4 = (float)input.Read() / 255f;
							float ng4 = (float)input.Read() / 255f;
							float nb4 = (float)input.Read() / 255f;
							byte b9 = input.ReadUByte();
							if (b9 != 1)
							{
								if (b9 == 2)
								{
									this.SetBezier(input, timeline5, bezier4++, frame5, 0, time7, time8, r7, nr3, 1f);
									this.SetBezier(input, timeline5, bezier4++, frame5, 1, time7, time8, g7, ng3, 1f);
									this.SetBezier(input, timeline5, bezier4++, frame5, 2, time7, time8, b7, nb3, 1f);
									this.SetBezier(input, timeline5, bezier4++, frame5, 3, time7, time8, r8, nr4, 1f);
									this.SetBezier(input, timeline5, bezier4++, frame5, 4, time7, time8, g8, ng4, 1f);
									this.SetBezier(input, timeline5, bezier4++, frame5, 5, time7, time8, b8, nb4, 1f);
								}
							}
							else
							{
								timeline5.SetStepped(frame5);
							}
							time7 = time8;
							r7 = nr3;
							g7 = ng3;
							b7 = nb3;
							r8 = nr4;
							g8 = ng4;
							b8 = nb4;
							frame5++;
						}
						timelines.Add(timeline5);
						break;
					}
					case 5:
					{
						AlphaTimeline timeline6 = new AlphaTimeline(frameCount, input.ReadInt(true), slotIndex);
						float time9 = input.ReadFloat();
						float a4 = (float)input.Read() / 255f;
						int frame6 = 0;
						int bezier5 = 0;
						for (;;)
						{
							timeline6.SetFrame(frame6, time9, a4);
							if (frame6 == frameLast)
							{
								break;
							}
							float time10 = input.ReadFloat();
							float a5 = (float)input.Read() / 255f;
							byte b9 = input.ReadUByte();
							if (b9 != 1)
							{
								if (b9 == 2)
								{
									this.SetBezier(input, timeline6, bezier5++, frame6, 0, time9, time10, a4, a5, 1f);
								}
							}
							else
							{
								timeline6.SetStepped(frame6);
							}
							time9 = time10;
							a4 = a5;
							frame6++;
						}
						timelines.Add(timeline6);
						break;
					}
					}
					ii++;
				}
				i++;
			}
			int k = 0;
			int l = input.ReadInt(true);
			while (k < l)
			{
				int boneIndex = input.ReadInt(true);
				int ii2 = 0;
				int nn2 = input.ReadInt(true);
				while (ii2 < nn2)
				{
					int type = (int)input.ReadUByte();
					int frameCount2 = input.ReadInt(true);
					if (type == 10)
					{
						InheritTimeline timeline7 = new InheritTimeline(frameCount2, boneIndex);
						for (int frame7 = 0; frame7 < frameCount2; frame7++)
						{
							timeline7.SetFrame(frame7, input.ReadFloat(), InheritEnum.Values[(int)input.ReadUByte()]);
						}
						timelines.Add(timeline7);
					}
					else
					{
						int bezierCount = input.ReadInt(true);
						switch (type)
						{
						case 0:
							this.ReadTimeline(input, timelines, new RotateTimeline(frameCount2, bezierCount, boneIndex), 1f);
							break;
						case 1:
							this.ReadTimeline(input, timelines, new TranslateTimeline(frameCount2, bezierCount, boneIndex), scale);
							break;
						case 2:
							this.ReadTimeline(input, timelines, new TranslateXTimeline(frameCount2, bezierCount, boneIndex), scale);
							break;
						case 3:
							this.ReadTimeline(input, timelines, new TranslateYTimeline(frameCount2, bezierCount, boneIndex), scale);
							break;
						case 4:
							this.ReadTimeline(input, timelines, new ScaleTimeline(frameCount2, bezierCount, boneIndex), 1f);
							break;
						case 5:
							this.ReadTimeline(input, timelines, new ScaleXTimeline(frameCount2, bezierCount, boneIndex), 1f);
							break;
						case 6:
							this.ReadTimeline(input, timelines, new ScaleYTimeline(frameCount2, bezierCount, boneIndex), 1f);
							break;
						case 7:
							this.ReadTimeline(input, timelines, new ShearTimeline(frameCount2, bezierCount, boneIndex), 1f);
							break;
						case 8:
							this.ReadTimeline(input, timelines, new ShearXTimeline(frameCount2, bezierCount, boneIndex), 1f);
							break;
						case 9:
							this.ReadTimeline(input, timelines, new ShearYTimeline(frameCount2, bezierCount, boneIndex), 1f);
							break;
						}
					}
					ii2++;
				}
				k++;
			}
			int m = 0;
			int n = input.ReadInt(true);
			while (m < n)
			{
				int index = input.ReadInt(true);
				int num = input.ReadInt(true);
				int frameLast2 = num - 1;
				IkConstraintTimeline timeline8 = new IkConstraintTimeline(num, input.ReadInt(true), index);
				int flags = input.Read();
				float time11 = input.ReadFloat();
				float mix = (((flags & 1) != 0) ? (((flags & 2) != 0) ? input.ReadFloat() : 1f) : 0f);
				float softness = (((flags & 4) != 0) ? (input.ReadFloat() * scale) : 0f);
				int frame8 = 0;
				int bezier6 = 0;
				for (;;)
				{
					timeline8.SetFrame(frame8, time11, mix, softness, ((flags & 8) != 0) ? 1 : (-1), (flags & 16) != 0, (flags & 32) != 0);
					if (frame8 == frameLast2)
					{
						break;
					}
					flags = input.Read();
					float time12 = input.ReadFloat();
					float mix2 = (((flags & 1) != 0) ? (((flags & 2) != 0) ? input.ReadFloat() : 1f) : 0f);
					float softness2 = (((flags & 4) != 0) ? (input.ReadFloat() * scale) : 0f);
					if ((flags & 64) != 0)
					{
						timeline8.SetStepped(frame8);
					}
					else if ((flags & 128) != 0)
					{
						this.SetBezier(input, timeline8, bezier6++, frame8, 0, time11, time12, mix, mix2, 1f);
						this.SetBezier(input, timeline8, bezier6++, frame8, 1, time11, time12, softness, softness2, scale);
					}
					time11 = time12;
					mix = mix2;
					softness = softness2;
					frame8++;
				}
				timelines.Add(timeline8);
				m++;
			}
			int i2 = 0;
			int n2 = input.ReadInt(true);
			while (i2 < n2)
			{
				int index2 = input.ReadInt(true);
				int num2 = input.ReadInt(true);
				int frameLast3 = num2 - 1;
				TransformConstraintTimeline timeline9 = new TransformConstraintTimeline(num2, input.ReadInt(true), index2);
				float time13 = input.ReadFloat();
				float mixRotate = input.ReadFloat();
				float mixX = input.ReadFloat();
				float mixY = input.ReadFloat();
				float mixScaleX = input.ReadFloat();
				float mixScaleY = input.ReadFloat();
				float mixShearY = input.ReadFloat();
				int frame9 = 0;
				int bezier7 = 0;
				for (;;)
				{
					timeline9.SetFrame(frame9, time13, mixRotate, mixX, mixY, mixScaleX, mixScaleY, mixShearY);
					if (frame9 == frameLast3)
					{
						break;
					}
					float time14 = input.ReadFloat();
					float mixRotate2 = input.ReadFloat();
					float mixX2 = input.ReadFloat();
					float mixY2 = input.ReadFloat();
					float mixScaleX2 = input.ReadFloat();
					float mixScaleY2 = input.ReadFloat();
					float mixShearY2 = input.ReadFloat();
					byte b9 = input.ReadUByte();
					if (b9 != 1)
					{
						if (b9 == 2)
						{
							this.SetBezier(input, timeline9, bezier7++, frame9, 0, time13, time14, mixRotate, mixRotate2, 1f);
							this.SetBezier(input, timeline9, bezier7++, frame9, 1, time13, time14, mixX, mixX2, 1f);
							this.SetBezier(input, timeline9, bezier7++, frame9, 2, time13, time14, mixY, mixY2, 1f);
							this.SetBezier(input, timeline9, bezier7++, frame9, 3, time13, time14, mixScaleX, mixScaleX2, 1f);
							this.SetBezier(input, timeline9, bezier7++, frame9, 4, time13, time14, mixScaleY, mixScaleY2, 1f);
							this.SetBezier(input, timeline9, bezier7++, frame9, 5, time13, time14, mixShearY, mixShearY2, 1f);
						}
					}
					else
					{
						timeline9.SetStepped(frame9);
					}
					time13 = time14;
					mixRotate = mixRotate2;
					mixX = mixX2;
					mixY = mixY2;
					mixScaleX = mixScaleX2;
					mixScaleY = mixScaleY2;
					mixShearY = mixShearY2;
					frame9++;
				}
				timelines.Add(timeline9);
				i2++;
			}
			int i3 = 0;
			int n3 = input.ReadInt(true);
			while (i3 < n3)
			{
				int index3 = input.ReadInt(true);
				PathConstraintData data = skeletonData.pathConstraints.Items[index3];
				int ii3 = 0;
				int nn3 = input.ReadInt(true);
				while (ii3 < nn3)
				{
					int type2 = (int)input.ReadUByte();
					int frameCount3 = input.ReadInt(true);
					int bezierCount2 = input.ReadInt(true);
					switch (type2)
					{
					case 0:
						this.ReadTimeline(input, timelines, new PathConstraintPositionTimeline(frameCount3, bezierCount2, index3), (data.positionMode == PositionMode.Fixed) ? scale : 1f);
						break;
					case 1:
						this.ReadTimeline(input, timelines, new PathConstraintSpacingTimeline(frameCount3, bezierCount2, index3), (data.spacingMode == SpacingMode.Length || data.spacingMode == SpacingMode.Fixed) ? scale : 1f);
						break;
					case 2:
					{
						PathConstraintMixTimeline timeline10 = new PathConstraintMixTimeline(frameCount3, bezierCount2, index3);
						float time15 = input.ReadFloat();
						float mixRotate3 = input.ReadFloat();
						float mixX3 = input.ReadFloat();
						float mixY3 = input.ReadFloat();
						int frame10 = 0;
						int bezier8 = 0;
						int frameLast4 = timeline10.FrameCount - 1;
						for (;;)
						{
							timeline10.SetFrame(frame10, time15, mixRotate3, mixX3, mixY3);
							if (frame10 == frameLast4)
							{
								break;
							}
							float time16 = input.ReadFloat();
							float mixRotate4 = input.ReadFloat();
							float mixX4 = input.ReadFloat();
							float mixY4 = input.ReadFloat();
							byte b9 = input.ReadUByte();
							if (b9 != 1)
							{
								if (b9 == 2)
								{
									this.SetBezier(input, timeline10, bezier8++, frame10, 0, time15, time16, mixRotate3, mixRotate4, 1f);
									this.SetBezier(input, timeline10, bezier8++, frame10, 1, time15, time16, mixX3, mixX4, 1f);
									this.SetBezier(input, timeline10, bezier8++, frame10, 2, time15, time16, mixY3, mixY4, 1f);
								}
							}
							else
							{
								timeline10.SetStepped(frame10);
							}
							time15 = time16;
							mixRotate3 = mixRotate4;
							mixX3 = mixX4;
							mixY3 = mixY4;
							frame10++;
						}
						timelines.Add(timeline10);
						break;
					}
					}
					ii3++;
				}
				i3++;
			}
			int i4 = 0;
			int n4 = input.ReadInt(true);
			while (i4 < n4)
			{
				int index4 = input.ReadInt(true) - 1;
				int ii4 = 0;
				int nn4 = input.ReadInt(true);
				while (ii4 < nn4)
				{
					int type3 = (int)input.ReadUByte();
					int frameCount4 = input.ReadInt(true);
					if (type3 == 8)
					{
						PhysicsConstraintResetTimeline timeline11 = new PhysicsConstraintResetTimeline(frameCount4, index4);
						for (int frame11 = 0; frame11 < frameCount4; frame11++)
						{
							timeline11.SetFrame(frame11, input.ReadFloat());
						}
						timelines.Add(timeline11);
					}
					else
					{
						int bezierCount3 = input.ReadInt(true);
						switch (type3)
						{
						case 0:
							this.ReadTimeline(input, timelines, new PhysicsConstraintInertiaTimeline(frameCount4, bezierCount3, index4), 1f);
							break;
						case 1:
							this.ReadTimeline(input, timelines, new PhysicsConstraintStrengthTimeline(frameCount4, bezierCount3, index4), 1f);
							break;
						case 2:
							this.ReadTimeline(input, timelines, new PhysicsConstraintDampingTimeline(frameCount4, bezierCount3, index4), 1f);
							break;
						case 4:
							this.ReadTimeline(input, timelines, new PhysicsConstraintMassTimeline(frameCount4, bezierCount3, index4), 1f);
							break;
						case 5:
							this.ReadTimeline(input, timelines, new PhysicsConstraintWindTimeline(frameCount4, bezierCount3, index4), 1f);
							break;
						case 6:
							this.ReadTimeline(input, timelines, new PhysicsConstraintGravityTimeline(frameCount4, bezierCount3, index4), 1f);
							break;
						case 7:
							this.ReadTimeline(input, timelines, new PhysicsConstraintMixTimeline(frameCount4, bezierCount3, index4), 1f);
							break;
						}
					}
					ii4++;
				}
				i4++;
			}
			int i5 = 0;
			int n5 = input.ReadInt(true);
			while (i5 < n5)
			{
				Skin skin = skeletonData.skins.Items[input.ReadInt(true)];
				int ii5 = 0;
				int nn5 = input.ReadInt(true);
				while (ii5 < nn5)
				{
					int slotIndex2 = input.ReadInt(true);
					int iii = 0;
					int nnn = input.ReadInt(true);
					while (iii < nnn)
					{
						string attachmentName = input.ReadStringRef();
						Attachment attachment = skin.GetAttachment(slotIndex2, attachmentName);
						if (attachment == null)
						{
							throw new SerializationException("Timeline attachment not found: " + attachmentName);
						}
						int timelineType2 = (int)input.ReadUByte();
						int frameCount5 = input.ReadInt(true);
						int frameLast5 = frameCount5 - 1;
						if (timelineType2 != 0)
						{
							if (timelineType2 == 1)
							{
								SequenceTimeline timeline12 = new SequenceTimeline(frameCount5, slotIndex2, attachment);
								for (int frame12 = 0; frame12 < frameCount5; frame12++)
								{
									float time17 = input.ReadFloat();
									int modeAndIndex = input.ReadInt();
									timeline12.SetFrame(frame12, time17, (SequenceMode)(modeAndIndex & 15), modeAndIndex >> 4, input.ReadFloat());
								}
								timelines.Add(timeline12);
							}
						}
						else
						{
							VertexAttachment vertexAttachment = (VertexAttachment)attachment;
							bool weighted = vertexAttachment.Bones != null;
							float[] vertices = vertexAttachment.Vertices;
							int deformLength = (weighted ? (vertices.Length / 3 << 1) : vertices.Length);
							DeformTimeline timeline13 = new DeformTimeline(frameCount5, input.ReadInt(true), slotIndex2, vertexAttachment);
							float time18 = input.ReadFloat();
							int frame13 = 0;
							int bezier9 = 0;
							for (;;)
							{
								int end = input.ReadInt(true);
								float[] deform;
								if (end == 0)
								{
									deform = (weighted ? new float[deformLength] : vertices);
								}
								else
								{
									deform = new float[deformLength];
									int start = input.ReadInt(true);
									end += start;
									if (scale == 1f)
									{
										for (int v = start; v < end; v++)
										{
											deform[v] = input.ReadFloat();
										}
									}
									else
									{
										for (int v2 = start; v2 < end; v2++)
										{
											deform[v2] = input.ReadFloat() * scale;
										}
									}
									if (!weighted)
									{
										int v3 = 0;
										int vn = deform.Length;
										while (v3 < vn)
										{
											deform[v3] += vertices[v3];
											v3++;
										}
									}
								}
								timeline13.SetFrame(frame13, time18, deform);
								if (frame13 == frameLast5)
								{
									break;
								}
								float time19 = input.ReadFloat();
								byte b9 = input.ReadUByte();
								if (b9 != 1)
								{
									if (b9 == 2)
									{
										this.SetBezier(input, timeline13, bezier9++, frame13, 0, time18, time19, 0f, 1f, 1f);
									}
								}
								else
								{
									timeline13.SetStepped(frame13);
								}
								time18 = time19;
								frame13++;
							}
							timelines.Add(timeline13);
						}
						iii++;
					}
					ii5++;
				}
				i5++;
			}
			int drawOrderCount = input.ReadInt(true);
			if (drawOrderCount > 0)
			{
				DrawOrderTimeline timeline14 = new DrawOrderTimeline(drawOrderCount);
				int slotCount = skeletonData.slots.Count;
				for (int i6 = 0; i6 < drawOrderCount; i6++)
				{
					float time20 = input.ReadFloat();
					int offsetCount = input.ReadInt(true);
					int[] drawOrder = new int[slotCount];
					for (int ii6 = slotCount - 1; ii6 >= 0; ii6--)
					{
						drawOrder[ii6] = -1;
					}
					int[] unchanged = new int[slotCount - offsetCount];
					int originalIndex = 0;
					int unchangedIndex = 0;
					for (int ii7 = 0; ii7 < offsetCount; ii7++)
					{
						int slotIndex3 = input.ReadInt(true);
						while (originalIndex != slotIndex3)
						{
							unchanged[unchangedIndex++] = originalIndex++;
						}
						drawOrder[originalIndex + input.ReadInt(true)] = originalIndex++;
					}
					while (originalIndex < slotCount)
					{
						unchanged[unchangedIndex++] = originalIndex++;
					}
					for (int ii8 = slotCount - 1; ii8 >= 0; ii8--)
					{
						if (drawOrder[ii8] == -1)
						{
							drawOrder[ii8] = unchanged[--unchangedIndex];
						}
					}
					timeline14.SetFrame(i6, time20, drawOrder);
				}
				timelines.Add(timeline14);
			}
			int eventCount = input.ReadInt(true);
			if (eventCount > 0)
			{
				EventTimeline timeline15 = new EventTimeline(eventCount);
				for (int i7 = 0; i7 < eventCount; i7++)
				{
					float num3 = input.ReadFloat();
					EventData eventData = skeletonData.events.Items[input.ReadInt(true)];
					Event e = new Event(num3, eventData);
					e.intValue = input.ReadInt(false);
					e.floatValue = input.ReadFloat();
					e.stringValue = input.ReadString();
					if (e.stringValue == null)
					{
						e.stringValue = eventData.String;
					}
					if (e.Data.AudioPath != null)
					{
						e.volume = input.ReadFloat();
						e.balance = input.ReadFloat();
					}
					timeline15.SetFrame(i7, e);
				}
				timelines.Add(timeline15);
			}
			float duration = 0f;
			Timeline[] items = timelines.Items;
			int i8 = 0;
			int n6 = timelines.Count;
			while (i8 < n6)
			{
				duration = Math.Max(duration, items[i8].Duration);
				i8++;
			}
			return new Animation(name, timelines, duration);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000152C0 File Offset: 0x000134C0
		private void ReadTimeline(SkeletonBinary.SkeletonInput input, ExposedList<Timeline> timelines, CurveTimeline1 timeline, float scale)
		{
			float time = input.ReadFloat();
			float value = input.ReadFloat() * scale;
			int frame = 0;
			int bezier = 0;
			int frameLast = timeline.FrameCount - 1;
			for (;;)
			{
				timeline.SetFrame(frame, time, value);
				if (frame == frameLast)
				{
					break;
				}
				float time2 = input.ReadFloat();
				float value2 = input.ReadFloat() * scale;
				byte b = input.ReadUByte();
				if (b != 1)
				{
					if (b == 2)
					{
						this.SetBezier(input, timeline, bezier++, frame, 0, time, time2, value, value2, scale);
					}
				}
				else
				{
					timeline.SetStepped(frame);
				}
				time = time2;
				value = value2;
				frame++;
			}
			timelines.Add(timeline);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00015354 File Offset: 0x00013554
		private void ReadTimeline(SkeletonBinary.SkeletonInput input, ExposedList<Timeline> timelines, CurveTimeline2 timeline, float scale)
		{
			float time = input.ReadFloat();
			float value = input.ReadFloat() * scale;
			float value2 = input.ReadFloat() * scale;
			int frame = 0;
			int bezier = 0;
			int frameLast = timeline.FrameCount - 1;
			for (;;)
			{
				timeline.SetFrame(frame, time, value, value2);
				if (frame == frameLast)
				{
					break;
				}
				float time2 = input.ReadFloat();
				float nvalue = input.ReadFloat() * scale;
				float nvalue2 = input.ReadFloat() * scale;
				byte b = input.ReadUByte();
				if (b != 1)
				{
					if (b == 2)
					{
						this.SetBezier(input, timeline, bezier++, frame, 0, time, time2, value, nvalue, scale);
						this.SetBezier(input, timeline, bezier++, frame, 1, time, time2, value2, nvalue2, scale);
					}
				}
				else
				{
					timeline.SetStepped(frame);
				}
				time = time2;
				value = nvalue;
				value2 = nvalue2;
				frame++;
			}
			timelines.Add(timeline);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x00015420 File Offset: 0x00013620
		private void SetBezier(SkeletonBinary.SkeletonInput input, CurveTimeline timeline, int bezier, int frame, int value, float time1, float time2, float value1, float value2, float scale)
		{
			timeline.SetBezier(bezier, frame, value, time1, value1, input.ReadFloat(), input.ReadFloat() * scale, input.ReadFloat(), input.ReadFloat() * scale, time2, value2);
		}

		// Token: 0x04000257 RID: 599
		public const int BONE_ROTATE = 0;

		// Token: 0x04000258 RID: 600
		public const int BONE_TRANSLATE = 1;

		// Token: 0x04000259 RID: 601
		public const int BONE_TRANSLATEX = 2;

		// Token: 0x0400025A RID: 602
		public const int BONE_TRANSLATEY = 3;

		// Token: 0x0400025B RID: 603
		public const int BONE_SCALE = 4;

		// Token: 0x0400025C RID: 604
		public const int BONE_SCALEX = 5;

		// Token: 0x0400025D RID: 605
		public const int BONE_SCALEY = 6;

		// Token: 0x0400025E RID: 606
		public const int BONE_SHEAR = 7;

		// Token: 0x0400025F RID: 607
		public const int BONE_SHEARX = 8;

		// Token: 0x04000260 RID: 608
		public const int BONE_SHEARY = 9;

		// Token: 0x04000261 RID: 609
		public const int BONE_INHERIT = 10;

		// Token: 0x04000262 RID: 610
		public const int SLOT_ATTACHMENT = 0;

		// Token: 0x04000263 RID: 611
		public const int SLOT_RGBA = 1;

		// Token: 0x04000264 RID: 612
		public const int SLOT_RGB = 2;

		// Token: 0x04000265 RID: 613
		public const int SLOT_RGBA2 = 3;

		// Token: 0x04000266 RID: 614
		public const int SLOT_RGB2 = 4;

		// Token: 0x04000267 RID: 615
		public const int SLOT_ALPHA = 5;

		// Token: 0x04000268 RID: 616
		public const int ATTACHMENT_DEFORM = 0;

		// Token: 0x04000269 RID: 617
		public const int ATTACHMENT_SEQUENCE = 1;

		// Token: 0x0400026A RID: 618
		public const int PATH_POSITION = 0;

		// Token: 0x0400026B RID: 619
		public const int PATH_SPACING = 1;

		// Token: 0x0400026C RID: 620
		public const int PATH_MIX = 2;

		// Token: 0x0400026D RID: 621
		public const int PHYSICS_INERTIA = 0;

		// Token: 0x0400026E RID: 622
		public const int PHYSICS_STRENGTH = 1;

		// Token: 0x0400026F RID: 623
		public const int PHYSICS_DAMPING = 2;

		// Token: 0x04000270 RID: 624
		public const int PHYSICS_MASS = 4;

		// Token: 0x04000271 RID: 625
		public const int PHYSICS_WIND = 5;

		// Token: 0x04000272 RID: 626
		public const int PHYSICS_GRAVITY = 6;

		// Token: 0x04000273 RID: 627
		public const int PHYSICS_MIX = 7;

		// Token: 0x04000274 RID: 628
		public const int PHYSICS_RESET = 8;

		// Token: 0x04000275 RID: 629
		public const int CURVE_LINEAR = 0;

		// Token: 0x04000276 RID: 630
		public const int CURVE_STEPPED = 1;

		// Token: 0x04000277 RID: 631
		public const int CURVE_BEZIER = 2;

		// Token: 0x04000278 RID: 632
		private readonly List<SkeletonBinary.LinkedMesh> linkedMeshes = new List<SkeletonBinary.LinkedMesh>();

		// Token: 0x02000073 RID: 115
		internal class Vertices
		{
			// Token: 0x04000279 RID: 633
			public int length;

			// Token: 0x0400027A RID: 634
			public int[] bones;

			// Token: 0x0400027B RID: 635
			public float[] vertices;
		}

		// Token: 0x02000074 RID: 116
		internal class SkeletonInput
		{
			// Token: 0x0600042D RID: 1069 RVA: 0x0001545E File Offset: 0x0001365E
			public SkeletonInput(Stream input)
			{
				this.input = input;
			}

			// Token: 0x0600042E RID: 1070 RVA: 0x00015486 File Offset: 0x00013686
			public int Read()
			{
				return this.input.ReadByte();
			}

			// Token: 0x0600042F RID: 1071 RVA: 0x00015493 File Offset: 0x00013693
			public byte ReadUByte()
			{
				return (byte)this.input.ReadByte();
			}

			// Token: 0x06000430 RID: 1072 RVA: 0x000154A1 File Offset: 0x000136A1
			public sbyte ReadSByte()
			{
				int num = this.input.ReadByte();
				if (num == -1)
				{
					throw new EndOfStreamException();
				}
				return (sbyte)num;
			}

			// Token: 0x06000431 RID: 1073 RVA: 0x000154B9 File Offset: 0x000136B9
			public bool ReadBoolean()
			{
				return this.input.ReadByte() != 0;
			}

			// Token: 0x06000432 RID: 1074 RVA: 0x000154CC File Offset: 0x000136CC
			public float ReadFloat()
			{
				this.input.Read(this.bytesBigEndian, 0, 4);
				this.chars[3] = this.bytesBigEndian[0];
				this.chars[2] = this.bytesBigEndian[1];
				this.chars[1] = this.bytesBigEndian[2];
				this.chars[0] = this.bytesBigEndian[3];
				return BitConverter.ToSingle(this.chars, 0);
			}

			// Token: 0x06000433 RID: 1075 RVA: 0x0001553C File Offset: 0x0001373C
			public int ReadInt()
			{
				this.input.Read(this.bytesBigEndian, 0, 4);
				return ((int)this.bytesBigEndian[0] << 24) + ((int)this.bytesBigEndian[1] << 16) + ((int)this.bytesBigEndian[2] << 8) + (int)this.bytesBigEndian[3];
			}

			// Token: 0x06000434 RID: 1076 RVA: 0x00015588 File Offset: 0x00013788
			public long ReadLong()
			{
				this.input.Read(this.bytesBigEndian, 0, 8);
				return (long)(((ulong)this.bytesBigEndian[0] << 56) + ((ulong)this.bytesBigEndian[1] << 48) + ((ulong)this.bytesBigEndian[2] << 40) + ((ulong)this.bytesBigEndian[3] << 32) + ((ulong)this.bytesBigEndian[4] << 24) + ((ulong)this.bytesBigEndian[5] << 16) + ((ulong)this.bytesBigEndian[6] << 8) + (ulong)this.bytesBigEndian[7]);
			}

			// Token: 0x06000435 RID: 1077 RVA: 0x0001560C File Offset: 0x0001380C
			public int ReadInt(bool optimizePositive)
			{
				int b = this.input.ReadByte();
				int result = b & 127;
				if ((b & 128) != 0)
				{
					b = this.input.ReadByte();
					result |= (b & 127) << 7;
					if ((b & 128) != 0)
					{
						b = this.input.ReadByte();
						result |= (b & 127) << 14;
						if ((b & 128) != 0)
						{
							b = this.input.ReadByte();
							result |= (b & 127) << 21;
							if ((b & 128) != 0)
							{
								result |= (this.input.ReadByte() & 127) << 28;
							}
						}
					}
				}
				if (!optimizePositive)
				{
					return (result >> 1) ^ -(result & 1);
				}
				return result;
			}

			// Token: 0x06000436 RID: 1078 RVA: 0x000156B0 File Offset: 0x000138B0
			public string ReadString()
			{
				int byteCount = this.ReadInt(true);
				if (byteCount == 0)
				{
					return null;
				}
				if (byteCount != 1)
				{
					byteCount--;
					byte[] buffer = this.chars;
					if (buffer.Length < byteCount)
					{
						buffer = new byte[byteCount];
					}
					this.ReadFully(buffer, 0, byteCount);
					return Encoding.UTF8.GetString(buffer, 0, byteCount);
				}
				return "";
			}

			// Token: 0x06000437 RID: 1079 RVA: 0x00015704 File Offset: 0x00013904
			public string ReadStringRef()
			{
				int index = this.ReadInt(true);
				if (index != 0)
				{
					return this.strings[index - 1];
				}
				return null;
			}

			// Token: 0x06000438 RID: 1080 RVA: 0x00015728 File Offset: 0x00013928
			public void ReadFully(byte[] buffer, int offset, int length)
			{
				while (length > 0)
				{
					int count = this.input.Read(buffer, offset, length);
					if (count <= 0)
					{
						throw new EndOfStreamException();
					}
					offset += count;
					length -= count;
				}
			}

			// Token: 0x06000439 RID: 1081 RVA: 0x00015760 File Offset: 0x00013960
			public string GetVersionString()
			{
				string versionStringOld3X;
				try
				{
					long initialPosition = this.input.Position;
					this.ReadLong();
					long stringPosition = this.input.Position;
					int num = this.ReadInt(true);
					this.input.Position = stringPosition;
					if (num <= 13)
					{
						string version = this.ReadString();
						if (char.IsDigit(version[0]))
						{
							return version;
						}
					}
					this.input.Position = initialPosition;
					versionStringOld3X = this.GetVersionStringOld3X();
				}
				catch (Exception e)
				{
					string text = "Stream does not contain valid binary Skeleton Data.\n";
					Exception ex = e;
					throw new ArgumentException(text + ((ex != null) ? ex.ToString() : null), "input");
				}
				return versionStringOld3X;
			}

			// Token: 0x0600043A RID: 1082 RVA: 0x00015808 File Offset: 0x00013A08
			public string GetVersionStringOld3X()
			{
				int byteCount = this.ReadInt(true);
				if (byteCount > 1)
				{
					this.input.Position += (long)(byteCount - 1);
				}
				byteCount = this.ReadInt(true);
				if (byteCount > 1 && byteCount <= 13)
				{
					byteCount--;
					byte[] buffer = new byte[byteCount];
					this.ReadFully(buffer, 0, byteCount);
					return Encoding.UTF8.GetString(buffer, 0, byteCount);
				}
				throw new ArgumentException("Stream does not contain valid binary Skeleton Data.");
			}

			// Token: 0x0400027C RID: 636
			private byte[] chars = new byte[32];

			// Token: 0x0400027D RID: 637
			private byte[] bytesBigEndian = new byte[8];

			// Token: 0x0400027E RID: 638
			internal string[] strings;

			// Token: 0x0400027F RID: 639
			private Stream input;
		}

		// Token: 0x02000075 RID: 117
		private class LinkedMesh
		{
			// Token: 0x0600043B RID: 1083 RVA: 0x00015874 File Offset: 0x00013A74
			public LinkedMesh(MeshAttachment mesh, int skinIndex, int slotIndex, string parent, bool inheritTimelines)
			{
				this.mesh = mesh;
				this.skinIndex = skinIndex;
				this.slotIndex = slotIndex;
				this.parent = parent;
				this.inheritTimelines = inheritTimelines;
			}

			// Token: 0x04000280 RID: 640
			internal string parent;

			// Token: 0x04000281 RID: 641
			internal int skinIndex;

			// Token: 0x04000282 RID: 642
			internal int slotIndex;

			// Token: 0x04000283 RID: 643
			internal MeshAttachment mesh;

			// Token: 0x04000284 RID: 644
			internal bool inheritTimelines;
		}
	}
}
