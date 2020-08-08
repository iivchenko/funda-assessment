﻿using System;

namespace SaleStatistics.Infrastructure.Services.Sales
{
    public sealed class FundaSettings
    {
        public const string Section = "Funda";

        public string ApiAddress { get; set; }

        public Guid Key { get; set; }
    }
}
