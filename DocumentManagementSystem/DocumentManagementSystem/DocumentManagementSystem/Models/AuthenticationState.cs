﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Auth;

namespace DocumentManagementSystem.Models
{
    public class AuthenticationState
    {
        public static OAuth2Authenticator Authenticator { get; set; }
    }
}
