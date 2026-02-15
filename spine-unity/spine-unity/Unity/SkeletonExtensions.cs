using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000077 RID: 119
	public static class SkeletonExtensions
	{
		// Token: 0x06000348 RID: 840 RVA: 0x0001313A File Offset: 0x0001133A
		public static Color GetColor(this Skeleton s)
		{
			return new Color(s.R, s.G, s.B, s.A);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00013159 File Offset: 0x00011359
		public static Color GetColor(this RegionAttachment a)
		{
			return new Color(a.R, a.G, a.B, a.A);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00013178 File Offset: 0x00011378
		public static Color GetColor(this MeshAttachment a)
		{
			return new Color(a.R, a.G, a.B, a.A);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00013197 File Offset: 0x00011397
		public static Color GetColor(this Slot s)
		{
			return new Color(s.R, s.G, s.B, s.A);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000131B6 File Offset: 0x000113B6
		public static Color GetColorTintBlack(this Slot s)
		{
			return new Color(s.R2, s.G2, s.B2, 1f);
		}

		// Token: 0x0600034D RID: 845 RVA: 0x000131D4 File Offset: 0x000113D4
		public static void SetColor(this Skeleton skeleton, Color color)
		{
			skeleton.A = color.a;
			skeleton.R = color.r;
			skeleton.G = color.g;
			skeleton.B = color.b;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00013208 File Offset: 0x00011408
		public static void SetColor(this Skeleton skeleton, Color32 color)
		{
			skeleton.A = (float)color.a * 0.003921569f;
			skeleton.R = (float)color.r * 0.003921569f;
			skeleton.G = (float)color.g * 0.003921569f;
			skeleton.B = (float)color.b * 0.003921569f;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00013261 File Offset: 0x00011461
		public static void SetColor(this Slot slot, Color color)
		{
			slot.A = color.a;
			slot.R = color.r;
			slot.G = color.g;
			slot.B = color.b;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00013294 File Offset: 0x00011494
		public static void SetColor(this Slot slot, Color32 color)
		{
			slot.A = (float)color.a * 0.003921569f;
			slot.R = (float)color.r * 0.003921569f;
			slot.G = (float)color.g * 0.003921569f;
			slot.B = (float)color.b * 0.003921569f;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000132ED File Offset: 0x000114ED
		public static void SetColor(this RegionAttachment attachment, Color color)
		{
			attachment.A = color.a;
			attachment.R = color.r;
			attachment.G = color.g;
			attachment.B = color.b;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00013320 File Offset: 0x00011520
		public static void SetColor(this RegionAttachment attachment, Color32 color)
		{
			attachment.A = (float)color.a * 0.003921569f;
			attachment.R = (float)color.r * 0.003921569f;
			attachment.G = (float)color.g * 0.003921569f;
			attachment.B = (float)color.b * 0.003921569f;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00013379 File Offset: 0x00011579
		public static void SetColor(this MeshAttachment attachment, Color color)
		{
			attachment.A = color.a;
			attachment.R = color.r;
			attachment.G = color.g;
			attachment.B = color.b;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000133AC File Offset: 0x000115AC
		public static void SetColor(this MeshAttachment attachment, Color32 color)
		{
			attachment.A = (float)color.a * 0.003921569f;
			attachment.R = (float)color.r * 0.003921569f;
			attachment.G = (float)color.g * 0.003921569f;
			attachment.B = (float)color.b * 0.003921569f;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00013405 File Offset: 0x00011605
		public static void SetLocalScale(this Skeleton skeleton, Vector2 scale)
		{
			skeleton.ScaleX = scale.x;
			skeleton.ScaleY = scale.y;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00013420 File Offset: 0x00011620
		public static Matrix4x4 GetMatrix4x4(this Bone bone)
		{
			return new Matrix4x4
			{
				m00 = bone.A,
				m01 = bone.B,
				m03 = bone.WorldX,
				m10 = bone.C,
				m11 = bone.D,
				m13 = bone.WorldY,
				m33 = 1f
			};
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00013490 File Offset: 0x00011690
		public static void SetLocalPosition(this Bone bone, Vector2 position)
		{
			bone.X = position.x;
			bone.Y = position.y;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000134AA File Offset: 0x000116AA
		public static void SetLocalPosition(this Bone bone, Vector3 position)
		{
			bone.X = position.x;
			bone.Y = position.y;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000134C4 File Offset: 0x000116C4
		public static Vector2 GetLocalPosition(this Bone bone)
		{
			return new Vector2(bone.X, bone.Y);
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000134D7 File Offset: 0x000116D7
		public static Vector2 GetSkeletonSpacePosition(this Bone bone)
		{
			return new Vector2(bone.WorldX, bone.WorldY);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000134EC File Offset: 0x000116EC
		public static Vector2 GetSkeletonSpacePosition(this Bone bone, Vector2 boneLocal)
		{
			Vector2 o;
			bone.LocalToWorld(boneLocal.x, boneLocal.y, out o.x, out o.y);
			return o;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001351A File Offset: 0x0001171A
		public static Vector3 GetWorldPosition(this Bone bone, Transform spineGameObjectTransform)
		{
			return spineGameObjectTransform.TransformPoint(new Vector3(bone.WorldX, bone.WorldY));
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00013533 File Offset: 0x00011733
		public static Vector3 GetWorldPosition(this Bone bone, Transform spineGameObjectTransform, float positionScale)
		{
			return spineGameObjectTransform.TransformPoint(new Vector3(bone.WorldX * positionScale, bone.WorldY * positionScale));
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00013550 File Offset: 0x00011750
		public static Vector3 GetWorldPosition(this Bone bone, Transform spineGameObjectTransform, float positionScale, Vector2 positionOffset)
		{
			return spineGameObjectTransform.TransformPoint(new Vector3(bone.WorldX * positionScale + positionOffset.x, bone.WorldY * positionScale + positionOffset.y));
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001357C File Offset: 0x0001177C
		public static Quaternion GetQuaternion(this Bone bone)
		{
			float halfRotation = Mathf.Atan2(bone.C, bone.A) * 0.5f;
			return new Quaternion(0f, 0f, Mathf.Sin(halfRotation), Mathf.Cos(halfRotation));
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000135BC File Offset: 0x000117BC
		public static Quaternion GetLocalQuaternion(this Bone bone)
		{
			float halfRotation = bone.Rotation * 0.017453292f * 0.5f;
			return new Quaternion(0f, 0f, Mathf.Sin(halfRotation), Mathf.Cos(halfRotation));
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000135F7 File Offset: 0x000117F7
		public static Vector2 GetLocalScale(this Skeleton skeleton)
		{
			return new Vector2(skeleton.ScaleX, skeleton.ScaleY);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001360C File Offset: 0x0001180C
		public static void GetWorldToLocalMatrix(this Bone bone, out float ia, out float ib, out float ic, out float id)
		{
			float a = bone.A;
			float b = bone.B;
			float c = bone.C;
			float d = bone.D;
			float invDet = 1f / (a * d - b * c);
			ia = invDet * d;
			ib = invDet * -b;
			ic = invDet * -c;
			id = invDet * a;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00013660 File Offset: 0x00011860
		public static Vector2 WorldToLocal(this Bone bone, Vector2 worldPosition)
		{
			Vector2 o;
			bone.WorldToLocal(worldPosition.x, worldPosition.y, out o.x, out o.y);
			return o;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00013690 File Offset: 0x00011890
		public static Vector2 SetPositionSkeletonSpace(this Bone bone, Vector2 skeletonSpacePosition)
		{
			if (bone.Parent == null)
			{
				bone.SetLocalPosition(skeletonSpacePosition);
				return skeletonSpacePosition;
			}
			Vector2 parentLocal = bone.Parent.WorldToLocal(skeletonSpacePosition);
			bone.SetLocalPosition(parentLocal);
			return parentLocal;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000136C4 File Offset: 0x000118C4
		public static Material GetMaterial(this Attachment a)
		{
			object rendererObject = null;
			IHasTextureRegion renderableAttachment = a as IHasTextureRegion;
			if (renderableAttachment != null)
			{
				rendererObject = renderableAttachment.Region;
			}
			if (rendererObject == null)
			{
				return null;
			}
			return (Material)((AtlasRegion)rendererObject).page.rendererObject;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00013700 File Offset: 0x00011900
		public static Vector2[] GetLocalVertices(this VertexAttachment va, Slot slot, Vector2[] buffer)
		{
			int floatsCount = va.WorldVerticesLength;
			int bufferTargetSize = floatsCount >> 1;
			buffer = buffer ?? new Vector2[bufferTargetSize];
			if (buffer.Length < bufferTargetSize)
			{
				throw new ArgumentException(string.Format("Vector2 buffer too small. {0} requires an array of size {1}. Use the attachment's .WorldVerticesLength to get the correct size.", va.Name, floatsCount), "buffer");
			}
			if (va.Bones == null && slot.Deform.Count == 0)
			{
				float[] localVerts = va.Vertices;
				for (int i = 0; i < bufferTargetSize; i++)
				{
					int j = i * 2;
					buffer[i] = new Vector2(localVerts[j], localVerts[j + 1]);
				}
			}
			else
			{
				float[] floats = new float[floatsCount];
				va.ComputeWorldVertices(slot, floats);
				Bone bone = slot.Bone;
				float bwx = bone.WorldX;
				float bwy = bone.WorldY;
				float ia;
				float ib;
				float ic;
				float id;
				bone.GetWorldToLocalMatrix(out ia, out ib, out ic, out id);
				for (int k = 0; k < bufferTargetSize; k++)
				{
					int l = k * 2;
					float x = floats[l] - bwx;
					float y = floats[l + 1] - bwy;
					buffer[k] = new Vector2(x * ia + y * ib, x * ic + y * id);
				}
			}
			return buffer;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00013818 File Offset: 0x00011A18
		public static Vector2[] GetWorldVertices(this VertexAttachment a, Slot slot, Vector2[] buffer)
		{
			int worldVertsLength = a.WorldVerticesLength;
			int bufferTargetSize = worldVertsLength >> 1;
			buffer = buffer ?? new Vector2[bufferTargetSize];
			if (buffer.Length < bufferTargetSize)
			{
				throw new ArgumentException(string.Format("Vector2 buffer too small. {0} requires an array of size {1}. Use the attachment's .WorldVerticesLength to get the correct size.", a.Name, worldVertsLength), "buffer");
			}
			float[] floats = new float[worldVertsLength];
			a.ComputeWorldVertices(slot, floats);
			int i = 0;
			int j = worldVertsLength >> 1;
			while (i < j)
			{
				int k = i * 2;
				buffer[i] = new Vector2(floats[k], floats[k + 1]);
				i++;
			}
			return buffer;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000138A4 File Offset: 0x00011AA4
		public static Vector3 GetWorldPosition(this PointAttachment attachment, Slot slot, Transform spineGameObjectTransform)
		{
			Vector3 skeletonSpacePosition;
			skeletonSpacePosition.z = 0f;
			attachment.ComputeWorldPosition(slot.Bone, out skeletonSpacePosition.x, out skeletonSpacePosition.y);
			return spineGameObjectTransform.TransformPoint(skeletonSpacePosition);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000138E0 File Offset: 0x00011AE0
		public static Vector3 GetWorldPosition(this PointAttachment attachment, Bone bone, Transform spineGameObjectTransform)
		{
			Vector3 skeletonSpacePosition;
			skeletonSpacePosition.z = 0f;
			attachment.ComputeWorldPosition(bone, out skeletonSpacePosition.x, out skeletonSpacePosition.y);
			return spineGameObjectTransform.TransformPoint(skeletonSpacePosition);
		}

		// Token: 0x0400022A RID: 554
		private const float ByteToFloat = 0.003921569f;
	}
}
