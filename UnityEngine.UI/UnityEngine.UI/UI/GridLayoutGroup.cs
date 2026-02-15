using System;

namespace UnityEngine.UI
{
	// Token: 0x02000042 RID: 66
	[AddComponentMenu("Layout/Grid Layout Group", 152)]
	public class GridLayoutGroup : LayoutGroup
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000DD7F File Offset: 0x0000BF7F
		// (set) Token: 0x06000285 RID: 645 RVA: 0x0000DD87 File Offset: 0x0000BF87
		public GridLayoutGroup.Corner startCorner
		{
			get
			{
				return this.m_StartCorner;
			}
			set
			{
				base.SetProperty<GridLayoutGroup.Corner>(ref this.m_StartCorner, value);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000DD96 File Offset: 0x0000BF96
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0000DD9E File Offset: 0x0000BF9E
		public GridLayoutGroup.Axis startAxis
		{
			get
			{
				return this.m_StartAxis;
			}
			set
			{
				base.SetProperty<GridLayoutGroup.Axis>(ref this.m_StartAxis, value);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000DDAD File Offset: 0x0000BFAD
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000DDB5 File Offset: 0x0000BFB5
		public Vector2 cellSize
		{
			get
			{
				return this.m_CellSize;
			}
			set
			{
				base.SetProperty<Vector2>(ref this.m_CellSize, value);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000DDC4 File Offset: 0x0000BFC4
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0000DDCC File Offset: 0x0000BFCC
		public Vector2 spacing
		{
			get
			{
				return this.m_Spacing;
			}
			set
			{
				base.SetProperty<Vector2>(ref this.m_Spacing, value);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000DDDB File Offset: 0x0000BFDB
		// (set) Token: 0x0600028D RID: 653 RVA: 0x0000DDE3 File Offset: 0x0000BFE3
		public GridLayoutGroup.Constraint constraint
		{
			get
			{
				return this.m_Constraint;
			}
			set
			{
				base.SetProperty<GridLayoutGroup.Constraint>(ref this.m_Constraint, value);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600028E RID: 654 RVA: 0x0000DDF2 File Offset: 0x0000BFF2
		// (set) Token: 0x0600028F RID: 655 RVA: 0x0000DDFA File Offset: 0x0000BFFA
		public int constraintCount
		{
			get
			{
				return this.m_ConstraintCount;
			}
			set
			{
				base.SetProperty<int>(ref this.m_ConstraintCount, Mathf.Max(1, value));
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000DE0F File Offset: 0x0000C00F
		protected GridLayoutGroup()
		{
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000DE40 File Offset: 0x0000C040
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			int minColumns;
			int preferredColumns;
			if (this.m_Constraint == GridLayoutGroup.Constraint.FixedColumnCount)
			{
				preferredColumns = (minColumns = this.m_ConstraintCount);
			}
			else if (this.m_Constraint == GridLayoutGroup.Constraint.FixedRowCount)
			{
				preferredColumns = (minColumns = Mathf.CeilToInt((float)base.rectChildren.Count / (float)this.m_ConstraintCount - 0.001f));
			}
			else
			{
				minColumns = 1;
				preferredColumns = Mathf.CeilToInt(Mathf.Sqrt((float)base.rectChildren.Count));
			}
			base.SetLayoutInputForAxis((float)base.padding.horizontal + (this.cellSize.x + this.spacing.x) * (float)minColumns - this.spacing.x, (float)base.padding.horizontal + (this.cellSize.x + this.spacing.x) * (float)preferredColumns - this.spacing.x, -1f, 0);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000DF24 File Offset: 0x0000C124
		public override void CalculateLayoutInputVertical()
		{
			int minRows;
			if (this.m_Constraint == GridLayoutGroup.Constraint.FixedColumnCount)
			{
				minRows = Mathf.CeilToInt((float)base.rectChildren.Count / (float)this.m_ConstraintCount - 0.001f);
			}
			else if (this.m_Constraint == GridLayoutGroup.Constraint.FixedRowCount)
			{
				minRows = this.m_ConstraintCount;
			}
			else
			{
				float width = base.rectTransform.rect.width;
				int cellCountX = Mathf.Max(1, Mathf.FloorToInt((width - (float)base.padding.horizontal + this.spacing.x + 0.001f) / (this.cellSize.x + this.spacing.x)));
				minRows = Mathf.CeilToInt((float)base.rectChildren.Count / (float)cellCountX);
			}
			float minSpace = (float)base.padding.vertical + (this.cellSize.y + this.spacing.y) * (float)minRows - this.spacing.y;
			base.SetLayoutInputForAxis(minSpace, minSpace, -1f, 1);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000E023 File Offset: 0x0000C223
		public override void SetLayoutHorizontal()
		{
			this.SetCellsAlongAxis(0);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000E02C File Offset: 0x0000C22C
		public override void SetLayoutVertical()
		{
			this.SetCellsAlongAxis(1);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000E038 File Offset: 0x0000C238
		private void SetCellsAlongAxis(int axis)
		{
			int rectChildrenCount = base.rectChildren.Count;
			if (axis == 0)
			{
				for (int i = 0; i < rectChildrenCount; i++)
				{
					RectTransform rect = base.rectChildren[i];
					this.m_Tracker.Add(this, rect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
					rect.anchorMin = Vector2.up;
					rect.anchorMax = Vector2.up;
					rect.sizeDelta = this.cellSize;
				}
				return;
			}
			float width = base.rectTransform.rect.size.x;
			float height = base.rectTransform.rect.size.y;
			int cellCountX = 1;
			int cellCountY = 1;
			if (this.m_Constraint == GridLayoutGroup.Constraint.FixedColumnCount)
			{
				cellCountX = this.m_ConstraintCount;
				if (rectChildrenCount > cellCountX)
				{
					cellCountY = rectChildrenCount / cellCountX + ((rectChildrenCount % cellCountX > 0) ? 1 : 0);
				}
			}
			else if (this.m_Constraint == GridLayoutGroup.Constraint.FixedRowCount)
			{
				cellCountY = this.m_ConstraintCount;
				if (rectChildrenCount > cellCountY)
				{
					cellCountX = rectChildrenCount / cellCountY + ((rectChildrenCount % cellCountY > 0) ? 1 : 0);
				}
			}
			else
			{
				if (this.cellSize.x + this.spacing.x <= 0f)
				{
					cellCountX = int.MaxValue;
				}
				else
				{
					cellCountX = Mathf.Max(1, Mathf.FloorToInt((width - (float)base.padding.horizontal + this.spacing.x + 0.001f) / (this.cellSize.x + this.spacing.x)));
				}
				if (this.cellSize.y + this.spacing.y <= 0f)
				{
					cellCountY = int.MaxValue;
				}
				else
				{
					cellCountY = Mathf.Max(1, Mathf.FloorToInt((height - (float)base.padding.vertical + this.spacing.y + 0.001f) / (this.cellSize.y + this.spacing.y)));
				}
			}
			int cornerX = (int)(this.startCorner % GridLayoutGroup.Corner.LowerLeft);
			int cornerY = (int)(this.startCorner / GridLayoutGroup.Corner.LowerLeft);
			int cellsPerMainAxis;
			int actualCellCountX;
			int actualCellCountY;
			if (this.startAxis == GridLayoutGroup.Axis.Horizontal)
			{
				cellsPerMainAxis = cellCountX;
				actualCellCountX = Mathf.Clamp(cellCountX, 1, rectChildrenCount);
				if (this.m_Constraint == GridLayoutGroup.Constraint.FixedRowCount)
				{
					actualCellCountY = Mathf.Min(cellCountY, rectChildrenCount);
				}
				else
				{
					actualCellCountY = Mathf.Clamp(cellCountY, 1, Mathf.CeilToInt((float)rectChildrenCount / (float)cellsPerMainAxis));
				}
			}
			else
			{
				cellsPerMainAxis = cellCountY;
				actualCellCountY = Mathf.Clamp(cellCountY, 1, rectChildrenCount);
				if (this.m_Constraint == GridLayoutGroup.Constraint.FixedColumnCount)
				{
					actualCellCountX = Mathf.Min(cellCountX, rectChildrenCount);
				}
				else
				{
					actualCellCountX = Mathf.Clamp(cellCountX, 1, Mathf.CeilToInt((float)rectChildrenCount / (float)cellsPerMainAxis));
				}
			}
			Vector2 requiredSpace = new Vector2((float)actualCellCountX * this.cellSize.x + (float)(actualCellCountX - 1) * this.spacing.x, (float)actualCellCountY * this.cellSize.y + (float)(actualCellCountY - 1) * this.spacing.y);
			Vector2 startOffset = new Vector2(base.GetStartOffset(0, requiredSpace.x), base.GetStartOffset(1, requiredSpace.y));
			int childrenToMove = 0;
			if (rectChildrenCount > this.m_ConstraintCount && Mathf.CeilToInt((float)rectChildrenCount / (float)cellsPerMainAxis) < this.m_ConstraintCount)
			{
				childrenToMove = this.m_ConstraintCount - Mathf.CeilToInt((float)rectChildrenCount / (float)cellsPerMainAxis);
				childrenToMove += Mathf.FloorToInt((float)childrenToMove / ((float)cellsPerMainAxis - 1f));
				if (rectChildrenCount % cellsPerMainAxis == 1)
				{
					childrenToMove++;
				}
			}
			for (int j = 0; j < rectChildrenCount; j++)
			{
				int positionX;
				int positionY;
				if (this.startAxis == GridLayoutGroup.Axis.Horizontal)
				{
					if (this.m_Constraint == GridLayoutGroup.Constraint.FixedRowCount && rectChildrenCount - j <= childrenToMove)
					{
						positionX = 0;
						positionY = this.m_ConstraintCount - (rectChildrenCount - j);
					}
					else
					{
						positionX = j % cellsPerMainAxis;
						positionY = j / cellsPerMainAxis;
					}
				}
				else if (this.m_Constraint == GridLayoutGroup.Constraint.FixedColumnCount && rectChildrenCount - j <= childrenToMove)
				{
					positionX = this.m_ConstraintCount - (rectChildrenCount - j);
					positionY = 0;
				}
				else
				{
					positionX = j / cellsPerMainAxis;
					positionY = j % cellsPerMainAxis;
				}
				if (cornerX == 1)
				{
					positionX = actualCellCountX - 1 - positionX;
				}
				if (cornerY == 1)
				{
					positionY = actualCellCountY - 1 - positionY;
				}
				base.SetChildAlongAxis(base.rectChildren[j], 0, startOffset.x + (this.cellSize[0] + this.spacing[0]) * (float)positionX, this.cellSize[0]);
				base.SetChildAlongAxis(base.rectChildren[j], 1, startOffset.y + (this.cellSize[1] + this.spacing[1]) * (float)positionY, this.cellSize[1]);
			}
		}

		// Token: 0x04000157 RID: 343
		[SerializeField]
		protected GridLayoutGroup.Corner m_StartCorner;

		// Token: 0x04000158 RID: 344
		[SerializeField]
		protected GridLayoutGroup.Axis m_StartAxis;

		// Token: 0x04000159 RID: 345
		[SerializeField]
		protected Vector2 m_CellSize = new Vector2(100f, 100f);

		// Token: 0x0400015A RID: 346
		[SerializeField]
		protected Vector2 m_Spacing = Vector2.zero;

		// Token: 0x0400015B RID: 347
		[SerializeField]
		protected GridLayoutGroup.Constraint m_Constraint;

		// Token: 0x0400015C RID: 348
		[SerializeField]
		protected int m_ConstraintCount = 2;

		// Token: 0x02000043 RID: 67
		public enum Corner
		{
			// Token: 0x0400015E RID: 350
			UpperLeft,
			// Token: 0x0400015F RID: 351
			UpperRight,
			// Token: 0x04000160 RID: 352
			LowerLeft,
			// Token: 0x04000161 RID: 353
			LowerRight
		}

		// Token: 0x02000044 RID: 68
		public enum Axis
		{
			// Token: 0x04000163 RID: 355
			Horizontal,
			// Token: 0x04000164 RID: 356
			Vertical
		}

		// Token: 0x02000045 RID: 69
		public enum Constraint
		{
			// Token: 0x04000166 RID: 358
			Flexible,
			// Token: 0x04000167 RID: 359
			FixedColumnCount,
			// Token: 0x04000168 RID: 360
			FixedRowCount
		}
	}
}
