using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200019D RID: 413
	internal class AtlasAllocator
	{
		// Token: 0x06000B99 RID: 2969 RVA: 0x00029CBC File Offset: 0x00027EBC
		public AtlasAllocator(int width, int height, bool potPadding)
		{
			this.m_Root = new AtlasAllocator.AtlasNode();
			this.m_Root.m_Rect.Set((float)width, (float)height, 0f, 0f);
			this.m_Width = width;
			this.m_Height = height;
			this.powerOfTwoPadding = potPadding;
			this.m_NodePool = new ObjectPool<AtlasAllocator.AtlasNode>(delegate(AtlasAllocator.AtlasNode _)
			{
			}, delegate(AtlasAllocator.AtlasNode _)
			{
			}, true);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00029D58 File Offset: 0x00027F58
		public bool Allocate(ref Vector4 result, int width, int height)
		{
			AtlasAllocator.AtlasNode node = this.m_Root.Allocate(ref this.m_NodePool, width, height, this.powerOfTwoPadding);
			if (node != null)
			{
				result = node.m_Rect;
				return true;
			}
			result = Vector4.zero;
			return false;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00029D9C File Offset: 0x00027F9C
		public void Reset()
		{
			this.m_Root.Release(ref this.m_NodePool);
			this.m_Root.m_Rect.Set((float)this.m_Width, (float)this.m_Height, 0f, 0f);
		}

		// Token: 0x040007E8 RID: 2024
		private AtlasAllocator.AtlasNode m_Root;

		// Token: 0x040007E9 RID: 2025
		private int m_Width;

		// Token: 0x040007EA RID: 2026
		private int m_Height;

		// Token: 0x040007EB RID: 2027
		private bool powerOfTwoPadding;

		// Token: 0x040007EC RID: 2028
		private ObjectPool<AtlasAllocator.AtlasNode> m_NodePool;

		// Token: 0x0200019E RID: 414
		private class AtlasNode
		{
			// Token: 0x06000B9C RID: 2972 RVA: 0x00029DD8 File Offset: 0x00027FD8
			public AtlasAllocator.AtlasNode Allocate(ref ObjectPool<AtlasAllocator.AtlasNode> pool, int width, int height, bool powerOfTwoPadding)
			{
				if (this.m_RightChild != null)
				{
					AtlasAllocator.AtlasNode node = this.m_RightChild.Allocate(ref pool, width, height, powerOfTwoPadding);
					if (node == null)
					{
						node = this.m_BottomChild.Allocate(ref pool, width, height, powerOfTwoPadding);
					}
					return node;
				}
				int wPadd = 0;
				int hPadd = 0;
				if (powerOfTwoPadding)
				{
					wPadd = (int)this.m_Rect.x % width;
					hPadd = (int)this.m_Rect.y % height;
				}
				if ((float)width <= this.m_Rect.x - (float)wPadd && (float)height <= this.m_Rect.y - (float)hPadd)
				{
					this.m_RightChild = pool.Get();
					this.m_BottomChild = pool.Get();
					this.m_Rect.z = this.m_Rect.z + (float)wPadd;
					this.m_Rect.w = this.m_Rect.w + (float)hPadd;
					this.m_Rect.x = this.m_Rect.x - (float)wPadd;
					this.m_Rect.y = this.m_Rect.y - (float)hPadd;
					if (width > height)
					{
						this.m_RightChild.m_Rect.z = this.m_Rect.z + (float)width;
						this.m_RightChild.m_Rect.w = this.m_Rect.w;
						this.m_RightChild.m_Rect.x = this.m_Rect.x - (float)width;
						this.m_RightChild.m_Rect.y = (float)height;
						this.m_BottomChild.m_Rect.z = this.m_Rect.z;
						this.m_BottomChild.m_Rect.w = this.m_Rect.w + (float)height;
						this.m_BottomChild.m_Rect.x = this.m_Rect.x;
						this.m_BottomChild.m_Rect.y = this.m_Rect.y - (float)height;
					}
					else
					{
						this.m_RightChild.m_Rect.z = this.m_Rect.z + (float)width;
						this.m_RightChild.m_Rect.w = this.m_Rect.w;
						this.m_RightChild.m_Rect.x = this.m_Rect.x - (float)width;
						this.m_RightChild.m_Rect.y = this.m_Rect.y;
						this.m_BottomChild.m_Rect.z = this.m_Rect.z;
						this.m_BottomChild.m_Rect.w = this.m_Rect.w + (float)height;
						this.m_BottomChild.m_Rect.x = (float)width;
						this.m_BottomChild.m_Rect.y = this.m_Rect.y - (float)height;
					}
					this.m_Rect.x = (float)width;
					this.m_Rect.y = (float)height;
					return this;
				}
				return null;
			}

			// Token: 0x06000B9D RID: 2973 RVA: 0x0002A0A4 File Offset: 0x000282A4
			public void Release(ref ObjectPool<AtlasAllocator.AtlasNode> pool)
			{
				if (this.m_RightChild != null)
				{
					this.m_RightChild.Release(ref pool);
					this.m_BottomChild.Release(ref pool);
					pool.Release(this.m_RightChild);
					pool.Release(this.m_BottomChild);
				}
				this.m_RightChild = null;
				this.m_BottomChild = null;
				this.m_Rect = Vector4.zero;
			}

			// Token: 0x040007ED RID: 2029
			public AtlasAllocator.AtlasNode m_RightChild;

			// Token: 0x040007EE RID: 2030
			public AtlasAllocator.AtlasNode m_BottomChild;

			// Token: 0x040007EF RID: 2031
			public Vector4 m_Rect = new Vector4(0f, 0f, 0f, 0f);
		}
	}
}
