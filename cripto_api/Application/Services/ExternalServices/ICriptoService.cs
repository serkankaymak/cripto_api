﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ExternalServices;

public interface ICriptoService
{
    Task<List<Crypto>> GetCryptosWithTickers(DateTime dateTime);
    Task<Crypto> GetCryptoWithTickers(int cryptoId, DateTime dateTime);
}