﻿namespace TravelAgency.Domain
{
	public abstract class Entity<T>
	{
		public T Id { get; protected set; } = default!;
	}

	public abstract class Entity : Entity<long>
	{
	}
}
