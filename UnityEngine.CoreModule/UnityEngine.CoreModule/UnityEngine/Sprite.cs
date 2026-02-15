using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200008A RID: 138
	[NativeHeader("Runtime/Graphics/SpriteUtility.h")]
	[NativeHeader("Runtime/2D/Common/ScriptBindings/SpritesMarshalling.h")]
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	[ExcludeFromPreset]
	[NativeType("Runtime/Graphics/SpriteFrame.h")]
	public sealed class Sprite : Object
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x00004DC7 File Offset: 0x00002FC7
		[RequiredByNativeCode]
		private Sprite()
		{
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00004DD4 File Offset: 0x00002FD4
		internal int GetPackingMode()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.GetPackingMode_Injected(intPtr);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00004DF8 File Offset: 0x00002FF8
		internal int GetPackingRotation()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.GetPackingRotation_Injected(intPtr);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00004E1C File Offset: 0x0000301C
		internal int GetPacked()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.GetPacked_Injected(intPtr);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00004E40 File Offset: 0x00003040
		internal Rect GetTextureRect()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Rect rect;
			Sprite.GetTextureRect_Injected(intPtr, out rect);
			return rect;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00004E68 File Offset: 0x00003068
		internal Vector2 GetTextureRectOffset()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			Sprite.GetTextureRectOffset_Injected(intPtr, out vector);
			return vector;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00004E90 File Offset: 0x00003090
		internal Vector4 GetInnerUVs()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector4 vector;
			Sprite.GetInnerUVs_Injected(intPtr, out vector);
			return vector;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00004EB8 File Offset: 0x000030B8
		internal Vector4 GetOuterUVs()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector4 vector;
			Sprite.GetOuterUVs_Injected(intPtr, out vector);
			return vector;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00004EE0 File Offset: 0x000030E0
		internal Vector4 GetPadding()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector4 vector;
			Sprite.GetPadding_Injected(intPtr, out vector);
			return vector;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00004F08 File Offset: 0x00003108
		[FreeFunction("SpritesBindings::CreateSpriteWithoutTextureScripting")]
		internal static Sprite CreateSpriteWithoutTextureScripting(Rect rect, Vector2 pivot, float pixelsToUnits, Texture2D texture)
		{
			return Unmarshal.UnmarshalUnityObject<Sprite>(Sprite.CreateSpriteWithoutTextureScripting_Injected(ref rect, ref pivot, pixelsToUnits, Object.MarshalledUnityObject.Marshal<Texture2D>(texture)));
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00004F2C File Offset: 0x0000312C
		[FreeFunction("SpritesBindings::CreateSprite", ThrowsException = true)]
		internal static Sprite CreateSprite(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border, bool generateFallbackPhysicsShape, [Unmarshalled] SecondarySpriteTexture[] secondaryTexture)
		{
			return Unmarshal.UnmarshalUnityObject<Sprite>(Sprite.CreateSprite_Injected(Object.MarshalledUnityObject.Marshal<Texture2D>(texture), ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref border, generateFallbackPhysicsShape, secondaryTexture));
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00004F58 File Offset: 0x00003158
		public Bounds bounds
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Bounds bounds;
				Sprite.get_bounds_Injected(intPtr, out bounds);
				return bounds;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00004F80 File Offset: 0x00003180
		public Rect rect
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Rect rect;
				Sprite.get_rect_Injected(intPtr, out rect);
				return rect;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00004FA8 File Offset: 0x000031A8
		public Vector4 border
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector4 vector;
				Sprite.get_border_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00004FD0 File Offset: 0x000031D0
		public Texture2D texture
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Texture2D>(Sprite.get_texture_Injected(intPtr));
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00004FF8 File Offset: 0x000031F8
		internal Texture2D GetSecondaryTexture(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Texture2D>(Sprite.GetSecondaryTexture_Injected(intPtr, index));
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005020 File Offset: 0x00003220
		public int GetSecondaryTextureCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.GetSecondaryTextureCount_Injected(intPtr);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005044 File Offset: 0x00003244
		[FreeFunction("SpritesBindings::GetSecondaryTextures", ThrowsException = true, HasExplicitThis = true)]
		public int GetSecondaryTextures([Unmarshalled] [NotNull] SecondarySpriteTexture[] secondaryTexture)
		{
			if (secondaryTexture == null)
			{
				ThrowHelper.ThrowArgumentNullException(secondaryTexture, "secondaryTexture");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.GetSecondaryTextures_Injected(intPtr, secondaryTexture);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00005078 File Offset: 0x00003278
		public float pixelsPerUnit
		{
			[NativeMethod("GetPixelsToUnits")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Sprite.get_pixelsPerUnit_Injected(intPtr);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000509C File Offset: 0x0000329C
		public float spriteAtlasTextureScale
		{
			[NativeMethod("GetSpriteAtlasTextureScale")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Sprite.get_spriteAtlasTextureScale_Injected(intPtr);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x000050C0 File Offset: 0x000032C0
		public Texture2D associatedAlphaSplitTexture
		{
			[NativeMethod("GetAlphaTexture")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Texture2D>(Sprite.get_associatedAlphaSplitTexture_Injected(intPtr));
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x000050E8 File Offset: 0x000032E8
		public Vector2 pivot
		{
			[NativeMethod("GetPivotInPixels")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector2 vector;
				Sprite.get_pivot_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00005110 File Offset: 0x00003310
		public bool packed
		{
			get
			{
				return this.GetPacked() == 1;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000512C File Offset: 0x0000332C
		public SpritePackingMode packingMode
		{
			get
			{
				return (SpritePackingMode)this.GetPackingMode();
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00005144 File Offset: 0x00003344
		public SpritePackingRotation packingRotation
		{
			get
			{
				return (SpritePackingRotation)this.GetPackingRotation();
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000515C File Offset: 0x0000335C
		public Rect textureRect
		{
			get
			{
				return this.GetTextureRect();
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00005174 File Offset: 0x00003374
		public Vector2 textureRectOffset
		{
			get
			{
				return this.GetTextureRectOffset();
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000518C File Offset: 0x0000338C
		public Vector2[] vertices
		{
			[FreeFunction("SpriteAccessLegacy::GetSpriteVertices", HasExplicitThis = true)]
			[return: Unmarshalled]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Sprite.get_vertices_Injected(intPtr);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000051B0 File Offset: 0x000033B0
		public ushort[] triangles
		{
			[FreeFunction("SpriteAccessLegacy::GetSpriteIndices", HasExplicitThis = true)]
			[return: Unmarshalled]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Sprite.get_triangles_Injected(intPtr);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000051D4 File Offset: 0x000033D4
		public Vector2[] uv
		{
			[FreeFunction("SpriteAccessLegacy::GetSpriteUVs", HasExplicitThis = true)]
			[return: Unmarshalled]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Sprite.get_uv_Injected(intPtr);
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000051F8 File Offset: 0x000033F8
		public int GetPhysicsShapeCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.GetPhysicsShapeCount_Injected(intPtr);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000521C File Offset: 0x0000341C
		public uint GetScriptableObjectsCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.GetScriptableObjectsCount_Injected(intPtr);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00005240 File Offset: 0x00003440
		[FreeFunction("SpritesBindings::GetScriptableObjects", ThrowsException = true, HasExplicitThis = true)]
		public uint GetScriptableObjects([Unmarshalled] [NotNull] ScriptableObject[] scriptableObjects)
		{
			if (scriptableObjects == null)
			{
				ThrowHelper.ThrowArgumentNullException(scriptableObjects, "scriptableObjects");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.GetScriptableObjects_Injected(intPtr, scriptableObjects);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00005274 File Offset: 0x00003474
		public bool AddScriptableObject([NotNull] ScriptableObject obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ScriptableObject>(obj);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			return Sprite.AddScriptableObject_Injected(intPtr, intPtr2);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000052BC File Offset: 0x000034BC
		public bool RemoveScriptableObjectAt(uint i)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.RemoveScriptableObjectAt_Injected(intPtr, i);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000052E0 File Offset: 0x000034E0
		public bool SetScriptableObjectAt([NotNull] ScriptableObject obj, uint i)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<ScriptableObject>(obj);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			return Sprite.SetScriptableObjectAt_Injected(intPtr, intPtr2, i);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005328 File Offset: 0x00003528
		public int GetPhysicsShapePointCount(int shapeIdx)
		{
			int physicsShapeCount = this.GetPhysicsShapeCount();
			bool flag = shapeIdx < 0 || shapeIdx >= physicsShapeCount;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("Index({0}) is out of bounds(0 - {1})", shapeIdx, physicsShapeCount - 1));
			}
			return this.Internal_GetPhysicsShapePointCount(shapeIdx);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00005378 File Offset: 0x00003578
		[NativeMethod("GetPhysicsShapePointCount")]
		private int Internal_GetPhysicsShapePointCount(int shapeIdx)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Sprite.Internal_GetPhysicsShapePointCount_Injected(intPtr, shapeIdx);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000539C File Offset: 0x0000359C
		public int GetPhysicsShape(int shapeIdx, List<Vector2> physicsShape)
		{
			int physicsShapeCount = this.GetPhysicsShapeCount();
			bool flag = shapeIdx < 0 || shapeIdx >= physicsShapeCount;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("Index({0}) is out of bounds(0 - {1})", shapeIdx, physicsShapeCount - 1));
			}
			Sprite.GetPhysicsShapeImpl(this, shapeIdx, physicsShape);
			return physicsShape.Count;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000053F4 File Offset: 0x000035F4
		[FreeFunction("SpritesBindings::GetPhysicsShape", ThrowsException = true)]
		private unsafe static void GetPhysicsShapeImpl(Sprite sprite, int shapeIdx, [NotNull] List<Vector2> physicsShape)
		{
			if (physicsShape == null)
			{
				ThrowHelper.ThrowArgumentNullException(physicsShape, "physicsShape");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Sprite>(sprite);
				fixed (Vector2[] array = NoAllocHelpers.ExtractArrayFromList<Vector2>(physicsShape))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, physicsShape.Count);
					Sprite.GetPhysicsShapeImpl_Injected(intPtr, shapeIdx, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<Vector2>(physicsShape);
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00005468 File Offset: 0x00003668
		public void OverridePhysicsShape(IList<Vector2[]> physicsShapes)
		{
			bool flag = physicsShapes == null;
			if (flag)
			{
				throw new ArgumentNullException("physicsShapes");
			}
			for (int i = 0; i < physicsShapes.Count; i++)
			{
				Vector2[] physicsShape = physicsShapes[i];
				bool flag2 = physicsShape == null;
				if (flag2)
				{
					throw new ArgumentNullException("physicsShape", string.Format("Physics Shape at {0} is null.", i));
				}
				bool flag3 = physicsShape.Length < 3;
				if (flag3)
				{
					throw new ArgumentException(string.Format("Physics Shape at {0} has less than 3 vertices ({1}).", i, physicsShape.Length));
				}
			}
			Sprite.OverridePhysicsShapeCount(this, physicsShapes.Count);
			for (int idx = 0; idx < physicsShapes.Count; idx++)
			{
				Sprite.OverridePhysicsShape(this, physicsShapes[idx], idx);
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005534 File Offset: 0x00003734
		[FreeFunction("SpritesBindings::OverridePhysicsShapeCount")]
		private static void OverridePhysicsShapeCount(Sprite sprite, int physicsShapeCount)
		{
			Sprite.OverridePhysicsShapeCount_Injected(Object.MarshalledUnityObject.Marshal<Sprite>(sprite), physicsShapeCount);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00005550 File Offset: 0x00003750
		[FreeFunction("SpritesBindings::OverridePhysicsShape", ThrowsException = true)]
		private unsafe static void OverridePhysicsShape(Sprite sprite, [NotNull] Vector2[] physicsShape, int idx)
		{
			if (physicsShape == null)
			{
				ThrowHelper.ThrowArgumentNullException(physicsShape, "physicsShape");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Sprite>(sprite);
			Span<Vector2> span = new Span<Vector2>(physicsShape);
			fixed (Vector2* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Sprite.OverridePhysicsShape_Injected(intPtr, ref managedSpanWrapper, idx);
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x000055A0 File Offset: 0x000037A0
		[FreeFunction("SpritesBindings::OverrideGeometry", HasExplicitThis = true)]
		public unsafe void OverrideGeometry([NotNull] Vector2[] vertices, [NotNull] ushort[] triangles)
		{
			if (vertices == null)
			{
				ThrowHelper.ThrowArgumentNullException(vertices, "vertices");
			}
			if (triangles == null)
			{
				ThrowHelper.ThrowArgumentNullException(triangles, "triangles");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Sprite>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector2> span = new Span<Vector2>(vertices);
			fixed (Vector2* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<ushort> span2 = new Span<ushort>(triangles);
				fixed (ushort* pinnableReference = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)pinnableReference, span2.Length);
					Sprite.OverrideGeometry_Injected(intPtr, ref managedSpanWrapper, ref managedSpanWrapper2);
					ptr = null;
				}
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00005630 File Offset: 0x00003830
		internal static Sprite Create(Rect rect, Vector2 pivot, float pixelsToUnits, Texture2D texture)
		{
			return Sprite.CreateSpriteWithoutTextureScripting(rect, pivot, pixelsToUnits, texture);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000564C File Offset: 0x0000384C
		internal static Sprite Create(Rect rect, Vector2 pivot, float pixelsToUnits)
		{
			return Sprite.CreateSpriteWithoutTextureScripting(rect, pivot, pixelsToUnits, null);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00005668 File Offset: 0x00003868
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border, bool generateFallbackPhysicsShape)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, meshType, border, generateFallbackPhysicsShape, null);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000568C File Offset: 0x0000388C
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border, bool generateFallbackPhysicsShape, SecondarySpriteTexture[] secondaryTextures)
		{
			bool flag = texture == null;
			Sprite sprite;
			if (flag)
			{
				sprite = null;
			}
			else
			{
				bool flag2 = rect.xMax > (float)texture.width || rect.yMax > (float)texture.height;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Could not create sprite ({0}, {1}, {2}, {3}) from a {4}x{5} texture.", new object[] { rect.x, rect.y, rect.width, rect.height, texture.width, texture.height }));
				}
				bool flag3 = pixelsPerUnit <= 0f;
				if (flag3)
				{
					throw new ArgumentException("pixelsPerUnit must be set to a positive non-zero value.");
				}
				bool flag4 = secondaryTextures != null;
				if (flag4)
				{
					foreach (SecondarySpriteTexture st in secondaryTextures)
					{
						bool flag5 = st.texture == texture;
						if (flag5)
						{
							throw new ArgumentException(string.Format("{0} is using source Texture as Secondary Texture.", st.name));
						}
					}
				}
				sprite = Sprite.CreateSprite(texture, rect, pivot, pixelsPerUnit, extrude, meshType, border, generateFallbackPhysicsShape, secondaryTextures);
			}
			return sprite;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000057D0 File Offset: 0x000039D0
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, meshType, border, false);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000057F4 File Offset: 0x000039F4
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, meshType, Vector4.zero);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00005818 File Offset: 0x00003A18
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, SpriteMeshType.Tight);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00005838 File Offset: 0x00003A38
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, 0U);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00005854 File Offset: 0x00003A54
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot)
		{
			return Sprite.Create(texture, rect, pivot, 100f);
		}

		// Token: 0x060001F5 RID: 501
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPackingMode_Injected(IntPtr _unity_self);

		// Token: 0x060001F6 RID: 502
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPackingRotation_Injected(IntPtr _unity_self);

		// Token: 0x060001F7 RID: 503
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPacked_Injected(IntPtr _unity_self);

		// Token: 0x060001F8 RID: 504
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTextureRect_Injected(IntPtr _unity_self, out Rect ret);

		// Token: 0x060001F9 RID: 505
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTextureRectOffset_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x060001FA RID: 506
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetInnerUVs_Injected(IntPtr _unity_self, out Vector4 ret);

		// Token: 0x060001FB RID: 507
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOuterUVs_Injected(IntPtr _unity_self, out Vector4 ret);

		// Token: 0x060001FC RID: 508
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPadding_Injected(IntPtr _unity_self, out Vector4 ret);

		// Token: 0x060001FD RID: 509
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateSpriteWithoutTextureScripting_Injected([In] ref Rect rect, [In] ref Vector2 pivot, float pixelsToUnits, IntPtr texture);

		// Token: 0x060001FE RID: 510
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateSprite_Injected(IntPtr texture, [In] ref Rect rect, [In] ref Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, [In] ref Vector4 border, bool generateFallbackPhysicsShape, SecondarySpriteTexture[] secondaryTexture);

		// Token: 0x060001FF RID: 511
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bounds_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x06000200 RID: 512
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rect_Injected(IntPtr _unity_self, out Rect ret);

		// Token: 0x06000201 RID: 513
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_border_Injected(IntPtr _unity_self, out Vector4 ret);

		// Token: 0x06000202 RID: 514
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_texture_Injected(IntPtr _unity_self);

		// Token: 0x06000203 RID: 515
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetSecondaryTexture_Injected(IntPtr _unity_self, int index);

		// Token: 0x06000204 RID: 516
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSecondaryTextureCount_Injected(IntPtr _unity_self);

		// Token: 0x06000205 RID: 517
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSecondaryTextures_Injected(IntPtr _unity_self, SecondarySpriteTexture[] secondaryTexture);

		// Token: 0x06000206 RID: 518
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_pixelsPerUnit_Injected(IntPtr _unity_self);

		// Token: 0x06000207 RID: 519
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_spriteAtlasTextureScale_Injected(IntPtr _unity_self);

		// Token: 0x06000208 RID: 520
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_associatedAlphaSplitTexture_Injected(IntPtr _unity_self);

		// Token: 0x06000209 RID: 521
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_pivot_Injected(IntPtr _unity_self, out Vector2 ret);

		// Token: 0x0600020A RID: 522
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector2[] get_vertices_Injected(IntPtr _unity_self);

		// Token: 0x0600020B RID: 523
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ushort[] get_triangles_Injected(IntPtr _unity_self);

		// Token: 0x0600020C RID: 524
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector2[] get_uv_Injected(IntPtr _unity_self);

		// Token: 0x0600020D RID: 525
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPhysicsShapeCount_Injected(IntPtr _unity_self);

		// Token: 0x0600020E RID: 526
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetScriptableObjectsCount_Injected(IntPtr _unity_self);

		// Token: 0x0600020F RID: 527
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetScriptableObjects_Injected(IntPtr _unity_self, ScriptableObject[] scriptableObjects);

		// Token: 0x06000210 RID: 528
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool AddScriptableObject_Injected(IntPtr _unity_self, IntPtr obj);

		// Token: 0x06000211 RID: 529
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveScriptableObjectAt_Injected(IntPtr _unity_self, uint i);

		// Token: 0x06000212 RID: 530
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetScriptableObjectAt_Injected(IntPtr _unity_self, IntPtr obj, uint i);

		// Token: 0x06000213 RID: 531
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetPhysicsShapePointCount_Injected(IntPtr _unity_self, int shapeIdx);

		// Token: 0x06000214 RID: 532
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPhysicsShapeImpl_Injected(IntPtr sprite, int shapeIdx, ref BlittableListWrapper physicsShape);

		// Token: 0x06000215 RID: 533
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OverridePhysicsShapeCount_Injected(IntPtr sprite, int physicsShapeCount);

		// Token: 0x06000216 RID: 534
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OverridePhysicsShape_Injected(IntPtr sprite, ref ManagedSpanWrapper physicsShape, int idx);

		// Token: 0x06000217 RID: 535
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OverrideGeometry_Injected(IntPtr _unity_self, ref ManagedSpanWrapper vertices, ref ManagedSpanWrapper triangles);
	}
}
