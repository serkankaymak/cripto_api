﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos;
public class TokenDto : IDto
{
    public string Token { get; set; }

    public TokenDto(string token)
    {
        Token = token;
    }
}
