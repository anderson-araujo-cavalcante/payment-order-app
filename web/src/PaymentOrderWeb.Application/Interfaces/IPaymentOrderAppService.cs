﻿using Microsoft.AspNetCore.Http;

namespace PaymentOrderWeb.Application.Interfaces
{
    public interface IPaymentOrderAppService
    {
        Task Process(IEnumerable<IFormFile> files);
    }
}