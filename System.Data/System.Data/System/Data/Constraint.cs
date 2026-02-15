using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	/// <summary>Represents a constraint that can be enforced on one or more <see cref="T:System.Data.DataColumn" /> objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200002D RID: 45
	[DefaultProperty("ConstraintName")]
	[TypeConverter(typeof(ConstraintConverter))]
	public abstract class Constraint
	{
		/// <summary>The name of a constraint in the <see cref="T:System.Data.ConstraintCollection" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.Constraint" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.Constraint" /> name is a null value or empty string. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The <see cref="T:System.Data.ConstraintCollection" /> already contains a <see cref="T:System.Data.Constraint" /> with the same name (The comparison is not case-sensitive.). </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000344 RID: 836 RVA: 0x000125ED File Offset: 0x000107ED
		// (set) Token: 0x06000345 RID: 837 RVA: 0x000125F8 File Offset: 0x000107F8
		[DefaultValue("")]
		public virtual string ConstraintName
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (string.IsNullOrEmpty(value) && this.Table != null && this.InCollection)
				{
					throw ExceptionBuilder.NoConstraintName();
				}
				CultureInfo cultureInfo = ((this.Table != null) ? this.Table.Locale : CultureInfo.CurrentCulture);
				if (string.Compare(this._name, value, true, cultureInfo) != 0)
				{
					if (this.Table != null && this.InCollection)
					{
						this.Table.Constraints.RegisterName(value);
						if (this._name.Length != 0)
						{
							this.Table.Constraints.UnregisterName(this._name);
						}
					}
					this._name = value;
					return;
				}
				if (string.Compare(this._name, value, false, cultureInfo) != 0)
				{
					this._name = value;
				}
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000346 RID: 838 RVA: 0x000126BB File Offset: 0x000108BB
		// (set) Token: 0x06000347 RID: 839 RVA: 0x000126D7 File Offset: 0x000108D7
		internal string SchemaName
		{
			get
			{
				if (!string.IsNullOrEmpty(this._schemaName))
				{
					return this._schemaName;
				}
				return this.ConstraintName;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this._schemaName = value;
				}
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000348 RID: 840 RVA: 0x000126E8 File Offset: 0x000108E8
		// (set) Token: 0x06000349 RID: 841 RVA: 0x000126F0 File Offset: 0x000108F0
		internal virtual bool InCollection
		{
			get
			{
				return this._inCollection;
			}
			set
			{
				this._inCollection = value;
				this._dataSet = (value ? this.Table.DataSet : null);
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> to which the constraint applies.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> to which the constraint applies.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600034A RID: 842
		public abstract DataTable Table { get; }

		/// <summary>Gets the collection of user-defined constraint properties.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> of custom information.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00012710 File Offset: 0x00010910
		[Browsable(false)]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				PropertyCollection propertyCollection;
				if ((propertyCollection = this._extendedProperties) == null)
				{
					propertyCollection = (this._extendedProperties = new PropertyCollection());
				}
				return propertyCollection;
			}
		}

		// Token: 0x0600034C RID: 844
		internal abstract bool ContainsColumn(DataColumn column);

		// Token: 0x0600034D RID: 845
		internal abstract bool CanEnableConstraint();

		// Token: 0x0600034E RID: 846
		internal abstract Constraint Clone(DataSet destination);

		// Token: 0x0600034F RID: 847
		internal abstract Constraint Clone(DataSet destination, bool ignoreNSforTableLookup);

		// Token: 0x06000350 RID: 848 RVA: 0x00012735 File Offset: 0x00010935
		internal void CheckConstraint()
		{
			if (!this.CanEnableConstraint())
			{
				throw ExceptionBuilder.ConstraintViolation(this.ConstraintName);
			}
		}

		// Token: 0x06000351 RID: 849
		internal abstract void CheckCanAddToCollection(ConstraintCollection constraint);

		// Token: 0x06000352 RID: 850
		internal abstract bool CanBeRemovedFromCollection(ConstraintCollection constraint, bool fThrowException);

		// Token: 0x06000353 RID: 851
		internal abstract void CheckConstraint(DataRow row, DataRowAction action);

		// Token: 0x06000354 RID: 852
		internal abstract void CheckState();

		/// <summary>Gets the <see cref="T:System.Data.DataSet" /> to which this constraint belongs.</summary>
		// Token: 0x06000355 RID: 853 RVA: 0x0001274C File Offset: 0x0001094C
		protected void CheckStateForProperty()
		{
			try
			{
				this.CheckState();
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				throw ExceptionBuilder.BadObjectPropertyAccess(ex.Message);
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataSet" /> to which this constraint belongs.</summary>
		/// <returns>The <see cref="T:System.Data.DataSet" /> to which the constraint belongs.</returns>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00012798 File Offset: 0x00010998
		[CLSCompliant(false)]
		protected virtual DataSet _DataSet
		{
			get
			{
				return this._dataSet;
			}
		}

		// Token: 0x06000357 RID: 855
		internal abstract bool IsConstraintViolated();

		/// <summary>Gets the <see cref="P:System.Data.Constraint.ConstraintName" />, if there is one, as a string.</summary>
		/// <returns>The string value of the <see cref="P:System.Data.Constraint.ConstraintName" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000358 RID: 856 RVA: 0x000127A0 File Offset: 0x000109A0
		public override string ToString()
		{
			return this.ConstraintName;
		}

		// Token: 0x04000106 RID: 262
		private string _schemaName = string.Empty;

		// Token: 0x04000107 RID: 263
		private bool _inCollection;

		// Token: 0x04000108 RID: 264
		private DataSet _dataSet;

		// Token: 0x04000109 RID: 265
		internal string _name = string.Empty;

		// Token: 0x0400010A RID: 266
		internal PropertyCollection _extendedProperties;
	}
}
