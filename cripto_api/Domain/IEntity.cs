﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain;
public interface IEntity
{
}

public interface ISoftDeleteable
{
    bool IsDeleted { get; set; }
}