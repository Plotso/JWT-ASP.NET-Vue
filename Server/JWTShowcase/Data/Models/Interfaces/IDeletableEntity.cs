namespace JWTShowcase.Data.Models.Interfaces;
using System;

public interface IDeletableEntity
{
    bool IsDeleted { get; set; }

    DateTime? DeletedOn { get; set; }
}