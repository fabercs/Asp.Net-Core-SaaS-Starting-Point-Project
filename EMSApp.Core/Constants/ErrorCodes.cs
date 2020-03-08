﻿using System.Collections.Generic;

namespace EMSApp.Core
{
    public static class Constants
    {
        public static Dictionary<string, string> Errors = new Dictionary<string, string>
        {
            { "register_password_mismatch", "PasswordMismatch" },
            { "register_email_in_use", "EmailInUse"},
            { "register_tenant_exist","TenantExist" },
            { "no_tenant", "NoTenant" },
            { "no_tenant_contact", "NoTenantContact" },
            { "no_valid_token","NoValidToken" },
            { "token_expired", "TokenExpired" },
            { "server_error","ServerError" },
            { "auth_invalid_user_pass", "InvalidUserOrPass" },
            { "auth_user_not_found","UserNotFound" },
            { "token_not_expired", "TokenNotExpired" },
            { "token_not_found", "TokenNotFound" },
            { "token_mismatch", "TokenMismatch"}
        };
    }
}