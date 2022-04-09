namespace JWTShowcase.Data.Models;
using System;
using Interfaces;

public class BaseDeletableModel<TKey> : BaseModel<TKey>, IDeletableEntity
{
    public bool IsDeleted { get; set; }

    public DateTime? DeletedOn { get; set; }
}