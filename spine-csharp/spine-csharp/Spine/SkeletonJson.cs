using System;
using System.Collections.Generic;
using System.IO;

namespace Spine
{
	// Token: 0x0200007A RID: 122
	public class SkeletonJson : SkeletonLoader
	{
		// Token: 0x06000499 RID: 1177 RVA: 0x00016F95 File Offset: 0x00015195
		public SkeletonJson(AttachmentLoader attachmentLoader)
			: base(attachmentLoader)
		{
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00016FA9 File Offset: 0x000151A9
		public SkeletonJson(params Atlas[] atlasArray)
			: base(atlasArray)
		{
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00016FC0 File Offset: 0x000151C0
		public override SkeletonData ReadSkeletonData(string path)
		{
			SkeletonData skeletonData2;
			using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)))
			{
				SkeletonData skeletonData = this.ReadSkeletonData(reader);
				skeletonData.name = Path.GetFileNameWithoutExtension(path);
				skeletonData2 = skeletonData;
			}
			return skeletonData2;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00017010 File Offset: 0x00015210
		public SkeletonData ReadSkeletonData(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader", "reader cannot be null.");
			}
			float scale = this.scale;
			SkeletonData skeletonData = new SkeletonData();
			Dictionary<string, object> root = Json.Deserialize(reader) as Dictionary<string, object>;
			if (root == null)
			{
				throw new Exception("Invalid JSON.");
			}
			if (root.ContainsKey("skeleton"))
			{
				Dictionary<string, object> skeletonMap = (Dictionary<string, object>)root["skeleton"];
				skeletonData.hash = (string)skeletonMap["hash"];
				skeletonData.version = (string)skeletonMap["spine"];
				skeletonData.x = SkeletonJson.GetFloat(skeletonMap, "x", 0f);
				skeletonData.y = SkeletonJson.GetFloat(skeletonMap, "y", 0f);
				skeletonData.width = SkeletonJson.GetFloat(skeletonMap, "width", 0f);
				skeletonData.height = SkeletonJson.GetFloat(skeletonMap, "height", 0f);
				skeletonData.referenceScale = SkeletonJson.GetFloat(skeletonMap, "referenceScale", 100f) * scale;
				skeletonData.fps = SkeletonJson.GetFloat(skeletonMap, "fps", 30f);
				skeletonData.imagesPath = SkeletonJson.GetString(skeletonMap, "images", null);
				skeletonData.audioPath = SkeletonJson.GetString(skeletonMap, "audio", null);
			}
			if (root.ContainsKey("bones"))
			{
				foreach (object obj in ((List<object>)root["bones"]))
				{
					Dictionary<string, object> boneMap = (Dictionary<string, object>)obj;
					BoneData parent = null;
					if (boneMap.ContainsKey("parent"))
					{
						parent = skeletonData.FindBone((string)boneMap["parent"]);
						if (parent == null)
						{
							string text = "Parent bone not found: ";
							object obj2 = boneMap["parent"];
							throw new Exception(text + ((obj2 != null) ? obj2.ToString() : null));
						}
					}
					BoneData data = new BoneData(skeletonData.Bones.Count, (string)boneMap["name"], parent);
					data.length = SkeletonJson.GetFloat(boneMap, "length", 0f) * scale;
					data.x = SkeletonJson.GetFloat(boneMap, "x", 0f) * scale;
					data.y = SkeletonJson.GetFloat(boneMap, "y", 0f) * scale;
					data.rotation = SkeletonJson.GetFloat(boneMap, "rotation", 0f);
					data.scaleX = SkeletonJson.GetFloat(boneMap, "scaleX", 1f);
					data.scaleY = SkeletonJson.GetFloat(boneMap, "scaleY", 1f);
					data.shearX = SkeletonJson.GetFloat(boneMap, "shearX", 0f);
					data.shearY = SkeletonJson.GetFloat(boneMap, "shearY", 0f);
					string inheritString = SkeletonJson.GetString(boneMap, "inherit", Inherit.Normal.ToString());
					data.inherit = (Inherit)Enum.Parse(typeof(Inherit), inheritString, true);
					data.skinRequired = SkeletonJson.GetBoolean(boneMap, "skin", false);
					skeletonData.bones.Add(data);
				}
			}
			if (root.ContainsKey("slots"))
			{
				foreach (object obj3 in ((List<object>)root["slots"]))
				{
					Dictionary<string, object> slotMap = (Dictionary<string, object>)obj3;
					string slotName = (string)slotMap["name"];
					string boneName = (string)slotMap["bone"];
					BoneData boneData = skeletonData.FindBone(boneName);
					if (boneData == null)
					{
						throw new Exception("Slot bone not found: " + boneName);
					}
					SlotData data2 = new SlotData(skeletonData.Slots.Count, slotName, boneData);
					if (slotMap.ContainsKey("color"))
					{
						string color = (string)slotMap["color"];
						data2.r = SkeletonJson.ToColor(color, 0, 8);
						data2.g = SkeletonJson.ToColor(color, 1, 8);
						data2.b = SkeletonJson.ToColor(color, 2, 8);
						data2.a = SkeletonJson.ToColor(color, 3, 8);
					}
					if (slotMap.ContainsKey("dark"))
					{
						string color2 = (string)slotMap["dark"];
						data2.r2 = SkeletonJson.ToColor(color2, 0, 6);
						data2.g2 = SkeletonJson.ToColor(color2, 1, 6);
						data2.b2 = SkeletonJson.ToColor(color2, 2, 6);
						data2.hasSecondColor = true;
					}
					data2.attachmentName = SkeletonJson.GetString(slotMap, "attachment", null);
					if (slotMap.ContainsKey("blend"))
					{
						data2.blendMode = (BlendMode)Enum.Parse(typeof(BlendMode), (string)slotMap["blend"], true);
					}
					else
					{
						data2.blendMode = BlendMode.Normal;
					}
					skeletonData.slots.Add(data2);
				}
			}
			if (root.ContainsKey("ik"))
			{
				foreach (object obj4 in ((List<object>)root["ik"]))
				{
					Dictionary<string, object> constraintMap = (Dictionary<string, object>)obj4;
					IkConstraintData data3 = new IkConstraintData((string)constraintMap["name"]);
					data3.order = SkeletonJson.GetInt(constraintMap, "order", 0);
					data3.skinRequired = SkeletonJson.GetBoolean(constraintMap, "skin", false);
					if (constraintMap.ContainsKey("bones"))
					{
						foreach (object obj5 in ((List<object>)constraintMap["bones"]))
						{
							string boneName2 = (string)obj5;
							BoneData bone = skeletonData.FindBone(boneName2);
							if (bone == null)
							{
								throw new Exception("IK bone not found: " + boneName2);
							}
							data3.bones.Add(bone);
						}
					}
					string targetName = (string)constraintMap["target"];
					data3.target = skeletonData.FindBone(targetName);
					if (data3.target == null)
					{
						throw new Exception("IK target bone not found: " + targetName);
					}
					data3.mix = SkeletonJson.GetFloat(constraintMap, "mix", 1f);
					data3.softness = SkeletonJson.GetFloat(constraintMap, "softness", 0f) * scale;
					data3.bendDirection = (SkeletonJson.GetBoolean(constraintMap, "bendPositive", true) ? 1 : (-1));
					data3.compress = SkeletonJson.GetBoolean(constraintMap, "compress", false);
					data3.stretch = SkeletonJson.GetBoolean(constraintMap, "stretch", false);
					data3.uniform = SkeletonJson.GetBoolean(constraintMap, "uniform", false);
					skeletonData.ikConstraints.Add(data3);
				}
			}
			if (root.ContainsKey("transform"))
			{
				foreach (object obj6 in ((List<object>)root["transform"]))
				{
					Dictionary<string, object> constraintMap2 = (Dictionary<string, object>)obj6;
					TransformConstraintData data4 = new TransformConstraintData((string)constraintMap2["name"]);
					data4.order = SkeletonJson.GetInt(constraintMap2, "order", 0);
					data4.skinRequired = SkeletonJson.GetBoolean(constraintMap2, "skin", false);
					if (constraintMap2.ContainsKey("bones"))
					{
						foreach (object obj7 in ((List<object>)constraintMap2["bones"]))
						{
							string boneName3 = (string)obj7;
							BoneData bone2 = skeletonData.FindBone(boneName3);
							if (bone2 == null)
							{
								throw new Exception("Transform constraint bone not found: " + boneName3);
							}
							data4.bones.Add(bone2);
						}
					}
					string targetName2 = (string)constraintMap2["target"];
					data4.target = skeletonData.FindBone(targetName2);
					if (data4.target == null)
					{
						throw new Exception("Transform constraint target bone not found: " + targetName2);
					}
					data4.local = SkeletonJson.GetBoolean(constraintMap2, "local", false);
					data4.relative = SkeletonJson.GetBoolean(constraintMap2, "relative", false);
					data4.offsetRotation = SkeletonJson.GetFloat(constraintMap2, "rotation", 0f);
					data4.offsetX = SkeletonJson.GetFloat(constraintMap2, "x", 0f) * scale;
					data4.offsetY = SkeletonJson.GetFloat(constraintMap2, "y", 0f) * scale;
					data4.offsetScaleX = SkeletonJson.GetFloat(constraintMap2, "scaleX", 0f);
					data4.offsetScaleY = SkeletonJson.GetFloat(constraintMap2, "scaleY", 0f);
					data4.offsetShearY = SkeletonJson.GetFloat(constraintMap2, "shearY", 0f);
					data4.mixRotate = SkeletonJson.GetFloat(constraintMap2, "mixRotate", 1f);
					data4.mixX = SkeletonJson.GetFloat(constraintMap2, "mixX", 1f);
					data4.mixY = SkeletonJson.GetFloat(constraintMap2, "mixY", data4.mixX);
					data4.mixScaleX = SkeletonJson.GetFloat(constraintMap2, "mixScaleX", 1f);
					data4.mixScaleY = SkeletonJson.GetFloat(constraintMap2, "mixScaleY", data4.mixScaleX);
					data4.mixShearY = SkeletonJson.GetFloat(constraintMap2, "mixShearY", 1f);
					skeletonData.transformConstraints.Add(data4);
				}
			}
			if (root.ContainsKey("path"))
			{
				foreach (object obj8 in ((List<object>)root["path"]))
				{
					Dictionary<string, object> constraintMap3 = (Dictionary<string, object>)obj8;
					PathConstraintData data5 = new PathConstraintData((string)constraintMap3["name"]);
					data5.order = SkeletonJson.GetInt(constraintMap3, "order", 0);
					data5.skinRequired = SkeletonJson.GetBoolean(constraintMap3, "skin", false);
					if (constraintMap3.ContainsKey("bones"))
					{
						foreach (object obj9 in ((List<object>)constraintMap3["bones"]))
						{
							string boneName4 = (string)obj9;
							BoneData bone3 = skeletonData.FindBone(boneName4);
							if (bone3 == null)
							{
								throw new Exception("Path bone not found: " + boneName4);
							}
							data5.bones.Add(bone3);
						}
					}
					string targetName3 = (string)constraintMap3["target"];
					data5.target = skeletonData.FindSlot(targetName3);
					if (data5.target == null)
					{
						throw new Exception("Path target slot not found: " + targetName3);
					}
					data5.positionMode = (PositionMode)Enum.Parse(typeof(PositionMode), SkeletonJson.GetString(constraintMap3, "positionMode", "percent"), true);
					data5.spacingMode = (SpacingMode)Enum.Parse(typeof(SpacingMode), SkeletonJson.GetString(constraintMap3, "spacingMode", "length"), true);
					data5.rotateMode = (RotateMode)Enum.Parse(typeof(RotateMode), SkeletonJson.GetString(constraintMap3, "rotateMode", "tangent"), true);
					data5.offsetRotation = SkeletonJson.GetFloat(constraintMap3, "rotation", 0f);
					data5.position = SkeletonJson.GetFloat(constraintMap3, "position", 0f);
					if (data5.positionMode == PositionMode.Fixed)
					{
						data5.position *= scale;
					}
					data5.spacing = SkeletonJson.GetFloat(constraintMap3, "spacing", 0f);
					if (data5.spacingMode == SpacingMode.Length || data5.spacingMode == SpacingMode.Fixed)
					{
						data5.spacing *= scale;
					}
					data5.mixRotate = SkeletonJson.GetFloat(constraintMap3, "mixRotate", 1f);
					data5.mixX = SkeletonJson.GetFloat(constraintMap3, "mixX", 1f);
					data5.mixY = SkeletonJson.GetFloat(constraintMap3, "mixY", data5.mixX);
					skeletonData.pathConstraints.Add(data5);
				}
			}
			if (root.ContainsKey("physics"))
			{
				foreach (object obj10 in ((List<object>)root["physics"]))
				{
					Dictionary<string, object> constraintMap4 = (Dictionary<string, object>)obj10;
					PhysicsConstraintData data6 = new PhysicsConstraintData((string)constraintMap4["name"]);
					data6.order = SkeletonJson.GetInt(constraintMap4, "order", 0);
					data6.skinRequired = SkeletonJson.GetBoolean(constraintMap4, "skin", false);
					string boneName5 = (string)constraintMap4["bone"];
					data6.bone = skeletonData.FindBone(boneName5);
					if (data6.bone == null)
					{
						throw new Exception("Physics bone not found: " + boneName5);
					}
					data6.x = SkeletonJson.GetFloat(constraintMap4, "x", 0f);
					data6.y = SkeletonJson.GetFloat(constraintMap4, "y", 0f);
					data6.rotate = SkeletonJson.GetFloat(constraintMap4, "rotate", 0f);
					data6.scaleX = SkeletonJson.GetFloat(constraintMap4, "scaleX", 0f);
					data6.shearX = SkeletonJson.GetFloat(constraintMap4, "shearX", 0f);
					data6.limit = SkeletonJson.GetFloat(constraintMap4, "limit", 5000f) * scale;
					data6.step = 1f / (float)SkeletonJson.GetInt(constraintMap4, "fps", 60);
					data6.inertia = SkeletonJson.GetFloat(constraintMap4, "inertia", 1f);
					data6.strength = SkeletonJson.GetFloat(constraintMap4, "strength", 100f);
					data6.damping = SkeletonJson.GetFloat(constraintMap4, "damping", 1f);
					data6.massInverse = 1f / SkeletonJson.GetFloat(constraintMap4, "mass", 1f);
					data6.wind = SkeletonJson.GetFloat(constraintMap4, "wind", 0f);
					data6.gravity = SkeletonJson.GetFloat(constraintMap4, "gravity", 0f);
					data6.mix = SkeletonJson.GetFloat(constraintMap4, "mix", 1f);
					data6.inertiaGlobal = SkeletonJson.GetBoolean(constraintMap4, "inertiaGlobal", false);
					data6.strengthGlobal = SkeletonJson.GetBoolean(constraintMap4, "strengthGlobal", false);
					data6.dampingGlobal = SkeletonJson.GetBoolean(constraintMap4, "dampingGlobal", false);
					data6.massGlobal = SkeletonJson.GetBoolean(constraintMap4, "massGlobal", false);
					data6.windGlobal = SkeletonJson.GetBoolean(constraintMap4, "windGlobal", false);
					data6.gravityGlobal = SkeletonJson.GetBoolean(constraintMap4, "gravityGlobal", false);
					data6.mixGlobal = SkeletonJson.GetBoolean(constraintMap4, "mixGlobal", false);
					skeletonData.physicsConstraints.Add(data6);
				}
			}
			if (root.ContainsKey("skins"))
			{
				foreach (object obj11 in ((List<object>)root["skins"]))
				{
					Dictionary<string, object> skinMap = (Dictionary<string, object>)obj11;
					Skin skin = new Skin((string)skinMap["name"]);
					if (skinMap.ContainsKey("bones"))
					{
						foreach (object obj12 in ((List<object>)skinMap["bones"]))
						{
							string entryName = (string)obj12;
							BoneData bone4 = skeletonData.FindBone(entryName);
							if (bone4 == null)
							{
								throw new Exception("Skin bone not found: " + entryName);
							}
							skin.bones.Add(bone4);
						}
					}
					skin.bones.TrimExcess();
					if (skinMap.ContainsKey("ik"))
					{
						foreach (object obj13 in ((List<object>)skinMap["ik"]))
						{
							string entryName2 = (string)obj13;
							IkConstraintData constraint = skeletonData.FindIkConstraint(entryName2);
							if (constraint == null)
							{
								throw new Exception("Skin IK constraint not found: " + entryName2);
							}
							skin.constraints.Add(constraint);
						}
					}
					if (skinMap.ContainsKey("transform"))
					{
						foreach (object obj14 in ((List<object>)skinMap["transform"]))
						{
							string entryName3 = (string)obj14;
							TransformConstraintData constraint2 = skeletonData.FindTransformConstraint(entryName3);
							if (constraint2 == null)
							{
								throw new Exception("Skin transform constraint not found: " + entryName3);
							}
							skin.constraints.Add(constraint2);
						}
					}
					if (skinMap.ContainsKey("path"))
					{
						foreach (object obj15 in ((List<object>)skinMap["path"]))
						{
							string entryName4 = (string)obj15;
							PathConstraintData constraint3 = skeletonData.FindPathConstraint(entryName4);
							if (constraint3 == null)
							{
								throw new Exception("Skin path constraint not found: " + entryName4);
							}
							skin.constraints.Add(constraint3);
						}
					}
					if (skinMap.ContainsKey("physics"))
					{
						foreach (object obj16 in ((List<object>)skinMap["physics"]))
						{
							string entryName5 = (string)obj16;
							PhysicsConstraintData constraint4 = skeletonData.FindPhysicsConstraint(entryName5);
							if (constraint4 == null)
							{
								throw new Exception("Skin physics constraint not found: " + entryName5);
							}
							skin.constraints.Add(constraint4);
						}
					}
					skin.constraints.TrimExcess();
					if (skinMap.ContainsKey("attachments"))
					{
						foreach (KeyValuePair<string, object> slotEntry in ((Dictionary<string, object>)skinMap["attachments"]))
						{
							int slotIndex = this.FindSlotIndex(skeletonData, slotEntry.Key);
							foreach (KeyValuePair<string, object> entry in ((Dictionary<string, object>)slotEntry.Value))
							{
								try
								{
									Attachment attachment = this.ReadAttachment((Dictionary<string, object>)entry.Value, skin, slotIndex, entry.Key, skeletonData);
									if (attachment != null)
									{
										skin.SetAttachment(slotIndex, entry.Key, attachment);
									}
								}
								catch (Exception e)
								{
									string text2 = "Error reading attachment: ";
									string key = entry.Key;
									string text3 = ", skin: ";
									Skin skin2 = skin;
									throw new Exception(text2 + key + text3 + ((skin2 != null) ? skin2.ToString() : null), e);
								}
							}
						}
					}
					skeletonData.skins.Add(skin);
					if (skin.name == "default")
					{
						skeletonData.defaultSkin = skin;
					}
				}
			}
			int i = 0;
			int j = this.linkedMeshes.Count;
			while (i < j)
			{
				SkeletonJson.LinkedMesh linkedMesh = this.linkedMeshes[i];
				Skin skin3 = ((linkedMesh.skin == null) ? skeletonData.defaultSkin : skeletonData.FindSkin(linkedMesh.skin));
				if (skin3 == null)
				{
					throw new Exception("Slot not found: " + linkedMesh.skin);
				}
				Attachment parent2 = skin3.GetAttachment(linkedMesh.slotIndex, linkedMesh.parent);
				if (parent2 == null)
				{
					throw new Exception("Parent mesh not found: " + linkedMesh.parent);
				}
				linkedMesh.mesh.TimelineAttachment = (linkedMesh.inheritTimelines ? ((VertexAttachment)parent2) : linkedMesh.mesh);
				linkedMesh.mesh.ParentMesh = (MeshAttachment)parent2;
				if (linkedMesh.mesh.Region != null)
				{
					linkedMesh.mesh.UpdateRegion();
				}
				i++;
			}
			this.linkedMeshes.Clear();
			if (root.ContainsKey("events"))
			{
				foreach (KeyValuePair<string, object> entry2 in ((Dictionary<string, object>)root["events"]))
				{
					Dictionary<string, object> entryMap = (Dictionary<string, object>)entry2.Value;
					EventData data7 = new EventData(entry2.Key);
					data7.Int = SkeletonJson.GetInt(entryMap, "int", 0);
					data7.Float = SkeletonJson.GetFloat(entryMap, "float", 0f);
					data7.String = SkeletonJson.GetString(entryMap, "string", string.Empty);
					data7.AudioPath = SkeletonJson.GetString(entryMap, "audio", null);
					if (data7.AudioPath != null)
					{
						data7.Volume = SkeletonJson.GetFloat(entryMap, "volume", 1f);
						data7.Balance = SkeletonJson.GetFloat(entryMap, "balance", 0f);
					}
					skeletonData.events.Add(data7);
				}
			}
			if (root.ContainsKey("animations"))
			{
				foreach (KeyValuePair<string, object> entry3 in ((Dictionary<string, object>)root["animations"]))
				{
					try
					{
						this.ReadAnimation((Dictionary<string, object>)entry3.Value, entry3.Key, skeletonData);
					}
					catch (Exception e2)
					{
						throw new Exception("Error reading animation: " + entry3.Key + "\n" + e2.Message, e2);
					}
				}
			}
			skeletonData.bones.TrimExcess();
			skeletonData.slots.TrimExcess();
			skeletonData.skins.TrimExcess();
			skeletonData.events.TrimExcess();
			skeletonData.animations.TrimExcess();
			skeletonData.ikConstraints.TrimExcess();
			return skeletonData;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001880C File Offset: 0x00016A0C
		private Attachment ReadAttachment(Dictionary<string, object> map, Skin skin, int slotIndex, string name, SkeletonData skeletonData)
		{
			float scale = this.scale;
			name = SkeletonJson.GetString(map, "name", name);
			string typeName = SkeletonJson.GetString(map, "type", "region");
			switch ((AttachmentType)Enum.Parse(typeof(AttachmentType), typeName, true))
			{
			case AttachmentType.Region:
			{
				string path = SkeletonJson.GetString(map, "path", name);
				object sequenceJson;
				map.TryGetValue("sequence", out sequenceJson);
				Sequence sequence = SkeletonJson.ReadSequence(sequenceJson);
				RegionAttachment region = this.attachmentLoader.NewRegionAttachment(skin, name, path, sequence);
				if (region == null)
				{
					return null;
				}
				region.Path = path;
				region.x = SkeletonJson.GetFloat(map, "x", 0f) * scale;
				region.y = SkeletonJson.GetFloat(map, "y", 0f) * scale;
				region.scaleX = SkeletonJson.GetFloat(map, "scaleX", 1f);
				region.scaleY = SkeletonJson.GetFloat(map, "scaleY", 1f);
				region.rotation = SkeletonJson.GetFloat(map, "rotation", 0f);
				region.width = SkeletonJson.GetFloat(map, "width", 32f) * scale;
				region.height = SkeletonJson.GetFloat(map, "height", 32f) * scale;
				region.sequence = sequence;
				if (map.ContainsKey("color"))
				{
					string color = (string)map["color"];
					region.r = SkeletonJson.ToColor(color, 0, 8);
					region.g = SkeletonJson.ToColor(color, 1, 8);
					region.b = SkeletonJson.ToColor(color, 2, 8);
					region.a = SkeletonJson.ToColor(color, 3, 8);
				}
				if (region.Region != null)
				{
					region.UpdateRegion();
				}
				return region;
			}
			case AttachmentType.Boundingbox:
			{
				BoundingBoxAttachment box = this.attachmentLoader.NewBoundingBoxAttachment(skin, name);
				if (box == null)
				{
					return null;
				}
				this.ReadVertices(map, box, SkeletonJson.GetInt(map, "vertexCount", 0) << 1);
				return box;
			}
			case AttachmentType.Mesh:
			case AttachmentType.Linkedmesh:
			{
				string path2 = SkeletonJson.GetString(map, "path", name);
				object sequenceJson2;
				map.TryGetValue("sequence", out sequenceJson2);
				Sequence sequence2 = SkeletonJson.ReadSequence(sequenceJson2);
				MeshAttachment mesh = this.attachmentLoader.NewMeshAttachment(skin, name, path2, sequence2);
				if (mesh == null)
				{
					return null;
				}
				mesh.Path = path2;
				if (map.ContainsKey("color"))
				{
					string color2 = (string)map["color"];
					mesh.r = SkeletonJson.ToColor(color2, 0, 8);
					mesh.g = SkeletonJson.ToColor(color2, 1, 8);
					mesh.b = SkeletonJson.ToColor(color2, 2, 8);
					mesh.a = SkeletonJson.ToColor(color2, 3, 8);
				}
				mesh.Width = SkeletonJson.GetFloat(map, "width", 0f) * scale;
				mesh.Height = SkeletonJson.GetFloat(map, "height", 0f) * scale;
				mesh.Sequence = sequence2;
				string parent = SkeletonJson.GetString(map, "parent", null);
				if (parent != null)
				{
					this.linkedMeshes.Add(new SkeletonJson.LinkedMesh(mesh, SkeletonJson.GetString(map, "skin", null), slotIndex, parent, SkeletonJson.GetBoolean(map, "timelines", true)));
					return mesh;
				}
				float[] uvs = SkeletonJson.GetFloatArray(map, "uvs", 1f);
				this.ReadVertices(map, mesh, uvs.Length);
				mesh.triangles = SkeletonJson.GetIntArray(map, "triangles");
				mesh.regionUVs = uvs;
				if (mesh.Region != null)
				{
					mesh.UpdateRegion();
				}
				if (map.ContainsKey("hull"))
				{
					mesh.HullLength = SkeletonJson.GetInt(map, "hull", 0) << 1;
				}
				if (map.ContainsKey("edges"))
				{
					mesh.Edges = SkeletonJson.GetIntArray(map, "edges");
				}
				return mesh;
			}
			case AttachmentType.Path:
			{
				PathAttachment pathAttachment = this.attachmentLoader.NewPathAttachment(skin, name);
				if (pathAttachment == null)
				{
					return null;
				}
				pathAttachment.closed = SkeletonJson.GetBoolean(map, "closed", false);
				pathAttachment.constantSpeed = SkeletonJson.GetBoolean(map, "constantSpeed", true);
				int vertexCount = SkeletonJson.GetInt(map, "vertexCount", 0);
				this.ReadVertices(map, pathAttachment, vertexCount << 1);
				pathAttachment.lengths = SkeletonJson.GetFloatArray(map, "lengths", scale);
				return pathAttachment;
			}
			case AttachmentType.Point:
			{
				PointAttachment point = this.attachmentLoader.NewPointAttachment(skin, name);
				if (point == null)
				{
					return null;
				}
				point.x = SkeletonJson.GetFloat(map, "x", 0f) * scale;
				point.y = SkeletonJson.GetFloat(map, "y", 0f) * scale;
				point.rotation = SkeletonJson.GetFloat(map, "rotation", 0f);
				return point;
			}
			case AttachmentType.Clipping:
			{
				ClippingAttachment clip = this.attachmentLoader.NewClippingAttachment(skin, name);
				if (clip == null)
				{
					return null;
				}
				string end = SkeletonJson.GetString(map, "end", null);
				if (end != null)
				{
					SlotData slot = skeletonData.FindSlot(end);
					if (slot == null)
					{
						throw new Exception("Clipping end slot not found: " + end);
					}
					clip.EndSlot = slot;
				}
				this.ReadVertices(map, clip, SkeletonJson.GetInt(map, "vertexCount", 0) << 1);
				return clip;
			}
			default:
				return null;
			}
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00018D08 File Offset: 0x00016F08
		public static Sequence ReadSequence(object sequenceJson)
		{
			Dictionary<string, object> map = sequenceJson as Dictionary<string, object>;
			if (map == null)
			{
				return null;
			}
			return new Sequence(SkeletonJson.GetInt(map, "count"))
			{
				start = SkeletonJson.GetInt(map, "start", 1),
				digits = SkeletonJson.GetInt(map, "digits", 0),
				setupIndex = SkeletonJson.GetInt(map, "setup", 0)
			};
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00018D68 File Offset: 0x00016F68
		private void ReadVertices(Dictionary<string, object> map, VertexAttachment attachment, int verticesLength)
		{
			attachment.WorldVerticesLength = verticesLength;
			float[] vertices = SkeletonJson.GetFloatArray(map, "vertices", 1f);
			float scale = base.Scale;
			if (verticesLength == vertices.Length)
			{
				if (scale != 1f)
				{
					for (int i = 0; i < vertices.Length; i++)
					{
						vertices[i] *= scale;
					}
				}
				attachment.vertices = vertices;
				return;
			}
			ExposedList<float> weights = new ExposedList<float>(verticesLength * 3 * 3);
			ExposedList<int> bones = new ExposedList<int>(verticesLength * 3);
			int j = 0;
			int k = vertices.Length;
			while (j < k)
			{
				int boneCount = (int)vertices[j++];
				bones.Add(boneCount);
				int nn = j + (boneCount << 2);
				while (j < nn)
				{
					bones.Add((int)vertices[j]);
					weights.Add(vertices[j + 1] * base.Scale);
					weights.Add(vertices[j + 2] * base.Scale);
					weights.Add(vertices[j + 3]);
					j += 4;
				}
			}
			attachment.bones = bones.ToArray();
			attachment.vertices = weights.ToArray();
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00018E70 File Offset: 0x00017070
		private int FindSlotIndex(SkeletonData skeletonData, string slotName)
		{
			SlotData[] slots = skeletonData.slots.Items;
			int i = 0;
			int j = skeletonData.slots.Count;
			while (i < j)
			{
				if (slots[i].name == slotName)
				{
					return i;
				}
				i++;
			}
			throw new Exception("Slot not found: " + slotName);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x00018EC4 File Offset: 0x000170C4
		private void ReadAnimation(Dictionary<string, object> map, string name, SkeletonData skeletonData)
		{
			float scale = this.scale;
			ExposedList<Timeline> timelines = new ExposedList<Timeline>();
			if (map.ContainsKey("slots"))
			{
				foreach (KeyValuePair<string, object> entry in ((Dictionary<string, object>)map["slots"]))
				{
					string slotName = entry.Key;
					int slotIndex = this.FindSlotIndex(skeletonData, slotName);
					foreach (KeyValuePair<string, object> timelineEntry in ((Dictionary<string, object>)entry.Value))
					{
						List<object> values = (List<object>)timelineEntry.Value;
						int frames = values.Count;
						if (frames != 0)
						{
							string timelineName = timelineEntry.Key;
							if (timelineName == "attachment")
							{
								AttachmentTimeline timeline = new AttachmentTimeline(frames, slotIndex);
								int frame = 0;
								foreach (object obj in values)
								{
									Dictionary<string, object> keyMap = (Dictionary<string, object>)obj;
									timeline.SetFrame(frame++, SkeletonJson.GetFloat(keyMap, "time", 0f), SkeletonJson.GetString(keyMap, "name", null));
								}
								timelines.Add(timeline);
							}
							else if (timelineName == "rgba")
							{
								RGBATimeline timeline2 = new RGBATimeline(frames, frames << 2, slotIndex);
								List<object>.Enumerator keyMapEnumerator = values.GetEnumerator();
								keyMapEnumerator.MoveNext();
								Dictionary<string, object> keyMap2 = (Dictionary<string, object>)keyMapEnumerator.Current;
								float time = SkeletonJson.GetFloat(keyMap2, "time", 0f);
								string text = (string)keyMap2["color"];
								float r = SkeletonJson.ToColor(text, 0, 8);
								float g = SkeletonJson.ToColor(text, 1, 8);
								float b = SkeletonJson.ToColor(text, 2, 8);
								float a = SkeletonJson.ToColor(text, 3, 8);
								int frame2 = 0;
								int bezier = 0;
								for (;;)
								{
									timeline2.SetFrame(frame2, time, r, g, b, a);
									if (!keyMapEnumerator.MoveNext())
									{
										break;
									}
									Dictionary<string, object> dictionary = (Dictionary<string, object>)keyMapEnumerator.Current;
									float time2 = SkeletonJson.GetFloat(dictionary, "time", 0f);
									string text2 = (string)dictionary["color"];
									float nr = SkeletonJson.ToColor(text2, 0, 8);
									float ng = SkeletonJson.ToColor(text2, 1, 8);
									float nb = SkeletonJson.ToColor(text2, 2, 8);
									float na = SkeletonJson.ToColor(text2, 3, 8);
									if (keyMap2.ContainsKey("curve"))
									{
										object obj2 = keyMap2["curve"];
										bezier = SkeletonJson.ReadCurve(obj2, timeline2, bezier, frame2, 0, time, time2, r, nr, 1f);
										bezier = SkeletonJson.ReadCurve(obj2, timeline2, bezier, frame2, 1, time, time2, g, ng, 1f);
										bezier = SkeletonJson.ReadCurve(obj2, timeline2, bezier, frame2, 2, time, time2, b, nb, 1f);
										bezier = SkeletonJson.ReadCurve(obj2, timeline2, bezier, frame2, 3, time, time2, a, na, 1f);
									}
									time = time2;
									r = nr;
									g = ng;
									b = nb;
									a = na;
									keyMap2 = dictionary;
									frame2++;
								}
								timeline2.Shrink(bezier);
								timelines.Add(timeline2);
							}
							else if (timelineName == "rgb")
							{
								RGBTimeline timeline3 = new RGBTimeline(frames, frames * 3, slotIndex);
								List<object>.Enumerator keyMapEnumerator2 = values.GetEnumerator();
								keyMapEnumerator2.MoveNext();
								Dictionary<string, object> keyMap3 = (Dictionary<string, object>)keyMapEnumerator2.Current;
								float time3 = SkeletonJson.GetFloat(keyMap3, "time", 0f);
								string text3 = (string)keyMap3["color"];
								float r2 = SkeletonJson.ToColor(text3, 0, 6);
								float g2 = SkeletonJson.ToColor(text3, 1, 6);
								float b2 = SkeletonJson.ToColor(text3, 2, 6);
								int frame3 = 0;
								int bezier2 = 0;
								for (;;)
								{
									timeline3.SetFrame(frame3, time3, r2, g2, b2);
									if (!keyMapEnumerator2.MoveNext())
									{
										break;
									}
									Dictionary<string, object> dictionary2 = (Dictionary<string, object>)keyMapEnumerator2.Current;
									float time4 = SkeletonJson.GetFloat(dictionary2, "time", 0f);
									string text4 = (string)dictionary2["color"];
									float nr2 = SkeletonJson.ToColor(text4, 0, 6);
									float ng2 = SkeletonJson.ToColor(text4, 1, 6);
									float nb2 = SkeletonJson.ToColor(text4, 2, 6);
									if (keyMap3.ContainsKey("curve"))
									{
										object obj3 = keyMap3["curve"];
										bezier2 = SkeletonJson.ReadCurve(obj3, timeline3, bezier2, frame3, 0, time3, time4, r2, nr2, 1f);
										bezier2 = SkeletonJson.ReadCurve(obj3, timeline3, bezier2, frame3, 1, time3, time4, g2, ng2, 1f);
										bezier2 = SkeletonJson.ReadCurve(obj3, timeline3, bezier2, frame3, 2, time3, time4, b2, nb2, 1f);
									}
									time3 = time4;
									r2 = nr2;
									g2 = ng2;
									b2 = nb2;
									keyMap3 = dictionary2;
									frame3++;
								}
								timeline3.Shrink(bezier2);
								timelines.Add(timeline3);
							}
							else if (timelineName == "alpha")
							{
								List<object>.Enumerator keyMapEnumerator3 = values.GetEnumerator();
								keyMapEnumerator3.MoveNext();
								timelines.Add(SkeletonJson.ReadTimeline(ref keyMapEnumerator3, new AlphaTimeline(frames, frames, slotIndex), 0f, 1f));
							}
							else if (timelineName == "rgba2")
							{
								RGBA2Timeline timeline4 = new RGBA2Timeline(frames, frames * 7, slotIndex);
								List<object>.Enumerator keyMapEnumerator4 = values.GetEnumerator();
								keyMapEnumerator4.MoveNext();
								Dictionary<string, object> keyMap4 = (Dictionary<string, object>)keyMapEnumerator4.Current;
								float time5 = SkeletonJson.GetFloat(keyMap4, "time", 0f);
								string text5 = (string)keyMap4["light"];
								float r3 = SkeletonJson.ToColor(text5, 0, 8);
								float g3 = SkeletonJson.ToColor(text5, 1, 8);
								float b3 = SkeletonJson.ToColor(text5, 2, 8);
								float a2 = SkeletonJson.ToColor(text5, 3, 8);
								string text6 = (string)keyMap4["dark"];
								float r4 = SkeletonJson.ToColor(text6, 0, 6);
								float g4 = SkeletonJson.ToColor(text6, 1, 6);
								float b4 = SkeletonJson.ToColor(text6, 2, 6);
								int frame4 = 0;
								int bezier3 = 0;
								for (;;)
								{
									timeline4.SetFrame(frame4, time5, r3, g3, b3, a2, r4, g4, b4);
									if (!keyMapEnumerator4.MoveNext())
									{
										break;
									}
									Dictionary<string, object> dictionary3 = (Dictionary<string, object>)keyMapEnumerator4.Current;
									float time6 = SkeletonJson.GetFloat(dictionary3, "time", 0f);
									string text7 = (string)dictionary3["light"];
									float nr3 = SkeletonJson.ToColor(text7, 0, 8);
									float ng3 = SkeletonJson.ToColor(text7, 1, 8);
									float nb3 = SkeletonJson.ToColor(text7, 2, 8);
									float na2 = SkeletonJson.ToColor(text7, 3, 8);
									string text8 = (string)dictionary3["dark"];
									float nr4 = SkeletonJson.ToColor(text8, 0, 6);
									float ng4 = SkeletonJson.ToColor(text8, 1, 6);
									float nb4 = SkeletonJson.ToColor(text8, 2, 6);
									if (keyMap4.ContainsKey("curve"))
									{
										object obj4 = keyMap4["curve"];
										bezier3 = SkeletonJson.ReadCurve(obj4, timeline4, bezier3, frame4, 0, time5, time6, r3, nr3, 1f);
										bezier3 = SkeletonJson.ReadCurve(obj4, timeline4, bezier3, frame4, 1, time5, time6, g3, ng3, 1f);
										bezier3 = SkeletonJson.ReadCurve(obj4, timeline4, bezier3, frame4, 2, time5, time6, b3, nb3, 1f);
										bezier3 = SkeletonJson.ReadCurve(obj4, timeline4, bezier3, frame4, 3, time5, time6, a2, na2, 1f);
										bezier3 = SkeletonJson.ReadCurve(obj4, timeline4, bezier3, frame4, 4, time5, time6, r4, nr4, 1f);
										bezier3 = SkeletonJson.ReadCurve(obj4, timeline4, bezier3, frame4, 5, time5, time6, g4, ng4, 1f);
										bezier3 = SkeletonJson.ReadCurve(obj4, timeline4, bezier3, frame4, 6, time5, time6, b4, nb4, 1f);
									}
									time5 = time6;
									r3 = nr3;
									g3 = ng3;
									b3 = nb3;
									a2 = na2;
									r4 = nr4;
									g4 = ng4;
									b4 = nb4;
									keyMap4 = dictionary3;
									frame4++;
								}
								timeline4.Shrink(bezier3);
								timelines.Add(timeline4);
							}
							else
							{
								if (!(timelineName == "rgb2"))
								{
									throw new Exception(string.Concat(new string[] { "Invalid timeline type for a slot: ", timelineName, " (", slotName, ")" }));
								}
								RGB2Timeline timeline5 = new RGB2Timeline(frames, frames * 6, slotIndex);
								List<object>.Enumerator keyMapEnumerator5 = values.GetEnumerator();
								keyMapEnumerator5.MoveNext();
								Dictionary<string, object> keyMap5 = (Dictionary<string, object>)keyMapEnumerator5.Current;
								float time7 = SkeletonJson.GetFloat(keyMap5, "time", 0f);
								string text9 = (string)keyMap5["light"];
								float r5 = SkeletonJson.ToColor(text9, 0, 6);
								float g5 = SkeletonJson.ToColor(text9, 1, 6);
								float b5 = SkeletonJson.ToColor(text9, 2, 6);
								string text10 = (string)keyMap5["dark"];
								float r6 = SkeletonJson.ToColor(text10, 0, 6);
								float g6 = SkeletonJson.ToColor(text10, 1, 6);
								float b6 = SkeletonJson.ToColor(text10, 2, 6);
								int frame5 = 0;
								int bezier4 = 0;
								for (;;)
								{
									timeline5.SetFrame(frame5, time7, r5, g5, b5, r6, g6, b6);
									if (!keyMapEnumerator5.MoveNext())
									{
										break;
									}
									Dictionary<string, object> dictionary4 = (Dictionary<string, object>)keyMapEnumerator5.Current;
									float time8 = SkeletonJson.GetFloat(dictionary4, "time", 0f);
									string text11 = (string)dictionary4["light"];
									float nr5 = SkeletonJson.ToColor(text11, 0, 6);
									float ng5 = SkeletonJson.ToColor(text11, 1, 6);
									float nb5 = SkeletonJson.ToColor(text11, 2, 6);
									string text12 = (string)dictionary4["dark"];
									float nr6 = SkeletonJson.ToColor(text12, 0, 6);
									float ng6 = SkeletonJson.ToColor(text12, 1, 6);
									float nb6 = SkeletonJson.ToColor(text12, 2, 6);
									if (keyMap5.ContainsKey("curve"))
									{
										object obj5 = keyMap5["curve"];
										bezier4 = SkeletonJson.ReadCurve(obj5, timeline5, bezier4, frame5, 0, time7, time8, r5, nr5, 1f);
										bezier4 = SkeletonJson.ReadCurve(obj5, timeline5, bezier4, frame5, 1, time7, time8, g5, ng5, 1f);
										bezier4 = SkeletonJson.ReadCurve(obj5, timeline5, bezier4, frame5, 2, time7, time8, b5, nb5, 1f);
										bezier4 = SkeletonJson.ReadCurve(obj5, timeline5, bezier4, frame5, 3, time7, time8, r6, nr6, 1f);
										bezier4 = SkeletonJson.ReadCurve(obj5, timeline5, bezier4, frame5, 4, time7, time8, g6, ng6, 1f);
										bezier4 = SkeletonJson.ReadCurve(obj5, timeline5, bezier4, frame5, 5, time7, time8, b6, nb6, 1f);
									}
									time7 = time8;
									r5 = nr5;
									g5 = ng5;
									b5 = nb5;
									r6 = nr6;
									g6 = ng6;
									b6 = nb6;
									keyMap5 = dictionary4;
									frame5++;
								}
								timeline5.Shrink(bezier4);
								timelines.Add(timeline5);
							}
						}
					}
				}
			}
			if (map.ContainsKey("bones"))
			{
				foreach (KeyValuePair<string, object> entry2 in ((Dictionary<string, object>)map["bones"]))
				{
					string boneName = entry2.Key;
					int boneIndex = -1;
					BoneData[] bones = skeletonData.bones.Items;
					int i = 0;
					int j = skeletonData.bones.Count;
					while (i < j)
					{
						if (bones[i].name == boneName)
						{
							boneIndex = i;
							break;
						}
						i++;
					}
					if (boneIndex == -1)
					{
						throw new Exception("Bone not found: " + boneName);
					}
					foreach (KeyValuePair<string, object> timelineEntry2 in ((Dictionary<string, object>)entry2.Value))
					{
						List<object> values2 = (List<object>)timelineEntry2.Value;
						List<object>.Enumerator keyMapEnumerator6 = values2.GetEnumerator();
						if (keyMapEnumerator6.MoveNext())
						{
							int frames2 = values2.Count;
							string timelineName2 = timelineEntry2.Key;
							if (timelineName2 == "rotate")
							{
								timelines.Add(SkeletonJson.ReadTimeline(ref keyMapEnumerator6, new RotateTimeline(frames2, frames2, boneIndex), 0f, 1f));
							}
							else if (timelineName2 == "translate")
							{
								TranslateTimeline timeline6 = new TranslateTimeline(frames2, frames2 << 1, boneIndex);
								timelines.Add(SkeletonJson.ReadTimeline(ref keyMapEnumerator6, timeline6, "x", "y", 0f, scale));
							}
							else if (timelineName2 == "translatex")
							{
								timelines.Add(SkeletonJson.ReadTimeline(ref keyMapEnumerator6, new TranslateXTimeline(frames2, frames2, boneIndex), 0f, scale));
							}
							else if (timelineName2 == "translatey")
							{
								timelines.Add(SkeletonJson.ReadTimeline(ref keyMapEnumerator6, new TranslateYTimeline(frames2, frames2, boneIndex), 0f, scale));
							}
							else if (timelineName2 == "scale")
							{
								ScaleTimeline timeline7 = new ScaleTimeline(frames2, frames2 << 1, boneIndex);
								timelines.Add(SkeletonJson.ReadTimeline(ref keyMapEnumerator6, timeline7, "x", "y", 1f, 1f));
							}
							else if (timelineName2 == "scalex")
							{
								timelines.Add(SkeletonJson.ReadTimeline(ref keyMapEnumerator6, new ScaleXTimeline(frames2, frames2, boneIndex), 1f, 1f));
							}
							else if (timelineName2 == "scaley")
							{
								timelines.Add(SkeletonJson.ReadTimeline(ref keyMapEnumerator6, new ScaleYTimeline(frames2, frames2, boneIndex), 1f, 1f));
							}
							else if (timelineName2 == "shear")
							{
								ShearTimeline timeline8 = new ShearTimeline(frames2, frames2 << 1, boneIndex);
								timelines.Add(SkeletonJson.ReadTimeline(ref keyMapEnumerator6, timeline8, "x", "y", 0f, 1f));
							}
							else if (timelineName2 == "shearx")
							{
								timelines.Add(SkeletonJson.ReadTimeline(ref keyMapEnumerator6, new ShearXTimeline(frames2, frames2, boneIndex), 0f, 1f));
							}
							else if (timelineName2 == "sheary")
							{
								timelines.Add(SkeletonJson.ReadTimeline(ref keyMapEnumerator6, new ShearYTimeline(frames2, frames2, boneIndex), 0f, 1f));
							}
							else
							{
								if (!(timelineName2 == "inherit"))
								{
									throw new Exception(string.Concat(new string[] { "Invalid timeline type for a bone: ", timelineName2, " (", boneName, ")" }));
								}
								InheritTimeline timeline9 = new InheritTimeline(frames2, boneIndex);
								int frame6 = 0;
								for (;;)
								{
									Dictionary<string, object> keyMap6 = (Dictionary<string, object>)keyMapEnumerator6.Current;
									float time9 = SkeletonJson.GetFloat(keyMap6, "time", 0f);
									Inherit inherit = (Inherit)Enum.Parse(typeof(Inherit), SkeletonJson.GetString(keyMap6, "inherit", Inherit.Normal.ToString()), true);
									timeline9.SetFrame(frame6, time9, inherit);
									if (!keyMapEnumerator6.MoveNext())
									{
										break;
									}
									frame6++;
								}
								timelines.Add(timeline9);
							}
						}
					}
				}
			}
			if (map.ContainsKey("ik"))
			{
				foreach (KeyValuePair<string, object> timelineMap in ((Dictionary<string, object>)map["ik"]))
				{
					List<object> values3 = (List<object>)timelineMap.Value;
					List<object>.Enumerator keyMapEnumerator7 = values3.GetEnumerator();
					if (keyMapEnumerator7.MoveNext())
					{
						Dictionary<string, object> keyMap7 = (Dictionary<string, object>)keyMapEnumerator7.Current;
						IkConstraintData constraint = skeletonData.FindIkConstraint(timelineMap.Key);
						IkConstraintTimeline timeline10 = new IkConstraintTimeline(values3.Count, values3.Count << 1, skeletonData.IkConstraints.IndexOf(constraint));
						float time10 = SkeletonJson.GetFloat(keyMap7, "time", 0f);
						float mix = SkeletonJson.GetFloat(keyMap7, "mix", 1f);
						float softness = SkeletonJson.GetFloat(keyMap7, "softness", 0f) * scale;
						int frame7 = 0;
						int bezier5 = 0;
						for (;;)
						{
							timeline10.SetFrame(frame7, time10, mix, softness, SkeletonJson.GetBoolean(keyMap7, "bendPositive", true) ? 1 : (-1), SkeletonJson.GetBoolean(keyMap7, "compress", false), SkeletonJson.GetBoolean(keyMap7, "stretch", false));
							if (!keyMapEnumerator7.MoveNext())
							{
								break;
							}
							Dictionary<string, object> dictionary5 = (Dictionary<string, object>)keyMapEnumerator7.Current;
							float time11 = SkeletonJson.GetFloat(dictionary5, "time", 0f);
							float mix2 = SkeletonJson.GetFloat(dictionary5, "mix", 1f);
							float softness2 = SkeletonJson.GetFloat(dictionary5, "softness", 0f) * scale;
							if (keyMap7.ContainsKey("curve"))
							{
								object obj6 = keyMap7["curve"];
								bezier5 = SkeletonJson.ReadCurve(obj6, timeline10, bezier5, frame7, 0, time10, time11, mix, mix2, 1f);
								bezier5 = SkeletonJson.ReadCurve(obj6, timeline10, bezier5, frame7, 1, time10, time11, softness, softness2, scale);
							}
							time10 = time11;
							mix = mix2;
							softness = softness2;
							keyMap7 = dictionary5;
							frame7++;
						}
						timeline10.Shrink(bezier5);
						timelines.Add(timeline10);
					}
				}
			}
			if (map.ContainsKey("transform"))
			{
				foreach (KeyValuePair<string, object> timelineMap2 in ((Dictionary<string, object>)map["transform"]))
				{
					List<object> values4 = (List<object>)timelineMap2.Value;
					List<object>.Enumerator keyMapEnumerator8 = values4.GetEnumerator();
					if (keyMapEnumerator8.MoveNext())
					{
						Dictionary<string, object> keyMap8 = (Dictionary<string, object>)keyMapEnumerator8.Current;
						TransformConstraintData constraint2 = skeletonData.FindTransformConstraint(timelineMap2.Key);
						TransformConstraintTimeline timeline11 = new TransformConstraintTimeline(values4.Count, values4.Count * 6, skeletonData.TransformConstraints.IndexOf(constraint2));
						float time12 = SkeletonJson.GetFloat(keyMap8, "time", 0f);
						float mixRotate = SkeletonJson.GetFloat(keyMap8, "mixRotate", 1f);
						float mixShearY = SkeletonJson.GetFloat(keyMap8, "mixShearY", 1f);
						float mixX = SkeletonJson.GetFloat(keyMap8, "mixX", 1f);
						float mixY = SkeletonJson.GetFloat(keyMap8, "mixY", mixX);
						float mixScaleX = SkeletonJson.GetFloat(keyMap8, "mixScaleX", 1f);
						float mixScaleY = SkeletonJson.GetFloat(keyMap8, "mixScaleY", mixScaleX);
						int frame8 = 0;
						int bezier6 = 0;
						for (;;)
						{
							timeline11.SetFrame(frame8, time12, mixRotate, mixX, mixY, mixScaleX, mixScaleY, mixShearY);
							if (!keyMapEnumerator8.MoveNext())
							{
								break;
							}
							Dictionary<string, object> dictionary6 = (Dictionary<string, object>)keyMapEnumerator8.Current;
							float time13 = SkeletonJson.GetFloat(dictionary6, "time", 0f);
							float mixRotate2 = SkeletonJson.GetFloat(dictionary6, "mixRotate", 1f);
							float mixShearY2 = SkeletonJson.GetFloat(dictionary6, "mixShearY", 1f);
							float mixX2 = SkeletonJson.GetFloat(dictionary6, "mixX", 1f);
							float mixY2 = SkeletonJson.GetFloat(dictionary6, "mixY", mixX2);
							float mixScaleX2 = SkeletonJson.GetFloat(dictionary6, "mixScaleX", 1f);
							float mixScaleY2 = SkeletonJson.GetFloat(dictionary6, "mixScaleY", mixScaleX2);
							if (keyMap8.ContainsKey("curve"))
							{
								object obj7 = keyMap8["curve"];
								bezier6 = SkeletonJson.ReadCurve(obj7, timeline11, bezier6, frame8, 0, time12, time13, mixRotate, mixRotate2, 1f);
								bezier6 = SkeletonJson.ReadCurve(obj7, timeline11, bezier6, frame8, 1, time12, time13, mixX, mixX2, 1f);
								bezier6 = SkeletonJson.ReadCurve(obj7, timeline11, bezier6, frame8, 2, time12, time13, mixY, mixY2, 1f);
								bezier6 = SkeletonJson.ReadCurve(obj7, timeline11, bezier6, frame8, 3, time12, time13, mixScaleX, mixScaleX2, 1f);
								bezier6 = SkeletonJson.ReadCurve(obj7, timeline11, bezier6, frame8, 4, time12, time13, mixScaleY, mixScaleY2, 1f);
								bezier6 = SkeletonJson.ReadCurve(obj7, timeline11, bezier6, frame8, 5, time12, time13, mixShearY, mixShearY2, 1f);
							}
							time12 = time13;
							mixRotate = mixRotate2;
							mixX = mixX2;
							mixY = mixY2;
							mixScaleX = mixScaleX2;
							mixScaleY = mixScaleY2;
							mixShearY = mixShearY2;
							keyMap8 = dictionary6;
							frame8++;
						}
						timeline11.Shrink(bezier6);
						timelines.Add(timeline11);
					}
				}
			}
			if (map.ContainsKey("path"))
			{
				foreach (KeyValuePair<string, object> constraintMap in ((Dictionary<string, object>)map["path"]))
				{
					PathConstraintData constraint3 = skeletonData.FindPathConstraint(constraintMap.Key);
					if (constraint3 == null)
					{
						throw new Exception("Path constraint not found: " + constraintMap.Key);
					}
					int constraintIndex = skeletonData.pathConstraints.IndexOf(constraint3);
					foreach (KeyValuePair<string, object> timelineEntry3 in ((Dictionary<string, object>)constraintMap.Value))
					{
						List<object> values5 = (List<object>)timelineEntry3.Value;
						List<object>.Enumerator keyMapEnumerator9 = values5.GetEnumerator();
						if (keyMapEnumerator9.MoveNext())
						{
							int frames3 = values5.Count;
							string timelineName3 = timelineEntry3.Key;
							if (timelineName3 == "position")
							{
								CurveTimeline1 timeline12 = new PathConstraintPositionTimeline(frames3, frames3, constraintIndex);
								timelines.Add(SkeletonJson.ReadTimeline(ref keyMapEnumerator9, timeline12, 0f, (constraint3.positionMode == PositionMode.Fixed) ? scale : 1f));
							}
							else if (timelineName3 == "spacing")
							{
								CurveTimeline1 timeline13 = new PathConstraintSpacingTimeline(frames3, frames3, constraintIndex);
								timelines.Add(SkeletonJson.ReadTimeline(ref keyMapEnumerator9, timeline13, 0f, (constraint3.spacingMode == SpacingMode.Length || constraint3.spacingMode == SpacingMode.Fixed) ? scale : 1f));
							}
							else if (timelineName3 == "mix")
							{
								PathConstraintMixTimeline timeline14 = new PathConstraintMixTimeline(frames3, frames3 * 3, constraintIndex);
								Dictionary<string, object> keyMap9 = (Dictionary<string, object>)keyMapEnumerator9.Current;
								float time14 = SkeletonJson.GetFloat(keyMap9, "time", 0f);
								float mixRotate3 = SkeletonJson.GetFloat(keyMap9, "mixRotate", 1f);
								float mixX3 = SkeletonJson.GetFloat(keyMap9, "mixX", 1f);
								float mixY3 = SkeletonJson.GetFloat(keyMap9, "mixY", mixX3);
								int frame9 = 0;
								int bezier7 = 0;
								for (;;)
								{
									timeline14.SetFrame(frame9, time14, mixRotate3, mixX3, mixY3);
									if (!keyMapEnumerator9.MoveNext())
									{
										break;
									}
									Dictionary<string, object> dictionary7 = (Dictionary<string, object>)keyMapEnumerator9.Current;
									float time15 = SkeletonJson.GetFloat(dictionary7, "time", 0f);
									float mixRotate4 = SkeletonJson.GetFloat(dictionary7, "mixRotate", 1f);
									float mixX4 = SkeletonJson.GetFloat(dictionary7, "mixX", 1f);
									float mixY4 = SkeletonJson.GetFloat(dictionary7, "mixY", mixX4);
									if (keyMap9.ContainsKey("curve"))
									{
										object obj8 = keyMap9["curve"];
										bezier7 = SkeletonJson.ReadCurve(obj8, timeline14, bezier7, frame9, 0, time14, time15, mixRotate3, mixRotate4, 1f);
										bezier7 = SkeletonJson.ReadCurve(obj8, timeline14, bezier7, frame9, 1, time14, time15, mixX3, mixX4, 1f);
										bezier7 = SkeletonJson.ReadCurve(obj8, timeline14, bezier7, frame9, 2, time14, time15, mixY3, mixY4, 1f);
									}
									time14 = time15;
									mixRotate3 = mixRotate4;
									mixX3 = mixX4;
									mixY3 = mixY4;
									keyMap9 = dictionary7;
									frame9++;
								}
								timeline14.Shrink(bezier7);
								timelines.Add(timeline14);
							}
						}
					}
				}
			}
			if (map.ContainsKey("physics"))
			{
				foreach (KeyValuePair<string, object> constraintMap2 in ((Dictionary<string, object>)map["physics"]))
				{
					int index = -1;
					if (!string.IsNullOrEmpty(constraintMap2.Key))
					{
						PhysicsConstraintData constraint4 = skeletonData.FindPhysicsConstraint(constraintMap2.Key);
						if (constraint4 == null)
						{
							throw new Exception("Physics constraint not found: " + constraintMap2.Key);
						}
						index = skeletonData.physicsConstraints.IndexOf(constraint4);
					}
					foreach (KeyValuePair<string, object> timelineEntry4 in ((Dictionary<string, object>)constraintMap2.Value))
					{
						List<object> values6 = (List<object>)timelineEntry4.Value;
						List<object>.Enumerator keyMapEnumerator10 = values6.GetEnumerator();
						if (keyMapEnumerator10.MoveNext())
						{
							int frames4 = values6.Count;
							string timelineName4 = timelineEntry4.Key;
							if (timelineName4 == "reset")
							{
								PhysicsConstraintResetTimeline timeline15 = new PhysicsConstraintResetTimeline(frames4, index);
								int frame10 = 0;
								foreach (object obj9 in values6)
								{
									Dictionary<string, object> keyMap10 = (Dictionary<string, object>)obj9;
									timeline15.SetFrame(frame10++, SkeletonJson.GetFloat(keyMap10, "time", 0f));
								}
								timelines.Add(timeline15);
							}
							else
							{
								CurveTimeline1 timeline16;
								if (timelineName4 == "inertia")
								{
									timeline16 = new PhysicsConstraintInertiaTimeline(frames4, frames4, index);
								}
								else if (timelineName4 == "strength")
								{
									timeline16 = new PhysicsConstraintStrengthTimeline(frames4, frames4, index);
								}
								else if (timelineName4 == "damping")
								{
									timeline16 = new PhysicsConstraintDampingTimeline(frames4, frames4, index);
								}
								else if (timelineName4 == "mass")
								{
									timeline16 = new PhysicsConstraintMassTimeline(frames4, frames4, index);
								}
								else if (timelineName4 == "wind")
								{
									timeline16 = new PhysicsConstraintWindTimeline(frames4, frames4, index);
								}
								else if (timelineName4 == "gravity")
								{
									timeline16 = new PhysicsConstraintGravityTimeline(frames4, frames4, index);
								}
								else
								{
									if (!(timelineName4 == "mix"))
									{
										continue;
									}
									timeline16 = new PhysicsConstraintMixTimeline(frames4, frames4, index);
								}
								timelines.Add(SkeletonJson.ReadTimeline(ref keyMapEnumerator10, timeline16, 0f, 1f));
							}
						}
					}
				}
			}
			if (map.ContainsKey("attachments"))
			{
				foreach (KeyValuePair<string, object> attachmentsMap in ((Dictionary<string, object>)map["attachments"]))
				{
					Skin skin = skeletonData.FindSkin(attachmentsMap.Key);
					foreach (KeyValuePair<string, object> slotMap in ((Dictionary<string, object>)attachmentsMap.Value))
					{
						SlotData slot = skeletonData.FindSlot(slotMap.Key);
						if (slot == null)
						{
							throw new Exception("Slot not found: " + slotMap.Key);
						}
						foreach (KeyValuePair<string, object> attachmentMap in ((Dictionary<string, object>)slotMap.Value))
						{
							Attachment attachment = skin.GetAttachment(slot.index, attachmentMap.Key);
							if (attachment == null)
							{
								throw new Exception("Timeline attachment not found: " + attachmentMap.Key);
							}
							foreach (KeyValuePair<string, object> timelineMap3 in ((Dictionary<string, object>)attachmentMap.Value))
							{
								List<object> values7 = (List<object>)timelineMap3.Value;
								List<object>.Enumerator keyMapEnumerator11 = values7.GetEnumerator();
								if (keyMapEnumerator11.MoveNext())
								{
									Dictionary<string, object> keyMap11 = (Dictionary<string, object>)keyMapEnumerator11.Current;
									int frames5 = values7.Count;
									string timelineName5 = timelineMap3.Key;
									if (timelineName5 == "deform")
									{
										VertexAttachment vertexAttachment = (VertexAttachment)attachment;
										bool weighted = vertexAttachment.bones != null;
										float[] vertices = vertexAttachment.vertices;
										int deformLength = (weighted ? (vertices.Length / 3 << 1) : vertices.Length);
										DeformTimeline timeline17 = new DeformTimeline(frames5, frames5, slot.Index, vertexAttachment);
										float time16 = SkeletonJson.GetFloat(keyMap11, "time", 0f);
										int frame11 = 0;
										int bezier8 = 0;
										for (;;)
										{
											float[] deform;
											if (!keyMap11.ContainsKey("vertices"))
											{
												deform = (weighted ? new float[deformLength] : vertices);
											}
											else
											{
												deform = new float[deformLength];
												int start = SkeletonJson.GetInt(keyMap11, "offset", 0);
												float[] verticesValue = SkeletonJson.GetFloatArray(keyMap11, "vertices", 1f);
												Array.Copy(verticesValue, 0, deform, start, verticesValue.Length);
												if (scale != 1f)
												{
													int k = start;
													int l = k + verticesValue.Length;
													while (k < l)
													{
														deform[k] *= scale;
														k++;
													}
												}
												if (!weighted)
												{
													for (int m = 0; m < deformLength; m++)
													{
														deform[m] += vertices[m];
													}
												}
											}
											timeline17.SetFrame(frame11, time16, deform);
											if (!keyMapEnumerator11.MoveNext())
											{
												break;
											}
											Dictionary<string, object> dictionary8 = (Dictionary<string, object>)keyMapEnumerator11.Current;
											float time17 = SkeletonJson.GetFloat(dictionary8, "time", 0f);
											if (keyMap11.ContainsKey("curve"))
											{
												bezier8 = SkeletonJson.ReadCurve(keyMap11["curve"], timeline17, bezier8, frame11, 0, time16, time17, 0f, 1f, 1f);
											}
											time16 = time17;
											keyMap11 = dictionary8;
											frame11++;
										}
										timeline17.Shrink(bezier8);
										timelines.Add(timeline17);
									}
									else if (timelineName5 == "sequence")
									{
										SequenceTimeline timeline18 = new SequenceTimeline(frames5, slot.index, attachment);
										float lastDelay = 0f;
										int frame12 = 0;
										while (keyMap11 != null)
										{
											float delay = SkeletonJson.GetFloat(keyMap11, "delay", lastDelay);
											SequenceMode sequenceMode = (SequenceMode)Enum.Parse(typeof(SequenceMode), SkeletonJson.GetString(keyMap11, "mode", "hold"), true);
											timeline18.SetFrame(frame12, SkeletonJson.GetFloat(keyMap11, "time", 0f), sequenceMode, SkeletonJson.GetInt(keyMap11, "index", 0), delay);
											lastDelay = delay;
											keyMap11 = (keyMapEnumerator11.MoveNext() ? ((Dictionary<string, object>)keyMapEnumerator11.Current) : null);
											frame12++;
										}
										timelines.Add(timeline18);
									}
								}
							}
						}
					}
				}
			}
			if (map.ContainsKey("drawOrder"))
			{
				List<object> list = (List<object>)map["drawOrder"];
				DrawOrderTimeline timeline19 = new DrawOrderTimeline(list.Count);
				int slotCount = skeletonData.slots.Count;
				int frame13 = 0;
				foreach (object obj10 in list)
				{
					Dictionary<string, object> keyMap12 = (Dictionary<string, object>)obj10;
					int[] drawOrder = null;
					if (keyMap12.ContainsKey("offsets"))
					{
						drawOrder = new int[slotCount];
						for (int n = slotCount - 1; n >= 0; n--)
						{
							drawOrder[n] = -1;
						}
						List<object> offsets = (List<object>)keyMap12["offsets"];
						int[] unchanged = new int[slotCount - offsets.Count];
						int originalIndex = 0;
						int unchangedIndex = 0;
						using (List<object>.Enumerator enumerator6 = offsets.GetEnumerator())
						{
							while (enumerator6.MoveNext())
							{
								object obj11 = enumerator6.Current;
								Dictionary<string, object> offsetMap = (Dictionary<string, object>)obj11;
								int slotIndex2 = this.FindSlotIndex(skeletonData, (string)offsetMap["slot"]);
								while (originalIndex != slotIndex2)
								{
									unchanged[unchangedIndex++] = originalIndex++;
								}
								int index2 = originalIndex + (int)((float)offsetMap["offset"]);
								drawOrder[index2] = originalIndex++;
							}
							goto IL_1E25;
						}
						goto IL_1E14;
						IL_1E25:
						if (originalIndex >= slotCount)
						{
							for (int i2 = slotCount - 1; i2 >= 0; i2--)
							{
								if (drawOrder[i2] == -1)
								{
									drawOrder[i2] = unchanged[--unchangedIndex];
								}
							}
							goto IL_1E55;
						}
						IL_1E14:
						unchanged[unchangedIndex++] = originalIndex++;
						goto IL_1E25;
					}
					IL_1E55:
					timeline19.SetFrame(frame13, SkeletonJson.GetFloat(keyMap12, "time", 0f), drawOrder);
					frame13++;
				}
				timelines.Add(timeline19);
			}
			if (map.ContainsKey("events"))
			{
				List<object> list2 = (List<object>)map["events"];
				EventTimeline timeline20 = new EventTimeline(list2.Count);
				int frame14 = 0;
				foreach (object obj12 in list2)
				{
					Dictionary<string, object> keyMap13 = (Dictionary<string, object>)obj12;
					EventData eventData = skeletonData.FindEvent((string)keyMap13["name"]);
					if (eventData == null)
					{
						string text13 = "Event not found: ";
						object obj13 = keyMap13["name"];
						throw new Exception(text13 + ((obj13 != null) ? obj13.ToString() : null));
					}
					Event e = new Event(SkeletonJson.GetFloat(keyMap13, "time", 0f), eventData)
					{
						intValue = SkeletonJson.GetInt(keyMap13, "int", eventData.Int),
						floatValue = SkeletonJson.GetFloat(keyMap13, "float", eventData.Float),
						stringValue = SkeletonJson.GetString(keyMap13, "string", eventData.String)
					};
					if (e.data.AudioPath != null)
					{
						e.volume = SkeletonJson.GetFloat(keyMap13, "volume", eventData.Volume);
						e.balance = SkeletonJson.GetFloat(keyMap13, "balance", eventData.Balance);
					}
					timeline20.SetFrame(frame14, e);
					frame14++;
				}
				timelines.Add(timeline20);
			}
			timelines.TrimExcess();
			float duration = 0f;
			Timeline[] items = timelines.Items;
			int i3 = 0;
			int n2 = timelines.Count;
			while (i3 < n2)
			{
				duration = Math.Max(duration, items[i3].Duration);
				i3++;
			}
			skeletonData.animations.Add(new Animation(name, timelines, duration));
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001B0F4 File Offset: 0x000192F4
		private static Timeline ReadTimeline(ref List<object>.Enumerator keyMapEnumerator, CurveTimeline1 timeline, float defaultValue, float scale)
		{
			Dictionary<string, object> keyMap = (Dictionary<string, object>)keyMapEnumerator.Current;
			float time = SkeletonJson.GetFloat(keyMap, "time", 0f);
			float value = SkeletonJson.GetFloat(keyMap, "value", defaultValue) * scale;
			int frame = 0;
			int bezier = 0;
			for (;;)
			{
				timeline.SetFrame(frame, time, value);
				if (!keyMapEnumerator.MoveNext())
				{
					break;
				}
				Dictionary<string, object> dictionary = (Dictionary<string, object>)keyMapEnumerator.Current;
				float time2 = SkeletonJson.GetFloat(dictionary, "time", 0f);
				float value2 = SkeletonJson.GetFloat(dictionary, "value", defaultValue) * scale;
				if (keyMap.ContainsKey("curve"))
				{
					bezier = SkeletonJson.ReadCurve(keyMap["curve"], timeline, bezier, frame, 0, time, time2, value, value2, scale);
				}
				time = time2;
				value = value2;
				keyMap = dictionary;
				frame++;
			}
			timeline.Shrink(bezier);
			return timeline;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001B1B4 File Offset: 0x000193B4
		private static Timeline ReadTimeline(ref List<object>.Enumerator keyMapEnumerator, CurveTimeline2 timeline, string name1, string name2, float defaultValue, float scale)
		{
			Dictionary<string, object> keyMap = (Dictionary<string, object>)keyMapEnumerator.Current;
			float time = SkeletonJson.GetFloat(keyMap, "time", 0f);
			float value = SkeletonJson.GetFloat(keyMap, name1, defaultValue) * scale;
			float value2 = SkeletonJson.GetFloat(keyMap, name2, defaultValue) * scale;
			int frame = 0;
			int bezier = 0;
			for (;;)
			{
				timeline.SetFrame(frame, time, value, value2);
				if (!keyMapEnumerator.MoveNext())
				{
					break;
				}
				Dictionary<string, object> dictionary = (Dictionary<string, object>)keyMapEnumerator.Current;
				float time2 = SkeletonJson.GetFloat(dictionary, "time", 0f);
				float nvalue = SkeletonJson.GetFloat(dictionary, name1, defaultValue) * scale;
				float nvalue2 = SkeletonJson.GetFloat(dictionary, name2, defaultValue) * scale;
				if (keyMap.ContainsKey("curve"))
				{
					object obj = keyMap["curve"];
					bezier = SkeletonJson.ReadCurve(obj, timeline, bezier, frame, 0, time, time2, value, nvalue, scale);
					bezier = SkeletonJson.ReadCurve(obj, timeline, bezier, frame, 1, time, time2, value2, nvalue2, scale);
				}
				time = time2;
				value = nvalue;
				value2 = nvalue2;
				keyMap = dictionary;
				frame++;
			}
			timeline.Shrink(bezier);
			return timeline;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001B2AC File Offset: 0x000194AC
		private static int ReadCurve(object curve, CurveTimeline timeline, int bezier, int frame, int value, float time1, float time2, float value1, float value2, float scale)
		{
			string curveString = curve as string;
			if (curveString != null)
			{
				if (curveString == "stepped")
				{
					timeline.SetStepped(frame);
				}
				return bezier;
			}
			List<object> list = (List<object>)curve;
			int i = value << 2;
			float cx = (float)list[i];
			float cy = (float)list[i + 1] * scale;
			float cx2 = (float)list[i + 2];
			float cy2 = (float)list[i + 3] * scale;
			SkeletonJson.SetBezier(timeline, frame, value, bezier, time1, value1, cx, cy, cx2, cy2, time2, value2);
			return bezier + 1;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001B340 File Offset: 0x00019540
		private static void SetBezier(CurveTimeline timeline, int frame, int value, int bezier, float time1, float value1, float cx1, float cy1, float cx2, float cy2, float time2, float value2)
		{
			timeline.SetBezier(bezier, frame, value, time1, value1, cx1, cy1, cx2, cy2, time2, value2);
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001B368 File Offset: 0x00019568
		private static float[] GetFloatArray(Dictionary<string, object> map, string name, float scale)
		{
			List<object> list = (List<object>)map[name];
			float[] values = new float[list.Count];
			if (scale == 1f)
			{
				int i = 0;
				int j = list.Count;
				while (i < j)
				{
					values[i] = (float)list[i];
					i++;
				}
			}
			else
			{
				int k = 0;
				int l = list.Count;
				while (k < l)
				{
					values[k] = (float)list[k] * scale;
					k++;
				}
			}
			return values;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001B3E8 File Offset: 0x000195E8
		private static int[] GetIntArray(Dictionary<string, object> map, string name)
		{
			List<object> list = (List<object>)map[name];
			int[] values = new int[list.Count];
			int i = 0;
			int j = list.Count;
			while (i < j)
			{
				values[i] = (int)((float)list[i]);
				i++;
			}
			return values;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001B432 File Offset: 0x00019632
		private static float GetFloat(Dictionary<string, object> map, string name, float defaultValue)
		{
			if (!map.ContainsKey(name))
			{
				return defaultValue;
			}
			return (float)map[name];
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001B44B File Offset: 0x0001964B
		private static int GetInt(Dictionary<string, object> map, string name, int defaultValue)
		{
			if (!map.ContainsKey(name))
			{
				return defaultValue;
			}
			return (int)((float)map[name]);
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001B465 File Offset: 0x00019665
		private static int GetInt(Dictionary<string, object> map, string name)
		{
			if (!map.ContainsKey(name))
			{
				throw new ArgumentException("Named value not found: " + name);
			}
			return (int)((float)map[name]);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001B48E File Offset: 0x0001968E
		private static bool GetBoolean(Dictionary<string, object> map, string name, bool defaultValue)
		{
			if (!map.ContainsKey(name))
			{
				return defaultValue;
			}
			return (bool)map[name];
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001B4A7 File Offset: 0x000196A7
		private static string GetString(Dictionary<string, object> map, string name, string defaultValue)
		{
			if (!map.ContainsKey(name))
			{
				return defaultValue;
			}
			return (string)map[name];
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001B4C0 File Offset: 0x000196C0
		private static float ToColor(string hexString, int colorIndex, int expectedLength = 8)
		{
			if (hexString.Length < expectedLength)
			{
				throw new ArgumentException("Color hexadecimal length must be " + expectedLength.ToString() + ", received: " + hexString, "hexString");
			}
			return (float)Convert.ToInt32(hexString.Substring(colorIndex * 2, 2), 16) / 255f;
		}

		// Token: 0x040002AC RID: 684
		private readonly List<SkeletonJson.LinkedMesh> linkedMeshes = new List<SkeletonJson.LinkedMesh>();

		// Token: 0x0200007B RID: 123
		private class LinkedMesh
		{
			// Token: 0x060004AE RID: 1198 RVA: 0x0001B510 File Offset: 0x00019710
			public LinkedMesh(MeshAttachment mesh, string skin, int slotIndex, string parent, bool inheritTimelines)
			{
				this.mesh = mesh;
				this.skin = skin;
				this.slotIndex = slotIndex;
				this.parent = parent;
				this.inheritTimelines = inheritTimelines;
			}

			// Token: 0x040002AD RID: 685
			internal string parent;

			// Token: 0x040002AE RID: 686
			internal string skin;

			// Token: 0x040002AF RID: 687
			internal int slotIndex;

			// Token: 0x040002B0 RID: 688
			internal MeshAttachment mesh;

			// Token: 0x040002B1 RID: 689
			internal bool inheritTimelines;
		}
	}
}
