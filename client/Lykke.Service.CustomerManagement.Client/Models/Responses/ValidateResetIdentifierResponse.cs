﻿using Lykke.Service.CustomerManagement.Client.Enums;

namespace Lykke.Service.CustomerManagement.Client.Models.Responses
{
    /// <summary>
    /// Class which holds response for validation of identifier
    /// </summary>
    public class ValidateResetIdentifierResponse
    {
        /// <summary>
        /// Holds information about business errors
        /// </summary>
        public ValidateResetIdentifierError Error { get; set; }
    }
}
