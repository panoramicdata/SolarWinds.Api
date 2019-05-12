using System;

namespace SolarWinds.Api
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
	internal class TableAttribute : Attribute
	{
		public string TableName { get; }

		public TableAttribute(string tableName) => TableName = tableName;
	}
}