﻿namespace JWTShowcase.Services;

public interface ICurrentTokenService
{
    string Get();

    void Set(string token);
}
