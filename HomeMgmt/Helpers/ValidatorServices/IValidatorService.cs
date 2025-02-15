﻿using HomeMgmt.Models.UserModels;

namespace HomeMgmt.Helpers.ValidatorServices
{
    public interface IValidatorService
    {
        void Validate(UserAccount userAccount);
        void Validate(UserRole userRole);
    }
}
