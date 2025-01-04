using Application.Dtos;
using MediatR;
using SendGrid;
using Shared.ApiResponse;
using Shared.Validasiton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS;




public abstract class ARequest<T> : IRequest<T>
{
    public Guid RequestId { get; }

    protected ARequest() { RequestId = Guid.NewGuid(); }

    // Türeyen sınıf, kendi kurallarına göre bu metodu implemente edecek.
    public abstract ValidationResult Validate();
}
