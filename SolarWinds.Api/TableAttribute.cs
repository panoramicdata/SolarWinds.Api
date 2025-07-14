using System;

namespace SolarWinds.Api;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
internal class TableAttribute(string tableName) : Attribute
{
	public string TableName { get; } = tableName;
}