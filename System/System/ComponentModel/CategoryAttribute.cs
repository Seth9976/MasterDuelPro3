using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the name of the category in which to group the property or event when displayed in a <see cref="T:System.Windows.Forms.PropertyGrid" /> control set to Categorized mode.</summary>
	// Token: 0x020002B4 RID: 692
	[AttributeUsage(AttributeTargets.All)]
	public class CategoryAttribute : Attribute
	{
		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Default category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the default category.</returns>
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x0004443C File Offset: 0x0004263C
		public static CategoryAttribute Default
		{
			get
			{
				if (CategoryAttribute.defAttr == null)
				{
					CategoryAttribute.defAttr = new CategoryAttribute();
				}
				return CategoryAttribute.defAttr;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CategoryAttribute" /> class using the category name Default.</summary>
		// Token: 0x0600105E RID: 4190 RVA: 0x0004445A File Offset: 0x0004265A
		public CategoryAttribute()
			: this("Default")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CategoryAttribute" /> class using the specified category name.</summary>
		/// <param name="category">The name of the category. </param>
		// Token: 0x0600105F RID: 4191 RVA: 0x00044467 File Offset: 0x00042667
		public CategoryAttribute(string category)
		{
			this.categoryValue = category;
			this.localized = false;
		}

		/// <summary>Gets the name of the category for the property or event that this attribute is applied to.</summary>
		/// <returns>The name of the category for the property or event that this attribute is applied to.</returns>
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x00044480 File Offset: 0x00042680
		public string Category
		{
			get
			{
				if (!this.localized)
				{
					this.localized = true;
					string localizedString = this.GetLocalizedString(this.categoryValue);
					if (localizedString != null)
					{
						this.categoryValue = localizedString;
					}
				}
				return this.categoryValue;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.CategoryAttribute" />..</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001061 RID: 4193 RVA: 0x000444B9 File Offset: 0x000426B9
		public override bool Equals(object obj)
		{
			return obj == this || (obj is CategoryAttribute && this.Category.Equals(((CategoryAttribute)obj).Category));
		}

		/// <summary>Returns the hash code for this attribute.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001062 RID: 4194 RVA: 0x000444E1 File Offset: 0x000426E1
		public override int GetHashCode()
		{
			return this.Category.GetHashCode();
		}

		/// <summary>Looks up the localized name of the specified category.</summary>
		/// <returns>The localized name of the category, or null if a localized name does not exist.</returns>
		/// <param name="value">The identifer for the category to look up. </param>
		// Token: 0x06001063 RID: 4195 RVA: 0x000444F0 File Offset: 0x000426F0
		protected virtual string GetLocalizedString(string value)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(value);
			if (num <= 1062369733U)
			{
				if (num <= 630759034U)
				{
					if (num <= 433860734U)
					{
						if (num != 175614239U)
						{
							if (num == 433860734U)
							{
								if (value == "Default")
								{
									return "Misc";
								}
							}
						}
						else if (value == "Action")
						{
							return "Action";
						}
					}
					else if (num != 521774151U)
					{
						if (num == 630759034U)
						{
							if (value == "DragDrop")
							{
								return "Drag Drop";
							}
						}
					}
					else if (value == "Behavior")
					{
						return "Behavior";
					}
				}
				else if (num <= 723360612U)
				{
					if (num != 676498961U)
					{
						if (num == 723360612U)
						{
							if (value == "Mouse")
							{
								return "Mouse";
							}
						}
					}
					else if (value == "Scale")
					{
						return "Scale";
					}
				}
				else if (num != 822184863U)
				{
					if (num != 1041509726U)
					{
						if (num == 1062369733U)
						{
							if (value == "Data")
							{
								return "Data";
							}
						}
					}
					else if (value == "Text")
					{
						return "Text";
					}
				}
				else if (value == "Appearance")
				{
					return "Appearance";
				}
			}
			else if (num <= 2809814704U)
			{
				if (num <= 1779622119U)
				{
					if (num != 1762750224U)
					{
						if (num == 1779622119U)
						{
							if (value == "Config")
							{
								return "Configurations";
							}
						}
					}
					else if (value == "DDE")
					{
						return "DDE";
					}
				}
				else if (num != 2055433310U)
				{
					if (num != 2368288673U)
					{
						if (num == 2809814704U)
						{
							if (value == "Font")
							{
								return "Font";
							}
						}
					}
					else if (value == "List")
					{
						return "List";
					}
				}
				else if (value == "WindowStyle")
				{
					return "Window Style";
				}
			}
			else if (num <= 3441084684U)
			{
				if (num != 3159863731U)
				{
					if (num == 3441084684U)
					{
						if (value == "Key")
						{
							return "Key";
						}
					}
				}
				else if (value == "Focus")
				{
					return "Focus";
				}
			}
			else if (num != 3799987242U)
			{
				if (num != 3901555439U)
				{
					if (num == 4152902175U)
					{
						if (value == "Layout")
						{
							return "Layout";
						}
					}
				}
				else if (value == "Design")
				{
					return "Design";
				}
			}
			else if (value == "Position")
			{
				return "Position";
			}
			return value;
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001064 RID: 4196 RVA: 0x0004482E File Offset: 0x00042A2E
		public override bool IsDefaultAttribute()
		{
			return this.Category.Equals(CategoryAttribute.Default.Category);
		}

		// Token: 0x04000A5D RID: 2653
		private static volatile CategoryAttribute defAttr;

		// Token: 0x04000A5E RID: 2654
		private bool localized;

		// Token: 0x04000A5F RID: 2655
		private string categoryValue;
	}
}
