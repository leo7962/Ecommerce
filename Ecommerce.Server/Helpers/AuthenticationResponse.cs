﻿namespace Ecommerce.Server.Helpers;

public class AuthenticationResponse
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}