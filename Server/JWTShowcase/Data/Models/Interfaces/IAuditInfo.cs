﻿namespace JWTShowcase.Data.Models.Interfaces;
using System;

public interface IAuditInfo
{
    DateTime CreatedOn { get; set; }

    DateTime? ModifiedOn { get; set; }
}